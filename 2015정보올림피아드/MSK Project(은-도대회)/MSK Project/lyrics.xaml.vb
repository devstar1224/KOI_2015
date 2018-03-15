
Imports System.Windows.Interop.WindowInteropHelper
Imports System.Windows.Interop

Public Class lyrics
#Region " Move Form "
    <Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <Runtime.InteropServices.DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2
#End Region
    Dim timer As New System.Windows.Forms.Timer
    Dim savelyrics As String
    Dim s As Integer = 0
    Dim len2 As Integer = 0
    Dim status As New libZPlay.TStreamTime
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim p As New Point(My.Computer.Screen.Bounds.Width - Module1.form1.Left, (My.Computer.Screen.Bounds.Height - Module1.form1.Left) + (My.Computer.Screen.Bounds.Height - Module1.form1.Left) / 2)
        Me.Left = p.X
        Me.Top = p.Y
        AddHandler timer.Tick, AddressOf Timer1_Event
        timer.Enabled = True
        timer.Interval = 1

        If Not (GetSetting("MSKProject", "Setting", "pos") = "") Then
            p = New Point(GetSetting("MSKProject", "Setting", "pos").Split(".")(0), GetSetting("MSKProject", "Setting", "pos").Split(".")(1))

        End If



    End Sub
    Public Sub Timer1_Event(ByVal sender As Object, ByVal e As EventArgs)
        Me.Topmost = True

        If Module1.mode = 1 Then

            lyrics.Items.Clear()
            synctime.Items.Clear()
            outputdata.Text = "가사없음"
            Module1.mode = 0
            savelyrics = ""


        End If

        If Not (Module1.lyrics = "") Then
            lyrics.Items.Clear()
            synctime.Items.Clear()

            savelyrics = Module1.lyrics + vbNewLine + "["
            For i = 1 To UBound(Split(savelyrics, "[")) - 1


                If Not (Split(Split(savelyrics, "[")(i), "]")(0) = "00:00.00") Then

                    synctime.Items.Add(Split(Split(savelyrics, "[")(i), "]")(0))
                    Dim min As Integer
                    Try
                        min = Split(Split(savelyrics, "[")(i), "]")(0).Split(":")(0)
                    Catch ee As Exception
                        Continue For


                    End Try

                    Dim sec As Double = Split(Split(savelyrics, "[")(i), "]")(0).Split(":")(1)
                    Dim result = (min * 60) + (sec)
                    lyrics.Items.Add(result)
                End If



            Next
            lyrics.SelectedItem = 0
            synctime.SelectedIndex = lyrics.SelectedIndex
            Module1.lyrics = ""
        Else
            Module1.zplay.GetPosition(status)
            If Not (lyrics.Items.Count = 0) Then

                Try
                    If ((status.ms / 1000) >= lyrics.SelectedItem) Then
                        lyrics.SelectedIndex += 1
                        s = 0

                    ElseIf s = 0 Then
                        s = 1
                        lyrics.SelectedIndex -= 1
                        Dim str As String = ""


                        Dim save As String = synctime.SelectedItem
                        Dim length As Integer = UBound(Split(savelyrics, synctime.SelectedItem))

                        If length = 1 Then

                            str += Split(Split(savelyrics, "[" + synctime.SelectedItem + "]")(1), vbNewLine)(0)
                            outputdata.Text = str.Split("[")(0)


                            synctime.SelectedIndex = lyrics.SelectedIndex
                            str = ""

                        Else

                            For i = 1 To length


                                str += Split(Split(savelyrics, "[" + synctime.SelectedItem + "]")(i), vbNewLine)(0)
                                If i = 1 Then

                                ElseIf i = 2 Then



                                End If


                            Next
                            outputdata.Text = str.Split("[")(0)


                            synctime.SelectedIndex = lyrics.SelectedIndex
                            str = ""

                        End If
                    End If

                Catch ex As Exception

                    lyrics.SelectedIndex = lyrics.Items.Count - 1

                    If s = 0 Then
                        s = 1
                        Dim str As String = ""


                        Dim save As String = synctime.SelectedItem
                        Dim length As Integer = UBound(Split(savelyrics, synctime.SelectedItem))

                        If length = 1 Then

                            str += Split(Split(savelyrics, "[" + synctime.SelectedItem + "]")(1), vbNewLine)(0)
                            outputdata.Text = str.Split("[")(0)



                            synctime.SelectedIndex = lyrics.SelectedIndex
                            str = ""
                            Exit Sub

                        End If


                        For i = 1 To length

                            str += Split(Split(savelyrics, "[" + synctime.SelectedItem + "]")(i), vbNewLine)(0)


                        Next
                        outputdata.Text = str.Split("[")(0)


                        synctime.SelectedIndex = lyrics.SelectedIndex
                        str = ""
                    End If

                End Try

                If lyrics.Items(lyrics.Items.Count - 1).ToString() <= (status.ms / 1000) Then

                    lyrics.SelectedIndex = lyrics.Items.Count - 1

                    Dim str As String = ""


                    Dim save As String = synctime.SelectedItem
                    Dim length As Integer = UBound(Split(savelyrics, synctime.SelectedItem))


                    For i = 1 To length

                        str += Split(Split(savelyrics, "[" + synctime.SelectedItem + "]")(i), vbNewLine)(0)


                    Next
                    outputdata.Text = str.Split("[")(0)

                    synctime.SelectedIndex = lyrics.SelectedIndex
                    str = ""
                End If

            End If



        End If







    End Sub

    Private Sub Window_MouseMove(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim wih As New WindowInteropHelper(Me)

        Dim hwnd As New IntPtr
        hwnd = wih.Handle

        ReleaseCapture()
        SendMessage(hwnd, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

    Private Sub Window_MouseDown_1(sender As Object, e As MouseButtonEventArgs)

    End Sub
End Class
