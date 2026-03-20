import { describe, it, expect, beforeAll } from 'vitest';
import { execSync } from 'node:child_process';
import { readFileSync, existsSync, readdirSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const playgroundDir = resolve(__dirname, '../../sites/monads');
const distDir = resolve(playgroundDir, 'dist');

/** Read all CSS files from the dist/_astro directory and concatenate them. */
function readDistCss(): string {
  const astroDir = resolve(distDir, '_astro');
  if (!existsSync(astroDir)) return '';
  const cssFiles = readdirSync(astroDir).filter((f) => f.endsWith('.css'));
  return cssFiles
    .map((f) => readFileSync(resolve(astroDir, f), 'utf-8'))
    .join('\n');
}

describe('integration: playground site build', () => {
  beforeAll(() => {
    // Build the playground site — hardcoded command, no user input
    execSync('pnpm build', { cwd: playgroundDir, stdio: 'pipe', timeout: 60000 });
  }, 120000);

  it('should produce dist directory', () => {
    expect(existsSync(distDir)).toBe(true);
  });

  it('should produce index.html', () => {
    expect(existsSync(resolve(distDir, 'index.html'))).toBe(true);
  });

  it('should contain Bogoware brand tokens in CSS', () => {
    const css = readDistCss();
    // The base.css tokens should be bundled into the output CSS
    expect(css).toContain('bw-indigo');
  });

  it('should contain font preconnect hints', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('fonts.googleapis.com');
    expect(html).toContain('fonts.gstatic.com');
  });

  it('should contain favicon link', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('favicon.svg');
  });

  it('should contain the branded footer', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).toContain('Bogoware');
  });

  it('should NOT inject GA script when no ID configured', () => {
    const html = readFileSync(resolve(distDir, 'index.html'), 'utf-8');
    expect(html).not.toContain('googletagmanager.com');
  });
});

describe('integration: architect mode typography', () => {
  it('playground uses architect mode by default', () => {
    // The schema defaults to 'architect' mode when no mode is specified.
    // Verify the built CSS contains architect-specific font references.
    const css = readDistCss();
    expect(css).toContain('Space Grotesk');
  });
});
