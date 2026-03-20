# Bogoware.Monads — Claude Code Guide

## Build / Test

```bash
dotnet build -c Release
dotnet test
```

Multi-targets `netstandard2.1`, `net8.0`, `net9.0`, `net10.0`. SDK pinned in `global.json`.

## Documentation Site (Astro/Starlight)

```bash
cd website && pnpm install && pnpm build   # Build docs
cd website && pnpm dev                      # Dev server on :4321
```

- Site: `website/` — Astro/Starlight with architect typography mode
- Theme: `@bogoware/starlight-theme` from NPM (source in its own repo)
- API docs: auto-generated via `scripts/generate-api-docs.sh` (xmldoc2md → Starlight frontmatter)
- Changelog: synced from root via `scripts/sync-readme.js`
- Starlight plugin hook is `setup` (not `config:setup`) in v0.30.x

## Branch Model

- **`rel/prod`** is the production branch
- Feature branches: `feature/*`, `fix/*`
- Docs deploy: GitHub Actions on push to `rel/prod` or release published

## Design Spec & Plans

- Spec: `design/specs/2026-03-20-astro-starlight-migration-design.md`
- Plan: `design/plans/2026-03-20-starlight-theme-package.md`
- Skill: `design/skills/bogoware-docs.md` (reusable for all Bogoware doc sites)
