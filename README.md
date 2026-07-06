WatchVegas: Stability Monitor

WatchVegas is a dedicated stability monitoring application designed to keep your environment running smoothly. 
It provides real-time oversight of system performance with a focus on unobtrusive operation and persistent configuration.
Key Features

    Always on Top: Keeps the monitoring window visible at all times.

    Open on Boot-up: Automatically launches with Windows.

    System Tray Integration: Minimizes to the tray for a clean taskbar.

    Window State Persistence: Remembers your preferred window position and size.

    Diagnostic Screen Capture: Provides visual context for troubleshooting stability issues.

    Module Monitoring: Tracks active .dll dependencies in real-time.

Screen Capture & Diagnostics

WatchVegas includes an advanced diagnostic feature designed for post-crash analysis. 
This is most beneficial when you are troubleshooting intermittent application crashes or system freezes.

    How it works: When active, the application captures your screen at regular intervals to provide a visual history of what was occurring immediately before a crash or freeze.

    The "Flash": You may observe a brief "flash" on your display during a capture event. This is normal behavior and indicates the system is successfully capturing the current state of your workspace.

    Circular Buffer: To maintain system efficiency and conserve disk space, the application maintains a circular buffer of the 10 most recent images. Once the 10th image is captured, the oldest image is automatically overwritten.

DLL Module Monitoring

To assist in identifying the root causes of instability, WatchVegas monitors the loading and unloading of .dll files within the target environment.

    Real-time Logging: Any significant changes in library usage are captured and logged directly to the RichTextBox (RTB) interface in the main window.

    Diagnostic Value: If you experience a freeze or crash, checking the RTB logs can help determine if a specific module caused the instability, allowing you to isolate problematic dependencies.

How It Works

    Startup: Upon execution, WatchVegas initializes the monitoring service and checks your saved configuration parameters.

    Monitoring: The app continuously tracks the defined stability metrics and module usage. Pin the window to the top of your display for continuous observation.

    Minimize to Tray: Clicking the minimize button will transition the app to the system tray. Right-click the system tray icon to restore the window or exit the application.

    Auto-Launch: Manage your auto-start settings via the configuration form to enable or disable auto-launch on boot.

Requirements

    OS: Windows 10 or later.

    Framework: .NET 8.0 (Desktop Runtime).

Support & Feedback

If you encounter any issues, please check the configuration parameters in the set-up form to ensure your thresholds are set correctly. 

For further assistance or bug reports, please contact chrisj.minchin@gmail.com

© 2026 Christopher J Minchin. 

All rights reserved.
