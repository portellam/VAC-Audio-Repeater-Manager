# NOTES

* Lists are subject to change.

## TODO
* Add unit tests.
* Refactor; clean code.
* Add new features.
* QA test.

## Known bugs
* When using a KVM, there is a bug where audio may be "glitched". This may be caused by switching PC inputs while playing audio over the KVM (ex: HDMI or optical audio, 3.5mm analog audio, etc.). WORK-AROUND: do not add Wave inputs/outputs which operate over the KVM. 
* Application will crash without warning when attempting to add vector when one exists between Wave-In and Wave-Out pair(s).

## Potential improvements
* GUI presentation: Grid or snapping of colored boxes of Wave inputs and outputs.
* Console application.
* Presentation: Combination of Web-graph and Spreadsheet style representation of audio repeater layout.

## Gotchas
* When using a physical KVM (Keyboard-Video-Mouse) switch which includes audio , there is a phenonmenon where audio playback will pause. This is observed when a KVM is between and/or switching PC inputs (ex: PC #1 -> PC #2). Audio will resume normall after a short moment.