# Bogoware Brand & Documentation Ecosystem — Design Specification

**Version:** 1.0.0
**Date:** 2026-03-20
**Status:** Draft
**Author:** MrBogomips

---

## 1. Executive Summary

This document specifies the design system and migration plan for the Bogoware documentation ecosystem. The project migrates two existing Docusaurus 3.9.2 sites (Bogoware.Monads and Bogoware.Localization) and one nascent Astro 5.x blog to a unified platform built on **Astro + Starlight**, bound together by a shared, publishable NPM theme package (`@bogoware/starlight-theme`).

The theme implements the **Bogoware brand identity** — anchored by the Bogoliubov Vertex logo — and exposes a **dual typography system**: *Architect Mode* for technical documentation and *Florentine Mode* for long-form writing. All three consumer sites share the same color palette, logo variants, spacing scale, and component library while differing only in typographic personality.

### Goals

1. **Brand coherence** — A single visual identity across all Bogoware open-source projects and the personal blog.
2. **Modern tooling** — Replace Docusaurus with Astro/Starlight for faster builds, better DX, and native partial hydration.
3. **Reusable theme** — Publish `@bogoware/starlight-theme` to NPM so any future Bogoware project can adopt the brand in one dependency.
4. **Zero content loss** — Migrate all 42 Monads pages, 22 Localization pages, and 2 blog posts without URL breakage.
5. **Automated API docs** — Preserve the `xmldoc2md` pipeline, adapting only the frontmatter post-processing.
6. **SEO/GEO excellence** — Structured data, semantic HTML, Open Graph / Twitter Card meta, sitemap, robots.txt, canonical URLs.
7. **Social platform integration** — Rich link previews with branded OG images, auto-generated per page.
8. **Analytics** — Optional Google Analytics (GA4) integration via configurable Measurement ID.

---

## 2. Brand Identity

### 2.1 Logo: The Bogoliubov Vertex

#### Origin

The mark draws from the **Bogoliubov transformation** in quantum field theory — specifically the vertex where two operators cross. In Feynman-diagram notation this crossing represents a fundamental interaction point: creation and annihilation operators exchanging identity. The logo distills this into a minimal geometric form: two curves crossing at a central vertex with C2 rotational symmetry.

#### Geometry

Two cubic Bezier curves intersect at the origin. Each curve sweeps from top-left to bottom-right (and its C2 rotation), creating an X-like crossing with organic, non-linear tension. Strokes taper from 6px at the endpoints down to 4px at the vertex, then back to 6px — evoking particle world-lines narrowing at an interaction point.

**Construction rules:**

| Parameter | Value |
|---|---|
| Viewbox | `0 0 120 120` |
| Center | `(60, 60)` |
| Vertex radius | 7px (the zone where strokes are thinnest) |
| Stroke taper | 6 → 4 → 6 (endpoint → vertex → endpoint) |
| Symmetry | C2 rotational (180-degree) — no mirror, no rotation |
| Curve type | Cubic Bezier |
| Field lines | 4 lines, stroke-width 1.8, opacity 0.08–0.12 |

#### SVG Path Data

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <!-- Container circle (full variant only) -->
  <circle cx="60" cy="60" r="56" stroke="currentColor" stroke-width="1.5"
          opacity="0.15" fill="none"/>

  <!-- Field lines (full variant only) -->
  <line x1="60" y1="4"  x2="60" y2="116" stroke="currentColor"
        stroke-width="1.8" opacity="0.08"/>
  <line x1="4"  y1="60" x2="116" y2="60" stroke="currentColor"
        stroke-width="1.8" opacity="0.10"/>
  <line x1="18" y1="18" x2="102" y2="102" stroke="currentColor"
        stroke-width="1.8" opacity="0.08"/>
  <line x1="102" y1="18" x2="18" y2="102" stroke="currentColor"
        stroke-width="1.8" opacity="0.12"/>

  <!-- Primary curve A: top-left → bottom-right -->
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104"
        stroke="url(#grad-a)" stroke-width="6" stroke-linecap="round"
        fill="none">
    <!-- Taper: simulated via stroke-dasharray or variable-width path -->
  </path>

  <!-- Primary curve B: C₂ rotation of A (bottom-left → top-right) -->
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16"
        stroke="url(#grad-b)" stroke-width="6" stroke-linecap="round"
        fill="none"/>

  <!-- Vertex accent dot -->
  <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9"/>

  <!-- Gradient definitions -->
  <defs>
    <linearGradient id="grad-a" x1="20" y1="16" x2="100" y2="104"
                    gradientUnits="userSpaceOnUse">
      <stop offset="0%"   stop-color="#6366f1"/>
      <stop offset="50%"  stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#99f0ff"/>
    </linearGradient>
    <linearGradient id="grad-b" x1="20" y1="104" x2="100" y2="16"
                    gradientUnits="userSpaceOnUse">
      <stop offset="0%"   stop-color="#99f0ff"/>
      <stop offset="50%"  stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#6366f1"/>
    </linearGradient>
  </defs>
</svg>
```

#### Variants

| Variant | Description | Use Case |
|---|---|---|
| **Full** | Container circle + 4 field lines + vertex curves + accent dot | Hero sections, splash screens, OG cards |
| **Simplified** | Vertex curves + accent dot only (no container, no field lines) | Favicon, navbar, small contexts |
| **Monochrome** | Single-color strokes; curve A = solid, curve B = dashed (2,3) | Print, low-color environments |
| **Light** | Gradient strokes on transparent background, optimized for light BG | Light mode header |
| **Dark** | Gradient strokes on transparent background, optimized for dark BG | Dark mode header |

#### Monochrome Accessibility

The monochrome variant uses solid vs. dashed stroke differentiation so that the two curves remain distinguishable without color. Curve A uses a continuous stroke; curve B uses `stroke-dasharray="6,4"`. This satisfies WCAG 1.4.1 (Use of Color).

### 2.2 Color System

#### Primary Palette

| Token | Hex | Role |
|---|---|---|
| `--bw-indigo` | `#6366f1` | Primary brand, links, CTA |
| `--bw-violet` | `#8b5cf6` | Gradient endpoint, accents |
| `--bw-cyan` | `#99f0ff` | Secondary brand, highlights |
| `--bw-periwinkle` | `#b0a0ff` | Gradient midpoint, soft accent |

