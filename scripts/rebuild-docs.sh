#!/bin/bash
set -e

# Full documentation rebuild script
# Syncs CHANGELOG, generates API docs, and builds the Starlight site

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"
DOCS_DIR="$ROOT_DIR/docs"

echo "=== Full Documentation Rebuild ==="
echo ""

# Step 1: Sync CHANGELOG
echo "Step 1: Syncing CHANGELOG..."
node "$SCRIPT_DIR/sync-readme.js"
echo ""

# Step 2: Generate API documentation
echo "Step 2: Generating API documentation..."
"$SCRIPT_DIR/generate-api-docs.sh"
echo ""

# Step 3: Install dependencies
echo "Step 3: Installing dependencies..."
cd "$DOCS_DIR"
pnpm install
echo ""

# Step 4: Build the Starlight site
echo "Step 4: Building Starlight site..."
pnpm build
echo ""

echo "=== Documentation Build Complete ==="
echo ""
echo "To preview the site locally, run:"
echo "  cd docs && pnpm -C sites/monads preview"
echo ""
echo "The built site is in: $DOCS_DIR/sites/monads/dist"
