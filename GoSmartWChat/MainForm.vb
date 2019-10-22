Imports System.Threading
Imports CefSharp
Imports CefSharp.WinForms
Imports Microsoft.Office.Interop
Imports System
Imports System.Threading.Tasks
Imports System.ComponentModel
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Timers
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.Script.Serialization


Public Class MainForm
    Public browser As ChromiumWebBrowser
    Public cache_dir As String
    Public wsCRM As Boolean
    Public myNumber As String
    Public myName As String
    Public ipPublic As String
    Public ipPrivate As String
    Public dbServer As String
    Public dbConnectString As String
    Public version As String

    Private Function GetIPv4Address() As String
        GetIPv4Address = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                GetIPv4Address = ipheal.ToString()
            End If
        Next

    End Function

    Private Function GetExternalIp() As String
        Try
            Dim ExternalIP As String
            ExternalIP = (New System.Net.WebClient()).DownloadString("http://checkip.dyndns.org/")
            ExternalIP = (New System.Text.RegularExpressions.Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")) _
                     .Matches(ExternalIP)(0).ToString()
            Return ExternalIP
        Catch
            Return Nothing
        End Try
    End Function

    Public Function GetImageFromBD(id As Integer) As String

        Dim conn As New MySqlConnection(dbConnectString)
        Dim srcBT As Byte()
        Dim sAns As String
        Dim iPos As String

        Try
            conn.Open()

            Dim sql As String = "SELECT image as Foto, length(image) AS picLen FROM ws_messages_images WHERE id =" + CStr(id)
            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                If (reader("picLen")) > 0 Then

                    Dim byteImage() As Byte = reader("Foto")

                    sAns = Encoding.Default.GetString(byteImage)
                    iPos = InStr(sAns, Chr(0))
                    ' If iPos > 0 Then sAns = Left(sAns, (iPos - 1))
                    reader.Close()
                End If

            End While
            reader.Close()

        Finally
            conn.Close()
        End Try

        Return sAns

    End Function
    Public Function GetImageFromBDToBase64(id As Integer) As String

        Dim conn As New MySqlConnection(dbConnectString)
        Dim srcBT As Byte()

        Try
            conn.Open()

            Dim sql As String = "SELECT image as Foto, length(image) AS picLen FROM ws_messages_images WHERE id =" + CStr(id)
            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                If (reader("picLen")) > 0 Then

                    Dim byteImage() As Byte = reader("Foto")
                    Dim stmFoto As New System.IO.MemoryStream(byteImage)
                    'PictureBox1.Image = Image.FromStream(stmFoto)
                    'PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

                    Dim base64String As String = Convert.ToBase64String(byteImage, 0, byteImage.Length)
                    reader.Close()
                    conn.Close()
                    Return base64String
                Else
                    Return "0"
                End If
            End While
            reader.Close()

        Finally
            conn.Close()
        End Try





    End Function
    Public Sub ReplaceSpecialCharacters()
        '        Dim d
        'Set d = CreateObject("Scripting.Dictionary")
        '    d.Add "Ã¨", "è"
        '    d.Add "Ãˆ", "è"
        '    d.Add "Ã¹", "ù"
        '    d.Add "Ã ", "à"
        '    d.Add "Ã¡", "á"
        '    d.Add "Ã²", "ò"
        '    d.Add "Ã-", "Ö"
        '    d.Add "Ã–", "Ö"
        '    d.Add "Ã„", "Ä"

        '    For Each Key In d
        '            Selection.Replace What:=Key, Replacement:=d(Key), LookAt:=xlPart, SearchOrder _
        '            :=xlByRows, MatchCase:=True, SearchFormat:=False, ReplaceFormat:=False
        '    Next Key

        'Set d = Nothing

    End Sub

    Public Function UnicodeToUTF8(strLine As String) As String

        'Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(strLine)
        'Dim a As String = System.Text.Encoding.UTF8.GetString(bytes)
        'Return a
        Dim part As Byte
        Dim utf8Encoding As New System.Text.UTF8Encoding(True)
        Dim utf8Decoding As New System.Text.UnicodeEncoding
        Dim ASCIIEncoding As New System.Text.ASCIIEncoding

        Dim encodedString() As Byte

        encodedString = utf8Encoding.GetBytes(strLine)
        For Each part In encodedString
            Console.Write("[{0}]", part)
            'Console.Write(b)
        Next

        Return utf8Encoding.GetString(encodedString)

    End Function

    Public Function GetRawBytes(str As String) As Byte()
        Dim charcount As Integer
        Dim byttemp As Byte()
        charcount = str.Length
        ReDim byttemp(charcount)

        For i = 0 To charcount - 1
            Try
                byttemp(i) = Convert.ToByte(str(i))
            Catch ex As Exception

            End Try

        Next
        Return byttemp

    End Function

    Public Function UTF8toUnicode(strLine As String) As String
        Dim bytUTF8 As Byte()
        Dim bytUnicode As Byte()
        Dim strUnicode As String
        strUnicode = String.Empty

        bytUTF8 = GetRawBytes(strLine)
        bytUnicode = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, bytUTF8)
        strUnicode = Encoding.Unicode.GetString(bytUnicode)
        Return strUnicode

    End Function

    Public Sub InitUser()
        Dim task1, task2 As Object
        Dim script, mensaje As String

        Try
            Task.Delay(10000).Wait()
            script = "var yo = window.Store.Contact.get(window.Store.Conn.me); yo.__x_id.user;"
            task1 = browser.EvaluateScriptAsync(script)
            script = "yo.__x_displayName;"
            task2 = browser.EvaluateScriptAsync(script)
            myNumber = task1.Result.Result.ToString()
            myName = task2.Result.Result.ToString()

            If (task1.Result.Result Is Nothing) Then

            Else
                Me.Timer2.Enabled = False
                If (MySettings.Default.StartingMode = 0) Then  'Modo CRM

                    'validar numero en base de datos
                    If (checkLoggednumber(task1.Result.Result.ToString())) Then
                        mensaje = "Bienvenido " + task2.Result.Result.ToString() + ". Tienes el número: " + task1.Result.Result.ToString() + " registrado en la BD " + dbServer
                        MsgBox(mensaje, MsgBoxStyle.Information, "Validar Número")
                        Me.Timer1.Enabled = True
                        Me.Timer1.Interval = 30000
                        wsCRM = True
                    Else
                        mensaje = "Hola " + task2.Result.Result.ToString() + ". El número: " + task1.Result.Result.ToString() + " no se encuentra registrado en el CRM en la BD " + dbServer
                        MsgBox(mensaje, MsgBoxStyle.Critical, "Validar Número")
                    End If
                ElseIf (MySettings.Default.StartingMode = 1) Then   'Modo Campaña
                    mensaje = "Bienvenido " + task2.Result.Result.ToString() + ". Tienes el número: " + task1.Result.Result.ToString()
                    MsgBox(mensaje, MsgBoxStyle.Information, "Validar Número")
                    Me.Timer1.Enabled = True
                    Me.Timer1.Interval = 30000
                End If
            End If
        Catch ex As Exception
            Task.Delay(1000).Wait()

        End Try

    End Sub

    Public Function GetNormalDate(ByVal TimestampToConvert As Long, ByVal Local As Boolean) As DateTime
        Dim mdtIniUnixDate As DateTime
        mdtIniUnixDate = New DateTime(1970, 1, 1, 0, 0, 0)

        If Local Then
            Return mdtIniUnixDate.AddSeconds(TimestampToConvert).ToLocalTime
        Else
            Return mdtIniUnixDate.AddSeconds(TimestampToConvert)
        End If
    End Function

    Public Sub SplitAndStoreUnreadMessages(inMessages As String)
        Dim newDate As DateTime
        Dim number, message, UTCTimestamp, sql, formatedDate, imageBase64, mimetype, body, id_ws_messages, id_user_crm As String
        'Dim con As New MySqlConnection("Server=gosmartcrm.com; Database=gosmart_v3; Uid=root; Pwd=GO(smart)")
        'Dim con As New MySqlConnection("Server=localhost; Database=gosmart_v201; Uid=root; Pwd=")
        Dim con As New MySqlConnection(dbConnectString)

        Try
            ':::Abrimos la conexión
            con.Open()

            Dim NewString As String = inMessages.TrimEnd(";")

            Dim parts As String() = NewString.Split(New Char() {";"c})

            Dim part As String
            For Each part In parts
                Dim subparts As String() = part.Split(New Char() {"|"c})
                Dim subpart As String
                number = subparts(0)
                message = subparts(1)
                mimetype = subparts(4)
                body = subparts(3)

                Dim bytesCodificados As Byte() = System.Text.Encoding.UTF8.GetBytes(message)

                UTCTimestamp = subparts(2)
                newDate = GetNormalDate(UTCTimestamp, 1)

                Dim format As String = "dd/MM/yy HH:mm:ss"
                'Console.WriteLine(newDate.ToString(format))
                formatedDate = newDate.ToString(format)

                sql = "SELECT AUTO_INCREMENT as proximo FROM information_schema.TABLES WHERE TABLE_NAME = 'ws_messages'  AND TABLE_SCHEMA='" + dbServer + "'"
                Dim cmd3 As New MySqlCommand(sql, con)
                Dim reader As MySqlDataReader = cmd3.ExecuteReader()

                While reader.Read()
                    id_ws_messages = reader("proximo")
                End While
                reader.Close()

                sql = "insert into ws_messages (from_number, target_number, message, date, is_send_flag) values('" _
                    + number + "','" + myNumber + "','" + message + "',DATE_FORMAT('" + formatedDate + "','%d/%m/%y %T'),2)"

                'Console.WriteLine(sql)
                Dim cmd As New MySqlCommand(sql, con)
                cmd.ExecuteScalar()
                'MsgBox(id_ws_messages)

                If (mimetype <> "0") Then
                    imageBase64 = "data:" + mimetype + ";base64," + body
                    sql = "insert into ws_messages_images (id_ws_messages, type, image) values('" _
                    + CType(id_ws_messages, String) + "','" + mimetype + "','" + imageBase64 + "')"

                    'Console.WriteLine(sql)
                    Dim cmd2 As New MySqlCommand(sql, con)
                    cmd2.ExecuteScalar()
                End If

                'Hacer el llamado a la Notificación de fireBase de Google
                Dim id_leads, id_contacts As Integer
                Dim from_number, target_number, wsMessage, wsDay, wsMonth, wsHour, token As String

                sql = "SELECT wsm.id AS id_ws, wsm.id_related as idUser, wsm.type_related as userType, wsm.id_user_crm as id_user_crm, wsm.message, 
                    day(wsm.date) as day, monthname(wsm.date) as month, year(wsm.date) as year, time(wsm.date) as hour, uc.name, target_number, from_number, 
                    CASE 
                    WHEN wsmimg.image is NULL THEN ''
                    ELSE wsmimg.image
                    END as attachedImage,
                    CASE 
                    WHEN wsmimg.type is NULL THEN ''
                    ELSE wsmimg.type
                    END as imgType
                    FROM ws_messages wsm
                    LEFT JOIN user_crm uc ON wsm.id_user_crm = uc.id 
                    LEFT JOIN ws_messages_images wsmimg ON wsm.id = wsmimg.id_ws_messages
                    WHERE wsm.id = " + id_ws_messages

                Dim cmd4 As New MySqlCommand(sql, con)
                'cmd.Parameters.AddWithValue("@cod", Convert.ToInt32(TextBox1.Text))
                Dim reader2 As MySqlDataReader = cmd4.ExecuteReader()
                Dim tsHour As TimeSpan

                While reader2.Read()
                    If CType(reader2("userType"), Integer) = 1 Then
                        id_leads = CType(reader2("idUser"), Integer)
                        id_contacts = 0
                    Else
                        id_contacts = CType(reader2("idUser"), Integer)
                        id_leads = 0
                    End If

                    from_number = CType(reader2("from_number"), String)
                    target_number = CType(reader2("target_number"), String)

                    wsMessage = CType(reader2("message"), String)
                    wsMessage = wsMessage.Replace(vbNullChar, "")
                    wsMessage = wsMessage.Replace(vbLf, "\n")
                    wsMessage = wsMessage.Replace(vbCr, "\n")

                    wsDay = CType(reader2("day"), String)
                    wsMonth = CType(reader2("month"), String)
                    tsHour = reader2("hour")
                    wsHour = tsHour.ToString("hh\:mm\:ss")
                    id_user_crm = CType(reader2("id_user_crm"), String)

                    'Validar si hay imagen asociada al mensaje
                    'If (Not DBNull.Value.Equals(reader2("imgType"))) Then
                    '    imgType = CType(reader2("imgType"), String)
                    'Else
                    '    imgType = ""
                    'End If

                    'If imgType <> "" Then

                    'Else

                    'End If

                    SendNotificationFromFirebaseCloud(id_user_crm, id_ws_messages, CType(id_leads, String),
                                                      CType(id_contacts, String), wsMessage, target_number, wsDay, wsMonth, wsHour, from_number)

                End While
                reader2.Close()

                'Console.WriteLine("id:" + CType(id_ws_messages, String) + " id_leads:" + CType(id_leads, String) +
                '                  " id_contacts:" + CType(id_contacts, String) + " message:" + wsMessage + " token:" +
                '                  token + " target_number:" + target_number + " Day:" + wsDay + " Month:" + wsMonth + " Hour:" + wsHour)


            Next

        Catch ex As Exception
            ':::Si no se conecta nos mostrara el posible fallo en la conexión
            'MsgBox("Fail at SplitAndStoreUnreadMessages: " & ex.Message)
        Finally
            con.Close()

        End Try
    End Sub

    Public Sub getUnreadMessages()
        Dim task As Object
        Dim script, messages As String

        script = "var noleido = window.WAPI.getUnreadMessagesOld();
                var agrupado = """";
                if(noleido.length > 0) {
                    for(var i=0; i<noleido.length; i++){
                        if(noleido[i].isGroup == false){
                            for(var j=0; j<noleido[i].messages.length; j++){
                                if(noleido[i].messages[j].type==""chat"" && noleido[i].messages[j].body != ""undefined""){
                                    agrupado += noleido[i].id.user + ""|"" + noleido[i].messages[j].body + ""|"" + noleido[i].messages[j].timestamp  + ""|"" + ""0"" + ""|"" + ""0"" + "";""; 
                                } else if(noleido[i].messages[j].type==""image"" && noleido[i].messages[j].body != ""undefined""){
                                    agrupado += noleido[i].id.user + ""|"" + noleido[i].messages[j].caption + ""|"" + noleido[i].messages[j].timestamp + ""|"" + noleido[i].messages[j].content + ""|"" + noleido[i].messages[j].mimetype + "";""; 
                                }
                            }
                        }
                    }
                };
                agrupado;"

        task = browser.EvaluateScriptAsync(script)
        'MsgBox(task.Result.Result.ToString())
        Console.WriteLine(task.Result.Result.ToString())

        messages = task.Result.Result.ToString()
        If messages <> "" Then
            SplitAndStoreUnreadMessages(messages)
        End If
    End Sub
    Public Sub checkWSMessages()
        ':::Nuestro objeto MySqlConnection con la cadena de conexión y la ruta de la base
        Dim conParameters As String

        'Dim con As New MySqlConnection("Server=gosmartcrm.com; Database=gosmart_v3; Uid=root; Pwd=GO(smart)")
        'Dim con2 As New MySqlConnection("Server=gosmartcrm.com; Database=gosmart_v3; Uid=root; Pwd=GO(smart)")
        'Dim con As New MySqlConnection("Server=localhost; Database=gosmart_v201; Uid=root; Pwd=")
        'Dim con2 As New MySqlConnection("Server=localhost; Database=gosmart_v201; Uid=root; Pwd=")

        Dim con As New MySqlConnection(dbConnectString)
        Dim con2 As New MySqlConnection(dbConnectString)

        Dim mess, sql, script, wsNumber, wsMessage, imgType As String
        Dim DelayForTextMessage, DelayForIMageMessage As Integer

        ':::Utilizamos el try para capturar posibles errores
        Try
            ':::Abrimos la conexión
            con.Open()
            con2.Open()
            ':::Si se estableció conexión correctamente dirá "Conectado"
            'MsgBox("Conectado")
            sql = "SELECT wm.message, wm.target_number, wm.id, wi.type imgType, wi.image wsImage FROM ws_messages wm LEFT JOIN 
                ws_messages_images wi ON wm.id = wi.id_ws_messages WHERE is_send_flag=0 AND from_number='" + myNumber + "'"

            Dim cmd As New MySqlCommand(sql, con)
            'cmd.Parameters.AddWithValue("@cod", Convert.ToInt32(TextBox1.Text))
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                mess = "Enviar: " + CType(reader("message"), String) + " al telefono: " + CType(reader("target_number"), String)
                Console.WriteLine(mess)

                wsNumber = CType(reader("target_number"), String)
                wsMessage = CType(reader("message"), String)
                'wsMessage = UTF8toUnicode(wsMessage)
                wsMessage = wsMessage.Replace(vbNullChar, "")
                wsMessage = wsMessage.Replace(vbLf, "\n")
                wsMessage = wsMessage.Replace(vbCr, "\n")

                'Validar si hay imagen asociada al mensaje
                If (Not DBNull.Value.Equals(reader("imgType"))) Then
                    imgType = CType(reader("imgType"), String)
                Else
                    imgType = ""
                End If
                If imgType <> "" Then
                    Dim byteImage() As Byte = reader("wsImage")
                    Dim sAns As String
                    sAns = Encoding.Default.GetString(byteImage)
                    DelayForIMageMessage = MySettings.Default.DelayForImageMessage * 1000
                    SendImageMessageToID(wsNumber, wsMessage, sAns)
                    Task.Delay(DelayForIMageMessage).Wait()
                Else
                    DelayForTextMessage = MySettings.Default.DelayForTextMessage * 1000
                    SendTxtMessageToID(wsNumber, wsMessage)
                    Task.Delay(DelayForTextMessage).Wait()
                End If

                ' ahora el update de mensaje enviado
                sql = "update ws_messages set is_send_flag=1 where id=" + CType(reader("id"), String)
                Dim cmd2 As New MySqlCommand(sql, con2)
                cmd2.ExecuteScalar()
            End While
            reader.Close()

        Catch ex As Exception
            ':::Si no se conecta nos mostrara el posible fallo en la conexión
            'MsgBox("Fail at checkWSMessages: " & ex.Message)
        Finally
            con2.Close()
            con.Close()
        End Try

    End Sub
    Public Function checkLoggednumber(loggedNumber As String) As Boolean
        Dim count As Int32
        Dim sql As String
        'Dim con As New MySqlConnection("Server=gosmartcrm.com; Database=gosmart_v3; Uid=root; Pwd=GO(smart)")
        'Dim con As New MySqlConnection("Server=localhost; Database=gosmart_v201; Uid=root; Pwd=")
        Dim con As New MySqlConnection(dbConnectString)

        ':::Utilizamos el try para capturar posibles errores
        Try
            ':::Abrimos la conexión
            con.Open()
            ':::Si se estableció conexión correctamente dirá "Conectado"
            'MsgBox("Conectado")
            sql = "SELECT count(*) FROM provides_number WHERE number_phone like '%" + loggedNumber + "'"

            Dim cmd As New MySqlCommand(sql, con)
            'cmd.Parameters.AddWithValue("@cod", Convert.ToInt32(TextBox1.Text))

            count = cmd.ExecuteScalar()

        Catch ex As Exception
            ':::Si no se conecta nos mostrara el posible fallo en la conexión
            'MsgBox("Fail at checkLoggednumber: " & ex.Message)
            Return False
        Finally
            con.Close()
        End Try

        If (count > 0) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function ImageToBase64(srcFile As String) As String
        Dim destBase64 As String
        Dim srcBT As Byte()

        Dim sr As New IO.FileStream(srcFile, IO.FileMode.Open)

        ReDim srcBT(sr.Length)
        sr.Read(srcBT, 0, sr.Length)
        sr.Close()

        destBase64 = "data:image/png;base64," + System.Convert.ToBase64String(srcBT)
        Return destBase64

    End Function

    Public Function SendImageMessage(number As String, message As String, file As String) As Boolean
        Dim salida, script As String

        salida = ImageToBase64(file)

        script = "window.send_media(""" + number + "@c.us"",""" + salida + """,""" + message + """,null,null);"
        'MsgBox(script)

        browser.ExecuteScriptAsync(script)

        Return True
    End Function
    Public Function SendImageMessageToID(number As String, message As String, file As String) As Boolean
        Dim salida, script As String

        'salida = ImageToBase64(file)
        salida = file
        script = "window.WAPI.sendImage('" + salida + "','" + number + "@c.us','File','" + message + "');"
        'MsgBox(script)

        browser.ExecuteScriptAsync(script)

        Return True
    End Function

    Public Function SendTxtMessage(number As String, message As String) As Boolean
        Dim script As String

        script = "Store.Chat.find(""" + number + "@c.us"").then(function(chat) {
    chat.markComposing();
    chat.sendMessage(""" + message + """);
});"
        browser.ExecuteScriptAsync(script)

        Return True
    End Function
    Public Function SendTxtMessageToID(number As String, message As String) As Boolean
        Dim script As String

        script = "window.WAPI.sendMessageToID(""" + number + "@c.us"",""" + message + """);"
        'MsgBox(script)

        browser.ExecuteScriptAsync(script)

        Return True
    End Function

    Public Function SendUsingExcelFile(XLSFile As String, interval As Integer, flagSimpleImage As Integer) As Boolean
        Dim objApps As Excel.Application
        Dim objBooks As Excel.Workbook
        Dim objSheets As Excel.Sheets
        Dim objSheet As Excel._Worksheet
        Dim range As Excel.Range
        Dim DelayForTextMessage, DelayForImageMessage, iRows, iCols, rowCount, colCount As Integer
        Dim chatNumber, chatMessage, chatImage As String

        objApps = New Excel.Application

        'objBooks = objApps.Workbooks.Open("C:\VS.NET\CellNum.xlsx")
        objBooks = objApps.Workbooks.Open(XLSFile)
        objSheets = objBooks.Worksheets
        objSheet = CType(objSheets(1), Excel._Worksheet)
        range = objSheet.UsedRange

        'Retrieve the data from the range.
        Dim saRet(,) As Object
        saRet = CType(range.Value, Object(,))

        rowCount = range.Rows.Count
        colCount = range.Columns.Count
        If (colCount = 2 And flagSimpleImage <> 1) Or (colCount = 3 And flagSimpleImage <> 2) Then
            Return False
        End If

        iRows = saRet.GetUpperBound(0)
        iCols = saRet.GetUpperBound(1)

        DelayForTextMessage = MySettings.Default.DelayForTextMessage * 1000
        DelayForImageMessage = MySettings.Default.DelayForImageMessage * 1000

        Dim rowCounter As Integer

        For rowCounter = 1 To iRows
            chatNumber = saRet(rowCounter, 1).ToString()
            chatMessage = saRet(rowCounter, 2).ToString()

            If (colCount = 2) Then
                'SendTxtMessage(chatNumber, chatMessage)
                SendTxtMessageToID(chatNumber, chatMessage)
                Task.Delay(DelayForTextMessage).Wait()
                ' MsgBox(chatNumber + "---" + chatMessage)
            ElseIf (colCount = 3) Then
                chatImage = saRet(rowCounter, 3).ToString()
                ' SendImageMessage(chatNumber, chatMessage, chatImage)
                SendImageMessageToID(chatNumber, chatMessage, chatImage)
                'MsgBox(chatNumber + "---" + chatMessage + "---" + chatImage)
                Task.Delay(DelayForImageMessage).Wait()
            End If
        Next rowCounter

        'Clean up a little.
        range = Nothing
        objSheet = Nothing
        objSheets = Nothing
        objBooks.Close()
        objApps.Quit()


        Return True
    End Function

    Public Sub initWebPackJSON2()
        Dim script As String

        script = "/**
 * This script contains WAPI functions that need to be run in the context of the webpage
 */

/**
 * Auto discovery the webpack object references of instances that contains all functions used by the WAPI
 * functions and creates the Store object.
 */
if (!window.Store) {
    (function () {
        function getStore(modules) {
            let foundCount = 0;
            let neededObjects = [
                { id: ""Store"", conditions: (module) => (module.Chat && module.Msg) ? module : null },
                { id: ""MediaCollection"", conditions: (module) => (module.default && module.default.prototype && module.default.prototype.processFiles !== undefined) ? module.default : null },
                { id: ""ChatClass"", conditions: (module) => (module.default && module.default.prototype && module.default.prototype.Collection !== undefined && module.default.prototype.Collection === ""Chat"") ? module : null },
                { id: ""MediaProcess"", conditions: (module) => (module.BLOB) ? module : null },
                { id: ""Wap"", conditions: (module) => (module.createGroup) ? module : null },
                { id: ""ServiceWorker"", conditions: (module) => (module.default && module.default.killServiceWorker) ? module : null },
                { id: ""State"", conditions: (module) => (module.STATE && module.STREAM) ? module : null },
                { id: ""WapDelete"", conditions: (module) => (module.sendConversationDelete && module.sendConversationDelete.length == 2) ? module : null },
                { id: ""Conn"", conditions: (module) => (module.default && module.default.ref && module.default.refTTL) ? module.default : null },
                { id: ""WapQuery"", conditions: (module) => (module.queryExist) ? module : ((module.default && module.default.queryExist) ? module.default : null) },
                { id: ""CryptoLib"", conditions: (module) => (module.decryptE2EMedia) ? module : null },
                { id: ""OpenChat"", conditions: (module) => (module.default && module.default.prototype && module.default.prototype.openChat) ? module.default : null },
                { id: ""UserConstructor"", conditions: (module) => (module.default && module.default.prototype && module.default.prototype.isServer && module.default.prototype.isUser) ? module.default : null },
                { id: ""SendTextMsgToChat"", conditions: (module) => (module.sendTextMsgToChat) ? module.sendTextMsgToChat : null },
                { id: ""SendSeen"", conditions: (module) => (module.sendSeen) ? module.sendSeen : null },
                { id: ""sendDelete"", conditions: (module) => (module.sendDelete) ? module.sendDelete : null }
            ];
            for (let idx in modules) {
                if ((typeof modules[idx] === ""object"") && (modules[idx] !== null)) {
                    let first = Object.values(modules[idx])[0];
                    if ((typeof first === ""object"") && (first.exports)) {
                        for (let idx2 in modules[idx]) {
                            let module = modules(idx2);
                            if (!module) {
                                continue;
                            }
                            neededObjects.forEach((needObj) => {
                                if (!needObj.conditions || needObj.foundedModule)
                                    return;
                                let neededModule = needObj.conditions(module);
                                if (neededModule !== null) {
                                    foundCount++;
                                    needObj.foundedModule = neededModule;
                                }
                            });
                            if (foundCount == neededObjects.length) {
                                break;
                            }
                        }

                        let neededStore = neededObjects.find((needObj) => needObj.id === ""Store"");
                        window.Store = neededStore.foundedModule ? neededStore.foundedModule : {};
                        neededObjects.splice(neededObjects.indexOf(neededStore), 1);
                        neededObjects.forEach((needObj) => {
                            if (needObj.foundedModule) {
                                window.Store[needObj.id] = needObj.foundedModule;
                            }
                        });
                        window.Store.ChatClass.default.prototype.sendMessage = function (e) {
                            return window.Store.SendTextMsgToChat(this, ...arguments);
                        }
                        return window.Store;
                    }
                }
            }
        }

        webpackJsonp([], { 'parasite': (x, y, z) => getStore(z) }, ['parasite']);
    })();
}

window.WAPI = {
    lastRead: {}
};

window.WAPI._serializeRawObj = (obj) => {
    if (obj) {
        return obj.toJSON();
    }
    return {}
};

/**
 * Serializes a chat object
 *
 * @param rawChat Chat object
 * @returns {{}}
 */

window.WAPI._serializeChatObj = (obj) => {
    if (obj == undefined) {
        return null;
    }

    return Object.assign(window.WAPI._serializeRawObj(obj), {
        kind         : obj.kind,
        isGroup      : obj.isGroup,
        contact      : obj['contact'] ? window.WAPI._serializeContactObj(obj['contact'])        : null,
        groupMetadata: obj[""groupMetadata""] ? window.WAPI._serializeRawObj(obj[""groupMetadata""]): null,
        presence     : obj[""presence""] ? window.WAPI._serializeRawObj(obj[""presence""])          : null,
        msgs         : null
    });
};

window.WAPI._serializeContactObj = (obj) => {
    if (obj == undefined) {
        return null;
    }

    return Object.assign(window.WAPI._serializeRawObj(obj), {
        formattedName      : obj.formattedName,
        isHighLevelVerified: obj.isHighLevelVerified,
        isMe               : obj.isMe,
        isMyContact        : obj.isMyContact,
        isPSA              : obj.isPSA,
        isUser             : obj.isUser,
        isVerified         : obj.isVerified,
        isWAContact        : obj.isWAContact,
        profilePicThumbObj : obj.profilePicThumb ? WAPI._serializeProfilePicThumb(obj.profilePicThumb): {},
        statusMute         : obj.statusMute,
        msgs               : null
    });
};

window.WAPI._serializeMessageObj = (obj) => {
    if (obj == undefined) {
        return null;
    }

    return Object.assign(window.WAPI._serializeRawObj(obj), {
        id            : obj.id._serialized,
        sender        : obj[""senderObj""] ? WAPI._serializeContactObj(obj[""senderObj""]): null,
        timestamp     : obj[""t""],
        content       : obj[""body""],
        isGroupMsg    : obj.isGroupMsg,
        isLink        : obj.isLink,
        isMMS         : obj.isMMS,
        isMedia       : obj.isMedia,
        isNotification: obj.isNotification,
        isPSA         : obj.isPSA,
        type          : obj.type,
        chat          : WAPI._serializeChatObj(obj['chat']),
        chatId        : obj.id.remote,
        quotedMsgObj  : WAPI._serializeMessageObj(obj['_quotedMsgObj']),
        mediaData     : window.WAPI._serializeRawObj(obj['mediaData'])
    });
};

window.WAPI._serializeNumberStatusObj = (obj) => {
    if (obj == undefined) {
        return null;
    }

    return Object.assign({}, {
        id               : obj.jid,
        status           : obj.status,
        isBusiness       : (obj.biz === true),
        canReceiveMessage: (obj.status === 200)
    });
};

window.WAPI._serializeProfilePicThumb = (obj) => {
    if (obj == undefined) {
        return null;
    }

    return Object.assign({}, {
        eurl   : obj.eurl,
        id     : obj.id,
        img    : obj.img,
        imgFull: obj.imgFull,
        raw    : obj.raw,
        tag    : obj.tag
    });
}

window.WAPI.createGroup = function (name, contactsId) {
    if (!Array.isArray(contactsId)) {
        contactsId = [contactsId];
    }

    return window.Store.Wap.createGroup(name, contactsId);
};

window.WAPI.leaveGroup = function (groupId) {
    groupId = typeof groupId == ""string"" ? groupId : groupId._serialized;
    var group = WAPI.getChat(groupId);
    return group.sendExit()
};


window.WAPI.getAllContacts = function (done) {
    const contacts = window.Store.Contact.map((contact) => WAPI._serializeContactObj(contact));

    if (done !== undefined) done(contacts);
    return contacts;
};

/**
 * Fetches all contact objects from store, filters them
 *
 * @param done Optional callback function for async execution
 * @returns {Array|*} List of contacts
 */
window.WAPI.getMyContacts = function (done) {
    const contacts = window.Store.Contact.filter((contact) => contact.isMyContact === true).map((contact) => WAPI._serializeContactObj(contact));
    if (done !== undefined) done(contacts);
    return contacts;
};

/**
 * Fetches contact object from store by ID
 *
 * @param id ID of contact
 * @param done Optional callback function for async execution
 * @returns {T|*} Contact object
 */
window.WAPI.getContact = function (id, done) {
    const found = window.Store.Contact.get(id);

    if (done !== undefined) done(window.WAPI._serializeContactObj(found))
    return window.WAPI._serializeContactObj(found);
};

/**
 * Fetches all chat objects from store
 *
 * @param done Optional callback function for async execution
 * @returns {Array|*} List of chats
 */
window.WAPI.getAllChats = function (done) {
    const chats = window.Store.Chat.map((chat) => WAPI._serializeChatObj(chat));

    if (done !== undefined) done(chats);
    return chats;
};

window.WAPI.haveNewMsg = function (chat) {
    return chat.unreadCount > 0;
};

window.WAPI.getAllChatsWithNewMsg = function (done) {
    const chats = window.Store.Chat.filter(window.WAPI.haveNewMsg).map((chat) => WAPI._serializeChatObj(chat));

    if (done !== undefined) done(chats);
    return chats;
};

/**
 * Fetches all chat IDs from store
 *
 * @param done Optional callback function for async execution
 * @returns {Array|*} List of chat id's
 */
window.WAPI.getAllChatIds = function (done) {
    const chatIds = window.Store.Chat.map((chat) => chat.id._serialized || chat.id);

    if (done !== undefined) done(chatIds);
    return chatIds;
};

/**
 * Fetches all groups objects from store
 *
 * @param done Optional callback function for async execution
 * @returns {Array|*} List of chats
 */
window.WAPI.getAllGroups = function (done) {
    const groups = window.Store.Chat.filter((chat) => chat.isGroup);

    if (done !== undefined) done(groups);
    return groups;
};

/**
 * Fetches chat object from store by ID
 *
 * @param id ID of chat
 * @param done Optional callback function for async execution
 * @returns {T|*} Chat object
 */
window.WAPI.getChat = function (id, done) {
    id = typeof id == ""string"" ? id : id._serialized;
    const found = window.Store.Chat.get(id);
    if (done !== undefined) done(found);
    return found;
}

window.WAPI.getChatByName = function (name, done) {
    const found = window.Store.Chat.find((chat) => chat.name === name);
    if (done !== undefined) done(found);
    return found;
};

window.WAPI.sendImageFromDatabasePicBot = function (picId, chatId, caption) {
    var chatDatabase = window.WAPI.getChatByName('DATABASEPICBOT');
    var msgWithImg   = chatDatabase.msgs.find((msg) => msg.caption == picId);

    if (msgWithImg === undefined) {
        return false;
    }
    var chatSend = WAPI.getChat(chatId);
    if (chatSend === undefined) {
        return false;
    }
    const oldCaption = msgWithImg.caption;

    msgWithImg.id.id     = window.WAPI.getNewId();
    msgWithImg.id.remote = chatId;
    msgWithImg.t         = Math.ceil(new Date().getTime() / 1000);
    msgWithImg.to        = chatId;

    if (caption !== undefined && caption !== '') {
        msgWithImg.caption = caption;
    } else {
        msgWithImg.caption = '';
    }

    msgWithImg.collection.send(msgWithImg).then(function (e) {
        msgWithImg.caption = oldCaption;
    });

    return true;
};

window.WAPI.sendMessageWithThumb = function (thumb, url, title, description, chatId, done) {
    var chatSend = WAPI.getChat(chatId);
    if (chatSend === undefined) {
        if (done !== undefined) done(false);
        return false;
    }
    var linkPreview = {
        canonicalUrl: url,
        description : description,
        matchedText : url,
        title       : title,
        thumbnail   : thumb
    };
    chatSend.sendMessage(url, { linkPreview: linkPreview, mentionedJidList: [], quotedMsg: null, quotedMsgAdminGroupJid: null });
    if (done !== undefined) done(true);
    return true;
};

window.WAPI.getNewId = function () {
    var text     = """";
    var possible = ""ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"";

    for (var i = 0; i < 20; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    return text;
};

