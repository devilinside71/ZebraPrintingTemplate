
Imports System.Xml

Public Class Form1
    Public cimkenames As List(Of String) = New List(Of String)
    Public cimkepar1 As List(Of String) = New List(Of String)
    Public cimkepar2 As List(Of String) = New List(Of String)
    Public cimkepar3 As List(Of String) = New List(Of String)
    Public cimkepar4 As List(Of String) = New List(Of String)
    Public cimkepar5 As List(Of String) = New List(Of String)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Icon = My.Resources.ZebraTL
        Call ZebraPrint.LoadPrinters()
        ComboBoxPrinter.Items.Clear()
        For Each printername In ZebraPrint.PrinterNames
            ComboBoxPrinter.Items.Add(printername)
        Next
        ComboBoxPrinter.SelectedIndex = 0
        Call ZebraPrint.LoadLabels()
        Call LoadData()
    End Sub
    ''' <summary>
    ''' Prints label.
    ''' Replaces variables with parameters
    ''' </summary>
    ''' <param name="replacement_text">replacement of variables</param>
    ''' <param name="labelsample_number">which labelsample</param>
    Sub ZPLPrint(replacement_text As String, labelsample_number As Integer)
        Dim strPrinter As String
        Dim strPrintText As String
        Dim res As String



        strPrinter = ComboBoxPrinter.Text
        Try
            strPrinter = ZebraPrint.PrinterWinNames(ComboBoxPrinter.SelectedIndex)
        Catch ex As Exception

        End Try

        strPrintText = ZebraPrint.LabelCodes(labelsample_number)
        strPrintText = strPrintText.Replace("UTF8STR", replacement_text)
        strPrintText = strPrintText.Replace("UTF8CODE", ZebraPrint.GetZPLutf8Code(replacement_text))

        res = ZebraPrint.SendStringToPrinter(strPrinter, strPrintText)

    End Sub
    ''' <summary>
    ''' Prints label.
    ''' Replaces variables with parameters
    ''' </summary>
    ''' <param name="replacement_text">replacement of variables</param>
    ''' <param name="printer_number">which printer</param>
    ''' <param name="labelsample_number">which labelsample</param>
    Sub ZPLPrint(replacement_text As String, printer_number As Integer, labelsample_number As Integer)
        Dim strPrinter As String
        Dim strPrintText As String
        Dim res As String



        strPrinter = ZebraPrint.PrinterWinNames(printer_number)

        strPrintText = ZebraPrint.LabelCodes(labelsample_number)
        strPrintText = strPrintText.Replace("UTF8STR", replacement_text)
        strPrintText = strPrintText.Replace("UTF8CODE", ZebraPrint.GetZPLutf8Code(replacement_text))

        res = ZebraPrint.SendStringToPrinter(strPrinter, strPrintText)


    End Sub
    Private Sub ButtonPrint_Click(sender As Object, e As EventArgs) Handles ButtonPrint.Click
        Call ZPLPrint("Almásrétes", 0, 0)
    End Sub
    ''' <summary>
    ''' Print from text file.
    ''' Format of the file: name,quantity
    ''' </summary>
    Sub ZPL_PrintBatch()
        Dim strPrinter As String
        Dim strPrintText As String
        Dim FILE_NAME As String = "CimkekBatch.txt"
        Dim strTextLine As String
        Dim strTxtPar1 As String
        Dim strTxtPar2 As String
        Dim strDelimiter As String = ","


        strPrinter = ComboBoxPrinter.Text
        Try
            strPrinter = ZebraPrint.PrinterWinNames(ComboBoxPrinter.SelectedIndex)
        Catch ex As Exception

        End Try

        If System.IO.File.Exists(FILE_NAME) = True Then

            Dim objReader As New System.IO.StreamReader(FILE_NAME)

            Do While objReader.Peek() <> -1
                strTextLine = objReader.ReadLine()
                strTxtPar1 = strTextLine.Split(strDelimiter)(0)
                strTxtPar2 = strTextLine.Split(strDelimiter)(1)
                Debug.Print(strTxtPar1 & ":" & strTxtPar2)
                strPrintText = ZebraPrint.LabelCodes(0)

                Dim strPar1 As String
                strPar1 = GetPar1(strTxtPar1)
                Debug.Print(strPar1)

                If strPar1 <> vbNullString Then

                Else

                End If

            Loop
        Else
            MessageBox.Show("CimkekBatch.txt nem található")
        End If

        MessageBox.Show("KÉSZ!")
    End Sub
    Public Function GetPar1(cimkename As String) As String
        GetPar1 = vbNullString
        For i = 0 To cimkenames.Count - 1
            If cimkenames(i) = cimkename Then
                GetPar1 = cimkepar1(i)
                Exit For
            End If
        Next
    End Function
    Public Function GetPar2(cimkename As String) As String
        GetPar2 = vbNullString
        For i = 0 To cimkenames.Count - 1
            If cimkenames(i) = cimkename Then
                GetPar2 = cimkepar2(i)
                Exit For
            End If
        Next
    End Function
    Public Function GetPar3(cimkename As String) As String
        GetPar3 = vbNullString
        For i = 0 To cimkenames.Count - 1
            If cimkenames(i) = cimkename Then
                GetPar3 = cimkepar3(i)
                Exit For
            End If
        Next
    End Function
    Public Function GetPar4(cimkename As String) As String
        GetPar4 = vbNullString
        For i = 0 To cimkenames.Count - 1
            If cimkenames(i) = cimkename Then
                GetPar4 = cimkepar4(i)
                Exit For
            End If
        Next
    End Function
    Public Function GetPar5(cimkename As String) As String
        GetPar5 = vbNullString
        For i = 0 To cimkenames.Count - 1
            If cimkenames(i) = cimkename Then
                GetPar5 = cimkepar5(i)
                Exit For
            End If
        Next
    End Function
    ''' <summary>
    ''' Load data from Data.xml
    ''' </summary>
    Sub LoadData()
        Dim m_xmlr As XmlTextReader
        'Create the XML Reader
        m_xmlr = New XmlTextReader("Data.xml")
        'Disable whitespace so that you don't have to read over whitespaces
        m_xmlr.WhitespaceHandling = WhitespaceHandling.None
        'read the xml declaration and advance to family tag
        m_xmlr.Read()
        'read the family tag
        m_xmlr.Read()
        'Load the Loop
        While Not m_xmlr.EOF
            'Go to the name tag
            m_xmlr.Read()
            'if not start element exit while loop
            If Not m_xmlr.IsStartElement() Then
                Exit While
            End If
            'Get the Attribute Value
            cimkenames.Add(m_xmlr.GetAttribute("name"))
            'Read elements 
            m_xmlr.Read()
            cimkepar1.Add(m_xmlr.ReadElementString("par1"))
            cimkepar2.Add(m_xmlr.ReadElementString("par2"))
            cimkepar3.Add(m_xmlr.ReadElementString("par3"))
            cimkepar4.Add(m_xmlr.ReadElementString("par4"))
            cimkepar5.Add(m_xmlr.ReadElementString("par5"))
        End While
        'close the reader
        m_xmlr.Close()
    End Sub


End Class
