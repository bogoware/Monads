# Documentation Site

This project uses [Docusaurus 3](https://docusaurus.io/) to generate a documentation website with API reference auto-generated from XML documentation comments.

**Live Site:** https://bogoware.github.io/Monads/

## Prerequisites

- Node.js 20.x or later
- .NET SDK 8.0, 9.0, or 10.0
- npm (comes with Node.js)

## Quick Start

```bash
# Install dependencies
cd docs && npm install

# Start development server
npm run start
# Site available at http://localhost:3000/monads/
```

## Project Structure

```
docs/
├── docusaurus.config.ts    # Site configuration
├── sidebars.ts             # Navigation configuration
├── package.json            # Node.js dependencies
├── static/                 # Static assets (images, .nojekyll)
├── src/
│   └── css/custom.css      # Custom styles
└── docs/                   # Documentation content
    ├── intro.md            # Landing page (slug: /)
    ├── changelog.md        # Project changelog
    ├── getting-started/    # Installation and setup guides
    ├── concepts/           # Core concepts (Result, Maybe, Errors)
    ├── guides/             # How-to guides
    └── api/                # Auto-generated API reference

scripts/
├── sync-readme.js          # Sync CHANGELOG.md to docs
├── generate-api-docs.sh    # Generate API docs from XML
├── rebuild-docs.sh         # Full rebuild script
└── dev-docs.sh             # Start dev server

.config/
└── dotnet-tools.json       # .NET tool manifest (xmldoc2md)
```

## Manual Build Commands

### 1. Install Dependencies

```bash
# Restore .NET tools (xmldoc2md)
dotnet tool restore

# Install npm dependencies
cd docs && npm install
```

### 2. Generate API Documentation

The API documentation is auto-generated from XML documentation comments in the C# source code.

```bash
# Build .NET project with XML documentation
dotnet build -c Release

# Find the generated DLL and XML
DLL_FILE=$(find src -name "Bogoware.Monads.dll" -path "*/bin/Release/*" | grep -v "/ref/" | head -1)

# Generate markdown API docs
dotnet xmldoc2md "$DLL_FILE" -o docs/docs/api --index-page-name index --github-pages

# Add frontmatter to generated files (required for Docusaurus)
cd docs/docs/api
for file in *.md; do
    if [ -f "$file" ]; then
        filename=$(basename "$file" .md)
        if [ "$filename" = "index" ]; then
            title="API Reference"
            position=1
        else
            title="${filename#bogoware.monads.}"
            position=99
        fi
        temp_file=$(mktemp)
        echo "---" > "$temp_file"
        echo "title: \"$title\"" >> "$temp_file"
        echo "sidebar_position: $position" >> "$temp_file"
        echo "---" >> "$temp_file"
        echo "" >> "$temp_file"
        cat "$file" >> "$temp_file"
        mv "$temp_file" "$file"
    fi
done

# Fix <br> tags for MDX compatibility
sed -i '' 's/<br>/<br \/>/g' *.md
```

Or use the provided script:

```bash
./scripts/generate-api-docs.sh
```

### 3. Sync Changelog

```bash
node scripts/sync-readme.js
```

### 4. Build the Site

```bash
cd docs

# Development build with hot reload
npm run start

# Production build
npm run build

# Preview production build locally
npm run serve
```

### 5. Full Rebuild (All Steps)

```bash
./scripts/rebuild-docs.sh
```

## Development Workflow

### Local Development

```bash
# Start dev server with hot reload
./scripts/dev-docs.sh
# or
cd docs && npm run start
```

The site will be available at http://localhost:3000/monads/

### Adding Documentation

1. **New concept/guide**: Create `.md` file in appropriate folder (`docs/docs/concepts/` or `docs/docs/guides/`)
2. **Add frontmatter** at the top:
   ```yaml
   ---
   sidebar_position: 1
   title: Your Page Title
   ---
   ```
3. The sidebar auto-generates from the folder structure

### Updating API Documentation

API docs are regenerated from XML comments. To update:

1. Update XML documentation comments in C# source files
2. Run `./scripts/generate-api-docs.sh`
3. Or wait for CI to regenerate on merge to `rel/prod`

## CI/CD Deployment

The documentation is automatically built and deployed via GitHub Actions when:

- Push to `rel/prod` branch
- A release is published
- Manual workflow dispatch

**Workflow file:** `.github/workflows/docs.yml`

### Enabling GitHub Pages

1. Go to repository **Settings** > **Pages**
2. Under "Build and deployment", select **Source: GitHub Actions**
3. The site will deploy automatically on the next push to `rel/prod`

## Troubleshooting

### MDX Compilation Errors

If you see errors about `<br>` or `<T>` tags:

```bash
# Fix <br> tags in API docs
cd docs/docs/api
sed -i '' 's/<br>/<br \/>/g' *.md

# Escape generic types in markdown links
# Use: [Result\<T\>](./path) instead of [Result<T>](./path)
```

### Build Fails with Missing XML Documentation

Ensure the project generates XML documentation:

```xml
<!-- In Monads.csproj -->
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>
```

### Broken Links

Run the build to check for broken links:

```bash
cd docs && npm run build
```

Docusaurus will report any broken internal links.

### Cache Issues

Clear Docusaurus cache if you see stale content:

```bash
cd docs
rm -rf .docusaurus .cache-loader build
npm run build
```

## Configuration Reference

### Site Configuration (`docusaurus.config.ts`)

| Setting | Value | Description |
|---------|-------|-------------|
| `url` | `https://bogoware.github.io` | Base URL |
| `baseUrl` | `/monads/` | Path prefix |
| `routeBasePath` | `/` | Docs at root (docs-only mode) |
| `markdown.format` | `detect` | Auto-detect MDX vs CommonMark |

### Sidebar Configuration (`sidebars.ts`)

- `docsSidebar`: Main documentation (intro, changelog, getting-started, concepts, guides)
- `apiSidebar`: Auto-generated API reference

## Tools Used

| Tool | Version | Purpose |
|------|---------|---------|
| Docusaurus | 3.9.x | Static site generator |
| xmldoc2markdown | 5.0.0 | Generate markdown from XML docs |
| Prism | (bundled) | C# syntax highlighting |
