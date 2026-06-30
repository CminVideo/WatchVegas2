WatchVegas2 – User Guide & Feature Overview

Welcome to WatchVegas2, a comprehensive monitoring tool designed to track hardware performance and plugin activity for Vegas Pro. 
This guide introduces all major features, explains how they work, and helps you make the most of the application.
1. Hardware Monitoring

WatchVegas2 continuously monitors your system’s hardware performance, including CPU, GPU, VRAM, RAM, disk usage, and CPU temperature.

    Real-Time Graphs: Visual charts display live metrics for CPU, RAM, GPU, and Disk loads, updating regularly.
    Adaptive Temperature and GPU Load Warnings: Uses a configurable baseline and margin system to detect abnormal increases and alert you.
    System Load Summary: Provides a color-coded overview of system usage status (NORMAL, MODERATE, HEAVY, CRITICAL).
    Alerts: When thresholds are exceeded (for example, CPU temperature or GPU usage), log alerts appear with optional automatic screenshot capture to document the state.

2. Vegas Pro Module Scanning

WatchVegas2 scans the loaded modules of the Vegas Pro process (vegas2026) to provide insight into which plugins and system components are active:

    Module History Log: A detailed, timestamped log of all loaded modules is maintained for diagnostics and review.
    Plugin Highlighting: Important plugins such as BorisFX, NewBlue, Mocha, and others are highlighted distinctly for easy identification.
    Live Updates: The module list refreshes periodically to detect newly loaded or unloaded modules.
    Crash Snapshots: Captures a detailed snapshot of the loaded modules and system state when Vegas closes unexpectedly.

3. Watched Plugins Feature

Customize and maintain a list of plugin names or keywords you want to watch specifically:

    Monitors plugin load events against the watched list using substring matches.
    Logs special [WATCHED] notifications for matched plugins.
    Optionally triggers automatic screenshot captures upon detection.
    Helps focus monitoring and alerts on critical or suspect plugins.

Example:

If "Boris" is on your watched list, detection of BorisFXPlugin.dll will generate a log entry like:

[2024-06-09 14:25:30] [WATCHED]: BorisFXPlugin.dll (matched 'Boris')

4. Interference Application Monitoring

Detects and alerts if potentially conflicting background applications are running alongside Vegas Pro, such as Discord, OBS, Steam, or others:

    Monitors a preset list of interference apps.
    Provides alerts with warnings when detected.
    Displays current conflict status only when changes occur for cleaner logs.
    User can toggle monitoring on/off via UI.

5. Logging & Reporting

Robust logging captures system and application state for diagnostics:

    Logs are saved daily automatically, with customizable file locations.
    Detailed reports include hardware stats, loaded plugins, and application status.
    Supports saving logs manually through the UI.
    Generates diagnostic snapshots for printing or PDF export.

6. Automatic & Manual Screenshots

Captures screenshots silently when key events occur to provide visual evidence:

    Triggered automatically on warnings or watched plugin detection.
    Can be manually triggered by the user at any time.
    Screenshots saved in a designated folder with timestamps.
    Supports silent background capture to avoid interrupting workflow.

7. User Interface & Customization

    Simple UI with color-coded logs and charts.
    Adjustable refresh rates and visibility options.
    Plugin watching, logging, and auto-screenshot features configurable via settings.
    Tooltip legends explain color codes.
    Built-in search and filter capabilities for logs and module lists.

8. Crash Analysis & Heartbeat Logging

    Automatically captures crash snapshots on unexpected Vegas closure.
    Heartbeat logging creates periodic system status records.
    Enables tracking of hardware trends and application behavior over time.

Getting Started

    Launch WatchVegas2; ensure it’s running only once.
    Configure Settings through the provided settings window:
        Set temperature and GPU warning thresholds.
        Enable or disable watched plugins and specify the keywords.
        Toggle automatic screenshot capture.
        Enable interference application monitoring.

    Start Vegas Pro and observe in WatchVegas2 as it detects Vegas, scans modules, updates system stats, and monitors critical plugins.
    Use the UI controls to refresh module scans manually, view logs, save reports, or trigger screenshots.

Tips & Best Practices

    Regularly update your watched plugins list to focus on critical components.
    Use adaptive warnings to better tune thresholds according to your system baseline.
    Review logs and screenshots to diagnose performance issues or plugin conflicts.
    Disable interference monitoring if not needed to reduce noise in logs.

Thank you for using WatchVegas2! For support or issue reporting, please refer to the accompanying documentation or contact the development team.


Requirements

    OS: Windows 10 or later.

    Framework: .NET 8.0 (Desktop Runtime).

Support & Feedback

If you encounter any issues, please check the configuration parameters in the set-up form to ensure your thresholds are set correctly. 

For further assistance or bug reports, please contact chrisj.minchin@gmail.com

© 2026 Christopher J Minchin. 

All rights reserved.