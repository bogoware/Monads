#!/bin/bash
set -e

# Start the Docusaurus development server

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(dirname "$SCRIPT_DIR")"
DOCS_DIR="$ROOT_DIR/docs"

echo "=== Starting Documentation Development Server ==="
echo ""

# Install dependencies if needed
if [ ! -d "$DOCS_DIR/node_modules" ]; then
    echo "Installing npm dependencies..."
    cd "$DOCS_DIR"
    npm install
fi

# Start the development server
cd "$DOCS_DIR"
echo "Starting Docusaurus dev server..."
echo "The site will be available at: http://localhost:3000/monads/"
echo ""
npm run start
