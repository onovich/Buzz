# Buzz

[简体中文](README.zh-CN.md)

Compose controller rumble without lifecycle clutter.

![Buzz cover](docs/cover.png)

## What it includes

- Low + high motors.
- Task lifecycle.
- Unity-ready.

## Getting started

In Unity, open **Window → Package Manager**, choose **Add package from git URL**, and enter:

```text
https://github.com/onovich/Buzz.git?path=/Assets/com.tenon.buzz#main
```

The package metadata declares Unity `2019.4` or later.

## Repository map

- `Assets/` — Unity scripts, scenes, packages, and authored assets.
- `Packages/` — Unity package dependencies.
- `.editorconfig` — Repository component.
- `.vscode` — Repository component.
- `LICENSE/` — Repository component.

## Status

The current repository describes Buzz as stable and available. Frequency blending is deliberately not implemented: a later task interrupts an earlier task on the same motor.

## License

This repository is licensed under [MIT](LICENSE).
