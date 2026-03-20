import { describe, it, expect } from 'vitest';
import { bogowareThemeSchema, type BogowareThemeConfig } from './schema.js';

describe('bogowareThemeSchema', () => {
  it('should accept a valid full config', () => {
    const config: BogowareThemeConfig = {
      mode: 'architect',
      logoVariant: 'simplified',
      fieldLines: true,
      seo: {
        siteName: 'Bogoware.Monads',
        defaultDescription: 'Functional programming patterns for C#',
        locale: 'en_US',
        ogImage: { enabled: true, strategy: 'dynamic' },
        twitterCard: 'summary_large_image',
        twitterSite: '@bogoware',
        structuredData: { type: 'SoftwareSourceCode', author: 'Bogoware' },
      },
      analytics: {
        googleAnalyticsId: 'G-XXXXXXXXXX',
        respectDnt: true,
        cookieless: false,
      },
    };
    const result = bogowareThemeSchema.parse(config);
    expect(result.mode).toBe('architect');
    expect(result.seo.ogImage.strategy).toBe('dynamic');
  });

  it('should apply defaults for minimal config', () => {
    const result = bogowareThemeSchema.parse({});
    expect(result.mode).toBe('architect');
    expect(result.logoVariant).toBe('simplified');
    expect(result.fieldLines).toBe(true);
    expect(result.seo.ogImage.enabled).toBe(true);
    expect(result.analytics.respectDnt).toBe(true);
  });

  it('should reject invalid mode', () => {
    expect(() => bogowareThemeSchema.parse({ mode: 'gothic' })).toThrow();
  });

  it('should reject invalid logoVariant', () => {
    expect(() => bogowareThemeSchema.parse({ logoVariant: 'neon' })).toThrow();
  });
});
