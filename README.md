# macos-thick-header

Here is a project to reproduce the bug [https://github.com/AvaloniaUI/Avalonia/issues/15696](https://github.com/AvaloniaUI/Avalonia/issues/15696)

If you click outside the window header, mouse capture works correctly.
However, if you click within the header area, the capture is lost when the cursor leaves the header region.