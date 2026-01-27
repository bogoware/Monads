#!/usr/bin/env node

/**
 * Syncs README.md and CHANGELOG.md from the project root to Docusaurus docs folder.
 * Adds Docusaurus frontmatter to make them compatible with the documentation site.
 */

const fs = require('fs');
const path = require('path');

const ROOT_DIR = path.join(__dirname, '..');
const DOCS_DIR = path.join(ROOT_DIR, 'docs', 'docs');

/**
 * Adds frontmatter to markdown content
 */
function addFrontmatter(content, frontmatter) {
    const frontmatterStr = Object.entries(frontmatter)
        .map(([key, value]) => `${key}: ${value}`)
        .join('\n');
    return `---\n${frontmatterStr}\n---\n\n${content}`;
}

/**
 * Transforms README.md for Docusaurus
 */
function transformReadme(content) {
    // Remove existing badges line (we'll add them in the intro.md)
    // Keep the content structure intact but adapt links
    let transformed = content;

    // Fix relative links to CHANGELOG
    transformed = transformed.replace(
        /\[CHANGELOG\]\(\.\/CHANGELOG\.md\)/g,
        '[CHANGELOG](./changelog)'
    );

    return transformed;
}

/**
 * Transforms CHANGELOG.md for Docusaurus
 */
function transformChangelog(content) {
    // The changelog can be used as-is with frontmatter
    return content;
}

// Files to sync
const filesToSync = [
    {
        source: path.join(ROOT_DIR, 'CHANGELOG.md'),
        dest: path.join(DOCS_DIR, 'changelog.md'),
        frontmatter: {
            sidebar_position: 2,
            title: 'Changelog'
        },
        transform: transformChangelog
    }
];

// Process each file
for (const file of filesToSync) {
    try {
        if (!fs.existsSync(file.source)) {
            console.warn(`Warning: Source file not found: ${file.source}`);
            continue;
        }

        let content = fs.readFileSync(file.source, 'utf8');

        if (file.transform) {
            content = file.transform(content);
        }

        content = addFrontmatter(content, file.frontmatter);

        fs.writeFileSync(file.dest, content, 'utf8');
        console.log(`Synced: ${path.basename(file.source)} -> ${path.relative(ROOT_DIR, file.dest)}`);
    } catch (error) {
        console.error(`Error syncing ${file.source}: ${error.message}`);
        process.exit(1);
    }
}

console.log('Sync complete!');
