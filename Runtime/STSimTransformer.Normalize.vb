'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Drawing
Imports System.Globalization
Imports SyncroSim.Core

Partial Class STSimTransformer

    ''' <summary>
    ''' Normalizes the run control data feeds
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeRunControl()

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_RUN_CONTROL_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then
            dr = ds.GetData().NewRow()
            ds.GetData().Rows.Add(dr)
        End If

        If (dr(RUN_CONTROL_MIN_ITERATION_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_MIN_ITERATION_COLUMN_NAME) = 1
        End If

        If (dr(RUN_CONTROL_MAX_ITERATION_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_MAX_ITERATION_COLUMN_NAME) = 1
            Me.AddStatusRecord(StatusRecordType.Warning, STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING)
        End If

        If (dr(RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME) = 0
        End If

        If (dr(RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME) Is DBNull.Value) Then
            dr(RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME) = 5
            Me.AddStatusRecord(StatusRecordType.Warning, STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING)
        End If

    End Sub

    ''' <summary>
    ''' Normalizes the output options data feed
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeOutputOptions()

        Dim dsrc As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_RUN_CONTROL_NAME)
        Dim drrc As DataRow = dsrc.GetDataRow()

        Dim MaxTimestep As Integer = CInt(drrc(RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME))

        Dim dsoo As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_OO_NAME)
        Dim droo As DataRow = dsoo.GetDataRow()

        If (droo Is Nothing) Then
            droo = dsoo.GetData().NewRow()
            dsoo.GetData().Rows.Add(droo)
        End If

        Dim AnySummaryOutput As Boolean = AnyNonSpatialOutputOptionsSelected(droo)
        Dim AnySpatialOutput As Boolean = AnySpatialOutputOptionsSelected(droo)

        If (Not AnySummaryOutput And Not AnySpatialOutput) Then

            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME, CInt(True))
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME, CInt(True))
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME, CInt(True))
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME, CInt(True))
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME, CInt(True))

            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, 1)
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, 1)
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME, 1)
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, 1)
            DataTableUtilities.SetRowValue(droo, DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, 1)

            If (Me.IsSpatial()) Then

                DataTableUtilities.SetRowValue(droo, DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME, CInt(True))
                DataTableUtilities.SetRowValue(droo, DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, MaxTimestep)
                DataTableUtilities.SetRowValue(droo, DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME, CInt(True))
                DataTableUtilities.SetRowValue(droo, DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME, MaxTimestep)

            End If

            Me.AddStatusRecord(StatusRecordType.Information, STATUS_NO_OUTPUT_OPTIONS_WARNIING)

        End If

        Me.ValidateTimesteps(droo, DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME, DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, "Summary state classes", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME, DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, "Summary transitions", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME, "Summary transitions by state class", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME, DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, "Summary state attributes", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME, DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, "Summary transition attributes", MaxTimestep)

        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, "Raster state classes", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, "Raster transitions", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME, "Raster ages", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME, "Raster Time-since-transition", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME, "Raster stratum", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, "Raster state attributes", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, "Raster transition attributes", MaxTimestep)
        Me.ValidateTimesteps(droo, DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME, DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME, "Raster average annual transition probability", MaxTimestep)

        Me.ValidateStateAttributes(droo)
        Me.ValidateTransitionAttributes(droo)

    End Sub

    ''' <summary>
    ''' Normalizes the Initial conditions
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeInitialConditions()

        If Me.IsSpatial Then

            ' See if we're going to run Spatially based on ONLY on Non spatial initial conditions
            Dim NoSpatialIC As Boolean = Not (Me.ResultScenario.GetDataSheet(DATASHEET_SPIC_NAME).HasData())

            Me.NormalizeNonSpatialInitialConditions(False)
            Me.FillInitialConditionsDistributionMap()

            ' See if we're going to run Spatially based on Non spatial initial conditions
            If NoSpatialIC Then

                ' If no Spatial IC records for this Scenario, we're using Non spatial initial conditions.
                Me.AddStatusRecord(StatusRecordType.Information, STATUS_SPATIAL_RUN_USING_NONSPATIAL_IC)
                Me.CreateSpatialICFromNonSpatialIC()

            Else
                Me.FillInitialConditionsSpatialMap()
                Me.CreateSpatialICFromCombinedIC()
            End If

        Else
            Me.NormalizeNonSpatialInitialConditions(True)
        End If

    End Sub

    ''' <summary>
    ''' Normalizes the Non Spatial initial conditions data sheets
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeNonSpatialInitialConditions(verboseStatus As Boolean)

        Dim ICNS_Sheet As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME)
        Dim ICNS_Row As DataRow = ICNS_Sheet.GetDataRow()
        Dim ICNSDist_Table As DataTable = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME).GetData()
        Dim DeterministicTransitions_Table As DataTable = Me.ResultScenario.GetDataSheet(DATASHEET_DT_NAME).GetData()
        Dim Strata_Sheet As DataSheet = Me.ResultScenario.Project.GetDataSheet(DATASHEET_STRATA_NAME)

        'Add a row if necessary 

        If (ICNS_Row Is Nothing) Then
            ICNS_Row = ICNS_Sheet.GetData().NewRow()
            ICNS_Sheet.GetData().Rows.Add(ICNS_Row)
        End If

        'Total Amount

        If (ICNS_Row(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME) Is DBNull.Value) Then

            ICNS_Row(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME) = DATASHEET_RUN_CONTROL_TOTAL_AMOUNT_DEFAULT

            If verboseStatus Then
                Me.AddStatusRecord(StatusRecordType.Warning, STATUS_USING_DEFAULT_TOTAL_AMOUNT_WARNING)
            End If

        End If

        'Num Cells

        If (ICNS_Row(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME) Is DBNull.Value) Then

            If Me.IsSpatial Then
                ICNS_Row(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME) = DATASHEET_RUN_CONTROL_SPATIAL_NUM_CELLS_DEFAULT
            Else
                ICNS_Row(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME) = DATASHEET_RUN_CONTROL_NON_SPATIAL_NUM_CELLS_DEFAULT
            End If

            If verboseStatus Then
                Me.AddStatusRecord(StatusRecordType.Warning, STATUS_USING_DEFAULT_NUM_CELLS_WARNING)
            End If

        End If

        'Initial Conditions Non-Spatial Distribution.  If there are already rows in this table then
        'don't do anything, but if not then attempt to populate it using the data in deterministic transitions.

        If (ICNSDist_Table.Rows.Count > 0) Then
            Return
        End If

        If verboseStatus Then
            Me.AddStatusRecord(StatusRecordType.Warning, STATUS_NO_INITIAL_CONDITIONS_WARNING)
        End If

        If (DeterministicTransitions_Table.Rows.Count = 0) Then
            Return
        End If

        'If a deterministic transition has an explicit stratum we want to use its data to create
        'a new record in the distribution.  And if a deterministic transition has a NULL stratum
        'then we want to a new record in the distribution for each stratum.  Note, however, that
        'we want to favor explicit strata over NULL strata.

        Dim ExplicitStrata As New Dictionary(Of String, Boolean)

        For Each dr As DataRow In DeterministicTransitions_Table.Rows

            If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim StratumId As Integer = CInt(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME))
                Dim StateClassId As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))
                Dim Key As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", StratumId, StateClassId)

                ExplicitStrata.Add(Key, True)

                Dim NewRow As DataRow = ICNSDist_Table.NewRow

                NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = StratumId
                NewRow(DATASHEET_STATECLASS_ID_COLUMN_NAME) = StateClassId
                NewRow(DATASHEET_AGE_MIN_COLUMN_NAME) = dr(DATASHEET_AGE_MIN_COLUMN_NAME)
                NewRow(DATASHEET_AGE_MAX_COLUMN_NAME) = dr(DATASHEET_AGE_MAX_COLUMN_NAME)
                NewRow(DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME) = 1.0

                ICNSDist_Table.Rows.Add(NewRow)

            End If

        Next

        For Each dr As DataRow In DeterministicTransitions_Table.Rows

            If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) Is DBNull.Value) Then

                For Each drst As DataRow In Strata_Sheet.GetData().Rows

                    If (drst.RowState <> DataRowState.Deleted) Then

                        Dim StratumId As Integer = CInt(drst(Strata_Sheet.PrimaryKeyColumn.Name))
                        Dim StateClassId As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))
                        Dim Key As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", StratumId, StateClassId)

                        If (Not ExplicitStrata.ContainsKey(Key)) Then

                            Dim NewRow As DataRow = ICNSDist_Table.NewRow

                            NewRow(DATASHEET_STRATUM_ID_COLUMN_NAME) = StratumId
                            NewRow(DATASHEET_STATECLASS_ID_COLUMN_NAME) = StateClassId
                            NewRow(DATASHEET_AGE_MIN_COLUMN_NAME) = dr(DATASHEET_AGE_MIN_COLUMN_NAME)
                            NewRow(DATASHEET_AGE_MAX_COLUMN_NAME) = dr(DATASHEET_AGE_MAX_COLUMN_NAME)
                            NewRow(DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME) = 1.0

                            ICNSDist_Table.Rows.Add(NewRow)

                        End If

                    End If

                Next

            End If

        Next

    End Sub

    ''' <summary>
    ''' Determines if any non-spatial output options are selected
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AnyNonSpatialOutputOptionsSelected(ByVal dr As DataRow) As Boolean

        If (dr(DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME) IsNot DBNull.Value) Then

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' Determines if any spatial output options are selected
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AnySpatialOutputOptionsSelected(ByVal dr As DataRow) As Boolean

        If (dr(DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME) IsNot DBNull.Value Or
            dr(DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME) IsNot DBNull.Value) Then

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' Validates the state attribute output option selection
    ''' </summary>
    ''' <param name="dr">The output options data row</param>
    ''' <remarks>
    ''' If state attributes exist but the option to output them is not enabled, log a warning.
    ''' </remarks>
    Private Sub ValidateStateAttributes(ByVal dr As DataRow)

        If (dr(DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME) Is DBNull.Value And
            dr(DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME) Is DBNull.Value) Then

            Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_VALUE_NAME)

            If (ds.GetData().Rows.Count > 0) Then
                Me.AddStatusRecord(StatusRecordType.Information, STATUS_STATE_ATTRIBUTE_VALUES_EXIST_WARNING)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Validates the transition attribute output option selection
    ''' </summary>
    ''' <param name="dr">The output options data row</param>
    ''' <remarks>
    ''' If transition attributes exist but the option to output them is not enabled, log a warning.
    ''' </remarks>
    Private Sub ValidateTransitionAttributes(ByVal dr As DataRow)

        If (dr(DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME) Is DBNull.Value And
            dr(DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME) Is DBNull.Value) Then

            Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME)

            If (ds.GetData().Rows.Count > 0) Then
                Me.AddStatusRecord(StatusRecordType.Information, STATUS_TRANSITION_ATTRIBUTE_VALUES_EXIST_WARNING)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Validates the timesteps for the specified column name and maximum timestep
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="optionColumnName"></param>
    ''' <param name="timestepsColumnName"></param>
    ''' <param name="timestepsColumnHeaderText"></param>
    ''' <param name="maxTimestep"></param>
    ''' <remarks></remarks>
    Private Sub ValidateTimesteps(
        ByVal dr As DataRow,
        ByVal optionColumnName As String,
        ByVal timestepsColumnName As String,
        ByVal timestepsColumnHeaderText As String,
        ByVal maxTimestep As Integer)

        If (dr(optionColumnName) Is DBNull.Value) Then
            Return
        End If

        If (Not CBool(dr(optionColumnName))) Then
            Return
        End If

        If (dr(timestepsColumnName) Is DBNull.Value) Then

            Dim message As String = String.Format(CultureInfo.CurrentCulture,
                "ST-Sim timestep value for '{0}' is invalid.  Using default.", timestepsColumnHeaderText)

            Me.AddStatusRecord(StatusRecordType.Warning, message)

            dr(timestepsColumnName) = 5
            Return

        End If

        Dim val As Integer = CInt(dr(timestepsColumnName))

        If (val > maxTimestep) Then

            Dim message As String = String.Format(CultureInfo.CurrentCulture,
                "ST-Sim timestep value for '{0}' out of range.  Using default.", timestepsColumnHeaderText)

            Me.AddStatusRecord(StatusRecordType.Warning, message)

            dr(timestepsColumnName) = maxTimestep
            Return

        End If

    End Sub

    ''' <summary>
    ''' Normalizes the color data for any color data feeds
    ''' </summary>
    ''' <remarks>
    ''' If there are records but no colors at all then randomly add from a palette.  Note that if any
    ''' records have color data then we don't change anything.
    ''' </remarks>
    Private Sub NormalizeColorData()

        Debug.Assert(Me.IsSpatial)

        NormalizeColorData(Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME), DATASHEET_COLOR_COLUMN_NAME)
        NormalizeColorData(Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME), DATASHEET_COLOR_COLUMN_NAME)
        NormalizeColorData(Me.Project.GetDataSheet(DATASHEET_AGE_GROUP_NAME), DATASHEET_COLOR_COLUMN_NAME)

    End Sub

    Private Sub NormalizeColorData(ByVal ds As DataSheet, ByVal colorColumnName As String)

        If (Me.Session.IsRunningOnMono) Then
            Return
        End If

        Dim rnd As New Random
        Dim dt As DataTable = ds.GetData()

        Dim colors() As System.Drawing.Color =
        {
            Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange,
            Color.Teal, Color.Brown, Color.Purple, Color.Gray, Color.Cyan
        }

        If (dt.DefaultView.Count = 0) Then
            Return
        End If

        For Each dr As DataRow In dt.Rows

            If (dr.RowState <> DataRowState.Deleted) Then

                If (dr(colorColumnName) IsNot DBNull.Value) Then
                    Return
                End If

            End If

        Next

        Dim availableColors = colors.ToList()

        For Each dr As DataRow In dt.Rows

            If (dr.RowState <> DataRowState.Deleted) Then

                Dim Index As Integer = rnd.Next(availableColors.Count)
                dr(colorColumnName) = ColorUtilities.StringFromColor(availableColors(Index))

                'Remove used color from the list of availables. 
                'If depleted, recharge with a fresh list

                availableColors.RemoveAt(Index)

                If availableColors.Count = 0 Then
                    availableColors = colors.ToList()
                End If

            End If

        Next

        ds.Changes.Add(New ChangeRecord(Me, "Normalized Color Data"))

        Dim msg As String = String.Format(CultureInfo.CurrentCulture,
            "Color values not specified for '{0}.'  Using defaults.", ds.DisplayName)

        Me.AddStatusRecord(StatusRecordType.Information, msg)

    End Sub

    ''' <summary>
    ''' Normalizes the ID values for Stratums and State Class.
    ''' </summary>
    ''' <remarks>
    ''' If there are records with no ID then add sequenctial ID values.  
    ''' </remarks>
    Private Sub NormalizeMapIds()

        If (Me.IsSpatial) Then

            NormalizeMapIds(Me.Project.GetDataSheet(DATASHEET_STRATA_NAME))
            NormalizeMapIds(Me.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME))
            NormalizeMapIds(Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME))
            NormalizeMapIds(Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME))

        End If

    End Sub

    Private Sub NormalizeMapIds(ByVal ds As DataSheet)

        Dim dt As DataTable = ds.GetData()

        If (dt.DefaultView.Count = 0) Then
            Return
        End If

        Dim index As Integer = 0
        ' Grab the max value already in use, if any exist
        For Each dr As DataRow In dt.Rows

            If (dr(DATASHEET_MAPID_COLUMN_NAME) IsNot DBNull.Value) Then
                index = Math.Max(index, CType(dr(DATASHEET_MAPID_COLUMN_NAME), Integer))
            End If
        Next

        Dim noIDFound As Boolean = False
        For Each dr As DataRow In dt.Rows

            If (dr(DATASHEET_MAPID_COLUMN_NAME) Is DBNull.Value) Then
                noIDFound = True
                index += 1
                dr(DATASHEET_MAPID_COLUMN_NAME) = index

            End If
        Next

        If noIDFound Then
            Dim msg As String = String.Format(CultureInfo.CurrentCulture,
                "ID values not specified for '{0}.'  Using defaults.", ds.DisplayName)

            Me.AddStatusRecord(StatusRecordType.Information, msg)
        End If

    End Sub

    ''' <summary>
    ''' If a collection item (a.) references a user distribution value, and (b.) has a NULL
    ''' for stratum and/or secondary stratum then we want to replace that item with an "expanded"
    ''' set of items with explicit values based on the contents of the distribution value collection.
    '''      
    ''' NOTE: The expanded set of records should contain only unique combinations of stratum and
    '''       secondary stratum, null-able values inclusive.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NormalizeForUserDistributions()

        If (Me.m_DistributionProvider.Values.Count > 0) Then

            Dim Expander As New STSimDistributionBaseExpander(Me.m_DistributionProvider)

            Me.ExpandTransitionTargets(Expander)
            Me.ExpandTransitionMultipliers(Expander)
            Me.ExpandTransitionAttributeTargets(Expander)
            Me.ExpandTransitionDirectionMultipliers(Expander)
            Me.ExpandTransitionSlopeMultipliers(Expander)
            Me.ExpandTransitionAdjacencyMultipliers(Expander)

        End If

    End Sub

    Private Sub ExpandTransitionTargets(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionTargets.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionTargets)

            Me.m_TransitionTargets.Clear()

            For Each t As TransitionTarget In NewItems
                Me.m_TransitionTargets.Add(t)
            Next

        End If

    End Sub

    Private Sub ExpandTransitionMultipliers(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionMultiplierValues.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionMultiplierValues)

            Me.m_TransitionMultiplierValues.Clear()

            For Each t As TransitionMultiplierValue In NewItems
                Me.m_TransitionMultiplierValues.Add(t)
            Next

        End If

    End Sub

    Private Sub ExpandTransitionAttributeTargets(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionAttributeTargets.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionAttributeTargets)

            Me.m_TransitionAttributeTargets.Clear()

            For Each t As TransitionAttributeTarget In NewItems
                Me.m_TransitionAttributeTargets.Add(t)
            Next

        End If

    End Sub

    Private Sub ExpandTransitionDirectionMultipliers(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionDirectionMultipliers.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionDirectionMultipliers)

            Me.m_TransitionDirectionMultipliers.Clear()

            For Each t As TransitionDirectionMultiplier In NewItems
                Me.m_TransitionDirectionMultipliers.Add(t)
            Next

        End If

    End Sub

    Private Sub ExpandTransitionSlopeMultipliers(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionSlopeMultipliers.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionSlopeMultipliers)

            Me.m_TransitionSlopeMultipliers.Clear()

            For Each t As TransitionSlopeMultiplier In NewItems
                Me.m_TransitionSlopeMultipliers.Add(t)
            Next

        End If

    End Sub

    Private Sub ExpandTransitionAdjacencyMultipliers(ByVal expander As STSimDistributionBaseExpander)

        If (Me.m_TransitionAdjacencyMultipliers.Count > 0) Then

            Dim NewItems As IEnumerable(Of STSimDistributionBase) =
                expander.Expand(Me.m_TransitionAdjacencyMultipliers)

            Me.m_TransitionAdjacencyMultipliers.Clear()

            For Each t As TransitionAdjacencyMultiplier In NewItems
                Me.m_TransitionAdjacencyMultipliers.Add(t)
            Next

        End If

    End Sub

End Class
