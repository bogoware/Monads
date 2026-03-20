import { describe, it, expect } from 'vitest';
import { existsSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
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

describe('self-hosted WOFF2 fonts', () => {
  for (const font of requiredFonts) {
    it(`${font} should exist`, () => {
      expect(existsSync(resolve(fontsDir, font))).toBe(true);
    });
  }
});