window.WAPI.getChatById = function (id, done) {
    let found = WAPI.getChat(id);
    if (found) {
        found = WAPI._serializeChatObj(found);
    } else {
        found = false;
    }

    if (done !== undefined) done(found);
    return found;
};


/**
 * I return all unread messages from an asked chat and mark them as read.
 *
 * :param id: chat id
 * :type  id: string
 *
 * :param includeMe: indicates if user messages have to be included
 * :type  includeMe: boolean
 *
 * :param includeNotifications: indicates if notifications have to be included
 * :type  includeNotifications: boolean
 *
 * :param done: callback passed by selenium
 * :type  done: function
 *
 * :returns: list of unread messages from asked chat
 * :rtype: object
 */
window.WAPI.getUnreadMessagesInChat = function (id, includeMe, includeNotifications, done) {
    // get chat and its messages
    let chat     = WAPI.getChat(id);
    let messages = chat.msgs._models;

    // initialize result list
    let output = [];

    // look for unread messages, newest is at the end of array
    for (let i = messages.length - 1; i >= 0; i--) {
        // system message: skip it
        if (i === ""remove"") {
            continue;
        }

        // get message
        let messageObj = messages[i];

        // found a read message: stop looking for others
        if (typeof (messageObj.isNewMsg) !== ""boolean"" || messageObj.isNewMsg === false) {
            continue;
        } else {
            messageObj.isNewMsg = false;
            // process it
            let message = WAPI.processMessageObj(messageObj,
                    includeMe,
                    includeNotifications);

            // save processed message on result list
            if (message)
                output.push(message);
        }
    }
    // callback was passed: run it
    if (done !== undefined) done(output);
    // return result list
    return output;
}
;