#### Dark Mode

| Token | Hex | Role |
|---|---|---|
| `--bw-bg` | `#0f172a` | Page background (Slate 900) |
| `--bw-surface` | `#1e293b` | Card/sidebar background (Slate 800) |
| `--bw-border` | `#334155` | Borders, dividers (Slate 700) |
| `--bw-text` | `#e2e8f0` | Primary text (Slate 200) |
| `--bw-text-muted` | `#94a3b8` | Secondary text (Slate 400) |

#### Light Mode

| Token | Hex | Role |
|---|---|---|
| `--bw-bg` | `#f8fafc` | Page background (Slate 50) |
| `--bw-surface` | `#ffffff` | Card/sidebar background |
| `--bw-border` | `#e2e8f0` | Borders, dividers (Slate 200) |
| `--bw-text` | `#1e293b` | Primary text (Slate 800) |
| `--bw-text-muted` | `#64748b` | Secondary text (Slate 500) |
| `--bw-indigo-deep` | `#4f46e5` | Deeper indigo for light BG contrast |
| `--bw-violet-deep` | `#7c3aed` | Deeper violet for light BG contrast |
| `--bw-cyan-deep` | `#06b6d4` | Deeper cyan for light BG contrast |

#### Code Syntax Accents

| Token | Hex | Role |
|---|---|---|
| `--bw-code-cyan` | `#67e8f9` | Keywords, types |
| `--bw-code-mint` | `#86efac` | Strings, literals |
| `--bw-code-indigo` | `#a5b4fc` | Functions, methods |
| `--bw-code-amber` | `#fcd34d` | Numbers, constants |
| `--bw-code-rose` | `#fda4af` | Errors, deletions |

#### CMYK / Print Fallbacks

