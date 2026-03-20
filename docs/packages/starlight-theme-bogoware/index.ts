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
      setup({ updateConfig, config: starlightConfig, logger }) {
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

        // TODO: Register component overrides once override .astro files exist
        // const componentOverrides: Record<string, string> = {
        //   Header: '@bogoware/starlight-theme/overrides/Header.astro',
        //   Hero: '@bogoware/starlight-theme/overrides/Hero.astro',
        //   Sidebar: '@bogoware/starlight-theme/overrides/Sidebar.astro',
        //   Footer: '@bogoware/starlight-theme/overrides/Footer.astro',
        //   Head: '@bogoware/starlight-theme/overrides/Head.astro',
        // };

        updateConfig({
          customCss: [
            ...(starlightConfig.customCss ?? []),
            ...cssImports,
          ],
          // TODO: Uncomment when override components are created
          // components: {
          //   ...starlightConfig.components,
          //   ...componentOverrides,
          // },
        });
      },
    },
  };
}
