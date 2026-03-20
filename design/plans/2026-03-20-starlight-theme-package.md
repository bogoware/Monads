# @bogoware/starlight-theme Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build and publish `@bogoware/starlight-theme`, a reusable Astro Starlight theme plugin implementing the Bogoware brand identity with dual typography (Architect/Florentine), SEO/social optimization, and optional Google Analytics.

**Architecture:** Starlight plugin using the official plugin API. Injects CSS custom properties, component overrides, and Astro integrations. Monorepo with pnpm workspaces: `packages/starlight-theme-bogoware/` (the package) + `docs/` (playground site).

**Tech Stack:** Astro 5.x, Starlight >=0.30, TypeScript, Zod (config validation), satori + sharp (OG images), pnpm workspaces

---

## Task 1: Scaffold monorepo

**Goal:** Create the pnpm workspace monorepo structure with the theme package and a minimal Starlight playground site.

### Step 1.1 — Create workspace root

- [ ] Create file: `docs/pnpm-workspace.yaml`

```yaml
packages:
  - 'packages/*'
  - 'sites/*'
```

- [ ] Create file: `docs/package.json`

```json
{
  "name": "@bogoware/starlight-monorepo",
  "private": true,
  "type": "module",
  "scripts": {
    "dev": "pnpm -C sites/monads dev",
    "build": "pnpm -C sites/monads build",
    "test": "echo \"no tests yet\""
  }
}
```

### Step 1.2 — Create theme package skeleton

- [ ] Create directory: `docs/packages/starlight-theme-bogoware/`
- [ ] Create file: `docs/packages/starlight-theme-bogoware/package.json`

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
  "dependencies": {
    "zod": "^3.23.0"
  },
  "devDependencies": {
    "@astrojs/starlight": "^0.30.0",
    "astro": "^5.17.0",
    "typescript": "^5.7.0"
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

- [ ] Create file: `docs/packages/starlight-theme-bogoware/tsconfig.json`

```json
{
  "extends": "astro/tsconfigs/strict",
  "compilerOptions": {
    "outDir": "./dist",
    "declaration": true,
    "moduleResolution": "bundler",
    "module": "ESNext",
    "target": "ESNext"
  },
  "include": ["**/*.ts", "**/*.astro"]
}
```

- [ ] Create placeholder file: `docs/packages/starlight-theme-bogoware/index.ts`

```typescript
import type { StarlightPlugin } from '@astrojs/starlight/types';

export default function bogowareTheme(): StarlightPlugin {
  return {
    name: '@bogoware/starlight-theme',
    hooks: {
      setup({ config, updateConfig }) {
        // Plugin will be implemented in subsequent tasks
      },
    },
  };
}
```

### Step 1.3 — Create playground Starlight site

- [ ] Create directory: `docs/sites/monads/`
- [ ] Create file: `docs/sites/monads/package.json`

```json
{
  "name": "@bogoware/docs-monads",
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
    "@bogoware/starlight-theme": "workspace:*"
  }
}
```

- [ ] Create file: `docs/sites/monads/astro.config.mjs`

```javascript
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  site: 'https://bogoware.github.io',
  base: '/Monads/',
  integrations: [
    starlight({
      title: 'Bogoware.Monads',
      plugins: [
        bogowareTheme({
          mode: 'architect',
          logoVariant: 'simplified',
          fieldLines: true,
        }),
      ],
      sidebar: [
        { label: 'Introduction', link: '/' },
      ],
    }),
  ],
});
```

- [ ] Create file: `docs/sites/monads/src/content/docs/index.mdx`

```mdx
---
title: Bogoware.Monads
description: Functional programming patterns for C#
---

Welcome to the Bogoware.Monads documentation playground.

This site validates that the `@bogoware/starlight-theme` plugin loads correctly.
```

- [ ] Create file: `docs/sites/monads/src/content.config.ts`

```typescript
import { defineCollection } from 'astro:content';
import { docsSchema } from '@astrojs/starlight/schema';

export const collections = {
  docs: defineCollection({ schema: docsSchema() }),
};
```

### Step 1.4 — Install dependencies and verify

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm install`
- [ ] Verify: command exits with code 0, `node_modules/` created
- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Verify: dev server starts on `localhost:4321`, no errors in console
- [ ] Stop the dev server (Ctrl+C)

### Step 1.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/pnpm-workspace.yaml docs/package.json \
  docs/packages/starlight-theme-bogoware/ \
  docs/sites/monads/
git commit -m "chore: scaffold pnpm workspace monorepo with theme package and playground site"
```

---

## Task 2: Plugin entry + Zod schema

**Goal:** Define the configuration schema with Zod validation and implement the Starlight plugin interface that reads the validated config.

### Step 2.1 — Write failing test: schema validates correct config

- [ ] Create file: `docs/packages/starlight-theme-bogoware/schema.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { bogowareThemeSchema, type BogowareThemeConfig } from './schema.js';

describe('bogowareThemeSchema', () => {
  it('should accept a valid full config', () => {
    const config: BogowareThemeConfig = {
      mode: 'architect',
      logoVariant: 'simplified',
      accentColor: undefined,
      fieldLines: true,
      seo: {
        siteName: 'Bogoware.Monads',
        defaultDescription: 'Functional programming patterns for C#',
        locale: 'en_US',
        ogImage: { enabled: true, strategy: 'dynamic' },
        twitterCard: 'summary_large_image',
        twitterSite: '@bogoware',
        structuredData: { type: 'SoftwareSourceCode', author: 'Bogoware' },
      },
      analytics: {
        googleAnalyticsId: 'G-XXXXXXXXXX',
        respectDnt: true,
        cookieless: false,
      },
    };
    const result = bogowareThemeSchema.parse(config);
    expect(result.mode).toBe('architect');
    expect(result.seo.ogImage.strategy).toBe('dynamic');
  });

  it('should apply defaults for minimal config', () => {
    const result = bogowareThemeSchema.parse({});
    expect(result.mode).toBe('architect');
    expect(result.logoVariant).toBe('simplified');
    expect(result.fieldLines).toBe(true);
    expect(result.seo.ogImage.enabled).toBe(true);
    expect(result.analytics.respectDnt).toBe(true);
  });

  it('should reject invalid mode', () => {
    expect(() => bogowareThemeSchema.parse({ mode: 'gothic' })).toThrow();
  });

  it('should reject invalid logoVariant', () => {
    expect(() => bogowareThemeSchema.parse({ logoVariant: 'neon' })).toThrow();
  });
});
```

- [ ] Add vitest to the theme package. Modify `docs/packages/starlight-theme-bogoware/package.json` — add to `devDependencies`:

```json
"vitest": "^3.0.0"
```

- [ ] Add test script to `docs/packages/starlight-theme-bogoware/package.json`:

```json
"scripts": {
  "test": "vitest run",
  "test:watch": "vitest"
}
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm install`
- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (schema.ts does not exist yet)

### Step 2.2 — Implement schema.ts

- [ ] Create file: `docs/packages/starlight-theme-bogoware/schema.ts`

```typescript
import { z } from 'zod';

const ogImageSchema = z.object({
  enabled: z.boolean().default(true),
  strategy: z.enum(['dynamic', 'static']).default('dynamic'),
});

const structuredDataSchema = z.object({
  type: z.enum(['SoftwareSourceCode', 'Blog', 'WebSite']).default('SoftwareSourceCode'),
  author: z.string().default('Bogoware'),
});

const seoSchema = z.object({
  siteName: z.string().default('Bogoware'),
  defaultDescription: z.string().default(''),
  locale: z.string().default('en_US'),
  ogImage: ogImageSchema.default({}),
  twitterCard: z.enum(['summary', 'summary_large_image']).default('summary_large_image'),
  twitterSite: z.string().optional(),
  structuredData: structuredDataSchema.default({}),
});

const analyticsSchema = z.object({
  googleAnalyticsId: z.string().optional(),
  respectDnt: z.boolean().default(true),
  cookieless: z.boolean().default(false),
});

export const bogowareThemeSchema = z.object({
  mode: z.enum(['architect', 'florentine']).default('architect'),
  logoVariant: z.enum(['full', 'simplified', 'monochrome']).default('simplified'),
  accentColor: z.string().optional(),
  fieldLines: z.boolean().default(true),
  seo: seoSchema.default({}),
  analytics: analyticsSchema.default({}),
});

export type BogowareThemeConfig = z.input<typeof bogowareThemeSchema>;
export type ResolvedBogowareThemeConfig = z.output<typeof bogowareThemeSchema>;
```

### Step 2.3 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all 4 tests PASS

### Step 2.4 — Implement plugin entry (index.ts)

- [ ] Replace file: `docs/packages/starlight-theme-bogoware/index.ts`

```typescript
import type { StarlightPlugin } from '@astrojs/starlight/types';
import { bogowareThemeSchema, type BogowareThemeConfig } from './schema.js';

export type { BogowareThemeConfig, ResolvedBogowareThemeConfig } from './schema.js';

export default function bogowareTheme(
  userConfig: BogowareThemeConfig = {}
): StarlightPlugin {
  const config = bogowareThemeSchema.parse(userConfig);

  return {
    name: '@bogoware/starlight-theme',
    hooks: {
      setup({ updateConfig, config: starlightConfig, addIntegration }) {
        // Inject base CSS (always loaded)
        const cssImports: string[] = [
          '@bogoware/starlight-theme/styles/base.css',
        ];

        // Inject typography CSS based on mode
        if (config.mode === 'architect') {
          cssImports.push('@bogoware/starlight-theme/styles/architect.css');
        } else {
          cssImports.push('@bogoware/starlight-theme/styles/florentine.css');
        }

        // Register component overrides
        const componentOverrides: Record<string, string> = {
          Header: '@bogoware/starlight-theme/overrides/Header.astro',
          Hero: '@bogoware/starlight-theme/overrides/Hero.astro',
          Sidebar: '@bogoware/starlight-theme/overrides/Sidebar.astro',
          Footer: '@bogoware/starlight-theme/overrides/Footer.astro',
          Head: '@bogoware/starlight-theme/overrides/Head.astro',
          SocialLinks: '@bogoware/starlight-theme/overrides/SocialMeta.astro',
        };

        updateConfig({
          customCss: [
            ...(starlightConfig.customCss ?? []),
            ...cssImports,
          ],
          components: {
            ...starlightConfig.components,
            ...componentOverrides,
          },
        });
      },
    },
  };
}
```

### Step 2.5 — Write integration test: plugin loads without errors

- [ ] Add test file: `docs/packages/starlight-theme-bogoware/plugin.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import bogowareTheme from './index.js';

describe('bogowareTheme plugin', () => {
  it('should return a valid Starlight plugin object', () => {
    const plugin = bogowareTheme({ mode: 'architect' });
    expect(plugin.name).toBe('@bogoware/starlight-theme');
    expect(plugin.hooks).toBeDefined();
    expect(typeof plugin.hooks.setup).toBe('function');
  });

  it('should accept empty config (all defaults)', () => {
    const plugin = bogowareTheme();
    expect(plugin.name).toBe('@bogoware/starlight-theme');
  });

  it('should accept florentine mode', () => {
    const plugin = bogowareTheme({ mode: 'florentine' });
    expect(plugin.name).toBe('@bogoware/starlight-theme');
  });

  it('should throw on invalid config', () => {
    // @ts-expect-error — intentionally passing invalid config
    expect(() => bogowareTheme({ mode: 'gothic' })).toThrow();
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all tests PASS (schema + plugin tests)

### Step 2.6 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/schema.ts \
  docs/packages/starlight-theme-bogoware/schema.test.ts \
  docs/packages/starlight-theme-bogoware/plugin.test.ts \
  docs/packages/starlight-theme-bogoware/index.ts \
  docs/packages/starlight-theme-bogoware/package.json
git commit -m "feat: implement Zod config schema and Starlight plugin entry point"
```

---

## Task 3: Base CSS — palette tokens + spacing scale

**Goal:** Create the shared CSS custom properties file with the full color palette (dark/light modes), spacing scale, layout dimensions, and code syntax accents from the design spec.

### Step 3.1 — Write failing test: CSS tokens exist and are valid

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/__tests__/base-css.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve } from 'node:path';

const baseCss = readFileSync(
  resolve(__dirname, '..', 'base.css'),
  'utf-8'
);

