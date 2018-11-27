Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.IO
Imports System.Collections
Imports System.Net

Public Class Update
    Dim locat As String = System.Reflection.Assembly.GetEntryAssembly.Location
    Dim MyDirectory As String = System.IO.Path.GetDirectoryName(locat)
    Public totalsize As String
    Public link As String
    Public Csize As String
    Public amount As String

    'Update Check
    Private Sub Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Text = Me.ProductVersion
        Try
            Dim instance As WebClient = New WebClient
            Dim address As String = "http://h2781494.stratoserver.net/arcdps/version.txt"
            Dim returnValue As String
            returnValue = instance.DownloadString(address)
            Label5.Text = returnValue
            If Label5.Text >= Label4.Text Then
            Else
                Button1.Enabled = True
            End If
        Catch ex As Exception

        End Try
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    'UPDATE FUNKUTION Self
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            link = "http://h2781494.stratoserver.net/arcdps/Arcdps-Client.exe"
            Dim size1 As Integer
            Dim wr As WebRequest
            wr = WebRequest.Create(link)
            Dim webr As WebResponse = wr.GetResponse
            size1 = webr.ContentLength
            webr.Close()
            size1 = size1 / 1024
            ProgressBar1.Maximum = size1
            totalsize = size1
            My.Computer.Network.DownloadFile("http://h2781494.stratoserver.net/arcdps/Arcdps-Client.exe", MyDirectory + "\" + Me.ProductName + ".exe")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            Shell(MyDirectory + "\" + Me.ProductName + ".exe")
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If File.Exists(MyDirectory + "\" + Me.ProductName + ".exe") Then
            Dim o As New FileInfo(MyDirectory + "\" + Me.ProductName + ".exe")
            amount = o.Length
            amount = amount / 1024
            Csize = amount
            ProgressBar1.Value = amount
        End If
        Label3.Text = Csize + " KBs / " + totalsize + " KBs"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Computer.FileSystem.RenameFile(MyDirectory + "\" + Me.ProductName + ".exe", Me.ProductName + ".old")
        Timer1.Start()
        BackgroundWorker1.RunWorkerAsync()
        Button1.Enabled = False
    End Sub
End Class