/**
 * Load more messages in chat object from store by ID
 *
 * @param id ID of chat
 * @param done Optional callback function for async execution
 * @returns None
 */
window.WAPI.loadEarlierMessages = function (id, done) {
    const found = WAPI.getChat(id);
    if (done !== undefined) {
        found.loadEarlierMsgs().then(function () {
            done()
        });
    } else {
        found.loadEarlierMsgs();
    }
};

/**
 * Load more messages in chat object from store by ID
 *
 * @param id ID of chat
 * @param done Optional callback function for async execution
 * @returns None
 */
window.WAPI.loadAllEarlierMessages = function (id, done) {
    const found = WAPI.getChat(id);
    x = function () {
        if (!found.msgs.msgLoadState.noEarlierMsgs) {
            found.loadEarlierMsgs().then(x);
        } else if (done) {
            done();
        }
    };
    x();
};

window.WAPI.asyncLoadAllEarlierMessages = function (id, done) {
    done();
    window.WAPI.loadAllEarlierMessages(id);
};

window.WAPI.areAllMessagesLoaded = function (id, done) {
    const found = WAPI.getChat(id);
    if (!found.msgs.msgLoadState.noEarlierMsgs) {
        if (done) done(false);
        return false
    }
    if (done) done(true);
    return true
};

