'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Module MathUtils

    ''' <summary>
    ''' Linear Interpolation Y Value Calculation
    ''' Y = ( ( X - X1 )( Y2 - Y1) / ( X2 - X1) ) + Y1
    ''' </summary>
    ''' <param name="x1">x value of 1st co-ordinate </param>
    ''' <param name="y1">y value of 1st co-ordinate</param>
    ''' <param name="x2">x value of 2nd co-ordinate</param>
    ''' <param name="y2">y value of 2nd co-ordinate</param>
    ''' <param name="x">X = Target x co-ordinate</param>
    ''' <returns> Y = Interpolated y co-ordinate</returns>
    ''' <remarks></remarks>
    Public Function Interpolate(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByVal x As Double) As Double

        Debug.Assert(x > x1 And x < x2)

        Dim rise As Double = y2 - y1
        Dim run As Double = x2 - x1
        Dim slope As Double = rise / run
        Dim y As Double = y1 + (x - x1) * slope

        Return y

    End Function

    Public Function CompareDoublesEqual(lhs As Double, rhs As Double, Optional epsilon As Double = [Double].Epsilon) As Boolean
        Return (Math.Abs(lhs - rhs) < epsilon)
    End Function

    Public Function CompareDoublesGTEqual(lhs As Double, rhs As Double, Optional epsilon As Double = [Double].Epsilon) As Boolean
        Return ((CompareDoublesEqual(lhs, rhs, epsilon) OrElse lhs > rhs))
    End Function

    Public Function CompareDoublesGT(lhs As Double, rhs As Double, Optional epsilon As Double = [Double].Epsilon) As Boolean
        Return ((Not CompareDoublesEqual(lhs, rhs, epsilon) AndAlso lhs > rhs))
    End Function

End Module
