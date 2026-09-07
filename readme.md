ShadowFlame
Speech helpers and Dates on Windows
Form Library and Message Encapsulation
===========

ShadowFlame is a small Windows Forms utility project. This README documents the public APIs available in the solution and includes instructions for running the unit tests.

Public APIs
-----------

The following classes and public members are provided by the ShadowFlame project (summary list):

- ShadowFlame.Dates
  - static string Short(DateTime? date = null)
  - static string Long(DateTime? date = null)
  - static string Time(DateTime? date = null)
  - static string NextDayOfWeek(DayOfWeek dayOfWeek, DateTime? from = null)

- ShadowFlame.GlobalFontService
  - static GlobalFontService Instance { get; }
  - Font CurrentFont { get; }
  - void ChooseFont()
  - event EventHandler<Font> FontChanged

- ShadowFlame.DisplayMessage
  - static void Show(string message, string caption, MessageBoxIcon icon)
  - static void ShowError(string message, string Caption = "Error")
  - static void ShowInfo(string message, string Caption = "Information")
  - static void ShowWarning(string message, string Caption = "Warning")
  - static bool GetConfirmation(string text, string caption = "Confirm")

- ShadowFlame.ConsoleTTS
  - ConsoleTTS()  (constructor)
  - bool SpeechEnabled { get; }
  - void Speak(string message)
  - Task SpeakAsync(string message, CancellationToken cancellationToken = default)
  - void SpeakAndDisplay(string message = "", bool SameLine = false)
  - void Stop()
  - Task InitializeAsync()
  - void Dispose()

- ShadowFlame.WindowsTTS
  - string[] GetAvailableVoices()
  - Task<string[]> GetAvailableVoicesAsync()
  - void SelectVoice(string name)
  - Task SelectVoiceAsync(string name)
  - void SetRate(int rate)
  - Task SetRateAsync(int rate)
  - void SetVolume(int volume)
  - Task SetVolumeAsync(int volume)
  - void Speak(string text)
  - Task SpeakAsync(string text)
  - void Stop()
  - void Dispose()

- ShadowFlame.TTSSetup (WinForms)
  - (Form) dialog used to configure voice, rate and volume and save settings to AppData

Notes
-----
- Many UI-related APIs (forms, dialogs) are intentionally simple and perform MessageBox interactions or show dialogs. Tests should avoid constructing UI dialogs on non-interactive CI agents.
- The TTS APIs use System.Speech.Synthesis; behaviour depends on installed voices and Windows audio subsystem.

Running tests
-------------

A test project using xUnit is included at ShadowFlame.Tests. To run the tests locally using the dotnet CLI:

  dotnet test ShadowFlame.Tests/ShadowFlame.Tests.csproj

If you use Visual Studio, open the solution and run tests from Test Explorer.

License & contribution
----------------------

This repository contains user-owned code. Feel free to run the tests and open a PR with improvements.
