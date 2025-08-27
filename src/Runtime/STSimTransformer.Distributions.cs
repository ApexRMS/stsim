// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Initializes the distribution provider
        /// </summary>
        /// <remarks></remarks>
        private void InitializeDistributionProvider()
        {
            Debug.Assert(this.m_DistributionProvider == null);
            this.m_DistributionProvider = new STSimDistributionProvider(this.ResultScenario, this.m_RandomGenerator);
        }

        /// <summary>
        /// Initializes the value for all collection items with distributions.
        /// </summary>
        /// <remarks></remarks>
        private void InitializeDistributionValues()
        {
            this.m_DistributionProvider.InitializeExternalVariableValues();
            this.m_DistributionProvider.STSimInitializeDistributionValues();

            this.InitializeStateAttributeValueDistributionValues();
            this.InitializeTransitionAttributeValueDistributionValues();
            this.InitializeTransitionTargetDistributionValues();
            this.InitializeTransitionAttributeTargetDistributionValues();
            this.InitializeTransitionMultiplierDistributionValues();
            this.InitializeTransitionDirectionMultiplierDistributionValues();
            this.InitializeTransitionSlopeMultiplierDistributionValues();
            this.InitializeTransitionAdjacencyMultiplierDistributionValues();
        }

        private void InitializeStateAttributeValueDistributionValues()
        {
            try
            {
                foreach (StateAttributeValue t in this.m_StateAttributeValues)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("State Attribute Values" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionAttributeValueDistributionValues()
        {
            try
            {
                foreach (TransitionAttributeValue t in this.m_TransitionAttributeValues)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Attribute Values" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionTargetDistributionValues()
        {
            try
            {
                List<string> truncatedDists = new List<string>();

                foreach (TransitionTarget t in this.m_TransitionTargets)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);

                        if (t.CurrentValue.HasValue && (t.CurrentValue < 0.0))
                        {
                            t.CurrentValue = 0.0;
                            string distName = GetProjectItemName(Constants.DATASHEET_CORE_DISTRIBUTION_TYPE, t.DistributionTypeId);

                            if (!truncatedDists.Contains(distName))
                            {
                                truncatedDists.Add(distName);
                            }
                        }
                    }
                }

                if (truncatedDists.Count > 0)
                {
                    string truncatedDistsString = string.Join(", ", truncatedDists);
                    this.RecordStatus(StatusType.Warning, String.Format(
                        "The following distributions produce a negative transition target that has been truncated to 0: {0}",
                        truncatedDistsString));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Targets" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionAttributeTargetDistributionValues()
        {
            try
            {
                foreach (TransitionAttributeTarget t in this.m_TransitionAttributeTargets)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Attribute Targets" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionMultiplierDistributionValues()
        {
            try
            {
                foreach (TransitionMultiplierValue t in this.m_TransitionMultiplierValues)
                {
                    t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Multiplier Values" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionDirectionMultiplierDistributionValues()
        {
            try
            {
                foreach (TransitionDirectionMultiplier t in this.m_TransitionDirectionMultipliers)
                {
                    t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Direction Multipliers" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionSlopeMultiplierDistributionValues()
        {
            try
            {
                foreach (TransitionSlopeMultiplier t in this.m_TransitionSlopeMultipliers)
                {
                    t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Slope Multipliers" + " -> " + ex.Message);
            }
        }

        private void InitializeTransitionAdjacencyMultiplierDistributionValues()
        {
            try
            {
                foreach (TransitionAdjacencyMultiplier t in this.m_TransitionAdjacencyMultipliers)
                {
                    t.Initialize(this.MinimumIteration, this.MinimumTimestep, this.m_DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Adjacency Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples the external variable values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        /// <remarks></remarks>
        private void ResampleExternalVariableValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                this.m_DistributionProvider.SampleExternalVariableValues(iteration, timestep, frequency);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("External Variable Values" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples the distribution values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        /// <remarks></remarks>
        private void ResampleDistributionValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                this.m_DistributionProvider.SampleDistributionValues(iteration, timestep, frequency);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Distribution Values" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples State Attribute values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        private void ResampleStateAttributeValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (StateAttributeValue t in this.m_StateAttributeValues)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("State Attribute Values" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Attribute values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        private void ResampleTransitionAttributeValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionAttributeValue t in this.m_TransitionAttributeValues)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Attribute Values" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Target values
        /// </summary>
        /// <remarks></remarks>
        private void ResampleTransitionTargetValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                List<string> truncatedDists = new List<string>();

                foreach (TransitionTarget t in this.m_TransitionTargets)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);

                        if (t.CurrentValue.HasValue && (t.CurrentValue < 0.0))
                        {
                            t.CurrentValue = 0.0;
                            string distName = GetProjectItemName(Constants.DATASHEET_CORE_DISTRIBUTION_TYPE, t.DistributionTypeId);

                            if (!truncatedDists.Contains(distName))
                            {
                                truncatedDists.Add(distName);
                            }
                        }
                    }
                }

                if (truncatedDists.Count > 0)
                {
                    string truncatedDistsString = string.Join(", ", truncatedDists);
                    this.RecordStatus(StatusType.Warning, String.Format(
                        "The following distributions produce a negative transition target that has been truncated to 0: {0}",
                        truncatedDistsString));
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Targets" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Attribute Target values
        /// </summary>
        /// <remarks></remarks>
        private void ResampleTransitionAttributeTargetValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionAttributeTarget t in this.m_TransitionAttributeTargets)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Attribute Targets" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Multiplier values
        /// </summary>
        /// <remarks></remarks>
        private void ResampleTransitionMultiplierValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionMultiplierValue t in this.m_TransitionMultiplierValues)
                {
                    t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Mulitplier Values" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Direction Multiplier values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        /// <remarks></remarks>
        private void ResampleTransitionDirectionMultiplierValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionDirectionMultiplier t in this.m_TransitionDirectionMultipliers)
                {
                    t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Direction Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Slope Multiplier values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        /// <remarks></remarks>
        private void ResampleTransitionSlopeMultiplierValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionSlopeMultiplier t in this.m_TransitionSlopeMultipliers)
                {
                    t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Slope Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Resamples Transition Adjacency Multiplier values
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="frequency"></param>
        /// <remarks></remarks>
        private void ResampleTransitionAdjacencyMultiplierValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (TransitionAdjacencyMultiplier t in this.m_TransitionAdjacencyMultipliers)
                {
                    t.Sample(iteration, timestep, this.m_DistributionProvider, frequency);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Transition Adjacency Multipliers" + " -> " + ex.Message);
            }
        }

        private string GetProjectItemName(string dataSheetName, int? id)
        {
            if (!id.HasValue)
            {
                return "NULL";
            }
            else
            {
                DataSheet ds = this.Project.GetDataSheet(dataSheetName);
                return ds.ValidationTable.GetDisplayName(id.Value);
            }
        }
    }
}
