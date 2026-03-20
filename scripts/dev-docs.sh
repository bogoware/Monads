#!/bin/bash
set -e

# Start the Starlight development server

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"
DOCS_DIR="$ROOT_DIR/website"

echo "=== Starting Documentation Development Server ==="
echo ""

# Install dependencies if needed
if [ ! -d "$DOCS_DIR/node_modules" ]; then
    echo "Installing dependencies..."
    cd "$DOCS_DIR"
    pnpm install
fi

# Start the development server
cd "$DOCS_DIR"
echo "Starting Starlight dev server..."
echo "The site will be available at: http://localhost:4321/Monads/"
echo ""
pnpm dev
