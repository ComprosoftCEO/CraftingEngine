Imports System.IO

Module Main

    Public AllElements As New List(Of Element)
    Public UnlockedElements As New List(Of Element)
    Dim Rand As New Random()


    Private Const Base64 As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@~"

    Sub Main()

        Console.Title = "Crafting Engine"

        ReadElements()
        FillUnlocked()

        DrawTitle()

        Run()

    End Sub


#Region "Drawing"
    'Draw the message at the top
    Sub DrawTitle()
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine(" _____            __ _   _                _____            _            ")
        Console.WriteLine("/  __ \          / _| | (_)              |  ___|          (_)           ")
        Console.WriteLine("| /  \/_ __ __ _| |_| |_ _ _ __   __ _   | |__ _ __   __ _ _ _ __   ___ ")
        Console.WriteLine("| |   | '__/ _` |  _| __| | '_ \ / _` |  |  __| '_ \ / _` | | '_ \ / _ \")
        Console.WriteLine("| \__/\ | | (_| | | | |_| | | | | (_| |  | |__| | | | (_| | | | | |  __/")
        Console.WriteLine(" \____/_|  \__,_|_|  \__|_|_| |_|\__, |  \____/_| |_|\__, |_|_| |_|\___|")
        Console.WriteLine("                                  __/ |               __/ |             ")
        Console.WriteLine("(C) Comprosoft 2016              |___/               |___/              ")
        Console.WriteLine("  *Based on Little Alchemy by Jakub Koziol")
        Console.WriteLine(Environment.NewLine & Environment.NewLine)

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Type in a recepie with:")

        Console.ForegroundColor = ConsoleColor.Magenta
        Console.Write("   (Element)")
        Console.ForegroundColor = ConsoleColor.White
        Console.Write(" + ")
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("(Element)" & Environment.NewLine)

        Console.ForegroundColor = ConsoleColor.Gray
        Console.WriteLine("  Type 'list' to show available elements")
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("  Type 'help' to show additional help" & Environment.NewLine)

    End Sub

    'Draw information for the commands
    Private Sub DrawHelp()

        Console.Clear()

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("Help Screen:")
        Console.WriteLine("------------")

        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Type in a recepie with:")

        Console.ForegroundColor = ConsoleColor.Magenta
        Console.Write("   (Element)")
        Console.ForegroundColor = ConsoleColor.White
        Console.Write(" + ")
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("(Element)" & Environment.NewLine)

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine(Environment.NewLine & "All Commands:")
        Console.WriteLine("--------------")

        Console.ForegroundColor = ConsoleColor.Gray
        Console.WriteLine("  (Element) = show the crafting recepie(s) for that element")
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("  list [L] = show available elements")
        Console.WriteLine("     [L] (optional) = Starting letter(s), number(s), or symbol(s)")

        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("  hint = Show a random crafting recepie using unlocked elements")
        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.Blue
        Console.WriteLine("  save = Get the current password")
        Console.ForegroundColor = ConsoleColor.Magenta
        Console.WriteLine("  load = Enter a password to load progress")
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("  reset = reset game progress")

        Console.WriteLine("")

        Console.ForegroundColor = ConsoleColor.DarkCyan
        Console.WriteLine("  clear = clear the screen")
        Console.ForegroundColor = ConsoleColor.Gray
        Console.WriteLine("  help = display all commands" & Environment.NewLine())




    End Sub
#End Region



#Region "Reading"
    'Read the elements from my.resources
    Private Sub ReadElements()


        Dim Elements() As String = My.Resources.CraftingRecepies.Split(New String() {Environment.NewLine},
                                                                       StringSplitOptions.RemoveEmptyEntries)

        'Loop through each line in the text file
        For Each e As String In Elements

            'Remove any enters
            e = e.Replace(vbCr, "").Replace(vbLf, "")

            'Split the array 
            Dim TempArray1() As String = e.Split(":")

            'Exit the loop if the recepie is invalid
            If (TempArray1.Length < 2 Or TempArray1.Length > 2) Then
                GoTo Problem
            End If

            'Split the first part into two parts
            Dim TempArray2() As String = TempArray1(0).Split(",")


            'Now create the element
            Try
                AllElements.Add(New Element(TempArray2(0), TempArray1(1), GetConsoleColor(TempArray2(1))))
            Catch ex As Exception
                GoTo Problem
            End Try
        Next

        Exit Sub

Problem:
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine("Error reading crafting recepie file!")
        Console.ReadKey()
        End

    End Sub

    'Returns a console color from a string
    Function GetConsoleColor(ByVal input As String) As ConsoleColor
        input = input.ToLower
        Select Case input
            Case "black"
                Return ConsoleColor.Black
            Case "blue"
                Return ConsoleColor.Blue
            Case "cyan"
                Return ConsoleColor.Cyan
            Case "dblue", "dkblue", "d blue", "dk blue", "darkblue", "dark blue"
                Return ConsoleColor.DarkBlue
            Case "dcyan", "dkcyan", "d cyan", "dk cyan", "darkblue", "dark cyan"
                Return ConsoleColor.DarkCyan
            Case "dgray", "dkgray", "d gray", "dk gray", "darkgray", "dark gray"
                Return ConsoleColor.DarkGray
            Case "dgreen", "dkgreen", "d green", "dk green", "darkgreen", "dark green"
                Return ConsoleColor.DarkGreen
            Case "dmagenta", "dkmagenta", "d magenta", "dk magenta", "darkmagenta", "dark magenta"
                Return ConsoleColor.DarkMagenta
            Case "dred", "dkred", "d red", "dk red", "darkred", "dark red"
                Return ConsoleColor.DarkRed
            Case "dyellow", "dkyellow", "d yellow", "dk yellow", "darkyellow", "dark yellow"
                Return ConsoleColor.DarkYellow
            Case "gray"
                Return ConsoleColor.Gray
            Case "green"
                Return ConsoleColor.Green
            Case "magenta"
                Return ConsoleColor.Magenta
            Case "red"
                Return ConsoleColor.Red
            Case "white"
                Return ConsoleColor.White
            Case "yellow"
                Return ConsoleColor.Yellow
            Case Else
                Return ConsoleColor.White
        End Select
    End Function
#End Region


#Region "Listing and Crafting"
    'List all unlocked elements
    Sub ListElements(ByVal Letters As List(Of Char))

        UnlockedElements.Sort()
        Letters.Sort()

        If Letters.Count = 0 Then
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine(Environment.NewLine & "Available Elements:")
            For Each e As Element In UnlockedElements
                e.Draw()
            Next
            Console.WriteLine("")
        Else
            Console.WriteLine("")
            For Each c As Char In Letters

                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine("------" & c & "------")

                Dim Found As Boolean = False
                For Each e As Element In UnlockedElements
                    If e.GetName.Substring(0, 1).ToLower() = c.ToString().ToLower() Then
                        e.Draw()
                        Found = True
                    End If
                Next
                If Found = False Then
                    Console.ForegroundColor = ConsoleColor.Magenta
                    Console.WriteLine(" *No Elements Found")
                End If
                Console.WriteLine("")
            Next
        End If
    End Sub


    'Find the recepie of an element
    Sub FindReceipie(ByVal input As String)
        Console.WriteLine("")
        Dim isFound As Boolean = False
        For Each e As Element In UnlockedElements

            If e.GetName.ToLower = input.ToLower Then
                e.DrawRecepies()
                isFound = True
            End If

        Next

        If isFound = False Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Unknown element " + input & Environment.NewLine)
        End If
    End Sub


    'Craft the two elements together
    Sub Craft(ByVal element1 As String, ByVal element2 As String)

        Dim ToBreak As Boolean = False
        If TestUnlocked(element1) = False Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Environment.NewLine & "Unknown element " + element1)
            ToBreak = True
        End If

        If TestUnlocked(element2) = False Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Environment.NewLine & "Unknown element " + element2)
            ToBreak = True
        End If

        If ToBreak = True Then
            Console.WriteLine("")
            Exit Sub
        End If



        Dim didCraft As Boolean = False

        For Each e As Element In AllElements

            If e.TestElement(element1.ToLower, element2.ToLower) Then
                If didCraft = False Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.WriteLine(Environment.NewLine & "You crafted:")
                    didCraft = True
                End If
                e.Draw()

                If TestUnlocked(e.GetName) = False Then
                    UnlockedElements.Add(e)
                End If

            End If
        Next

        Console.WriteLine("")

        If didCraft = False Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("You didn't craft anything..." & Environment.NewLine)
        End If
    End Sub

    'Test if an element is already unlocked
    Function TestUnlocked(ByVal input As String) As Boolean
        For Each e As Element In UnlockedElements
            If e.GetName.ToLower = input.ToLower Then
                Return True
            End If
        Next
        Return False
    End Function


    'Test if all elements are already unlocked
    Function TestAllUnlocked() As Boolean

        If UnlockedElements.Count = AllElements.Count Then : Return True : Else : Return False : End If

    End Function


    'Display a random crafting recepie
    Sub Hint()

        'Pick two random elements
        Dim Rand As New Random()
