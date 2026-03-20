import { describe, it, expect } from 'vitest';
import bogowareTheme from './index.js';

describe('bogowareTheme plugin', () => {
  it('should return a valid Starlight plugin object', () => {
    const plugin = bogowareTheme({ mode: 'architect' });
    expect(plugin.name).toBe('@bogoware/starlight-theme');
    expect(plugin.hooks).toBeDefined();
    expect(typeof plugin.hooks.setup).toBe('function');
  });

  it('should accept empty config (all defaults)', () => {
    const plugin = bogowareTheme();
    expect(plugin.name).toBe('@bogoware/starlight-theme');
  });

  it('should accept florentine mode', () => {
    const plugin = bogowareTheme({ mode: 'florentine' });
    expect(plugin.name).toBe('@bogoware/starlight-theme');
  });

  it('should throw on invalid config', () => {
    // @ts-expect-error — intentionally passing invalid config
    expect(() => bogowareTheme({ mode: 'gothic' })).toThrow();
  });
});
