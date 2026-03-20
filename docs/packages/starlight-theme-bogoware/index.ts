import type { StarlightPlugin } from '@astrojs/starlight/types';

export default function bogowareTheme(userConfig?: Record<string, unknown>): StarlightPlugin {
  return {
    name: '@bogoware/starlight-theme',
    hooks: {
      setup({ config, updateConfig, logger }) {
        logger.info('Bogoware theme loaded');
      },
    },
  };
}
