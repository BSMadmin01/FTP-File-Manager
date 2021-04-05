Imports System.Net
Imports System.IO
Public Class Form2


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim request = DirectCast(WebRequest.Create(ComboBox1.SelectedItem & "://" & TextBox1.Text), FtpWebRequest)
        Dim ftpurl As String = ComboBox1.SelectedItem & "://" & TextBox1.Text
        request.Credentials = New NetworkCredential(TextBox2.Text, TextBox3.Text)
        request.Method = WebRequestMethods.Ftp.ListDirectory

        Try
            Using response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
                ' Folder exists here
                MsgBox("Test Succesfull", MsgBoxStyle.Information, "Connection Test Status")

                Dim objIniFile As New IniFile("C:\FTP.ini")
                '//Remote Connection Setup
                objIniFile.WriteString("FTP", "PROTOCOL", ComboBox1.Text)
                objIniFile.WriteString("FTP", "ENCRIPTION", ComboBox2.Text)
                objIniFile.WriteString("FTP", "FTP", TextBox1.Text)
                objIniFile.WriteString("FTP", "USERNAME", TextBox2.Text)
                objIniFile.WriteString("FTP", "PASSWORD", TextBox3.Text)

            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)
            'Does not exist
            If response.StatusCode = FtpStatusCode.NotLoggedIn Then
                MsgBox("Wrong credentials", MsgBoxStyle.Exclamation, "Connection Test Error")
            ElseIf response.StatusCode = FtpStatusCode.Undefined Then
                MsgBox("No FTP server found", MsgBoxStyle.Exclamation, "Connection Test Error")
            End If
        End Try

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If System.IO.File.Exists("C:\FTP.ini") Then

        Else
            Dim ftpfile As FileStream = File.Create("C:\FTP.ini")
        End If

        TextBox1.Text = "0.0.0.0"
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0    ' The first item has index 0 '
        End If

        If ComboBox2.Items.Count > 0 Then
            ComboBox2.SelectedIndex = 0    ' The first item has index 0 '
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class