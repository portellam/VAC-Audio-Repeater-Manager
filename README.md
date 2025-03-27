<img align="left" width="100" height="100" src="/logo.png"/>
<br>

# Virtual Audio Cable Audio Repeater Manager
##### VAC Audio Repeater Manager (VACARM)
### In Development
Easily multiplex multiple audio sources. Create, manage, and automate
instances of [*Virtual Audio Cable*](#Licensing) *(VAC)* audio repeaters. Compatible
with Microsoft Windows 2000 to Windows 11.

### [Download](#4-download)

## Table of Contents
- [1. Why?](#1-why)
  - [1.1. Use-Case: Software-Defined Multiplexing](#11-use-case-software-defined-multiplexing)
  - [1.2. Real-World Equivalent](#12-real-world-equivalent)
- [2. Features](#2-features)
- [3. Requirements](#3-requirements)
  - [3.1. Example](#31-example)
  - [3.2. Operating System Requirements](#32-operating-system-requirements)
- [4. Download](#4-download)
- [5. Usage](#5-usage)
  - [5.1. Install](#51-install)
  - [5.2. Graphics User Interface (GUI)](#52-graphics-user-interface-gui)
- [6. Contact](#6-contact)
- [7. References](#7-references)
  - [7.1. Multiplexing](#71-multiplexing)
  - [7.2. .NET Framework version history](#72-net-framework-version-history)
- [8. Credits](#8-credits)
- [9. Licenses](#9-licenses)

## 1. Why?
- Allows for [multiplexing](#71-multiplexing) of audio streams, of which is
not natively supported in Microsoft Windows.
  * If a user wishes to listen to music, a video game, and more, the *same*
audio output must be selected.
  * Windows programs
(example: media players, video games, Internet browsers, etc.) may only choose
one (1) audio output (and input).
- For example, effectively treat a
[video game session](#11-use-case-software-defined-multiplexing) like
[music recording](#12-real-world-equivalent).
- **Turnkey** solution, no tedious work of opening or closing instances of VAC
audio repeater.

### 1.1. Use-Case: Software-Defined Multiplexing
Given:
- one (1) Gaming PC:
  * to play video games.
  * to run VAC Audio Repeater Manager.
  * to capture of the audio/video via a broadcast/stream client.
- three (3) audio sources:
  * a local microphone.
  * incoming voice chat audio.
  * game audio.
- Optional: a separate PC to record; one (1) Recording PC:
  * cables and/or adapters to transfer audio (and video) from the Gaming PC to the
Recording PC.
  * to capture of the audio/video via a broadcast/stream client.

Allows:
1. Specify to only listen to two of the three audio sources
(incoming voice and game audio) on the Gaming PC.
2. Record each source as separate audio streams on a (Gaming or separate) PC,
for future audio/video editing.
3. Optional: capture the audio/video via a broadcast/stream client.

### 1.2. Real-World Equivalent
`(Come on, Dave, give me a break)` - *Unchained*, Van Halen

Given:
- multiple incoming audio sources (instruments and microphones).
- a time-keeping audio source (a Metronome).
- the incoming/outgoing communication between audio engineer and band.

Allows:
- Listen to all sources together, but record separately. 
  * Record all channels from music band separately, for optimal future
remastering.
  * Allow for future editor(s) to mitigate unnecessary audio sources
(time-keeping audio and audio engineer communication) within master recording.

## 2. Features
- Load/Save audio stream setup to/from file.
  - Configuration files
  - `.bat` script files
- Manage audio stream setup of current or other Windows machine(s).
- Start/stop audio repeaters from within the application.
- Easily automate audio stream setup given user-input and/or arguments.

## 3. Requirements
- [*VAC Audio Repeater*](https://vac.muzychenko.net/en/repeater.htm)
- [*VAC Control Panel*](https://vac.muzychenko.net/en/download.htm)
<sup>[**1**](#31-example)</sup>

### 3.1. Example
A minimum of one (1) *Virtual Audio Cable* (Line-In and Line-Out pair) to
faciliate [multiplexing](#71-multiplexing).

### 3.2. Operating System Requirements
| Minimum OS version                 | CPU architecture | .NET version      |
| :---                               | :---:            | :---:             |
| Windows 10 ver. 1809, Server 2019  | 32-bit or 64-bit | Core 8.0  	      |
| Windows 7 SP1, Server 2008 R2      | 32-bit or 64-bit | Framework 4.8     |
| Windows Vista SP2, Server 2008 SP2 | 32-bit or 64-bit | Framework 4.6     |
| Windows XP SP3, Server 2003 SP2    | 32-bit           | Framework 4.0     |
| Windows 2000 SP4                   | 32-bit           | Framework 2.0 SP1 |

Reference: [Wikipedia](#72-net-framework-version-history)

## 4. Download
**In Development**
- ~Download the Latest Release:&ensp;[Codeberg][41], [GitHub][42]~

- Download the `.zip` file:
    1. Viewing from the top of the repository's (current) webpage, click the
        drop-down icon:
        - `···` on Codeberg.
        - `<> Code ` on GitHub.
    2. Click `Download ZIP` and save.
    3. Open the `.zip` file, then extract its contents.

- Clone the repository:
    1. Open a Command Line Interface (CLI).
        - Open a console emulator (for Debian systems: Konsole).
        - Open an existing console: press `CTRL` + `ALT` + `F2`, `F3`, `F4`, `F5`,  or
        `F6`.
            - **To return to the desktop,** press `CTRL` + `ALT` + `F7`.
            - `F1` is reserved for debug output of the Linux kernel.
            - `F7` is reserved for video output of the desktop environment.
            - `F8` and above are unused.
    2. Change your directory to your home folder or anywhere safe:
        - `cd ~`
    3. Clone the repository:
        - `git clone https://www.codeberg.org/portellam/vac-audio-repeater-manager`
        - `git clone https://www.github.com/portellam/vac-audio-repeater-manager`

[41]: https://codeberg.org/portellam/vac-audio-repeater-manager/releases/latest
[42]: https://github.com/portellam/vac-audio-repeater-manager/releases/latest

## 5. Usage
### 5.1. Install
**In Development**

### 5.2. Graphics User Interface (GUI)
**In Development**

## 6. Contact
Did you encounter a bug? Do you need help? Please visit the
**Issues page** ([Codeberg][61], [GitHub][62]).

[61]: https://codeberg.org/portellam/vac-audio-repeater-manager/issues
[62]: https://github.com/portellam/vac-audio-repeater-manager/issues

## 7. References
### 7.1. Multiplexing
In the scope of this project: one *virtual* Line-In to one or more *physical*
Line-Out(s).

*In telecommunications and computer networking, multiplexing*
*(sometimes contracted to muxing) is a method by which multiple analog or*
*digital signals are combined into one signal over a shared medium.*
[Wikipedia][711]

### 7.2. .NET Framework version history
[Wikipedia][721].

[711]: https://en.wikipedia.org/wiki/Multiplexing
[721]: https://en.wikipedia.org/wiki/.NET_Framework_version_history

## 8. Credits
[Eugene Muzychenko][91] for creating *Virtual Audio Cable*.

[91]: https://eugene.muzychenko.net/EMuzychenko_Resume_Eng.htm

## 9. Licenses
*Virtual Audio Cable* Copyright © 1998-2025 Eugene V. Muzychenko.

*VAC Audio Repeater Manager*, VACARM GPL-3.0, Copyleft 2025 Alexander Portell.