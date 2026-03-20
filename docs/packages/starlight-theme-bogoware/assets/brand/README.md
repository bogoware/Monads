# Bogoware Brand Assets

## Logo Variants

| File | Use case |
|------|----------|
| `svg/bogoware-logo-dark.svg` | Full logo on **dark** backgrounds |
| `svg/bogoware-logo-light.svg` | Full logo on **light** backgrounds |
| `svg/bogoware-logo-mark.svg` | Icon-only mark for inline contexts and small spaces |
| `svg/bogoware-logo-favicon.svg` | Browser tab favicon (rounded rect background) |
| `svg/bogoware-logo-monochrome.svg` | Single-color printing, fax, embossing |

## PNG Exports

All PNGs are generated from `logo-favicon.svg` (the rounded-rect mark).

| File | Size | Purpose |
|------|------|---------|
| `png/bogoware-logo-16.png` | 16x16 | ICO component |
| `png/bogoware-logo-32.png` | 32x32 | ICO component, small icon |
| `png/bogoware-logo-48.png` | 48x48 | Desktop icon |
| `png/bogoware-logo-128.png` | 128x128 | NuGet package icon |
| `png/bogoware-logo-180.png` | 180x180 | Apple touch icon |
| `png/bogoware-logo-192.png` | 192x192 | Android Chrome icon |
| `png/bogoware-logo-256.png` | 256x256 | General purpose |
| `png/bogoware-logo-512.png` | 512x512 | PWA splash, high-DPI |

## Favicon Directory

| File | Purpose |
|------|---------|
| `favicon/favicon.svg` | SVG favicon for modern browsers |
| `favicon/favicon.ico` | Multi-size ICO for legacy browsers (16+32px) |
| `favicon/apple-touch-icon.png` | 180x180 Apple touch icon |
| `favicon/site.webmanifest` | PWA manifest |

## Social / Platform

| File | Size | Purpose |
|------|------|---------|
| `social/og-default.svg` | 1200x630 | Open Graph / Twitter card |
| `social/github-avatar.png` | 460x460 | GitHub organization avatar |
| `social/nuget-icon.png` | 128x128 | NuGet package icon |

## Regenerating PNGs

PNGs are generated from source SVGs using `rsvg-convert`:

```bash
rsvg-convert -w SIZE -h SIZE logo-favicon.svg -o bogoware-logo-SIZE.png
```

## Notes

- `favicon.ico` was generated using ImageMagick from the 16px and 32px PNGs.
