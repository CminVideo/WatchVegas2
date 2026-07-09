Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Timers
Imports System.Windows.Forms.DataVisualization.Charting
Imports LibreHardwareMonitor.Hardware
Imports System.Runtime.InteropServices
Imports System.Drawing.Printing
Imports System.Diagnostics.Eventing.Reader


''Imports OpenHardwareMonitor.Hardware
''XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

Public Class frmMain2
    Public CurrentCpuValue As String = "0"
    Public CurrentGpuValue As String = "0"
    Public CurrentVramValue As String = "0"
    Public CurrentDiskValue As String = "0"

    ' currentCpuValue = ChartText ' Replace with your CPU variable/label
    ' CurrentVramValue=ChartVRAM ' Replace with your VRAM variable/label

    ' --- UI & Timers ---
    Private HasAlerted As Boolean = False
    Private WithEvents dataGatherTimer As New System.Windows.Forms.Timer()
    Private WithEvents computer As New Computer()
    Private myCustomTip As New ToolTip()
    Private hardwareLock As New Object()

    ' --- Hardware/Performance Tracking ---
    Private cpuCounter As PerformanceCounter
    Private ramCounter As PerformanceCounter
    Private diskCounter As PerformanceCounter
    Private cpuSensor As ISensor
    Private gpuSensor As ISensor
    Private latestCpuTemp As Single = 0
    Private currentVramUsage As Single = 0
    Private latestCpu As Single = 0, latestRam As Single = 0, latestDisk As Single = 0
    Private latestGpu As Single = 0

    ' --- Monitoring & State Variables ---
    Private IsReady As Boolean = False
    Private isMonitoring As Boolean = True
    Private isMonitoringConflicts As Boolean = True
    Private isScanningModules As Boolean = False
    Private initialScanDone As Boolean = False
    Private loadingMessageShown As Boolean = False

    ' Vegas & Plugin Logic
    Private isVegasRunning As Boolean = False
    Private isBorisDetected As Boolean = False
    Private lastKnownModules As New List(Of String)
    Private fullModuleHistory As New List(Of String)
    Private showOnlyPlugins As Boolean = False ' Defaulted to False so it starts in "All/Verbose" mode

    ' Alerts & Throttling
    Private lastTempAlertTime As DateTime = DateTime.MinValue
    Private lastVegasAlertTime As DateTime = DateTime.MinValue
    Private lastAlertTime As DateTime = DateTime.MinValue
    Private lastUiUpdate As DateTime = DateTime.MinValue
    Private uiUpdateCounter As Integer = 0
    Private moduleCheckCounter As Integer = 0
    Private xCounter As Integer = 0
    Private isThrottlingAlerted As Boolean = False
    Private lastDisplayedTemp As Single = -1

    ' Interference Monitoring
    Private ReadOnly interferenceApps As String() = {"discord", "obs64", "rtss", "steam", "gamebar", "overwolf"}
    Private lastKnownConflicts As New List(Of String)
    Private IsOverheating As Boolean = False
    ' --- Constants & Interop ---
    Private Const WVersion As String = " Version 2.5.2"
    Private Const WM_SETREDRAW As Integer = 11
    Private Const VramWarningThreshold As Double = 90.0 ' percent
    Private Const SystemLoadWeightCpu As Double = 0.3
    Private Const SystemLoadWeightRam As Double = 0.4
    Private Const SystemLoadWeightGpu As Double = 0.3
    Private hasVegasInitialized As Boolean = False
    Private isChecking As Boolean = False
    Private heartbeatCounter As Integer = 0
    Private currentCpuLoad As String = "0%"
    Private currentGpuLoad As String = "0%"
    Private currentVramLoad As String = "0%"
    Private lastCpuLoad As String = "0%"
    Private lastGpuLoad As String = "0%"
    Private lastVramLoad As String = "0%"
    Private gpuCounter As PerformanceCounter
    Private vramCounter As PerformanceCounter
    Private cpuValue As String = "0"
    Private gpuValue As String = "0"
    Private vramValue As String = "0"
    Private diskValue As String = "0"
    Private Shared lastDiskLoad As String = "0%"
    Private WithEvents printDoc As New PrintDocument()
    Public currentLogFilePath As String = ""
    Public printableReportContent As String = ""
    Private heartbeatEnabled As Boolean = True
    Private lastModuleLoaded As String = ""
    Private lastCrashTime As String = ""
    Private lastCrashSummary As String = ""
    Private lastCrashCulpritModule As String = "Unknown"
    Private isVegasHung As Boolean = False
    Private hangStartTime As DateTime = DateTime.MinValue
    Private lastCrashWasHungBeforehand As Boolean = False
    Private lastCrashHangDurationSeconds As Integer = 0
    Private trayIconGreen As Icon
    Private trayIconRed As Icon
    Private isVramAlerted As Boolean = False
    'Private Const VramWarningThreshold As Double = 90.0 ' percent
    Private sessionWarnings As New List(Of SessionWarningEntry)
    Private sessionWarningsLock As New Object()
    Private Const AdaptiveSampleCount As Integer = 30
    Private cpuTempSamples As New Queue(Of Double)()
    Private Const AdaptiveWarningMargin As Double = 15.0
    Private lastTdrCheckTime As DateTime = DateTime.UtcNow
    Private isRamAlerted As Boolean = False
    Private currentGpuTemp As Double = 0
    Private vegasStartTime As DateTime = DateTime.MinValue



    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Boolean, lParam As Integer) As Integer
    End Function
    Dim waitAttempts As Integer = 0
    Public Sub SaveCurrentLog()
        Dim logPath As String = GetCurrentLogPath()

        If System.IO.File.Exists(logPath) Then
            ' It is already saved! We can just show the user where it is.
            MessageBox.Show("Log is up to date at: " & logPath, "Log Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            ' If it doesn't exist, dump the current text just in case
            System.IO.File.WriteAllText(logPath, RTBlog.Text)
        End If
    End Sub





    Private Sub frmMain2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mnuHeartbeatLogging.Checked = My.Settings.HeartbeatEnabled
        MenuStrip1.RenderMode = ToolStripRenderMode.Professional
        'MenuStrip1.BackColor = Color.Aquamarine
        MenuStrip1.BackColor = Color.FromArgb(153, 180, 209)

        ' Tray icon setup - green dot = watching normally, red = a crash/hang-close was just flagged
        trayIconGreen = CreateDotIcon(Color.LimeGreen)
        trayIconRed = CreateDotIcon(Color.Red)
        TrayIcon.Icon = trayIconGreen
        TrayIcon.Text = "WatchVegas - Watching"
        TrayIcon.Visible = True
        RevertTrayIconTimer.Interval = 10000
        RevertTrayIconTimer.Enabled = False

        ' Add this for every chart you have
        ChartCPU.Legends.Clear()
        ChartGPU.Legends.Clear()
        ChartDisk.Legends.Clear()
        ChartRAM.Legends.Clear()

        For Each s As System.Windows.Forms.DataVisualization.Charting.Series In ChartCPU.Series
            s.IsVisibleInLegend = False
        Next

        For Each s As System.Windows.Forms.DataVisualization.Charting.Series In ChartGPU.Series
            s.IsVisibleInLegend = False
        Next

        For Each s As System.Windows.Forms.DataVisualization.Charting.Series In ChartDisk.Series
            s.IsVisibleInLegend = False
        Next

        For Each s As System.Windows.Forms.DataVisualization.Charting.Series In ChartRAM.Series
            s.IsVisibleInLegend = False
        Next

        Me.Text = "WatchVegas-2026" & WVersion
        RTBlog.Text = "Loading please wait....." & vbCrLf

        ' Set up UI first
        ApplyAppSettings()

        ' Start the init process
        Task.Run(Sub()

                     Try

                         ' --- INITIALIZATION ---
                         computer.IsCpuEnabled = True
                         computer.IsGpuEnabled = True
                         computer.IsMemoryEnabled = True
                         computer.IsStorageEnabled = True
                         computer.Open()

                         ' --- SAFE COUNTER INITIALIZATION ---
                         Try

                             cpuCounter = New PerformanceCounter(
                             "Processor",
                             "% Processor Time",
                             "_Total")

                             ramCounter = New PerformanceCounter(
                             "Memory",
                             "% Committed Bytes In Use")

                             If PerformanceCounterCategory.Exists("PhysicalDisk") Then

                                 diskCounter = New PerformanceCounter(
                                 "PhysicalDisk",
                                 "% Disk Time",
                                 "_Total")

                             End If

                             IsReady = True

                         Catch ex As Exception

                             Me.BeginInvoke(
                             Sub()
                                 LogToScreenAndDisk(
                                     "Warning: Could not init performance counters. " &
                                     ex.Message)
                             End Sub)

                             IsReady = False

                         End Try

                     Catch ex As Exception

                         Me.BeginInvoke(
                         Sub()
                             LogToScreenAndDisk(
                                 "BACKGROUND ERROR: " &
                                 ex.Message)
                         End Sub)

                     End Try

                 End Sub)

        ' Start your timer AFTER the task has started
        'LogToScreenAndDisk("TEST MESSAGE")
        dataGatherTimer.Start()
        'LogToScreenAndDisk("TEST AFTER TIMER")
        LogToScreenAndDisk("System Ready. Tracking hardware.")
        LogToScreenAndDisk(
    "Hardware monitoring active. Heartbeat recording enabled (log file only).")

        ' --- Check if Vegas is already running at startup ---
        Dim procs = Process.GetProcessesByName("vegas2026")
        If procs.Length > 0 Then

            hasVegasInitialized = True

            RTBlog.AppendText(
            $"[{DateTime.Now:yyyyMMdd_HHmmss}] Vegas detected on startup. Scanning modules..." &
            vbCrLf)

            Task.Run(Sub() ScanModulesToFile())

        Else

            hasVegasInitialized = False

        End If
        BtnCF.ForeColor = Color.Navy
        BtnCF.BackColor = SystemColors.Control
    End Sub

    Private Sub LogToFileOnly(message As String)

        Try

            Dim filePath As String = GetCurrentLogPath()

            SyncLock Me
                System.IO.File.AppendAllText(
                filePath,
                message & Environment.NewLine)
            End SyncLock

        Catch ex As Exception
            Debug.WriteLine("File logging error: " & ex.Message)
        End Try

    End Sub



    Private Function GetModuleColor(moduleName As String) As Color
        ' Check for specific keywords (case-insensitive)
        If moduleName.Contains("BorisFX", StringComparison.OrdinalIgnoreCase) Then Return Color.DarkOrange
        If moduleName.Contains("BCC", StringComparison.OrdinalIgnoreCase) Then Return Color.RoyalBlue
        If moduleName.Contains("NewBlue", StringComparison.OrdinalIgnoreCase) Then Return Color.LimeGreen
        If moduleName.Contains("Mocha", StringComparison.OrdinalIgnoreCase) Then Return Color.MediumPurple

        ' Using FromArgb allows precise control over darkness. 
        ' Lower numbers are darker (0,0,0 is black). 
        ' 64, 64, 64 is a strong dark grey.
        Return Color.FromArgb(10, 10, 10)
    End Function


    Private Sub ScanModulesToFile()

        ' Force the filter OFF so the scan always shows a full list
        showOnlyPlugins = False

        Try

            Dim filePath As String = GetCurrentLogPath()
            Dim moduleList As New List(Of String)
            Dim procs = Process.GetProcessesByName("vegas2026")

            If procs.Length > 0 Then

                Dim targetProcess = procs(0)

                fullModuleHistory.Clear()

                ' One timestamp for the log file
                Dim scanTime As String =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

                ' Short timestamp for RTBlog display
                Dim displayTime As String =
                DateTime.Now.ToString("HH:mm:ss")

                For Each m As ProcessModule In targetProcess.Modules

                    ' Store clean version for RTBlog
                    fullModuleHistory.Add(
                    $"[{displayTime}] {m.ModuleName}")

                    ' Store detailed version for the log file
                    moduleList.Add(
                    $"[{scanTime}] Module: {m.ModuleName} | Path: {m.FileName}")

                Next

                System.IO.File.AppendAllLines(filePath, moduleList)

                ' Trigger UI refresh
                Me.BeginInvoke(Sub() RefreshLogView(True))

            Else

                Me.BeginInvoke(
                Sub()
                    RTBlog.AppendText(
                        $"[{DateTime.Now:HH:mm:ss}] [ERROR]: Vegas2026 not found." &
                        vbCrLf)
                End Sub)

            End If

        Catch ex As Exception

            Me.BeginInvoke(
            Sub()
                RTBlog.AppendText(
                    $"[{DateTime.Now:HH:mm:ss}] [ERROR]: Scan failed - " &
                    ex.Message &
                    vbCrLf)
            End Sub)

        End Try

    End Sub

    ' Ensure this function exists ONCE in your class


    Private Function IsImportantPlugin(moduleName As String) As Boolean
        Dim nameUpper As String = moduleName.ToUpper()
        Return nameUpper.Contains("BORIS") OrElse
           nameUpper.Contains("BCC") OrElse
           nameUpper.Contains("MOCHA") OrElse
           nameUpper.Contains("NEWBLUE") OrElse
           nameUpper.Contains("NBX")
    End Function

    Private Sub UpdateWatchedPluginsStatus()
        Try
            Dim count As Integer = 0
            Dim watchList = My.Settings.WatchedPlugins

            If watchList IsNot Nothing AndAlso watchList.Count > 0 Then
                SyncLock lastKnownModules
                    For Each modName As String In lastKnownModules
                        For Each watched As String In watchList
                            If modName.ToUpper().Contains(watched.ToUpper()) Then
                                count += 1
                                Exit For
                            End If
                        Next
                    Next
                End SyncLock
            End If

            Dim statusText As String = $"Watched Plugins: {count}"

            If StatusStrip1.InvokeRequired Then
                StatusStrip1.BeginInvoke(Sub() TolWatchedPlugins.Text = statusText)
            Else
                TolWatchedPlugins.Text = statusText
            End If
        Catch
        End Try
    End Sub




    Private Sub UpdateCpuTemperature(currentTemp As Double)
        cpuTempSamples.Enqueue(currentTemp)
        While cpuTempSamples.Count > AdaptiveSampleCount
            cpuTempSamples.Dequeue()
        End While

        If cpuTempSamples.Count < AdaptiveSampleCount Then
            If Not My.Settings.chkadaptiveWarning Then
                CheckTemperatureAlert(currentTemp)
            End If
            toolCpuTemp.Text = "CPU: " & Math.Round(currentTemp) & "°C"
            Return
        End If

        If My.Settings.chkadaptiveWarning Then
            Dim baseline As Double = cpuTempSamples.Average()
            If currentTemp > baseline + AdaptiveWarningMargin Then
                RaiseAdaptiveTemperatureWarning(currentTemp, baseline)
            Else
                ClearTemperatureWarning()
            End If
            toolCpuTemp.Text = $"CPU: {Math.Round(currentTemp)}°C (Baseline {baseline:0.0}°C)"
        Else
            CheckTemperatureAlert(currentTemp)
            toolCpuTemp.Text = "CPU: " & Math.Round(currentTemp) & "°C"
        End If
    End Sub

    Private Sub SetStatusStripColor(backColor As Color)
        If backColor = SystemColors.Control OrElse backColor = SystemColors.ActiveCaption Then
            StatusStrip1.BackColor = Color.FromArgb(153, 180, 209)
            StatusStrip1.ForeColor = Color.Black
            toolCpuTemp.ForeColor = Color.Black
            ToolVRAM.ForeColor = Color.Black
            TolWatchedPlugins.ForeColor = Color.Black
        Else
            StatusStrip1.BackColor = backColor
            StatusStrip1.ForeColor = Color.White
            toolCpuTemp.ForeColor = Color.White
            ToolVRAM.ForeColor = Color.White
            TolWatchedPlugins.ForeColor = Color.White
        End If
    End Sub


    Private Sub RaiseAdaptiveTemperatureWarning(currentTemp As Double, baseline As Double)
        If Not HasAlerted Then
            RTBlog.SelectionStart = RTBlog.TextLength
            RTBlog.SelectionColor = Color.Red
            RTBlog.AppendText(Environment.NewLine & $"!!! WARNING: CPU Temp {currentTemp:0.0}°C exceeds baseline {baseline:0.0}°C + margin of {AdaptiveWarningMargin}°C" & Environment.NewLine)
            RTBlog.SelectionColor = RTBlog.ForeColor
            Me.WindowState = FormWindowState.Normal
            Me.Activate()
            LogSessionWarning($"CPU Temp {currentTemp:0.0}°C exceeds baseline {baseline:0.0}°C + margin of {AdaptiveWarningMargin}°C", WarningSeverity.Caution)
        End If
        SetStatusStripColor(Color.Red)
        IsOverheating = True
    End Sub

    Private Sub ClearTemperatureWarning()
        If IsOverheating Then
            SetStatusStripColor(SystemColors.Control)
            IsOverheating = False
            HasAlerted = False
        End If
    End Sub



    Private Sub CheckTemperatureAlert(currentTemp As Double)
        ' 1. Check if the feature is enabled
        If My.Settings.EnableThermalWarning Then

            ' 2. Evaluate the danger
            If currentTemp > My.Settings.TempThreshold Then

                ' LOCK THE RED STATE
                IsOverheating = True

                ' Only set if it isn't already red (stops unnecessary refreshing)
                If StatusStrip1.BackColor <> Color.Red Then
                    SetStatusStripColor(Color.Red)
                End If

                ' 3. Alert the user (once)
                If Not HasAlerted Then
                    RTBlog.SelectionStart = RTBlog.TextLength
                    RTBlog.SelectionColor = Color.Red
                    RTBlog.AppendText(Environment.NewLine & "!!! WARNING: CPU Temp " & currentTemp & "°C exceeds limit " & My.Settings.TempThreshold & "°C")
                    RTBlog.SelectionColor = RTBlog.ForeColor
                    Me.WindowState = FormWindowState.Normal
                    Me.Activate()
                    HasAlerted = True
                    LogSessionWarning($"CPU Temp {currentTemp}°C exceeds limit {My.Settings.TempThreshold}°C", WarningSeverity.Caution)
                End If

            Else
                ' 4. COOL DOWN LOGIC
                ' Only turn it off if it was actually in the "Overheating" state
                If IsOverheating Then
                    SetStatusStripColor(SystemColors.Control)
                    IsOverheating = False
                    HasAlerted = False
                End If
            End If
        End If
    End Sub
    ' This generates the exact same filename for Save, Open, and Print
    ' Add this once to your class
    Private Function GetCurrentLogPath() As String
        ' Ensures we have a Logs folder
        Dim logDir As String = Path.Combine(Application.StartupPath, "Logs")
        If Not Directory.Exists(logDir) Then Directory.CreateDirectory(logDir)

        ' Returns: ...\Logs\Wvegaslog_20260604.txt
        Return Path.Combine(logDir, "Wvegaslog_" & DateTime.Now.ToString("yyyyMMdd") & ".txt")
    End Function
    Private Sub OpenLogFile()
        Dim logPath As String = GetCurrentLogPath()

        If System.IO.File.Exists(logPath) Then
            Process.Start("notepad.exe", logPath)
        Else
            MessageBox.Show("No log file found for today (" & logPath & "). " & vbCrLf &
                        "Please ensure the application has generated some data first.",
                        "Log Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub RefreshPerformanceMetrics()
        Try
            ' Using PerformanceCounters to get real data
            Using cpuCounter As New PerformanceCounter("Processor", "% Processor Time", "_Total"),
              diskCounter As New PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total")

                ' First call to NextValue() always returns 0, so we call it and sleep
                cpuCounter.NextValue()
                diskCounter.NextValue()
                System.Threading.Thread.Sleep(500)

                lastCpuLoad = Math.Round(cpuCounter.NextValue(), 0).ToString() & "%"
                lastDiskLoad = Math.Round(diskCounter.NextValue(), 0).ToString() & "%"
            End Using

            ' Mocking GPU/VRAM as PerformanceCounter for GPU is complex and requires specific drivers
            lastGpuLoad = "N/A"
            lastVramLoad = "N/A"
        Catch ex As Exception
            lastCpuLoad = "Err"
            lastDiskLoad = "Err"
        End Try
    End Sub


    Private Sub InitializeSensors()
        cpuSensor = Nothing

        ' STRICT SEARCH: Only look at HardwareType.Cpu
        For Each hw As IHardware In computer.Hardware
            ' We only care about the CPU
            If hw.HardwareType = HardwareType.Cpu Then
                hw.Update()

                For Each sensor As ISensor In hw.Sensors
                    If sensor.SensorType = SensorType.Temperature AndAlso sensor.Value.HasValue Then
                        ' Found a valid CPU temp!
                        ' We want "Package" if available, otherwise take the first valid core
                        If sensor.Name.Contains("Package") Then
                            cpuSensor = sensor
                            Exit For
                        ElseIf cpuSensor Is Nothing Then
                            cpuSensor = sensor
                        End If
                    End If
                Next
            End If

            If cpuSensor IsNot Nothing Then Exit For
        Next

        If cpuSensor IsNot Nothing Then
            LogToScreenAndDisk(">>> SUCCESS: CPU Sensor Initialized: " & cpuSensor.Name)
        End If
    End Sub

    ' --- CHART MANAGEMENT ---
    Private Sub SetupCharts()
        Dim chartList As Chart() = {ChartCPU, ChartRAM, ChartGPU, ChartDisk}
        Dim titles As String() = {"CPU", "RAM", "GPU", "Disk"}
        Dim maxValues As Double() = {10, 50, 25, 10}
        ' Define the color array: Top (0) to Bottom (3)
        Dim chartColors As Color() = {Color.Blue, Color.Green, Color.DarkGoldenrod, Color.Red}

        For i As Integer = 0 To chartList.Length - 1
            Dim ch As Chart = chartList(i)

            ch.Series.Clear()
            ch.Titles.Clear()

            ' 1. The Thick Border (Box)
            ch.ChartAreas(0).BorderColor = Color.Black
            ch.ChartAreas(0).BorderWidth = 2
            ch.ChartAreas(0).BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid

            ' 2. Background Color
            ch.ChartAreas(0).BackColor = Color.LightYellow
            ch.ChartAreas(0).BackGradientStyle = GradientStyle.None

            ' 3. Static X-Axis
            ch.ChartAreas(0).AxisX.Minimum = 0
            ch.ChartAreas(0).AxisX.Maximum = 60
            ch.ChartAreas(0).AxisX.Interval = 12
            ch.ChartAreas(0).AxisX.LabelStyle.Format = "0"
            ch.ChartAreas(0).AxisX.MajorGrid.LineColor = Color.Gray

            ' 4. Y-Axis
            ch.ChartAreas(0).AxisY.Minimum = 0
            ch.ChartAreas(0).AxisY.Maximum = maxValues(i)
            ch.ChartAreas(0).AxisY.LabelStyle.Format = "0"
            ch.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.Gray

            ' 5. Series (Colors assigned by index)
            Dim s As New Series("Data")
            s.ChartType = SeriesChartType.Line
            s.BorderWidth = 3
            s.Color = chartColors(i) ' Assigns Blue, Green, Orange, Red respectively
            ch.Series.Add(s)

            ' 6. Title
            ch.Titles.Add(titles(i) & ": 0%")
            ch.Titles(0).Font = New Font("Arial", 10, FontStyle.Bold)
        Next
    End Sub

    Private Sub WriteToErrorLog(ex As Exception)
        Try
            Dim path As String = "error_log.txt"
            Dim message As String = DateTime.Now.ToString() & " | CRASH: " & ex.Message & vbCrLf & ex.StackTrace & vbCrLf & "-----------------" & vbCrLf
            System.IO.File.AppendAllText(path, message)
        Catch
            ' If we can't write to the file, there is nothing more we can do
        End Try
    End Sub
    Private Sub UpdateChart(targetChart As System.Windows.Forms.DataVisualization.Charting.Chart, value As Double, seriesName As String)
        If targetChart Is Nothing Then Exit Sub

        ' 1. INITIALIZE SERIES (Removed Legend link)
        If targetChart.Series.IndexOf(seriesName) = -1 Then
            Dim s As New System.Windows.Forms.DataVisualization.Charting.Series(seriesName)
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
            s.BorderWidth = 4
            ' The line "s.Legend = ..." is gone
            targetChart.Series.Add(s)
        End If
        ' 2. APPLY STYLING
        targetChart.ChartAreas(0).BackColor = Color.LightYellow
        Dim warnAt As Double = If(seriesName.Contains("GPU"), My.Settings.GpuChartWarningThreshold, My.Settings.CpuChartWarningThreshold)
        Dim warningColor As Color = GetChartWarningColor(value, warnAt, warnAt + 15, SystemColors.ActiveCaption)
        targetChart.BackColor = warningColor
        targetChart.ChartAreas(0).BorderColor = warningColor
        targetChart.ChartAreas(0).BorderWidth = If(value >= warnAt, 4, 1)
        targetChart.BorderlineColor = warningColor
        targetChart.BorderlineWidth = If(value >= warnAt, 4, 2)
        targetChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None

        ' 3. APPLY COLORS
        Select Case True
            Case seriesName.Contains("CPU") : targetChart.Series(seriesName).Color = Color.Blue
            Case seriesName.Contains("GPU") : targetChart.Series(seriesName).Color = Color.Red
            Case seriesName.Contains("RAM") : targetChart.Series(seriesName).Color = Color.Green
            Case seriesName.Contains("Disk") : targetChart.Series(seriesName).Color = Color.Orange
        End Select

        ' 4. TITLE & FONT
        If targetChart.Titles.Count = 0 Then targetChart.Titles.Add(seriesName)
        targetChart.Titles(0).Font = New Font("Arial", 10, FontStyle.Bold)
        targetChart.Titles(0).Text = seriesName & ": " & Math.Round(value) & "%"

        ' 5. ADD DATA POINT
        targetChart.Series(seriesName).Points.AddXY(xCounter, value)

        ' 6. MANAGE BUFFER
        If targetChart.Series(seriesName).Points.Count > 30 Then
            targetChart.Series(seriesName).Points.RemoveAt(0)
        End If

        ' 7. DYNAMIC STEPPED SCALING
        Dim maxVal As Double = 0
        For Each pt As System.Windows.Forms.DataVisualization.Charting.DataPoint In targetChart.Series(seriesName).Points
            If pt.YValues(0) > maxVal Then maxVal = pt.YValues(0)
        Next

        If maxVal <= 25 Then
            targetChart.ChartAreas(0).AxisY.Maximum = 25
        ElseIf maxVal <= 50 Then
            targetChart.ChartAreas(0).AxisY.Maximum = 50
        Else
            targetChart.ChartAreas(0).AxisY.Maximum = 100
        End If
        targetChart.ChartAreas(0).AxisY.Minimum = 0

        ' 8. SLIDE X-AXIS
        If targetChart.ChartAreas.Count > 0 Then
            targetChart.ChartAreas(0).AxisX.Minimum = xCounter - 30
            targetChart.ChartAreas(0).AxisX.Maximum = xCounter
        End If

        targetChart.Invalidate()
    End Sub

    Private Sub ApplyChartStyles(targetChart As System.Windows.Forms.DataVisualization.Charting.Chart, seriesName As String)
        ' Background and Border
        targetChart.ChartAreas(0).BackColor = Color.LightYellow
        targetChart.BorderlineColor = Color.Black
        targetChart.BorderlineWidth = 2

        ' Standardized Title Font (Fixes the sizing issue)
        targetChart.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

        ' Fixed Y-Axis
        targetChart.ChartAreas(0).AxisY.Minimum = 0
        targetChart.ChartAreas(0).AxisY.Maximum = 100

        ' Specific Line Colors (Order: Top to Bottom)
        If seriesName.Contains("CPU") Then
            targetChart.Series(0).Color = Color.Blue
        ElseIf seriesName.Contains("GPU") Then
            targetChart.Series(0).Color = Color.Red
        ElseIf seriesName.Contains("RAM") Then
            targetChart.Series(0).Color = Color.Green
        ElseIf seriesName.Contains("Disk") Then
            targetChart.Series(0).Color = Color.Orange
        End If
    End Sub
    ' --- UTILITIES & PROCESSES ---

    ' Place this with your other Utility Subs (e.g., near LogToScreenAndDisk)



    Public Sub LogToScreenAndDisk(message As String)
        ' 1. Thread safety
        If RTBlog.InvokeRequired Then
            RTBlog.Invoke(Sub() LogToScreenAndDisk(message))
            Return
        End If

        ' 2. Prepare the message
        Dim fullMessage As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & ": " & message & vbCrLf

        ' 3. SAVE TO THE CORRECT, DYNAMIC LOG FILE (Unified)
        Try
            System.IO.File.AppendAllText(GetCurrentLogPath(), fullMessage)
        Catch ex As Exception
            Debug.WriteLine("Log file busy: " & ex.Message)
        End Try

        ' 4. LOG TO SCREEN
        RTBlog.SelectionStart = RTBlog.TextLength
        If message.Contains("WARNING") OrElse message.Contains("TEST ALERT") Then
            RTBlog.SelectionColor = Color.Red
        Else
            RTBlog.SelectionColor = Color.Black
        End If
        RTBlog.AppendText(fullMessage)
        RTBlog.ScrollToCaret()
    End Sub

    Public Sub TakeScreenshot(Optional isManual As Boolean = True)
        Try
            Dim path As String = GetScreenshotPath()
            If Not IO.Directory.Exists(path) Then IO.Directory.CreateDirectory(path)
            Me.Visible = False
            Application.DoEvents()
            System.Threading.Thread.Sleep(300)
            Using bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.CopyFromScreen(0, 0, 0, 0, bmp.Size)
                End Using
                Dim prefix As String = If(isManual, "Manual_", "Auto_")
                Dim fileName As String = prefix & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".png"
                bmp.Save(IO.Path.Combine(path, fileName), System.Drawing.Imaging.ImageFormat.Png)
            End Using
            Me.Visible = True
        Catch ex As Exception
            Me.Visible = True
            LogToScreenAndDisk("!!! Error taking screenshot: " & ex.Message & " !!!")
        End Try
    End Sub



    Private isCheckingActive As Boolean = False

    Private Sub PerformProcessChecks()

        If isChecking Then Return
        isChecking = True

        Task.Run(Sub()

                     Try

                         Dim processList() As Process = Process.GetProcessesByName("vegas2026")
                         Dim isRunningNow As Boolean = (processList.Length > 0)

                         Debug.WriteLine($"Vegas Count={processList.Length} Running={isRunningNow}")

                         ' --- Integrated Responsiveness Check ---
                         If isRunningNow Then

                             Dim anyHungNow As Boolean = False

                             For Each p As Process In processList

                                 If Not p.Responding Then
                                     anyHungNow = True

                                     If Not isVegasHung Then
                                         isVegasHung = True
                                         hangStartTime = DateTime.Now
                                         LogToScreenAndDisk("!!! WARNING: Vegas detected as NOT RESPONDING !!!")
                                         LogSessionWarning("Vegas became unresponsive (not crashed - hang started)", WarningSeverity.Severe)
                                         lastVegasAlertTime = DateTime.Now
                                     ElseIf (DateTime.Now - lastVegasAlertTime).TotalSeconds > 30 Then
                                         Dim hungSoFar As Integer = CInt((DateTime.Now - hangStartTime).TotalSeconds)
                                         LogToScreenAndDisk($"!!! Vegas STILL not responding - {hungSoFar}s and counting !!!")
                                         lastVegasAlertTime = DateTime.Now
                                     End If

                                 End If

                             Next

                             If Not anyHungNow AndAlso isVegasHung Then
                                 Dim hungTotal As Integer = CInt((DateTime.Now - hangStartTime).TotalSeconds)
                                 LogToScreenAndDisk($">>> Vegas became responsive again after {hungTotal}s.")
                                 LogSessionWarning($"Vegas recovered from a hang after ~{hungTotal}s", WarningSeverity.Info)
                                 isVegasHung = False
                             End If

                         End If

                         ' --- Check Vegas CPU Priority ---
                         If isRunningNow Then
                             Try
                                 Dim vegasProc = Process.GetProcessesByName("vegas2026").FirstOrDefault()
                                 If vegasProc IsNot Nothing Then
                                     Dim priority As String = vegasProc.PriorityClass.ToString()
                                     If vegasProc.PriorityClass <> ProcessPriorityClass.Normal AndAlso
                                    vegasProc.PriorityClass <> ProcessPriorityClass.AboveNormal AndAlso
                                    vegasProc.PriorityClass <> ProcessPriorityClass.High Then
                                         LogToScreenAndDisk($"!!! WARNING: Vegas CPU priority is {priority} - this may cause stuttering or freezes")
                                         LogSessionWarning($"Vegas CPU priority is unexpectedly low: {priority}", WarningSeverity.Caution)
                                     End If
                                 End If
                             Catch
                             End Try
                         End If

                         ' --- Log State Change ---
                         If isRunningNow <> isVegasRunning Then

                             isVegasRunning = isRunningNow

                             If isRunningNow Then
                                 vegasStartTime = DateTime.Now
                                 Me.BeginInvoke(Sub()
                                                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] >>> vegas2026 detected running." & vbCrLf)
                                                    RTBlog.AppendText("... Loading modules, please wait ..." & vbCrLf)
                                                End Sub)
                                 Task.Run(Sub() PauseWindowsUpdate())
                             End If

                             If Not isRunningNow Then

                                 vegasStartTime = DateTime.MinValue
                                 Me.BeginInvoke(Sub() Me.Text = "WatchVegas-2026" & WVersion)

                                 Dim closeDetectedTime As DateTime = DateTime.Now

                                 Dim wasHungAtClose As Boolean = isVegasHung
                                 Dim hungDurationAtClose As Integer = If(wasHungAtClose, CInt((DateTime.Now - hangStartTime).TotalSeconds), 0)
                                 isVegasHung = False

                                 System.Threading.Thread.Sleep(3000)

                                 Dim culpritModule As String = "Unknown"
                                 Dim isRealCrash As Boolean = WasRealCrash("vegas2026", closeDetectedTime.AddSeconds(-60), culpritModule)

                                 If isRealCrash Then
                                     lastCrashCulpritModule = culpritModule
                                     lastCrashWasHungBeforehand = wasHungAtClose
                                     lastCrashHangDurationSeconds = hungDurationAtClose
                                     Dim hangNote As String = If(wasHungAtClose, $" (was unresponsive for ~{hungDurationAtClose}s beforehand)", "")
                                     Me.BeginInvoke(Sub() RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] >>> CRASH CONFIRMED via Windows Event Log. Culprit module: {culpritModule}{hangNote}" & vbCrLf))
                                     LogSessionWarning($"CRASH CONFIRMED - Culprit module: {culpritModule}{hangNote}", WarningSeverity.Critical)
                                     FlagTrayAlert("VEGAS Crashed", $"Culprit module: {culpritModule}{hangNote}")
                                     CaptureCrashSnapshot()
                                 ElseIf wasHungAtClose Then
                                     Me.BeginInvoke(Sub() RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] >>> vegas2026 closed after being unresponsive for ~{hungDurationAtClose}s (likely force-closed due to a hang - no formal crash event was logged)." & vbCrLf))
                                     LogSessionWarning($"Vegas closed after a ~{hungDurationAtClose}s hang (likely force-closed, no formal crash event found)", WarningSeverity.Critical)
                                     FlagTrayAlert("VEGAS Closed After Hang", $"Was unresponsive for ~{hungDurationAtClose}s before closing")
                                     LogToFileOnly($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] vegas2026 closed after a ~{hungDurationAtClose}s hang (likely force-closed).")
                                 Else
                                     Me.BeginInvoke(Sub() RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] vegas2026 closed normally (no crash event found - not flagging as a crash)." & vbCrLf))
                                     LogToFileOnly($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] vegas2026 closed normally.")
                                 End If

                                 Task.Run(Sub() ResumeWindowsUpdate())

                                 lastKnownModules.Clear()
                                 fullModuleHistory.Clear()
                                 initialScanDone = False

                             End If

                         End If

                         ' --- Interference Monitor ---
                         If isRunningNow AndAlso isMonitoringConflicts Then

                             Dim oldConflicts As New List(Of String)(lastKnownConflicts)
                             Dim detected As New List(Of String)

                             For Each appName As String In interferenceApps

                                 Dim conflictProcesses() As Process = Process.GetProcessesByName(appName)

                                 If conflictProcesses.Length > 0 Then
                                     detected.Add(appName)
                                     If Not lastKnownConflicts.Contains(appName) Then
                                         lastKnownConflicts.Add(appName)
                                         LogSessionWarning($"Interference app detected: {appName}", WarningSeverity.Caution)
                                         Me.BeginInvoke(Sub()
                                                            RTBlog.SelectionColor = Color.Red
                                                            RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                                                            RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [!] WARNING: Interference app detected: {appName}" & vbCrLf)
                                                            RTBlog.SelectionColor = RTBlog.ForeColor
                                                            RTBlog.ScrollToCaret()
                                                        End Sub)
                                     End If
                                 ElseIf lastKnownConflicts.Contains(appName) Then
                                     lastKnownConflicts.Remove(appName)
                                 End If

                             Next

                             Dim changed As Boolean = False
                             If oldConflicts.Count <> detected.Count Then
                                 changed = True
                             Else
                                 For Each n In detected
                                     If Not oldConflicts.Contains(n) Then
                                         changed = True
                                         Exit For
                                     End If
                                 Next
                             End If

                             If changed Then
                                 Me.BeginInvoke(Sub()
                                                    If detected.Count > 0 Then
                                                        RTBlog.SelectionColor = Color.Red
                                                        RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                                                        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [INTERFERENCE]: Detected {detected.Count} app(s): {String.Join(", ", detected)}" & vbCrLf)
                                                    Else
                                                        RTBlog.SelectionColor = Color.DarkGreen
                                                        RTBlog.SelectionFont = RTBlog.Font
                                                        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [INTERFERENCE]: Conflicts cleared - no conflicts detected" & vbCrLf)
                                                    End If
                                                    RTBlog.SelectionColor = RTBlog.ForeColor
                                                    RTBlog.ScrollToCaret()
                                                End Sub)
                             End If

                         End If

                         ' --- Module Scan ---
                         If isRunningNow Then

                             Dim doScan As Boolean = True
                             If initialScanDone Then
                                 moduleCheckCounter += 1
                                 If moduleCheckCounter < 6 Then
                                     doScan = False
                                 Else
                                     moduleCheckCounter = 0
                                 End If
                             End If

                             If doScan AndAlso Not isScanningModules Then

                                 isScanningModules = True

                                 Try

                                     Dim pWithWindow As Process = Nothing
                                     Dim vegasProcs() As Process = Process.GetProcessesByName("vegas2026")
                                     For Each cand As Process In vegasProcs
                                         If cand.MainWindowHandle <> IntPtr.Zero Then
                                             pWithWindow = cand
                                             Exit For
                                         End If
                                     Next

                                     If pWithWindow IsNot Nothing Then

                                         For Each m As ProcessModule In pWithWindow.Modules

                                             If Not lastKnownModules.Contains(m.ModuleName) Then

                                                 lastKnownModules.Add(m.ModuleName)
                                                 lastModuleLoaded = m.ModuleName
                                                 fullModuleHistory.Add($"[{DateTime.Now:HH:mm:ss}] {m.ModuleName}")

                                                 ' Flag codec DLLs that load after initial startup scan
                                                 ' as they often indicate a newly opened media file
                                                 If initialScanDone Then
                                                     Dim modUpper As String = m.ModuleName.ToUpper()
                                                     If modUpper.Contains("CODEC") OrElse
   modUpper.Contains("DECODER") OrElse
   modUpper.Contains("ENCODER") OrElse
   modUpper.Contains("MFT") OrElse
   modUpper.Contains("LAV") OrElse
   modUpper.Contains("PRORES") OrElse
   modUpper.Contains("HEVC") OrElse
   modUpper.Contains("H264") OrElse
   modUpper.Contains("H265") OrElse
   modUpper.Contains("OFX") Then
                                                         LogSessionWarning($"New codec/plugin loaded mid-session: {m.ModuleName}", WarningSeverity.Info)
                                                     End If
                                                 End If



                                                 Dim currentMod As String = m.ModuleName

                                                 Me.BeginInvoke(Sub()

                                                                    Dim modUp As String = currentMod.ToUpperInvariant()
                                                                    Dim isPlugin As Boolean =
                                                                    modUp.Contains("BORIS") OrElse
                                                                    modUp.Contains("BCC") OrElse
                                                                    modUp.Contains("NEWBLUE") OrElse
                                                                    modUp.Contains("NBX") OrElse
                                                                    modUp.Contains("TITLER")

                                                                    If showOnlyPlugins AndAlso Not isPlugin Then Return

                                                                    If modUp.Contains("BORIS") OrElse modUp.Contains("BCC") Then
                                                                        RTBlog.SelectionColor = Color.FromArgb(0, 60, 0)
                                                                        RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                                                                    ElseIf modUp.Contains("NEWBLUE") OrElse modUp.Contains("NBX") OrElse modUp.Contains("TITLER") Then
                                                                        RTBlog.SelectionColor = Color.FromArgb(150, 80, 0)
                                                                        RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                                                                    Else
                                                                        RTBlog.SelectionColor = Color.FromArgb(60, 60, 60)
                                                                        RTBlog.SelectionFont = RTBlog.Font
                                                                    End If

                                                                    Dim label As String = If(isPlugin, "[PLUGIN]", "[SYS]")
                                                                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] {label}: {currentMod}" & vbCrLf)
                                                                    RTBlog.SelectionColor = RTBlog.ForeColor
                                                                    RTBlog.ScrollToCaret()

                                                                End Sub)

                                             End If

                                         Next

                                         initialScanDone = True

                                     End If

                                 Finally
                                     isScanningModules = False
                                 End Try

                             End If

                         End If

                     Catch ex As Exception

                         Me.BeginInvoke(Sub()
                                            RTBlog.SelectionColor = Color.Red
                                            RTBlog.AppendText($"[!!! CRASH !!!] {ex.Message}" & vbCrLf)
                                            RTBlog.ScrollToCaret()
                                        End Sub)

                     Finally
                         isChecking = False
                     End Try

                     Me.BeginInvoke(Sub()
                                        UpdateWatchedPluginsStatus()
                                    End Sub)

                 End Sub)

    End Sub

    ' --- TEST SANDBOX ---
    Public Sub TestModuleScan(Optional showAll As Boolean = False)
        ' 1. THE RETRY LOOP (New Approach)
        Dim p As Process = Nothing
        Dim retryCount As Integer = 0

        ' Keep trying for up to 5 seconds to find a process that actually has modules
        While retryCount < 5
            Dim procs = Process.GetProcessesByName("vegas2026")

            If procs.Length > 0 Then
                Dim candidate = procs(0)
                candidate.Refresh()

                ' Check if this process has loaded its modules
                If candidate.Modules.Count > 0 Then
                    p = candidate
                    Exit While ' Success! We found the process and it has data.
                End If
            End If

            System.Threading.Thread.Sleep(1000) ' Wait 1 second and re-query
            retryCount += 1
        End While

        ' 2. VALIDATION
        If p Is Nothing Then
            RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [ERROR]: Could not find Vegas process with loaded modules." & vbCrLf)
            Return
        End If

        ' 3. SCANNING
        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] --- SCANNING MODULES IN VEGAS ---" & vbCrLf)

        Try
            For Each m As ProcessModule In p.Modules
                Dim modName = m.ModuleName.ToUpper()

                ' Boris / BCC
                If modName.Contains("BORIS") OrElse modName.Contains("BCC") Then
                    RTBlog.SelectionColor = Color.FromArgb(0, 60, 0)
                    RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [PLUGIN]: {modName}" & vbCrLf)

                    ' NewBlue
                ElseIf modName.Contains("NEWBLUE") OrElse modName.Contains("NBX") OrElse modName.Contains("TITLER") Then
                    RTBlog.SelectionColor = Color.FromArgb(150, 80, 0)
                    RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [PLUGIN]: {modName}" & vbCrLf)

                    ' Moca
                ElseIf modName.Contains("MOCAPRO") OrElse modName.Contains("MOCA") Then
                    RTBlog.SelectionColor = Color.Purple
                    RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [PLUGIN]: {modName}" & vbCrLf)

                    ' System
                ElseIf showAll Then
                    RTBlog.SelectionColor = Color.Gray
                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [SYS]: {modName}" & vbCrLf)
                End If
            Next
        Catch ex As Exception
            RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [ERROR]: {ex.Message}" & vbCrLf)
        End Try

        RTBlog.SelectionColor = RTBlog.ForeColor
        RTBlog.ScrollToCaret()
    End Sub




    Private Sub RefreshLogView(Optional showHeader As Boolean = True)

        RTBlog.Clear()

        ' 1. Print the Header ONLY if showHeader is True
        If showHeader Then

            RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)

            RTBlog.SelectionColor = Color.DarkBlue
            RTBlog.AppendText(
            $"[{DateTime.Now:HH:mm:ss}] Vegas detected. Found {fullModuleHistory.Count} modules." &
            vbCrLf)

            RTBlog.SelectionColor = Color.DarkGreen
            RTBlog.AppendText(
            $"[{DateTime.Now:HH:mm:ss}] Heartbeat recording active (log file only)." &
            vbCrLf & vbCrLf)

        End If

        ' 2. Loop and Filter
        Dim searchTerm As String = txtSearch.Text.ToUpper()

        For Each logLine In fullModuleHistory

            Dim modUp = logLine.ToUpper()

            Dim isPlugin =
            modUp.Contains("BORIS") OrElse
            modUp.Contains("BCC") OrElse
            modUp.Contains("NEWBLUE") OrElse
            modUp.Contains("NBX") OrElse
            modUp.Contains("TITLER")

            ' Filter 1: Toggle (FX Only)
            If showOnlyPlugins AndAlso Not isPlugin Then Continue For

            ' Filter 2: Search Box
            If Not String.IsNullOrEmpty(searchTerm) AndAlso
           Not modUp.Contains(searchTerm) Then Continue For

            ' Apply colors
            If modUp.Contains("BORIS") OrElse modUp.Contains("BCC") Then

                RTBlog.SelectionColor = Color.FromArgb(0, 60, 0)
                RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)

            ElseIf modUp.Contains("NEWBLUE") OrElse
               modUp.Contains("NBX") OrElse
               modUp.Contains("TITLER") Then

                RTBlog.SelectionColor = Color.FromArgb(150, 80, 0)
                RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)

            Else

                RTBlog.SelectionColor = Color.FromArgb(60, 60, 60)
                RTBlog.SelectionFont = RTBlog.Font

            End If

            RTBlog.AppendText(logLine & vbCrLf)

        Next

        RTBlog.SelectionColor = RTBlog.ForeColor
        RTBlog.SelectionFont = RTBlog.Font

    End Sub
    Public Function GetScreenshotPath() As String
        Dim path As String = IO.Path.Combine(Application.StartupPath, "Screenshots")
        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        Return path
    End Function

    Private Sub PauseWindowsUpdate()
        Try
            Dim sc As New ServiceProcess.ServiceController("wuauserv")
            If sc.Status = ServiceProcess.ServiceControllerStatus.Running Then
                sc.Stop()
                sc.WaitForStatus(ServiceProcess.ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30))
                LogToScreenAndDisk("[SYSTEM] Windows Update paused while Vegas is running.")
                LogSessionWarning("Windows Update paused for Vegas session", WarningSeverity.Info)
            End If
            sc.Dispose()
        Catch ex As Exception
            LogToScreenAndDisk("[SYSTEM] Could not pause Windows Update: " & ex.Message)
        End Try
    End Sub

    Private Sub ResumeWindowsUpdate()
        Try
            Dim sc As New ServiceProcess.ServiceController("wuauserv")
            If sc.Status = ServiceProcess.ServiceControllerStatus.Stopped Then
                sc.Start()
                sc.WaitForStatus(ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(30))
                LogToScreenAndDisk("[SYSTEM] Windows Update resumed.")
            End If
            sc.Dispose()
        Catch ex As Exception
            LogToScreenAndDisk("[SYSTEM] Could not resume Windows Update: " & ex.Message)
        End Try
    End Sub




    Private Sub RtbLog_MouseEnter(sender As Object, e As EventArgs) Handles RTBlog.MouseEnter
        ' Optional: Ensures the tooltip reacts quickly
        myCustomTip.AutomaticDelay = 200

        Dim legend As String = "Color Legend:" & vbCrLf &
                               "Green: BorisFX" & vbCrLf &
                               "Orange: NewBlue/Titler" & vbCrLf &
                               "Blue: Other Modules" & vbCrLf &
                               "Gray: System"

        myCustomTip.SetToolTip(RTBlog, legend)
    End Sub
    ' --- EVENTS ---
    Private Sub frmMain2_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Normal Then
            For Each c As Control In Me.Controls
                If TypeOf c Is Chart Then c.Refresh()
            Next

            ' RTBlog can render solid black after sitting minimized for a while and being
            ' restored - a known RichTextBox quirk where a plain Refresh() doesn't always
            ' clear the stale rendering surface. Toggling WM_SETREDRAW off/on forces it to
            ' actually rebuild, then Invalidate/Refresh forces the repaint.
            SendMessage(RTBlog.Handle, WM_SETREDRAW, False, 0)
            SendMessage(RTBlog.Handle, WM_SETREDRAW, True, 0)
            RTBlog.Invalidate()
            RTBlog.Refresh()
        End If
    End Sub

    Private Sub frmMain2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Me.WindowState = FormWindowState.Normal Then
            SendMessage(RTBlog.Handle, WM_SETREDRAW, False, 0)
            SendMessage(RTBlog.Handle, WM_SETREDRAW, True, 0)
            RTBlog.Invalidate()
            RTBlog.Refresh()
        End If
    End Sub


    Private Sub frmMain2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        dataGatherTimer.Stop()
        TrayIcon.Visible = False
        If trayIconGreen IsNot Nothing Then trayIconGreen.Dispose()
        If trayIconRed IsNot Nothing Then trayIconRed.Dispose()
        If computer IsNot Nothing Then computer.Close()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If isVegasRunning AndAlso vegasStartTime <> DateTime.MinValue Then
            Dim elapsed As TimeSpan = DateTime.Now - vegasStartTime
            Me.Text = $"WatchVegas-2026{WVersion} | Vegas running: {elapsed.Hours}h {elapsed.Minutes}m {elapsed.Seconds}s"
        End If


        xCounter += 1

        If xCounter Mod 5 = 0 Then
            PerformProcessChecks()
        End If
        If xCounter Mod 10 = 0 Then
            CheckForTdrEvents()
        End If

        Debug.WriteLine("Timer Tick - isMonitoring: " &
                isMonitoring &
                " - computer is Nothing: " &
                (computer Is Nothing))

        ' Don't touch computer.Hardware until the background init (computer.Open() and
        ' the performance counters) has actually finished - otherwise the first tick or
        ' two after launch can throw and log a spurious "Hardware scan failed" warning.
        If Not IsReady Then Exit Sub

        If Not isMonitoring Then Exit Sub

        xCounter += 1

        If computer IsNot Nothing Then

            Dim c As Double = 0
            Dim r As Double = 0
            Dim currentGpuLoad As Double = 0
            Dim currentDiskLoad As Double = 0
            Dim foundTemp As Double = -999

            Try

                For Each hardware As IHardware In computer.Hardware

                    Try
                        hardware.Update()
                    Catch ex As Exception
                        LogToScreenAndDisk(
                    "[WARNING] Hardware update failed for " &
                    hardware.HardwareType.ToString() &
                    " (" &
                    hardware.Name &
                    "): " &
                    ex.Message)
                        Continue For
                    End Try

                    For Each sensor As ISensor In hardware.Sensors

                        ' CPU Temperature
                        If sensor.SensorType = SensorType.Temperature Then
                            If (sensor.Name.Contains("Core") OrElse
                            sensor.Name.Contains("Package") OrElse
                            sensor.Name.Contains("Tctl")) AndAlso
                            sensor.Value.HasValue Then
                                foundTemp = sensor.Value.Value
                            End If
                        End If

                        ' GPU Temperature - checked outside Load block so it actually fires
                        If (hardware.HardwareType = HardwareType.GpuNvidia OrElse
                        hardware.HardwareType = HardwareType.GpuAmd OrElse
                        hardware.HardwareType = HardwareType.GpuIntel) AndAlso
                        sensor.SensorType = SensorType.Temperature AndAlso
                        sensor.Value.HasValue Then
                            currentGpuTemp = sensor.Value.Value
                            ' LogToScreenAndDisk($"DEBUG GPU TEMP: {sensor.Name} = {sensor.Value.Value}")
                        End If

                        ' Load sensors
                        If sensor.SensorType = SensorType.Load AndAlso sensor.Value.HasValue Then

                            Select Case hardware.HardwareType

                                Case HardwareType.Cpu
                                    If sensor.Name.Contains("Total", StringComparison.OrdinalIgnoreCase) Then
                                        c = sensor.Value.Value
                                    End If

                                Case HardwareType.Memory
                                    r = sensor.Value.Value

                                Case HardwareType.GpuNvidia,
                                 HardwareType.GpuAmd,
                                 HardwareType.GpuIntel
                                    If sensor.Name.Contains("Core", StringComparison.OrdinalIgnoreCase) OrElse
                                   sensor.Name.Contains("Load", StringComparison.OrdinalIgnoreCase) Then
                                        currentGpuLoad = sensor.Value.Value
                                    End If
                                    If sensor.Name.Contains("Memory", StringComparison.OrdinalIgnoreCase) OrElse
                                   sensor.Name.Contains("VRAM", StringComparison.OrdinalIgnoreCase) Then
                                        currentVramUsage = sensor.Value.Value
                                    End If

                                Case HardwareType.Storage
                                    If sensor.Name.Contains("Activity", StringComparison.OrdinalIgnoreCase) Then
                                        currentDiskLoad = sensor.Value.Value
                                    End If

                            End Select

                        End If

                    Next

                Next

            Catch ex As Exception
                LogToScreenAndDisk(
            "[WARNING] Hardware scan failed: " &
            ex.Message)
            End Try

            ' LibreHardwareMonitor's disk "Activity" sensor isn't reliably exposed on many
            ' drives (NVMe SSDs especially) - fall back to the Windows performance counter,
            ' which works far more consistently across drive types and vendors.
            If currentDiskLoad = 0 AndAlso diskCounter IsNot Nothing Then
                Try
                    currentDiskLoad = diskCounter.NextValue()
                Catch
                End Try
            End If

            ' Update UI
            If foundTemp <> -999 Then
                UpdateCpuTemperature(foundTemp)
            End If

            ' Update VRAM display
            ToolVRAM.Text = "VRAM: " & Math.Round(currentVramUsage) & "%"

            CheckVramUsageAlert(currentVramUsage)

            ' Update Charts
            UpdateChart(Me.ChartCPU, c, "CPU Usage")
            UpdateChart(Me.ChartRAM, r, "RAM Usage")
            UpdateChart(Me.ChartGPU, currentGpuLoad, "GPU Usage")
            UpdateChart(Me.ChartDisk, currentDiskLoad, "Disk Usage")

            ' Combined system-load indicator
            Dim systemLoad As Double = (c * SystemLoadWeightCpu) + (r * SystemLoadWeightRam) + (currentGpuLoad * SystemLoadWeightGpu)
            If systemLoad < 40 Then
                Panel1.BackColor = SystemColors.ActiveCaption
                lblSystemLoad.Text = "System Load: NORMAL (" & CInt(systemLoad) & "%)"
            ElseIf systemLoad < 70 Then
                Panel1.BackColor = Color.Green
                lblSystemLoad.Text = "System Load: MODERATE (" & CInt(systemLoad) & "%)"
            ElseIf systemLoad < 85 Then
                Panel1.BackColor = Color.Orange
                lblSystemLoad.Text = "System Load: HEAVY (" & CInt(systemLoad) & "%)"
            Else
                Panel1.BackColor = Color.Red
                lblSystemLoad.Text = "System Load: CRITICAL (" & CInt(systemLoad) & "%)"
            End If

            ' Store current values
            CurrentCpuValue = c.ToString()
            CurrentVramValue = currentVramUsage.ToString()
            CurrentGpuValue = currentGpuLoad.ToString()
            CurrentDiskValue = currentDiskLoad.ToString()

            ' Save values for heartbeat logging
            lastCpuLoad = c.ToString("0.0")
            lastGpuLoad = currentGpuLoad.ToString("0.0")
            lastVramLoad = currentVramUsage.ToString("0.0")

            ' Available RAM in GB
            Dim availableRamGB As Double = 0
            Try
                Dim perfRam As New PerformanceCounter("Memory", "Available MBytes")
                availableRamGB = Math.Round(perfRam.NextValue() / 1024, 1)
                perfRam.Dispose()
            Catch
            End Try

            If availableRamGB > 0 Then
                If StatusStrip1.InvokeRequired Then
                    StatusStrip1.BeginInvoke(Sub()
                                                 ToolVRAM.Text = $"VRAM: {Math.Round(currentVramUsage)}%  |  RAM Free: {availableRamGB}GB"
                                             End Sub)
                Else
                    ToolVRAM.Text = $"VRAM: {Math.Round(currentVramUsage)}%  |  RAM Free: {availableRamGB}GB  |  GPU: {Math.Round(currentGpuTemp)}°C"
                End If

                If availableRamGB < 2 Then
                    If Not isRamAlerted Then
                        LogSessionWarning($"Available RAM critically low: {availableRamGB}GB", WarningSeverity.Critical)
                        isRamAlerted = True
                    End If
                ElseIf availableRamGB < 4 Then
                    If Not isRamAlerted Then
                        LogSessionWarning($"Available RAM getting low: {availableRamGB}GB", WarningSeverity.Caution)
                        isRamAlerted = True
                    End If
                Else
                    isRamAlerted = False
                End If
            End If

            ' Update tray icon tooltip with live stats
            Dim tooltipText As String = $"CPU:{Math.Round(foundTemp)}°C {lastCpuLoad}% | GPU:{lastGpuLoad}% | VRAM:{Math.Round(currentVramUsage)}%"
            If tooltipText.Length > 63 Then tooltipText = tooltipText.Substring(0, 60) & "..."
            TrayIcon.Text = tooltipText

        End If

    End Sub


    Private Sub BtnCopyErrData_Click(sender As Object, e As EventArgs) Handles BtnCopyErrData.Click
        If Not String.IsNullOrWhiteSpace(RTBlog.Text) Then
            Clipboard.SetText(RTBlog.Text)
            MessageBox.Show("Log data copied to clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub SaveLogToFile()
        ' DEBUG: Check what is actually inside the box
        'MessageBox.Show("RtbLog contains " & RTBlog.Text.Length & " characters.")

        Using sfd As New SaveFileDialog()
            sfd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*"
            sfd.FileName = "Wvegaslog_" & DateTime.Now.ToString("yyyyMMdd") & ".txt"

            If sfd.ShowDialog() = DialogResult.OK Then
                Try
                    ' This saves whatever is currently in RtbLog
                    System.IO.File.WriteAllText(sfd.FileName, RTBlog.Text)
                    MessageBox.Show("File saved. Check the contents of the file.")
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End If
        End Using
    End Sub

    Private Sub LogRunningProcesses()
        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] --- SCANNING ALL BACKGROUND APPS ---" & vbCrLf)

        ' Get all processes, but filter out the super common system stuff
        Dim allProcs = Process.GetProcesses().OrderBy(Function(p) p.ProcessName)

        For Each p In allProcs
            ' Skip the very basic system processes to keep the list clean
            Dim name = p.ProcessName.ToLower()
            If name = "system" Or name = "idle" Or name = "svchost" Or name = "explorer" Then Continue For

            ' Print to log
            RTBlog.SelectionColor = Color.Blue
            RTBlog.AppendText($"[INFO]: Found running app: {p.ProcessName}" & vbCrLf)
        Next
        RTBlog.ScrollToCaret()
    End Sub
    Private Sub TlsSave_Click(sender As Object, e As EventArgs)
        SaveLogToFile()
    End Sub

    Private Sub FXOnly_Click(sender As Object, e As EventArgs)
        ' Toggle your logic variable
        showOnlyPlugins = Not showOnlyPlugins

        ' Sync the button state
        ' FXOnly.Checked = showOnlyPlugins

        ' Run your refresh logic
        RefreshLogView(False)
    End Sub

    Private Sub toolConflicts_Click(sender As Object, e As EventArgs)
        ' Toggle the monitoring state
        isMonitoringConflicts = Not isMonitoringConflicts

        ' Update the button color to show status
        ' Green for active, Gray for inactive
        ' toolConflicts.BackColor = If(isMonitoringConflicts, Color.LightGreen, SystemColors.Control)

        ' Optional: Give the user visual feedback in the log
        RTBlog.AppendText($"[{Date.Now:yyyyMMdd_HHmmss}] [SYSTEM]: Interference Monitor is now {If(isMonitoringConflicts, "ACTIVE", "DISABLED")}" & vbCrLf)
    End Sub
    Public Sub ApplyAppSettings()
        ' 1. Apply Opacity
        Me.Opacity = My.Settings.OpacityValue / 100

        ' 2. Apply Always On Top
        Me.TopMost = My.Settings.AlwaysOnTopSetting

        ' 3. Update Refresh Rate
        If My.Settings.RefreshRate > 0 Then
            Dim newInterval As Integer = My.Settings.RefreshRate * 1000
            Timer1.Interval = newInterval
            dataGatherTimer.Interval = newInterval
        End If
    End Sub
    Private Sub BtnScan_Click(sender As Object, e As EventArgs)
        ' 1. Reset the filter state to ensure the scan shows all modules
        showOnlyPlugins = False

        ' 2. Reset the visual state of the FX Only button to match the "All Modules" state
        'FXOnly.BackColor = SystemColors.Control

        ' 3. Run the scan logic
        LogRunningProcesses()
    End Sub

    Private Sub TlsBorisFX_Click(sender As Object, e As EventArgs)
        Dim localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim target = Path.Combine(localPath, "BorisFX\mocha.log")
        If File.Exists(target) Then
            Process.Start(New ProcessStartInfo(target) With {.UseShellExecute = True})
        Else
            MessageBox.Show("BorisFX log not found.")
        End If
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Try
            Dim about As New frmAbout()
            about.ShowDialog(Me)
        Catch ex As Exception
            ' This will tell us if it's a Designer error, a missing file, or a memory error
            MessageBox.Show("CRITICAL ERROR: " & ex.ToString())
        End Try
    End Sub

    Private Sub TlsCaptureScreen_Click(sender As Object, e As EventArgs)
        ' Create an instance of your screenshot form
        Dim screenshotForm As New frmCaptureOne

        ' Show it as a dialog, centered relative to the main window
        screenshotForm.ShowDialog(Me)
    End Sub

    Private Sub OpenSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenSettingsToolStripMenuItem.Click
        ' Create and show the settings form
        Dim settingsForm As New frmSettings()

        ' Show as modal
        settingsForm.ShowDialog(Me)

        ' Refresh the app state in case settings were changed
        ApplyAppSettings()
    End Sub

    Private Sub SaveDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveDataToolStripMenuItem.Click
        OpenLogFile()
    End Sub

    Private Sub OpenDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenDataToolStripMenuItem.Click
        ' Setup the Open Dialog
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*"
            ofd.Title = "Open Log File"

            ' If the user selects a file and clicks OK
            If ofd.ShowDialog() = DialogResult.OK Then
                Try
                    ' Open the selected file in Notepad
                    System.Diagnostics.Process.Start("notepad.exe", ofd.FileName)
                Catch ex As Exception
                    MessageBox.Show("Error opening file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub





    ' Add a variable at the Class Level if you haven't yet:
    Dim stabilizationCount As Integer = 0

    Private Sub tmrWaitVegas_Tick(sender As Object, e As EventArgs) Handles tmrWaitVegas.Tick
        Dim vegasProcs = Process.GetProcessesByName("vegas2026")

        If vegasProcs.Length > 0 Then
            ' Vegas is running
            If hasVegasInitialized = False Then
                ' Set the visual style to Red and Bold
                RTBlog.SelectionColor = Color.Red
                RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)

                RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] Vegas detected, wait till fully loaded then click Refresh Data button." & vbCrLf)

                ' Reset the font/color back to default
                RTBlog.SelectionColor = RTBlog.ForeColor
                RTBlog.SelectionFont = RTBlog.Font

                hasVegasInitialized = True
            End If
        Else
            ' Vegas is NOT running. 
            ' Only log the closure if we know it WAS running (hasVegasInitialized = True)
            If hasVegasInitialized = True Then
                ' Clear the log for a "fresh state" visual
                RTBlog.Clear()

                ' Set the visual style to Red and Bold
                RTBlog.SelectionColor = Color.Red
                RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)

                RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] Vegas has been closed." & vbCrLf)

                ' Reset the font/color back to default
                RTBlog.SelectionColor = RTBlog.ForeColor
                RTBlog.SelectionFont = RTBlog.Font

                ' Now reset the flag so it's ready to detect a new launch
                hasVegasInitialized = False
            End If
        End If
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        btnRefresh.Enabled = False

        ' 2. Clear the RTBlog to prepare for new input
        RTBlog.Clear()

        ' 3. Re-trigger the scanning logic
        ' This calls the same function used at startup for consistency
        ScanModulesToFile()

        ' 4. Re-enable the button
        btnRefresh.Enabled = True
    End Sub
    Private Function GetLogFileName() As String
        ' Returns: Wvegaslog_20260604.txt
        Return "Wvegaslog_" & DateTime.Now.ToString("yyyyMMdd") & ".txt"
    End Function

    Private Sub mnuCrashHistory_Click(sender As Object, e As EventArgs) Handles mnuCrashHistory.Click
        Try
            Dim historyFile As String = Path.Combine(Application.StartupPath, "Logs", "CrashHistory.txt")
            If File.Exists(historyFile) Then
                Process.Start("notepad.exe", historyFile)
            Else
                MessageBox.Show("No crash history recorded yet.", "Crash History", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to open crash history: " & ex.Message)
        End Try
    End Sub





    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) _
    Handles txtSearch.TextChanged

        Dim searchTerm As String = txtSearch.Text.Trim()

        If searchTerm = "" Then Exit Sub

        Dim index As Integer = RTBlog.Find(searchTerm, 0, RichTextBoxFinds.None)

        If index >= 0 Then
            RTBlog.Select(index, searchTerm.Length)
            RTBlog.ScrollToCaret()
        End If

    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Application.Exit()

    End Sub

    Private Sub PrintLgToPDFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintLgToPDFToolStripMenuItem.Click
        ' 1. Create the data snapshot right now
        GenerateCurrentSnapshot()

        ' 2. Print it
        PrintDialog1.Document = PrintDocument1
        If PrintDialog1.ShowDialog() = DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub




    Private Sub LogHardwareStatsOnly()
        ' Construct the log line using your existing variables
        ' (Ensure these variables are strings like "45%")
        Dim statusMsg As String = $"[{DateTime.Now:yyyyMMdd_HHmmss}] | CPU: {lastCpuLoad} | GPU: {lastGpuLoad} | VRAM: {lastVramLoad}"

        LogToScreenAndDisk(statusMsg)
    End Sub
    Private Sub dataGatherTimer_Tick(sender As Object, e As EventArgs) Handles dataGatherTimer.Tick

        heartbeatCounter += 1

        If heartbeatCounter >= 15 Then

            WriteHeartbeatToDisk()

            heartbeatCounter = 0

        End If

    End Sub

    Private Sub ListRunningProcesses()
        Task.Run(Sub()
                     ' Collect all process names in the background first - no sleep needed
                     Dim allProcesses As Process() = Process.GetProcesses().OrderBy(Function(p) p.ProcessName).ToArray()
                     Dim names As New List(Of String)

                     For Each p As Process In allProcesses
                         Dim name As String = p.ProcessName.ToLower()
                         If name = "system" OrElse name = "idle" OrElse name = "svchost" OrElse name = "explorer" Then Continue For
                         names.Add(p.ProcessName)
                     Next

                     ' Single UI update with the entire batch instead of one invoke per process
                     Me.BeginInvoke(Sub()
                                        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] --- SCANNING ALL BACKGROUND APPS ---" & vbCrLf)
                                        For Each procName In names
                                            RTBlog.SelectionColor = Color.Blue
                                            RTBlog.AppendText($"[INFO]: Found running app: {procName}" & vbCrLf)
                                        Next
                                        RTBlog.SelectionColor = RTBlog.ForeColor
                                        RTBlog.ScrollToCaret()
                                    End Sub)
                 End Sub)
    End Sub

    ' --- ADD THIS TO YOUR CODE ---
    Private Sub LogSystemState()
        ' Just read the variables that your background process is already filling
        ' No "Refresh" or ".NextValue()" here. Just read the memory.

        Dim statusMsg As String = $"[{DateTime.Now:yyyyMMdd_HHmmss}] | CPU: {latestCpu:0}% | GPU: {latestGpu:0}% | VRAM: {currentVramUsage:0}%"

        LogToScreenAndDisk(statusMsg)
    End Sub
    Private Sub RefreshHardwareStats()
        ' This updates your variables with the absolute latest values from the counters
        If cpuCounter IsNot Nothing Then latestCpu = cpuCounter.NextValue()
        If gpuCounter IsNot Nothing Then latestGpu = gpuCounter.NextValue()
        If vramCounter IsNot Nothing Then currentVramUsage = vramCounter.NextValue()
    End Sub
    Private Sub WriteHardwareStatsToLog()
        ' Force the update to happen right now
        Dim logPath As String = GetCurrentLogPath()
        Dim logLine As String = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | CPU: {lastCpuLoad} | GPU: {lastGpuLoad} | VRAM: {lastVramLoad}"

        Try
            ' Ensure we are appending to the same file your other logs use
            System.IO.File.AppendAllText(logPath, logLine & vbCrLf)
        Catch ex As Exception
            ' Optional: Debug.WriteLine("Logging failed")
        End Try
    End Sub
    ' Call this method from your main timer/loop every 3-4 seconds
    Private Sub WriteHeartbeatToDisk()

        Dim heartbeat As String =
        $"[HEARTBEAT {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " &
        $"CPU:{lastCpuLoad} | " &
        $"GPU:{lastGpuLoad} | " &
        $"VRAM:{lastVramLoad} | " &
        $"Vegas:{isVegasRunning} | " &
        $"Temp:{toolCpuTemp.Text}"

        LogToFileOnly(heartbeat)

    End Sub

    Private Sub CaptureCrashSnapshot()

        lastCrashTime =
    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        lastCrashSummary =
    "Crash Time: " & lastCrashTime & vbCrLf &
    "Culprit Module: " & lastCrashCulpritModule & vbCrLf &
    "Was Unresponsive Before Crash: " & If(lastCrashWasHungBeforehand, $"Yes (~{lastCrashHangDurationSeconds}s)", "No") & vbCrLf &
    "CPU: " & lastCpuLoad & "%" & vbCrLf &
    "GPU: " & lastGpuLoad & "%" & vbCrLf &
    "VRAM: " & lastVramLoad & "%" & vbCrLf &
    "Disk: " & lastDiskLoad & vbCrLf &
    "Temperature: " & toolCpuTemp.Text & vbCrLf &
    "Vegas Status: " &
    If(isVegasRunning, "Running", "Closed") & vbCrLf &
    "Last Module: " & lastModuleLoaded & vbCrLf &
    "Modules Loaded: " & lastKnownModules.Count

        LogToScreenAndDisk("Crash snapshot captured.")

        Try

            Dim crashFile As String =
            Path.Combine(Application.StartupPath,
                         "Logs",
                         "LastCrashSnapshot.txt")


            File.WriteAllText(crashFile, lastCrashSummary)

        Catch ex As Exception

            LogToScreenAndDisk(
            "Failed to save crash snapshot: " &
            ex.Message)

        End Try
        ' Append to running crash history - never overwritten, builds up over time
        Try
            Dim historyFile As String = Path.Combine(Application.StartupPath, "Logs", "CrashHistory.txt")
            Dim separator As String = "================================================" & vbCrLf
            File.AppendAllText(historyFile, separator & lastCrashSummary & vbCrLf)
        Catch ex As Exception
            LogToScreenAndDisk("Failed to update crash history: " & ex.Message)
        End Try
    End Sub


    Private Sub LogFilteredNotification(moduleName As String, path As String)
        ' 1. Logic Gate: Identify if it is a plugin we care about
        Dim nameUpper = moduleName.ToUpper()
        Dim isPlugin As Boolean = nameUpper.Contains("BORIS") OrElse
                              nameUpper.Contains("BCC") OrElse
                              nameUpper.Contains("MOCHA") OrElse
                              nameUpper.Contains("NEWBLUE") OrElse
                              nameUpper.Contains("NBX")

        ' If it's NOT a plugin, we do NOT show it in the UI (only log to file)
        If Not isPlugin Then Return

        ' 2. Assign Styling
        Dim col As Color = Color.Black
        If nameUpper.Contains("BORIS") Or nameUpper.Contains("BCC") Then col = Color.DarkOrange
        If nameUpper.Contains("MOCHA") Then col = Color.MediumPurple
        If nameUpper.Contains("NEWBLUE") Then col = Color.LimeGreen

        ' 3. Safe UI Update
        Me.BeginInvoke(Sub()
                           RTBlog.SelectionStart = RTBlog.TextLength
                           RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                           RTBlog.SelectionColor = col
                           RTBlog.AppendText($"[PLUGIN]: {moduleName}" & vbCrLf)
                           RTBlog.SelectionColor = RTBlog.ForeColor ' Reset color
                           RTBlog.ScrollToCaret()
                       End Sub)
    End Sub

    ' This handles the actual drawing of the text onto the page
    'Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs)
    '    ' Simply print the string we generated in memory
    '    Dim printFont As New Font("Consolas", 12)
    '    e.Graphics.DrawString(printableReportContent, printFont, Brushes.Black, e.MarginBounds)

    '    ' Only one page needed for this report
    '    e.HasMorePages = False
    'End Sub

    Private Sub btnCheckLog_Click(sender As Object, e As EventArgs)
        Dim logPath = GetCurrentLogPath()

        If File.Exists(logPath) Then
            Dim logContent = File.ReadAllText(logPath)

            ' Check if the file is emptye 
            If String.IsNullOrWhiteSpace(logContent) Then
                MessageBox.Show("The log file exists but is empty.", "Log Status")
            Else
                MessageBox.Show(logContent, "Current Log Contents")
            End If
        Else
            MessageBox.Show("Log file not found at: " & logPath, "Error")
        End If
    End Sub

    ' --- PRINTING LOGIC ---
    Public Sub TriggerPrint()
        Dim printDialog As New PrintDialog()
        printDialog.Document = printDoc

        ' This opens the system dialog for selecting "Microsoft Print to PDF"
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If
    End Sub



    Public Sub GenerateCurrentSnapshot()

        printableReportContent = ""

        printableReportContent &= "WatchVegas2 Diagnostic Report" & vbCrLf
        printableReportContent &= "Generated: " &
                              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") &
                              vbCrLf

        printableReportContent &= "==================================" & vbCrLf & vbCrLf

        printableReportContent &= "Hardware Snapshot" & vbCrLf
        printableReportContent &= "------------------------" & vbCrLf
        printableReportContent &= "CPU Usage:  " & lastCpuLoad & "%" & vbCrLf
        printableReportContent &= "GPU Usage:  " & lastGpuLoad & "%" & vbCrLf
        printableReportContent &= "VRAM Usage: " & lastVramLoad & "%" & vbCrLf
        printableReportContent &= "Disk Usage: " & lastDiskLoad & "%" & vbCrLf
        printableReportContent &= vbCrLf

        printableReportContent &= "Vegas Status" & vbCrLf
        printableReportContent &= "------------------------" & vbCrLf

        If isVegasRunning Then
            printableReportContent &= "RUNNING" & vbCrLf
        Else
            printableReportContent &= "NOT RUNNING" & vbCrLf
        End If

        printableReportContent &= vbCrLf

        printableReportContent &= "Loaded Modules" & vbCrLf
        printableReportContent &= "------------------------" & vbCrLf

        For Each item As String In fullModuleHistory
            printableReportContent &= item & vbCrLf
        Next

    End Sub



    Private Sub PrintDocument1_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' 1. Define the path (ensure this file exists at this location)
        Dim logoPath As String = Path.Combine(Application.StartupPath, "Assets", "WVLogo.png")

        ' 2. Draw the logo
        If System.IO.File.Exists(logoPath) Then
            Using logo As Image = Image.FromFile(logoPath)
                e.Graphics.DrawImage(logo, 50, 50, 64, 64)
            End Using
        End If

        ' 3. Draw the text below the logo
        e.Graphics.DrawString(printableReportContent, New Font("Arial", 12), Brushes.Black, 50, 130)
    End Sub

    Private Sub btnSD_Click(sender As Object, e As EventArgs) Handles btnSD.Click
        SaveLogToFile()
    End Sub

    Private Sub Btnsfxc_Click(sender As Object, e As EventArgs) Handles Btnsfxc.Click
        ' Toggle your logic variable
        showOnlyPlugins = Not showOnlyPlugins

        ' Sync the button state
        ' FXOnly.Checked = showOnlyPlugins

        ' Run your refresh logic
        RefreshLogView(False)
    End Sub

    Private Sub BtnCF_Click(sender As Object, e As EventArgs) Handles BtnCF.Click
        isMonitoringConflicts = Not isMonitoringConflicts
        If isMonitoringConflicts Then
            BtnCF.ForeColor = Color.Navy
            BtnCF.BackColor = SystemColors.Control
        Else
            BtnCF.ForeColor = Color.Red
            BtnCF.BackColor = SystemColors.Control
        End If
        RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] [SYSTEM]: Interference Monitor is now {If(isMonitoringConflicts, "ACTIVE", "DISABLED")}" & vbCrLf)
    End Sub

    Private Sub BtnMM_Click(sender As Object, e As EventArgs) Handles BtnMM.Click
        ' 1. Reset the filter state to ensure the scan shows all modules
        showOnlyPlugins = False

        ' 2. Reset the visual state of the FX Only button to match the "All Modules" state
        ' FXOnly.BackColor = SystemColors.Control

        ' 3. Run the scan logic
        LogRunningProcesses()
    End Sub

    Private Sub BtnSdata_Click(sender As Object, e As EventArgs) Handles BtnBorisFX.Click
        Dim localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim target = Path.Combine(localPath, "BorisFX\mocha.log")
        If File.Exists(target) Then
            Process.Start(New ProcessStartInfo(target) With {.UseShellExecute = True})
        Else
            MessageBox.Show("BorisFX log not found.")
        End If
    End Sub

    Private Sub BtnCs_Click(sender As Object, e As EventArgs) Handles BtnCs.Click
        ' Create an instance of your screenshot form
        Dim screenshotForm As New frmCaptureOne()

        ' Show it as a dialog, centered relative to the main window
        screenshotForm.ShowDialog(Me)
    End Sub

    Private Sub TsMiRefresh_Click(sender As Object, e As EventArgs) Handles TsMiRefresh.Click
        btnRefresh.PerformClick()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        txtSearch.Text = Nothing
    End Sub

    Private Sub mnuHeartbeatLogging_Click(sender As Object, e As EventArgs) _
    Handles mnuHeartbeatLogging.Click

        My.Settings.HeartbeatEnabled = mnuHeartbeatLogging.Checked
        My.Settings.Save()

        LogToScreenAndDisk(
        $"Heartbeat logging {If(mnuHeartbeatLogging.Checked, "enabled", "disabled")}")

    End Sub
    ' Resting ActiveCaption -> Orange near a warning level -> Red near critical, applied
    ' to the Chart control's own outer BackColor (not the inner yellow plot area).
    Private Function GetChartWarningColor(value As Double, warnAt As Double, dangerAt As Double, normalColor As Color) As Color
        If value >= dangerAt Then Return Color.FromArgb(255, 200, 200) ' very light red
        If value >= warnAt Then Return Color.FromArgb(255, 235, 180) ' very light orange
        Return normalColor
    End Function

    ' VRAM exhaustion is one of the most common real-world causes of editor freezes and
    ' crashes - this gives it the same kind of alert path CPU temp already has, which it
    ' was previously missing despite VRAM already being tracked and displayed.
    Private Sub CheckVramUsageAlert(currentVramPercent As Double)
        If currentVramPercent > My.Settings.VramWarningThreshold Then
            If Not isVramAlerted Then
                RTBlog.SelectionStart = RTBlog.TextLength
                RTBlog.SelectionColor = Color.Red
                RTBlog.AppendText("!!! WARNING: VRAM Usage " & Math.Round(currentVramPercent) & "% exceeds " & My.Settings.VramWarningThreshold & "% - high risk of a GPU-related freeze or crash" & vbCrLf)
                RTBlog.SelectionColor = RTBlog.ForeColor
                Me.WindowState = FormWindowState.Normal
                Me.Activate()
                isVramAlerted = True
                LogSessionWarning($"VRAM Usage {Math.Round(currentVramPercent)}% exceeds {My.Settings.VramWarningThreshold}% - high risk of a GPU-related freeze or crash", WarningSeverity.Caution)
                If My.Settings.AutoScreenshot Then
                    Try
                        Dim localCapture As New frmCaptureOne()
                        localCapture.CaptureScreen()
                        localCapture.SaveBufferedScreenshots()
                    Catch ex As Exception
                        LogToScreenAndDisk("Screenshot failed: " & ex.Message)
                    End Try
                End If
            End If
            SetStatusStripColor(Color.DarkRed)
        Else
            If isVramAlerted Then
                isVramAlerted = False
                If Not IsOverheating Then
                    SetStatusStripColor(SystemColors.Control)
                End If
            End If
        End If
    End Sub

    ' Checks the Windows Application event log for a genuine "Application Error" fault
    ' (Event ID 1000) matching the given process, logged at or after sinceTime. Used to
    ' tell a real crash apart from a normal close - the process disappearing from the
    ' process list alone doesn't tell you which one happened. When a match is found,
    ' culpritModule is set to the faulting module named in the event - often the actual
    ' plugin/codec DLL responsible, not just "vegas2026.exe crashed."
    Private Function WasRealCrash(processNameContains As String, sinceTime As DateTime, ByRef culpritModule As String) As Boolean
        culpritModule = "Unknown"
        Try
            Dim cutoff As String = sinceTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            Dim queryStr As String = "*[System[Provider[@Name='Application Error'] and (EventID=1000) and TimeCreated[@SystemTime>='" & cutoff & "']]]"
            Dim eventQuery As New EventLogQuery("Application", PathType.LogName, queryStr)
            Using reader As New EventLogReader(eventQuery)
                Dim rec As EventRecord = reader.ReadEvent()
                While rec IsNot Nothing
                    Dim msg As String = rec.FormatDescription()
                    If msg IsNot Nothing AndAlso msg.ToUpper().Contains(processNameContains.ToUpper()) Then
                        culpritModule = ExtractCrashField(msg, "Faulting module name:")
                        Return True
                    End If
                    rec = reader.ReadEvent()
                End While
            End Using
        Catch
            ' If the event log can't be queried for some reason, fail safe by assuming
            ' no confirmed crash rather than risk a false alarm
        End Try
        Return False
    End Function

    Private Sub CheckForTdrEvents()
        Try
            Dim cutoff As String = lastTdrCheckTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            Dim queryStr As String = "*[System[Provider[@Name='Display'] and (EventID=4101) and TimeCreated[@SystemTime>='" & cutoff & "']]]"
            Dim eventQuery As New EventLogQuery("System", PathType.LogName, queryStr)
            Using reader As New EventLogReader(eventQuery)
                Dim rec As EventRecord = reader.ReadEvent()
                While rec IsNot Nothing
                    Dim ts As String = If(rec.TimeCreated.HasValue, rec.TimeCreated.Value.ToString("HH:mm:ss"), "?")
                    RTBlog.SelectionStart = RTBlog.TextLength
                    RTBlog.SelectionColor = Color.Red
                    RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
                    RTBlog.AppendText($"[{DateTime.Now:yyyyMMdd_HHmmss}] !!! GPU DRIVER CRASH (TDR) DETECTED at {ts} - display driver stopped responding and recovered !!!" & vbCrLf)
                    RTBlog.SelectionColor = RTBlog.ForeColor
                    RTBlog.SelectionFont = RTBlog.Font
                    RTBlog.ScrollToCaret()
                    LogSessionWarning($"GPU Driver TDR crash detected at {ts}", WarningSeverity.Critical)
                    LogToFileOnly($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] GPU DRIVER TDR CRASH at {ts}")
                    FlagTrayAlert("GPU Driver Crash", "Display driver stopped responding and recovered at " & ts)
                    rec = reader.ReadEvent()
                End While
            End Using
        Catch
            ' Event log may be inaccessible - fail silently
        End Try
        lastTdrCheckTime = DateTime.UtcNow
    End Sub




    ' Pulls a single field (e.g. "Faulting module name: foo.dll, version: ...") out of
    ' the rendered Application Error message and returns just the name portion.
    Private Function ExtractCrashField(msg As String, label As String) As String
        Try
            Dim idx = msg.IndexOf(label)
            If idx >= 0 Then
                Dim startIdx = idx + label.Length
                Dim endIdx = msg.IndexOf(vbLf, startIdx)
                If endIdx < 0 Then endIdx = msg.Length
                Dim raw = msg.Substring(startIdx, endIdx - startIdx).Trim()
                Dim commaIdx = raw.IndexOf(","c)
                If commaIdx > 0 Then Return raw.Substring(0, commaIdx).Trim()
                Return raw
            End If
        Catch
        End Try
        Return "Unknown"
    End Function

    ' Draws a simple filled-circle icon in memory - avoids needing a separate .ico asset
    ' for the tray icon's "watching" (green) and "alert" (red) states.
    Private Function CreateDotIcon(dotColor As Color) As Icon
        Using bmp As New Bitmap(32, 32)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Transparent)
                Using brush As New SolidBrush(dotColor)
                    g.FillEllipse(brush, 4, 4, 24, 24)
                End Using
            End Using
            Return Icon.FromHandle(bmp.GetHicon())
        End Using
    End Function

    ' Flips the tray icon red, pops a balloon notification, and arms the timer that
    ' reverts the icon back to green after a few seconds. Reserved for genuine
    ' crash/hang-close events, not routine resource warnings.
    Private Sub FlagTrayAlert(title As String, message As String)
        Try
            TrayIcon.Icon = trayIconRed
            TrayIcon.ShowBalloonTip(8000, title, message, ToolTipIcon.Warning)
            RevertTrayIconTimer.Enabled = True
        Catch
        End Try
    End Sub

    Private Sub RevertTrayIconTimer_Tick(sender As Object, e As EventArgs) Handles RevertTrayIconTimer.Tick
        TrayIcon.Icon = trayIconGreen
        RevertTrayIconTimer.Enabled = False
    End Sub

    ' Records a warning-level event for the current session, viewable later via
    ' Tools > Session Warnings and copyable to the clipboard. Scoped to genuine warnings
    ' (temp/VRAM thresholds, hangs, interference, crashes) rather than every routine log
    ' line, so it stays a focused "what actually went wrong" summary.
    Private Sub LogSessionWarning(message As String, severity As WarningSeverity)
        Dim entry As New SessionWarningEntry With {
            .Severity = severity,
            .Text = $"[{DateTime.Now:HH:mm:ss}] {message}"
        }
        SyncLock sessionWarningsLock
            sessionWarnings.Add(entry)
        End SyncLock
    End Sub

    Private Sub mnuSessionWarnings_Click(sender As Object, e As EventArgs) Handles mnuSessionWarnings.Click
        ' A short one-shot timer (rather than calling this directly) reliably lets the
        ' menu fully release its mouse capture before the popup opens - otherwise a modal
        ' dialog opened directly from a menu click can occasionally not receive input properly.
        SessionWarningsPopupTimer.Interval = 50
        SessionWarningsPopupTimer.Enabled = True
    End Sub

    Private Sub SessionWarningsPopupTimer_Tick(sender As Object, e As EventArgs) Handles SessionWarningsPopupTimer.Tick
        SessionWarningsPopupTimer.Enabled = False
        ShowSessionWarningsPopup()
    End Sub

    Private Sub ShowSessionWarningsPopup()
        Dim warningsCopy As List(Of SessionWarningEntry)
        SyncLock sessionWarningsLock
            warningsCopy = New List(Of SessionWarningEntry)(sessionWarnings)
        End SyncLock

        Using frm As New frmSessionWarnings(warningsCopy)
            frm.TopMost = True
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub SummaryReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryReportToolStripMenuItem.Click
        RTBlog.Clear()

        RTBlog.SelectionFont = New Font(RTBlog.Font, FontStyle.Bold)
        RTBlog.SelectionColor = Color.DarkBlue
        RTBlog.AppendText("=== Session Summary Report ===" & vbCrLf)
        RTBlog.AppendText("Generated: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & vbCrLf & vbCrLf)

        RTBlog.SelectionColor = Color.Black
        RTBlog.SelectionFont = RTBlog.Font

        RTBlog.AppendText("Hardware Status:" & vbCrLf)
        RTBlog.AppendText("  CPU Load:  " & lastCpuLoad & "%" & vbCrLf)
        RTBlog.AppendText("  GPU Load:  " & lastGpuLoad & "%" & vbCrLf)
        RTBlog.AppendText("  VRAM Load: " & lastVramLoad & "%" & vbCrLf)
        RTBlog.AppendText("  Disk Load: " & lastDiskLoad & "%" & vbCrLf)
        RTBlog.AppendText("  CPU Temp:  " & toolCpuTemp.Text & vbCrLf & vbCrLf)

        RTBlog.AppendText("Vegas Status:" & vbCrLf)
        RTBlog.AppendText("  " & If(isVegasRunning, "RUNNING", "NOT RUNNING") & vbCrLf)
        RTBlog.AppendText("  Version: " & vbCrLf & vbCrLf)

        RTBlog.AppendText("Modules Loaded: " & fullModuleHistory.Count & vbCrLf)
        RTBlog.AppendText("Last Module: " & If(String.IsNullOrEmpty(lastModuleLoaded), "(None)", lastModuleLoaded) & vbCrLf & vbCrLf)

        RTBlog.AppendText("Session Warnings: " & sessionWarnings.Count & vbCrLf)
        If sessionWarnings.Count > 0 Then
            For Each w In sessionWarnings
                RTBlog.SelectionColor = GetSeverityColor(w.Severity)
                RTBlog.AppendText("  " & w.Text & vbCrLf)
            Next
            RTBlog.SelectionColor = RTBlog.ForeColor
        End If

        RTBlog.SelectionFont = RTBlog.Font
        RTBlog.ScrollToCaret()
    End Sub

    Private Function GetSeverityColor(severity As WarningSeverity) As Color
        Select Case severity
            Case WarningSeverity.Info : Return Color.DimGray
            Case WarningSeverity.Caution : Return Color.DarkGoldenrod
            Case WarningSeverity.Severe : Return Color.DarkOrange
            Case WarningSeverity.Critical : Return Color.Red
            Case Else : Return Color.Black
        End Select
    End Function



    Private Sub mnuCrashAnalysis_Click(
    sender As Object,
    e As EventArgs) Handles mnuCrashAnalysis.Click

        Try

            Dim crashFile As String =
            Path.Combine(
                Application.StartupPath,
                "Logs",
                "LastCrashSnapshot.txt")

            If File.Exists(crashFile) Then

                Dim crashData As String =
                File.ReadAllText(crashFile)

                MessageBox.Show(
                crashData,
                "Crash Analysis")

            Else

                MessageBox.Show(
                "No crash data has been captured yet.",
                "Crash Analysis")

            End If

        Catch ex As Exception

            MessageBox.Show(
            "Unable to load crash report:" &
            vbCrLf &
            ex.Message,
            "Crash Analysis")

        End Try

    End Sub
    Private Sub Contents_Click(sender As Object, e As EventArgs) Handles Contents.Click
        Try
            Dim helpForm As New frmHelp()
            helpForm.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show("Unable to open Help: " & ex.Message)
        End Try
    End Sub

    Private Sub TolOpenScreenshotFolder_Click(sender As Object, e As EventArgs) Handles TolOpenScreenshotFolder.Click
        Try
            Dim path As String = GetScreenshotPath()
            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            Process.Start("explorer.exe", path)
        Catch ex As Exception
            MessageBox.Show("Unable to open folder: " & ex.Message)
        End Try
    End Sub

End Class

' Grey = informational/resolved, Golden Yellow = elevated/caution,
' Orange = an active hang in progress, Red = the session was actually lost
' (a confirmed crash or a force-close after a hang).
Public Enum WarningSeverity
    Info
    Caution
    Severe
    Critical
End Enum

Public Class SessionWarningEntry
    Public Property Severity As WarningSeverity
    Public Property Text As String
End Class
