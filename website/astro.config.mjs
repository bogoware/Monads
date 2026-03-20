import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  site: 'https://bogoware.github.io',
  base: '/Monads/',
  integrations: [
    starlight({
      title: 'Bogoware.Monads',
      tagline: 'Functional programming patterns for C# — Result, Maybe, and more',
      logo: {
        src: './src/assets/logo-favicon.svg',
        replacesTitle: false,
      },
      plugins: [
        bogowareTheme({
          mode: 'architect',
          logoVariant: 'simplified',
          fieldLines: true,
          seo: {
            siteName: 'Bogoware.Monads',
            defaultDescription: 'Functional programming patterns for C# — Result, Maybe, and more',
            structuredData: { type: 'SoftwareSourceCode', author: 'Bogoware' },
          },
          analytics: { googleAnalyticsId: 'G-JK35QX0826' },
        }),
      ],
      social: {
        github: 'https://github.com/bogoware/monads',
      },
      editLink: {
        baseUrl: 'https://github.com/bogoware/monads/tree/rel/prod/website/',
      },
      sidebar: [
        { label: 'Introduction', slug: '' },
        { label: 'Changelog', slug: 'changelog' },
        {
          label: 'Getting Started',
          autogenerate: { directory: 'getting-started' },
        },
        {
          label: 'Concepts',
          autogenerate: { directory: 'concepts' },
        },
        {
          label: 'Guides',
          autogenerate: { directory: 'guides' },
        },
        {
          label: 'API Reference',
          autogenerate: { directory: 'api' },
        },
      ],
    }),
  ],
});
