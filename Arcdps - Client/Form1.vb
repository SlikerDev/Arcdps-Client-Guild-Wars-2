Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Collections
Imports System.Net

Public Class Form1
    Dim locat As String = System.Reflection.Assembly.GetEntryAssembly.Location
    Dim MyDirectory As String = System.IO.Path.GetDirectoryName(locat)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(MyDirectory + "\" + Me.ProductName + ".old") Then
            File.Delete(MyDirectory + "\" + Me.ProductName + ".old")
        End If
        ToolStripStatusLabel2.Text = My.Settings.GW2folder
        FolderBrowserDialog1.SelectedPath = My.Settings.GW2folder
        If My.Computer.Network.IsAvailable Then
        Else
            MsgBox("Ich finde kein Internet...")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            ToolStripStatusLabel2.Text = FolderBrowserDialog1.SelectedPath
            My.Settings.GW2folder = FolderBrowserDialog1.SelectedPath
            My.Settings.Save()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not IO.Directory.Exists(My.Application.Info.DirectoryPath & "\addons") Then
            Try
                IO.Directory.CreateDirectory(My.Settings.GW2folder & "\addons")
            Catch ex As Exception
                MsgBox("Ordner Konnte nicht Erstellt werden!")
            End Try
        End If

        If Not IO.Directory.Exists(My.Application.Info.DirectoryPath & "\addons\arcdps") Then
            Try
                IO.Directory.CreateDirectory(My.Settings.GW2folder & "\addons\arcdps")
                My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/arcdps.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
                My.Computer.Network.DownloadFile("http://h2781494.stratoserver.net/arcdps_lang.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
            Catch ex As Exception
                MsgBox("Ordner Konnte nicht Erstellt werden!")
            End Try
        End If
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll", My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll.md5sum", My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll", My.Settings.GW2folder & "\bin64\d3d9_arcdps_buildtemplates.dll")
        MsgBox("Installation ist Fertig!")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9_arcdps_buildtemplates.dll")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
        MsgBox("Deinstallation ist Fertig!")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Process.Start("https://www.deltaconnected.com/arcdps/")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/arcdps.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll", My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll.md5sum", My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        MsgBox("ArcDPS is now UptoDate!")
    End Sub
    'Info Menü
    Private Sub BugMeldenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BugMeldenToolStripMenuItem.Click
        Process.Start("https://github.com/sliker2013/Arcdps-Client-Guild-Wars-2/issues")
    End Sub

    Private Sub InfoToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem1.Click
        Info.Show()
    End Sub

    Private Sub SucheNachUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SucheNachUpdatesToolStripMenuItem.Click
        Arcdps___Client.Update.Show()
    End Sub
    'Start Menü
    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        Process.Start("https://www.deltaconnected.com/arcdps/")
    End Sub

    Private Sub VersionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionToolStripMenuItem.Click
        Process.Start("https://github.com/sliker2013/Arcdps-Client-Guild-Wars-2")
    End Sub

    Private Sub SchließenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SchließenToolStripMenuItem.Click
        Application.Exit()
    End Sub
End Class
