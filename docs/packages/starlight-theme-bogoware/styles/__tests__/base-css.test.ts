import { describe, it, expect } from 'vitest';
import { readFileSync } from 'node:fs';
import { resolve, dirname } from 'node:path';
import { fileURLToPath } from 'node:url';

const __dirname = dirname(fileURLToPath(import.meta.url));
const baseCss = readFileSync(resolve(__dirname, '..', 'base.css'), 'utf-8');

describe('base.css', () => {
  it('should define primary palette tokens', () => {
    expect(baseCss).toContain('--bw-indigo: #6366f1');
    expect(baseCss).toContain('--bw-violet: #8b5cf6');
    expect(baseCss).toContain('--bw-cyan: #99f0ff');
    expect(baseCss).toContain('--bw-periwinkle: #b0a0ff');
  });

  it('should define dark mode tokens', () => {
    expect(baseCss).toContain('--bw-bg: #0f172a');
    expect(baseCss).toContain('--bw-surface: #1e293b');
    expect(baseCss).toContain('--bw-border: #334155');
    expect(baseCss).toContain('--bw-text: #e2e8f0');
    expect(baseCss).toContain('--bw-text-muted: #94a3b8');
  });

  it('should define light mode tokens', () => {
    expect(baseCss).toContain('--bw-bg: #f8fafc');
    expect(baseCss).toContain('--bw-text: #1e293b');
    expect(baseCss).toContain('--bw-indigo-deep: #4f46e5');
  });

  it('should define spacing scale', () => {
    expect(baseCss).toContain('--bw-space-1: 4px');
    expect(baseCss).toContain('--bw-space-2: 8px');
    expect(baseCss).toContain('--bw-space-4: 16px');
    expect(baseCss).toContain('--bw-space-8: 32px');
    expect(baseCss).toContain('--bw-space-16: 64px');
  });

  it('should define code syntax accent tokens', () => {
    expect(baseCss).toContain('--bw-code-cyan: #67e8f9');
    expect(baseCss).toContain('--bw-code-mint: #86efac');
    expect(baseCss).toContain('--bw-code-indigo: #a5b4fc');
    expect(baseCss).toContain('--bw-code-amber: #fcd34d');
    expect(baseCss).toContain('--bw-code-rose: #fda4af');
  });

  it('should define layout dimensions', () => {
    expect(baseCss).toContain('--bw-content-max-width: 720px');
    expect(baseCss).toContain('--bw-content-wide-max-width: 1200px');
    expect(baseCss).toContain('--bw-sidebar-width: 240px');
    expect(baseCss).toContain('--bw-navbar-height: 64px');
  });
});
