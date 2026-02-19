---
name: block-no-verify
enabled: true
event: bash
pattern: git\s+(commit|push)\s+.*--no-verify|git\s+(commit|push)\s+.*\s-n\b
action: block
---

**Git Hook Bypass Attempt Blocked**

The `--no-verify` or `-n` flag bypasses pre-commit hooks, which may skip important validations.

**Why this is blocked:**
- Pre-commit hooks enforce code quality standards
- Bypassing hooks can introduce unvalidated commits
- This flag is often misused to skip legitimate checks

**Required Action:**
Remove the `--no-verify` or `-n` flag and let the hooks run properly.
If hooks are failing, fix the underlying issues instead of bypassing them.