/**
 * Load more messages in chat object from store by ID till a particular date
 *
 * @param id ID of chat
 * @param lastMessage UTC timestamp of last message to be loaded
 * @param done Optional callback function for async execution
 * @returns None
 */

window.WAPI.loadEarlierMessagesTillDate = function (id, lastMessage, done) {
    const found = WAPI.getChat(id);
    x = function () {
        if (found.msgs.models[0].t > lastMessage && !found.msgs.msgLoadState.noEarlierMsgs) {
            found.loadEarlierMsgs().then(x);
        } else {
            done();
        }
    };
    x();
};


/**
 * Fetches all group metadata objects from store
 *
 * @param done Optional callback function for async execution
 * @returns {Array|*} List of group metadata
 */
window.WAPI.getAllGroupMetadata = function (done) {
    const groupData = window.Store.GroupMetadata.map((groupData) => groupData.all);

    if (done !== undefined) done(groupData);
    return groupData;
};

/**
 * Fetches group metadata object from store by ID
 *
 * @param id ID of group
 * @param done Optional callback function for async execution
 * @returns {T|*} Group metadata object
 */
window.WAPI.getGroupMetadata = async function (id, done) {
    let output = window.Store.GroupMetadata.get(id);

    if (output !== undefined) {
        if (output.stale) {
            await output.update();
        }
    }

    if (done !== undefined) done(output);
    return output;

};


/**
 * Fetches group participants
 *
 * @param id ID of group
 * @returns {Promise.<*>} Yields group metadata
 * @private
 */
window.WAPI._getGroupParticipants = async function (id) {
    const metadata = await WAPI.getGroupMetadata(id);
    return metadata.participants;
};

/**
 * Fetches IDs of group participants
 *
 * @param id ID of group
 * @param done Optional callback function for async execution
 * @returns {Promise.<Array|*>} Yields list of IDs
 */
window.WAPI.getGroupParticipantIDs = async function (id, done) {
    const output = (await WAPI._getGroupParticipants(id))
            .map((participant) => participant.id);

    if (done !== undefined) done(output);
    return output;
};

window.WAPI.getGroupAdmins = async function (id, done) {
    const output = (await WAPI._getGroupParticipants(id))
            .filter((participant) => participant.isAdmin)
            .map((admin) => admin.id);

    if (done !== undefined) done(output);
    return output;
};

/**
 * Gets object representing the logged in user
 *
 * @returns {Array|*|$q.all}
 */
window.WAPI.getMe = function (done) {
    const rawMe = window.Store.Contact.get(window.Store.Conn.me);

    if (done !== undefined) done(rawMe.all);
    return rawMe.all;
};

window.WAPI.isLoggedIn = function (done) {
    // Contact always exists when logged in
    const isLogged = window.Store.Contact && window.Store.Contact.checksum !== undefined;

    if (done !== undefined) done(isLogged);
    return isLogged;
};

window.WAPI.processMessageObj = function (messageObj, includeMe, includeNotifications) {
    if (messageObj.isNotification) {
        if (includeNotifications)
            return WAPI._serializeMessageObj(messageObj);
        else
            return;
        // System message
        // (i.e. ""Messages you send to this chat and calls are now secured with end-to-end encryption..."")
    } else if (messageObj.id.fromMe === false || includeMe) {
        return WAPI._serializeMessageObj(messageObj);
    }
    return;
};

window.WAPI.getAllMessagesInChat = function (id, includeMe, includeNotifications, done) {
    const chat     = WAPI.getChat(id);
    let   output   = [];
    const messages = chat.msgs._models;

    for (const i in messages) {
        if (i === ""remove"") {
            continue;
        }
        const messageObj = messages[i];

        let message = WAPI.processMessageObj(messageObj, includeMe, includeNotifications)
        if (message)
            output.push(message);
    }
    if (done !== undefined) done(output);
    return output;
};

