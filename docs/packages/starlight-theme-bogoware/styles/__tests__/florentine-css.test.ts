import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const florentineCss = readFileSync(resolve(__dirname, '..', 'florentine.css'), 'utf-8');

describe('florentine.css', () => {
  it('should define Cormorant Garamond font-face for headings', () => {
    expect(florentineCss).toContain("font-family: 'Cormorant Garamond'");
  });

  it('should define Crimson Pro font-face for body text', () => {
    expect(florentineCss).toContain("font-family: 'Crimson Pro'");
  });

  it('should define Fira Code font-face for code', () => {
    expect(florentineCss).toContain("font-family: 'Fira Code'");
  });

  it('should include italic variant for Crimson Pro', () => {
    expect(florentineCss).toContain('font-style: italic');
  });

  it('should use font-display: swap for all font-faces', () => {
    expect(florentineCss).toContain('font-display: swap');
  });

  it('should reference woff2 font files', () => {
    expect(florentineCss).toContain('.woff2');
  });

  it('should define heading font token', () => {
    expect(florentineCss).toContain("--bw-font-heading: 'Cormorant Garamond'");
  });

  it('should define body font token', () => {
    expect(florentineCss).toContain("--bw-font-body: 'Crimson Pro'");
  });

  it('should define code font token', () => {
    expect(florentineCss).toContain("--bw-font-code: 'Fira Code'");
  });

  it('should define display type scale at 2.5rem', () => {
    expect(florentineCss).toContain('--bw-text-display: 2.5rem');
  });

  it('should map to Starlight font variables', () => {
    expect(florentineCss).toContain('--sl-font: var(--bw-font-body)');
    expect(florentineCss).toContain('--sl-font-mono: var(--bw-font-code)');
  });
});
