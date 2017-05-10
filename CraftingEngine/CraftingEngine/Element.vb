Imports System.Globalization.CultureInfo

Public Class Element

    Implements IComparable(Of Element)

    Private name As String
    Private allRecepies As New List(Of Recepie)
    Private color As ConsoleColor

    Sub New(ByVal n As String, ByVal recepies As String, Optional ByVal c As ConsoleColor = ConsoleColor.White)

        name = n

        'Recepies are written as :element1,element2;element1,element2
        Dim EachRecepie As String() = recepies.Split(";")

        For Each r As String In EachRecepie

            Dim CR As String() = r.Split("+")

            If CR.Length > 2 Then
                Throw New Exception("Illegal crafting recepie!")
            ElseIf CR.Length > 1 Then
                allRecepies.Add(New Recepie(CR(0), CR(1)))
            End If

        Next

        'Define the console color
        color = c

    End Sub

    Function Compare(ByVal other As CraftingEngine.Element) As Integer Implements IComparable(Of CraftingEngine.Element).CompareTo
        Return name.CompareTo(other.name)
    End Function


    Function GetName() As String
        Return name
    End Function

    Function GetColor() As ConsoleColor
        Return color
    End Function

    'Write the receipies to the screen
    Sub DrawRecepies()
        Console.ForegroundColor = ConsoleColor.White
        Console.Write("Crafting recepie for ")
        Console.ForegroundColor = Me.color
        Console.WriteLine(CurrentCulture.TextInfo.ToTitleCase(name) & ":")
        Console.ForegroundColor = ConsoleColor.White

        For i As Integer = 0 To New String("Crafting Recepies for " & name & ":").Length - 1
            Console.Write("-")
        Next
        Console.WriteLine()


        If allRecepies.Count = 0 Then
            Console.ForegroundColor = ConsoleColor.Magenta
            Console.WriteLine(" *Uncraftable")
            Console.ForegroundColor = ConsoleColor.White
        Else

            For Each r As Recepie In allRecepies
                r.DrawRecepie()
            Next
        End If
        Console.WriteLine()

    End Sub

    'Get the recepies as a string
    Function GetRecepies() As String

        Dim returnString As String = "Crafting Recepies for " & name & ":" & Environment.NewLine

        For Each r As Recepie In allRecepies
            returnString += r.ToString & Environment.NewLine
        Next

        Return returnString

    End Function

    'Get the number of recepies
    Function GetReceipieCount() As Integer

        Return allRecepies.Count()

    End Function

    'Test if the element can be made with the current items
    Function TestElement(ByVal e1 As String, ByVal e2 As String) As Boolean

        For Each R As Recepie In allRecepies
            If R.TestRecepie(e1, e2) Then
                Return True
            End If
        Next

        Return False
    End Function

    Shared Function GetElementColor(ByVal n As String) As ConsoleColor
        For Each e As Element In AllElements

            If e.name.ToLower = n.ToLower Then
                Return e.color
            End If
        Next
        Return ConsoleColor.White
    End Function

    'Draw the element to the console
    Sub Draw(Optional ByVal NewLine As Boolean = True)
        Dim TempColor As ConsoleColor = Console.ForegroundColor
        Console.ForegroundColor = Me.color
        If NewLine = True Then
            Console.WriteLine(CurrentCulture.TextInfo.ToTitleCase(Me.name))
        Else
            Console.Write(CurrentCulture.TextInfo.ToTitleCase(Me.name))
        End If
        Console.ForegroundColor = TempColor
    End Sub


    'Holds a crafting recepie for an element
    Protected Friend Class Recepie
        Private element1 As String
        Private element2 As String

        Sub New(ByVal e1 As String, ByVal e2 As String)
            element1 = e1
            element2 = e2
        End Sub

        Function TestRecepie(ByVal e1 As String, ByVal e2 As String) As Boolean

            If (e1.ToLower = element1.ToLower And e2.ToLower = element2.ToLower) Or
                (e1.ToLower = element2.ToLower And e2.ToLower = element1.ToLower) Then
                Return True
            Else
                Return False
            End If

        End Function

        'Convert the recepie to a string
        Public Overrides Function ToString() As String
            Return element1 & "+" & element2
        End Function

        'Draw the colors to the console
        Public Sub DrawRecepie()
            Console.ForegroundColor = Element.GetElementColor(element1)
            Console.Write(CurrentCulture.TextInfo.ToTitleCase(element1))
            Console.ForegroundColor = ConsoleColor.White
            Console.Write(" + ")
            Console.ForegroundColor = Element.GetElementColor(element2)
            Console.WriteLine(CurrentCulture.TextInfo.ToTitleCase(element2))
        End Sub

    End Class
End Class