window.WAPI.getAllMessageIdsInChat = function (id, includeMe, includeNotifications, done) {
    const chat     = WAPI.getChat(id);
    let   output   = [];
    const messages = chat.msgs._models;

    for (const i in messages) {
        if ((i === ""remove"")
                || (!includeMe && messages[i].isMe)
                || (!includeNotifications && messages[i].isNotification)) {
            continue;
        }
        output.push(messages[i].id._serialized);
    }
    if (done !== undefined) done(output);
    return output;
};

window.WAPI.getMessageById = function (id, done) {
    let result = false;
    try {
        let msg = window.Store.Msg.get(id);
        if (msg) {
            result = WAPI.processMessageObj(msg, true, true);
        }
    } catch (err) { }

    if (done !== undefined) {
        done(result);
    } else {
        return result;
    }
};

window.WAPI.ReplyMessage = function (idMessage, message, done) {
    var messageObject = window.Store.Msg.get(idMessage);
    if (messageObject === undefined) {
        if (done !== undefined) done(false);
        return false;
    }
    messageObject = messageObject.value();

    const chat = WAPI.getChat(messageObject.chat.id)
    if (chat !== undefined) {
        if (done !== undefined) {
            chat.sendMessage(message, null, messageObject).then(function () {
                function sleep(ms) {
                    return new Promise(resolve => setTimeout(resolve, ms));
                }

                var trials = 0;

                function check() {
                    for (let i = chat.msgs.models.length - 1; i >= 0; i--) {
                        let msg = chat.msgs.models[i];

                        if (!msg.senderObj.isMe || msg.body != message) {
                            continue;
                        }
                        done(WAPI._serializeMessageObj(msg));
                        return True;
                    }
                    trials += 1;
                    console.log(trials);
                    if (trials > 30) {
                        done(true);
                        return;
                    }
                    sleep(500).then(check);
                }
                check();
            });
            return true;
        } else {
            chat.sendMessage(message, null, messageObject);
            return true;
        }
    } else {
        if (done !== undefined) done(false);
        return false;
    }
};

window.WAPI.sendMessageToID = function (id, message, done) {
    try {
        window.getContact = (id) => {
            return Store.WapQuery.queryExist(id);
        }
        window.getContact(id).then(contact => {
            if (contact.status === 404) {
                done(true);
            } else {
                Store.Chat.find(contact.jid).then(chat => {
                    chat.sendMessage(message);
                    return true;
                }).catch(reject => {
                    if (WAPI.sendMessage(id, message)) {
                        done(true);
                        return true;
                    }else{
                        done(false);
                        return false;
                    }
                });
            }
        });
    } catch (e) {
        if (window.Store.Chat.length === 0)
            return false;

        firstChat = Store.Chat.models[0];
        var originalID = firstChat.id;
        firstChat.id = typeof originalID === ""string"" ? id : new window.Store.UserConstructor(id, { intentionallyUsePrivateConstructor: true });
        if (done !== undefined) {
            firstChat.sendMessage(message).then(function () {
                firstChat.id = originalID;
                done(true);
            });
            return true;
        } else {
            firstChat.sendMessage(message);
            firstChat.id = originalID;
            return true;
        }
    }
    if (done !== undefined) done(false);
    return false;
}

window.WAPI.sendMessage = function (id, message, done) {
    var chat = WAPI.getChat(id);
    if (chat !== undefined) {
        if (done !== undefined) {
            chat.sendMessage(message).then(function () {
                function sleep(ms) {
                    return new Promise(resolve => setTimeout(resolve, ms));
                }

                var trials = 0;

                function check() {
                    for (let i = chat.msgs.models.length - 1; i >= 0; i--) {
                        let msg = chat.msgs.models[i];

                        if (!msg.senderObj.isMe || msg.body != message) {
                            continue;
                        }
                        done(WAPI._serializeMessageObj(msg));
                        return True;
                    }
                    trials += 1;
                    console.log(trials);
                    if (trials > 30) {
                        done(true);
                        return;
                    }
                    sleep(500).then(check);
                }
                check();
            });
            return true;
        } else {
            chat.sendMessage(message);
            return true;
        }
    } else {
        if (done !== undefined) done(false);
        return false;
    }
};

window.WAPI.sendMessage2 = function (id, message, done) {
    var chat = WAPI.getChat(id);
    if (chat !== undefined) {
        try {
            if (done !== undefined) {
                chat.sendMessage(message).then(function () {
                    done(true);
                });
            } else {
                chat.sendMessage(message);
            }
            return true;
        } catch (error) {
            if (done !== undefined) done(false)
            return false;
        }
    }
    if (done !== undefined) done(false)
    return false;
};

window.WAPI.sendSeen = function (id, done) {
    var chat = window.WAPI.getChat(id);
    if (chat !== undefined) {
        if (done !== undefined) {
            //Store.SendSeen(Store.Chat.models[0], false).then(function () {
            Store.SendSeen(chat, false).then(function () {
                done(true);
            });
            return true;
        } else {
            //Store.SendSeen(Store.Chat.models[0], false);
            Store.SendSeen(Chat, false);
            return true;
        }
    }
    if (done !== undefined) done();
    return false;
};

function isChatMessage(message) {
    if (message.isSentByMe) {
        return false;
    }
    if (message.isNotification) {
        return false;
    }
    if (!message.isUserCreatedType) {
        return false;
    }
    return true;
}


window.WAPI.getUnreadMessages = function (includeMe, includeNotifications, use_unread_count, done) {
    const chats  = window.Store.Chat.models;
    let   output = [];

    for (let chat in chats) {
        if (isNaN(chat)) {
            continue;
        }

        let messageGroupObj = chats[chat];
        let messageGroup    = WAPI._serializeChatObj(messageGroupObj);

        messageGroup.messages = [];

        const messages = messageGroupObj.msgs._models;
        for (let i = messages.length - 1; i >= 0; i--) {
            let messageObj = messages[i];
            if (typeof (messageObj.isNewMsg) != ""boolean"" || messageObj.isNewMsg === false) {
                continue;
            } else {
                messageObj.isNewMsg = false;
                let message = WAPI.processMessageObj(messageObj, includeMe, includeNotifications);
                if (message) {
                    messageGroup.messages.push(message);
                }
            }
        }

        if (messageGroup.messages.length > 0) {
            output.push(messageGroup);
        } else { // no messages with isNewMsg true
            if (use_unread_count) {
                let n = messageGroupObj.unreadCount; // will use unreadCount attribute to fetch last n messages from sender
                for (let i = messages.length - 1; i >= 0; i--) {
                    let messageObj = messages[i];
                    if (n > 0) {
                        if (!messageObj.isSentByMe) {
                            let message = WAPI.processMessageObj(messageObj, includeMe, includeNotifications);
                            messageGroup.messages.unshift(message);
                            n -= 1;
                        }
                    } else if (n === -1) { // chat was marked as unread so will fetch last message as unread
                        if (!messageObj.isSentByMe) {
                            let message = WAPI.processMessageObj(messageObj, includeMe, includeNotifications);
                            messageGroup.messages.unshift(message);
                            break;
                        }
                    } else { // unreadCount = 0
                        break;
                    }
                }
                if (messageGroup.messages.length > 0) {
                    messageGroupObj.unreadCount = 0; // reset unread counter
                    output.push(messageGroup);
                }
            }
        }
    }
    if (done !== undefined) {
        done(output);
    }
    return output;
};

window.WAPI.getGroupOwnerID = async function (id, done) {
    const output = (await WAPI.getGroupMetadata(id)).owner.id;
    if (done !== undefined) {
        done(output);
    }
    return output;

};

window.WAPI.getCommonGroups = async function (id, done) {
    let output = [];

    groups = window.WAPI.getAllGroups();

    for (let idx in groups) {
        try {
            participants = await window.WAPI.getGroupParticipantIDs(groups[idx].id);
            if (participants.filter((participant) => participant == id).length) {
                output.push(groups[idx]);
            }
        } catch (err) {
            console.log(""Error in group:"");
            console.log(groups[idx]);
            console.log(err);
        }
    }

    if (done !== undefined) {
        done(output);
    }
    return output;
};


window.WAPI.getProfilePicSmallFromId = function (id, done) {
    window.Store.ProfilePicThumb.find(id).then(function (d) {
        if (d.img !== undefined) {
            window.WAPI.downloadFileWithCredentials(d.img, done);
        } else {
            done(false);
        }
    }, function (e) {
        done(false);
    })
};

window.WAPI.getProfilePicFromId = function (id, done) {
    window.Store.ProfilePicThumb.find(id).then(function (d) {
        if (d.imgFull !== undefined) {
            window.WAPI.downloadFileWithCredentials(d.imgFull, done);
        } else {
            done(false);
        }
    }, function (e) {
        done(false);
    })
};

window.WAPI.downloadFileWithCredentials = function (url, done) {
    let xhr = new XMLHttpRequest();

    xhr.onload = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                let reader = new FileReader();
                reader.readAsDataURL(xhr.response);
                reader.onload = function (e) {
                    done(reader.result.substr(reader.result.indexOf(',') + 1))
                };
            } else {
                console.error(xhr.statusText);
            }
        } else {
            console.log(err);
            done(false);
        }
    };

    xhr.open(""GET"", url, true);
    xhr.withCredentials = true;
    xhr.responseType = 'blob';
    xhr.send(null);
};


window.WAPI.downloadFile = function (url, done) {
    let xhr = new XMLHttpRequest();


    xhr.onload = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                let reader = new FileReader();
                reader.readAsDataURL(xhr.response);
                reader.onload = function (e) {
                    done(reader.result.substr(reader.result.indexOf(',') + 1))
                };
            } else {
                console.error(xhr.statusText);
            }
        } else {
            console.log(err);
            done(false);
        }
    };

    xhr.open(""GET"", url, true);
    xhr.responseType = 'blob';
    xhr.send(null);
};

window.WAPI.getBatteryLevel = function (done) {
    if (window.Store.Conn.plugged) {
        if (done !== undefined) {
            done(100);
        }
        return 100;
    }
    output = window.Store.Conn.battery;
    if (done !== undefined) {
        done(output);
    }
    return output;
};

window.WAPI.deleteConversation = function (chatId, done) {
    let userId       = new window.Store.UserConstructor(chatId, {intentionallyUsePrivateConstructor: true});
    let conversation = WAPI.getChat(userId);

    if (!conversation) {
        if (done !== undefined) {
            done(false);
        }
        return false;
    }

    window.Store.sendDelete(conversation, false).then(() => {
        if (done !== undefined) {
            done(true);
        }
    }).catch(() => {
        if (done !== undefined) {
            done(false);
        }
    });

    return true;
};

