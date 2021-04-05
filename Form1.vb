Imports System.Net
Imports System.IO
Public Class Form1

    Dim Logstore As String = "C:\ABS_Reader\Resourses\"
    Dim filetoupload As String = "Bitacora.xlsx"
    Dim ftpprot As String                   '//Form2.TextBox2
    Dim ftpencript As String
    Dim ftpurl As String = "ftp:\\72.167.3.1/data/"
    Dim ftpusr As String = "miguel"
    Dim ftppass As String = "Bse1234@"


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form2.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' Create a web request that will be used to talk with the server and set the request method to upload a file by ftp.

        '//Dim objIniFile As New IniFile("C:\FTP.ini")
        '//Remote Connection Setup
        '//Dim ftpprot As String = objIniFile.GetString("FTP", "PROTOCOL", "(none)")
        '//Dim ftpencript As String = objIniFile.GetString("FTP", "ENCRIPTION", "(none)")
        '//Dim ftpurl As String = objIniFile.GetString("FTP", "FTP", "(none)")
        '//Dim ftpusr As String = objIniFile.GetString("FTP", "USERNAME", "(none)")
        '//Dim ftppass As String = objIniFile.GetString("FTP", "PASSWORD", "(none)")

        Dim ftpRequest As FtpWebRequest = DirectCast(WebRequest.Create(ftpurl & filetoupload), FtpWebRequest)
        ' Confirm the Network credentials based on the user name and password passed in.
        ftpRequest.Credentials = New NetworkCredential(ftpusr, ftppass)
        Try
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile

            ' Read into a Byte array the contents of the file to be uploaded 
            Dim bytes() As Byte = System.IO.File.ReadAllBytes(Logstore & filetoupload)

            ' Transfer the byte array contents into the request stream, write and then close when done.
            Dim UploadStream As Stream = ftpRequest.GetRequestStream()
            UploadStream.Write(bytes, 0, bytes.Length)
            UploadStream.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Process Complete")
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim objIniFile As New IniFile("C:\FTP.ini")
        '//Remote Connection Setup
        Dim ftpprot As String = objIniFile.GetString("FTP", "PROTOCOL", "(none)")
        Dim ftpencript As String = objIniFile.GetString("FTP", "ENCRIPTION", "(none)")
        Dim ftpurl As String = objIniFile.GetString("FTP", "FTP", "(none)")
        Dim ftpusr As String = objIniFile.GetString("FTP", "USERNAME", "(none)")
        Dim ftppass As String = objIniFile.GetString("FTP", "PASSWORD", "(none)")

        Label3.Text = ftpurl
        Label4.Text = ftpusr
        Label5.Text = ftppass
    End Sub
End Class
