Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Collections
Imports System.Net

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolStripStatusLabel2.Text = My.Settings.GW2folder
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
            Catch ex As Exception
                MsgBox("Ordner Konnte nicht Erstellt werden!")
            End Try
        End If
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll", My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll.md5sum", My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        MsgBox("Installation ist Fertig!")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        MsgBox("Deinstallation ist Fertig!")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("https://www.deltaconnected.com/arcdps/")
    End Sub
End Class