window.WAPI.deleteMessage = function (chatId, messageArray, revoke=false, done) {
    let userId       = new window.Store.UserConstructor(chatId, {intentionallyUsePrivateConstructor: true});
    let conversation = WAPI.getChat(userId);

    if(!conversation) {
        if(done !== undefined) {
            done(false);
        }
        return false;
    }

    if (!Array.isArray(messageArray)) {
        messageArray = [messageArray];
    }

    if (revoke) {
        conversation.sendRevokeMsgs(messageArray, conversation);    
    } else {
        conversation.sendDeleteMsgs(messageArray, conversation);    
    }


    if (done !== undefined) {
        done(true);
    }

    return true;
};

window.WAPI.checkNumberStatus = function (id, done) {
    window.Store.WapQuery.queryExist(id).then((result) => {
        if( done !== undefined) {
            if (result.jid === undefined) throw 404;
            done(window.WAPI._serializeNumberStatusObj(result));
        }
    }).catch((e) => {
        if (done !== undefined) {
            done(window.WAPI._serializeNumberStatusObj({
                status: e,
                jid   : id
            }));
        }
    });

    return true;
};

/**
 * New messages observable functions.
 */
window.WAPI._newMessagesQueue     = [];
window.WAPI._newMessagesBuffer    = (sessionStorage.getItem('saved_msgs') != null) ? JSON.parse(sessionStorage.getItem('saved_msgs')) : [];
window.WAPI._newMessagesDebouncer = null;
window.WAPI._newMessagesCallbacks = [];

window.Store.Msg.off('add');
sessionStorage.removeItem('saved_msgs');

window.WAPI._newMessagesListener = window.Store.Msg.on('add', (newMessage) => {
    if (newMessage && newMessage.isNewMsg && !newMessage.isSentByMe) {
        let message = window.WAPI.processMessageObj(newMessage, false, false);
        if (message) {
            window.WAPI._newMessagesQueue.push(message);
            window.WAPI._newMessagesBuffer.push(message);
        }

        // Starts debouncer time to don't call a callback for each message if more than one message arrives
        // in the same second
        if (!window.WAPI._newMessagesDebouncer && window.WAPI._newMessagesQueue.length > 0) {
            window.WAPI._newMessagesDebouncer = setTimeout(() => {
                let queuedMessages = window.WAPI._newMessagesQueue;

                window.WAPI._newMessagesDebouncer = null;
                window.WAPI._newMessagesQueue     = [];

                let removeCallbacks = [];

                window.WAPI._newMessagesCallbacks.forEach(function (callbackObj) {
                    if (callbackObj.callback !== undefined) {
                        callbackObj.callback(queuedMessages);
                    }
                    if (callbackObj.rmAfterUse === true) {
                        removeCallbacks.push(callbackObj);
                    }
                });

                // Remove removable callbacks.
                removeCallbacks.forEach(function (rmCallbackObj) {
                    let callbackIndex = window.WAPI._newMessagesCallbacks.indexOf(rmCallbackObj);
                    window.WAPI._newMessagesCallbacks.splice(callbackIndex, 1);
                });
            }, 1000);
        }
    }
});

window.WAPI._unloadInform = (event) => {
    // Save in the buffer the ungot unreaded messages
    window.WAPI._newMessagesBuffer.forEach((message) => {
        Object.keys(message).forEach(key => message[key] === undefined ? delete message[key] : '');
    });
    sessionStorage.setItem(""saved_msgs"", JSON.stringify(window.WAPI._newMessagesBuffer));

    // Inform callbacks that the page will be reloaded.
    window.WAPI._newMessagesCallbacks.forEach(function (callbackObj) {
        if (callbackObj.callback !== undefined) {
            callbackObj.callback({ status: -1, message: 'page will be reloaded, wait and register callback again.' });
        }
    });
};

window.addEventListener(""unload"", window.WAPI._unloadInform, false);
window.addEventListener(""beforeunload"", window.WAPI._unloadInform, false);
window.addEventListener(""pageunload"", window.WAPI._unloadInform, false);

/**
 * Registers a callback to be called when a new message arrives the WAPI.
 * @param rmCallbackAfterUse - Boolean - Specify if the callback need to be executed only once
 * @param done - function - Callback function to be called when a new message arrives.
 * @returns {boolean}
 */
window.WAPI.waitNewMessages = function (rmCallbackAfterUse = true, done) {
    window.WAPI._newMessagesCallbacks.push({ callback: done, rmAfterUse: rmCallbackAfterUse });
    return true;
};

/**
 * Reads buffered new messages.
 * @param done - function - Callback function to be called contained the buffered messages.
 * @returns {Array}
 */
window.WAPI.getBufferedNewMessages = function (done) {
    let bufferedMessages = window.WAPI._newMessagesBuffer;
    window.WAPI._newMessagesBuffer = [];
    if (done !== undefined) {
        done(bufferedMessages);
    }
    return bufferedMessages;
};
/** End new messages observable functions **/

window.WAPI.sendImage = function (imgBase64, chatid, filename, caption, done) {
//var idUser = new window.Store.UserConstructor(chatid);
var idUser = new window.Store.UserConstructor(chatid, { intentionallyUsePrivateConstructor: true });
// create new chat
return Store.Chat.find(idUser).then((chat) => {
    var mediaBlob = window.WAPI.base64ImageToFile(imgBase64, filename);
    var mc = new Store.MediaCollection();
    mc.processFiles([mediaBlob], chat, 1).then(() => {
        var media = mc.models[0];
        media.sendToChat(chat, { caption: caption });
        if (done !== undefined) done(true);
    });
});
}

window.WAPI.base64ImageToFile = function (b64Data, filename) {
    var arr   = b64Data.split(',');
    var mime  = arr[0].match(/:(.*?);/)[1];
    var bstr  = atob(arr[1]);
    var n     = bstr.length;
    var u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, {type: mime});
};

/**
 * Send contact card to a specific chat using the chat ids
 *
 * @param {string} to '000000000000@c.us'
 * @param {string|array} contact '111111111111@c.us' | ['222222222222@c.us', '333333333333@c.us, ... 'nnnnnnnnnnnn@c.us']
 */
window.WAPI.sendContact = function (to, contact) {
    if (!Array.isArray(contact)) {
        contact = [contact];
    }
    contact = contact.map((c) => {
        return WAPI.getChat(c).__x_contact;
    });

    if (contact.length > 1) {
        window.WAPI.getChat(to).sendContactList(contact);
    } else if (contact.length === 1) {
        window.WAPI.getChat(to).sendContact(contact[0]);
    }
};

/**
 * Create an chat ID based in a cloned one
 *
 * @param {string} chatId '000000000000@c.us'
 */
window.WAPI.getNewMessageId = function (chatId) {
    var newMsgId = Store.Msg.models[0].__x_id.clone();

    newMsgId.fromMe      = true;
    newMsgId.id          = WAPI.getNewId().toUpperCase();
    newMsgId.remote      = chatId;
    newMsgId._serialized = `${newMsgId.fromMe}_${newMsgId.remote}_${newMsgId.id}`

    return newMsgId;
};

/**
 * Send Customized VCard without the necessity of contact be a Whatsapp Contact
 *
 * @param {string} chatId '000000000000@c.us'
 * @param {object|array} vcard { displayName: 'Contact Name', vcard: 'BEGIN:VCARD\nVERSION:3.0\nN:;Contact Name;;;\nEND:VCARD' } | [{ displayName: 'Contact Name 1', vcard: 'BEGIN:VCARD\nVERSION:3.0\nN:;Contact Name 1;;;\nEND:VCARD' }, { displayName: 'Contact Name 2', vcard: 'BEGIN:VCARD\nVERSION:3.0\nN:;Contact Name 2;;;\nEND:VCARD' }]
 */
window.WAPI.sendVCard = function (chatId, vcard) {
    var chat    = Store.Chat.get(chatId);
    var tempMsg = Object.create(Store.Msg.models.filter(msg => msg.__x_isSentByMe)[0]);
    var newId   = window.WAPI.getNewMessageId(chatId);

    var extend = {
        ack     : 0,
        id      : newId,
        local   : !0,
        self    : ""out"",
        t       : parseInt(new Date().getTime() / 1000),
        to      : chatId,
        isNewMsg: !0,
    };

    if (Array.isArray(vcard)) {
        Object.assign(extend, {
            type     : ""multi_vcard"",
            vcardList: vcard
        });

        delete extend.body;
    } else {
        Object.assign(extend, {
            type   : ""vcard"",
            subtype: vcard.displayName,
            body   : vcard.vcard
        });

        delete extend.vcardList;
    }

    Object.assign(tempMsg, extend);

    chat.addAndSendMsg(tempMsg);
};
/**
 * Block contact 
 * @param {string} id '000000000000@c.us'
 * @param {*} done - function - Callback function to be called when a new message arrives.
 */
window.WAPI.contactBlock = function (id, done) {
    const contact = window.Store.Contact.get(id);
    if (contact !== undefined) {
        contact.setBlock(!0);
        done(true);
        return true;
    }
    done(false);
    return false;
}
/**
 * unBlock contact 
 * @param {string} id '000000000000@c.us'
 * @param {*} done - function - Callback function to be called when a new message arrives.
 */
window.WAPI.contactUnblock = function (id, done) {
    const contact = window.Store.Contact.get(id);
    if (contact !== undefined) {
        contact.setBlock(!1);
        done(true);
        return true;
    }
    done(false);
    return false;
}

/**
 * Remove participant of Group
 * @param {*} idGroup '0000000000-00000000@g.us'
 * @param {*} idParticipant '000000000000@c.us'
 * @param {*} done - function - Callback function to be called when a new message arrives.
 */
window.WAPI.removeParticipantGroup = function (idGroup, idParticipant, done) {
    window.Store.WapQuery.removeParticipants(idGroup, [idParticipant]).then(() => {
        const metaDataGroup = window.Store.GroupMetadata.get(id)
        checkParticipant = metaDataGroup.participants._index[idParticipant];
        if (checkParticipant === undefined) {
            done(true); return true;
        }
    })
}

/**
 * Promote Participant to Admin in Group
 * @param {*} idGroup '0000000000-00000000@g.us'
 * @param {*} idParticipant '000000000000@c.us'
 * @param {*} done - function - Callback function to be called when a new message arrives.
 */
window.WAPI.promoteParticipantAdminGroup = function (idGroup, idParticipant, done) {
    window.Store.WapQuery.promoteParticipants(idGroup, [idParticipant]).then(() => {
        const metaDataGroup = window.Store.GroupMetadata.get(id)
        checkParticipant = metaDataGroup.participants._index[idParticipant];
        if (checkParticipant !== undefined && checkParticipant.isAdmin) {
            done(true); return true;
        }
        done(false); return false;
    })
}

