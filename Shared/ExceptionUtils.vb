'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************
Imports System.Globalization

Module ExceptionUtils

    ''' <summary>
    ''' Throws an argument exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowArgumentException(message As [String])
        ThrowArgumentException(message, New Object() {})
    End Sub

    ''' <summary>
    ''' Throws an argument exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowArgumentException(message As [String], ParamArray args As Object())
        Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, message, args))
    End Sub

    ''' <summary>
    ''' Throws an invalid operation exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowInvalidOperationException(message As [String], ParamArray args As Object())
        Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, message, args))
    End Sub

End Module
