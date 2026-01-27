import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

const config: Config = {
  title: 'Bogoware.Monads',
  tagline: 'Functional programming patterns for C# - Result, Maybe, and more',
  favicon: 'img/favicon.ico',

  future: {
    v4: true,
  },

  url: 'https://bogoware.github.io',
  baseUrl: '/monads/',

  organizationName: 'bogoware',
  projectName: 'monads',

  onBrokenLinks: 'throw',

  markdown: {
    format: 'detect',
  },

  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          routeBasePath: '/', // Docs-only mode: serve docs at root
          editUrl: 'https://github.com/bogoware/monads/tree/rel/prod/docs/',
        },
        blog: false, // Disable blog
        theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    colorMode: {
      defaultMode: 'light',
      disableSwitch: false,
      respectPrefersColorScheme: true,
    },
    navbar: {
      title: 'Bogoware.Monads',
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'docsSidebar',
          position: 'left',
          label: 'Documentation',
        },
        {
          type: 'docSidebar',
          sidebarId: 'apiSidebar',
          position: 'left',
          label: 'API Reference',
        },
        {
          href: 'https://www.nuget.org/packages/Bogoware.Monads',
          label: 'NuGet',
          position: 'right',
        },
        {
          href: 'https://github.com/bogoware/monads',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Documentation',
          items: [
            {
              label: 'Getting Started',
              to: '/',
            },
            {
              label: 'API Reference',
              to: '/api',
            },
            {
              label: 'Changelog',
              to: '/changelog',
            },
          ],
        },
        {
          title: 'Resources',
          items: [
            {
              label: 'NuGet Package',
              href: 'https://www.nuget.org/packages/Bogoware.Monads',
            },
            {
              label: 'GitHub Repository',
              href: 'https://github.com/bogoware/monads',
            },
            {
              label: 'Report an Issue',
              href: 'https://github.com/bogoware/monads/issues',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} Bogoware. Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['csharp', 'bash', 'json'],
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
