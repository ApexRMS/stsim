'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Class AgeHelper

    Private m_IsEnabled As Boolean
    Private m_Frequency As Integer
    Private m_Maximum As Integer

    Const AGE_KEY_DEFAULT As Integer = 0
    Const MAX_AGE_CLASSES As Integer = 1000

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="isEnabled"></param>
    ''' <param name="frequency"></param>
    ''' <param name="maximum"></param>
    ''' <remarks></remarks>
    Public Sub New(
        ByVal isEnabled As Boolean,
        ByVal frequency As Integer,
        ByVal maximum As Integer)

        If (isEnabled) Then

            If (frequency <= 0) Then

                ExceptionUtils.ThrowArgumentException(
                    "The age reporting frequency must be greater than zero.")

            End If

            If (maximum < frequency) Then

                ExceptionUtils.ThrowArgumentException(
                    "The maximum age cannot be less than the age reporting frequency.")

            End If

        End If

        Me.m_IsEnabled = isEnabled
        Me.m_Frequency = frequency
        Me.m_Maximum = maximum

        If ((Me.m_Maximum / Me.m_Frequency) > MAX_AGE_CLASSES) Then
            Me.m_Maximum = (Me.m_Frequency * MAX_AGE_CLASSES)
        End If

    End Sub

    ''' <summary>
    ''' Gets a key for the specified age
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' If enabled then the key is the minimum age, otherwise it is the default.
    ''' </remarks>
    Public Function GetKey(ByVal age As Integer) As Integer

        If (Not Me.m_IsEnabled) Then
            Return AGE_KEY_DEFAULT
        Else
            Return Me.GetAgeMinimum(age).Value
        End If

    End Function

    ''' <summary>
    ''' Gets the minimum age
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgeMinimum(ByVal age As Integer) As Nullable(Of Integer)

        If (Not Me.m_IsEnabled) Then
            Return Nothing
        Else

            If (age > Me.m_Maximum) Then
                Return ((Me.m_Maximum \ Me.m_Frequency) * Me.m_Frequency)
            Else
                Return ((age \ Me.m_Frequency) * Me.m_Frequency)
            End If

        End If

    End Function

    ''' <summary>
    ''' Gets the reporting age maximum for the specified age
    ''' </summary>
    ''' <param name="age"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgeMaximum(ByVal age As Integer) As Nullable(Of Integer)

        If (Not Me.m_IsEnabled) Then
            Return Nothing
        Else

            Dim a As Nullable(Of Integer) = Me.GetAgeMinimum(age) + (Me.m_Frequency - 1)

            If (a.Value >= Me.m_Maximum) Then
                a = Nothing
            End If

            Return a

        End If

    End Function

    ''' <summary>
    ''' Gets an enumeration of age descriptors for the current configuration
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAges() As IEnumerable(Of AgeDescriptor)

        Dim v As Integer = 0
        Dim lst As New List(Of AgeDescriptor)

        While (v <= Me.m_Maximum)

            Dim min As Integer = Me.GetAgeMinimum(v).Value
            Dim max As Nullable(Of Integer) = Me.GetAgeMaximum(v)

            lst.Add(New AgeDescriptor(min, max))
            v += Me.m_Frequency

        End While

        Debug.Assert(lst.Count > 0)
        Debug.Assert(Not lst(lst.Count - 1).MaximumAge.HasValue)

        Return lst

    End Function

End Class

''' <summary>
''' Age Descriptor
''' </summary>
''' <remarks></remarks>
Class AgeDescriptor

    Private m_MinimumAge As Integer
    Private m_MaximumAge As Nullable(Of Integer)

    Public Sub New(
        ByVal minimumAge As Integer,
        ByVal maximumAge As Nullable(Of Integer))

        Me.m_MinimumAge = minimumAge
        Me.m_MaximumAge = maximumAge

    End Sub

    Public Property MinimumAge As Integer
        Get
            Return Me.m_MinimumAge
        End Get
        Set(value As Integer)
            Me.m_MinimumAge = value
        End Set
    End Property

    Public Property MaximumAge As Nullable(Of Integer)
        Get
            Return Me.m_MaximumAge
        End Get
        Set(value As Nullable(Of Integer))
            Me.m_MaximumAge = value
        End Set
    End Property

End Class