/**
 * Demote Admin of Group
 * @param {*} idGroup '0000000000-00000000@g.us'
 * @param {*} idParticipant '000000000000@c.us'
 * @param {*} done - function - Callback function to be called when a new message arrives.
 */
window.WAPI.demoteParticipantAdminGroup = function (idGroup, idParticipant, done) {
    window.Store.WapQuery.demoteParticipants(idGroup, [idParticipant]).then(() => {
        const metaDataGroup = window.Store.GroupMetadata.get(id)
        if (metaDataGroup === undefined) {
            done(false); return false;
        }
        checkParticipant = metaDataGroup.participants._index[idParticipant];
        if (checkParticipant !== undefined && checkParticipant.isAdmin) {
            done(false); return false;
        }
        done(true); return true;
    })
}

"
        browser.ExecuteScriptAsync(script)
    End Sub

    Public Sub initWebPackJSON()
        Dim script As String

        script = "setTimeout(function() {
function getAllModules() {
    return new Promise((resolve) => {
        const id = _.uniqueId(""fakeModule_"");
        window[""webpackJsonp""](
            [],
            {
                [id]: function(module, exports, __webpack_require__) {
                    resolve(__webpack_require__.c);
                }
            },
            [id]
        );
    });
}

var modules = getAllModules()._value;

for (var key in modules) {
	if (modules[key].exports) {
		if (modules[key].exports.createFromData) {
			createFromData_id = modules[key].id.replace(/""/g, '""');
		}
		if (modules[key].exports.prepRawMedia) {
			prepareRawMedia_id = modules[key].id.replace(/""/g, '""');
		}
		if (modules[key].exports.default) {
			if (modules[key].exports.default.Wap) {
				store_id = modules[key].id.replace(/""/g, '""');
			}
		}
	}
}

}, 2000);

function _requireById(id) {
	return webpackJsonp([], null, [id]);
}
var createFromData_id = 0;
var prepareRawMedia_id = 0;
var store_id = 0;

