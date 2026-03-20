import { describe, it, expect } from 'vitest';
import { readFileSync, existsSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const assetsDir = resolve(__dirname, '..');

describe('logo SVG assets', () => {
  const variants = ['logo-dark.svg', 'logo-light.svg', 'logo-mark.svg', 'logo-favicon.svg', 'logo-monochrome.svg'];

  for (const file of variants) {
    it(`${file} should exist`, () => {
      expect(existsSync(resolve(assetsDir, file))).toBe(true);
    });

    it(`${file} should be valid SVG`, () => {
      const svg = readFileSync(resolve(assetsDir, file), 'utf-8');
      expect(svg).toContain('xmlns="http://www.w3.org/2000/svg"');
      expect(svg).toContain('viewBox="0 0 200 200"');
    });
  }

  it('logo-dark.svg should have gradient paths', () => {
    const svg = readFileSync(resolve(assetsDir, 'logo-dark.svg'), 'utf-8');
    expect(svg).toContain('M 28,28 C 72,28 55,88 100,100');
    expect(svg).toContain('#6366f1');
    expect(svg).toContain('#99f0ff');
  });

  it('logo-monochrome.svg should have dashed path', () => {
    const svg = readFileSync(resolve(assetsDir, 'logo-monochrome.svg'), 'utf-8');
    expect(svg).toContain('stroke-dasharray');
  });

  it('logo-favicon.svg should have thicker strokes', () => {
    const svg = readFileSync(resolve(assetsDir, 'logo-favicon.svg'), 'utf-8');
    expect(svg).toMatch(/stroke-width="2[0-9]"/);
  });
});
