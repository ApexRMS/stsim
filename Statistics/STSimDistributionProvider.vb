'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Public NotInheritable Class STSimDistributionProvider
    Inherits DistributionProvider

    Private m_DistributionValues As New DistributionValueCollection
    Private m_DistributionValueMap As STSimDistributionValueMap

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal randomGenerator As RandomGenerator)

        MyBase.New(scenario, randomGenerator)

        Me.FillDistributionValueCollection()
        Me.CreateDistributionValueMap()

    End Sub

    Public ReadOnly Property Values As DistributionValueCollection
        Get
            Return Me.m_DistributionValues
        End Get
    End Property

    Public Sub STSimInitializeDistributionValues()

        For Each Value As STSimDistributionValue In Me.m_DistributionValues
            Value.Initialize(Me)
        Next

    End Sub

    Public Function STSimSample(
        ByVal distributionTypeId As Integer,
        ByVal distributionMean As Nullable(Of Double),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMinimum As Nullable(Of Double),
        ByVal distributionMaximum As Nullable(Of Double),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer)) As Double

        Return Me.STSimInternalSample(
            distributionTypeId,
            distributionMean,
            distributionSD,
            distributionMinimum,
            distributionMaximum,
            iteration,
            timestep,
            stratumId,
            secondaryStratumId)

    End Function

    Private Function STSimInternalSample(
        ByVal distributionTypeId As Integer,
        ByVal distributionMean As Nullable(Of Double),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMinimum As Nullable(Of Double),
        ByVal distributionMaximum As Nullable(Of Double),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer)) As Double

        If (distributionTypeId = Me.BetaDistributionTypeId Or
            distributionTypeId = Me.NormalDistributionTypeId Or
            distributionTypeId = Me.UniformDistributionTypeId Or
            distributionTypeId = Me.UniformIntegerDistributionTypeId) Then

            Return MyBase.Sample(
                distributionTypeId,
                distributionMean,
                distributionSD,
                distributionMinimum,
                distributionMaximum,
                iteration,
                timestep)

        Else

            Return Me.STSimGetUserDistribution(
                distributionTypeId,
                iteration,
                timestep,
                stratumId,
                secondaryStratumId)

        End If

    End Function

    Private Sub FillDistributionValueCollection()

        Debug.Assert(Me.m_DistributionValues.Count = 0)
        Dim ds As DataSheet = Me.Scenario.GetDataSheet(DISTRIBUTION_VALUE_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData().Rows

            Try

                Dim ValueDistributionFrequency As Nullable(Of DistributionFrequency) = Nothing

                If (dr(DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                    ValueDistributionFrequency = CType(dr(DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME), DistributionFrequency)
                End If

                Dim Item As New STSimDistributionValue(
                    DataTableUtilities.GetNullableInt(dr, DATASHEET_ITERATION_COLUMN_NAME),
                    DataTableUtilities.GetNullableInt(dr, DATASHEET_TIMESTEP_COLUMN_NAME),
                    DataTableUtilities.GetNullableInt(dr, DATASHEET_STRATUM_ID_COLUMN_NAME),
                    DataTableUtilities.GetNullableInt(dr, DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME),
                    DataTableUtilities.GetNullableInt(dr, DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME),
                    CInt(dr(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME)),
                    DataTableUtilities.GetNullableInt(dr, DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME),
                    DataTableUtilities.GetNullableInt(dr, DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME),
                    ValueDistributionFrequency,
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME),
                    DataTableUtilities.GetNullableDouble(dr, DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME))

                Me.Validate(
                    Item.ValueDistributionTypeId,
                    Item.Value,
                    Item.ValueDistributionSD,
                    Item.ValueDistributionMin,
                    Item.ValueDistributionMax)

                Me.m_DistributionValues.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    Private Sub CreateDistributionValueMap()

        Debug.Assert(Me.m_DistributionValueMap Is Nothing)
        Me.m_DistributionValueMap = New STSimDistributionValueMap()

        For Each Value As STSimDistributionValue In Me.m_DistributionValues
            Me.m_DistributionValueMap.AddValue(Value)
        Next

    End Sub

    Private Function STSimGetUserDistribution(
        ByVal distributionTypeId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer)) As Double

        Const SAMPLE_ERROR As String =
            "Attempted to sample from a distribution that has no corresponding distribution values in the scenario.  More information:" & vbCrLf &
            "Type={0}, Iteration={1}, Timestep={2}, Stratum={3}, SecondaryStratum={4}"

        Dim Values As DistributionValueCollection =
            Me.m_DistributionValueMap.GetValues(distributionTypeId, iteration, timestep, stratumId, secondaryStratumId)

        If (Values Is Nothing) Then

            Me.ThrowNoValuesException(
                SAMPLE_ERROR,
                distributionTypeId,
                iteration,
                timestep,
                stratumId,
                secondaryStratumId)

        End If

        Return MyBase.SampleUserDistribution(
            Values,
            distributionTypeId,
            iteration,
            timestep)

    End Function

    Private Function GetProjectItemName(ByVal dataSheetName As String, ByVal id As Integer) As String

        Dim ds As DataSheet = Me.Scenario.Project.GetDataSheet(dataSheetName)
        Return ds.ValidationTable.GetDisplayName(id)

    End Function

    Private Sub ThrowNoValuesException(
        ByVal message As String,
        ByVal distributionTypeId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer))

        Dim StratumName As String = "NULL"
        Dim SecondaryStratumName As String = "NULL"

        If (stratumId.HasValue) Then
            StratumName = Me.GetProjectItemName(DATASHEET_STRATA_NAME, stratumId.Value)
        End If

        If (secondaryStratumId.HasValue) Then
            SecondaryStratumName = Me.GetProjectItemName(DATASHEET_SECONDARY_STRATA_NAME, secondaryStratumId.Value)
        End If

        ExceptionUtils.ThrowInvalidOperationException(
            message,
            Me.GetProjectItemName(DISTRIBUTION_TYPE_DATASHEET_NAME, distributionTypeId),
            iteration,
            timestep,
            StratumName,
            SecondaryStratumName)

    End Sub

End Class