function fixBinary (bin) {
	var length = bin.length;
	var buf = new ArrayBuffer(length);
	var arr = new Uint8Array(buf);
	for (var i = 0; i < length; i++) {
	  arr[i] = bin.charCodeAt(i);
	}
	return buf;
}
var send_media;
window.send_media = function(jid, link, caption, msg_id, content_type) {
	var file = """";
	var createFromDataClass = _requireById(createFromData_id)[""default""];
	var prepareRawMediaClass = _requireById(prepareRawMedia_id).prepRawMedia;
	window.Store.Chat.find(jid).then((chat) => {
		chat.markComposing();
		var img_b64 = link;
		var base64 = img_b64.split(',')[1];
		var type = img_b64.split(',')[0];
		type = type.split(';')[0];
		type = type.split(':')[1];
		var binary = fixBinary(atob(base64));
		var blob = new Blob([binary], {type: type});
		var random_name = Math.random().toString(36).substr(2, 5);
		file = new File([blob], random_name, {
			type: type,
			lastModified: Date.now()
		});
		
		var temp = createFromDataClass.createFromData(file, file.type);
		var rawMedia = prepareRawMediaClass(temp, {});
		var target = _.filter(window.Store.Msg.models, (msg) => {
			return msg.id.id === msg_id;
		})[0];
		var textPortion = {
			caption: caption,
			mentionedJidList: [],
			quotedMsg: target
		};
		rawMedia.sendToChat(chat, textPortion);
		
		
	});
}

window.sendMessageToID = function (id, message, done) {
    if (window.Store.Chat.length == 0)
        return false;
    
    firstChat = Store.Chat.models[0];
    var originalID = firstChat.id;
    firstChat.id = typeof originalID == ""string"" ? id : new window.Store.UserConstructor(id);
    if (done !== undefined) {
        firstChat.sendMessage(message).then(function () {
            firstChat.id = originalID;
            done(true);
        });
        return true;
    } else {
        firstChat.sendMessage(message);
        firstChat.id = originalID;
        return true;
    }

    if (done !== undefined)
        done();
    else
        return false;

    return true;
}

var Store = {};

function init() {
 window.Store = _requireById(store_id).default;
 console.log(""Store is ready"");
 console.log(window.Store); 
}

setTimeout(function() {
 init();
}, 5000);"

        browser.ExecuteScriptAsync(script)

    End Sub

    Public Sub InitBrowser(destino As Integer)

        If destino = 1 Then
            browser.Load("web.whatsapp.com")
        ElseIf destino = 2 Then
            browser.Load("www.google.com")
        ElseIf destino = 3 Then
            browser.Load("www.gmail.com")
        ElseIf destino = 4 Then
            browser.Load("https://web.whatsapp.com/send?phone=584166188026")
            Task.Delay(1000).Wait()
            'InitBrowser(1)
        End If

        'AddHandler browser.LoadingStateChanged, AddressOf browser_LoadingStateChanged
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cefSett As New CefSettings
        Dim dbServer2 As String

        version = "2.5.17.06"

        ' ****************************************************************
        'Esta variable mientras se habilita la forma inicial (CRM - Campaign)
        MySettings.Default.StartingMode = 0
        MySettings.Default.Save()
        ' ****************************************************************
        ' ****************************************************************

        'Habilitar solamente para usuarios avanzados
        'ConfigurationForm.ComboBox_server.Enabled = False
        dbServer = MySettings.Default.CRMServer

        If dbServer = "gosmart_v3" Then
            dbConnectString = "Server=gosmartcrm.com; Database=" + dbServer + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_dev" Then
            dbServer2 = "gosmart_v103"
            dbConnectString = "Server=dev.gosmartcrm.com; Database=" + dbServer2 + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_v103" Then
            dbConnectString = "Server=gosmartcrm.com; Database=" + dbServer + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_v201" Then
            dbConnectString = "Server=localhost; Database=" + dbServer + "; Uid=root; Pwd="
        End If

        'dbServer = "gosmart_v201"
        'dbConnectString = "Server=localhost; Database=" + dbServer + "; Uid=root; Pwd="

        wsCRM = False

        cache_dir = Application.StartupPath + "\tmp"

        If (System.IO.Directory.Exists(cache_dir) = False) Then
            System.IO.Directory.CreateDirectory(cache_dir)
        End If

        cefSett.CachePath = cache_dir
        cefSett.CefCommandLineArgs.Add("persist_session_cookies", "1")
        CefSharp.Cef.Initialize(cefSett)

        browser = New ChromiumWebBrowser("")
        ChatPanel.Controls.Add(browser)

        browser.Dock = DockStyle.Fill

        'btnChat.PerformClick()

    End Sub

    Private Sub btnChat_Click(sender As Object, e As EventArgs) Handles btnChat.Click
        Dim cefSett As New CefSettings
        Dim task1, task2 As Object
        Dim script, mensaje As String
        Dim dbServer2 As String

        dbServer = MySettings.Default.CRMServer

        If dbServer = "gosmart_v3" Then
            dbConnectString = "Server=gosmartcrm.com; Database=" + dbServer + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_dev" Then
            dbServer2 = "gosmart_v103"
            dbConnectString = "Server=dev.gosmartcrm.com; Database=" + dbServer2 + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_v103" Then
            dbConnectString = "Server=gosmartcrm.com; Database=" + dbServer + "; Uid=root; Pwd=GO(smart)"
        ElseIf dbServer = "gosmart_v201" Then
            dbConnectString = "Server=localhost; Database=" + dbServer + "; Uid=root; Pwd="
        End If

        cache_dir = Application.StartupPath + "\tempWhatsapp"
        cefSett.CachePath = cache_dir
        cefSett.CefCommandLineArgs.Add("persist_session_cookies", "1")

        InitBrowser(1)
        Task.Delay(3000).Wait()

        Do While (browser.GetBrowser.IsLoading)
        Loop
        ' initWebPackJSON() 'Este es el primer codigo
        initWebPackJSON2()
        'Task.Delay(15000).Wait()

        Timer2.Enabled = True


    End Sub

    Private Sub btnLoadSimpleXS_Click(sender As Object, e As EventArgs) Handles btnLoadSimpleXS.Click
        Dim DelayForTextMessage As Integer
        Dim XLSTextFile As String

        XLSTextFile = MySettings.Default.TextXLSFile
        DelayForTextMessage = MySettings.Default.DelayForTextMessage * 1000
        SendUsingExcelFile(XLSTextFile, DelayForTextMessage, 1)

    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click

        ConfigurationForm.ShowDialog()

    End Sub

    Private Sub btnLoadImageXLS_Click(sender As Object, e As EventArgs) Handles btnLoadImageXLS.Click
        Dim DelayForImageMessage As Integer
        Dim XLSImageFile As String

        XLSImageFile = MySettings.Default.ImageXLSFile
        DelayForImageMessage = MySettings.Default.DelayForImageMessage * 1000
        SendUsingExcelFile(XLSImageFile, DelayForImageMessage, 2)


    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        browser.ShowDevTools()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.SendTxtMessageToID("584241697419", "prueba fin")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'MsgBox("timer activo")
        Dim task1, task2 As Object
        Dim script, sql As String
        Dim con As New MySqlConnection(dbConnectString)

        ipPublic = GetExternalIp()
        ipPrivate = GetIPv4Address()

        script = "window.WAPI.getBatteryLevel();"
        task1 = browser.EvaluateScriptAsync(script)

        script = "window.Store.State.default.__x_state;"
        task2 = browser.EvaluateScriptAsync(script)
        Console.WriteLine(task2.Result.Result.ToString())

        'If (task1.Result.Result Is Nothing) Then
        If (task2.Result.Result.ToString() = "CONNECTED") Then

            PictureBoxOffline.Visible = False
            PictureBoxOnline.Visible = True
            Lbl_status.Text = "Online"
            If (MySettings.Default.StartingMode = 1) Then  'Modo Campaña
                Me.btnLoadImageXLS.Visible = True
                Me.btnLoadSimpleXS.Visible = True
                Me.btnLoadImageXLS.Enabled = True
                Me.btnLoadSimpleXS.Enabled = True
            End If

            If wsCRM = True Then
                PictureBoxCRMOff.Visible = False
                PictureBoxCRMOn.Visible = True
                lblCRM.Text = "CRM On"
                checkWSMessages()
                getUnreadMessages()

                'actualizar en CRM que esta conectado
                Try
                    con.Open()
                    sql = "CALL ws_servers('" + myNumber + "','" + ipPublic + "','" + ipPrivate + "',1)"
                    Dim cmd As New MySqlCommand(sql, con)
                    cmd.ExecuteScalar()
                Finally
                    con.Close()

                End Try

            Else
                PictureBoxOffline.Visible = True
                PictureBoxOnline.Visible = False
                Lbl_status.Text = "Offline"
                Me.btnLoadImageXLS.Enabled = False
                Me.btnLoadSimpleXS.Enabled = False

                PictureBoxCRMOff.Visible = True
                PictureBoxCRMOn.Visible = False
                lblCRM.Text = "CRM Off"
                Me.btnActivateCRM.Enabled = False

                MsgBox("You are disconnected from WhatsApp")

                'actualizar en CRM que esta desconectado
                Try
                    con.Open()
                    sql = "CALL ws_servers('" + myNumber + "','" + ipPublic + "','" + ipPrivate + "',0)"
                    Dim cmd As New MySqlCommand(sql, con)
                    cmd.ExecuteScalar()
                Finally
                    con.Close()

                End Try

            End If

            'Me.btnActivateCRM.Enabled = True

            'MsgBox(task.Result.Result.ToString())
        End If

    End Sub

    Private Sub btnActivateCRM_Click(sender As Object, e As EventArgs) Handles btnActivateCRM.Click
        Dim dinamicToolTip As String

        If wsCRM = True Then
            wsCRM = False
            PictureBoxCRMOff.Visible = True
            PictureBoxCRMOn.Visible = False
            lblCRM.Text = "CRM Off"
            dinamicToolTip = "Start CRM WhatsApp"
            settingToolTip.SetToolTip(btnActivateCRM, dinamicToolTip)

        Else
            wsCRM = True
            PictureBoxCRMOff.Visible = False
            PictureBoxCRMOn.Visible = True
            lblCRM.Text = "CRM On"
            dinamicToolTip = "Stop CRM WhatsApp"
            settingToolTip.SetToolTip(btnActivateCRM, dinamicToolTip)

        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        InitUser()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim str As String = "Si escribo en CRM con acento WS camión así né "

        MsgBox(UnicodeToUTF8(str))
        Console.WriteLine(UnicodeToUTF8(str))

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        GetImageFromBDToBase64(1)
    End Sub

    Private Sub Button1_Click_3(sender As Object, e As EventArgs)
        Dim base64Img As String
        base64Img = GetImageFromBD(19)
        Console.WriteLine(base64Img)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'Dim wsMessage As String

        'wsMessage = "Buen da se±ora Maria, le hacemos env­o de lo que es su usuario y clave para el servicio legal. USUARIO: marguello CONTRASEÃ‘A: Escoto1980 la pagina web es www.LegalShield.com"
        'wsMessage = "ñ"

        'wsMessage = UTF8toUnicode(wsMessage)

        Dim con As New MySqlConnection(dbConnectString)
        Dim mess, sql, script, wsNumber, wsMessage, imgType As String

        ':::Abrimos la conexión
        con.Open()

        sql = "SELECT wm.message, wm.target_number, wm.id, wi.type imgType, wi.image wsImage FROM ws_messages wm LEFT JOIN 
                ws_messages_images wi ON wm.id = wi.id_ws_messages WHERE is_send_flag=0"

        Dim cmd As New MySqlCommand(sql, con)
        'cmd.Parameters.AddWithValue("@cod", Convert.ToInt32(TextBox1.Text))
        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        While reader.Read()
            mess = "Enviar antes: " + CType(reader("message"), String) + " al telefono: " + CType(reader("target_number"), String)
            Console.WriteLine(mess)

            wsNumber = CType(reader("target_number"), String)
            wsMessage = CType(reader("message"), String)
            'wsMessage = UTF8toUnicode(wsMessage)
            wsMessage = wsMessage.Replace(vbNullChar, "")
            wsMessage = wsMessage.Replace(vbLf, "\n")
            wsMessage = wsMessage.Replace(vbCr, "\n")


            mess = "Enviar despues: " + CType(reader("message"), String) + " al telefono: " + CType(reader("target_number"), String)
            Console.WriteLine(mess)

        End While
        reader.Close()



    End Sub

    Private Sub MainForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        CefSharp.Cef.Shutdown()

    End Sub

    Private Sub MainForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub MainForm_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        CefSharp.Cef.Shutdown()
    End Sub

    Private Sub Button1_Click_4(sender As Object, e As EventArgs)
        SendNotificationFromFirebaseCloud_blank()


    End Sub


    Public Shared Function SendNotificationFromFirebaseCloud_blank() As String

        Dim result = "-1"
        Dim webAddr = "https://fcm.googleapis.com/fcm/send"
        Dim httpWebRequest = CType(WebRequest.Create(webAddr), HttpWebRequest)
        httpWebRequest.ContentType = "application/json"
        httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAl58habE:APA91bFAJGgAgy1mErtl2-SLS0rMhIzuhh-01Le0VmQTgEL1Nfzor7-foP_8JvdzgVUcMVHgmmtjxzll9gVginx38M3QHlDdRR0T5acmBK30GQXjY5PCCjX83SF5ewGI7MBEMkbu8bjL")
        httpWebRequest.Method = "POST"
        Dim streamWriter = New StreamWriter(httpWebRequest.GetRequestStream)

        Dim strNJson As String = "{""message"":{""topic"":""deals"",""notification"":{""body"":""View latest deals from top brands."",""title"":""Latest Deals""}}}"

        Dim strNewJson As String = "{""to"":""do5Fes9IdiY:APA91bGuU-RZCQ_wK6lMlpfvGypClIwZ5KUedxHPCh2ykcnqmPQXEh106-ZH4zDcKCLtWJbnizhBjBCtca2yfaK4NqZa7TJXE3UtpUiqe8ejsD0GiDKoncxflJARzIyGdBD_E_nl-uzh"",
        ""data"":{""title"":""New WS Message"", ""message"":""Nuevo mensaje"", ""type"":""whatsApp"",
        ""payload"":{""id_ws"":""379"", ""id_leads"":""18851"", ""id_contacts"":""0"", ""target_number"":""584241697419"", ""message"":""Test 14"", ""day"":""11"", ""month"":""Jun"", ""hour"":""15:41:00""}, ""icon"":""../../assets/media/img/ws.png""}}"

        streamWriter.Write(strNewJson)
        streamWriter.Flush()
        Dim httpResponse = CType(httpWebRequest.GetResponse, HttpWebResponse)
        Dim streamReader = New StreamReader(httpResponse.GetResponseStream)
        result = streamReader.ReadToEnd
        Console.WriteLine(result)
        Return result

    End Function

    Public Function SendNotificationFromFirebaseCloud(id_user_crm As String, id_ws As String, id_leads As String, id_contacts As String, message As String, target_number As String, wsDay As String, wsMonth As String, wsHour As String, from_number As String) As String
        Dim con As New MySqlConnection(dbConnectString)
        Dim sql, token As String

        Dim result = "-1"
        Dim webAddr = "https://fcm.googleapis.com/fcm/send"
        'Dim httpWebRequest = CType(WebRequest.Create(webAddr), HttpWebRequest)
        'httpWebRequest.ContentType = "application/json"
        'httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAl58habE:APA91bFAJGgAgy1mErtl2-SLS0rMhIzuhh-01Le0VmQTgEL1Nfzor7-foP_8JvdzgVUcMVHgmmtjxzll9gVginx38M3QHlDdRR0T5acmBK30GQXjY5PCCjX83SF5ewGI7MBEMkbu8bjL")
        'httpWebRequest.Method = "POST"
        'Dim streamWriter = New StreamWriter(httpWebRequest.GetRequestStream)

        ':::Utilizamos el try para capturar posibles errores
        Try
            ':::Abrimos la conexión
            con.Open()

            sql = "SELECT token as token FROM user_crm_token WHERE id_user_crm = " + id_user_crm + " AND DATE(date_token)  = DATE(curdate())"
            Dim cmd As New MySqlCommand(sql, con)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Console.WriteLine(sql)

            While reader.Read()
                token = reader("token")
                Dim fulltitle As String = "WhatsApp from " + myName + " (" + from_number + ")"
                Dim strNewJson As String = "{""to"":""" + token + """,
                ""data"":{""title"":""" + fulltitle + """, ""message"":""" + message + """, ""type"":""whatsApp"",
                ""payload"":{""id_ws"":""" + id_ws + """, ""id_leads"":""" + id_leads + """, ""id_contacts"":""" + id_contacts +
                """, ""target_number"":""" + target_number + """, ""message"":""" + message + """, ""day"":""" + wsDay + """, ""month"":""" +
                wsMonth + """, ""hour"":""" + wsHour + """}, ""icon"":""../../assets/media/img/ws.png""}}"

                Dim httpWebRequest = CType(WebRequest.Create(webAddr), HttpWebRequest)
                httpWebRequest.ContentType = "application/json"
                httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAl58habE:APA91bFAJGgAgy1mErtl2-SLS0rMhIzuhh-01Le0VmQTgEL1Nfzor7-foP_8JvdzgVUcMVHgmmtjxzll9gVginx38M3QHlDdRR0T5acmBK30GQXjY5PCCjX83SF5ewGI7MBEMkbu8bjL")
                httpWebRequest.Method = "POST"
                Dim streamWriter = New StreamWriter(httpWebRequest.GetRequestStream)

                streamWriter.Write(strNewJson)
                streamWriter.Flush()
                Dim httpResponse = CType(httpWebRequest.GetResponse, HttpWebResponse)
                Dim streamReader = New StreamReader(httpResponse.GetResponseStream)
                result = streamReader.ReadToEnd

            End While
            reader.Close()

        Catch ex As Exception
            ':::Si no se conecta nos mostrara el posible fallo en la conexión
            'MsgBox("Fail at checkLoggednumber: " & ex.Message)
            Return "Failed"
        Finally
            con.Close()
        End Try

        'Dim strNewJson As String = "{""to"":""do5Fes9IdiY:APA91bGuU-RZCQ_wK6lMlpfvGypClIwZ5KUedxHPCh2ykcnqmPQXEh106-ZH4zDcKCLtWJbnizhBjBCtca2yfaK4NqZa7TJXE3UtpUiqe8ejsD0GiDKoncxflJARzIyGdBD_E_nl-uzh"",
        '""data"":{""title"":""New WS Message"", ""message"":""Nueno mensaje!"", ""type"":""whatsApp"",
        '""payload"":{""id_ws"":""3"", ""id_leads"":""17493"", ""id_contacts"":""0"", ""target_number"":""584241697419"", ""message"":""Nueno mensaje!"", ""day"":""10"", ""month"":""06"", ""hour"":""17:00:01""}, ""icon"":""../../assets/media/img/ws.png""}}"

        'streamWriter.Write(strNewJson)
        'streamWriter.Flush()
        'Dim httpResponse = CType(httpWebRequest.GetResponse, HttpWebResponse)
        'Dim streamReader = New StreamReader(httpResponse.GetResponseStream)
        'result = streamReader.ReadToEnd

        Return result

    End Function


End Class