describe('base.css', () => {
  it('should define primary palette tokens', () => {
    expect(baseCss).toContain('--bw-indigo: #6366f1');
    expect(baseCss).toContain('--bw-violet: #8b5cf6');
    expect(baseCss).toContain('--bw-cyan: #99f0ff');
    expect(baseCss).toContain('--bw-periwinkle: #b0a0ff');
  });

  it('should define dark mode tokens', () => {
    expect(baseCss).toContain('--bw-bg: #0f172a');
    expect(baseCss).toContain('--bw-surface: #1e293b');
    expect(baseCss).toContain('--bw-border: #334155');
    expect(baseCss).toContain('--bw-text: #e2e8f0');
    expect(baseCss).toContain('--bw-text-muted: #94a3b8');
  });

  it('should define light mode tokens', () => {
    expect(baseCss).toContain('--bw-bg: #f8fafc');
    expect(baseCss).toContain('--bw-text: #1e293b');
    expect(baseCss).toContain('--bw-indigo-deep: #4f46e5');
  });

  it('should define spacing scale', () => {
    expect(baseCss).toContain('--bw-space-1: 4px');
    expect(baseCss).toContain('--bw-space-2: 8px');
    expect(baseCss).toContain('--bw-space-4: 16px');
    expect(baseCss).toContain('--bw-space-8: 32px');
    expect(baseCss).toContain('--bw-space-16: 64px');
  });

  it('should define code syntax accent tokens', () => {
    expect(baseCss).toContain('--bw-code-cyan: #67e8f9');
    expect(baseCss).toContain('--bw-code-mint: #86efac');
    expect(baseCss).toContain('--bw-code-indigo: #a5b4fc');
    expect(baseCss).toContain('--bw-code-amber: #fcd34d');
    expect(baseCss).toContain('--bw-code-rose: #fda4af');
  });

  it('should define layout dimensions', () => {
    expect(baseCss).toContain('--bw-content-max-width: 720px');
    expect(baseCss).toContain('--bw-content-wide-max-width: 1200px');
    expect(baseCss).toContain('--bw-sidebar-width: 240px');
    expect(baseCss).toContain('--bw-navbar-height: 64px');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (base.css does not exist)

### Step 3.2 — Implement base.css

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/base.css`

```css
/* ==========================================================================
   @bogoware/starlight-theme — Base Design Tokens
   Shared palette, spacing scale, layout dimensions, and code syntax accents.
   ========================================================================== */

/* --------------------------------------------------------------------------
   Primary Brand Palette (color-scheme independent)
   -------------------------------------------------------------------------- */
:root {
  --bw-indigo: #6366f1;
  --bw-violet: #8b5cf6;
  --bw-cyan: #99f0ff;
  --bw-periwinkle: #b0a0ff;

  /* Brand gradients */
  --bw-gradient-primary: linear-gradient(135deg, var(--bw-indigo), var(--bw-violet));
  --bw-gradient-accent: linear-gradient(135deg, var(--bw-indigo), var(--bw-cyan));
  --bw-gradient-full: linear-gradient(135deg, var(--bw-indigo), var(--bw-violet), var(--bw-cyan));

  /* --------------------------------------------------------------------------
     Spacing Scale (4px base unit)
     -------------------------------------------------------------------------- */
  --bw-space-1: 4px;
  --bw-space-2: 8px;
  --bw-space-3: 12px;
  --bw-space-4: 16px;
  --bw-space-6: 24px;
  --bw-space-8: 32px;
  --bw-space-12: 48px;
  --bw-space-16: 64px;

  /* --------------------------------------------------------------------------
     Layout Dimensions
     -------------------------------------------------------------------------- */
  --bw-content-max-width: 720px;
  --bw-content-wide-max-width: 1200px;
  --bw-sidebar-width: 240px;
  --bw-sidebar-collapsed-width: 0px;
  --bw-toc-width: 200px;
  --bw-navbar-height: 64px;

  /* --------------------------------------------------------------------------
     Border Radii
     -------------------------------------------------------------------------- */
  --bw-radius-sm: 4px;
  --bw-radius-md: 8px;
  --bw-radius-lg: 12px;
  --bw-radius-xl: 16px;
  --bw-radius-full: 9999px;

  /* --------------------------------------------------------------------------
     Transitions
     -------------------------------------------------------------------------- */
  --bw-transition-fast: 150ms ease;
  --bw-transition-base: 250ms ease;
  --bw-transition-slow: 400ms ease;

  /* --------------------------------------------------------------------------
     Code Syntax Accent Tokens
     -------------------------------------------------------------------------- */
  --bw-code-cyan: #67e8f9;
  --bw-code-mint: #86efac;
  --bw-code-indigo: #a5b4fc;
  --bw-code-amber: #fcd34d;
  --bw-code-rose: #fda4af;
}

/* --------------------------------------------------------------------------
   Dark Mode Tokens (default — Starlight uses dark-first)
   -------------------------------------------------------------------------- */
:root,
[data-theme='dark'] {
  --bw-bg: #0f172a;
  --bw-surface: #1e293b;
  --bw-border: #334155;
  --bw-text: #e2e8f0;
  --bw-text-muted: #94a3b8;
}

/* --------------------------------------------------------------------------
   Light Mode Tokens
   -------------------------------------------------------------------------- */
[data-theme='light'] {
  --bw-bg: #f8fafc;
  --bw-surface: #ffffff;
  --bw-border: #e2e8f0;
  --bw-text: #1e293b;
  --bw-text-muted: #64748b;
  --bw-indigo-deep: #4f46e5;
  --bw-violet-deep: #7c3aed;
  --bw-cyan-deep: #06b6d4;
}

/* --------------------------------------------------------------------------
   Map to Starlight CSS custom properties
   -------------------------------------------------------------------------- */
:root {
  --sl-color-accent-low: var(--bw-violet);
  --sl-color-accent: var(--bw-indigo);
  --sl-color-accent-high: var(--bw-cyan);
  --sl-color-bg-nav: var(--bw-surface);
  --sl-color-bg-sidebar: var(--bw-surface);
}

/* --------------------------------------------------------------------------
   Breakpoints (reference — used in component CSS)
   Mobile:  < 768px
   Tablet:  768px — 1023px
   Desktop: >= 1024px
   -------------------------------------------------------------------------- */
```

### Step 3.3 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all base-css tests PASS

### Step 3.4 — Verify tokens apply in playground

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Open `http://localhost:4321/Monads/` in browser
- [ ] Verify: page loads without CSS errors; inspect element shows `--bw-indigo` defined on `:root`
- [ ] Stop dev server

### Step 3.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/styles/base.css \
  docs/packages/starlight-theme-bogoware/styles/__tests__/base-css.test.ts
git commit -m "feat: add base CSS with palette tokens, spacing scale, and layout dimensions"
```

---

## Task 4: Architect typography CSS

**Goal:** Create the Architect typography system (Space Grotesk + DM Sans + Space Mono) with `@font-face` declarations and type scale.

### Step 4.1 — Write failing test: architect CSS has correct font declarations

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/__tests__/architect-css.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve } from 'node:path';

const css = readFileSync(
  resolve(__dirname, '..', 'architect.css'),
  'utf-8'
);

describe('architect.css', () => {
  it('should declare Space Grotesk font-face', () => {
    expect(css).toContain("font-family: 'Space Grotesk'");
    expect(css).toContain('font-display: swap');
  });

  it('should declare DM Sans font-face', () => {
    expect(css).toContain("font-family: 'DM Sans'");
  });

  it('should declare Space Mono font-face', () => {
    expect(css).toContain("font-family: 'Space Mono'");
  });

  it('should define heading font-family variable', () => {
    expect(css).toContain("--bw-font-heading: 'Space Grotesk'");
  });

  it('should define body font-family variable', () => {
    expect(css).toContain("--bw-font-body: 'DM Sans'");
  });

  it('should define code font-family variable', () => {
    expect(css).toContain("--bw-font-code: 'Space Mono'");
  });

  it('should define display heading size for H1', () => {
    expect(css).toContain('--bw-text-display: 2.25rem');
  });

  it('should reference WOFF2 font files', () => {
    expect(css).toContain('.woff2');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (architect.css does not exist)

### Step 4.2 — Implement architect.css

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/architect.css`

```css
/* ==========================================================================
   @bogoware/starlight-theme — Architect Typography
   Space Grotesk (headings) + DM Sans (body) + Space Mono (code)
   For technical documentation, API references, and guides.
   ========================================================================== */

/* --------------------------------------------------------------------------
   @font-face Declarations — Self-hosted WOFF2 with Google Fonts CDN fallback
   -------------------------------------------------------------------------- */

/* Space Grotesk — Bold (700) */
@font-face {
  font-family: 'Space Grotesk';
  font-style: normal;
  font-weight: 700;
  font-display: swap;
  src: local('Space Grotesk Bold'),
       url('../assets/fonts/SpaceGrotesk-Bold.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/spacegrotesk/v16/V8mDoQDjQSkFtoMM3T6r8E7mPbF4C_k3HqU.woff2') format('woff2');
}

/* Space Grotesk — ExtraBold (800) */
@font-face {
  font-family: 'Space Grotesk';
  font-style: normal;
  font-weight: 800;
  font-display: swap;
  src: local('Space Grotesk ExtraBold'),
       url('../assets/fonts/SpaceGrotesk-ExtraBold.woff2') format('woff2');
}

/* DM Sans — Regular (400) */
@font-face {
  font-family: 'DM Sans';
  font-style: normal;
  font-weight: 400;
  font-display: swap;
  src: local('DM Sans Regular'),
       url('../assets/fonts/DMSans-Regular.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/dmsans/v15/rP2tp2ywxg089UriI5-g4vlH9VoD8CmcqZG40F9JadbnoEwA.woff2') format('woff2');
}

/* DM Sans — Medium (500) */
@font-face {
  font-family: 'DM Sans';
  font-style: normal;
  font-weight: 500;
  font-display: swap;
  src: local('DM Sans Medium'),
       url('../assets/fonts/DMSans-Medium.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/dmsans/v15/rP2tp2ywxg089UriI5-g4vlH9VoD8CmcqZG40F9JadbnoEwA.woff2') format('woff2');
}

/* Space Mono — Regular (400) */
@font-face {
  font-family: 'Space Mono';
  font-style: normal;
  font-weight: 400;
  font-display: swap;
  src: local('Space Mono Regular'),
       url('../assets/fonts/SpaceMono-Regular.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/spacemono/v13/i7dPIFZifjKcF5UAWdDRYEF8RQ.woff2') format('woff2');
}

/* --------------------------------------------------------------------------
   Typography Tokens
   -------------------------------------------------------------------------- */
:root {
  /* Font families */
  --bw-font-heading: 'Space Grotesk', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
  --bw-font-body: 'DM Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
  --bw-font-code: 'Space Mono', 'Cascadia Code', 'Fira Code', 'JetBrains Mono', monospace;

  /* Type scale — Headings */
  --bw-text-display: 2.25rem;       /* 36px — H1 / Display */
  --bw-text-display-lh: 1.15;
  --bw-text-h2: 1.75rem;            /* 28px */
  --bw-text-h2-lh: 1.2;
  --bw-text-h3: 1.375rem;           /* 22px */
  --bw-text-h3-lh: 1.25;

  /* Type scale — Body */
  --bw-text-body: 1rem;             /* 16px */
  --bw-text-body-lg: 1.125rem;      /* 18px */
  --bw-text-body-lh: 1.65;
  --bw-text-small: 0.875rem;        /* 14px */
  --bw-text-small-lh: 1.5;

  /* Type scale — Code */
  --bw-text-code: 0.875rem;         /* 14px */
  --bw-text-code-inline-lh: 1.6;
  --bw-text-code-block-lh: 1.7;

  /* Map to Starlight CSS custom properties */
  --sl-font: var(--bw-font-body);
  --sl-font-mono: var(--bw-font-code);
}

/* --------------------------------------------------------------------------
   Heading Styles
   -------------------------------------------------------------------------- */
h1,
.sl-markdown-content h1 {
  font-family: var(--bw-font-heading);
  font-weight: 700;
  font-size: var(--bw-text-display);
  line-height: var(--bw-text-display-lh);
  letter-spacing: -0.02em;
}

h2,
.sl-markdown-content h2 {
  font-family: var(--bw-font-heading);
  font-weight: 700;
  font-size: var(--bw-text-h2);
  line-height: var(--bw-text-h2-lh);
  letter-spacing: -0.01em;
}

h3,
.sl-markdown-content h3 {
  font-family: var(--bw-font-heading);
  font-weight: 600;
  font-size: var(--bw-text-h3);
  line-height: var(--bw-text-h3-lh);
}

/* --------------------------------------------------------------------------
   Body Styles
   -------------------------------------------------------------------------- */
body,
.sl-markdown-content {
  font-family: var(--bw-font-body);
  font-size: var(--bw-text-body);
  line-height: var(--bw-text-body-lh);
}

/* --------------------------------------------------------------------------
   Code Styles
   -------------------------------------------------------------------------- */
code,
.sl-markdown-content code {
  font-family: var(--bw-font-code);
  font-size: var(--bw-text-code);
  line-height: var(--bw-text-code-inline-lh);
}

pre code,
.sl-markdown-content pre code {
  line-height: var(--bw-text-code-block-lh);
}

/* --------------------------------------------------------------------------
   Caption / Small Text
   -------------------------------------------------------------------------- */
small,
.sl-markdown-content .caption,
figcaption {
  font-family: var(--bw-font-body);
  font-size: var(--bw-text-small);
  line-height: var(--bw-text-small-lh);
  color: var(--bw-text-muted);
}
```

### Step 4.3 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all architect-css tests PASS

### Step 4.4 — Visual verification in playground

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Open `http://localhost:4321/Monads/` — verify headings render in Space Grotesk, body in DM Sans
- [ ] Stop dev server

### Step 4.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/styles/architect.css \
  docs/packages/starlight-theme-bogoware/styles/__tests__/architect-css.test.ts
git commit -m "feat: add Architect typography CSS with Space Grotesk, DM Sans, Space Mono"
```

---

## Task 5: Florentine typography CSS

**Goal:** Create the Florentine typography system (Cormorant Garamond + Crimson Pro + Fira Code) with `@font-face` declarations and type scale.

### Step 5.1 — Write failing test: florentine CSS has correct font declarations

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/__tests__/florentine-css.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve } from 'node:path';

const css = readFileSync(
  resolve(__dirname, '..', 'florentine.css'),
  'utf-8'
);

describe('florentine.css', () => {
  it('should declare Cormorant Garamond font-face', () => {
    expect(css).toContain("font-family: 'Cormorant Garamond'");
    expect(css).toContain('font-display: swap');
  });

  it('should declare Crimson Pro font-face', () => {
    expect(css).toContain("font-family: 'Crimson Pro'");
  });

  it('should declare Crimson Pro italic', () => {
    expect(css).toContain('font-style: italic');
  });

  it('should declare Fira Code font-face', () => {
    expect(css).toContain("font-family: 'Fira Code'");
  });

  it('should define heading font-family variable', () => {
    expect(css).toContain("--bw-font-heading: 'Cormorant Garamond'");
  });

  it('should define body font-family variable', () => {
    expect(css).toContain("--bw-font-body: 'Crimson Pro'");
  });

  it('should define code font-family variable', () => {
    expect(css).toContain("--bw-font-code: 'Fira Code'");
  });

  it('should define larger display heading for Florentine (40px = 2.5rem)', () => {
    expect(css).toContain('--bw-text-display: 2.5rem');
  });

  it('should reference WOFF2 font files', () => {
    expect(css).toContain('.woff2');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (florentine.css does not exist)

### Step 5.2 — Implement florentine.css

- [ ] Create file: `docs/packages/starlight-theme-bogoware/styles/florentine.css`

```css
/* ==========================================================================
   @bogoware/starlight-theme — Florentine Typography
   Cormorant Garamond (headings) + Crimson Pro (body) + Fira Code (code)
   For blog posts, essays, and long-form narrative content.
   ========================================================================== */

/* --------------------------------------------------------------------------
   @font-face Declarations — Self-hosted WOFF2 with Google Fonts CDN fallback
   -------------------------------------------------------------------------- */

/* Cormorant Garamond — SemiBold (600) */
@font-face {
  font-family: 'Cormorant Garamond';
  font-style: normal;
  font-weight: 600;
  font-display: swap;
  src: local('Cormorant Garamond SemiBold'),
       url('../assets/fonts/CormorantGaramond-SemiBold.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/cormorantgaramond/v16/co3WmX5slCNuHLi8bLeY9MK7whWMhyjQAllvuQWJ5heb_w.woff2') format('woff2');
}

/* Cormorant Garamond — Bold (700) */
@font-face {
  font-family: 'Cormorant Garamond';
  font-style: normal;
  font-weight: 700;
  font-display: swap;
  src: local('Cormorant Garamond Bold'),
       url('../assets/fonts/CormorantGaramond-Bold.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/cormorantgaramond/v16/co3WmX5slCNuHLi8bLeY9MK7whWMhyjQornlvuQWJ5heb_w.woff2') format('woff2');
}

/* Crimson Pro — Regular (400) */
@font-face {
  font-family: 'Crimson Pro';
  font-style: normal;
  font-weight: 400;
  font-display: swap;
  src: local('Crimson Pro Regular'),
       url('../assets/fonts/CrimsonPro-Regular.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/crimsonpro/v24/q5uUsoa5M_tv7IihmnkabC5XiXCAlXGks1WZzm18OJE_VNWjQ.woff2') format('woff2');
}

/* Crimson Pro — Italic (400i) */
@font-face {
  font-family: 'Crimson Pro';
  font-style: italic;
  font-weight: 400;
  font-display: swap;
  src: local('Crimson Pro Italic'),
       url('../assets/fonts/CrimsonPro-Italic.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/crimsonpro/v24/q5uSsoa5M_tv7IihmnkabAReu49Y_Bo-HVKMBi4Ue5s7dtC4yZNE.woff2') format('woff2');
}

/* Fira Code — Regular (400) */
@font-face {
  font-family: 'Fira Code';
  font-style: normal;
  font-weight: 400;
  font-display: swap;
  src: local('Fira Code Regular'),
       url('../assets/fonts/FiraCode-Regular.woff2') format('woff2'),
       url('https://fonts.gstatic.com/s/firacode/v22/uU9eCBsR6Z2vfE9aq3bL0fxyUs4tcw4W_D1sJVD7MOzloj0.woff2') format('woff2');
}

/* --------------------------------------------------------------------------
   Typography Tokens
   -------------------------------------------------------------------------- */
:root {
  /* Font families */
  --bw-font-heading: 'Cormorant Garamond', Georgia, 'Times New Roman', serif;
  --bw-font-body: 'Crimson Pro', Georgia, 'Times New Roman', serif;
  --bw-font-code: 'Fira Code', 'Cascadia Code', 'JetBrains Mono', monospace;

  /* Type scale — Headings (slightly larger for serif elegance) */
  --bw-text-display: 2.5rem;        /* 40px — H1 / Display */
  --bw-text-display-lh: 1.15;
  --bw-text-h2: 2rem;               /* 32px */
  --bw-text-h2-lh: 1.2;
  --bw-text-h3: 1.5rem;             /* 24px */
  --bw-text-h3-lh: 1.25;

  /* Type scale — Body (larger for serif readability) */
  --bw-text-body: 1.125rem;         /* 18px */
  --bw-text-body-lg: 1.25rem;       /* 20px */
  --bw-text-body-lh: 1.75;
  --bw-text-small: 0.9375rem;       /* 15px */
  --bw-text-small-lh: 1.5;

  /* Type scale — Code */
  --bw-text-code: 0.9375rem;        /* 15px */
  --bw-text-code-inline-lh: 1.6;
  --bw-text-code-block-lh: 1.7;

  /* Map to Starlight CSS custom properties */
  --sl-font: var(--bw-font-body);
  --sl-font-mono: var(--bw-font-code);
}

/* --------------------------------------------------------------------------
   Heading Styles
   -------------------------------------------------------------------------- */
h1,
.sl-markdown-content h1 {
  font-family: var(--bw-font-heading);
  font-weight: 700;
  font-size: var(--bw-text-display);
  line-height: var(--bw-text-display-lh);
  letter-spacing: -0.01em;
}

h2,
.sl-markdown-content h2 {
  font-family: var(--bw-font-heading);
  font-weight: 600;
  font-size: var(--bw-text-h2);
  line-height: var(--bw-text-h2-lh);
}

h3,
.sl-markdown-content h3 {
  font-family: var(--bw-font-heading);
  font-weight: 500;
  font-size: var(--bw-text-h3);
  line-height: var(--bw-text-h3-lh);
}

/* --------------------------------------------------------------------------
   Body Styles
   -------------------------------------------------------------------------- */
body,
.sl-markdown-content {
  font-family: var(--bw-font-body);
  font-size: var(--bw-text-body);
  line-height: var(--bw-text-body-lh);
}

/* --------------------------------------------------------------------------
   Code Styles
   -------------------------------------------------------------------------- */
code,
.sl-markdown-content code {
  font-family: var(--bw-font-code);
  font-size: var(--bw-text-code);
  line-height: var(--bw-text-code-inline-lh);
}

pre code,
.sl-markdown-content pre code {
  line-height: var(--bw-text-code-block-lh);
}

/* --------------------------------------------------------------------------
   Caption / Small Text
   -------------------------------------------------------------------------- */
small,
.sl-markdown-content .caption,
figcaption {
  font-family: var(--bw-font-body);
  font-size: var(--bw-text-small);
  line-height: var(--bw-text-small-lh);
  color: var(--bw-text-muted);
}
```

### Step 5.3 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all florentine-css tests PASS

### Step 5.4 — Visual verification: switch mode and check

- [ ] Modify `docs/sites/monads/astro.config.mjs` — change `mode: 'architect'` to `mode: 'florentine'`
- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Verify: headings render in Cormorant Garamond, body in Crimson Pro
- [ ] Stop dev server
- [ ] Revert mode back to `'architect'` in `astro.config.mjs`

### Step 5.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/styles/florentine.css \
  docs/packages/starlight-theme-bogoware/styles/__tests__/florentine-css.test.ts
git commit -m "feat: add Florentine typography CSS with Cormorant Garamond, Crimson Pro, Fira Code"
```

---

## Task 6: Logo component

**Goal:** Create the `BogowareLogo.astro` component and all SVG logo asset variants.

### Step 6.1 — Create SVG logo assets

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/logo-dark.svg`

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <circle cx="60" cy="60" r="56" stroke="#e2e8f0" stroke-width="1.5" opacity="0.15" fill="none"/>
  <line x1="60" y1="4" x2="60" y2="116" stroke="#e2e8f0" stroke-width="1.8" opacity="0.08"/>
  <line x1="4" y1="60" x2="116" y2="60" stroke="#e2e8f0" stroke-width="1.8" opacity="0.10"/>
  <line x1="18" y1="18" x2="102" y2="102" stroke="#e2e8f0" stroke-width="1.8" opacity="0.08"/>
  <line x1="102" y1="18" x2="18" y2="102" stroke="#e2e8f0" stroke-width="1.8" opacity="0.12"/>
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#grad-a-dark)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#grad-b-dark)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <circle cx="60" cy="60" r="3.5" fill="#e2e8f0" opacity="0.9"/>
  <defs>
    <linearGradient id="grad-a-dark" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#6366f1"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#99f0ff"/>
    </linearGradient>
    <linearGradient id="grad-b-dark" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#99f0ff"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#6366f1"/>
    </linearGradient>
  </defs>
</svg>
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/logo-light.svg`

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <circle cx="60" cy="60" r="56" stroke="#1e293b" stroke-width="1.5" opacity="0.15" fill="none"/>
  <line x1="60" y1="4" x2="60" y2="116" stroke="#1e293b" stroke-width="1.8" opacity="0.08"/>
  <line x1="4" y1="60" x2="116" y2="60" stroke="#1e293b" stroke-width="1.8" opacity="0.10"/>
  <line x1="18" y1="18" x2="102" y2="102" stroke="#1e293b" stroke-width="1.8" opacity="0.08"/>
  <line x1="102" y1="18" x2="18" y2="102" stroke="#1e293b" stroke-width="1.8" opacity="0.12"/>
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#grad-a-light)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#grad-b-light)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <circle cx="60" cy="60" r="3.5" fill="#1e293b" opacity="0.9"/>
  <defs>
    <linearGradient id="grad-a-light" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#4f46e5"/>
      <stop offset="50%" stop-color="#7c3aed"/>
      <stop offset="100%" stop-color="#06b6d4"/>
    </linearGradient>
    <linearGradient id="grad-b-light" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#06b6d4"/>
      <stop offset="50%" stop-color="#7c3aed"/>
      <stop offset="100%" stop-color="#4f46e5"/>
    </linearGradient>
  </defs>
</svg>
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/logo-mark.svg`

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#grad-a-mark)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#grad-b-mark)" stroke-width="6" stroke-linecap="round" fill="none"/>
  <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9"/>
  <defs>
    <linearGradient id="grad-a-mark" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#6366f1"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#99f0ff"/>
    </linearGradient>
    <linearGradient id="grad-b-mark" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#99f0ff"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#6366f1"/>
    </linearGradient>
  </defs>
</svg>
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/logo-favicon.svg`

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <style>
    @media (prefers-color-scheme: light) {
      .vertex-dot { fill: #1e293b; }
    }
    @media (prefers-color-scheme: dark) {
      .vertex-dot { fill: #e2e8f0; }
    }
  </style>
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#grad-a-fav)" stroke-width="8" stroke-linecap="round" fill="none"/>
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#grad-b-fav)" stroke-width="8" stroke-linecap="round" fill="none"/>
  <circle class="vertex-dot" cx="60" cy="60" r="5" opacity="0.9"/>
  <defs>
    <linearGradient id="grad-a-fav" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#6366f1"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#99f0ff"/>
    </linearGradient>
    <linearGradient id="grad-b-fav" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
      <stop offset="0%" stop-color="#99f0ff"/>
      <stop offset="50%" stop-color="#8b5cf6"/>
      <stop offset="100%" stop-color="#6366f1"/>
    </linearGradient>
  </defs>
</svg>
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/logo-monochrome.svg`

```svg
<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 120 120" fill="none">
  <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="currentColor" stroke-width="6" stroke-linecap="round" fill="none"/>
  <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="currentColor" stroke-width="6" stroke-linecap="round" stroke-dasharray="6,4" fill="none"/>
  <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9"/>
</svg>
```

### Step 6.2 — Write failing test: BogowareLogo component exists and has correct structure

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/__tests__/logo.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

const componentsDir = resolve(__dirname, '..');

describe('BogowareLogo.astro', () => {
  it('should exist', () => {
    expect(existsSync(resolve(componentsDir, 'BogowareLogo.astro'))).toBe(true);
  });

  it('should accept variant prop', () => {
    const content = readFileSync(resolve(componentsDir, 'BogowareLogo.astro'), 'utf-8');
    expect(content).toContain('variant');
  });

  it('should accept size prop', () => {
    const content = readFileSync(resolve(componentsDir, 'BogowareLogo.astro'), 'utf-8');
    expect(content).toContain('size');
  });

  it('should contain SVG path data', () => {
    const content = readFileSync(resolve(componentsDir, 'BogowareLogo.astro'), 'utf-8');
    expect(content).toContain('<svg');
    expect(content).toContain('viewBox');
  });
});

describe('SVG assets', () => {
  const assetsDir = resolve(__dirname, '..', '..', 'assets');
  const requiredAssets = [
    'logo-dark.svg',
    'logo-light.svg',
    'logo-mark.svg',
    'logo-favicon.svg',
    'logo-monochrome.svg',
  ];

  for (const asset of requiredAssets) {
    it(`should include ${asset}`, () => {
      expect(existsSync(resolve(assetsDir, asset))).toBe(true);
    });
  }
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: BogowareLogo tests FAIL (component does not exist yet), SVG asset tests PASS

### Step 6.3 — Implement BogowareLogo.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/BogowareLogo.astro`

```astro
---
export interface Props {
  /** Logo variant: 'full' includes container circle and field lines,
   *  'simplified' shows only vertex curves + accent dot,
   *  'monochrome' uses single-color with solid/dashed differentiation */
  variant?: 'full' | 'simplified' | 'monochrome';
  /** Size in pixels (applied as width and height) */
  size?: number;
  /** Additional CSS class */
  class?: string;
}

const {
  variant = 'simplified',
  size = 32,
  class: className = '',
} = Astro.props;
---

{variant === 'full' && (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 120 120"
    fill="none"
    width={size}
    height={size}
    class:list={['bw-logo', 'bw-logo--full', className]}
    aria-label="Bogoware logo"
    role="img"
  >
    <circle cx="60" cy="60" r="56" stroke="currentColor" stroke-width="1.5" opacity="0.15" fill="none" />
    <line x1="60" y1="4" x2="60" y2="116" stroke="currentColor" stroke-width="1.8" opacity="0.08" />
    <line x1="4" y1="60" x2="116" y2="60" stroke="currentColor" stroke-width="1.8" opacity="0.10" />
    <line x1="18" y1="18" x2="102" y2="102" stroke="currentColor" stroke-width="1.8" opacity="0.08" />
    <line x1="102" y1="18" x2="18" y2="102" stroke="currentColor" stroke-width="1.8" opacity="0.12" />
    <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#bw-grad-a)" stroke-width="6" stroke-linecap="round" fill="none" />
    <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#bw-grad-b)" stroke-width="6" stroke-linecap="round" fill="none" />
    <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9" />
    <defs>
      <linearGradient id="bw-grad-a" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
        <stop offset="0%" stop-color="var(--bw-indigo, #6366f1)" />
        <stop offset="50%" stop-color="var(--bw-violet, #8b5cf6)" />
        <stop offset="100%" stop-color="var(--bw-cyan, #99f0ff)" />
      </linearGradient>
      <linearGradient id="bw-grad-b" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
        <stop offset="0%" stop-color="var(--bw-cyan, #99f0ff)" />
        <stop offset="50%" stop-color="var(--bw-violet, #8b5cf6)" />
        <stop offset="100%" stop-color="var(--bw-indigo, #6366f1)" />
      </linearGradient>
    </defs>
  </svg>
)}

{variant === 'simplified' && (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 120 120"
    fill="none"
    width={size}
    height={size}
    class:list={['bw-logo', 'bw-logo--simplified', className]}
    aria-label="Bogoware logo"
    role="img"
  >
    <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="url(#bw-grad-a-s)" stroke-width="6" stroke-linecap="round" fill="none" />
    <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="url(#bw-grad-b-s)" stroke-width="6" stroke-linecap="round" fill="none" />
    <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9" />
    <defs>
      <linearGradient id="bw-grad-a-s" x1="20" y1="16" x2="100" y2="104" gradientUnits="userSpaceOnUse">
        <stop offset="0%" stop-color="var(--bw-indigo, #6366f1)" />
        <stop offset="50%" stop-color="var(--bw-violet, #8b5cf6)" />
        <stop offset="100%" stop-color="var(--bw-cyan, #99f0ff)" />
      </linearGradient>
      <linearGradient id="bw-grad-b-s" x1="20" y1="104" x2="100" y2="16" gradientUnits="userSpaceOnUse">
        <stop offset="0%" stop-color="var(--bw-cyan, #99f0ff)" />
        <stop offset="50%" stop-color="var(--bw-violet, #8b5cf6)" />
        <stop offset="100%" stop-color="var(--bw-indigo, #6366f1)" />
      </linearGradient>
    </defs>
  </svg>
)}

{variant === 'monochrome' && (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    viewBox="0 0 120 120"
    fill="none"
    width={size}
    height={size}
    class:list={['bw-logo', 'bw-logo--monochrome', className]}
    aria-label="Bogoware logo"
    role="img"
  >
    <path d="M 20,16 C 38,36 46,52 60,60 C 74,68 82,84 100,104" stroke="currentColor" stroke-width="6" stroke-linecap="round" fill="none" />
    <path d="M 20,104 C 38,84 46,68 60,60 C 74,52 82,36 100,16" stroke="currentColor" stroke-width="6" stroke-linecap="round" stroke-dasharray="6,4" fill="none" />
    <circle cx="60" cy="60" r="3.5" fill="currentColor" opacity="0.9" />
  </svg>
)}
```

### Step 6.4 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all logo and SVG asset tests PASS

### Step 6.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/components/BogowareLogo.astro \
  docs/packages/starlight-theme-bogoware/components/__tests__/logo.test.ts \
  docs/packages/starlight-theme-bogoware/assets/logo-dark.svg \
  docs/packages/starlight-theme-bogoware/assets/logo-light.svg \
  docs/packages/starlight-theme-bogoware/assets/logo-mark.svg \
  docs/packages/starlight-theme-bogoware/assets/logo-favicon.svg \
  docs/packages/starlight-theme-bogoware/assets/logo-monochrome.svg
git commit -m "feat: add BogowareLogo component and all SVG logo variants"
```

---

## Task 7: Header override

**Goal:** Create a custom Header component with the Bogoware logo and a gradient accent bar.

### Step 7.1 — Write failing test: Header.astro exists and has required structure

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/__tests__/header.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

const headerPath = resolve(__dirname, '..', 'Header.astro');

describe('Header.astro', () => {
  it('should exist', () => {
    expect(existsSync(headerPath)).toBe(true);
  });

  it('should import BogowareLogo', () => {
    const content = readFileSync(headerPath, 'utf-8');
    expect(content).toContain('BogowareLogo');
  });

  it('should contain a header element', () => {
    const content = readFileSync(headerPath, 'utf-8');
    expect(content).toContain('<header');
  });

  it('should contain gradient accent bar', () => {
    const content = readFileSync(headerPath, 'utf-8');
    expect(content).toContain('accent-bar');
  });

  it('should contain a nav element', () => {
    const content = readFileSync(headerPath, 'utf-8');
    expect(content).toContain('<nav');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (Header.astro does not exist)

### Step 7.2 — Implement Header.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/Header.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';
import BogowareLogo from '../components/BogowareLogo.astro';

const { hasSidebar, locale } = Astro.props;
---

<div class="bw-accent-bar accent-bar"></div>
<header class="bw-header">
  <nav class="bw-header__nav" aria-label="Main navigation">
    <div class="bw-header__left">
      <a href={`${import.meta.env.BASE_URL}`} class="bw-header__logo-link" aria-label="Home">
        <BogowareLogo variant="simplified" size={28} />
        <span class="bw-header__site-title">
          <slot name="site-title" />
        </span>
      </a>
    </div>

    <div class="bw-header__center">
      <slot name="search" />
    </div>

    <div class="bw-header__right">
      <slot name="social-icons" />
      <slot name="theme-select" />
    </div>
  </nav>
</header>

<style>
  .bw-accent-bar {
    height: 3px;
    background: var(--bw-gradient-full, linear-gradient(135deg, #6366f1, #8b5cf6, #99f0ff));
    width: 100%;
    position: fixed;
    top: 0;
    left: 0;
    z-index: 100;
  }

  .bw-header {
    position: fixed;
    top: 3px;
    left: 0;
    right: 0;
    height: calc(var(--bw-navbar-height, 64px) - 3px);
    background: var(--bw-surface, #1e293b);
    border-bottom: 1px solid var(--bw-border, #334155);
    z-index: 99;
    display: flex;
    align-items: center;
    padding: 0 var(--bw-space-6, 24px);
  }

  .bw-header__nav {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    max-width: var(--bw-content-wide-max-width, 1200px);
    margin: 0 auto;
    gap: var(--bw-space-4, 16px);
  }

  .bw-header__left {
    display: flex;
    align-items: center;
    flex-shrink: 0;
  }

  .bw-header__logo-link {
    display: flex;
    align-items: center;
    gap: var(--bw-space-2, 8px);
    text-decoration: none;
    color: var(--bw-text, #e2e8f0);
  }

  .bw-header__logo-link:hover {
    opacity: 0.85;
  }

  .bw-header__site-title {
    font-family: var(--bw-font-heading, sans-serif);
    font-weight: 700;
    font-size: 1.125rem;
    letter-spacing: -0.01em;
  }

  .bw-header__center {
    flex: 1;
    display: flex;
    justify-content: center;
    max-width: 400px;
  }

  .bw-header__right {
    display: flex;
    align-items: center;
    gap: var(--bw-space-2, 8px);
    flex-shrink: 0;
  }

  @media (max-width: 767px) {
    .bw-header__site-title {
      display: none;
    }
    .bw-header__center {
      max-width: none;
    }
  }
</style>
```

### Step 7.3 — Verify test passes

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all header tests PASS

### Step 7.4 — Visual verification

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Verify: custom header renders with logo and gradient bar at top
- [ ] Stop dev server

### Step 7.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/overrides/Header.astro \
  docs/packages/starlight-theme-bogoware/overrides/__tests__/header.test.ts
git commit -m "feat: add custom Header override with Bogoware logo and gradient accent bar"
```

---

## Task 8: Hero override + FieldLines component

**Goal:** Create the decorative FieldLines SVG background and Hero section with gradient title and CTA.

### Step 8.1 — Write failing test: FieldLines and Hero components exist

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/__tests__/fieldlines.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

describe('FieldLines.astro', () => {
  const filePath = resolve(__dirname, '..', 'FieldLines.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should contain SVG element', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('<svg');
  });

  it('should contain line elements for field lines', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('<line');
  });

  it('should accept density prop', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('density');
  });
});
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/__tests__/hero.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

describe('Hero.astro', () => {
  const filePath = resolve(__dirname, '..', 'Hero.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should import FieldLines', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('FieldLines');
  });

  it('should contain hero section', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('bw-hero');
  });

  it('should contain gradient title styling', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('gradient');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: tests FAIL (components do not exist)

### Step 8.2 — Implement FieldLines.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/FieldLines.astro`

```astro
---
export interface Props {
  /** Number of field lines in each direction (vertical, horizontal, diagonal) */
  density?: number;
  /** Base opacity for field lines (0-1) */
  opacity?: number;
  /** Additional CSS class */
  class?: string;
}

const {
  density = 8,
  opacity = 0.06,
  class: className = '',
} = Astro.props;

// Generate line positions distributed across the viewbox
const viewSize = 600;
const step = viewSize / (density + 1);
const lines: Array<{ x1: number; y1: number; x2: number; y2: number; opacity: number }> = [];

// Vertical lines
for (let i = 1; i <= density; i++) {
  const x = Math.round(step * i);
  lines.push({ x1: x, y1: 0, x2: x, y2: viewSize, opacity: opacity * (0.6 + Math.random() * 0.4) });
}

// Horizontal lines
for (let i = 1; i <= density; i++) {
  const y = Math.round(step * i);
  lines.push({ x1: 0, y1: y, x2: viewSize, y2: y, opacity: opacity * (0.6 + Math.random() * 0.4) });
}

// Diagonal lines (fewer, for subtlety)
const diagCount = Math.max(2, Math.floor(density / 2));
const diagStep = viewSize / (diagCount + 1);
for (let i = 1; i <= diagCount; i++) {
  const offset = Math.round(diagStep * i);
  // Top-left to bottom-right
  lines.push({ x1: offset - 100, y1: 0, x2: offset + 100, y2: viewSize, opacity: opacity * 0.5 });
  // Top-right to bottom-left
  lines.push({ x1: viewSize - offset + 100, y1: 0, x2: viewSize - offset - 100, y2: viewSize, opacity: opacity * 0.5 });
}
---

<svg
  xmlns="http://www.w3.org/2000/svg"
  viewBox={`0 0 ${viewSize} ${viewSize}`}
  fill="none"
  class:list={['bw-field-lines', className]}
  aria-hidden="true"
  preserveAspectRatio="xMidYMid slice"
>
  {lines.map((line) => (
    <line
      x1={line.x1}
      y1={line.y1}
      x2={line.x2}
      y2={line.y2}
      stroke="currentColor"
      stroke-width="1"
      opacity={line.opacity}
    />
  ))}
</svg>

<style>
  .bw-field-lines {
    position: absolute;
    inset: 0;
    width: 100%;
    height: 100%;
    pointer-events: none;
    color: var(--bw-text-muted, #94a3b8);
  }
</style>
```

### Step 8.3 — Implement Hero.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/Hero.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';
import FieldLines from '../components/FieldLines.astro';
import BogowareLogo from '../components/BogowareLogo.astro';

const { data } = Astro.props.entry;
const hero = data.hero;
const title = hero?.title ?? data.title;
const tagline = hero?.tagline;
const actions = hero?.actions ?? [];
---

{hero && (
  <section class="bw-hero">
    <div class="bw-hero__bg">
      <FieldLines density={10} opacity={0.05} />
    </div>

    <div class="bw-hero__content">
      <div class="bw-hero__logo">
        <BogowareLogo variant="full" size={120} />
      </div>

      <h1 class="bw-hero__title gradient-text">
        {title}
      </h1>

      {tagline && (
        <p class="bw-hero__tagline">{tagline}</p>
      )}

      {actions.length > 0 && (
        <div class="bw-hero__actions">
          {actions.map((action) => (
            <a
              href={action.link}
              class:list={[
                'bw-hero__action',
                action.variant === 'primary' ? 'bw-hero__action--primary' : 'bw-hero__action--secondary',
              ]}
            >
              {action.label}
            </a>
          ))}
        </div>
      )}
    </div>
  </section>
)}

<style>
  .bw-hero {
    position: relative;
    padding: var(--bw-space-16, 64px) var(--bw-space-6, 24px);
    text-align: center;
    overflow: hidden;
    min-height: 400px;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .bw-hero__bg {
    position: absolute;
    inset: 0;
    z-index: 0;
  }

  .bw-hero__content {
    position: relative;
    z-index: 1;
    max-width: var(--bw-content-max-width, 720px);
    margin: 0 auto;
  }

  .bw-hero__logo {
    margin-bottom: var(--bw-space-8, 32px);
    display: flex;
    justify-content: center;
  }

  .bw-hero__title {
    font-family: var(--bw-font-heading, sans-serif);
    font-weight: 800;
    font-size: clamp(2rem, 5vw, 3.5rem);
    line-height: 1.1;
    margin-bottom: var(--bw-space-4, 16px);
  }

  .gradient-text {
    background: var(--bw-gradient-full, linear-gradient(135deg, #6366f1, #8b5cf6, #99f0ff));
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
  }

  .bw-hero__tagline {
    font-family: var(--bw-font-body, sans-serif);
    font-size: 1.25rem;
    color: var(--bw-text-muted, #94a3b8);
    margin-bottom: var(--bw-space-8, 32px);
    max-width: 560px;
    margin-inline: auto;
    line-height: 1.6;
  }

  .bw-hero__actions {
    display: flex;
    gap: var(--bw-space-4, 16px);
    justify-content: center;
    flex-wrap: wrap;
  }

  .bw-hero__action {
    display: inline-flex;
    align-items: center;
    padding: var(--bw-space-3, 12px) var(--bw-space-6, 24px);
    border-radius: var(--bw-radius-md, 8px);
    font-family: var(--bw-font-body, sans-serif);
    font-weight: 500;
    font-size: 1rem;
    text-decoration: none;
    transition: all var(--bw-transition-fast, 150ms ease);
  }

  .bw-hero__action--primary {
    background: var(--bw-gradient-primary, linear-gradient(135deg, #6366f1, #8b5cf6));
    color: #ffffff;
  }

  .bw-hero__action--primary:hover {
    opacity: 0.9;
    transform: translateY(-1px);
  }

  .bw-hero__action--secondary {
    border: 1px solid var(--bw-border, #334155);
    color: var(--bw-text, #e2e8f0);
    background: transparent;
  }

  .bw-hero__action--secondary:hover {
    border-color: var(--bw-indigo, #6366f1);
    color: var(--bw-indigo, #6366f1);
  }
</style>
```

### Step 8.4 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all FieldLines and Hero tests PASS

### Step 8.5 — Visual verification with hero content

- [ ] Modify `docs/sites/monads/src/content/docs/index.mdx` to add hero frontmatter:

```mdx
---
title: Bogoware.Monads
description: Functional programming patterns for C#
hero:
  title: Bogoware.Monads
  tagline: Functional programming patterns for C# — Result, Option, and beyond.
  actions:
    - label: Get Started
      link: /Monads/getting-started/
      variant: primary
    - label: API Reference
      link: /Monads/api/
      variant: secondary
---

Welcome to the Bogoware.Monads documentation.
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Verify: hero section renders with field lines background, gradient title, CTA buttons
- [ ] Stop dev server

### Step 8.6 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/components/FieldLines.astro \
  docs/packages/starlight-theme-bogoware/components/__tests__/fieldlines.test.ts \
  docs/packages/starlight-theme-bogoware/overrides/Hero.astro \
  docs/packages/starlight-theme-bogoware/overrides/__tests__/hero.test.ts \
  docs/sites/monads/src/content/docs/index.mdx
git commit -m "feat: add Hero override with FieldLines decorative background and gradient title"
```

---

## Task 9: Sidebar + Footer overrides

**Goal:** Create branded Sidebar and Footer component overrides.

### Step 9.1 — Write failing tests

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/__tests__/sidebar.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

describe('Sidebar.astro', () => {
  const filePath = resolve(__dirname, '..', 'Sidebar.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should contain sidebar element or class', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('bw-sidebar');
  });

  it('should contain active-state indicator styling', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('active');
  });
});
```

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/__tests__/footer.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

describe('Footer.astro', () => {
  const filePath = resolve(__dirname, '..', 'Footer.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should contain footer element', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('<footer');
  });

  it('should contain copyright text', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('Bogoware');
  });

  it('should import BogowareLogo', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('BogowareLogo');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: tests FAIL

### Step 9.2 — Implement Sidebar.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/Sidebar.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';
import Default from '@astrojs/starlight/components/Sidebar.astro';
---

<div class="bw-sidebar">
  <Default {...Astro.props}><slot /></Default>
</div>

<style is:global>
  .bw-sidebar {
    --sl-color-bg-sidebar: var(--bw-surface, #1e293b);
  }

  /* Active state indicator */
  .bw-sidebar nav a[aria-current='page'],
  .bw-sidebar nav a.active {
    color: var(--bw-indigo, #6366f1);
    font-weight: 600;
    border-inline-start: 3px solid var(--bw-indigo, #6366f1);
    padding-inline-start: calc(var(--bw-space-4, 16px) - 3px);
    background: color-mix(in srgb, var(--bw-indigo, #6366f1) 8%, transparent);
    border-radius: 0 var(--bw-radius-sm, 4px) var(--bw-radius-sm, 4px) 0;
  }

  /* Sidebar link hover */
  .bw-sidebar nav a:hover {
    color: var(--bw-indigo, #6366f1);
    background: color-mix(in srgb, var(--bw-indigo, #6366f1) 5%, transparent);
  }

  /* Sidebar group labels */
  .bw-sidebar .top-level > li > details > summary,
  .bw-sidebar .top-level > li > a {
    font-family: var(--bw-font-heading, sans-serif);
    font-weight: 600;
    font-size: 0.875rem;
    text-transform: uppercase;
    letter-spacing: 0.05em;
    color: var(--bw-text-muted, #94a3b8);
  }
</style>
```

### Step 9.3 — Implement Footer.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/Footer.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';
import BogowareLogo from '../components/BogowareLogo.astro';

const currentYear = new Date().getFullYear();
---

<footer class="bw-footer">
  <div class="bw-footer__inner">
    <div class="bw-footer__brand">
      <BogowareLogo variant="simplified" size={24} />
      <span class="bw-footer__copyright">
        &copy; {currentYear} Bogoware. All rights reserved.
      </span>
    </div>

    <div class="bw-footer__links">
      <a href="https://github.com/Bogoware" class="bw-footer__link" target="_blank" rel="noopener noreferrer">
        GitHub
      </a>
      <a href="https://www.npmjs.com/org/bogoware" class="bw-footer__link" target="_blank" rel="noopener noreferrer">
        NPM
      </a>
    </div>
  </div>
</footer>

<style>
  .bw-footer {
    border-top: 1px solid var(--bw-border, #334155);
    padding: var(--bw-space-8, 32px) var(--bw-space-6, 24px);
    background: var(--bw-surface, #1e293b);
  }

  .bw-footer__inner {
    max-width: var(--bw-content-wide-max-width, 1200px);
    margin: 0 auto;
    display: flex;
    align-items: center;
    justify-content: space-between;
    flex-wrap: wrap;
    gap: var(--bw-space-4, 16px);
  }

  .bw-footer__brand {
    display: flex;
    align-items: center;
    gap: var(--bw-space-2, 8px);
  }

  .bw-footer__copyright {
    font-family: var(--bw-font-body, sans-serif);
    font-size: var(--bw-text-small, 0.875rem);
    color: var(--bw-text-muted, #94a3b8);
  }

  .bw-footer__links {
    display: flex;
    gap: var(--bw-space-4, 16px);
  }

  .bw-footer__link {
    font-family: var(--bw-font-body, sans-serif);
    font-size: var(--bw-text-small, 0.875rem);
    color: var(--bw-text-muted, #94a3b8);
    text-decoration: none;
    transition: color var(--bw-transition-fast, 150ms ease);
  }

  .bw-footer__link:hover {
    color: var(--bw-indigo, #6366f1);
  }
</style>
```

### Step 9.4 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all sidebar and footer tests PASS

### Step 9.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/overrides/Sidebar.astro \
  docs/packages/starlight-theme-bogoware/overrides/Footer.astro \
  docs/packages/starlight-theme-bogoware/overrides/__tests__/sidebar.test.ts \
  docs/packages/starlight-theme-bogoware/overrides/__tests__/footer.test.ts
git commit -m "feat: add Sidebar and Footer overrides with brand styling"
```

---

## Task 10: SEO — Head override + SocialMeta

**Goal:** Inject font preconnect, favicon, canonical URL, Open Graph meta tags, Twitter Card meta tags, and JSON-LD structured data.

### Step 10.1 — Write failing tests

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/__tests__/head.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

describe('Head.astro', () => {
  const filePath = resolve(__dirname, '..', 'Head.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should contain preconnect link for Google Fonts', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('fonts.googleapis.com');
    expect(content).toContain('fonts.gstatic.com');
    expect(content).toContain('preconnect');
  });

  it('should contain favicon link', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('favicon');
  });

  it('should contain canonical URL', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('canonical');
  });
});

describe('SocialMeta.astro', () => {
  const filePath = resolve(__dirname, '..', 'SocialMeta.astro');

  it('should exist', () => {
    expect(existsSync(filePath)).toBe(true);
  });

  it('should contain og:title', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('og:title');
  });

  it('should contain og:description', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('og:description');
  });

  it('should contain og:image', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('og:image');
  });

  it('should contain twitter:card', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('twitter:card');
  });

  it('should contain JSON-LD structured data', () => {
    const content = readFileSync(filePath, 'utf-8');
    expect(content).toContain('application/ld+json');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: tests FAIL

### Step 10.2 — Implement Head.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/Head.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';
import Default from '@astrojs/starlight/components/Head.astro';

const canonicalUrl = new URL(Astro.url.pathname, Astro.site);
---

<!-- Default Starlight head -->
<Default {...Astro.props}><slot /></Default>

<!-- Google Fonts preconnect -->
<link rel="preconnect" href="https://fonts.googleapis.com" />
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />

<!-- Favicon -->
<link rel="icon" type="image/svg+xml" href={`${import.meta.env.BASE_URL}favicon.svg`} />
<link rel="icon" type="image/x-icon" href={`${import.meta.env.BASE_URL}favicon.ico`} />
<link rel="apple-touch-icon" sizes="180x180" href={`${import.meta.env.BASE_URL}apple-touch-icon.png`} />

<!-- Canonical URL -->
<link rel="canonical" href={canonicalUrl.href} />

<!-- Sitemap reference -->
<link rel="sitemap" href={`${import.meta.env.BASE_URL}sitemap-index.xml`} />

<!-- Robots -->
<meta name="robots" content="index, follow" />
```

### Step 10.3 — Implement SocialMeta.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/overrides/SocialMeta.astro`

```astro
---
import type { Props } from '@astrojs/starlight/props';

const { entry } = Astro.props;
const pageTitle = entry.data.title;
const pageDescription = entry.data.description ?? '';
const canonicalUrl = new URL(Astro.url.pathname, Astro.site).href;
const siteUrl = Astro.site?.href ?? '';
const ogImageUrl = new URL(`${import.meta.env.BASE_URL}og/${entry.slug || 'index'}.png`, Astro.site).href;

// Determine structured data type based on content
const isApiPage = Astro.url.pathname.includes('/api/');
const jsonLdType = isApiPage ? 'SoftwareSourceCode' : 'TechArticle';

const jsonLd = {
  '@context': 'https://schema.org',
  '@type': jsonLdType,
  headline: pageTitle,
  description: pageDescription,
  author: {
    '@type': 'Organization',
    name: 'Bogoware',
    url: siteUrl,
  },
  publisher: {
    '@type': 'Organization',
    name: 'Bogoware',
    logo: {
      '@type': 'ImageObject',
      url: `${siteUrl}logo.png`,
    },
  },
  mainEntityOfPage: canonicalUrl,
  image: ogImageUrl,
  ...(isApiPage ? { programmingLanguage: 'C#' } : {}),
};
---

<!-- Open Graph -->
<meta property="og:type" content="article" />
<meta property="og:title" content={pageTitle} />
<meta property="og:description" content={pageDescription} />
<meta property="og:image" content={ogImageUrl} />
<meta property="og:image:width" content="1200" />
<meta property="og:image:height" content="630" />
<meta property="og:url" content={canonicalUrl} />
<meta property="og:site_name" content="Bogoware" />
<meta property="og:locale" content="en_US" />

<!-- Twitter/X Card -->
<meta name="twitter:card" content="summary_large_image" />
<meta name="twitter:title" content={pageTitle} />
<meta name="twitter:description" content={pageDescription} />
<meta name="twitter:image" content={ogImageUrl} />

<!-- JSON-LD Structured Data -->
<script type="application/ld+json" set:html={JSON.stringify(jsonLd)} />
```

### Step 10.4 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all Head and SocialMeta tests PASS

### Step 10.5 — Visual verification: view page source

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads build`
- [ ] Inspect the built HTML in `docs/sites/monads/dist/index.html`
- [ ] Verify: `<link rel="preconnect">`, `<link rel="canonical">`, `<meta property="og:title">`, `<script type="application/ld+json">` are all present

### Step 10.6 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/overrides/Head.astro \
  docs/packages/starlight-theme-bogoware/overrides/SocialMeta.astro \
  docs/packages/starlight-theme-bogoware/overrides/__tests__/head.test.ts
git commit -m "feat: add Head and SocialMeta overrides for SEO, OG tags, and JSON-LD"
```

---

## Task 11: Dynamic OG image generation

**Goal:** Generate branded 1200x630 PNG images per page at build time using satori + sharp.

### Step 11.1 — Install dependencies

- [ ] Add to `docs/packages/starlight-theme-bogoware/package.json` dependencies:

```json
"dependencies": {
  "zod": "^3.23.0",
  "satori": "^0.12.0",
  "sharp": "^0.33.0"
}
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm install`

### Step 11.2 — Write failing test: OG image generation utility

- [ ] Create file: `docs/packages/starlight-theme-bogoware/lib/__tests__/og-image.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { generateOgImage } from '../og-image.js';

describe('generateOgImage', () => {
  it('should return a Buffer (PNG)', async () => {
    const result = await generateOgImage({
      title: 'Test Page',
      description: 'A test page description',
      siteName: 'Bogoware.Monads',
      siteUrl: 'https://bogoware.github.io/Monads/',
      mode: 'architect',
    });
    expect(result).toBeInstanceOf(Buffer);
    // PNG magic bytes: 137 80 78 71
    expect(result[0]).toBe(137);
    expect(result[1]).toBe(80);
    expect(result[2]).toBe(78);
    expect(result[3]).toBe(71);
  }, 30000);

  it('should produce an image of 1200x630', async () => {
    const sharp = await import('sharp');
    const buf = await generateOgImage({
      title: 'Dimensions Test',
      description: 'Verify dimensions',
      siteName: 'Bogoware',
      siteUrl: 'https://bogoware.github.io',
      mode: 'architect',
    });
    const metadata = await sharp.default(buf).metadata();
    expect(metadata.width).toBe(1200);
    expect(metadata.height).toBe(630);
  }, 30000);
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS (module does not exist)

### Step 11.3 — Implement og-image.ts generation utility

- [ ] Create file: `docs/packages/starlight-theme-bogoware/lib/og-image.ts`

```typescript
import satori from 'satori';
import sharp from 'sharp';
import { readFileSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));

export interface OgImageOptions {
  title: string;
  description: string;
  siteName: string;
  siteUrl: string;
  mode: 'architect' | 'florentine';
}

/** Load a font file, returning an ArrayBuffer. Returns empty buffer
 *  if file is missing (CI/test environments). */
function loadFont(relativePath: string): ArrayBuffer {
  try {
    const fontPath = resolve(__dirname, '..', 'assets', 'fonts', relativePath);
    const buffer = readFileSync(fontPath);
    return buffer.buffer.slice(
      buffer.byteOffset,
      buffer.byteOffset + buffer.byteLength
    );
  } catch {
    return new ArrayBuffer(0);
  }
}

export async function generateOgImage(
  options: OgImageOptions
): Promise<Buffer> {
  const { title, description, siteName, siteUrl, mode } = options;

  // Choose font based on mode
  const headingFont =
    mode === 'architect'
      ? 'SpaceGrotesk-Bold.woff2'
      : 'CormorantGaramond-Bold.woff2';
  const bodyFont =
    mode === 'architect'
      ? 'DMSans-Regular.woff2'
      : 'CrimsonPro-Regular.woff2';

  const fonts = [
    {
      name: 'Heading',
      data: loadFont(headingFont),
      weight: 700 as const,
      style: 'normal' as const,
    },
    {
      name: 'Body',
      data: loadFont(bodyFont),
      weight: 400 as const,
      style: 'normal' as const,
    },
  ].filter((f) => f.data.byteLength > 0);

  // Fallback: fetch Inter from Google Fonts if no local fonts found
  if (fonts.length === 0) {
    const response = await fetch(
      'https://fonts.gstatic.com/s/inter/v18/UcCO3FwrK3iLTeHuS_nVMrMxCp50SjIw2boKoduKmMEVuLyfAZ9hjQ.woff2'
    );
    const fontData = await response.arrayBuffer();
    fonts.push({
      name: 'Heading',
      data: fontData,
      weight: 700 as const,
      style: 'normal' as const,
    });
  }

  const svg = await satori(
    {
      type: 'div',
      props: {
        style: {
          width: '1200px',
          height: '630px',
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'space-between',
          padding: '60px 80px',
          background: 'linear-gradient(135deg, #0f172a 0%, #1e293b 100%)',
          color: '#e2e8f0',
          fontFamily: 'Body',
        },
        children: [
          // Top: logo mark + site name
          {
            type: 'div',
            props: {
              style: {
                display: 'flex',
                alignItems: 'center',
                gap: '16px',
              },
              children: [
                {
                  type: 'div',
                  props: {
                    style: {
                      width: '48px',
                      height: '48px',
                      borderRadius: '50%',
                      background:
                        'linear-gradient(135deg, #6366f1, #8b5cf6, #99f0ff)',
                      display: 'flex',
                      alignItems: 'center',
                      justifyContent: 'center',
                    },
                    children: [
                      {
                        type: 'div',
                        props: {
                          style: {
                            width: '8px',
                            height: '8px',
                            borderRadius: '50%',
                            background: '#ffffff',
                          },
                        },
                      },
                    ],
                  },
                },
                {
                  type: 'span',
                  props: {
                    style: {
                      fontSize: '20px',
                      fontFamily: 'Heading',
                      color: '#94a3b8',
                    },
                    children: siteName,
                  },
                },
              ],
            },
          },
          // Middle: title + description
          {
            type: 'div',
            props: {
              style: {
                display: 'flex',
                flexDirection: 'column',
                gap: '16px',
                flex: '1',
                justifyContent: 'center',
              },
              children: [
                {
                  type: 'h1',
                  props: {
                    style: {
                      fontSize: title.length > 40 ? '40px' : '52px',
                      fontFamily: 'Heading',
                      fontWeight: 700,
                      lineHeight: 1.15,
                      margin: 0,
                      color: '#ffffff',
                    },
                    children: title,
                  },
                },
                description
                  ? {
                      type: 'p',
                      props: {
                        style: {
                          fontSize: '22px',
                          lineHeight: 1.5,
                          color: '#94a3b8',
                          margin: 0,
                          maxWidth: '800px',
                        },
                        children: description.slice(0, 160),
                      },
                    }
                  : null,
              ].filter(Boolean),
            },
          },
          // Bottom: gradient bar + URL
          {
            type: 'div',
            props: {
              style: {
                display: 'flex',
                flexDirection: 'column',
                gap: '12px',
              },
              children: [
                {
                  type: 'div',
                  props: {
                    style: {
                      height: '4px',
                      background:
                        'linear-gradient(90deg, #6366f1, #8b5cf6, #99f0ff)',
                      borderRadius: '2px',
                      width: '200px',
                    },
                  },
                },
                {
                  type: 'span',
                  props: {
                    style: {
                      fontSize: '16px',
                      color: '#64748b',
                      fontFamily: 'Body',
                    },
                    children: siteUrl.replace(/\/$/, ''),
                  },
                },
              ],
            },
          },
        ],
      },
    },
    {
      width: 1200,
      height: 630,
      fonts,
    }
  );

  const pngBuffer = await sharp(Buffer.from(svg)).png().toBuffer();
  return pngBuffer;
}
```

### Step 11.4 — Create OG image documentation component

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/OgImage.astro`

```astro
---
/**
 * OgImage.astro — Dynamic OG image template documentation component.
 *
 * The actual OG image generation happens via lib/og-image.ts (satori + sharp),
 * called from integration.ts at build time. This component documents the
 * template structure:
 *
 * - Dark gradient background (#0f172a to #1e293b)
 * - Bogoware logo mark (top-left)
 * - Page title in display font
 * - Page description in body font
 * - Gradient accent bar at bottom
 * - Site URL footer
 *
 * Dimensions: 1200 x 630 pixels (PNG)
 */
export interface Props {
  title: string;
  description: string;
  siteName: string;
  siteUrl: string;
  mode: 'architect' | 'florentine';
}
---

<!-- This component documents the OG image template structure.
     Actual image generation uses satori + sharp in lib/og-image.ts -->
<div class="og-image-preview" style="width: 1200px; height: 630px; background: linear-gradient(135deg, #0f172a, #1e293b); color: #e2e8f0; padding: 60px 80px; display: flex; flex-direction: column; justify-content: space-between;">
  <slot />
</div>
```

### Step 11.5 — Implement the Astro integration for build-time OG generation

- [ ] Create file: `docs/packages/starlight-theme-bogoware/integration.ts`

```typescript
import type { AstroIntegration } from 'astro';
import type { ResolvedBogowareThemeConfig } from './schema.js';
import { generateOgImage } from './lib/og-image.js';
import { writeFileSync, mkdirSync, existsSync } from 'node:fs';
import { resolve, dirname } from 'node:path';

export function createBogowareIntegration(
  config: ResolvedBogowareThemeConfig
): AstroIntegration {
  return {
    name: '@bogoware/starlight-theme-integration',
    hooks: {
      'astro:build:done': async ({ dir, pages }) => {
        // Generate OG images
        if (config.seo.ogImage.enabled && config.seo.ogImage.strategy === 'dynamic') {
          const ogDir = resolve(dir.pathname, 'og');
          if (!existsSync(ogDir)) {
            mkdirSync(ogDir, { recursive: true });
          }

          for (const page of pages) {
            const slug = page.pathname.replace(/\/$/, '') || 'index';
            const outputPath = resolve(ogDir, `${slug}.png`);

            const parentDir = dirname(outputPath);
            if (!existsSync(parentDir)) {
              mkdirSync(parentDir, { recursive: true });
            }

            try {
              const png = await generateOgImage({
                title:
                  slug === 'index'
                    ? config.seo.siteName
                    : slug.split('/').pop() ?? slug,
                description: config.seo.defaultDescription,
                siteName: config.seo.siteName,
                siteUrl: '',
                mode: config.mode,
              });
              writeFileSync(outputPath, png);
            } catch (err) {
              console.warn(
                `[bogoware-theme] Failed to generate OG image for ${slug}:`,
                err
              );
            }
          }

          console.log(
            `[bogoware-theme] Generated OG images for ${pages.length} pages`
          );
        }

        // Generate robots.txt
        const { generateRobotsTxt } = await import('./lib/robots.js');
        const siteUrl = config.seo.siteName
          ? `https://bogoware.github.io/Monads/`
          : '';
        if (siteUrl) {
          const robotsTxt = generateRobotsTxt(siteUrl);
          const robotsPath = resolve(dir.pathname, 'robots.txt');
          writeFileSync(robotsPath, robotsTxt);
          console.log('[bogoware-theme] Generated robots.txt');
        }
      },
    },
  };
}
```

### Step 11.6 — Update index.ts to register the integration

- [ ] Modify `docs/packages/starlight-theme-bogoware/index.ts` — add at the top:

```typescript
import { createBogowareIntegration } from './integration.js';
```

- [ ] Inside the `setup()` hook, after the `updateConfig(...)` call, add:

```typescript
        // Register Astro integration for OG image generation and robots.txt
        addIntegration(createBogowareIntegration(config));
```

### Step 11.7 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: OG image tests PASS (generates valid 1200x630 PNG)

### Step 11.8 — Build verification

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads build`
- [ ] Verify: `docs/sites/monads/dist/og/index.png` exists
- [ ] Verify: `og:image` meta tag in built HTML points to correct URL

### Step 11.9 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/lib/og-image.ts \
  docs/packages/starlight-theme-bogoware/lib/__tests__/og-image.test.ts \
  docs/packages/starlight-theme-bogoware/components/OgImage.astro \
  docs/packages/starlight-theme-bogoware/integration.ts \
  docs/packages/starlight-theme-bogoware/index.ts
git commit -m "feat: add dynamic OG image generation with satori + sharp"
```

---

## Task 12: Google Analytics integration

**Goal:** Create a conditional GA4 analytics component that respects DNT and supports cookieless mode.

### Step 12.1 — Write failing test

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/__tests__/analytics.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

const analyticsPath = resolve(__dirname, '..', 'Analytics.astro');

describe('Analytics.astro', () => {
  it('should exist', () => {
    expect(existsSync(analyticsPath)).toBe(true);
  });

  it('should contain gtag.js URL', () => {
    const content = readFileSync(analyticsPath, 'utf-8');
    expect(content).toContain('googletagmanager.com/gtag/js');
  });

  it('should check navigator.doNotTrack', () => {
    const content = readFileSync(analyticsPath, 'utf-8');
    expect(content).toContain('doNotTrack');
  });

  it('should support cookieless mode', () => {
    const content = readFileSync(analyticsPath, 'utf-8');
    expect(content).toContain('client_storage');
    expect(content).toContain('anonymize_ip');
  });

  it('should accept measurementId prop', () => {
    const content = readFileSync(analyticsPath, 'utf-8');
    expect(content).toContain('measurementId');
  });

  it('should conditionally render based on measurementId', () => {
    const content = readFileSync(analyticsPath, 'utf-8');
    expect(content).toContain('measurementId');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS

### Step 12.2 — Implement Analytics.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/Analytics.astro`

```astro
---
export interface Props {
  /** Google Analytics 4 Measurement ID (e.g., 'G-XXXXXXXXXX') */
  measurementId?: string;
  /** Honor Do Not Track browser header */
  respectDnt?: boolean;
  /** Cookie-less measurement mode (no cookies, anonymized IP) */
  cookieless?: boolean;
}

const {
  measurementId,
  respectDnt = true,
  cookieless = false,
} = Astro.props;
---

{measurementId && (
  <script
    is:inline
    define:vars={{ id: measurementId, respectDnt, cookieless }}
  >
    (function() {
      // Respect Do Not Track if configured
      if (respectDnt && navigator.doNotTrack === '1') {
        return;
      }

      // Load gtag.js asynchronously
      var s = document.createElement('script');
      s.src = 'https://www.googletagmanager.com/gtag/js?id=' + id;
      s.async = true;
      document.head.appendChild(s);

      window.dataLayer = window.dataLayer || [];
      function gtag() { dataLayer.push(arguments); }
      gtag('js', new Date());
      gtag('config', id, Object.assign(
        { page_path: window.location.pathname },
        cookieless ? { client_storage: 'none', anonymize_ip: true } : {}
      ));
    })();
  </script>
)}
```

### Step 12.3 — Wire analytics into Head.astro

- [ ] Modify `docs/packages/starlight-theme-bogoware/overrides/Head.astro` — add import in frontmatter:

```astro
import Analytics from '../components/Analytics.astro';
```

- [ ] Add at the bottom of Head.astro, before any closing tag:

```astro
<!-- Google Analytics (conditional) -->
<Analytics
  measurementId={undefined}
  respectDnt={true}
  cookieless={false}
/>
```

**Note:** The measurementId will be wired through the config system in a later refinement. For now it defaults to undefined (no script injected).

### Step 12.4 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all analytics tests PASS

### Step 12.5 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/components/Analytics.astro \
  docs/packages/starlight-theme-bogoware/components/__tests__/analytics.test.ts \
  docs/packages/starlight-theme-bogoware/overrides/Head.astro
git commit -m "feat: add conditional GA4 analytics with DNT respect and cookieless mode"
```

---

## Task 13: Sitemap + RSS + robots.txt

**Goal:** Configure `@astrojs/sitemap` integration, generate `robots.txt`, and support RSS for Florentine mode.

### Step 13.1 — Install peer dependencies

- [ ] Add to `docs/packages/starlight-theme-bogoware/package.json` peerDependencies:

```json
"@astrojs/sitemap": ">=3.0.0"
```

- [ ] Add to `docs/sites/monads/package.json` dependencies:

```json
"@astrojs/sitemap": "^3.2.0"
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm install`

### Step 13.2 — Write failing test: robots.txt generation utility

- [ ] Create file: `docs/packages/starlight-theme-bogoware/lib/__tests__/robots.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { generateRobotsTxt } from '../robots.js';

describe('generateRobotsTxt', () => {
  it('should contain User-agent: *', () => {
    const result = generateRobotsTxt('https://bogoware.github.io/Monads/');
    expect(result).toContain('User-agent: *');
  });

  it('should contain Allow: /', () => {
    const result = generateRobotsTxt('https://bogoware.github.io/Monads/');
    expect(result).toContain('Allow: /');
  });

  it('should contain Sitemap URL', () => {
    const result = generateRobotsTxt('https://bogoware.github.io/Monads/');
    expect(result).toContain('Sitemap: https://bogoware.github.io/Monads/sitemap-index.xml');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS

### Step 13.3 — Implement robots.ts

- [ ] Create file: `docs/packages/starlight-theme-bogoware/lib/robots.ts`

```typescript
export function generateRobotsTxt(siteUrl: string): string {
  const normalizedUrl = siteUrl.endsWith('/') ? siteUrl : `${siteUrl}/`;

  return [
    'User-agent: *',
    'Allow: /',
    '',
    `Sitemap: ${normalizedUrl}sitemap-index.xml`,
    '',
  ].join('\n');
}
```

### Step 13.4 — Add sitemap integration to index.ts

- [ ] Modify `docs/packages/starlight-theme-bogoware/index.ts` — in the `setup()` hook, after existing `addIntegration` calls, add:

```typescript
        // Register @astrojs/sitemap integration
        try {
          const sitemap = await import('@astrojs/sitemap');
          addIntegration(sitemap.default());
        } catch {
          console.warn('[bogoware-theme] @astrojs/sitemap not found; sitemap generation skipped.');
        }
```

### Step 13.5 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: robots.txt tests PASS

### Step 13.6 — Build verification

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads build`
- [ ] Verify: `docs/sites/monads/dist/robots.txt` exists with correct Sitemap URL
- [ ] Verify: `docs/sites/monads/dist/sitemap-index.xml` exists

### Step 13.7 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/lib/robots.ts \
  docs/packages/starlight-theme-bogoware/lib/__tests__/robots.test.ts \
  docs/packages/starlight-theme-bogoware/integration.ts \
  docs/packages/starlight-theme-bogoware/index.ts \
  docs/sites/monads/package.json
git commit -m "feat: add sitemap integration and robots.txt generation"
```

---

## Task 14: FeatureCard component

**Goal:** Create a reusable feature card grid component for homepage use.

### Step 14.1 — Write failing test

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/__tests__/featurecard.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve } from 'node:path';

const featureCardPath = resolve(__dirname, '..', 'FeatureCard.astro');

describe('FeatureCard.astro', () => {
  it('should exist', () => {
    expect(existsSync(featureCardPath)).toBe(true);
  });

  it('should accept icon prop', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('icon');
  });

  it('should accept title prop', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('title');
  });

  it('should accept description prop', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('description');
  });

  it('should accept href prop', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('href');
  });

  it('should contain gradient accent styling', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('gradient');
  });

  it('should be a link card', () => {
    const content = readFileSync(featureCardPath, 'utf-8');
    expect(content).toContain('<a');
  });
});
```

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: test FAILS

### Step 14.2 — Implement FeatureCard.astro

- [ ] Create file: `docs/packages/starlight-theme-bogoware/components/FeatureCard.astro`

```astro
---
export interface Props {
  /** Emoji or SVG icon string */
  icon: string;
  /** Card title */
  title: string;
  /** Card description */
  description: string;
  /** Link destination */
  href: string;
  /** Additional CSS class */
  class?: string;
}

const { icon, title, description, href, class: className = '' } = Astro.props;
---

<a href={href} class:list={['bw-feature-card', className]}>
  <div class="bw-feature-card__icon">{icon}</div>
  <h3 class="bw-feature-card__title">{title}</h3>
  <p class="bw-feature-card__description">{description}</p>
  <div class="bw-feature-card__gradient-bar"></div>
</a>

<style>
  .bw-feature-card {
    display: flex;
    flex-direction: column;
    padding: var(--bw-space-6, 24px);
    background: var(--bw-surface, #1e293b);
    border: 1px solid var(--bw-border, #334155);
    border-radius: var(--bw-radius-lg, 12px);
    text-decoration: none;
    color: var(--bw-text, #e2e8f0);
    transition: all var(--bw-transition-base, 250ms ease);
    position: relative;
    overflow: hidden;
  }

  .bw-feature-card:hover {
    border-color: var(--bw-indigo, #6366f1);
    transform: translateY(-2px);
    box-shadow: 0 8px 24px color-mix(in srgb, var(--bw-indigo, #6366f1) 15%, transparent);
  }

  .bw-feature-card__icon {
    font-size: 2rem;
    margin-bottom: var(--bw-space-3, 12px);
  }

  .bw-feature-card__title {
    font-family: var(--bw-font-heading, sans-serif);
    font-weight: 600;
    font-size: 1.125rem;
    margin: 0 0 var(--bw-space-2, 8px) 0;
    line-height: 1.3;
  }

  .bw-feature-card__description {
    font-family: var(--bw-font-body, sans-serif);
    font-size: 0.9375rem;
    color: var(--bw-text-muted, #94a3b8);
    margin: 0;
    line-height: 1.6;
    flex: 1;
  }

  .bw-feature-card__gradient-bar {
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
    height: 3px;
    background: var(--bw-gradient-accent, linear-gradient(135deg, #6366f1, #99f0ff));
    opacity: 0;
    transition: opacity var(--bw-transition-fast, 150ms ease);
  }

  .bw-feature-card:hover .bw-feature-card__gradient-bar {
    opacity: 1;
  }
</style>
```

### Step 14.3 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all FeatureCard tests PASS

### Step 14.4 — Add feature cards to playground homepage

- [ ] Modify `docs/sites/monads/src/content/docs/index.mdx` — add after the hero content:

```mdx
import FeatureCard from '@bogoware/starlight-theme/components/FeatureCard.astro';

<div style="display: grid; grid-template-columns: repeat(auto-fit, minmax(280px, 1fr)); gap: 24px; margin-top: 48px;">
  <FeatureCard
    icon="🎯"
    title="Type-Safe Errors"
    description="Replace exceptions with Result<T> for predictable, composable error handling."
    href="/Monads/concepts/"
  />
  <FeatureCard
    icon="🔗"
    title="Monadic Chaining"
    description="Compose operations fluently with Map, Bind, and Match — no null checks."
    href="/Monads/guides/"
  />
  <FeatureCard
    icon="📚"
    title="Full API Reference"
    description="Complete documentation for every type, method, and extension in the library."
    href="/Monads/api/"
  />
</div>
```

### Step 14.5 — Visual verification

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs && pnpm -C sites/monads dev`
- [ ] Verify: 3 feature cards render in a grid on the homepage
- [ ] Stop dev server

### Step 14.6 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/components/FeatureCard.astro \
  docs/packages/starlight-theme-bogoware/components/__tests__/featurecard.test.ts \
  docs/sites/monads/src/content/docs/index.mdx
git commit -m "feat: add FeatureCard component with gradient accent and hover effects"
```

---

## Task 15: Font assets — self-hosted WOFF2

**Goal:** Download and add WOFF2 font files for both typography systems so fonts work offline.

### Step 15.1 — Download font files

- [ ] Create directory: `docs/packages/starlight-theme-bogoware/assets/fonts/`
- [ ] Download all required WOFF2 files:

```bash
# Space Grotesk Bold
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/SpaceGrotesk-Bold.woff2 \
  "https://fonts.gstatic.com/s/spacegrotesk/v16/V8mDoQDjQSkFtoMM3T6r8E7mPbF4C_k3HqU.woff2"

# Space Grotesk ExtraBold
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/SpaceGrotesk-ExtraBold.woff2 \
  "https://fonts.gstatic.com/s/spacegrotesk/v16/V8mDoQDjQSkFtoMM3T6r8E7mPb14Dvk3.woff2"

# DM Sans Regular
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/DMSans-Regular.woff2 \
  "https://fonts.gstatic.com/s/dmsans/v15/rP2tp2ywxg089UriI5-g4vlH9VoD8CmcqZG40F9JadbnoEwA.woff2"

# DM Sans Medium
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/DMSans-Medium.woff2 \
  "https://fonts.gstatic.com/s/dmsans/v15/rP2tp2ywxg089UriI5-g4vlH9VoD8CmcqZG40F9JadbnoEwA.woff2"

# Space Mono Regular
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/SpaceMono-Regular.woff2 \
  "https://fonts.gstatic.com/s/spacemono/v13/i7dPIFZifjKcF5UAWdDRYEF8RQ.woff2"

# Cormorant Garamond SemiBold
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/CormorantGaramond-SemiBold.woff2 \
  "https://fonts.gstatic.com/s/cormorantgaramond/v16/co3WmX5slCNuHLi8bLeY9MK7whWMhyjQAllvuQWJ5heb_w.woff2"

# Cormorant Garamond Bold
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/CormorantGaramond-Bold.woff2 \
  "https://fonts.gstatic.com/s/cormorantgaramond/v16/co3WmX5slCNuHLi8bLeY9MK7whWMhyjornlvuQWJ5heb_w.woff2"

# Crimson Pro Regular
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/CrimsonPro-Regular.woff2 \
  "https://fonts.gstatic.com/s/crimsonpro/v24/q5uUsoa5M_tv7IihmnkabC5XiXCAlXGks1WZzm18OJE_VNWjQ.woff2"

# Crimson Pro Italic
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/CrimsonPro-Italic.woff2 \
  "https://fonts.gstatic.com/s/crimsonpro/v24/q5uSsoa5M_tv7IihmnkabAReu49Y_Bo-HVKMBi4Ue5s7dtC4yZNE.woff2"

# Fira Code Regular
curl -L -o docs/packages/starlight-theme-bogoware/assets/fonts/FiraCode-Regular.woff2 \
  "https://fonts.gstatic.com/s/firacode/v22/uU9eCBsR6Z2vfE9aq3bL0fxyUs4tcw4W_D1sJVD7MOzloj0.woff2"
```

### Step 15.2 — Write test: all font files exist

- [ ] Create file: `docs/packages/starlight-theme-bogoware/assets/__tests__/fonts.test.ts`

```typescript
import { describe, it, expect } from 'vitest';
import { existsSync, statSync } from 'node:fs';
import { resolve } from 'node:path';

const fontsDir = resolve(__dirname, '..', 'fonts');

const requiredFonts = [
  'SpaceGrotesk-Bold.woff2',
  'SpaceGrotesk-ExtraBold.woff2',
  'DMSans-Regular.woff2',
  'DMSans-Medium.woff2',
  'SpaceMono-Regular.woff2',
  'CormorantGaramond-SemiBold.woff2',
  'CormorantGaramond-Bold.woff2',
  'CrimsonPro-Regular.woff2',
  'CrimsonPro-Italic.woff2',
  'FiraCode-Regular.woff2',
];

describe('Font assets', () => {
  for (const font of requiredFonts) {
    it(`should include ${font}`, () => {
      const fontPath = resolve(fontsDir, font);
      expect(existsSync(fontPath)).toBe(true);
    });

    it(`${font} should be non-empty`, () => {
      const fontPath = resolve(fontsDir, font);
      if (existsSync(fontPath)) {
        const stat = statSync(fontPath);
        expect(stat.size).toBeGreaterThan(0);
      }
    });
  }
});
```

### Step 15.3 — Verify tests pass

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: all font asset tests PASS

### Step 15.4 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/assets/fonts/ \
  docs/packages/starlight-theme-bogoware/assets/__tests__/fonts.test.ts
git commit -m "chore: add self-hosted WOFF2 font files for offline builds"
```

---

## Task 16: Integration tests

**Goal:** Build the playground site end-to-end and verify correctness.

### Step 16.1 — Create integration test script

- [ ] Create file: `docs/packages/starlight-theme-bogoware/tests/integration.test.ts`

```typescript
import { describe, it, expect, beforeAll } from 'vitest';
import { execFileSync } from 'node:child_process';
import { existsSync, readFileSync, readdirSync } from 'node:fs';
import { resolve } from 'node:path';

const monadsDir = resolve(__dirname, '..', '..', '..', 'sites', 'monads');
const distDir = resolve(monadsDir, 'dist');

describe('Integration: Full site build', () => {
  beforeAll(() => {
    execFileSync('pnpm', ['run', 'build'], {
      cwd: monadsDir,
      stdio: 'pipe',
      timeout: 120000,
    });
  }, 180000);

  it('should produce a dist directory', () => {
    expect(existsSync(distDir)).toBe(true);
  });

  it('should produce an index.html', () => {
    expect(existsSync(resolve(distDir, 'index.html'))).toBe(true);
  });

  it('should contain correct meta description in index.html', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('<meta');
    expect(html).toContain('description');
  });

  it('should contain OG meta tags in index.html', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('og:title');
  });

  it('should contain JSON-LD in index.html', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('application/ld+json');
  });

  it('should contain preconnect hints', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('fonts.googleapis.com');
    expect(html).toContain('preconnect');
  });

  it('should contain brand CSS tokens', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(
      html.includes('--bw-indigo') || existsSync(resolve(distDir, '_astro'))
    ).toBe(true);
  });

  it('should generate robots.txt', () => {
    expect(existsSync(resolve(distDir, 'robots.txt'))).toBe(true);
    const robots = readFileSync(resolve(distDir, 'robots.txt'), 'utf-8');
    expect(robots).toContain('User-agent: *');
    expect(robots).toContain('Sitemap:');
  });

  it('should generate sitemap', () => {
    const hasSitemap =
      existsSync(resolve(distDir, 'sitemap-index.xml')) ||
      existsSync(resolve(distDir, 'sitemap-0.xml'));
    expect(hasSitemap).toBe(true);
  });

  it('should not contain build error markers in HTML', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).not.toContain('AstroError');
    expect(html).not.toContain('Internal Server Error');
  });
});

describe('Integration: Architect mode', () => {
  it('should load architect CSS (Space Grotesk reference in built assets)', () => {
    if (!existsSync(distDir)) return;

    const astroDir = resolve(distDir, '_astro');
    if (existsSync(astroDir)) {
      const cssFiles = readdirSync(astroDir).filter((f) => f.endsWith('.css'));
      const allCss = cssFiles
        .map((f) => readFileSync(resolve(astroDir, f), 'utf-8'))
        .join('');
      expect(allCss).toContain('Space Grotesk');
    }
  });
});
```

### Step 16.2 — Add integration test script to package.json

- [ ] Modify `docs/packages/starlight-theme-bogoware/package.json` — update scripts:

```json
"scripts": {
  "test": "vitest run",
  "test:watch": "vitest",
  "test:integration": "vitest run tests/integration.test.ts --timeout 180000"
}
```

### Step 16.3 — Run integration tests

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test:integration`
- [ ] Verify: all integration tests PASS
- [ ] If any fail, debug and fix the relevant component, then re-run

### Step 16.4 — Commit

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/tests/integration.test.ts \
  docs/packages/starlight-theme-bogoware/package.json
git commit -m "test: add end-to-end integration tests for full site build"
```

---

## Task 17: Package preparation + publish

**Goal:** Finalize `package.json`, verify the package tarball, write README, and publish to NPM.

### Step 17.1 — Finalize package.json

- [ ] Verify `docs/packages/starlight-theme-bogoware/package.json` has complete fields:

```json
{
  "name": "@bogoware/starlight-theme",
  "version": "0.1.0",
  "description": "Bogoware brand theme for Astro Starlight — dual typography (Architect/Florentine), SEO optimization, and branded components.",
  "type": "module",
  "exports": {
    ".": "./index.ts",
    "./styles/*": "./styles/*",
    "./components/*": "./components/*",
    "./overrides/*": "./overrides/*",
    "./assets/*": "./assets/*"
  },
  "peerDependencies": {
    "@astrojs/starlight": ">=0.30.0",
    "astro": ">=5.0.0",
    "@astrojs/sitemap": ">=3.0.0"
  },
  "dependencies": {
    "zod": "^3.23.0",
    "satori": "^0.12.0",
    "sharp": "^0.33.0"
  },
  "devDependencies": {
    "@astrojs/starlight": "^0.30.0",
    "astro": "^5.17.0",
    "typescript": "^5.7.0",
    "vitest": "^3.0.0"
  },
  "scripts": {
    "test": "vitest run",
    "test:watch": "vitest",
    "test:integration": "vitest run tests/integration.test.ts --timeout 180000"
  },
  "keywords": ["astro", "starlight", "theme", "bogoware", "starlight-plugin"],
  "license": "MIT",
  "author": "Bogoware <info@bogoware.com>",
  "repository": {
    "type": "git",
    "url": "https://github.com/Bogoware/Monads.git",
    "directory": "docs/packages/starlight-theme-bogoware"
  },
  "homepage": "https://bogoware.github.io/Monads/",
  "files": [
    "index.ts",
    "integration.ts",
    "schema.ts",
    "lib/",
    "styles/",
    "overrides/",
    "components/",
    "assets/",
    "!**/__tests__/",
    "!**/*.test.ts"
  ]
}
```

### Step 17.2 — Write README.md

- [ ] Create file: `docs/packages/starlight-theme-bogoware/README.md`

```markdown
# @bogoware/starlight-theme

Bogoware brand theme plugin for [Astro Starlight](https://starlight.astro.build/).

## Features

- **Dual typography system:** Architect mode (geometric sans-serif) and Florentine mode (serif)
- **Bogoliubov Vertex logo:** SVG logo with gradient strokes, multiple variants
- **Full color system:** Dark and light mode tokens, code syntax accents
- **SEO optimized:** Open Graph meta, Twitter Cards, JSON-LD, sitemap, robots.txt
- **Dynamic OG images:** Per-page branded social images generated at build time
- **Google Analytics:** Optional GA4 with DNT respect and cookieless mode
- **Branded components:** Header, Hero, Sidebar, Footer, FeatureCard

## Installation

    pnpm add @bogoware/starlight-theme

## Usage

    // astro.config.mjs
    import starlight from '@astrojs/starlight';
    import bogowareTheme from '@bogoware/starlight-theme';

    export default defineConfig({
      integrations: [
        starlight({
          title: 'My Docs',
          plugins: [
            bogowareTheme({
              mode: 'architect',       // or 'florentine'
              logoVariant: 'simplified',
              fieldLines: true,
              seo: {
                siteName: 'My Project',
                defaultDescription: 'Project documentation',
              },
              analytics: {
                googleAnalyticsId: 'G-XXXXXXXXXX',
              },
            }),
          ],
        }),
      ],
    });

## Configuration

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| mode | 'architect' or 'florentine' | 'architect' | Typography system |
| logoVariant | 'full' or 'simplified' or 'monochrome' | 'simplified' | Navbar logo variant |
| accentColor | string | undefined | Override primary accent color |
| fieldLines | boolean | true | Enable decorative field-line backgrounds |
| seo | object | See schema.ts | SEO and social sharing config |
| analytics | object | See schema.ts | Google Analytics config |

## License

MIT
```

### Step 17.3 — Dry-run pack to verify tarball contents

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && npm pack --dry-run`
- [ ] Verify: output lists all expected files (index.ts, schema.ts, integration.ts, lib/, styles/, overrides/, components/, assets/)
- [ ] Verify: no test files or `__tests__/` directories are included

### Step 17.4 — Run all tests one final time

- [ ] Run: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && pnpm test`
- [ ] Verify: ALL tests pass

### Step 17.5 — Commit final package preparation

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git add docs/packages/starlight-theme-bogoware/package.json \
  docs/packages/starlight-theme-bogoware/README.md
git commit -m "chore: finalize package.json and README for @bogoware/starlight-theme v0.1.0"
```

### Step 17.6 — Publish to NPM

- [ ] Verify NPM authentication: `npm whoami`
- [ ] Verify scope access: `npm access ls-packages @bogoware` (or create org if needed)
- [ ] Publish: `cd /Users/mr/git/Bogoware/Monads/docs/packages/starlight-theme-bogoware && npm publish --access public`
- [ ] Verify: package appears at `https://www.npmjs.com/package/@bogoware/starlight-theme`

### Step 17.7 — Tag the release

- [ ] Run:

```bash
cd /Users/mr/git/Bogoware/Monads
git tag -a starlight-theme-v0.1.0 -m "Release @bogoware/starlight-theme v0.1.0"
git push origin rel/prod --tags
```

---

## Summary of Files Created

```
docs/
├── pnpm-workspace.yaml
├── package.json
├── packages/
│   └── starlight-theme-bogoware/
│       ├── package.json
│       ├── tsconfig.json
│       ├── README.md
│       ├── index.ts                          # Plugin entry point
│       ├── integration.ts                    # Astro integration (OG, robots.txt)
│       ├── schema.ts                         # Zod config validation
│       ├── schema.test.ts                    # Schema unit tests
│       ├── plugin.test.ts                    # Plugin unit tests
│       ├── lib/
│       │   ├── og-image.ts                   # OG image generation (satori + sharp)
│       │   ├── robots.ts                     # robots.txt generation
│       │   └── __tests__/
│       │       ├── og-image.test.ts
│       │       └── robots.test.ts
│       ├── styles/
│       │   ├── base.css                      # Palette, spacing, layout tokens
│       │   ├── architect.css                 # Architect typography
│       │   ├── florentine.css                # Florentine typography
│       │   └── __tests__/
│       │       ├── base-css.test.ts
│       │       ├── architect-css.test.ts
│       │       └── florentine-css.test.ts
│       ├── components/
│       │   ├── BogowareLogo.astro
│       │   ├── FieldLines.astro
│       │   ├── FeatureCard.astro
│       │   ├── OgImage.astro
│       │   ├── Analytics.astro
│       │   └── __tests__/
│       │       ├── logo.test.ts
│       │       ├── fieldlines.test.ts
│       │       ├── featurecard.test.ts
│       │       └── analytics.test.ts
│       ├── overrides/
│       │   ├── Header.astro
│       │   ├── Hero.astro
│       │   ├── Sidebar.astro
│       │   ├── Footer.astro
│       │   ├── Head.astro
│       │   ├── SocialMeta.astro
│       │   └── __tests__/
│       │       ├── header.test.ts
│       │       ├── hero.test.ts
│       │       ├── sidebar.test.ts
│       │       ├── footer.test.ts
│       │       └── head.test.ts
│       ├── assets/
│       │   ├── logo-dark.svg
│       │   ├── logo-light.svg
│       │   ├── logo-mark.svg
│       │   ├── logo-favicon.svg
│       │   ├── logo-monochrome.svg
│       │   ├── fonts/
│       │   │   ├── SpaceGrotesk-Bold.woff2
│       │   │   ├── SpaceGrotesk-ExtraBold.woff2
│       │   │   ├── DMSans-Regular.woff2
│       │   │   ├── DMSans-Medium.woff2
│       │   │   ├── SpaceMono-Regular.woff2
│       │   │   ├── CormorantGaramond-SemiBold.woff2
│       │   │   ├── CormorantGaramond-Bold.woff2
│       │   │   ├── CrimsonPro-Regular.woff2
│       │   │   ├── CrimsonPro-Italic.woff2
│       │   │   └── FiraCode-Regular.woff2
│       │   └── __tests__/
│       │       └── fonts.test.ts
│       └── tests/
│           └── integration.test.ts
└── sites/
    └── monads/
        ├── package.json
        ├── astro.config.mjs
        └── src/
            ├── content.config.ts
            └── content/
                └── docs/
                    └── index.mdx
```

## Commit History (Expected)

1. `chore: scaffold pnpm workspace monorepo with theme package and playground site`
2. `feat: implement Zod config schema and Starlight plugin entry point`
3. `feat: add base CSS with palette tokens, spacing scale, and layout dimensions`
4. `feat: add Architect typography CSS with Space Grotesk, DM Sans, Space Mono`
5. `feat: add Florentine typography CSS with Cormorant Garamond, Crimson Pro, Fira Code`
6. `feat: add BogowareLogo component and all SVG logo variants`
7. `feat: add custom Header override with Bogoware logo and gradient accent bar`
8. `feat: add Hero override with FieldLines decorative background and gradient title`
9. `feat: add Sidebar and Footer overrides with brand styling`
10. `feat: add Head and SocialMeta overrides for SEO, OG tags, and JSON-LD`
11. `feat: add dynamic OG image generation with satori + sharp`
12. `feat: add conditional GA4 analytics with DNT respect and cookieless mode`
13. `feat: add sitemap integration and robots.txt generation`
14. `feat: add FeatureCard component with gradient accent and hover effects`
15. `chore: add self-hosted WOFF2 font files for offline builds`
16. `test: add end-to-end integration tests for full site build`
17. `chore: finalize package.json and README for @bogoware/starlight-theme v0.1.0`

---

## Post-Plan Execution Tasks

After Plan 1 is complete and `@bogoware/starlight-theme` is published, execute the following:

### Subsequent Plans (one per subsystem)

| Plan | Repo | Description |
|---|---|---|
| **Plan 2** | `/Users/mr/git/Bogoware/Monads` | Migrate Docusaurus docs to Astro/Starlight (mode: architect) |
| **Plan 3** | `/Users/mr/git/Bogoware/Localization` | Migrate Docusaurus docs to Astro/Starlight (mode: architect) |
| **Plan 4** | `/Users/mr/git/MrBogomips/blog` | Replace starter blog with Astro/Starlight (mode: florentine) |
| **Plan 5** | Theme repo | Export all logo/brand assets (SVG, PNG, ICO, EPS) |
| **Plan 6** | Theme repo | Write SKILL.md for reusable documentation implementation |

### CLAUDE.md Revision (all repos)

After ALL plans are executed, run `/claude-md-management:revise-claude-md` in each repository to update CLAUDE.md with current project context:

- [ ] `/Users/mr/git/Bogoware/Monads` — Update with Astro/Starlight stack, theme dependency, build commands
- [ ] `/Users/mr/git/Bogoware/Localization` — Update with Astro/Starlight stack, theme dependency
- [ ] `/Users/mr/git/MrBogomips/blog` — Update with Astro/Starlight stack, Florentine mode, blog setup
- [ ] Theme repo (`@bogoware/starlight-theme`) — Create CLAUDE.md for the theme package itself
