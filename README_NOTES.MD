# NOTES

* Lists are subject to change.

## TODO
* Add unit tests.
* Refactor; clean code.
* Add new features.
* QA test.

## Known bugs
* When using a KVM, there is a bug where audio may be "glitched". This may be caused by switching PC inputs while playing audio over the KVM (ex: HDMI or optical audio, 3.5mm analog audio, etc.). SOLUTION: do not add Wave inputs/outputs which operate over the KVM. WORK-AROUND: Logout and login Windows user, then start VACARM again.
* Application will crash without warning when attempting to add vector when one exists between Wave-In and Wave-Out pair(s).

## Potential improvements
* How to disable prevention of system sleep (likely a bug/feature of VAC itself, see powercfg /override).
* Full parity of backwards and forwards Windows compatibility, as with Virtual Audio Cable (example: Windows XP, Vista, 7, etc.).
* Add tasks to task scheduler?
* Pause or stop audio repeater if devices in input/output pair are  disabled or removed?
	* Start audio repeater if devices in input/output pair are enabled?
* GUI presentation: Grid or snapping of colored boxes of Wave inputs and outputs.
* Check if Virtual Audio cable is installed.
* Create Console application
	* menus

		(C)reate Repeater
		(E)dit Repeater
		(D)elete Repeater
		(V)iew all Repeaters
		(Q)uit.

	* Create
		* Select one (1) input
		* Select one (1) or more outputs.
	* Edit
		* Change input.
		* Add or remove output(s).
	* Delete
		* Delete input.
	* View
		* View all repeaters.

	* For each repeater, create a command line.
	* Append all command lines to task scheduler, as a BAT file.
	* Have a function to be able to checkmark inputs/outputs.
* Presentation: Combination of Web-graph and Spreadsheet style representation of audio repeater layout.

## Gotchas
* When using a physical KVM (Keyboard-Video-Mouse) switch which includes audio , there is a phenonmenon where audio playback will pause. This is observed when a KVM is between and/or switching PC inputs (ex: PC #1 -> PC #2). Audio will resume normally after a short moment.