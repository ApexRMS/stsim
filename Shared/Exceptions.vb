'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' The STSim Exception
''' </summary>
''' <remarks></remarks>
<Serializable()>
Public NotInheritable Class STSimException
    Inherits Exception

    Public Sub New()
        MyBase.New("STSim exception")
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class

<Serializable()>
Public NotInheritable Class STSimMapDuplicateItemException
    Inherits Exception

    Public Sub New()
        MyBase.New("Duplicate Item Exception")
    End Sub

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class


