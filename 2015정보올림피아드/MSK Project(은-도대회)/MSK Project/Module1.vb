Imports System.Net
Imports System.IO
Module Module1
    Public zplay As New libZPlay.ZPlay
    Public lyrics As String
    Public frmobj As New lyrics
    Public mode As Integer = 0
    Public form1 As New MainWindow


    Function getlyrics(ByVal md5)
        Dim cweb As HttpWebRequest
        cweb = WebRequest.Create("http://server.escosoft.kr/tag.php?md5=" + md5)
        cweb.Method = "GET"
        Dim sr As New StreamReader(cweb.GetResponse.GetResponseStream)
        Try
            lyrics = Split(Split(sr.ReadToEnd(), "<strLyric>")(1), "</strLyric>")(0)
        Catch ex As Exception

        End Try


    End Function
End Module
