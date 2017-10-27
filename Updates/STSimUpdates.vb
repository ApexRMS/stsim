'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.IO
Imports System.Reflection
Imports System.Globalization

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class STSimUpdates
    Inherits UpdateProvider

    Public Overrides Sub PerformUpdate(store As DataStore, currentSchemaVersion As Integer)

        If (currentSchemaVersion < 1) Then
            STSIM0000001(store)
            STSIM0000002(store)
        End If

        If (currentSchemaVersion < 2) Then
            STSIM0000003(store)
            STSIM0000004(store)
        End If

        If (currentSchemaVersion < 3) Then
            STSIM0000005(store)
            STSIM0000006(store)
        End If

        If (currentSchemaVersion < 4) Then
            STSIM0000007(store)
        End If

        If (currentSchemaVersion < 5) Then
            STSIM0000008(store)
        End If

        If (currentSchemaVersion < 6) Then
            STSIM0000009(store)
        End If

        If (currentSchemaVersion < 7) Then
            STSIM0000010(store)
        End If

        If (currentSchemaVersion < 8) Then
            STSIM0000011(store)
        End If

        If (currentSchemaVersion < 9) Then
            STSIM0000012(store)
        End If

        If (currentSchemaVersion < 10) Then
            STSIM0000013(store)
        End If

        If (currentSchemaVersion < 11) Then
            STSIM0000014(store)
        End If

        If (currentSchemaVersion < 12) Then
            STSIM0000015(store)
        End If

        If (currentSchemaVersion < 13) Then
            STSIM0000016(store)
        End If

        If (currentSchemaVersion < 14) Then
            STSIM0000017(store)
        End If

        If (currentSchemaVersion < 15) Then
            STSIM0000018(store)
        End If

        If (currentSchemaVersion < 16) Then
            STSIM0000019(store)
        End If

        If (currentSchemaVersion < 17) Then
            STSIM0000020(store)
        End If

        If (currentSchemaVersion < 18) Then
            STSIM0000021(store)
        End If

        If (currentSchemaVersion < 19) Then
            STSIM0000022(store)
        End If

        If (currentSchemaVersion < 20) Then
            STSIM0000023(store)
        End If

        If (currentSchemaVersion < 21) Then
            STSIM0000024(store)
        End If

        If (currentSchemaVersion < 22) Then
            STSIM0000025(store)
        End If

        If (currentSchemaVersion < 23) Then
            STSIM0000026(store)
        End If

        If (currentSchemaVersion < 24) Then
            STSIM0000027(store)
        End If

        If (currentSchemaVersion < 25) Then
            STSIM0000028(store)
        End If

        If (currentSchemaVersion < 26) Then
            STSIM0000029(store)
        End If

        If (currentSchemaVersion < 27) Then
            STSIM0000030(store)
        End If

        If (currentSchemaVersion < 28) Then
            STSIM0000031(store)
        End If

        If (currentSchemaVersion < 29) Then
            STSIM0000032(store)
        End If

        If (currentSchemaVersion < 30) Then

            UpdateSTSimTables_SSIM_V_1(store)
            UpdateStockFlowTables_SSIM_V_1(store)
            UpdateEcologicalDepartureTables_SSIM_V_1(store)
            UpdateTransitionProbabilityEstimatorTables_SSIM_V_1(store)
            UpdateDynamicMultiplierTables_SSIM_V_1(store)
            RenameInputDirectories_SSIM_V_1(store)

        End If

        'At this point, the library has been updated to be compatible with
        'SyncroSim version 1.

        If (currentSchemaVersion < 31) Then
            STSIM0000033(store)
        End If

        If (currentSchemaVersion < 32) Then
            STSIM0000034(store)
        End If

        'Due to legacy mistakes, the update function names have departed from the version number.
        'We are going to fix that here with a dummy update for clarity.  From this point on, the
        'ST-Sim schema version number will be 40 or greater.

        If (currentSchemaVersion < 40) Then
            STSIM0000040(store)
        End If

        If (currentSchemaVersion < 41) Then
            STSIM0000041(store)
        End If

        If (currentSchemaVersion < 42) Then
            STSIM0000042(store)
        End If

        If (currentSchemaVersion < 43) Then
            STSIM0000043(store)
        End If

        If (currentSchemaVersion < 44) Then
            STSIM0000044(store)
        End If

        If (currentSchemaVersion < 45) Then
            STSIM0000045(store)
        End If

        If (currentSchemaVersion < 46) Then
            STSIM0000046(store)
        End If

        If (currentSchemaVersion < 47) Then
            STSIM0000047(store)
        End If

        If (currentSchemaVersion < 48) Then
            STSIM0000048(store)
        End If

        If (currentSchemaVersion < 49) Then
            STSIM0000049(store)
        End If

        If (currentSchemaVersion < 50) Then
            STSIM0000050(store)
        End If

        If (currentSchemaVersion < 51) Then
            STSIM0000051(store)
        End If

        If (currentSchemaVersion < 52) Then
            STSIM0000052(store)
        End If

        If (currentSchemaVersion < 53) Then
            STSIM0000053(store)
        End If

        If (currentSchemaVersion < 54) Then
            STSIM0000054(store)
        End If

        If (currentSchemaVersion < 55) Then
            STSIM0000055(store)
        End If

        If (currentSchemaVersion < 56) Then
            STSIM0000056(store)
        End If

        If (currentSchemaVersion < 57) Then
            STSIM0000057(store)
        End If

        If (currentSchemaVersion < 58) Then
            STSIM0000058(store)
        End If

        If (currentSchemaVersion < 59) Then
            STSIM0000059(store)
        End If

        If (currentSchemaVersion < 60) Then
            STSIM0000060(store)
        End If

        If (currentSchemaVersion < 61) Then
            STSIM0000061(store)
        End If

        If (currentSchemaVersion < 62) Then
            STSIM0000062(store)
        End If

        If (currentSchemaVersion < 63) Then
            STSIM0000063(store)
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000001
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' In this update, we want to change the TransitionTypeID column to TransitionGroupID.  Since this column
    ''' has its AllowDBNull property set to False, we can't just change the name and delete the values.  Instead
    ''' we need to delete the entire table which will cause the system to recreate it as the library loads.
    ''' </remarks>
    Private Shared Sub STSIM0000001(ByVal store As DataStore)
        store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierValues")
    End Sub

    ''' <summary>
    ''' STSIM0000002
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' In this update, we want to change the TransitionTypeID column to TransitionGroupID and we want to delete
    ''' the MultiplierType column.  Since the TransitionTypeID column has its AllowDBNull property set to False, 
    ''' we can't just change the name and delete the values.  Instead we need to delete the entire table which 
    ''' will cause the system to recreate it as the library loads.
    ''' </remarks>
    Private Shared Sub STSIM0000002(ByVal store As DataStore)

        If (store.TableExists("ST_TransitionSpatialMultiplier")) Then
            store.ExecuteNonQuery("DROP TABLE ST_TransitionSpatialMultiplier")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000003
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' In this update we will add support for transition attribute types as follows:
    ''' 
    ''' (1.) Rename the table ST_AttributeType to ST_StateAttributeType
    ''' (2.) Rename the column ST_AttributeTypeID ST_StateAttributeTypeID in the table ST_StateAttributeType
    ''' (3.) Rename the AttributeTypeID column to StateAttributeTypeID in the table ST_OutputStratumStateAttribute
    ''' 
    ''' Note that some databases such as SQLite don't support column renaming, so we need to 
    ''' select the values from the old table into the new, and then drop the old.
    ''' 
    ''' Also note that the required transition attribute type tables will be created automatically by the system
    ''' </remarks>
    Private Shared Sub STSIM0000003(ByVal store As DataStore)

        '(1.) and (2.) above
        '-------------------
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeType(ST_StateAttributeTypeID INTEGER, ProjectID INTEGER, [Name] TEXT(255), AttributeGroupID INTEGER, Units TEXT(50), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeType(ST_StateAttributeTypeID, ProjectID, [Name], AttributeGroupID, Units, Description) SELECT ST_AttributeTypeID, ProjectID, [Name], AttributeGroupID, Units, Description FROM ST_AttributeType")
        store.ExecuteNonQuery("DROP TABLE ST_AttributeType")

        '(3.) above
        '-------------------
        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumStateAttribute_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumStateAttribute RENAME TO ST_OutputStratumStateAttributeTEMP")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, StateAttributeTypeID INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumStateAttribute(ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, AttributeTypeID, Amount FROM ST_OutputStratumStateAttributeTEMP")
        store.ExecuteNonQuery("DROP TABLE ST_OutputStratumStateAttributeTEMP")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumStateAttribute_Index ON ST_OutputStratumStateAttribute(ScenarioID, Iteration, Timestep)")

    End Sub

    ''' <summary>
    ''' STSIM0000004
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' In this update we will rename the following tables so their names respect the established naming conventions.
    ''' 
    ''' ST_TransitionMultiplierValues -> ST_TransitionMultiplierValue
    ''' ST_StateAttributeValues       -> ST_StateAttributeValue
    ''' 
    ''' Note that some databases such as SQLite don't support column renaming, so we need to 
    ''' select the values from the old table into the new, and then drop the old.
    ''' 
    ''' Also note that in STSIM0000001 we drop the table ST_TransitionMultiplierValues to force an automatic recreation.
    ''' Because of this, the table will not exist if STSIM0000001 is run in conjunction with STSIM0000004.  If the table
    ''' doesn't exist then we don't need to create the new table since it will be created by the system.
    ''' </remarks>
    Private Shared Sub STSIM0000004(ByVal store As DataStore)

        'ST_TransitionMultiplierValues -> ST_TransitionMultiplierValue
        '-------------------------------------------------------------
        If (store.TableExists("ST_TransitionMultiplierValues")) Then

            store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue(ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, TransitionMultiplierTypeID INTEGER, Iteration INTEGER, Timestep INTEGER, Mean DOUBLE, SD DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD FROM ST_TransitionMultiplierValues")
            store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierValues")

        End If

        'ST_StateAttributeValues       -> ST_StateAttributeValue
        '-------------------------------------------------------
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue(ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, StratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, AttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value FROM ST_StateAttributeValues")
        store.ExecuteNonQuery("DROP TABLE ST_StateAttributeValues")

    End Sub

    ''' <summary>
    ''' STSIM0000005
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds transition attribute support to Output Options
    ''' </remarks>
    Private Shared Sub STSIM0000005(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " &
            "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " &
            "ProjectID                   INTEGER, " &
            "ScenarioID                  INTEGER, " &
            "SummaryOutputSC             INTEGER, " &
            "SummaryOutputSCTimesteps    INTEGER, " &
            "SummaryOutputTR             INTEGER, " &
            "SummaryOutputTRTimesteps    INTEGER, " &
            "SummaryOutputTRIntervalMean INTEGER, " &
            "SummaryOutputTRSC           INTEGER, " &
            "SummaryOutputTRSCTimesteps  INTEGER, " &
            "SummaryOutputSA              INTEGER, " &
            "SummaryOutputSATimesteps     INTEGER, " &
            "SummaryOutputTA              INTEGER, " &
            "SummaryOutputTATimesteps     INTEGER, " &
            "RasterOutputSC              INTEGER, " &
            "RasterOutputSCTimesteps     INTEGER, " &
            "RasterOutputTR              INTEGER, " &
            "RasterOutputTRTimesteps     INTEGER, " &
            "RasterOutputAge             INTEGER, " &
            "RasterOutputAgeTimesteps    INTEGER, " &
            "RasterOutputTST             INTEGER, " &
            "RasterOutputTSTTimesteps    INTEGER, " &
            "RasterOutputST              INTEGER, " &
            "RasterOutputSTTimesteps     INTEGER)")

        store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC," &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps)" &
            " SELECT " &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC, " &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps" &
            " FROM ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP")

    End Sub

    ''' <summary>
    ''' STSIM0000006
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update renames the Output?Attribute tables so that they do not contain the word 'Stratum'.
    ''' Note that if this update may be applied to libraries that do not yet have a transition attribute table.
    ''' </remarks>
    Private Shared Sub STSIM0000006(ByVal store As DataStore)

        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumStateAttribute_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumStateAttribute RENAME TO ST_OutputStateAttribute")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)")

        If (store.TableExists("ST_OutputStratumTransitionAttribute")) Then

            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionAttribute_Index")
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransitionAttribute RENAME TO ST_OutputTransitionAttribute")
            store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000007
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add Distribution support to Transition Targets.  Specifically, it will 
    ''' add the folliwng columns to the Transition Target table:
    ''' 
    ''' SD           : DOUBLE     //Standard Deviation
    ''' Distribution : Integer
    ''' MinimumValue : Integer
    ''' MaximumValue : Integer
    ''' </remarks>
    Private Shared Sub STSIM0000007(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO ST_TransitionTargetTEMP")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount FROM ST_TransitionTargetTEMP")
        store.ExecuteNonQuery("DROP TABLE ST_TransitionTargetTEMP")

    End Sub

    ''' <summary>
    ''' STSIM0000008
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add Modeling Zone support to ST-Sim as follows:
    ''' 
    ''' (1.)  Add primary and secondary stratum label columns to the Terminology table
    ''' (2.)  Update all projects so that their terminology table contains the new values
    ''' (3.)  Add a SecondaryStratum column to the Initial Conditions Distribution table
    ''' (4.)  Add a SecondaryStratum column to the Transition Targets table
    ''' (5.)  Add a SecondaryStratum column to the Transition Attribute Targets table
    ''' (6.)  Add a SecondaryStratum column to the Transition Multiplier Values table
    ''' (7.)  Add a SecondaryStratumFilename column to the Initial Conditions Spatial table
    ''' (8.)  Add a SecondaryStratum column to the Output Stratum State table
    ''' (9.)  Add a SecondaryStratum column to the Output Stratum Transition table
    ''' (10.) Add a SecondaryStratum column to the Output Stratum Transition State table
    ''' (11.) Add a SecondaryStratum column to the Output State Attribute table
    ''' (12.) Add a SecondaryStratum column to the Output Transition Attribute table
    ''' 
    ''' NOTE: The SecondaryStratum table does not need to be created here because the system will auto create missing tables.
    ''' NOTE: The Transition Attribute Target table may not yet exist.  If so, we can skip the alteration below.
    ''' NOTE: The Output Transition Attribute table may not yet exist.  If so, we can skip the alteration below.
    ''' NOTE: The Output Transition Multiplier Value table may not yet exist.  If so, we can skip the alteration below.
    ''' NOTE: The indexes on the output tables need to be recreated if the table is recreated.
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000008(ByVal store As DataStore)

        '(1.) and (2.) above
        store.ExecuteNonQuery("ALTER TABLE ST_Terminology RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_Terminology (ST_TerminologyID INTEGER PRIMARY KEY, ProjectID INTEGER, AmountLabel TEXT( 50 ), AmountUnits INTEGER, StateLabelX TEXT( 50 ), StateLabelY TEXT( 50 ), PrimaryStratumLabel TEXT( 50 ), SecondaryStratumLabel Text(50))")
        store.ExecuteNonQuery("INSERT INTO ST_Terminology (ST_TerminologyID, ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel) SELECT ST_TerminologyID, ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, 'Vegetation Type', 'Planning Zone' FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(3.) Above
        store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_InitialConditionsNonSpatialDistribution (ST_InitialConditionsNonSpatialDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, RelativeAmount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_InitialConditionsNonSpatialDistribution(ProjectID, ScenarioID, StratumID, StateClassID, AgeMin, AgeMax, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, StateClassID, AgeMin, AgeMax, RelativeAmount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(4.) Above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount, MinimumValue, MaximumValue, SD, Distribution) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount, MinimumValue, MaximumValue, SD, Distribution FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(5.) Above
        If (store.TableExists("ST_TransitionAttributeTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionAttributeTypeID, Amount, MinimumValue, MaximumValue, SD, Distribution) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionAttributeTypeID, Amount, MinimumValue, MaximumValue, SD, Distribution FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(6.) Above
        If (store.TableExists("ST_TransitionMultiplierValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionMultiplierTypeID INTEGER, Iteration INTEGER, Timestep INTEGER, Mean DOUBLE, SD DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(7.) Above
        store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsSpatial RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_InitialConditionsSpatial (ST_InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, NumRows INTEGER, NumColumns INTEGER, CellSize SINGLE, CellSizeUnits TEXT( 20 ), CellArea DOUBLE, CellAreaOverride INTEGER, XLLCorner SINGLE, YLLCorner SINGLE, SRS TEXT(1024), StratumFileName TEXT(255), SecondaryStratumFilename TEXT(255), StateClassFilename TEXT(255), AgeFilename TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_InitialConditionsSpatial(ProjectID, ScenarioID, NumRows, NumColumns, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, StateClassFileName, AgeFileName) SELECT ProjectID, ScenarioID, NumRows, NumColumns, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFilename, StateClassFilename, AgeFilename FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(8.) Above
        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumState RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumState (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, StateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, StateClassID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumState_Index ON ST_OutputStratumState(ScenarioID, Iteration, Timestep)")

        '(9.) Above
        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransition RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransition (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, TransitionGroupID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionGroupID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransition_Index ON ST_OutputStratumTransition(ScenarioID, Iteration, Timestep)")

        '(10.) Above
        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionState_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransitionState RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransitionState (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionTypeID INTEGER, StateClassID INTEGER, EndStateClassID INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransitionState(ScenarioID, Iteration, Timestep, StratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransitionState_Index ON ST_OutputStratumTransitionState(ScenarioID, Iteration, Timestep)")

        '(11.) Above
        store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStateAttribute RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStateAttribute (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateAttributeTypeID INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)")

        '(12.) Above
        If (store.TableExists("ST_OutputTransitionAttribute")) Then

            store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index")
            store.ExecuteNonQuery("ALTER TABLE ST_OutputTransitionAttribute RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_OutputTransitionAttribute (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, TransitionAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionAttributeTypeID, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
            store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000009
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' A bug in the schema generation code was causing each project record to be created with an AUTOINCRMENT primary key.  Although this does not
    ''' create problems with the application, the AUTOINCREMENT constraint is incorrect and we are going to correct all affected tables here.
    ''' </remarks>
    Private Shared Sub STSIM0000009(ByVal store As DataStore)

        'Attribute Group
        store.ExecuteNonQuery("ALTER TABLE ST_AttributeGroup RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_AttributeGroup(ST_AttributeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_AttributeGroup(ST_AttributeGroupID, ProjectID, Name, Description) SELECT ST_AttributeGroupID, ProjectID, Name, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Patch Prioritization
        store.ExecuteNonQuery("ALTER TABLE ST_PatchPrioritization RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_PatchPrioritization(ST_PatchPrioritizationID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_PatchPrioritization(ST_PatchPrioritizationID, ProjectID, Name) SELECT ST_PatchPrioritizationID, ProjectID, Name FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'State Attribute Type
        store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeType RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeType (ST_StateAttributeTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), AttributeGroupID INTEGER, Units TEXT( 50 ), Description Text(255))")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeType(ST_StateAttributeTypeID, ProjectID, Name, AttributeGroupID, Units, Description) SELECT ST_StateAttributeTypeID, ProjectID, Name, AttributeGroupID, Units, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'State Class
        store.ExecuteNonQuery("ALTER TABLE ST_StateClass RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateClass (ST_StateClassID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), StateLabelXID INTEGER, StateLabelYID INTEGER, ID INTEGER, Description TEXT( 255 ))")
        store.ExecuteNonQuery("INSERT INTO ST_StateClass(ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description) SELECT ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'State Label X
        store.ExecuteNonQuery("ALTER TABLE ST_StateLabelX RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateLabelX(ST_StateLabelXID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_StateLabelX(ST_StateLabelXID, ProjectID, Name, Description) SELECT ST_StateLabelXID, ProjectID, Name, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'State Label Y
        store.ExecuteNonQuery("ALTER TABLE ST_StateLabelY RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateLabelY(ST_StateLabelYID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_StateLabelY(ST_StateLabelYID, ProjectID, Name, Description) SELECT ST_StateLabelYID, ProjectID, Name, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Stratum
        store.ExecuteNonQuery("ALTER TABLE ST_Stratum RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_Stratum(ST_StratumID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_Stratum(ST_StratumID, ProjectID, Name, ID, Description) SELECT ST_StratumID, ProjectID, Name, ID, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Group
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionGroup RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionGroup(ST_TransitionGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionGroup(ST_TransitionGroupID, ProjectID, Name, Description) SELECT ST_TransitionGroupID, ProjectID, Name, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Multiplier Type
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierType RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierType(ST_TransitionMultiplierTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierType(ST_TransitionMultiplierTypeID, ProjectID, Name) SELECT ST_TransitionMultiplierTypeID, ProjectID, Name FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Type
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionType RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionType(ST_TransitionTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionType(ST_TransitionTypeID, ProjectID, Name, ID, Description) SELECT ST_TransitionTypeID, ProjectID, Name, ID, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Type Group
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTypeGroup RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTypeGroup(ST_TransitionTypeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER, IsSecondary INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTypeGroup(ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary) SELECT ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

    End Sub

    ''' <summary>
    ''' STSIM0000010
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add color columns to the State Class and Transition Type tables
    ''' </remarks>
    Private Shared Sub STSIM0000010(ByVal store As DataStore)

        'State Class
        store.ExecuteNonQuery("ALTER TABLE ST_StateClass RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateClass (ST_StateClassID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), StateLabelXID INTEGER, StateLabelYID INTEGER, ID INTEGER, Color TEXT(20), Description TEXT( 255 ))")
        store.ExecuteNonQuery("INSERT INTO ST_StateClass(ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description) SELECT ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Type
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionType RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionType(ST_TransitionTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Color TEXT(20), Description TEXT(255))")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionType(ST_TransitionTypeID, ProjectID, Name, ID, Description) SELECT ST_TransitionTypeID, ProjectID, Name, ID, Description FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

    End Sub

    ''' <summary>
    ''' STSIM0000011
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds raster attribute support to Output Options
    ''' </remarks>
    Private Shared Sub STSIM0000011(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " &
            "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " &
            "ProjectID                   INTEGER, " &
            "ScenarioID                  INTEGER, " &
            "SummaryOutputSC             INTEGER, " &
            "SummaryOutputSCTimesteps    INTEGER, " &
            "SummaryOutputTR             INTEGER, " &
            "SummaryOutputTRTimesteps    INTEGER, " &
            "SummaryOutputTRIntervalMean INTEGER, " &
            "SummaryOutputTRSC           INTEGER, " &
            "SummaryOutputTRSCTimesteps  INTEGER, " &
            "SummaryOutputSA             INTEGER, " &
            "SummaryOutputSATimesteps    INTEGER, " &
            "SummaryOutputTA             INTEGER, " &
            "SummaryOutputTATimesteps    INTEGER, " &
            "RasterOutputSC              INTEGER, " &
            "RasterOutputSCTimesteps     INTEGER, " &
            "RasterOutputTR              INTEGER, " &
            "RasterOutputTRTimesteps     INTEGER, " &
            "RasterOutputAge             INTEGER, " &
            "RasterOutputAgeTimesteps    INTEGER, " &
            "RasterOutputTST             INTEGER, " &
            "RasterOutputTSTTimesteps    INTEGER, " &
            "RasterOutputST              INTEGER, " &
            "RasterOutputSTTimesteps     INTEGER, " &
            "RasterOutputSA              INTEGER, " &
            "RasterOutputSATimesteps     INTEGER, " &
            "RasterOutputTA              INTEGER, " &
            "RasterOutputTATimesteps     INTEGER)")

        store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC," &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "SummaryOutputTA," &
            "SummaryOutputTATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps)" &
            " SELECT " &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC, " &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "SummaryOutputTA," &
            "SummaryOutputTATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps" &
            " FROM ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP")

    End Sub

    ''' <summary>
    ''' STSIM0000012
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add a timestep column to ST_StateAttributeValue and ST_TransitionAttributeValue
    ''' </remarks>
    Private Shared Sub STSIM0000012(ByVal store As DataStore)

        'State Attribute Value
        store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Attribute Value
        If (store.TableExists("ST_TransitionAttributeValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000013
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will create a new schema for the following tables:
    ''' 
    ''' TransitionTarget
    ''' TransitionAttributeTarget
    ''' TransitionMultiplierValue
    ''' TransitionSlopeMultiplier
    ''' TransitionDirectionMultiplier
    ''' 
    ''' The distribution, sd, min, and max fields will appear as follows:
    ''' 
    ''' DistributionType
    ''' DistributionSD
    ''' DistributionMin 
    ''' DistributionMax 
    ''' 
    ''' And finally, the TransitionMultiplierDistribution table will be dropped because its fields will be included in the TransitionMultiplierValue table.
    ''' However, before it is dropped, its values must be copied over to the new TransitionMultiplierValue table.
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000013(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        If (store.TableExists("ST_TransitionAttributeTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ST_TransitionMultiplierValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionMultiplierTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionSD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Mean, SD FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ST_TransitionSlopeMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionSlopeMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionSlopeMultiplier (ST_TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionSlopeMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Multiplier, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ST_TransitionDirectionMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionDirectionMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionDirectionMultiplier (ST_TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionDirectionMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Multiplier, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ST_TransitionMultiplierValue") And store.TableExists("ST_TransitionMultiplierDistribution")) Then

            store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionType=(SELECT ST_TransitionMultiplierDistribution.Distribution FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)")
            store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionMin=(SELECT ST_TransitionMultiplierDistribution.MinimumValue FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)")
            store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionMax=(SELECT ST_TransitionMultiplierDistribution.MaximumValue FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)")

            store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierDistribution")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000014
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add average annual transition probability support to the output options table.
    ''' </remarks>
    Private Shared Sub STSIM0000014(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions ADD COLUMN RasterOutputAATP INTEGER")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions ADD COLUMN RasterOutputAATPTimesteps INTEGER")

    End Sub

    ''' <summary>
    ''' STSIM0000015
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will perform minor maintenance on the chart variables table.  The new table has a VariableName column and we
    ''' need to update it with the correct varaible name for ST-Sim.
    ''' </remarks>
    Private Shared Sub STSIM0000015(ByVal store As DataStore)

        If (Not store.TableExists("STime_ChartVariable")) Then
            Return
        End If

        'We don't want to do this if the DataSheetName column is no longer there.  This will happen after Stochastic Time's STIME0000009
        'update has been applied.  See commments for that function.

        Dim dtcv As DataTable = store.CreateDataTable("STime_ChartVariable")

        If (Not dtcv.Columns.Contains("DataFeedName")) Then
            Return
        End If

        store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName='StateClassAmountVariable' WHERE DataSheetName='ST_OutputStratumState'")
        store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName='TransitionAmountVariable' WHERE DataSheetName='ST_OutputStratumTransition'")

        Dim dt As DataTable = store.CreateDataTableFromQuery("SELECT * FROM STime_ChartVariable WHERE DataFeedName='ST_OutputStateAttribute'", "Table")

        For Each dr As DataRow In dt.Rows

            Dim dsname As String = CStr(dr("DataSheetName"))
            Dim q As String = String.Format(CultureInfo.InvariantCulture, "UPDATE STime_ChartVariable SET VariableName='{0}' WHERE DataSheetName='{1}'", dsname, dsname)

            store.ExecuteNonQuery(q)

        Next

        dt = store.CreateDataTableFromQuery("SELECT * FROM STime_ChartVariable WHERE DataFeedName='ST_OutputTransitionAttribute'", "Table")

        For Each dr As DataRow In dt.Rows

            Dim dsname As String = CStr(dr("DataSheetName"))
            Dim q As String = String.Format(CultureInfo.InvariantCulture, "UPDATE STime_ChartVariable SET VariableName='{0}' WHERE DataSheetName='{1}'", dsname, dsname)

            store.ExecuteNonQuery(q)

        Next

    End Sub

    ''' <summary>
    ''' STSIM0000016
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add a secondary stratum column to ST_StateAttributeValue and ST_TransitionAttributeValue
    ''' </remarks>
    Private Shared Sub STSIM0000016(ByVal store As DataStore)

        'State Attribute Value
        store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, Timestep, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, Timestep, StratumID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        'Transition Attribute Value
        If (store.TableExists("ST_TransitionAttributeValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, Timestep, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, Timestep, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000017
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add secondary stratum/iteration/timestep columns to the following tables:
    ''' 
    ''' (1.)  ST_TransitionTarget              - Iteration
    ''' (2.)  ST_TransitionSizeDistribution    - Iteration, Timestep
    ''' (3.)  ST_TransitionSpreadDistribution  - Iteration, Timestep
    ''' (4.)  ST_TransitionPatchPrioritization - Iteration, Timestep
    ''' (5.)  ST_TransitionSpatialMultiplier   - Iteration
    ''' (6.)  ST_StateAttributeValue           - Iteration
    ''' (7.)  ST_TransitionAttributeValue      - Iteration
    ''' (8.)  ST_TransitionAttributeTarget     - Iteration
    ''' (9.)  ST_TimeSinceTransitionGroup      - SecondaryStratum
    ''' (10.) ST_TimeSinceTransitionRandomize  - SecondaryStratum
    ''' (11.) ST_TransitionSlopeMultiplier     - SecondaryStratum
    ''' (12.) ST_TransitionDirectionMultiplier - SecondaryStratum
    ''' (13.) ST_TransitionAdjacencyMultiplier - SecondaryStratum
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000017(ByVal store As DataStore)

        '(1.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(2.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionSizeDistribution RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionSizeDistribution (ST_TransitionSizeDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, MaximumArea DOUBLE, RelativeAmount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionSizeDistribution(ProjectID, ScenarioID, StratumID, TransitionGroupID, MaximumArea, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, TransitionGroupID, MaximumArea, RelativeAmount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(3.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionSpreadDistribution RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionSpreadDistribution (ST_TransitionSpreadDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, MaximumDistance DOUBLE, RelativeAmount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionSpreadDistribution(ProjectID, ScenarioID, StratumID, TransitionGroupID, StateClassID, MaximumDistance, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, TransitionGroupID, StateClassID, MaximumDistance, RelativeAmount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(4.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionPatchPrioritization RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionPatchPrioritization (ST_TransitionPatchPrioritizationID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, PatchPrioritization INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionPatchPrioritization(ProjectID, ScenarioID, TransitionGroupID, PatchPrioritization) SELECT ProjectID, ScenarioID, TransitionGroupID, PatchPrioritization FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(5.) above
        If (store.TableExists("ST_TransitionSpatialMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionSpatialMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionSpatialMultiplier (ST_TransitionSpatialMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, MultiplierFileName TEXT(255))")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionSpatialMultiplier(ProjectID, ScenarioID, Timestep, TransitionGroupID, MultiplierFileName) SELECT ProjectID, ScenarioID, Timestep, TransitionGroupID, TransitionSpatialMultiplierFilename FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(6.) above
        store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, SecondaryStratumID, Timestep, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, StratumID, SecondaryStratumID, Timestep, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(7.) above
        If (store.TableExists("ST_TransitionAttributeValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(8.) above
        If (store.TableExists("ST_TransitionAttributeTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(9.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionGroup RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TimeSinceTransitionGroup (ST_TimeSinceTransitionGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TimeSinceTransitionGroup(ProjectID, ScenarioID, StratumID, TransitionTypeID, TransitionGroupID) SELECT ProjectID, ScenarioID, StratumID, TransitionTypeID, TransitionGroupID FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(10.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TimeSinceTransitionRandomize (ST_TimeSinceTransitionRandomizeID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, MaxInitialTST INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TimeSinceTransitionRandomize(ProjectID, ScenarioID, StratumID, MaxInitialTST) SELECT ProjectID, ScenarioID, StratumID, MaxInitialTST FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(11.) above
        If (store.TableExists("ST_TransitionSlopeMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionSlopeMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionSlopeMultiplier (ST_TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionSlopeMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(12.) above
        If (store.TableExists("ST_TransitionDirectionMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionDirectionMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionDirectionMultiplier (ST_TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionDirectionMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        '(13.) above
        If (store.TableExists("ST_TransitionAdjacencyMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionAdjacencyMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionAdjacencyMultiplier (ST_TransitionAdjacencyMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, AttributeValue DOUBLE, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionAdjacencyMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000018
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will update the ST_TransitionTypeGroup table as follows:
    ''' 
    ''' (1.) Change the IsSecondary field to IsPrimary
    ''' (2.) Toggle the values in the IsPrimary field as follows:
    ''' 
    '''      IsSecondary(True) -> IsPrimary(False) // -1 == True
    '''      IsSecondary(False) -> IsPrimary(True) // 0 or NULL == False
    ''' 
    ''' Also, set any IsPrimary(True) values to NULL which means True for this particular column
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000018(ByVal store As DataStore)

        '(1.) above
        store.ExecuteNonQuery("ALTER TABLE ST_TransitionTypeGroup RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_TransitionTypeGroup(ST_TransitionTypeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER, IsPrimary INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_TransitionTypeGroup(ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsPrimary) SELECT ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        '(2.) above
        store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=2 WHERE IsPrimary=-1")
        store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=-1 WHERE IsPrimary=0 or IsPrimary IS NULL")
        store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=0 WHERE IsPrimary=2")
        store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=NULL WHERE IsPrimary=-1")

    End Sub

    ''' <summary>
    ''' STSIM0000019
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will update will append a CalcFromDist column to the ST_InitialConditionsNonSpatial table
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000019(ByVal store As DataStore)
        store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsNonSpatial ADD COLUMN CalcFromDist INTEGER")
    End Sub

    ''' <summary>
    ''' STSIM0000020
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add support for Transition Group, State Class, and Minimum Initial TST Value 
    ''' to the ST_TimeSinceTransitionRandomize table.
    ''' 
    ''' </remarks>
    Private Shared Sub STSIM0000020(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN TransitionGroupID INTEGER")
        store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN StateClassID INTEGER")
        store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN MinInitialTST INTEGER")

    End Sub

    ''' <summary>
    ''' STSIM0000021
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' "This update will add minimum and maximum iteration fields to the STSim Run Control table.
    ''' </remarks>
    Private Shared Sub STSIM0000021(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_RunControl RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_RunControl(ST_RunControlID INTEGER PRIMARY KEY, ProjectID INTEGER, ScenarioID INTEGER, MinimumIteration INTEGER, MaximumIteration INTEGER, MinimumTimestep INTEGER, MaximumTimestep INTEGER, RunSpatially INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_RunControl(ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially) SELECT ProjectID, ScenarioID, 1, Iteration, 0, Timestep, RunSpatially FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

    End Sub

    ''' <summary>
    ''' STSIM0000022
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' "This update will fix incorrect boolean values written to the CellAreaOverride table.
    ''' </remarks>
    Private Shared Sub STSIM0000022(ByVal store As DataStore)
        store.ExecuteNonQuery("UPDATE ST_InitialConditionsSpatial SET CellAreaOverride=-1 WHERE CellAreaOverride=1")
    End Sub

    ''' <summary>
    ''' STSIM0000023
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will add the "Zero Values" field to the ST_OutputOptions table
    ''' </remarks>
    Private Shared Sub STSIM0000023(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " &
            "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " &
            "ProjectID                   INTEGER, " &
            "ScenarioID                  INTEGER, " &
            "SummaryOutputSC             INTEGER, " &
            "SummaryOutputSCTimesteps    INTEGER, " &
            "SummaryOutputSCZeroValues   INTEGER, " &
            "SummaryOutputTR             INTEGER, " &
            "SummaryOutputTRTimesteps    INTEGER, " &
            "SummaryOutputTRIntervalMean INTEGER, " &
            "SummaryOutputTRSC           INTEGER, " &
            "SummaryOutputTRSCTimesteps  INTEGER, " &
            "SummaryOutputSA             INTEGER, " &
            "SummaryOutputSATimesteps    INTEGER, " &
            "SummaryOutputTA             INTEGER, " &
            "SummaryOutputTATimesteps    INTEGER, " &
            "RasterOutputSC              INTEGER, " &
            "RasterOutputSCTimesteps     INTEGER, " &
            "RasterOutputTR              INTEGER, " &
            "RasterOutputTRTimesteps     INTEGER, " &
            "RasterOutputAge             INTEGER, " &
            "RasterOutputAgeTimesteps    INTEGER, " &
            "RasterOutputTST             INTEGER, " &
            "RasterOutputTSTTimesteps    INTEGER, " &
            "RasterOutputST              INTEGER, " &
            "RasterOutputSTTimesteps     INTEGER, " &
            "RasterOutputSA              INTEGER, " &
            "RasterOutputSATimesteps     INTEGER, " &
            "RasterOutputTA              INTEGER, " &
            "RasterOutputTATimesteps     INTEGER, " &
            "RasterOutputAATP            INTEGER, " &
            "RasterOutputAATPTimesteps   INTEGER)")

        store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC," &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "SummaryOutputTA," &
            "SummaryOutputTATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps," &
            "RasterOutputSA," &
            "RasterOutputSATimesteps," &
            "RasterOutputTA," &
            "RasterOutputTATimesteps," &
            "RasterOutputAATP," &
            "RasterOutputAATPTimesteps)" &
            " SELECT " &
            "ProjectID," &
            "ScenarioID," &
            "SummaryOutputSC," &
            "SummaryOutputSCTimesteps," &
            "SummaryOutputTR," &
            "SummaryOutputTRTimesteps," &
            "SummaryOutputTRIntervalMean," &
            "SummaryOutputTRSC, " &
            "SummaryOutputTRSCTimesteps," &
            "SummaryOutputSA," &
            "SummaryOutputSATimesteps," &
            "SummaryOutputTA," &
            "SummaryOutputTATimesteps," &
            "RasterOutputSC," &
            "RasterOutputSCTimesteps," &
            "RasterOutputTR," &
            "RasterOutputTRTimesteps," &
            "RasterOutputAge," &
            "RasterOutputAgeTimesteps," &
            "RasterOutputTST," &
            "RasterOutputTSTTimesteps," &
            "RasterOutputST," &
            "RasterOutputSTTimesteps," &
            "RasterOutputSA, " &
            "RasterOutputSATimesteps," &
            "RasterOutputTA, " &
            "RasterOutputTATimesteps," &
            "RasterOutputAATP," &
            "RasterOutputAATPTimesteps" &
            " FROM ST_OutputOptionsTEMP")

        store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP")

    End Sub

    ''' <summary>
    ''' STSIM0000024
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will set the Run Control table's minimum timestep values to 0 instead of the 
    ''' incorrect value of 0 which was set by a previous update.
    ''' </remarks>
    Private Shared Sub STSIM0000024(ByVal store As DataStore)
        store.ExecuteNonQuery("UPDATE ST_RunControl SET MinimumTimestep=0")
    End Sub

    ''' <summary>
    ''' STSIM0000025
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will:
    ''' 
    ''' (1.) Add a Timestep Units field to the Terminology table.
    ''' (2.) Update the table with a default value for this field.
    ''' </remarks>
    Private Shared Sub STSIM0000025(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_Terminology RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_Terminology (ST_TerminologyID INTEGER PRIMARY KEY, ProjectID INTEGER, TimestepUnits TEXT( 50 ), AmountLabel TEXT( 50 ), AmountUnits INTEGER, StateLabelX TEXT( 50 ), StateLabelY TEXT( 50 ), PrimaryStratumLabel TEXT( 50 ), SecondaryStratumLabel Text(50))")
        store.ExecuteNonQuery("INSERT INTO ST_Terminology (ST_TerminologyID, ProjectID, TimestepUnits, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel) SELECT ST_TerminologyID, ProjectID, 'Timestep', AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

    End Sub

    ''' <summary>
    ''' STSIM0000026
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds age tracking support to the summary state class output table.
    ''' It also adds State Label X/Y columns so that disaggregation with these columns is possible.
    ''' </remarks>
    Private Shared Sub STSIM0000026(ByVal store As DataStore)

        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumState RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, StateLabelXID INTEGER, StateLabelYID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumState_Index ON ST_OutputStratumState(ScenarioID, Iteration, Timestep)")

    End Sub

    ''' <summary>
    ''' STSIM0000027
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds NumCells support to the Initial Conditions Spatial table.
    ''' </remarks>
    Private Shared Sub STSIM0000027(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsSpatial ADD COLUMN  NumCells INTEGER")
        ' Set NumCells value = Num Rows * Num Cols, which is a starting point. Will only be 100% if Primary
        ' Stratum raster has no NO_DATA_VALUE cells.
        store.ExecuteNonQuery("UPDATE ST_InitialConditionsSpatial set NumCells = NumRows * NumColumns")

    End Sub

    ''' <summary>
    ''' STSIM0000028
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds age tracking support to the transition summary table and both attribute tables.
    ''' </remarks>
    Private Shared Sub STSIM0000028(ByVal store As DataStore)

        'ST_OutputStratumTransition
        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransition RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransition(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransition_Index ON ST_OutputStratumTransition(ScenarioID, Iteration, Timestep)")

        'ST_OutputStateAttribute
        store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputStateAttribute RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)")

        'ST_OutputTransitionAttribute
        store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index")
        store.ExecuteNonQuery("ALTER TABLE ST_OutputTransitionAttribute RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_OutputTransitionAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
        store.ExecuteNonQuery("INSERT INTO ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")
        store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)")

    End Sub

    ''' <summary>
    ''' STSIM0000029
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will remove the embedded ID value from STime_ChartVariable.VariableName.
    ''' </remarks>
    Private Shared Sub STSIM0000029(ByVal store As DataStore)

        If (store.TableExists("STime_ChartVariable")) Then

            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'ST_OutputStateAttribute'")
            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'ST_OutputTransitionAttribute'")
            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'ST_OutputStateAttribute'")
            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'ST_OutputTransitionAttribute'")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000030
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will remove the Name column from the age tables. It will also remove
    ''' the Description column from the age groups table.
    ''' </remarks>
    Private Shared Sub STSIM0000030(ByVal store As DataStore)

        If (store.TableExists("ST_AgeType")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_AgeType RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_AgeType (ST_AgeTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Frequency INTEGER, MaximumAge INTEGER)")
            store.ExecuteNonQuery("INSERT INTO ST_AgeType (ST_AgeTypeID, ProjectID, Frequency, MaximumAge) SELECT ST_AgeTypeID, ProjectID, Frequency, MaximumAge FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ST_AgeGroup")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_AgeGroup RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_AgeGroup (ST_AgeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, MaximumAge INTEGER, ID INTEGER, Color TEXT(20))")
            store.ExecuteNonQuery("INSERT INTO ST_AgeGroup (ST_AgeGroupID, ProjectID, MaximumAge, ID, Color) SELECT ST_AgeGroupID, ProjectID, MaximumAge, ID, Color FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000031
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds a StateClassID column to the ST_TransitionMultiplierValue column.
    ''' </remarks>
    Private Shared Sub STSIM0000031(ByVal store As DataStore)

        If (store.TableExists("ST_TransitionMultiplierValue")) Then

            store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionMultiplierTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000032
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update will change ST_RunControl as follows:
    ''' 
    ''' (1.) Update "MinimumIteration" to have a "1" everywhere to fix a problem where NULL values
    '''      were erroneously written to this field at some point.
    ''' 
    ''' (2.) Change the order of the columns in ST_RunControl so that it matches the order in the code.
    ''' </remarks>
    Private Shared Sub STSIM0000032(ByVal store As DataStore)

        store.ExecuteNonQuery("ALTER TABLE ST_RunControl RENAME TO TEMP_TABLE")
        store.ExecuteNonQuery("CREATE TABLE ST_RunControl (ST_RunControlID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, MinimumIteration INTEGER, MaximumIteration INTEGER, MinimumTimestep INTEGER, MaximumTimestep INTEGER, RunSpatially INTEGER)")
        store.ExecuteNonQuery("INSERT INTO ST_RunControl(ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially) SELECT ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially FROM TEMP_TABLE")
        store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        store.ExecuteNonQuery("UPDATE ST_RunControl SET MinimumIteration=1 WHERE MinimumIteration IS NULL")

    End Sub

    ''' <summary>
    ''' STSIM0000033
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>Cretaes new indexes for output tables</remarks>
    Private Shared Sub STSIM0000033(ByVal store As DataStore)

        'Very old libraries did not create the schema until the tables were actually needed so if, for example, 
        'STSim_OutputStratumState does not exist we don't want to try to do anything.

        If (Not store.TableExists("STSim_OutputStratumState")) Then
            Return
        End If

        If (store.TableExists("ST_OutputStratumAmount")) Then

            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumAmount_Index")
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratum_Index ON STSim_OutputStratum (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID)")

        End If

        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index")
        store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumState_Index ON STSim_OutputStratumState (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeClass)")

        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index")
        store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumTransition_Index ON STSim_OutputStratumTransition (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeClass)")

        store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionState_Index")
        store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumTransitionState_Index ON STSim_OutputStratumTransitionState (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID)")

        store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index")
        store.ExecuteNonQuery("CREATE INDEX STSim_OutputStateAttribute_Index ON STSim_OutputStateAttribute (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeClass)")

        store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index")
        store.ExecuteNonQuery("CREATE INDEX STSim_OutputTransitionAttribute_Index ON STSim_OutputTransitionAttribute (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeClass)")

    End Sub

    ''' <summary>
    ''' STSIM0000034
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>Adds a color field to the stratum table</remarks>
    Private Shared Sub STSIM0000034(ByVal store As DataStore)

        If (store.TableExists("STSim_Stratum")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_Stratum RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_Stratum(StratumID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, ID INTEGER, Color TEXT(20), Description TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_Stratum(StratumID, ProjectID, Name, ID, Description) SELECT StratumID, ProjectID, Name, ID, Description FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000040
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>See comments in caller function for this dummy routine...</remarks>
    Private Shared Sub STSIM0000040(ByVal store As DataStore)
        Return
    End Sub

    ''' <summary>
    ''' STSIM0000041
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Removes invalid NULL entries for TransitionTypeID TransitionGroupID in the table STSim_TransitionTypeGroup
    ''' </remarks>
    Private Shared Sub STSIM0000041(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionTypeGroup")) Then

            store.ExecuteNonQuery("DELETE FROM STSim_TransitionTypeGroup WHERE TransitionTypeID IS NULL")
            store.ExecuteNonQuery("DELETE FROM STSim_TransitionTypeGroup WHERE TransitionGroupID IS NULL")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000042
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Changes table name 'STSim_OutputStratumAmount' to 'STSim_OutputStratum'
    ''' </remarks>
    Private Shared Sub STSIM0000042(ByVal store As DataStore)

        If (store.TableExists("STSim_OutputStratumAmount")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumAmount RENAME to STSim_OutputStratum")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000043
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Data feeds with distributions now get their distribution types from Stats_DistributionType instead
    ''' of a hard coded list.  This means that we must update every table so that it uses the new project
    ''' specific primary key IDs.
    ''' </remarks>
    Private Shared Sub STSIM0000043(ByVal store As DataStore)

        Dim Projects As DataTable = store.CreateDataTable("SSim_Project")
        Dim Scenarios As DataTable = store.CreateDataTable("SSim_Scenario")
        Dim DistTables As New Dictionary(Of Integer, DataTable)

        For Each ProjectRow As DataRow In Projects.Rows

            Dim ProjectId As Integer = CInt(ProjectRow("ProjectID"))

            Dim DistributionTypes As DataTable =
                store.CreateDataTableFromQuery(String.Format(CultureInfo.InvariantCulture,
                    "SELECT * FROM STime_DistributionType WHERE ProjectID={0}",
                    ProjectId), "DistributionTypes")

            Debug.Assert(DistributionTypes.Rows.Count = 4)
            DistTables.Add(ProjectId, DistributionTypes)

        Next

        UpdateDistributions("STSim_TransitionTarget", store, Scenarios, DistTables)
        UpdateDistributions("STSim_TransitionMultiplierValue", store, Scenarios, DistTables)
        UpdateDistributions("STSim_TransitionAttributeTarget", store, Scenarios, DistTables)
        UpdateDistributions("STSim_TransitionDirectionMultiplier", store, Scenarios, DistTables)
        UpdateDistributions("STSim_TransitionSlopeMultiplier", store, Scenarios, DistTables)
        UpdateDistributions("STSim_TransitionAdjacencyMultiplier", store, Scenarios, DistTables)

    End Sub

    Private Shared Sub UpdateDistributions(
        ByVal tableName As String,
        ByVal store As DataStore,
        ByVal scenarios As DataTable,
        ByVal distTables As Dictionary(Of Integer, DataTable))

        If (Not store.TableExists(tableName)) Then
            Return
        End If

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-1 WHERE DistributionType=0", tableName)) 'Normal
        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-2 WHERE DistributionType=1", tableName)) 'Beta
        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-3 WHERE DistributionType=2", tableName)) 'Uniform

        For Each ScenarioRow As DataRow In scenarios.Rows

            Dim ScenarioId As Integer = CInt(ScenarioRow("ScenarioID"))
            Dim ProjectId As Integer = CInt(ScenarioRow("ProjectID"))
            Dim DistributionTypes As DataTable = distTables(ProjectId)

            For Each DistTypeRow As DataRow In DistributionTypes.Rows

                Dim DistTypeId As Integer = CInt(DistTypeRow("DistributionTypeID"))
                Dim DistTypeName As String = CStr(DistTypeRow("Name"))
                Dim TempDistId As Integer
                Dim DoUpdate As Boolean = True

                If (DistTypeName = "Normal") Then
                    TempDistId = -1
                ElseIf (DistTypeName = "Beta") Then
                    TempDistId = -2
                ElseIf (DistTypeName = "Uniform") Then
                    TempDistId = -3
                Else
                    DoUpdate = False
                    Debug.Assert(DistTypeName = "Uniform Integer")
                End If

                If (DoUpdate) Then

                    store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
                        "UPDATE {0} SET DistributionType={1} WHERE DistributionType={2} AND ScenarioID={3}",
                        tableName, DistTypeId, TempDistId, ScenarioId))

                End If

            Next

        Next

    End Sub

    ''' <summary>
    ''' STSIM0000044
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds a TransitionMultiplierTypeID column to the STSim_TransitionSpatialMultiplier table
    ''' </remarks>
    Private Shared Sub STSIM0000044(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionSpatialMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSpatialMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSpatialMultiplier(TransitionSpatialMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, TransitionMultiplierTypeID INTEGER, MultiplierFileName TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionSpatialMultiplier(ScenarioID, Iteration, Timestep, TransitionGroupID, MultiplierFileName) SELECT ScenarioID, Iteration, Timestep, TransitionGroupID, MultiplierFileName FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000045
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds a single Transition Size Prioritization record to any scenario that
    ''' has Transition Size Distribution records.  
    ''' 
    ''' Tables are:
    ''' 
    ''' STSim_TransitionSizeDistribution
    ''' STSim_TransitionSizePrioritization
    ''' </remarks>
    Private Shared Sub STSIM0000045(ByVal store As DataStore)

        If (Not store.TableExists("STSim_TransitionSizeDistribution")) Then
            Return
        End If

        If (Not store.TableExists("STSim_TransitionSizePrioritization")) Then
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSizePrioritization(TransitionSizePrioritizationID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, TransitionGroupID INTEGER, Priority INTEGER)")
        End If

        Dim dt As DataTable = store.CreateDataTable("SSim_Scenario")

        For Each dr As DataRow In dt.Rows

            Dim ScenarioId As Integer = CInt(dr("ScenarioID"))
            Dim SizeDistQuery As String = String.Format(CultureInfo.InvariantCulture, "SELECT COUNT(ScenarioID) FROM STSim_TransitionSizeDistribution WHERE ScenarioID={0}", ScenarioId)
            Dim HasSizeDist As Boolean = (CInt(store.ExecuteScalar(SizeDistQuery)) > 0)

            If (HasSizeDist) Then

                store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
                    "INSERT INTO STSim_TransitionSizePrioritization(ScenarioID, Priority) VALUES({0},{1})",
                    ScenarioId, CInt(SizePrioritization.Largest)))

            End If

        Next

    End Sub

    ''' <summary>
    ''' STSIM0000046
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update changes the CellSize field type from SINGLE to DOUBLE because this type is no longer supported by SyncroSim
    ''' </remarks>
    Private Shared Sub STSIM0000046(ByVal store As DataStore)

        If (store.TableExists("STSim_InitialConditionsSpatial")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsSpatial(InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, NumRows INTEGER, NumColumns INTEGER, NumCells INTEGER, CellSize DOUBLE, CellSizeUnits TEXT, CellArea DOUBLE, CellAreaOverride INTEGER, XLLCorner DOUBLE, YLLCorner DOUBLE, SRS TEXT, StratumFileName TEXT, SecondaryStratumFileName TEXT, StateClassFileName TEXT, AgeFileName TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsSpatial(ScenarioID, NumRows, NumColumns, NumCells, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName) SELECT ScenarioID, NumRows, NumColumns, NumCells, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000047
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds model names and display names to all existing charts
    ''' </remarks>
    Private Shared Sub STSIM0000047(ByVal store As DataStore)

        If (store.TableExists("STime_Chart")) Then
            store.ExecuteNonQuery("UPDATE STime_Chart SET Model='stsim-model-transformer', ModelDisplayName='ST-Sim State and Transition'")
        End If

        If (store.TableExists("STime_Map")) Then
            store.ExecuteNonQuery("UPDATE STime_Map SET Model='stsim-model-transformer', ModelDisplayName='ST-Sim State and Transition'")
        End If

    End Sub

    ''' <summary>
    ''' This update adds a DistributionFrequency column to the relevant ST-Sim tables
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks></remarks>
    Private Shared Sub STSIM0000048(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTarget ADD COLUMN DistributionFrequencyID INTEGER")
            store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionTarget SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", CInt(StochasticTime.DistributionFrequency.Iteration)))

        End If

        If (store.TableExists("STSim_TransitionMultiplierValue")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionMultiplierValue ADD COLUMN DistributionFrequencyID INTEGER")
            store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionMultiplierValue SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", CInt(StochasticTime.DistributionFrequency.Iteration)))

        End If

        If (store.TableExists("STSim_TransitionAttributeTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeTarget ADD COLUMN DistributionFrequencyID INTEGER")
            store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionAttributeTarget SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", CInt(StochasticTime.DistributionFrequency.Iteration)))

        End If

        If (store.TableExists("STSim_TransitionDirectionMultiplier")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionDirectionMultiplier ADD COLUMN DistributionFrequencyID INTEGER")
        End If

        If (store.TableExists("STSim_TransitionSlopeMultiplier")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSlopeMultiplier ADD COLUMN DistributionFrequencyID INTEGER")
        End If

        If (store.TableExists("STSim_TransitionAdjacencyMultiplier")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAdjacencyMultiplier ADD COLUMN DistributionFrequencyID INTEGER")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000049
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' We need to copy the data from the STime_DistribtionValue table to the STSim_DistributionValue table
    ''' because the STSim_DistributionValue table is what ST-Sim's customized distribution support uses.
    ''' 
    ''' Also, the STime_DistributionValue table now has a distribution field which needs to have the ID for a  
    ''' Uniform Distribution because before that's how it was being sampled.
    ''' 
    ''' Steps:
    ''' 
    ''' 1.  Create the STSim_DistributionValue table
    ''' 2.  Copy the data from STime_DistribtionValue to STSim_DistributionValue
    ''' 3.  Update STSim_DistributionValue.ValueDistributionTypeID with the ID for a Uniform Distribution
    ''' 4.  Delete the contents of the STime_DistribtionValue table
    ''' </remarks>
    Private Shared Sub STSIM0000049(ByVal store As DataStore)

        'If neither of these tables exist then there is nothing to do

        If (Not store.TableExists("STime_DistributionType") Or Not store.TableExists("STime_DistributionValue")) Then
            Return
        End If

        '1. Above
        store.ExecuteNonQuery("CREATE TABLE STSim_DistributionValue(DistributionValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, DistributionTypeID INTEGER, ExternalVariableTypeID INTEGER, ExternalVariableMin DOUBLE, ExternalVariableMax DOUBLE, Value DOUBLE, ValueDistributionTypeID INTEGER, ValueDistributionFrequency INTEGER, ValueDistributionSD DOUBLE, ValueDistributionMin DOUBLE, ValueDistributionMax DOUBLE, ValueDistributionRelativeFrequency DOUBLE)")

        '2. Above
        store.ExecuteNonQuery("INSERT INTO STSim_DistributionValue(ScenarioID, Iteration, Timestep, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency) SELECT ScenarioID, Iteration, Timestep, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency FROM STime_DistributionValue")

        '3. Above
        Dim Projects As DataTable = store.CreateDataTable("SSim_Project")
        Dim Scenarios As DataTable = store.CreateDataTable("SSim_Scenario")
        Dim IDLookup As New Dictionary(Of Integer, Integer)

        For Each ProjectRow As DataRow In Projects.Rows

            Dim ProjectId As Integer = CInt(ProjectRow("ProjectID"))

            Dim DistributionTypes As DataTable =
                store.CreateDataTableFromQuery(String.Format(CultureInfo.InvariantCulture,
                    "SELECT * FROM STime_DistributionType WHERE ProjectID={0} AND Name='Uniform'",
                    ProjectId), "DistributionTypes")

            Debug.Assert(DistributionTypes.Rows.Count = 1)

            If (DistributionTypes.Rows.Count = 1) Then
                IDLookup.Add(ProjectId, CInt(DistributionTypes.Rows(0)("DistributionTypeID")))
            End If

        Next

        For Each ScenarioRow As DataRow In Scenarios.Rows

            Dim ScenarioId As Integer = CInt(ScenarioRow("ScenarioID"))
            Dim ProjectId As Integer = CInt(ScenarioRow("ProjectID"))

            Debug.Assert(IDLookup.ContainsKey(ProjectId))

            If (IDLookup.ContainsKey(ProjectId)) Then

                store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
                    "UPDATE STSim_DistributionValue SET ValueDistributionTypeID={0} WHERE ScenarioID={1}",
                    IDLookup(ProjectId), ScenarioId))

            End If

        Next

        '4. Above
        store.ExecuteNonQuery("DROP TABLE STime_DistributionValue")

    End Sub

    ''' <summary>
    ''' STSIM0000050
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds indexes to various tables to improve performance with large data sets
    ''' </remarks>
    Private Shared Sub STSIM0000050(ByVal store As DataStore)

        If (store.TableExists("STSim_InitialConditionsNonSpatialDistribution")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_InitialConditionsNonSpatialDistribution_Index ON STSim_InitialConditionsNonSpatialDistribution (ScenarioID)")
        End If

        If (store.TableExists("STSim_StateAttributeValue")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_StateAttributeValue_Index ON STSim_StateAttributeValue (ScenarioID)")
        End If

        If (store.TableExists("STSim_Transition")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_Transition_Index ON STSim_Transition (ScenarioID)")
        End If

        If (store.TableExists("STSim_TransitionAttributeTarget")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_TransitionAttributeTarget_Index ON STSim_TransitionAttributeTarget (ScenarioID)")
        End If

        If (store.TableExists("STSim_TransitionAttributeValue")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_TransitionAttributeValue_Index ON STSim_TransitionAttributeValue (ScenarioID)")
        End If

        If (store.TableExists("STSim_TransitionMultiplierValue")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_TransitionMultiplierValue_Index ON STSim_TransitionMultiplierValue (ScenarioID)")
        End If

        If (store.TableExists("STSim_TransitionTarget")) Then
            store.ExecuteNonQuery("CREATE INDEX STSim_TransitionTarget_Index ON STSim_TransitionTarget (ScenarioID)")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000050
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds Iteration to the Initial Condition data sets, and splits the IC Spatial table into 2.
    ''' </remarks>
    Private Shared Sub STSIM0000051(ByVal store As DataStore)

        If (store.TableExists("STSim_InitialConditionsNonSpatialDistribution")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsNonSpatialDistribution(InitialConditionsNonSpatialDistributionID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,Iteration INTEGER, StratumID INTEGER,SecondaryStratumID INTEGER,StateClassID INTEGER,AgeMin INTEGER,AgeMax INTEGER,RelativeAmount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsNonSpatialDistribution(ScenarioID,StratumID,SecondaryStratumID,StateClassID,AgeMin,AgeMax,RelativeAmount) SELECT ScenarioID,StratumID,SecondaryStratumID,StateClassID,AgeMin,AgeMax,RelativeAmount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        'Copy the raster metadata over to new table
        If (store.TableExists("STSim_InitialConditionsSpatial")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE")

            Dim sSQL As String =
                    "CREATE TABLE STSim_InitialConditionsSpatialProperties (InitialConditionsSpatialPropertiesID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,NumRows INTEGER,NumColumns INTEGER,NumCells INTEGER,CellSize DOUBLE,CellSizeUnits TEXT,CellArea DOUBLE,CellAreaOverride INTEGER,XLLCorner DOUBLE,YLLCorner DOUBLE,SRS TEXT)"
            store.ExecuteNonQuery(sSQL)

            sSQL = "insert into STSim_InitialConditionsSpatialProperties(ScenarioID,NumRows,NumColumns,NumCells,CellSize,CellSizeUnits,CellArea,CellAreaOverride,XLLCorner,YLLCorner,SRS) " &
                "select ScenarioID,NumRows,NumColumns,NumCells,CellSize,CellSizeUnits,CellArea,CellAreaOverride,XLLCorner,YLLCorner,SRS from TEMP_TABLE"
            store.ExecuteNonQuery(sSQL)

            ' Drop columns transfered to Properties table, and add Iteration
            sSQL = "CREATE TABLE STSim_InitialConditionsSpatial (InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,Iteration INTEGER, StratumFileName TEXT,SecondaryStratumFileName TEXT,StateClassFileName TEXT,AgeFileName TEXT)"
            store.ExecuteNonQuery(sSQL)
            sSQL = "insert into STSim_InitialConditionsSpatial(ScenarioID,StratumFileName,SecondaryStratumFileName,StateClassFileName,AgeFileName) select ScenarioID,StratumFileName,SecondaryStratumFileName,StateClassFileName,AgeFileName from TEMP_TABLE"
            store.ExecuteNonQuery(sSQL)

            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000052
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds an Iteration column to the STSim_TimeSinceTransitionRandomize table
    ''' </remarks>
    Private Shared Sub STSIM0000052(ByVal store As DataStore)

        If (store.TableExists("STSim_TimeSinceTransitionRandomize")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionRandomize ADD COLUMN Iteration INTEGER")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000053
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>This update adds a MaximizeFidelityToDistribution field to STSim_TransitionSizePrioritization</remarks>
    Private Shared Sub STSIM0000053(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionSizePrioritization")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSizePrioritization ADD COLUMN MaximizeFidelityToDistribution INTEGER")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000054
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>This update adds a MaximizeFidelityToTotalArea to STSim_TransitionSizePrioritization</remarks>
    Private Shared Sub STSIM0000054(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionSizePrioritization")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSizePrioritization ADD COLUMN MaximizeFidelityToTotalArea INTEGER")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000055
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' This update adds iteration and timestep fields to the following tables:
    ''' 
    ''' STSim_Transition
    ''' STSim_DeterministicTransition
    ''' </remarks>
    Private Shared Sub STSIM0000055(ByVal store As DataStore)

        If (store.TableExists("STSim_Transition")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_Transition RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_Transition(TransitionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumIDSource INTEGER, StateClassIDSource INTEGER, StratumIDDest INTEGER, StateClassIDDest INTEGER, TransitionTypeID INTEGER, Probability DOUBLE, Proportion DOUBLE, AgeMin INTEGER, AgeMax INTEGER, AgeRelative INTEGER, AgeReset INTEGER, TSTMin INTEGER, TSTMax INTEGER, TSTRelative INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_Transition(ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative) SELECT ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_DeterministicTransition")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_DeterministicTransition RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_DeterministicTransition(DeterministicTransitionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumIDSource INTEGER, StateClassIDSource INTEGER, StratumIDDest INTEGER, StateClassIDDest INTEGER, AgeMin INTEGER, AgeMax INTEGER, Location TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_DeterministicTransition(ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, AgeMin, AgeMax, Location) SELECT ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, AgeMin, AgeMax, Location FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' STSIM0000056
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks></remarks>
    Private Shared Sub STSIM0000056(ByVal store As DataStore)

        If (store.TableExists("STSim_TransitionGroup")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionGroup ADD COLUMN IsAuto INTEGER")
        End If

        If (store.TableExists("STSim_TransitionTypeGroup")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTypeGroup ADD COLUMN IsAuto INTEGER")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000057
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' A bug in the transition diagram was causing invalid boolean values to be written to the AgeReset
    ''' column which caused diagram editing to fail.
    ''' </remarks>
    Private Shared Sub STSIM0000057(ByVal store As DataStore)

        If (store.TableExists("STSim_Transition")) Then
            store.ExecuteNonQuery("UPDATE STSim_Transition SET AgeReset=-1 WHERE AgeReset=1")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000058
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Move the Spatial Output files to the new Datasheet based locations
    ''' </remarks>
    Private Shared Sub STSIM0000058(ByVal store As DataStore)

        ' Loop thru all the results scenarios in the library
        Dim dtScenarios As DataTable = store.CreateDataTable("SSim_Scenario")

        Dim outputDatasheets(,) As String = {
            {"STSim_OutputSpatialTST", "tg-*-tst"},  '' Make sure this at the top, otherwise files will be caught with tg-*
            {"STSim_OutputSpatialStratum", "str"},
            {"STSim_OutputSpatialState", "sc"},
            {"STSim_OutputSpatialAge", "age"},
            {"STSim_OutputSpatialTransition", "tg-*"},
            {"STSim_OutputSpatialStateAttribute", "sa-*"},
            {"STSim_OutputSpatialTransitionAttribute", "ta-*"},
            {"STSim_OutputSpatialAverageTransitionProbability", "tgap-*"},
            {"SF_OutputSpatialStockType", "stk-*"},
            {"SF_OutputSpatialStockGroup", "stkg-*"},
            {"SF_OutputSpatialFlowType", "flo-*"},
            {"SF_OutputSpatialFlowGroup", "flog-*"}
        }

        For Each row As DataRow In dtScenarios.Rows

            Dim scenarioId As Integer = CInt(row("ScenarioID"))

            For i As Integer = 0 To outputDatasheets.GetUpperBound(0)

                Dim dsName As String = outputDatasheets(i, 0)
                Dim fileFilter As String = outputDatasheets(i, 1)

                Dim oldLocation As String = GetLegacyOutputFolder(store, scenarioId)
                Dim newLocation As String = GetDatasheetOutputFolder(store, scenarioId, dsName)

                If Directory.Exists(oldLocation) Then

                    Dim files = Directory.GetFiles(oldLocation, "*" + fileFilter + ".*")

                    For Each oldFilename In files

                        If (Not Directory.Exists(newLocation)) Then
                            Directory.CreateDirectory(newLocation)
                        End If

                        Dim newFilename As String
                        If dsName = "STSim_OutputSpatialTST" Then
                            ' This is a special case, because we want to rename to a generic form
                            newFilename = Path.GetFileName(oldFilename)
                            newFilename = newFilename.Replace("-tst", "").Replace("tg-", "tst-")
                            newFilename = Path.Combine(newLocation, newFilename)
                        Else
                            newFilename = Path.Combine(newLocation, Path.GetFileName(oldFilename))
                        End If

                        If Not File.Exists(newFilename) Then
                            File.Move(oldFilename, newFilename)
                            Debug.Print(String.Format(CultureInfo.InvariantCulture, "Moving spatial output file '{0}' to {1}", oldFilename, newLocation))
                        End If

                    Next

                End If

            Next

        Next

        ' Rename any TST files already records in the STSim_OutputSpatialTST datasheet, to "tst-123".
        If (store.TableExists("STSim_OutputSpatialTST")) Then
            store.ExecuteNonQuery("update STSim_OutputSpatialTST set filename = Replace(Replace(filename,'tg-','tst-'),'-tst.','.')")
        End If

    End Sub

    ''' <summary>
    ''' STSIM0000059
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Map criteria used to be in the following form:
    ''' 
    '''     sc|      //variable with no item id
    '''     tg-342   //variable with item id
    ''' 
    ''' Now it is in the form:
    ''' 
    '''     sc|
    '''     tg-342-itemid-342-itemsrc-STSim_TransitionGroup
    '''     
    ''' This update must convert all map criteria to the new form.  This update is specific to ST-Sim and
    ''' StockFlow since the data sheet names must be known in order for a conversion to take place.
    ''' </remarks>
    Private Shared Sub STSIM0000059(ByVal store As DataStore)

        If (Not store.TableExists("STime_Map")) Then
            Return
        End If

        Dim dt As DataTable = store.CreateDataTable("STime_Map")

        For Each dr As DataRow In dt.Rows

            If (dr("Criteria") IsNot DBNull.Value) Then

                Dim id As Integer = CInt(dr("MapID"))
                Dim cr As String = CStr(dr("Criteria"))
                Dim sp() As String = cr.Split(CChar("|"))
                Dim sb As New Text.StringBuilder()

                For Each s As String In sp
                    sb.Append(ExpandMapCriteriaFrom1x(s))
                    sb.Append("|")
                Next

                Dim newcr As String = sb.ToString().TrimEnd(CChar("|"))
                Dim query As String = String.Format(CultureInfo.InvariantCulture, "UPDATE STime_Map SET Criteria='{0}' WHERE MapID={1}", newcr, id)

                store.ExecuteNonQuery(query)

            End If

        Next

    End Sub

    ''' <summary>
    ''' STSIM0000060
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>
    ''' Add Legend Column to Primary Stratum, State Class, and Transition Type, to aid in Map 
    ''' Criteria legend definition.
    ''' </remarks>
    Private Shared Sub STSIM0000060(ByVal store As DataStore)

        If (store.TableExists("STSim_Stratum")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_Stratum ADD COLUMN Legend TEXT")
        End If

        If (store.TableExists("STSim_StateClass")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_StateClass ADD COLUMN Legend TEXT")
        End If

        If (store.TableExists("STSim_TransitionType")) Then
            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionType ADD COLUMN Legend TEXT")
        End If

    End Sub

    ''' <summary>
    ''' SF0000061
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>Add missing index on STSim_DistributionValue and if missing drop</remarks>
    Private Shared Sub STSIM0000061(ByVal store As DataStore)

        If (store.TableExists("STSim_DistributionValue")) Then

            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_DistributionValue_Index")
            store.ExecuteNonQuery("CREATE INDEX STSim_DistributionValue_Index ON STSim_DistributionValue(ScenarioID)")

        End If

    End Sub

    ''' <summary>
    ''' SF0000062
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>This update adds a tertiary stratum to all applicable tables</remarks>
    Private Shared Sub STSIM0000062(ByVal store As DataStore)

        If (store.TableExists("STSim_Terminology")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_Terminology RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_Terminology(TerminologyID Integer PRIMARY KEY AUTOINCREMENT, ProjectID Integer, AmountLabel TEXT, AmountUnits Integer, StateLabelX TEXT, StateLabelY TEXT, PrimaryStratumLabel TEXT, SecondaryStratumLabel TEXT, TertiaryStratumLabel TEXT, TimestepUnits TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_Terminology(ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel, TertiaryStratumLabel, TimestepUnits) SELECT ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel, 'Parcel', TimestepUnits FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_Transition")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_Transition RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_Transition(TransitionID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, Timestep Integer, StratumIDSource Integer, StateClassIDSource Integer, StratumIDDest Integer, StateClassIDDest Integer, SecondaryStratumID Integer, TertiaryStratumID Integer, TransitionTypeID Integer, Probability Double, Proportion Double, AgeMin Integer, AgeMax Integer, AgeRelative Integer, AgeReset Integer, TSTMin Integer, TSTMax Integer, TSTRelative Integer)")
            store.ExecuteNonQuery("INSERT INTO STSim_Transition(ScenarioID, Iteration, Timestep, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative) SELECT ScenarioID, Iteration, Timestep, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_InitialConditionsNonSpatialDistribution")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsNonSpatialDistribution(InitialConditionsNonSpatialDistributionID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, StratumID Integer, SecondaryStratumID Integer, TertiaryStratumID Integer, StateClassID Integer, AgeMin Integer, AgeMax Integer, RelativeAmount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsNonSpatialDistribution(ScenarioID, Iteration, StratumID, SecondaryStratumID, StateClassID, AgeMin, AgeMax, RelativeAmount) SELECT ScenarioID, Iteration, StratumID, SecondaryStratumID, StateClassID, AgeMin, AgeMax, RelativeAmount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_InitialConditionsSpatial")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsSpatial(InitialConditionsSpatialID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, StratumFileName TEXT, SecondaryStratumFileName TEXT, TertiaryStratumFileName TEXT, StateClassFileName TEXT, AgeFileName TEXT)")
            store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsSpatial(ScenarioID, Iteration, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName) SELECT ScenarioID, Iteration, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTarget RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionTarget(TransitionTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionTarget(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionMultiplierValue")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionMultiplierValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionMultiplierValue(TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, TransitionGroupID INTEGER, TransitionMultiplierTypeID  INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionMultiplierValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, TransitionGroupID, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, TransitionGroupID, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TimeSinceTransitionGroup")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionGroup RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TimeSinceTransitionGroup(TimeSinceTransitionGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TimeSinceTransitionGroup(ScenarioID, StratumID, SecondaryStratumID, TransitionTypeID, TransitionGroupID) SELECT ScenarioID, StratumID, SecondaryStratumID, TransitionTypeID, TransitionGroupID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TimeSinceTransitionRandomize")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionRandomize RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TimeSinceTransitionRandomize(TimeSinceTransitionRandomizeID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, MinInitialTST INTEGER, MaxInitialTST INTEGER, Iteration INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TimeSinceTransitionRandomize(ScenarioID, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, MinInitialTST, MaxInitialTST, Iteration) SELECT ScenarioID, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, MinInitialTST, MaxInitialTST, Iteration FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_StateAttributeValue")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_StateAttributeValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_StateAttributeValue(StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, StateAttributeTypeID  INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_StateAttributeValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateAttributeTypeID, AgeMin, AgeMax, Value) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateAttributeTypeID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionAttributeValue")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAttributeValue(TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, TransitionAttributeTypeID  INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionAttributeValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, TransitionAttributeTypeID, AgeMin, AgeMax, Value) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, TransitionAttributeTypeID, AgeMin, AgeMax, Value FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionAttributeTarget")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeTarget RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAttributeTarget(TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionAttributeTarget(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_DistributionValue")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_DistributionValue RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_DistributionValue(DistributionValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, DistributionTypeID INTEGER, ExternalVariableTypeID INTEGER, ExternalVariableMin DOUBLE, ExternalVariableMax DOUBLE, Value DOUBLE, ValueDistributionTypeID INTEGER, ValueDistributionFrequency INTEGER, ValueDistributionSD DOUBLE, ValueDistributionMin DOUBLE, ValueDistributionMax DOUBLE, ValueDistributionRelativeFrequency DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_DistributionValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, Value, ValueDistributionTypeID, ValueDistributionFrequency, ValueDistributionSD, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, Value, ValueDistributionTypeID, ValueDistributionFrequency, ValueDistributionSD, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionDirectionMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionDirectionMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionDirectionMultiplier(TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionDirectionMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionSlopeMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSlopeMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSlopeMultiplier(TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionSlopeMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionAdjacencyMultiplier")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAdjacencyMultiplier RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAdjacencyMultiplier(TransitionAdjacencyMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AttributeValue DOUBLE, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionAdjacencyMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_TransitionPathwayAutoCorrelation")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionPathwayAutoCorrelation RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionPathwayAutoCorrelation(TransitionPathwayAutoCorrelationID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AutoCorrelationFactor DOUBLE, SpreadOnlyToLike INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionPathwayAutoCorrelation(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AutoCorrelationFactor, SpreadOnlyToLike) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AutoCorrelationFactor, SpreadOnlyToLike FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputStratum")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratum RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratum(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputStratum(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputStratumState")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumState RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, StateLabelXID INTEGER, StateLabelYID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputStratumTransition")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransition RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumTransition(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputStratumTransitionState")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransitionState RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumTransitionState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionTypeID INTEGER, StateClassID INTEGER, EndStateClassID INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumTransitionState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputStateAttribute")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputStateAttribute RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("STSim_OutputTransitionAttribute")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputTransitionAttribute RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputTransitionAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    ''' <summary>
    ''' SF0000063
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks>Adds Omit fields for Secondary and Tertiary Strata to OutputOptions</remarks>
    Private Shared Sub STSIM0000063(ByVal store As DataStore)

        If (store.TableExists("STSim_OutputOptions")) Then

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputOptions RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE STSim_OutputOptions(OutputOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, SummaryOutputSC INTEGER, SummaryOutputSCTimesteps INTEGER, SummaryOutputSCZeroValues INTEGER, SummaryOutputTR INTEGER, SummaryOutputTRTimesteps INTEGER, SummaryOutputTRIntervalMean INTEGER, SummaryOutputTRSC INTEGER, SummaryOutputTRSCTimesteps INTEGER, SummaryOutputSA INTEGER, SummaryOutputSATimesteps INTEGER, SummaryOutputTA INTEGER, SummaryOutputTATimesteps INTEGER, SummaryOutputOmitSS INTEGER, SummaryOutputOmitTS INTEGER, RasterOutputSC INTEGER, RasterOutputSCTimesteps INTEGER, RasterOutputTR INTEGER, RasterOutputTRTimesteps INTEGER, RasterOutputAge INTEGER, RasterOutputAgeTimesteps INTEGER, RasterOutputTST INTEGER, RasterOutputTSTTimesteps INTEGER, RasterOutputST INTEGER, RasterOutputSTTimesteps INTEGER, RasterOutputSA INTEGER, RasterOutputSATimesteps INTEGER, RasterOutputTA INTEGER, RasterOutputTATimesteps INTEGER, RasterOutputAATP INTEGER, RasterOutputAATPTimesteps INTEGER)")
            store.ExecuteNonQuery("INSERT INTO STSim_OutputOptions(ScenarioID, SummaryOutputSC, SummaryOutputSCTimesteps, SummaryOutputSCZeroValues, SummaryOutputTR, SummaryOutputTRTimesteps, SummaryOutputTRIntervalMean, SummaryOutputTRSC, SummaryOutputTRSCTimesteps, SummaryOutputSA, SummaryOutputSATimesteps, SummaryOutputTA, SummaryOutputTATimesteps, RasterOutputSC, RasterOutputSCTimesteps, RasterOutputTR, RasterOutputTRTimesteps, RasterOutputAge, RasterOutputAgeTimesteps, RasterOutputTST, RasterOutputTSTTimesteps, RasterOutputST, RasterOutputSTTimesteps, RasterOutputSA, RasterOutputSATimesteps, RasterOutputTA, RasterOutputTATimesteps, RasterOutputAATP, RasterOutputAATPTimesteps) SELECT ScenarioID , SummaryOutputSC, SummaryOutputSCTimesteps, SummaryOutputSCZeroValues, SummaryOutputTR, SummaryOutputTRTimesteps, SummaryOutputTRIntervalMean, SummaryOutputTRSC, SummaryOutputTRSCTimesteps, SummaryOutputSA, SummaryOutputSATimesteps, SummaryOutputTA, SummaryOutputTATimesteps, RasterOutputSC, RasterOutputSCTimesteps, RasterOutputTR, RasterOutputTRTimesteps, RasterOutputAge, RasterOutputAgeTimesteps, RasterOutputTST, RasterOutputTSTTimesteps, RasterOutputST, RasterOutputSTTimesteps, RasterOutputSA, RasterOutputSATimesteps, RasterOutputTA, RasterOutputTATimesteps, RasterOutputAATP, RasterOutputAATPTimesteps FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

End Class