TryAgain:
        Dim E1 As Element = UnlockedElements(Rand.Next(0, UnlockedElements.Count))
        Dim E2 As Element = UnlockedElements(Rand.Next(0, UnlockedElements.Count))

        'Try to figure out what you can create
        Dim didCraft As Boolean = False

        For Each e As Element In AllElements

            If e.TestElement(E1.GetName.ToLower, E2.GetName.ToLower) Then
                If didCraft = False Then
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(Environment.NewLine & "Try combining ")
                    E1.Draw(False)
                    Console.Write(" and ")
                    E2.Draw(True)
                    Console.WriteLine("")
                    didCraft = True
                    Exit Sub
                End If
            End If
        Next

        If didCraft = False Then
            GoTo TryAgain
        End If

    End Sub

#End Region


#Region "Saving and Loading"

    Private Function YesNo(Optional ByVal message As String = "Choose:",
                           Optional ByVal color As ConsoleColor = ConsoleColor.White) As Boolean

        Dim temp As ConsoleColor = Console.ForegroundColor
        Console.ForegroundColor = color
        Console.WriteLine(Environment.NewLine & message)

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("  (Y)es or (N)o")

        Console.ForegroundColor = temp

        'Force the user to press Y or N
        Dim h As System.ConsoleKeyInfo
        Do Until h.KeyChar.ToString.ToLower = "y" Or h.KeyChar.ToString.ToLower = "n"
            h = Console.ReadKey(True)
        Loop

        If h.KeyChar.ToString.ToLower = "y" Then
            Return True
        Else
            Console.WriteLine("")
            Return False
        End If

    End Function


    Private Sub Reset(Optional ByVal override As Boolean = False)

        If (override = True) OrElse (YesNo("Are you sure that you want to reset game progress?", ConsoleColor.Red)) Then

            UnlockedElements.Clear()
            FillUnlocked()
            Console.Clear()
            DrawTitle()

        End If

    End Sub

    'Fill in the default starting elements
    Private Sub FillUnlocked()

        'Compile the list of starting elements
        For i As Integer = 0 To 3 Step 1
            UnlockedElements.Add(AllElements.Item(i))
        Next


    End Sub


