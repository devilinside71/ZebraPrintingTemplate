
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call ZebraPrint.LoadPrinters()
        ComboBoxPrinter.Items.Clear()
        For Each pr In ZebraPrint.PrinterNames
            ComboBoxPrinter.Items.Add(pr)
        Next
        ComboBoxPrinter.SelectedIndex = 0
        Call ZebraPrint.LoadLabels()
    End Sub
    Sub ZPLPrint()
        Dim strPrinter As String
        Dim strPrintText As String
        Dim res As String

        Dim strTestText = "Almásrétes"

        strPrinter = ZebraPrint.PrinterWinNames(ComboBoxPrinter.SelectedIndex)

        strPrintText = ZebraPrint.LabelCodes(0)
        strPrintText = strPrintText.Replace("UTF8STR", strTestText)
        strPrintText = strPrintText.Replace("UTF8CODE", ZebraPrint.GetZPLutf8Code(strTestText))

        res = ZebraPrint.SendStringToPrinter(strPrinter, strPrintText)


    End Sub

    Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        Call ZPLPrint()
    End Sub
End Class
