import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';
import bogowareTheme from '@bogoware/starlight-theme';

export default defineConfig({
  integrations: [
    starlight({
      title: 'Theme Playground',
      plugins: [bogowareTheme()],
      sidebar: [{ label: 'Home', link: '/' }],
    }),
  ],
});