#Region "Strings"
    'Shuffle the string for the base 64 encryption
    Private Function ShuffleString(ByVal input As String, ByVal rand As Random) As String

        Dim strOutput As String = ""
        Dim intPlace As Integer

        While input.Length > 0

            intPlace = rand.Next(0, input.Length)
            strOutput += input.Substring(intPlace, 1)
            input = input.Remove(intPlace, 1)

        End While

        Return strOutput

    End Function

    'Do a pseudo xor to shuffle the string
    Private Function PseudoXor(ByVal input As String, ByVal seed As Integer)

        Dim AllChars As String = Base64

        Dim ReturnString As String = ""
        Dim rand As New Random(seed)

        For i = 0 To input.Length - 1 Step 1

            AllChars = ShuffleString(AllChars, rand)

            Dim CharIndex As Integer = AllChars.IndexOf(input.Substring(i, 1))

            ReturnString = ReturnString & "" & AllChars.Substring(AllChars.Length - (CharIndex) - 1, 1)

        Next


        Return ReturnString

    End Function

#End Region

    Private Function GetPassword() As String

        Dim RetString As String = ""

        'First, tell the computer how many characters are in the final output
        Dim High As Integer = Math.Floor(AllElements.Count / 64)
        Dim Low As Integer = AllElements.Count Mod 64
        RetString += Base64.Substring(High, 1) & Base64.Substring(Low, 1)

        'Create a binary array to store each value
        '  Round up to the nearest multiple of 6
        Dim IsUnlocked((Math.Ceiling(AllElements.Count / 6) * 6) - 1) As Boolean

        'Now, calculate each element enabled or disabled as a bit
        For i As Integer = 0 To AllElements.Count - 1
            IsUnlocked(i) = TestUnlocked(AllElements.Item(i).GetName)
        Next

        'Convert this boolean array into base 64 characters
        Dim AllNumbers(Math.Ceiling(Math.Ceiling(IsUnlocked.Length / 6) / 6) * 6 - 1) As Integer
        Dim j As Integer = 0
        For i As Integer = 0 To IsUnlocked.Count - 1 Step 6
            Dim Number As Integer = ToBase64(IsUnlocked(i), IsUnlocked(i + 1), IsUnlocked(i + 2), IsUnlocked(i + 3), IsUnlocked(i + 4), IsUnlocked(i + 5))
            AllNumbers(i / 6) = Number
        Next

        'Loop through the array of numbers and insert a checksum every sixth number
        For i As Integer = 0 To AllNumbers.Count - 1 Step 6

            'Figure out the checksum
            Dim CheckSum As Byte = 0
            CheckSum = CheckSum Xor AllNumbers(i)
            CheckSum = CheckSum Xor AllNumbers(i + 1)
            CheckSum = CheckSum Xor AllNumbers(i + 2)
            CheckSum = CheckSum Xor AllNumbers(i + 3)
            CheckSum = CheckSum Xor AllNumbers(i + 4)
            CheckSum = CheckSum Xor AllNumbers(i + 5)

            'Convert all of this into a string
            RetString += Base64.Substring(AllNumbers(i), 1)
            RetString += Base64.Substring(AllNumbers(i + 1), 1)
            RetString += Base64.Substring(AllNumbers(i + 2), 1)
            RetString += Base64.Substring(AllNumbers(i + 3), 1)
            RetString += Base64.Substring(AllNumbers(i + 4), 1)
            RetString += Base64.Substring(AllNumbers(i + 5), 1)

            'And add the checksum
            RetString += Base64.Substring(CheckSum, 1)
        Next


        'Shuffle the string using a random seed
        Dim Seed As Integer = Rand.Next(16777216)
        RetString = PseudoXor(RetString, Seed)


        'Place the seed onto the front four characters
        Dim Temp As Integer = Seed
        While Temp > 0
            RetString = Base64.Substring(Temp Mod 64, 1) & RetString
            Temp = Math.Floor(Temp / 64)
        End While

        'Store this string to the clipboard
        Windows.Forms.Clipboard.SetText(RetString)

        Return RetString

    End Function


    'Convert six booleans to an integer
    Private Function ToBase64(ByVal five As Boolean, ByVal four As Boolean,
                              ByVal three As Boolean, ByVal two As Boolean,
                              ByVal one As Boolean, ByVal zero As Boolean)
        Dim RetValue As Integer = 0
        If five Then : RetValue += 32 : End If
        If four Then : RetValue += 16 : End If
        If three Then : RetValue += 8 : End If
        If two Then : RetValue += 4 : End If
        If one Then : RetValue += 2 : End If
        If zero Then : RetValue += 1 : End If

        Return RetValue
    End Function


    'Convert the password 
    Private Sub LoadPassword()

