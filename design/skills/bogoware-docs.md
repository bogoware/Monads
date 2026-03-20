---
name: bogoware-docs
description: Implement, migrate, and review Bogoware documentation sites using @bogoware/starlight-theme
---

# Bogoware Documentation Site Implementation

Use this skill when setting up, migrating, or reviewing Bogoware documentation sites.

## Prerequisites

- Node.js >= 20
- pnpm (preferred) or npm
- @bogoware/starlight-theme published on NPM

## Quick Setup (New Site)

### 1. Create project structure

```bash
mkdir -p website/src/content/docs
cd website
```

### 2. Package.json

```json
{
  "name": "@bogoware/<project-name>-docs",
  "private": true,
  "type": "module",
  "scripts": {
    "dev": "astro dev",
    "build": "astro build",
    "preview": "astro preview"
  },
  "dependencies": {
    "@astrojs/starlight": "^0.30.0",
    "astro": "^5.17.0",
    "@bogoware/starlight-theme": "^0.1.1",
    "@astrojs/sitemap": "^3.2.0"
  }
}
```

### 3. Astro Config

```javascript
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  site: 'https://bogoware.github.io',
  base: '/<ProjectName>/',
  integrations: [
    starlight({
      title: 'Bogoware.<ProjectName>',
      plugins: [
        bogowareTheme({
          mode: 'architect',  // 'architect' for technical docs, 'florentine' for blog
          logoVariant: 'simplified',
          fieldLines: true,
          seo: {
            siteName: 'Bogoware.<ProjectName>',
            defaultDescription: '<Project description>',
            structuredData: { type: 'SoftwareSourceCode', author: 'Bogoware' },
          },
          // analytics: { googleAnalyticsId: 'G-XXXXXXXXXX' },
        }),
      ],
      social: {
        github: 'https://github.com/bogoware/<project-name>',
      },
      sidebar: [
        { label: 'Introduction', slug: '' },
        { label: 'Changelog', slug: 'changelog' },
        { label: 'Getting Started', autogenerate: { directory: 'getting-started' } },
        { label: 'Concepts', autogenerate: { directory: 'concepts' } },
        { label: 'Guides', autogenerate: { directory: 'guides' } },
        { label: 'API Reference', autogenerate: { directory: 'api' } },
      ],
    }),
  ],
});
```

### 4. Content Collection Config

```typescript
// src/content.config.ts
import { defineCollection } from 'astro:content';
import { docsSchema } from '@astrojs/starlight/schema';

export const collections = {
  docs: defineCollection({ schema: docsSchema() }),
};
```

### 5. Homepage (src/content/docs/index.md)

```markdown
---
title: Bogoware.<ProjectName>
description: <Short description>
template: splash
hero:
  title: Bogoware.<ProjectName>
  tagline: <Tagline>
  actions:
    - text: Get Started
      link: /<ProjectName>/getting-started/installation/
      icon: right-arrow
    - text: View on NuGet
      link: https://www.nuget.org/packages/Bogoware.<ProjectName>
      variant: minimal
---
```

## Migration from Docusaurus

### Frontmatter Conversion

| Docusaurus | Starlight |
|---|---|
| `sidebar_position: N` | `sidebar: { order: N }` |
| `sidebar_label: "Text"` | `sidebar: { label: "Text" }` |
| `title: "Text"` | `title: "Text"` (same) |
| No description | Add `description:` field |

### Admonitions
Starlight uses the same `:::note`, `:::tip`, `:::caution`, `:::danger` syntax as Docusaurus. Exception: `:::warning` must become `:::caution`.

### Internal Links
- Docusaurus: `[text](./path.md)` or `[text](../path.md)`
- Starlight: Same format works, but extensionless `[text](./path/)` is preferred

### API Docs Pipeline
Update `scripts/generate-api-docs.sh`:
- Output path: `website/src/content/docs/api/`
- Frontmatter format: `sidebar: { order: N }` instead of `sidebar_position: N`

### CI/CD (GitHub Actions)
Update `.github/workflows/docs.yml`:
- Use pnpm instead of npm
- Build command: `cd website && pnpm build`
- Artifact path: `website/dist`

## Typography Modes

| Mode | Display | Body | Code | Use For |
|---|---|---|---|---|
| **architect** | Space Grotesk | DM Sans | Space Mono | Library docs, API reference |
| **florentine** | Cormorant Garamond | Crimson Pro | Fira Code | Blog, essays, articles |

## Brand Compliance Checklist

When reviewing a Bogoware documentation site, verify:

- [ ] Uses `@bogoware/starlight-theme` as a Starlight plugin
- [ ] Correct mode: `architect` for technical docs, `florentine` for blog
- [ ] Logo variant is `simplified` (default) or `full`
- [ ] Site URL and base path are correct for GitHub Pages
- [ ] SEO config includes siteName and defaultDescription
- [ ] Social link points to correct GitHub repo
- [ ] Sidebar structure follows the standard pattern (Intro, Changelog, Getting Started, Concepts, Guides, API)
- [ ] All pages have `title` and `description` in frontmatter
- [ ] API docs are auto-generated from xmldoc2md (not hand-written)
- [ ] Changelog is synced from root CHANGELOG.md
- [ ] GitHub Actions workflow uses pnpm + Astro build
- [ ] No Docusaurus remnants (docusaurus.config.ts, sidebars.ts, src/css/custom.css)

## Color Palette Reference

| Token | Dark | Light | Usage |
|---|---|---|---|
| `--bw-indigo` | #6366f1 | #4f46e5 | Primary brand |
| `--bw-violet` | #8b5cf6 | #7c3aed | Accent gradient |
| `--bw-cyan` | #99f0ff | #06b6d4 | Secondary path |
| `--bw-bg` | #0f172a | #f8fafc | Background |
| `--bw-surface` | #1e293b | #ffffff | Surface/cards |
| `--bw-border` | #334155 | #e2e8f0 | Borders |
| `--bw-text` | #e2e8f0 | #1e293b | Body text |
| `--bw-text-muted` | #94a3b8 | #64748b | Muted text |

## Logo Variants

| Variant | File | Use When |
|---|---|---|
| Full (dark) | logo-dark.svg | Hero sections, large displays on dark bg |
| Full (light) | logo-light.svg | Large displays on light bg |
| Simplified | logo-mark.svg | Navbar, small contexts |
| Favicon | logo-favicon.svg | Browser tab, 16-32px |
| Monochrome | logo-monochrome.svg | Single-color print, high contrast |
