Imports System.IO

Public Class Form1
    Dim locat As String = System.Reflection.Assembly.GetEntryAssembly.Location
    Dim MyDirectory As String = System.IO.Path.GetDirectoryName(locat)
    Dim mouseOffset As Point

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LinkLabel1.Text = Me.ProductVersion
        LinkLabel1.Links.Add(0, 32, "https://github.com/sliker2013/Arcdps-Client-Guild-Wars-2/releases")
        If File.Exists(MyDirectory + "\" + Me.ProductName + ".old") Then
            File.Delete(MyDirectory + "\" + Me.ProductName + ".old")
        End If
        TextBox1.Text = My.Settings.GW2folder
        FolderBrowserDialog1.SelectedPath = My.Settings.GW2folder
        If My.Computer.Network.IsAvailable Then
        Else
            MsgBox("Ich finde kein Internet...")
        End If
        PictureBox2_MouseLeave(sender, e) 'Install BTN
        PictureBox4_MouseLeave(sender, e) 'Min BTN
        PictureBox5_MouseLeave(sender, e) 'Close BTN
        PictureBox6_MouseLeave(sender, e) 'Update BTN
        PictureBox7_MouseLeave(sender, e) 'Delete BTN
        PictureBox8_MouseLeave(sender, e) 'Play BTN
    End Sub

#Region "Form Design"
    Private Sub PictureBox3_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.MouseDown
        mouseOffset = New Point(-e.X, -e.Y)
    End Sub
    Private Sub PictureBox3_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox3.MouseMove
        If e.Button = MouseButtons.Left Then
            Dim mousePos = Control.MousePosition
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub

    'MIN BTN Hover
    Private Sub PictureBox4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox4.MouseHover
        PictureBox4.BackgroundImage = New Bitmap(My.Resources.minH)
    End Sub
    'MIN BTN Standart
    Private Sub PictureBox4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox4.MouseLeave
        PictureBox4.BackgroundImage = New Bitmap(My.Resources.min)
    End Sub
    'MIN BTN function
    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox5_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox5.MouseHover
        PictureBox5.BackgroundImage = New Bitmap(My.Resources.closeH)
    End Sub
    'Close BTN Standart
    Private Sub PictureBox5_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox5.MouseLeave
        PictureBox5.BackgroundImage = New Bitmap(My.Resources.close)
    End Sub

    'Close BTN
    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        Application.Exit()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start(e.Link.LinkData.ToString())
    End Sub
#End Region

#Region "Select function"
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            My.Settings.GW2folder = FolderBrowserDialog1.SelectedPath
            My.Settings.Save()
        End If
    End Sub
#End Region

#Region "Install function"
    'BTN Hover
    Private Sub PictureBox2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.BackgroundImage = New Bitmap(My.Resources.installH)
    End Sub
    'BTN Standart
    Private Sub PictureBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.BackgroundImage = New Bitmap(My.Resources.installN)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
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
                My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/sliker2013/Arcdps-Client-Guild-Wars-2/master/Files/arcdps_lang.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
            Catch ex As Exception
                MsgBox("Ordner Konnte nicht Erstellt werden!")
            End Try
        End If
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll", My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll.md5sum", My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/buildtemplates/d3d9_arcdps_buildtemplates.dll", My.Settings.GW2folder & "\bin64\d3d9_arcdps_buildtemplates.dll")
        MsgBox("Installation ist Fertig!")
    End Sub
#End Region

#Region "Delete function"
    'TSPlugin BTN Hover
    Private Sub PictureBox7_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseHover
        PictureBox7.BackgroundImage = New Bitmap(My.Resources.deleteH)
    End Sub
    'TSPlugin BTN Standart
    Private Sub PictureBox7_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox7.MouseLeave
        PictureBox7.BackgroundImage = New Bitmap(My.Resources.deleteN)
    End Sub
    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\bin64\d3d9.dll") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\bin64\d3d9_arcdps_buildtemplates.dll") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9_arcdps_buildtemplates.dll")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
        End If
        MsgBox("Deinstallation ist Fertig!")
    End Sub
#End Region

#Region "Update function"
    'TSJoin BTN Hover
    Private Sub PictureBox6_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseHover
        PictureBox6.BackgroundImage = New Bitmap(My.Resources.updateH)
    End Sub
    'TSJoin BTN Standart
    Private Sub PictureBox6_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox6.MouseLeave
        PictureBox6.BackgroundImage = New Bitmap(My.Resources.updateN)
    End Sub
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\bin64\d3d9.dll") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll")
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum") = True Then
            My.Computer.FileSystem.DeleteFile(My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        End If

        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/arcdps.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps.ini")
        My.Computer.Network.DownloadFile("https://raw.githubusercontent.com/sliker2013/Arcdps-Client-Guild-Wars-2/master/Files/arcdps_lang.ini", My.Settings.GW2folder & "\addons\arcdps\arcdps_lang.ini")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll", My.Settings.GW2folder & "\bin64\d3d9.dll")
        My.Computer.Network.DownloadFile("https://www.deltaconnected.com/arcdps/x64/d3d9.dll.md5sum", My.Settings.GW2folder & "\bin64\d3d9.dll.md5sum")
        MsgBox("ArcDPS is now UptoDate!")
    End Sub
#End Region

#Region "Play function"
    'Play BTN Hover
    Private Sub PictureBox8_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox8.MouseHover
        PictureBox8.BackgroundImage = New Bitmap(My.Resources.playH)
    End Sub
    'Play BTN Standart
    Private Sub PictureBox8_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox8.MouseLeave
        PictureBox8.BackgroundImage = New Bitmap(My.Resources.playN)
    End Sub
    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\Gw2.exe") = True Then
            Process.Start(My.Settings.GW2folder & "\Gw2.exe")
            Application.Exit()
        End If

        If My.Computer.FileSystem.FileExists(My.Settings.GW2folder & "\Gw2-64.exe") = True Then
            Process.Start(My.Settings.GW2folder & "\Gw2-64.exe")
            Application.Exit()
        End If
    End Sub
#End Region

    Private Sub InfoToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Info.Show()
    End Sub
    'To Rework
    Private Sub SucheNachUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Arcdps___Client.Update.Show()
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Process.Start("explorer.exe", My.Settings.GW2folder)
    End Sub
End Class
