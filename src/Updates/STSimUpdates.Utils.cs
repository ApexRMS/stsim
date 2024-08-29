// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Globalization;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        private static void MigrateTabularOutputOptions(DataStore store)
        {
            store.ExecuteNonQuery(@"CREATE TABLE stsim_OutputOptions ( 
                    OutputOptionsID                      INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                           INTEGER,
                    SummaryOutputSC                      INTEGER,
                    SummaryOutputSCTimesteps             INTEGER,
                    SummaryOutputSCAges                  INTEGER,
                    SummaryOutputSCZeroValues            INTEGER,
                    SummaryOutputTR                      INTEGER,
                    SummaryOutputTRTimesteps             INTEGER,
                    SummaryOutputTRAges                  INTEGER,
                    SummaryOutputTRIntervalMean          INTEGER,
                    SummaryOutputTRSC                    INTEGER,
                    SummaryOutputTRSCTimesteps           INTEGER,
                    SummaryOutputSA                      INTEGER,
                    SummaryOutputSATimesteps             INTEGER,
                    SummaryOutputSAAges                  INTEGER,
                    SummaryOutputTA                      INTEGER,
                    SummaryOutputTATimesteps             INTEGER,
                    SummaryOutputTAAges                  INTEGER,
                    SummaryOutputOmitSS                  INTEGER,
                    SummaryOutputOmitTS                  INTEGER)");

            store.ExecuteNonQuery(@"INSERT INTO stsim_OutputOptions(
                    ScenarioID                           ,
                    SummaryOutputSC                      ,
                    SummaryOutputSCTimesteps             ,
                    SummaryOutputSCAges                  ,
                    SummaryOutputSCZeroValues            ,
                    SummaryOutputTR                      ,
                    SummaryOutputTRTimesteps             ,
                    SummaryOutputTRAges                  ,
                    SummaryOutputTRIntervalMean          ,
                    SummaryOutputTRSC                    ,
                    SummaryOutputTRSCTimesteps           ,
                    SummaryOutputSA                      ,
                    SummaryOutputSATimesteps             ,
                    SummaryOutputSAAges                  ,
                    SummaryOutputTA                      ,
                    SummaryOutputTATimesteps             ,
                    SummaryOutputTAAges                  ,
                    SummaryOutputOmitSS                  ,
                    SummaryOutputOmitTS) 
                    SELECT 
                    ScenarioID                           ,
                    SummaryOutputSC                      ,
                    SummaryOutputSCTimesteps             ,
                    SummaryOutputSCAges                  ,
                    SummaryOutputSCZeroValues            ,
                    SummaryOutputTR                      ,
                    SummaryOutputTRTimesteps             ,
                    SummaryOutputTRAges                  ,
                    SummaryOutputTRIntervalMean          ,
                    SummaryOutputTRSC                    ,
                    SummaryOutputTRSCTimesteps           ,
                    SummaryOutputSA                      ,
                    SummaryOutputSATimesteps             ,
                    SummaryOutputSAAges                  ,
                    SummaryOutputTA                      ,
                    SummaryOutputTATimesteps             ,
                    SummaryOutputTAAges                  ,
                    SummaryOutputOmitSS                  ,
                    SummaryOutputOmitTS
                    FROM TEMP_TABLE");
        }

        private static void MigrateSpatialOutputOptions(DataStore store)
        {
            store.ExecuteNonQuery(@"CREATE TABLE stsim_OutputOptionsSpatial ( 
                    OutputOptionsSpatialID               INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                           INTEGER,
                    RasterOutputSC                       INTEGER,
                    RasterOutputSCTimesteps              INTEGER,
                    RasterOutputAge                      INTEGER,
                    RasterOutputAgeTimesteps             INTEGER,
                    RasterOutputST                       INTEGER,
                    RasterOutputSTTimesteps              INTEGER,
                    RasterOutputTR                       INTEGER,
                    RasterOutputTRTimesteps              INTEGER,
                    RasterOutputTransitionEvents         INTEGER,
                    RasterOutputTransitionEventTimesteps INTEGER,
                    RasterOutputTST                      INTEGER,
                    RasterOutputTSTTimesteps             INTEGER,
                    RasterOutputSA                       INTEGER,
                    RasterOutputSATimesteps              INTEGER,
                    RasterOutputTA                       INTEGER,
                    RasterOutputTATimesteps              INTEGER)");

            store.ExecuteNonQuery(@"INSERT INTO stsim_OutputOptionsSpatial(
                    ScenarioID                           ,
                    RasterOutputSC                       ,
                    RasterOutputSCTimesteps              ,
                    RasterOutputTR                       ,
                    RasterOutputTRTimesteps              ,
                    RasterOutputAge                      ,
                    RasterOutputAgeTimesteps             ,
                    RasterOutputTST                      ,
                    RasterOutputTSTTimesteps             ,
                    RasterOutputST                       ,
                    RasterOutputSTTimesteps              ,
                    RasterOutputSA                       ,
                    RasterOutputSATimesteps              ,
                    RasterOutputTA                       ,
                    RasterOutputTATimesteps              ,
                    RasterOutputTransitionEvents         ,
                    RasterOutputTransitionEventTimesteps) 
                    SELECT 
                    ScenarioID                           ,
                    RasterOutputSC                       ,
                    RasterOutputSCTimesteps              ,
                    RasterOutputTR                       ,
                    RasterOutputTRTimesteps              ,
                    RasterOutputAge                      ,
                    RasterOutputAgeTimesteps             ,
                    RasterOutputTST                      ,
                    RasterOutputTSTTimesteps             ,
                    RasterOutputST                       ,
                    RasterOutputSTTimesteps              ,
                    RasterOutputSA                       ,
                    RasterOutputSATimesteps              ,
                    RasterOutputTA                       ,
                    RasterOutputTATimesteps              ,
                    RasterOutputTransitionEvents         ,
                    RasterOutputTransitionEventTimesteps 
                    FROM TEMP_TABLE");
        }

        private static void MigrateSpatialAveragingOutputOptions(DataStore store)
        {
            store.ExecuteNonQuery(@"CREATE TABLE stsim_OutputOptionsSpatialAverage ( 
                    OutputOptionsSpatialAverageID        INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                           INTEGER,
                    AvgRasterOutputSC                    INTEGER,
                    AvgRasterOutputSCTimesteps           INTEGER,
                    AvgRasterOutputSCCumulative          INTEGER,
                    AvgRasterOutputAge                   INTEGER,
                    AvgRasterOutputAgeTimesteps          INTEGER,
                    AvgRasterOutputAgeCumulative         INTEGER,
                    AvgRasterOutputST                    INTEGER,
                    AvgRasterOutputSTTimesteps           INTEGER,
                    AvgRasterOutputSTCumulative          INTEGER,
                    AvgRasterOutputTP                    INTEGER,
                    AvgRasterOutputTPTimesteps           INTEGER,
                    AvgRasterOutputTPCumulative          INTEGER,
                    AvgRasterOutputTST                   INTEGER,
                    AvgRasterOutputTSTTimesteps          INTEGER,
                    AvgRasterOutputTSTCumulative         INTEGER,
                    AvgRasterOutputSA                    INTEGER,
                    AvgRasterOutputSATimesteps           INTEGER,
                    AvgRasterOutputSACumulative          INTEGER,
                    AvgRasterOutputTA                    INTEGER,
                    AvgRasterOutputTATimesteps           INTEGER,
                    AvgRasterOutputTACumulative          INTEGER)");

            store.ExecuteNonQuery(@"INSERT INTO stsim_OutputOptionsSpatialAverage(
                    ScenarioID                           ,
                    AvgRasterOutputTP                    ,
                    AvgRasterOutputTPTimesteps) 
                    SELECT 
                    ScenarioID                           ,
                    RasterOutputAATP                     ,
                    RasterOutputAATPTimesteps 
                    FROM TEMP_TABLE");
        }
    }
}
