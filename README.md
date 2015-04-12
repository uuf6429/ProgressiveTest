# ProgressiveTest

*Progressive Unit Testing for Visual Studio*

## Abstract

This source code provides a way for Unit Tests to show an indication of the state they're in by popping up a small tool window.

## Requirements

This code is based on WinForms, hence the two assemblies below have to be referenced in your Unit Test Project:

- `System.Windows.Forms`
- `System.Drawing`

## Usage

Add the source code (in `src`) to your project and extend your existing tests from `CS.TestUtils.ProgressiveTest`.
This will provide access to the `Progress` property object, which has this API:

- `void Open([string title])` - Opens the progress window with the specified title (optional).
- `void Close()` - Closes the progress window.
- `string Title` - Sets the window title.
- `decimal Progress` - Set the progress bar value.
- `string Message` - Sets the label text.

## Screenshot

![Screenshot](http://i.imgur.com/inwg3NT.png)