Retry:
        Try
            'Ask the user to enter the password
            Console.Clear()
            Console.ForegroundColor = ConsoleColor.Magenta
            Console.WriteLine("Please enter the password:")
            Console.ForegroundColor = ConsoleColor.Yellow
            Dim Password As String = Console.ReadLine()

            Password = Password.Trim()

            If Password.Length = 0 Then
                GoTo Retry
            End If

            'First, unscramble the data
            Dim Seed As Integer = 0
            For i As Integer = 0 To 3
                Dim Val As Integer = Base64.IndexOf(Password.Substring(i, 1))

                If (Val = -1) Then : Throw New Exception : End If

                Seed += Val * Math.Pow(64, 3 - i)
            Next

            Password = PseudoXor(Password.Substring(4, Password.Length - 4), Seed)


            'With the password unscrambled, figure out how many bytes to count
            Dim High As Integer = Base64.IndexOf(Password.Substring(0, 1))
            Dim Low As Integer = Base64.IndexOf(Password.Substring(1, 1))

            'Shave the extra characters off the password
            Password = Password.Substring(2, Password.Length - 2)


            'Calculate the length
            Dim PWLength As Integer = High * 64 + Low


            'Verify that the password is the correct length
            Dim BytesLength As Integer = Math.Ceiling(Math.Ceiling(PWLength / 6) / 6) * 6
            Dim ChecksumLength As Integer = BytesLength / 6

            If Password.Length <> (BytesLength + ChecksumLength) Then
                Throw New Exception
            End If


            'Verify that this count matches the number of crafting recepies
            If Not PWLength = AllElements.Count Then : Throw New Exception : End If

            'Verify the checksum by converting the password into a number array
            Dim NewPassword As String = ""
            For i As Integer = 0 To Password.Length - 1 Step 7
                Dim Checksum As Byte = 0
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i, 1))
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i + 1, 1))
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i + 2, 1))
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i + 3, 1))
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i + 4, 1))
                Checksum = Checksum Xor Base64.IndexOf(Password.Substring(i + 5, 1))

                If (Checksum <> Base64.IndexOf(Password.Substring(i + 6, 1))) Then
                    Throw New Exception
                End If

                'Trim the string for the new password
                NewPassword += Password.Substring(i, 6)
            Next


            'Convert the data into a boolean array
            '   Round up to next multiple of 6
            Dim IsUnlocked((Math.Ceiling(PWLength / 6) * 6) - 1) As Boolean


            'Get the booleans for each of these characters
            Dim j As Integer = 0
            For i As Integer = 0 To IsUnlocked.Length - 1 Step 6

                IsUnlocked(i) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 5) And 1
                IsUnlocked(i + 1) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 4) And 1
                IsUnlocked(i + 2) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 3) And 1
                IsUnlocked(i + 3) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 2) And 1
                IsUnlocked(i + 4) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 1) And 1
                IsUnlocked(i + 5) = (Base64.IndexOf(NewPassword.Substring(j, 1)) >> 0) And 1

                j += 1
            Next

            'Finally, copy this array over and reset
            UnlockedElements.Clear()
            For i As Integer = 0 To PWLength - 1
                If IsUnlocked(i) = True Then
                    UnlockedElements.Add(AllElements.Item(i))
                End If
            Next

            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine(Environment.NewLine & "Data Successfully Imported!" & Environment.NewLine & "  Press any key to continue...")
            Console.ReadKey()

            Console.Clear()
            DrawTitle()

        Catch ex As Exception

            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Environment.NewLine & "Error loading data." & Environment.NewLine & "  Press any key to continue...")
            Console.ReadKey(True)

            Reset(True)

            Console.Clear()
            DrawTitle()

        End Try
    End Sub



