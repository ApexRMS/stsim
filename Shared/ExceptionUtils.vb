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
    ''' Throws an argument Null exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowArgumentNullException(message As [String])
        ThrowArgumentNullException(message, New Object() {})
    End Sub

    ''' <summary>
    ''' Throws an argument Null exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowArgumentNullException(message As [String], ParamArray args As Object())
        Throw New ArgumentNullException(String.Format(CultureInfo.CurrentCulture, message, args))
    End Sub

    ''' <summary>
    ''' Throws an invalid operation exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowInvalidOperationException(message As [String])
        ThrowInvalidOperationException(message, New Object() {})
    End Sub

    ''' <summary>
    ''' Throws an invalid operation exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowInvalidOperationException(message As [String], ParamArray args As Object())
        Throw New InvalidOperationException(String.Format(CultureInfo.CurrentCulture, message, args))
    End Sub

    ''' <summary>
    ''' Throws a not implemented exception
    ''' </summary>
    ''' <param name="message"></param>
    Public Sub ThrowNotImplementedException(message As [String])
        ThrowNotImplementedException(message, New Object() {})
    End Sub

    ''' <summary>
    ''' Throws a not implemented exception
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="arg"></param>
    Public Sub ThrowNotImplementedException(message As [String], ParamArray args As Object())
        Throw New NotImplementedException(String.Format(CultureInfo.CurrentCulture, message, args))
    End Sub

End Module
