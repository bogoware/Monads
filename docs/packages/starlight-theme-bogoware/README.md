# @bogoware/starlight-theme

Bogoware brand identity theme for [Astro Starlight](https://starlight.astro.build/) documentation sites.

## Features

- **Dual typography system**: Architect mode (Space Grotesk + DM Sans) for technical docs, Florentine mode (Cormorant Garamond + Crimson Pro) for blogs
- **Complete brand identity**: Bogoliubov Vertex logo, color palette, spacing system
- **SEO optimized**: Font preconnect, favicon, Open Graph meta support
- **Google Analytics**: Optional GA4 with DNT respect and cookieless mode
- **Sitemap**: Automatic sitemap generation via @astrojs/sitemap
- **Self-hosted fonts**: WOFF2 font files for offline/air-gapped builds

## Installation

```bash
pnpm add @bogoware/starlight-theme
```

## Usage

```javascript
// astro.config.mjs
import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  integrations: [
    starlight({
      title: 'My Project',
      plugins: [
        bogowareTheme({
          mode: 'architect',        // or 'florentine'
          logoVariant: 'simplified', // 'full' | 'simplified' | 'monochrome'
          fieldLines: true,
          analytics: {
            googleAnalyticsId: 'G-XXXXXXXXXX', // optional
          },
        }),
      ],
    }),
  ],
});
```

## Typography Modes

| Mode | Display | Body | Code | Best For |
|------|---------|------|------|----------|
| **Architect** | Space Grotesk | DM Sans | Space Mono | Technical docs, APIs |
| **Florentine** | Cormorant Garamond | Crimson Pro | Fira Code | Blogs, essays |

## License

MIT
