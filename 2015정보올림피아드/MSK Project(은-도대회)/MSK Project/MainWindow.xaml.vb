
Imports Microsoft.Win32
Imports ID3TagLibrary
Imports System.Drawing
Imports System.IO
Imports MSK_Project.libZPlay
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Windows.Interop

Class MainWindow
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

    <DllImport("user32.dll")> _
    Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function

    Private Const VK_LBUTTON = &H1
    Private Const VK_RBUTTON = &H2
    Dim tray_ico As System.Windows.Forms.NotifyIcon

    Dim mlist As New ListBox
    Public streaminfo As New TStreamInfo
    Dim timer As New System.Windows.Forms.Timer
    Dim timer2 As New System.Windows.Forms.Timer
    Dim mstatus As New TStreamTime
    Dim vol, vole As New TStreamTime
    Dim click As Boolean = False
    Dim mm As Boolean = False
    Dim table1 As New System.Data.DataTable("MusicData")
    Dim dimg_m = 1
    Dim nlen As Integer = 14
    Dim slen As Integer = 4
    Dim cs As Integer
    Dim savename As String = ""
    Function caller()
       


        Try
            Module1.mode = 1

            Dim status As Boolean = False
            Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
            Dim str As String
            Dim id3 As New MP3File(mlist.SelectedItem)
            Dim id3v2 As ID3v2Tag = id3.Tag2
            Dim bmp As New System.Windows.Forms.PictureBox
            bmp.Image = id3v2.Artwork(0)
            Try
                If bmp.Image.Flags <= 0 Then

                End If
            Catch en As Exception
                status = True
            End Try
            If status = True Then
                img1.Source = imgrsrc.Source
            Else
                Dim cimg As New ImageSourceConverter
                img1.Source = BitmapToImage(id3v2.Artwork(0))


            End If
            str = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)
            Module1.zplay.GetStreamInfo(streaminfo)
            statuss.Maximum = streaminfo.Length.sec
            Module1.zplay.GetPosition(mstatus)
            timer.Enabled = True
            If InStr(mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 2), "\") Then
                mname.Content = "제목:" + Split(mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 2), "\")(mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 2).ToString().Split("\").Length - 1)
            Else
                mname.Content = "제목:" + mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 2)

            End If
            gname.Content = "Genre:" + id3v2.Genre


            Dim myval As String = "None"
            Dim path As String = mlist.SelectedItem
            Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim reader As New BinaryReader(fs)
            Dim filetag As TagLib.File
            Try
                filetag = TagLib.File.Create(path)
                fs.Seek(filetag.InvariantStartPosition, SeekOrigin.Begin)
                Module1.getlyrics(GetMD5Hash(reader.ReadBytes(163840)))
            Catch ex As Exception

            End Try

            savename = mname.Content
            If click = False Then
                If GetSetting("MSKProject", "Genre", "Rank") = "" Then
                    If Trim(id3v2.Genre) = "" Then
                        SaveSetting("MSKProject", "Genre", "Rank", "/" + "None" + "/")
                        SaveSetting("MSKProject", "Genre", "None", 1)
                    Else
                        SaveSetting("MSKProject", "Genre", "Rank", "/" + id3v2.Genre + "/")
                        SaveSetting("MSKProject", "Genre", id3v2.Genre, 1)
                    End If

                Else

                    If Trim(id3v2.Genre) = "" Then
                        myval = "None"
                    Else

                        myval = id3v2.Genre
                    End If

                    If GetSetting("MSKProject", "Genre", myval) = "" Then

                        SaveSetting("MSKProject", "Genre", myval, 1)
                    Else
                        SaveSetting("MSKProject", "Genre", myval, GetSetting("MSKProject", "Genre", myval) + 1)

                    End If

                    click = False

                End If




                If InStr(GetSetting("MSKProject", "Genre", "Rank"), myval) = 0 Then
                    SaveSetting("MSKProject", "Genre", "Rank", GetSetting("MSKProject", "Genre", "Rank") + myval + "/")
                    SaveSetting("MSKProject", "Genre", myval, 1)


                End If

            Else
                click = False



            End If

        Catch ex As Exception

        End Try


    End Function
    Private Sub tray_MouseDoubleClick()
        Me.Show()
        Me.WindowState = WindowState.Normal
    End Sub

    Protected Sub tray_Changed(e As System.EventArgs)
        Me.Hide()
        MyBase.OnStateChanged(e)
    End Sub
    Sub ntimer_Event(ByValsender As Object, ByVal e As EventArgs)
        If Len(mname.Content) >= 14 Then


            mname.Content = "제목:" + Mid(savename, slen, nlen)
            slen += 1
            nlen += 1
            If Len(savename) < nlen Then
                slen = 4
                nlen = 14
            End If

        End If

    End Sub
    Private Sub MyNotifyIcon_MouseDoubleClick()
        Me.Topmost = True
        Me.Topmost = False

        Me.Show()
        Me.WindowState = WindowState.Normal

    End Sub

    Protected Overrides Sub OnStateChanged(e As System.EventArgs)
        Me.Hide()
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Module1.form1 = Me

        tray_ico = New System.Windows.Forms.NotifyIcon()
        tray_ico.Icon = System.Drawing.SystemIcons.Information
        AddHandler tray_ico.MouseDoubleClick, AddressOf MyNotifyIcon_MouseDoubleClick
        tray_ico.Visible = True
        Me.ShowInTaskbar = False
        ListView1.Items.Clear()
        ListView1.Opacity = 0.5
        AddHandler timer2.Tick, AddressOf ntimer_Event
        timer2.Enabled = True
        timer2.Interval = 300

        AddHandler timer.Tick, AddressOf Timer1_Event
        timer.Enabled = False
        timer.Interval = 1
        Module1.zplay.EnableEqualizer(True)

        Dim getstr = GetSetting("data", "music", "c")

        If getstr = vbNullString Then
            SaveSetting("data", "music", "c", "true")
            table1.Clear()
            table1.Columns.Add("musicn")
            table1.Columns.Add("musicp")
            table1.WriteXml("C:\musicdata.xml")


        Else
            table1.Columns.Add("musicn")
            table1.Columns.Add("musicp")
            Try
                table1.ReadXml("C:\musicdata.xml")
            Catch ex As Exception

            End Try
            For i = 0 To table1.Rows.Count - 1

                ListView1.Items.Add(table1.Rows(i)("musicn"))
                mlist.Items.Add(table1.Rows(i)("musicp"))
            Next

        End If
        dimg_m += 2
        defaultimg.Source = a_loop.Source




    End Sub

    Private Sub ListView_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Dim id3, id3v2
        If ListView1.SelectedIndex = -1 Then


        Else
            Dim status As Boolean = False

            mlist.SelectedIndex = ListView1.SelectedIndex
            Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
            Dim strr As String
            id3 = New MP3File(mlist.SelectedItem)
            id3v2 = id3.Tag2
            Dim bmp As New System.Windows.Forms.PictureBox





            strr = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)


            If strr = "mp3" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
                Module1.zplay.StartPlayback()
            ElseIf strr = "wav" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfWav)
                Module1.zplay.StartPlayback()
            End If
            caller()



        End If


    End Sub


    Private Shared Function GetMD5Hash(data() As Byte) As String
        Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim byteToHash() As Byte = md5Obj.ComputeHash(data)

        Dim strResult As String = String.Empty

        For Each b As Byte In byteToHash
            strResult &= b.ToString("x2")
        Next

        md5Obj.Dispose()
        Return strResult
    End Function
    Public Sub Timer1_Event(ByVal sender As Object, ByVal e As EventArgs)

        Module1.zplay.GetPosition(mstatus)
        statuss.Value = Convert.ToDouble(mstatus.sec)

        Module1.zplay.SetMasterVolume(volumes.Value, volumes.Value)

        If statuss.Value = statuss.Maximum Then


            If dimg_m = 2 Then

                Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
                Module1.zplay.StartPlayback()
                Dim status As Boolean = False

                Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
                Dim strr As String
                Dim id3 As New MP3File(mlist.SelectedItem)
                Dim id3v2 As ID3v2Tag = id3.Tag2
                Dim bmp As New System.Windows.Forms.PictureBox
                bmp.Image = id3v2.Artwork(0)
                Try
                    If bmp.Image.Flags <= 0 Then

                    End If
                Catch en As Exception
                    status = True
                End Try
                If status = True Then
                    img1.Source = imgrsrc.Source
                Else
                    Dim cimg As New ImageSourceConverter
                    img1.Source = BitmapToImage(id3v2.Artwork(0))


                End If
                strr = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)


                If strr = "mp3" Then
                    Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
                    Module1.zplay.StartPlayback()
                    Module1.mode = 1


                ElseIf strr = "wav" Then
                    Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfWav)
                    Module1.zplay.StartPlayback()
                    Module1.mode = 1

                End If
                Module1.zplay.GetStreamInfo(streaminfo)
                statuss.Maximum = streaminfo.Length.sec
                Module1.zplay.GetPosition(mstatus)
                timer.Enabled = True
                mname.Content = "제목:" + ListView1.SelectedItem
                gname.Content = "Genre:" + id3v2.Genre

                Dim path As String = mlist.SelectedItem
                Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Dim reader As New BinaryReader(fs)

                Dim filetag As TagLib.File = TagLib.File.Create(path)
                fs.Seek(filetag.InvariantStartPosition, SeekOrigin.Begin)
                Module1.getlyrics(GetMD5Hash(reader.ReadBytes(163840)))



            End If




        ElseIf dimg_m = 3 Then
            Try

                If (statuss.Value = statuss.Maximum - 1) Then


                    mlist.SelectedIndex = mlist.SelectedIndex + 1

                    Dim Strd As String = mlist.SelectedItem.Split(".")(mlist.SelectedItem.split(".").length - 1)
                    If (statuss.Value = statuss.Maximum - 1) And (mlist.SelectedIndex = mlist.Items.Count - 1) Then

                        mlist.SelectedIndex = 0

                        Dim Str2 As String = mlist.SelectedItem.Split(".")(mlist.SelectedItem.split(".").length - 1)
                        If (statuss.Value = statuss.Maximum - 1) And Str2 = "mp3" Then
                            Module1.lyrics = ""
                            Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
                            Module1.zplay.StartPlayback()

                        ElseIf (statuss.Value = statuss.Maximum - 1) And Strd = "wav" Then
                            Module1.lyrics = ""
                            Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfWav)
                            Module1.zplay.StartPlayback()


                        End If



                        Dim status As Boolean
                        Dim id3v2 As ID3v2Tag

                        Dim id3 As New MP3File(mlist.SelectedItem)
                        id3v2 = id3.Tag2

                        Dim bmp As New System.Windows.Forms.PictureBox
                        bmp.Image = id3v2.Artwork(0)
                        Try
                            If bmp.Image.Flags <= 0 Then

                            End If
                        Catch en As Exception
                            status = True
                        End Try
                        If status = True Then
                            img1.Source = imgrsrc.Source
                        Else
                            Dim cimg As New ImageSourceConverter
                            img1.Source = BitmapToImage(id3v2.Artwork(0))


                        End If

                        Module1.zplay.GetStreamInfo(streaminfo)
                        statuss.Maximum = streaminfo.Length.sec
                    Else
                        If (statuss.Value = statuss.Maximum - 1) And Strd = "mp3" Then

                            Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
                            Dim status As Boolean
                            Dim id3v2 As ID3v2Tag

                            Dim id3 As New MP3File(mlist.SelectedItem)
                            id3v2 = id3.Tag2

                            Dim bmp As New System.Windows.Forms.PictureBox
                            bmp.Image = id3v2.Artwork(0)
                            Try
                                If bmp.Image.Flags <= 0 Then

                                End If
                            Catch en As Exception
                                status = True
                            End Try
                            If status = True Then
                                img1.Source = imgrsrc.Source
                            Else
                                Dim cimg As New ImageSourceConverter
                                img1.Source = BitmapToImage(id3v2.Artwork(0))


                            End If

                            Module1.zplay.GetStreamInfo(streaminfo)
                            statuss.Maximum = streaminfo.Length.sec
                            Module1.zplay.StartPlayback()

                        ElseIf (statuss.Value = statuss.Maximum - 1) And Strd = "wav" Then
                            Dim status As Boolean
                            Dim id3v2 As ID3v2Tag

                            Dim id3 As New MP3File(mlist.SelectedItem)
                            id3v2 = id3.Tag2

                            Dim bmp As New System.Windows.Forms.PictureBox
                            bmp.Image = id3v2.Artwork(0)
                            Try
                                If bmp.Image.Flags <= 0 Then

                                End If
                            Catch en As Exception
                                status = True
                            End Try
                            If status = True Then
                                img1.Source = imgrsrc.Source
                            Else
                                Dim cimg As New ImageSourceConverter
                                img1.Source = BitmapToImage(id3v2.Artwork(0))


                            End If

                            Module1.zplay.GetStreamInfo(streaminfo)
                            statuss.Maximum = streaminfo.Length.sec
                            Module1.zplay.StartPlayback()

                        End If

                    End If
                    caller()
                End If


            Catch ex As Exception
                mlist.SelectedIndex = 0
                Module1.lyrics = ""
                Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfWav)
                Module1.zplay.StartPlayback()
                If ListView1.SelectedIndex = -1 Then


                Else
                    Dim status As Boolean = False
                    Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
                    Dim str As String
                    Dim id3 As New MP3File(mlist.SelectedItem)
                    Dim id3v2 As ID3v2Tag = id3.Tag2
                    Dim bmp As New System.Windows.Forms.PictureBox
                    bmp.Image = id3v2.Artwork(0)
                    Try
                        If bmp.Image.Flags <= 0 Then

                        End If
                    Catch en As Exception
                        status = True
                    End Try
                    If status = True Then
                        img1.Source = imgrsrc.Source
                    Else
                        Dim cimg As New ImageSourceConverter
                        img1.Source = BitmapToImage(id3v2.Artwork(0))


                    End If
                    str = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)
                    Module1.zplay.GetStreamInfo(streaminfo)
                    statuss.Maximum = streaminfo.Length.sec
                    Module1.zplay.GetPosition(mstatus)
                    timer.Enabled = True

                    Dim path As String = mlist.SelectedItem
                    Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Dim reader As New BinaryReader(fs)

                    Dim filetag As TagLib.File = TagLib.File.Create(path)
                    fs.Seek(filetag.InvariantStartPosition, SeekOrigin.Begin)
                    caller()


                End If


            End Try


        End If

    End Sub

    Private Sub ListView1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ListView1.SelectionChanged

    End Sub

    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs)
        ListView1.Opacity = 1


    End Sub

    Private Sub ListView1_MouseLeave(sender As Object, e As MouseEventArgs)
        ListView1.Opacity = 0.5


    End Sub



    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim status As Boolean = False





        Dim fdialog As New OpenFileDialog
        fdialog.Filter = "Music File(*.mp3,*.wav)|*.mp3;*.wav"
        fdialog.Multiselect = True

        fdialog.ShowDialog()

        For i = 0 To fdialog.FileNames.Length - 1

            table1.Rows.Add(fdialog.SafeFileNames(i), fdialog.FileNames(i))

            ListView1.Items.Add(fdialog.SafeFileNames(i))


            mlist.Items.Add(fdialog.FileNames(i))
 

            Module1.zplay.SetMasterVolume(volumes.Value, volumes.Value)







        Next
        table1.WriteXml("C:\musicdata.xml")

    End Sub
    Private Function BitmapToImage(img As System.Drawing.Image) As BitmapImage

        Dim ms As New IO.MemoryStream
        Dim bi As New BitmapImage
        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        bi.BeginInit()
        bi.StreamSource = ms
        bi.EndInit()
        Return bi


    End Function
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        For i = 0 To ListView1.Items.Count - 1

            For a = 0 To ListView1.Items.Count - 1
                If Not (ListView1.SelectedIndex = -1) And (ListView1.SelectedIndex = a) Then
                    ListView1.Items.RemoveAt(a)

                    mlist.Items.RemoveAt(a)
                    table1.Rows(a).Delete()
                    table1.WriteXml("C:\musicdata.xml")
                    Exit For
                End If
            Next
        Next
    End Sub


    Private Sub volumes_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))

    End Sub


    Private Sub statuss_MouseMove(sender As Object, e As MouseEventArgs)
        statuss.CaptureMouse()



    End Sub

    Private Sub statuss_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
    End Sub



    Private Sub statuss_MouseLeave(sender As Object, e As MouseEventArgs)


    End Sub

    Private Sub default_MouseLeave(sender As Object, e As MouseEventArgs)
        defaultimg.Opacity = 0.5
    End Sub

    Private Sub default_MouseMove(sender As Object, e As MouseEventArgs)
        defaultimg.Opacity = 1

    End Sub

    Private Sub defaultimg_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If dimg_m = 2 Then
            dimg_m += 1
            defaultimg.Source = a_loop.Source


        ElseIf dimg_m = 3 Then
            defaultimg.Source = m_loop.Source

            dimg_m = 2

        End If
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        If mlist.SelectedIndex = -1 Then
            mlist.SelectedIndex = ListView1.SelectedIndex

            Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
            Module1.zplay.StartPlayback()
            caller()
        ElseIf Not (ListView1.SelectedIndex = mlist.SelectedIndex) Then
            mlist.SelectedIndex = ListView1.SelectedIndex

            Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
            Module1.zplay.StartPlayback()
            caller()

        Else

            If statuss.Value = 0 Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
                Module1.zplay.StartPlayback()
            Else
                Module1.zplay.ResumePlayback()

            End If
        End If


    End Sub

    Private Sub Button_Click_4(sender As Object, e As RoutedEventArgs)
        Module1.zplay.PausePlayback()

    End Sub

    Private Sub Button_Click_5(sender As Object, e As RoutedEventArgs)
        Dim sec As New TStreamTime
        sec.sec = 0

        Module1.zplay.Seek(TTimeFormat.tfSecond, sec, TSeekMethod.smFromBeginning)
        Module1.zplay.PausePlayback()


    End Sub

    Private Sub statuss_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles statuss.ValueChanged

    End Sub

    Private Sub Window_MouseMove(sender As Object, e As MouseEventArgs)
        statuss.ReleaseMouseCapture()

    End Sub

    Private Sub statuss_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Try

            Dim p As System.Windows.Point
            p = e.GetPosition(statuss)

            Dim sec As New TStreamTime

            sec.sec = (p.X / statuss.Width) * (statuss.Maximum - statuss.Minimum)
            click = True


            caller()


            Module1.zplay.Seek(TTimeFormat.tfSecond, sec, TSeekMethod.smFromBeginning)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        End

    End Sub

    Private Sub Image_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If Not tray_ico.Icon Is Nothing Then
            tray_ico.Icon.Dispose()
        End If
        If Not tray_ico Is Nothing Then
            tray_ico.Dispose()
        End If
        End

    End Sub

    Private Sub Image_MouseDown_1(sender As Object, e As MouseButtonEventArgs)
        Dim wih As New WindowInteropHelper(Me)

        Dim hwnd As New IntPtr
        hwnd = wih.Handle

        ReleaseCapture()
        SendMessage(hwnd, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

    Private Sub Image_MouseMove(sender As Object, e As MouseEventArgs)
        leftb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/오른쪽 넘어가기(활성화).png"))

    End Sub

    Private Sub Image_MouseLeave(sender As Object, e As MouseEventArgs)
        leftb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/오른쪽 넘어가기.png"))
    End Sub

    Private Sub Image_MouseMove_1(sender As Object, e As MouseEventArgs)
        rightb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/왼쪽 넘어가기(활성화).png"))

    End Sub

    Private Sub Image_MouseLeave_1(sender As Object, e As MouseEventArgs)
        rightb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/왼쪽 넘어가기.png"))


    End Sub

    Private Sub Image_MouseMove_2(sender As Object, e As MouseEventArgs)
        stopb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/멈추기(활성화).png"))

    End Sub

    Private Sub Image_MouseLeave_2(sender As Object, e As MouseEventArgs)
        stopb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/멈추기.png"))



    End Sub

    Private Sub Image_MouseMove_3(sender As Object, e As MouseEventArgs)

        exit1.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/Exit(활성화).png"))

    End Sub

    Private Sub Image_MouseLeave_3(sender As Object, e As MouseEventArgs)
        exit1.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/Exit.png"))





    End Sub

    Private Sub Image_MouseMove_4(sender As Object, e As MouseEventArgs)
        waitb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/멈추기2(활성화).png"))


    End Sub

    Private Sub Image_MouseLeave_4(sender As Object, e As MouseEventArgs)
        waitb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/멈추기2.png"))

    End Sub

    Private Sub Image_MouseMove_5(sender As Object, e As MouseEventArgs)
        playb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/시작(활성화).png"))


    End Sub

    Private Sub Image_MouseLeave_5(sender As Object, e As MouseEventArgs)
        playb.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/시작.png"))


    End Sub

    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        Dim eq As New EQSetting
        eq.Show()

    End Sub

    Private Sub Button_Click_6(sender As Object, e As RoutedEventArgs)
        Me.Hide()
        MyBase.OnStateChanged(e)
    End Sub

    Private Sub Button_Click_7(sender As Object, e As RoutedEventArgs)
        Dim grank As New grank
        grank.Show()

    End Sub

    Private Sub rightb_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim st As Boolean = False


        If (mlist.SelectedIndex + 1) = mlist.Items.Count Then

            st = True
            mlist.SelectedIndex = 0


        End If
        If st = False Then
            mlist.SelectedIndex += 1
        Else

            st = False



        End If


        Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
        Module1.zplay.StartPlayback()


        caller()


    End Sub

    Private Sub leftb_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Try


            mlist.SelectedIndex -= 1
            Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
            Module1.zplay.StartPlayback()
            Dim status As Boolean = False

            Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
            Dim strr As String
            Dim id3 As New MP3File(mlist.SelectedItem)
            Dim id3v2 As ID3v2Tag = id3.Tag2
            Dim bmp As New System.Windows.Forms.PictureBox
            bmp.Image = id3v2.Artwork(0)
            Try
                If bmp.Image.Flags <= 0 Then

                End If
            Catch en As Exception
                status = True
            End Try
            If status = True Then
                img1.Source = imgrsrc.Source
            Else
                Dim cimg As New ImageSourceConverter
                img1.Source = BitmapToImage(id3v2.Artwork(0))


            End If
            strr = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)


            If strr = "mp3" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
                Module1.zplay.StartPlayback()
                Module1.mode = 1


            ElseIf strr = "wav" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfWav)
                Module1.zplay.StartPlayback()
                Module1.mode = 1

            End If
            Module1.zplay.GetStreamInfo(streaminfo)
            statuss.Maximum = streaminfo.Length.sec
            Module1.zplay.GetPosition(mstatus)
            timer.Enabled = True
            mname.Content = "제목:" + ListView1.SelectedItem
            gname.Content = "Genre:" + id3v2.Genre

            Dim path As String = mlist.SelectedItem
            Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim reader As New BinaryReader(fs)

            Dim filetag As TagLib.File = TagLib.File.Create(path)
            fs.Seek(filetag.InvariantStartPosition, SeekOrigin.Begin)
            Module1.getlyrics(GetMD5Hash(reader.ReadBytes(163840)))
        Catch ex As Exception
            mlist.SelectedIndex = ListView1.Items.Count - 1

            Module1.zplay.OpenFile(mlist.SelectedItem, TStreamFormat.sfMp3)
            Module1.zplay.StartPlayback()
            Dim status As Boolean = False

            Dim length As Integer = mlist.SelectedItem.Split(".").Length - 1
            Dim strr As String
            Dim id3 As New MP3File(mlist.SelectedItem)
            Dim id3v2 As ID3v2Tag = id3.Tag2
            Dim bmp As New System.Windows.Forms.PictureBox
            bmp.Image = id3v2.Artwork(0)
            Try
                If bmp.Image.Flags <= 0 Then

                End If
            Catch en As Exception
                status = True
            End Try
            If status = True Then
                img1.Source = imgrsrc.Source
            Else
                Dim cimg As New ImageSourceConverter
                img1.Source = BitmapToImage(id3v2.Artwork(0))


            End If
            strr = mlist.SelectedItem.Split(".")(mlist.SelectedItem.Split(".").length - 1)


            If strr = "mp3" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfMp3)
                Module1.zplay.StartPlayback()



            ElseIf strr = "wav" Then
                Module1.zplay.OpenFile(mlist.SelectedItem, libZPlay.TStreamFormat.sfWav)
                Module1.zplay.StartPlayback()


            End If
            Module1.zplay.GetStreamInfo(streaminfo)
            statuss.Maximum = streaminfo.Length.sec
            Module1.zplay.GetPosition(mstatus)
            timer.Enabled = True
            mname.Content = "제목:" + ListView1.SelectedItem
            gname.Content = "Genre:" + id3v2.Genre

            Dim path As String = mlist.SelectedItem
            Dim fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim reader As New BinaryReader(fs)

            Dim filetag As TagLib.File = TagLib.File.Create(path)
            fs.Seek(filetag.InvariantStartPosition, SeekOrigin.Begin)

        End Try
        caller()

    End Sub
End Class
