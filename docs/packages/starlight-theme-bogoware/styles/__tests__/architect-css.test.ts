import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const architectCss = readFileSync(resolve(__dirname, '..', 'architect.css'), 'utf-8');

describe('architect.css', () => {
  it('should define Space Grotesk font-face for headings', () => {
    expect(architectCss).toContain("font-family: 'Space Grotesk'");
  });

  it('should define DM Sans font-face for body text', () => {
    expect(architectCss).toContain("font-family: 'DM Sans'");
  });

  it('should define Space Mono font-face for code', () => {
    expect(architectCss).toContain("font-family: 'Space Mono'");
  });

  it('should use font-display: swap for all font-faces', () => {
    expect(architectCss).toContain('font-display: swap');
  });

  it('should reference woff2 font files', () => {
    expect(architectCss).toContain('.woff2');
  });

  it('should define heading font token', () => {
    expect(architectCss).toContain("--bw-font-heading: 'Space Grotesk'");
  });

  it('should define body font token', () => {
    expect(architectCss).toContain("--bw-font-body: 'DM Sans'");
  });

  it('should define code font token', () => {
    expect(architectCss).toContain("--bw-font-code: 'Space Mono'");
  });

  it('should define display type scale at 2.25rem', () => {
    expect(architectCss).toContain('--bw-text-display: 2.25rem');
  });

  it('should map to Starlight font variables', () => {
    expect(architectCss).toContain('--sl-font: var(--bw-font-body)');
    expect(architectCss).toContain('--sl-font-mono: var(--bw-font-code)');
  });
});