#End Region






    'Runs the program
    Sub Run()

top:
        Console.ForegroundColor = ConsoleColor.White
        Console.Write(">")
        Dim input As String = Console.ReadLine()

        input = input.Trim()

        '--------------Special Commands----------------

        'First test for list
        If input.ToLower = "list" Then
            ListElements(New List(Of Char))
            GoTo top
        End If

        'Test for a letter search
        If (input.Split(" ")(0).ToLower() = "list") Then

            'Get all valid chars
            Dim ToSearchFor As New List(Of Char)
            Dim Break As Boolean = False
            For Each e As String In input.ToLower().Split(" ")
                e.Trim()
                If e.Length = 1 Then
                    ToSearchFor.Add(e)
                ElseIf Not (e = "list" Or e.Length = 0) Then
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.WriteLine(Environment.NewLine & "Invalid character " + e & Environment.NewLine)
                    Break = True
                End If
            Next
            If Break = False Then : ListElements(ToSearchFor.Distinct().ToList) : End If
            GoTo Top
        End If


        If (input.ToLower() = "hint") Then
            Hint()
            GoTo top
        End If


        If (input.ToLower() = "clear") Then
            Console.Clear()
            GoTo top
        End If


        If (input.ToLower() = "help") Then
            Console.WriteLine("")
            DrawHelp()
            GoTo top
        End If


        If (input.ToLower = "reset") Then
            Reset()
            GoTo top
        End If


        If (input.ToLower = "save") Then
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine(Environment.NewLine & "Your game password is:")
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine(GetPassword())

            Console.ForegroundColor = ConsoleColor.Magenta
            Console.WriteLine(Environment.NewLine & " *Password copied to clipboard" & Environment.NewLine)
            GoTo top
        End If


        If (input.ToLower = "load") Then
            If YesNo("Loading will erase previous saved data. Continue?", ConsoleColor.Magenta) Then
                LoadPassword()
            End If
            GoTo top
        End If

        '--------------Do Crafting---------------

        'Next, convert the list into an element
        Dim Inputs() = input.Split("+")

        'Test for invalid recepie
        If Inputs.Length > 2 Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(Environment.NewLine & "Bad crafting recepie!" & Environment.NewLine)
            GoTo top
        End If

        'Test for blank input
        If input.Length = 0 Then : GoTo top : End If

        'Test for find recepie
        If Inputs.Length = 1 Then
            FindReceipie(input.Trim())
            GoTo top
        End If

        Craft(Inputs(0).Trim(), Inputs(1).Trim())

        GoTo top
    End Sub


End Module
