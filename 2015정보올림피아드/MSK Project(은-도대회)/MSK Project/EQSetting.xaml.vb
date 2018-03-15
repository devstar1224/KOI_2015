Imports System.Threading.Thread
Imports System.Runtime.InteropServices
Imports System.Windows.Interop
Public Class EQSetting
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


    Private Sub eq1_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerPreampGain(20000 - eq1.Value * 1000)
    End Sub

    Private Sub eq2_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(0, 20000 - eq2.Value * 1000)
    End Sub

    Private Sub eq3_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(1, 20000 - eq3.Value * 1000)
    End Sub

    Private Sub eq4_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(2, 20000 - eq4.Value * 1000)
    End Sub

    Private Sub eq5_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(3, 20000 - eq5.Value * 1000)
    End Sub

    Private Sub eq6_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(4, 20000 - eq6.Value * 1000)
    End Sub

    Private Sub eq7_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(5, 20000 - eq7.Value * 1000)
    End Sub

    Private Sub eq8_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(6, 20000 - eq8.Value * 1000)
    End Sub

    Private Sub eq9_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(7, 20000 - eq9.Value * 1000)
    End Sub

    Private Sub eq10_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(8, 20000 - eq10.Value * 1000)
    End Sub

    Private Sub eq11_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        module1.zplay.SetEqualizerBandGain(9, 20000 - eq11.Value * 1000)
    End Sub
    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        module1.zplay.SetEqualizerPreampGain(0)
        For i = 0 To 9
            module1.zplay.SetEqualizerBandGain(i, 0)
        Next
        eq1.Value = 20
        eq2.Value = 20
        eq3.Value = 20
        eq4.Value = 20
        eq5.Value = 20
        eq6.Value = 20
        eq7.Value = 20
        eq8.Value = 20
        eq9.Value = 20
        eq10.Value = 20
        eq11.Value = 20
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim a As New System.Threading.Thread(AddressOf runv)
        a.Start()
        Dim p As New Point(My.Computer.Screen.Bounds.Width - Me.Width - 100, My.Computer.Screen.Bounds.Height - Me.Height)
        Me.Left = p.X

    End Sub
    Public Sub runv()
        Do
            module1.zplay = Module1.zplay
        Loop

    End Sub

    Private Sub Image_MouseMove(sender As Object, e As MouseEventArgs)
        exit1.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/Exit(활성화).png"))
    End Sub

    Private Sub Image_MouseLeave(sender As Object, e As MouseEventArgs)
        exit1.Source = New BitmapImage(New Uri("pack://application:,,,/MSK Project;component/Resource/Exit.png"))
    End Sub

    Private Sub exit1_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.Close()


    End Sub

    Private Sub Image_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim wih As New WindowInteropHelper(Me)

        Dim hwnd As New IntPtr
        hwnd = wih.Handle

        ReleaseCapture()
        SendMessage(hwnd, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub
End Class