| Use | Specification |
|---|---|
| Primary brand | Pantone 2728 C (close to Indigo #6366f1) |
| Secondary accent | Pantone 3115 C (close to Cyan #99f0ff) |
| Dark backgrounds | Pantone Black 6 C |
| Paper stock | Uncoated for technical docs, coated for marketing |

### 2.3 Typography — Dual System

The theme ships two complete typographic stacks. The consumer site declares which mode to use; the theme loads only the relevant font files.

#### Architect Mode (Technical Documentation)

For API references, library guides, and technical writing. Geometric sans-serifs convey precision; the monospace font aligns with code-heavy content.

| Role | Family | Weight(s) | Size Scale | Line Height |
|---|---|---|---|---|
| Display / H1 | Space Grotesk | 700, 800 | 36 / 48px | 1.15 |
| H2 | Space Grotesk | 700 | 28 / 36px | 1.2 |
| H3 | Space Grotesk | 600 | 22 / 28px | 1.25 |
| Body | DM Sans | 400, 500 | 16 / 18px | 1.65 |
| Small / Caption | DM Sans | 400 | 14px | 1.5 |
| Code (inline) | Space Mono | 400 | 14px | 1.6 |
| Code (block) | Space Mono | 400 | 14px | 1.7 |
| Fallback stack | `-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif` | — | — | — |

#### Florentine Mode (Blog & Articles)

For long-form essays, personal writing, and narrative content. Serifs improve sustained reading; the italic variant of Crimson Pro adds voice.

| Role | Family | Weight(s) | Size Scale | Line Height |
|---|---|---|---|---|
| Display / H1 | Cormorant Garamond | 600, 700 | 40 / 52px | 1.15 |
| H2 | Cormorant Garamond | 600 | 32 / 40px | 1.2 |
| H3 | Cormorant Garamond | 500 | 24 / 32px | 1.25 |
| Body | Crimson Pro | 400, 400i | 18 / 20px | 1.75 |
| Small / Caption | Crimson Pro | 400 | 15px | 1.5 |
| Code (inline) | Fira Code | 400 | 15px | 1.6 |
| Code (block) | Fira Code | 400 | 15px | 1.7 |
| Fallback stack | `Georgia, "Times New Roman", serif` | — | — | — |

#### Font Loading Strategy

All fonts are sourced from **Google Fonts** under the **OFL 1.1** license. The theme includes:

1. **Preconnect** hints for `fonts.googleapis.com` and `fonts.gstatic.com`.
2. **Self-hosted WOFF2 fallback** files in `assets/fonts/` for offline/CI builds.
3. **`font-display: swap`** to prevent FOIT (Flash of Invisible Text).
4. **Subsetting** via `&text=` parameter for display fonts (Latin only, ~30 KB per weight).

### 2.4 Spacing & Layout

#### Base Unit

All spacing derives from a **4px base unit**. The scale follows powers of 2:

| Token | Value | Usage |
|---|---|---|
| `--bw-space-1` | 4px | Tight gaps, icon padding |
| `--bw-space-2` | 8px | Inline element spacing |
| `--bw-space-3` | 12px | Small component padding |
| `--bw-space-4` | 16px | Standard component padding |
| `--bw-space-6` | 24px | Section gaps, card padding |
| `--bw-space-8` | 32px | Large section gaps |
| `--bw-space-12` | 48px | Page section separators |
| `--bw-space-16` | 64px | Hero/footer vertical padding |

#### Layout Dimensions

| Dimension | Value | Context |
|---|---|---|
| Content max-width (reading) | 720px | Prose content, blog posts |
| Content max-width (wide) | 1200px | API reference tables, code examples |
| Sidebar width | 240px | Desktop sidebar panel |
| Sidebar collapsed width | 0px | Mobile — sidebar becomes overlay |
| TOC width | 200px | Table of contents rail (right side) |
| Navbar height | 64px | Fixed top navigation |
| Breakpoint: mobile | < 768px | Single-column, hamburger nav |
| Breakpoint: tablet | 768–1023px | Sidebar overlay, content full-width |
| Breakpoint: desktop | >= 1024px | Sidebar + content + TOC three-column |

---

## 3. Architecture

### 3.1 Theme Package: @bogoware/starlight-theme

The theme is an **NPM-scoped package** that implements the Starlight plugin API. It provides CSS custom properties, component overrides, font loading, and logo assets. Consumer sites depend on it as a normal NPM dependency.

#### Config API

```typescript
// astro.config.mjs of a consumer site
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  integrations: [
    starlight({
      title: 'Bogoware.Monads',
      plugins: [
        bogowareTheme({
          // Typography mode: 'architect' for technical docs,
          // 'florentine' for blog/essays
          mode: 'architect',

          // Logo variant displayed in the navbar
          // 'full' | 'simplified' | 'monochrome'
          logoVariant: 'simplified',

          // Override the primary accent color (defaults to --bw-indigo)
          accentColor: undefined,

          // Enable field-line decorative background on hero sections
          fieldLines: true,

          // ─── SEO & Social ───────────────────────────────
          seo: {
            // Site-level defaults (can be overridden per-page via frontmatter)
            siteName: 'Bogoware.Monads',
            defaultDescription: 'Functional programming patterns for C#',
            locale: 'en_US',
            // Auto-generate OG images with branded template
            ogImage: {
              enabled: true,
              // 'dynamic' generates per-page; 'static' uses a single default
              strategy: 'dynamic',
            },
            // Twitter/X card type
            twitterCard: 'summary_large_image',
            twitterSite: '@bogoware',
            // JSON-LD structured data
            structuredData: {
              type: 'SoftwareSourceCode',  // or 'Blog', 'WebSite'
              author: 'Bogoware',
            },
          },

          // ─── Analytics ──────────────────────────────────
          analytics: {
            // Google Analytics 4 Measurement ID (optional)
            googleAnalyticsId: undefined,  // e.g. 'G-XXXXXXXXXX'
            // Respect DNT (Do Not Track) header
            respectDnt: true,
            // Cookie-less mode (no cookies, IP anonymization)
            cookieless: false,
          },
        }),
      ],
    }),
  ],
});
```

#### Plugin Entry Point

The plugin entry (`index.ts`) will:

1. Inject the appropriate CSS file based on `mode`.
2. Register component overrides via `overrides` config.
3. Add `<link rel="preconnect">` hints for Google Fonts.
4. Register the logo asset based on `logoVariant` and color mode.
5. Inject SEO `<Head>` overrides: Open Graph meta, Twitter Card meta, JSON-LD structured data, canonical URLs.
6. If `analytics.googleAnalyticsId` is set, inject the GA4 `gtag.js` snippet with DNT/cookieless options.
7. Register the sitemap integration (`@astrojs/sitemap`) and robots.txt generation.
8. Register the OG image generation integration (using `@vercel/og` or `satori` + `sharp` for dynamic branded OG images).

### 3.2 Package Structure

```
packages/starlight-theme-bogoware/
├── index.ts                    # Starlight plugin entry point
├── integration.ts              # Astro integration hooks (font preconnect, etc.)
├── schema.ts                   # Zod schema for plugin options
├── styles/
│   ├── base.css                # Shared: palette tokens, spacing scale, layout grid,
│   │                           #   logo sizing, border radii, transitions
│   ├── architect.css           # Space Grotesk + DM Sans + Space Mono
│   │                           #   @font-face declarations, heading/body rules
│   └── florentine.css          # Cormorant Garamond + Crimson Pro + Fira Code
│                               #   @font-face declarations, heading/body rules
├── overrides/
│   ├── Header.astro            # Custom header: Bogoware logo, gradient accent bar
│   ├── Hero.astro              # Custom hero: field emanation background,
│   │                           #   gradient title, tagline, CTA buttons
│   ├── Sidebar.astro           # Styled sidebar: brand colors, active-state indicator
│   ├── Footer.astro            # Branded footer: copyright, links, logo mark
│   ├── Head.astro              # Font preconnect, favicon, SEO meta, GA4 script
│   └── SocialMeta.astro        # OG/Twitter Card meta tags, JSON-LD structured data
├── components/
│   ├── BogowareLogo.astro      # Renders SVG logo with mode-appropriate colors
│   │                           #   Props: variant, size, class
│   ├── FieldLines.astro        # Decorative SVG field-line background
│   │                           #   Used in Hero, configurable density/opacity
│   ├── FeatureCard.astro       # Homepage feature card grid
│   │                           #   Props: icon, title, description, href
│   ├── OgImage.astro           # Dynamic OG image template (satori + sharp)
│   │                           #   Renders branded card with title, description, logo
│   └── Analytics.astro         # GA4 gtag.js conditional injection
│                               #   Props: measurementId, respectDnt, cookieless
├── assets/
│   ├── logo-dark.svg           # Gradient logo optimized for dark backgrounds
│   ├── logo-light.svg          # Gradient logo optimized for light backgrounds
│   ├── logo-mark.svg           # Standalone simplified mark (no container/fields)
│   ├── logo-favicon.svg        # 16px-optimized simplified mark
│   ├── logo-monochrome.svg     # Single-color, solid/dashed differentiation
│   └── fonts/                  # Self-hosted WOFF2 fallbacks
│       ├── SpaceGrotesk-Bold.woff2
│       ├── SpaceGrotesk-ExtraBold.woff2
│       ├── DMSans-Regular.woff2
│       ├── DMSans-Medium.woff2
│       ├── SpaceMono-Regular.woff2
│       ├── CormorantGaramond-SemiBold.woff2
│       ├── CormorantGaramond-Bold.woff2
│       ├── CrimsonPro-Regular.woff2
│       ├── CrimsonPro-Italic.woff2
│       └── FiraCode-Regular.woff2
├── tsconfig.json
└── package.json
```

#### `package.json` (theme)

```json
{
  "name": "@bogoware/starlight-theme",
  "version": "0.1.0",
  "type": "module",
  "exports": {
    ".": "./index.ts"
  },
  "peerDependencies": {
    "@astrojs/starlight": ">=0.30.0",
    "astro": ">=5.0.0"
  },
  "devDependencies": {
    "@astrojs/starlight": "^0.30.0",
    "astro": "^5.17.0",
    "zod": "^3.23.0"
  },
  "keywords": ["astro", "starlight", "theme", "bogoware"],
  "license": "MIT",
  "files": [
    "index.ts",
    "integration.ts",
    "schema.ts",
    "styles/",
    "overrides/",
    "components/",
    "assets/"
  ]
}
```

### 3.3 Monorepo Structure

The theme and documentation sites live in a **pnpm workspace monorepo** rooted at the Monads repository (initially), with the theme package extracted to its own repo once stabilized.

```
docs/                           # Monorepo root (temporary, in Monads repo)
├── pnpm-workspace.yaml
├── packages/
│   └── starlight-theme-bogoware/   # The theme package (see 3.2)
└── sites/
    └── monads/                     # Bogoware.Monads docs site
        ├── astro.config.mjs
        ├── package.json
        └── src/
            └── content/
                └── docs/           # Migrated markdown content
```

### 3.4 Consumer Sites

#### 3.4.1 Bogoware.Monads (Primary — Migration)

| Property | Current | Target |
|---|---|---|
| **Source path** | `/Users/mr/git/Bogoware/Monads/docs/` | Same repo, restructured |
| **Framework** | Docusaurus 3.9.2 | Astro 5.x + Starlight |
| **Theme** | Infima (default) | `@bogoware/starlight-theme`, mode: `architect` |
| **Content** | 42 Markdown files across `getting-started/`, `concepts/`, `guides/`, `api/` | Same structure under `src/content/docs/` |
| **API docs** | Auto-generated via `xmldoc2md` (34 files) | Same pipeline, adapted frontmatter |
| **Components** | `HomepageFeatures` (React/TSX) | `FeatureCard.astro` from theme |
| **Custom CSS** | Infima variable overrides | Theme CSS tokens |
| **Deploy** | GitHub Pages via `docs.yml` workflow | Same target, updated workflow |
| **URL** | `https://bogoware.github.io/Monads/` | Preserved |
| **Base path** | `/Monads/` | Preserved via `base` in `astro.config.mjs` |

**Content map:**

| Docusaurus path | Starlight path |
|---|---|
| `docs/intro.md` | `src/content/docs/index.mdx` |
| `docs/changelog.md` | `src/content/docs/changelog.md` |
| `docs/getting-started/*.md` | `src/content/docs/getting-started/*.md` |
| `docs/concepts/*.md` | `src/content/docs/concepts/*.md` |
| `docs/guides/*.md` | `src/content/docs/guides/*.md` |
| `docs/api/*.md` | `src/content/docs/api/*.md` |

**Sidebar mapping (Docusaurus → Starlight):**

```typescript
// Docusaurus sidebars.ts
docsSidebar: [
  'intro',
  'changelog',
  { type: 'category', label: 'Getting Started', ... },
  { type: 'category', label: 'Concepts', ... },
  { type: 'category', label: 'Guides', ... },
]

// Starlight sidebar config (in astro.config.mjs)
sidebar: [
  { label: 'Introduction', link: '/' },
  { label: 'Changelog', link: '/changelog/' },
  { label: 'Getting Started', autogenerate: { directory: 'getting-started' } },
  { label: 'Concepts', autogenerate: { directory: 'concepts' } },
  { label: 'Guides', autogenerate: { directory: 'guides' } },
  { label: 'API Reference', autogenerate: { directory: 'api' } },
]
```

#### 3.4.2 Bogoware.Localization (Secondary — Migration)

| Property | Current | Target |
|---|---|---|
| **Source path** | `/Users/mr/git/Bogoware/Localization/docs/` | Same repo, restructured |
| **Framework** | Docusaurus 3.9.2 | Astro 5.x + Starlight |
| **Theme** | `@bogoware/starlight-theme`, mode: `architect` | Same as Monads |
| **Content** | 22 Markdown files | Migrated to `src/content/docs/` |
| **API docs** | 10 auto-generated files via `xmldoc2md` | Same pipeline |
| **Deploy** | GitHub Pages | Same target |

This site follows the identical pattern as Monads. The migration is a template operation once Monads is complete.

#### 3.4.3 Personal Blog (Tertiary — Replace Existing)

| Property | Current | Target |
|---|---|---|
| **Source path** | `/Users/mr/git/MrBogomips/blog/` | Same repo, restructured |
| **Framework** | Astro 5.17.1 + Preact (bare starter) | Astro 5.x + Starlight |
| **Theme** | None (default starter) | `@bogoware/starlight-theme`, mode: `florentine` |
| **Content** | 2 posts (`post-1.md`, `post-1 copy.md`) | Blog collection under `src/content/docs/posts/` |
| **Features needed** | RSS, reading time, tags, author bio | Starlight + custom blog layout |
| **Deploy** | Not deployed | GitHub Pages |

**Blog-specific features:**

- RSS feed via `@astrojs/rss`
- Reading time calculated from word count (200 WPM average)
- Tag taxonomy with tag index pages
- Author bio component in footer of each post
- Florentine typography for enhanced readability

---

## 4. Migration Strategy

### 4.1 Content Migration (Per Site)

#### Step 1: Frontmatter Conversion

Docusaurus frontmatter fields map to Starlight as follows:

| Docusaurus | Starlight | Notes |
|---|---|---|
| `sidebar_position: N` | `sidebar: { order: N }` | Numeric ordering |
| `sidebar_label: "Text"` | `sidebar: { label: "Text" }` | Display label |
| `title: "Page Title"` | `title: "Page Title"` | Identical |
| `description: "..."` | `description: "..."` | Identical |
| `slug: "/custom"` | `slug: "custom"` | No leading slash |
| `hide_table_of_contents: true` | `tableOfContents: false` | Different key |
| `_category_.json` | `{ label, order }` in parent frontmatter or sidebar config | Category metadata moves |

**Automated conversion script:** A Node.js script will read each `.md` file, parse YAML frontmatter, transform the fields, and write the result. The script preserves the markdown body unchanged.

#### Step 2: Sidebar Configuration

Replace the Docusaurus `sidebars.ts` file with inline sidebar configuration in `astro.config.mjs`. The `_category_.json` files in each directory are replaced by the `autogenerate` directive.

#### Step 3: Component Migration

| Docusaurus Component | Starlight Replacement |
|---|---|
| `<HomepageFeatures>` (React) | `<FeatureCard>` grid in `index.mdx` (Astro) |
| Admonitions (`:::note`, `:::tip`) | Starlight `<Aside>` components (native) |
| Tabs (`@docusaurus/plugin-content-docs` tabs) | Starlight `<Tabs>` + `<TabItem>` |
| Code blocks with `title` meta | Starlight code blocks with `title` attribute |

#### Step 4: Internal Links

- Remove `.md` extensions from links (Starlight uses directory-based routing).
- Convert relative paths to match new `src/content/docs/` structure.
- Replace `@site/docs/` prefixes with relative paths.

#### Step 5: URL Preservation

Configure Astro `trailingSlash: 'always'` and verify that all existing URLs remain valid. Create redirects in `astro.config.mjs` for any changed paths.

### 4.2 API Documentation Pipeline

The existing pipeline generates Markdown from .NET XML documentation. The only changes needed are in frontmatter format.

#### Current Pipeline (Preserved)

1. `dotnet build -c Release` — Produces XML doc and DLL.
2. `dotnet xmldoc2md <DLL> -o docs/docs/api --index-page-name index --github-pages` — Generates Markdown.
3. Shell script adds Docusaurus frontmatter to each generated file.

#### Adapted Pipeline

1. Steps 1–2 remain identical.
2. The frontmatter post-processing script changes from:

```yaml
# Docusaurus format
---
title: "Result<T>"
sidebar_position: 99
---
```

To:

```yaml
# Starlight format
---
title: "Result<T>"
sidebar:
  order: 99
---
```

3. Output directory changes from `docs/docs/api/` to `docs/sites/monads/src/content/docs/api/` (or equivalent per monorepo layout).

#### Script: `scripts/adapt-api-frontmatter.sh`

```bash
for file in "$API_DIR"/*.md; do
  if [ -f "$file" ]; then
    filename=$(basename "$file" .md)
    if [ "$filename" = "index" ]; then
      title="API Reference"
      order=1
    else
      order=99
      h1_line=$(grep -m1 "^# " "$file" || true)
      if [ -n "$h1_line" ]; then
        title="${h1_line#\# }"
        title="${title//&lt;/<}"
        title="${title//&gt;/>}"
      else
        title="${filename#bogoware.monads.}"
      fi
    fi
    temp_file=$(mktemp)
    cat > "$temp_file" <<FRONTMATTER
---
title: "$title"
sidebar:
  order: $order
---

FRONTMATTER
    cat "$file" >> "$temp_file"
    mv "$temp_file" "$file"
  fi
done
```

### 4.3 CI/CD Pipeline (Per Site)

#### Workflow: `.github/workflows/docs.yml` (Updated)

```yaml
name: Documentation

on:
  push:
    branches: [rel/prod]
  release:
    types: [published]
  workflow_dispatch:

permissions:
  contents: read
  pages: write
  id-token: write

concurrency:
  group: "pages"
  cancel-in-progress: false

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: |
            8.0.x
            9.0.x
            10.0.x

      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '20'

      - name: Setup pnpm
        uses: pnpm/action-setup@v4
        with:
          version: 9

      - name: Restore dotnet tools
        run: dotnet tool restore

      - name: Build .NET project
        run: dotnet build -c Release

      - name: Find XML documentation
        id: find-xml
        run: |
          DLL_FILE=$(find src -name "Bogoware.Monads.dll" \
            -path "*/bin/Release/*" | grep -v "/ref/" | head -1)
          echo "dll_file=$DLL_FILE" >> "$GITHUB_OUTPUT"

      - name: Generate API documentation
        env:
          DLL_FILE: ${{ steps.find-xml.outputs.dll_file }}
        run: |
          API_DIR=docs/sites/monads/src/content/docs/api
          mkdir -p "$API_DIR"
          dotnet xmldoc2md "$DLL_FILE" -o "$API_DIR" \
            --index-page-name index --github-pages
          bash scripts/adapt-api-frontmatter.sh "$API_DIR"

      - name: Sync changelog
        run: node scripts/sync-readme.js

      - name: Install dependencies
        run: pnpm install --frozen-lockfile
        working-directory: docs

      - name: Build Astro
        run: pnpm run build
        working-directory: docs/sites/monads

      - name: Upload artifact
        uses: actions/upload-pages-artifact@v3
        with:
          path: docs/sites/monads/dist

  deploy:
    environment:
      name: github-pages
      url: ${{ steps.deployment.outputs.page_url }}
    runs-on: ubuntu-latest
    needs: build
    steps:
      - name: Deploy to GitHub Pages
        id: deployment
        uses: actions/deploy-pages@v4
```

---

## 5. Logo Asset Deliverables

### 5.1 Vector Formats

| File | Format | Description |
|---|---|---|
| `logo-full-dark.svg` | SVG | Full mark (container + fields + curves) for dark BG |
| `logo-full-light.svg` | SVG | Full mark for light BG |
| `logo-mark-dark.svg` | SVG | Simplified (curves + dot only) for dark BG |
| `logo-mark-light.svg` | SVG | Simplified for light BG |
| `logo-monochrome.svg` | SVG | Single-color with solid/dashed differentiation |
| `logo-full.eps` | EPS | Vector for print production |
| `logo-mark.eps` | EPS | Simplified vector for print |

### 5.2 Raster Formats (PNG)

Generated from SVG at the following sizes:

| Size | Use Case |
|---|---|
| 16 × 16 | Favicon (ICO layer) |
| 32 × 32 | Favicon (ICO layer, HiDPI) |
| 48 × 48 | Windows taskbar icon |
| 128 × 128 | NuGet package icon |
| 180 × 180 | Apple Touch Icon |
| 192 × 192 | Android Chrome icon |
| 256 × 256 | Large icon contexts |
| 512 × 512 | PWA splash, app stores |

All PNGs include transparent background. Two variants per size: dark-optimized and light-optimized.

### 5.3 Favicon

| File | Format | Contents |
|---|---|---|
| `favicon.ico` | ICO | Multi-size: 16, 32, 48px layers |
| `favicon.svg` | SVG | Simplified mark with `prefers-color-scheme` media query |

The SVG favicon uses CSS `@media (prefers-color-scheme: dark)` to switch between light and dark variants automatically.

### 5.4 Social & Platform Assets

| Asset | Size | Format | Usage |
|---|---|---|---|
| GitHub avatar | 256 × 256 | PNG | Organization profile picture |
| OG card | 1200 × 630 | PNG | Open Graph / link previews |
| Twitter header | 1500 × 500 | PNG | Twitter/X profile header |
| NuGet icon | 128 × 128 | PNG | NuGet package metadata |
| NPM icon | 80 × 80 | PNG | NPM package avatar |

#### OG Card Template

The OG card features:
- Dark background (`#0f172a`)
- Full logo mark centered, large (300px)
- Project name in Space Grotesk 700, white
- Tagline in DM Sans 400, `#94a3b8`
- Subtle field-line decoration in background at 0.05 opacity

### 5.5 Badge Marks

For `shields.io` integration and inline documentation badges:
- Height: 20px
- Format: SVG
- Variant: Simplified mark only (no text)
- Color: White on transparent (for compositing onto shield background)

---

## 6. SEO, Social Platforms & Analytics

### 6.1 Search Engine Optimization

The theme provides comprehensive SEO out of the box. All optimizations are automatic — consumer sites get them by installing the theme.

#### Semantic HTML & Structure
- Starlight already emits semantic HTML5 (`<article>`, `<nav>`, `<aside>`, `<header>`, `<footer>`)
- Headings follow strict hierarchy (H1 → H2 → H3), enforced by Starlight
- Breadcrumb navigation with `BreadcrumbList` JSON-LD
- Table of contents generated from headings

#### Meta Tags (auto-generated per page)
```html
<title>{pageTitle} | {siteName}</title>
<meta name="description" content="{pageDescription or first 160 chars}" />
<link rel="canonical" href="{fullCanonicalUrl}" />
<meta name="robots" content="index, follow" />
<meta name="author" content="{seo.structuredData.author}" />
<html lang="{seo.locale.split('_')[0]}" />
```

#### Sitemap & Robots
- `@astrojs/sitemap` integration auto-generates `sitemap-index.xml`
- Custom `robots.txt` generated at build time:
  ```
  User-agent: *
  Allow: /
  Sitemap: {siteUrl}/sitemap-index.xml
  ```
- `<link rel="sitemap" href="/sitemap-index.xml" />` injected in `<head>`

#### JSON-LD Structured Data
Each page emits a JSON-LD block in `<head>`:

```json
{
  "@context": "https://schema.org",
  "@type": "TechArticle",          // or "BlogPosting" in florentine mode
  "headline": "{pageTitle}",
  "description": "{pageDescription}",
  "author": {
    "@type": "Organization",        // or "Person" for blog
    "name": "{seo.structuredData.author}",
    "url": "{siteUrl}"
  },
  "datePublished": "{page.date}",
  "dateModified": "{page.lastModified}",
  "publisher": {
    "@type": "Organization",
    "name": "Bogoware",
    "logo": {
      "@type": "ImageObject",
      "url": "{siteUrl}/logo.png"
    }
  },
  "mainEntityOfPage": "{canonicalUrl}",
  "image": "{ogImageUrl}"
}
```

For API reference pages, uses `@type: "SoftwareSourceCode"` with `programmingLanguage: "C#"`.

#### Performance SEO
- Self-hosted WOFF2 fonts with `font-display: swap` and preload hints
- Critical CSS inlined by Astro's build
- Images lazy-loaded with `loading="lazy"` and explicit `width`/`height`
- Prefetch for navigation links via Starlight's built-in prefetch

### 6.2 Social Platform Integration (Open Graph & Twitter Cards)

#### Meta Tags (auto-generated per page)
```html
<!-- Open Graph -->
<meta property="og:type" content="article" />
<meta property="og:title" content="{pageTitle}" />
<meta property="og:description" content="{pageDescription}" />
<meta property="og:image" content="{ogImageUrl}" />
<meta property="og:image:width" content="1200" />
<meta property="og:image:height" content="630" />
<meta property="og:url" content="{canonicalUrl}" />
<meta property="og:site_name" content="{seo.siteName}" />
<meta property="og:locale" content="{seo.locale}" />

<!-- Twitter/X Card -->
<meta name="twitter:card" content="{seo.twitterCard}" />
<meta name="twitter:site" content="{seo.twitterSite}" />
<meta name="twitter:title" content="{pageTitle}" />
<meta name="twitter:description" content="{pageDescription}" />
<meta name="twitter:image" content="{ogImageUrl}" />
```

#### Dynamic OG Image Generation

When `seo.ogImage.strategy` is `'dynamic'`, the theme generates a unique 1200×630 branded OG image for EVERY page at build time using `satori` + `sharp`:

- **Background:** Dark gradient (#0f172a → #1e293b) with subtle field-line pattern
- **Logo:** Bogoliubov Vertex mark at top-left
- **Title:** Page title in Space Grotesk 700 (Architect) or Cormorant Garamond 700 (Florentine)
- **Description:** First line of page description in body font
- **Footer:** Site name + URL in JetBrains Mono
- **Accent:** Gradient bar (indigo → violet) at bottom edge

When `'static'`, uses a single pre-made OG image from the assets folder.

#### Per-Page Frontmatter Overrides
```yaml
---
title: Result<T> Monad
description: Type-safe error handling for C#
ogImage: /custom-og-image.png    # Override auto-generated image
twitterCard: summary              # Override card type per page
---
```

### 6.3 Google Analytics Integration

#### Configuration
```typescript
bogowareTheme({
  analytics: {
    googleAnalyticsId: 'G-XXXXXXXXXX',  // GA4 Measurement ID
    respectDnt: true,                    // Honor Do Not Track
    cookieless: false,                   // Cookie-less measurement mode
  },
})
```

#### Implementation
When `googleAnalyticsId` is provided, the `Analytics.astro` component injects:

```html
<!-- Only if DNT is not set (when respectDnt: true) -->
<script is:inline define:vars={{ id: googleAnalyticsId, cookieless }}>
  // Check DNT
  if (navigator.doNotTrack !== '1') {
    // Load gtag.js asynchronously
    const s = document.createElement('script');
    s.src = `https://www.googletagmanager.com/gtag/js?id=${id}`;
    s.async = true;
    document.head.appendChild(s);

    window.dataLayer = window.dataLayer || [];
    function gtag(){ dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', id, {
      // Cookie-less mode: anonymize IP, disable cookies
      ...(cookieless ? {
        client_storage: 'none',
        anonymize_ip: true,
      } : {}),
      // Page path for SPA-style navigation
      page_path: window.location.pathname,
    });
  }
</script>
```

#### Privacy Considerations
- `respectDnt: true` (default) — Does NOT load GA4 if browser sends `DNT: 1`
- `cookieless: true` — Loads GA4 but with `client_storage: 'none'` and `anonymize_ip: true`, compliant with strict cookie regulations (GDPR)
- If `googleAnalyticsId` is `undefined` or empty, NO analytics script is injected (zero overhead)
- No consent banner is needed in cookieless mode (no cookies set)

### 6.4 Additional Integrations

| Feature | Implementation | Notes |
|---|---|---|
| **RSS Feed** | `@astrojs/rss` | Auto-generated for blog (Florentine mode), optional for docs |
| **Canonical URLs** | Auto-computed from `site` + `base` + page path | Prevents duplicate content |
| **Last Modified** | Git-based `lastModified` date per page | Via `remark-modified-time` plugin |
| **Reading Time** | Auto-calculated for blog posts | Via `remark-reading-time` plugin |
| **Link Preview** | Discord, Slack, LinkedIn, Telegram all use OG tags | Branded previews everywhere |

---

## 7. Testing Plan

### 7.1 Visual Regression

- **Tool:** Playwright screenshot comparison or Percy
- **Key pages:** Homepage/hero, API reference index, a concept page, sidebar navigation, mobile viewport
- **Threshold:** < 0.1% pixel difference between baseline and current
- **Trigger:** Every PR to `rel/prod`

### 7.2 Lighthouse Audits

| Metric | Target | Minimum |
|---|---|---|
| Performance | >= 95 | >= 90 |
| Accessibility | >= 100 | >= 95 |
| Best Practices | >= 100 | >= 95 |
| SEO | >= 100 | >= 95 |

Run via `lighthouse-ci` in CI on every build. Fail the build if any metric drops below minimum.

### 7.3 Cross-Browser Testing

| Browser | Versions | Priority |
|---|---|---|
| Chrome | Last 3 | P0 |
| Firefox | Last 3 | P0 |
| Safari | Last 5 | P0 |
| Edge | Last 3 | P1 |

Focus areas: CSS custom properties, gradient rendering, font loading, SVG rendering, dark mode toggle.

### 7.4 Responsive Breakpoints

| Viewport | Width | Test Focus |
|---|---|---|
| Mobile S | 320px | Content overflow, font scaling, hamburger nav |
| Mobile L | 425px | Card grid stacking, sidebar overlay |
| Tablet | 768px | Sidebar transition, content reflow |
| Desktop | 1024px | Three-column layout activation |
| Wide | 1440px | Max-width constraints, whitespace balance |

### 7.5 Print Stylesheet

- Documentation pages must produce clean PDF output via browser print
- Code blocks: wrap long lines, use monochrome syntax highlighting
- Sidebar and navigation: hidden in print
- Page breaks: avoid breaking inside code blocks and tables

### 7.6 Dark/Light Mode

- Both modes must be fully functional with no invisible text or low-contrast elements
- Color tokens must switch correctly on toggle
- Logo variant must switch between `logo-dark.svg` and `logo-light.svg`
- Code syntax highlighting must use mode-appropriate palette
- `prefers-color-scheme` media query must be respected on first load

### 7.7 Font Fallback

- Disconnect Google Fonts CDN and verify that:
  - Self-hosted WOFF2 files load from `assets/fonts/`
  - If both fail, system fallback stack renders acceptably
  - No FOIT (Flash of Invisible Text) — `font-display: swap` verified
  - Layout does not shift significantly (CLS < 0.1)

### 7.8 SEO & Social Validation

- **Structured Data:** Validate JSON-LD with Google's Rich Results Test for every page template
- **OG Images:** Verify 1200×630 PNG generated for every page, check with Facebook Sharing Debugger and Twitter Card Validator
- **Meta Tags:** Automated check that every page has: `<title>`, `<meta description>`, `<link canonical>`, `og:title`, `og:description`, `og:image`, `twitter:card`
- **Sitemap:** Verify `sitemap-index.xml` contains all pages, valid XML, correct `<lastmod>` dates
- **Robots.txt:** Verify correct Allow/Disallow rules and sitemap reference
- **GA4:** When configured, verify `gtag.js` loads, fires `page_view` event; when DNT is set, verify script does NOT load
- **Link Previews:** Test shared links on: Slack, Discord, LinkedIn, Telegram, Twitter/X, Facebook — verify branded card renders

### 7.9 Content Integrity

- All 42 Monads pages render without errors
- All 22 Localization pages render without errors
- All internal links resolve (no 404s)
- API documentation frontmatter is correctly parsed
- Code blocks render with syntax highlighting for C#, Bash, JSON

---

## 8. CI/CD Integration

### 8.1 Theme Package Pipeline

```
Push to theme repo → GitHub Actions:
  1. pnpm install
  2. TypeScript type check (tsc --noEmit)
  3. CSS lint (stylelint)
  4. Build verification (import in test Astro project, build succeeds)
  5. Visual regression (screenshot test pages)
  6. Publish to NPM (on release tag only)
     - npm publish --access public
     - Requires NPM_TOKEN secret
```

**Triggers:**
- Push to `main`: Steps 1–5 (CI only)
- Release published: Steps 1–6 (CI + publish)
- Manual dispatch: Steps 1–5

### 8.2 Consumer Site Pipeline

```
Push to rel/prod → GitHub Actions:
  1. Setup .NET SDK (8.x, 9.x, 10.x)
  2. Setup Node.js 20 + pnpm 9
  3. Restore dotnet tools
  4. Build .NET project
  5. Generate API docs (xmldoc2md)
  6. Adapt API frontmatter
  7. Sync changelog
  8. pnpm install
  9. Build Astro (astro build)
  10. Lighthouse CI audit
  11. Upload Pages artifact
  12. Deploy to GitHub Pages
```

**Triggers:**
- Push to `rel/prod`: Full pipeline
- Release published: Full pipeline
- Manual dispatch: Full pipeline

### 8.3 Dependency Update Strategy

- Theme version pinned in consumer sites (exact version, not range)
- Renovate/Dependabot configured to create PRs for theme updates
- Theme updates require visual regression pass before merge

---

## Appendix A: Decision Log

| Decision | Choice | Rationale |
|---|---|---|
| Framework | Astro + Starlight | Faster builds, partial hydration, growing ecosystem, native Markdown support |
| Typography dual system | Architect + Florentine | Technical docs need geometric precision; blog needs humanist readability |
| Logo concept | Bogoliubov Vertex | Connects to the "Bogo-" name origin, visually distinctive, scales well |
| Font source | Google Fonts (OFL 1.1) | Free, high-quality, wide browser support, self-hostable |
| Package scope | `@bogoware` on NPM | Namespace consistency, prevents squatting |
| Monorepo tool | pnpm workspaces | Fast, disk-efficient, native workspace support |
| API doc generator | xmldoc2md (keep existing) | Already proven, minimal adaptation needed |
| Deploy target | GitHub Pages | Already in use, free for public repos, good DX |

## Appendix B: Migration Checklist

### Phase 1: Theme Package (Week 1–2)

- [ ] Initialize `packages/starlight-theme-bogoware/`
- [ ] Implement `base.css` with palette, spacing, layout tokens
- [ ] Implement `architect.css` with font-face and typography rules
- [ ] Implement `florentine.css` with font-face and typography rules
- [ ] Create all 5 SVG logo variants
- [ ] Build `BogowareLogo.astro` component
- [ ] Build `FieldLines.astro` component
- [ ] Build `FeatureCard.astro` component
- [ ] Implement Header, Hero, Sidebar, Footer overrides
- [ ] Write plugin entry point with config schema
- [ ] Test with a minimal Starlight site
- [ ] Publish v0.1.0 to NPM

### Phase 2: Monads Migration (Week 2–3)

- [ ] Scaffold Astro/Starlight site in `docs/sites/monads/`
- [ ] Run frontmatter conversion script on all 42 pages
- [ ] Configure sidebar in `astro.config.mjs`
- [ ] Replace `HomepageFeatures` with `FeatureCard` grid
- [ ] Adapt `generate-api-docs` workflow step
- [ ] Adapt `sync-readme.js` script
- [ ] Verify all internal links
- [ ] Run Lighthouse audit (targets met)
- [ ] Update `docs.yml` workflow
- [ ] Deploy to GitHub Pages
- [ ] Verify URL preservation (no broken external links)

### Phase 3: Localization Migration (Week 3–4)

- [ ] Scaffold Astro/Starlight site (template from Monads)
- [ ] Run frontmatter conversion on 22 pages
- [ ] Adapt API doc pipeline
- [ ] Deploy to GitHub Pages

### Phase 4: Blog Migration (Week 4–5)

- [ ] Scaffold Astro/Starlight site with Florentine mode
- [ ] Migrate 2 existing posts
- [ ] Implement RSS feed
- [ ] Implement reading time calculation
- [ ] Implement tag taxonomy
- [ ] Deploy to GitHub Pages

### Phase 5: Cleanup (Week 5)

- [ ] Remove Docusaurus dependencies from Monads
- [ ] Remove Docusaurus dependencies from Localization
- [ ] Remove old Preact starter from blog
- [ ] Generate all raster logo assets (PNG, ICO)
- [ ] Generate social assets (OG card, Twitter header)
- [ ] Update GitHub organization avatar
- [ ] Document theme API in README

---

*End of specification.*
