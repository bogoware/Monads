#!/usr/bin/env node

/**
 * Migrates Docusaurus frontmatter to Starlight frontmatter format.
 * - sidebar_position: N  →  sidebar: { order: N }
 * - Removes slug field (Starlight uses file-based routing)
 * - Removes sidebar_label (not used in Starlight)
 */

const fs = require('fs');
const path = require('path');

const DOCS_DIR = path.join(__dirname, '..', 'docs', 'sites', 'monads', 'src', 'content', 'docs');

function parseFrontmatter(content) {
  const match = content.match(/^---\n([\s\S]*?)\n---\n/);
  if (!match) return { frontmatter: {}, body: content };

  const frontmatterStr = match[1];
  const body = content.slice(match[0].length);
  const frontmatter = {};

  for (const line of frontmatterStr.split('\n')) {
    const colonIdx = line.indexOf(':');
    if (colonIdx === -1) continue;
    const key = line.slice(0, colonIdx).trim();
    let value = line.slice(colonIdx + 1).trim();
    // Remove surrounding quotes
    if ((value.startsWith('"') && value.endsWith('"')) ||
        (value.startsWith("'") && value.endsWith("'"))) {
      value = value.slice(1, -1);
    }
    frontmatter[key] = value;
  }

  return { frontmatter, body };
}

function buildFrontmatter(fm) {
  const lines = ['---'];
  for (const [key, value] of Object.entries(fm)) {
    if (typeof value === 'object') {
      lines.push(`${key}:`);
      for (const [subKey, subValue] of Object.entries(value)) {
        lines.push(`  ${subKey}: ${subValue}`);
      }
    } else if (typeof value === 'string' && (value.includes(':') || value.includes('<') || value.includes('>'))) {
      lines.push(`${key}: "${value}"`);
    } else {
      lines.push(`${key}: ${value}`);
    }
  }
  lines.push('---');
  return lines.join('\n');
}

function convertFrontmatter(oldFm) {
  const newFm = {};

  // Keep title
  if (oldFm.title) {
    newFm.title = oldFm.title;
  }

  // Add description if present
  if (oldFm.description) {
    newFm.description = oldFm.description;
  }

  // Convert sidebar_position to sidebar.order
  if (oldFm.sidebar_position) {
    newFm.sidebar = { order: parseInt(oldFm.sidebar_position, 10) };
  }

  // Skip: slug, sidebar_label (Docusaurus-specific)
  return newFm;
}

function processFile(filePath) {
  const content = fs.readFileSync(filePath, 'utf8');
  const { frontmatter, body } = parseFrontmatter(content);

  if (Object.keys(frontmatter).length === 0) return false;

  const newFm = convertFrontmatter(frontmatter);
  const newContent = buildFrontmatter(newFm) + '\n' + body;

  fs.writeFileSync(filePath, newContent, 'utf8');
  return true;
}

function walkDir(dir) {
  const entries = fs.readdirSync(dir, { withFileTypes: true });
  const files = [];
  for (const entry of entries) {
    const fullPath = path.join(dir, entry.name);
    if (entry.isDirectory()) {
      files.push(...walkDir(fullPath));
    } else if (entry.name.endsWith('.md')) {
      files.push(fullPath);
    }
  }
  return files;
}

// Process all files
const files = walkDir(DOCS_DIR);
let count = 0;
for (const file of files) {
  const relPath = path.relative(DOCS_DIR, file);
  if (processFile(file)) {
    console.log(`Converted: ${relPath}`);
    count++;
  } else {
    console.log(`Skipped (no frontmatter): ${relPath}`);
  }
}

console.log(`\nDone. Converted ${count} files.`);
