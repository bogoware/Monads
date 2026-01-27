#!/bin/bash
set -e

# Full documentation rebuild script
# Syncs README/CHANGELOG, generates API docs, and builds the Docusaurus site

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"
DOCS_DIR="$ROOT_DIR/docs"

echo "=== Full Documentation Rebuild ==="
echo ""

# Step 1: Sync README and CHANGELOG
echo "Step 1: Syncing README and CHANGELOG..."
node "$SCRIPT_DIR/sync-readme.js"
echo ""

# Step 2: Generate API documentation
echo "Step 2: Generating API documentation..."
"$SCRIPT_DIR/generate-api-docs.sh"
echo ""

# Step 3: Install npm dependencies
echo "Step 3: Installing npm dependencies..."
cd "$DOCS_DIR"
npm install
echo ""

# Step 4: Build the Docusaurus site
echo "Step 4: Building Docusaurus site..."
npm run build
echo ""

echo "=== Documentation Build Complete ==="
echo ""
echo "To preview the site locally, run:"
echo "  cd docs && npm run serve"
echo ""
echo "The built site is in: $DOCS_DIR/build"
