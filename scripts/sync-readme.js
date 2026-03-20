#!/usr/bin/env node

/**
 * Syncs CHANGELOG.md from the project root to the Starlight docs folder.
 * Adds Starlight-compatible frontmatter.
 */

const fs = require('fs');
const path = require('path');

const ROOT_DIR = path.join(__dirname, '..');
const DOCS_DIR = path.join(ROOT_DIR, 'website', 'src', 'content', 'docs');

/**
 * Adds YAML frontmatter to markdown content (supports nested objects)
 */
function addFrontmatter(content, frontmatter) {
    function serializeYaml(obj, indent = 0) {
        const prefix = '  '.repeat(indent);
        const lines = [];
        for (const [key, value] of Object.entries(obj)) {
            if (typeof value === 'object' && value !== null) {
                lines.push(`${prefix}${key}:`);
                lines.push(serializeYaml(value, indent + 1));
            } else {
                lines.push(`${prefix}${key}: ${value}`);
            }
        }
        return lines.join('\n');
    }
    return `---\n${serializeYaml(frontmatter)}\n---\n\n${content}`;
}

/**
 * Transforms README.md for Starlight
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
 * Transforms CHANGELOG.md for Starlight
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
            title: 'Changelog',
            sidebar: { order: 2 }
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
