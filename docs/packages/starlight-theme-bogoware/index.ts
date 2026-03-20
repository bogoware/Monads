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
      async setup({ updateConfig, addIntegration, astroConfig, config: starlightConfig, logger }) {
        logger.info(`Bogoware theme loaded (mode: ${config.mode})`);

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

        const componentOverrides: Record<string, string> = {
          Header: '@bogoware/starlight-theme/overrides/Header.astro',
          Hero: '@bogoware/starlight-theme/overrides/Hero.astro',
          Sidebar: '@bogoware/starlight-theme/overrides/Sidebar.astro',
          Footer: '@bogoware/starlight-theme/overrides/Footer.astro',
          Head: '@bogoware/starlight-theme/overrides/Head.astro',
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

        // Analytics integration
        if (config.analytics.googleAnalyticsId) {
          const id = config.analytics.googleAnalyticsId;
          const respectDnt = config.analytics.respectDnt;
          const cookieless = config.analytics.cookieless;

          addIntegration({
            name: '@bogoware/starlight-theme/analytics',
            hooks: {
              'astro:config:setup'({ injectScript }) {
                injectScript('head-inline', `
                  (function(){
                    ${respectDnt ? "if(navigator.doNotTrack==='1')return;" : ''}
                    var s=document.createElement('script');
                    s.src='https://www.googletagmanager.com/gtag/js?id=${id}';
                    s.async=true;document.head.appendChild(s);
                    window.dataLayer=window.dataLayer||[];
                    function gtag(){dataLayer.push(arguments)}
                    gtag('js',new Date());
                    gtag('config','${id}'${cookieless ? ",{client_storage:'none',anonymize_ip:true}" : ''});
                  })();
                `);
              },
            },
          });
        }

        // Sitemap integration
        const hasSitemap = astroConfig.integrations.some(
          (i: { name: string }) => i.name === '@astrojs/sitemap'
        );
        if (!hasSitemap) {
          try {
            const sitemap = await import('@astrojs/sitemap');
            addIntegration(sitemap.default());
            logger.info('Sitemap integration added');
          } catch {
            logger.warn('Install @astrojs/sitemap for automatic sitemap generation');
          }
        }
      },
    },
  };
}
