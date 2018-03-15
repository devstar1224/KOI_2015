
Public Class grank

    Private Sub grank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.Items.Clear()


        Dim l As String = ""
        Dim rc As Integer = Split(GetSetting("MSKProject", "Genre", "Rank"), "/").Length - 2




        Dim rankg As New ArrayList




        For i = 1 To rc


            Dim c As Integer = 0
            For a = 1 To rc



                If Not (a = i) And (GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(a), "/")(0)) > GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(i), "/")(0))) Then
                    c += 1

                End If
            Next


            Me.Left = Module1.form1.Left - 350
            Me.Top = Module1.form1.Top





        Next
        For ll = 1 To rc




            ListBox1.Items.Add(Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0) + ":" + GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))
            Chart1.Series.Add(Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)).Points.AddXY(1, GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))

            rankg.Add(GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))
        Next






    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim rc As Integer = 0
        For i = 1 To 10000
            Try
                If Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(i), "/")(0) Then

                End If
            Catch ex As Exception
                rc = i - 1
                Exit For

            End Try


        Next
        For a = 1 To rc

            SaveSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(a), "/")(0), "")
        Next

        SaveSetting("MSKProject", "Genre", "Rank", "")

        ListBox1.Items.Clear()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Chart1.Series.Clear()

        ListBox1.Items.Clear()


        Dim l As String = ""
        Dim rc As Integer = Split(GetSetting("MSKProject", "Genre", "Rank"), "/").Length - 2




        Dim rankg As New ArrayList




        For i = 1 To rc


            Dim c As Integer = 0
            For a = 1 To rc



                If Not (a = i) And (GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(a), "/")(0)) > GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(i), "/")(0))) Then
                    c += 1

                End If
            Next







        Next
        Dim maxdata As Integer = 0
        For ll = 1 To rc





            ListBox1.Items.Add(Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0) + ":" + GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))
            Chart1.Series.Add(Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)).Points.AddXY(0, GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))

            rankg.Add(GetSetting("MSKProject", "Genre", Split(Split(GetSetting("MSKProject", "Genre", "Rank"), "/")(ll), "/")(0)))
        Next


        Chart1.ChartAreas(0).AxisX.IntervalType = maxdata



    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub
End Class