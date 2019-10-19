/**
 * ModuleIONPoweredRCS.cs
 * (C) Copyright 2015-6, Jamie Leighton (JPLRepo)
 * IONRCS has been split from AmpYear mod.
 * The original code and concept of AmpYear rights go to SodiumEyes on the Kerbal Space Program Forums, which was covered by GNU License GPL (no version stated).
 * As such this code continues to be covered by GNU GPL license.
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad. 

 *  This file is part of IONRCS.
 *
 *  IONRCS is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  IONRCS is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with IONRCS.  If not, see <http://www.gnu.org/licenses/>.
 *
 */

using System.Collections.Generic;
using UnityEngine;

namespace IONRCS
{
    public class ModuleIONPoweredRCS : ModuleRCSFX, IResourceConsumer
    {
        //private Propellant definition;
        private string ElecChge = "ElectricCharge"; 
        private string Xenon = "XenonGas";

        // New context menu info
        [KSPField(isPersistant = false, guiName = "#autoLOC_IONRCS_00001", guiUnits = "#autoLOC_7001400", guiFormat = "F3", guiActive = true)] //#autoLOC_IONRCS_00001 = Current EC Usage
        public float ElecUsed = 0f;

        [KSPField(isPersistant = true, guiName = "#autoLOC_IONRCS_00002", guiUnits = "#autoLOC_7001400", guiFormat = "F3", guiActive = true)] //#autoLOC_IONRCS_00002 = Xenon Ratio
        public float xenonRatio = 0f;

        [KSPField(isPersistant = true, guiName = "#autoLOC_IONRCS_00003", guiUnits = "#autoLOC_7001400", guiFormat = "F3", guiActive = true)] //#autoLOC_IONRCS_00003 = Power Ratio
        public float powerRatio = 0f;
        
        public new List<PartResourceDefinition> GetConsumedResources()
        {
            List<PartResourceDefinition> resources = new List<PartResourceDefinition>();
            PartResourceDefinition EC = PartResourceLibrary.Instance.GetDefinition(ElecChge);
            resources.Add(EC);
            PartResourceDefinition XenonGas = PartResourceLibrary.Instance.GetDefinition(Xenon);
            resources.Add(XenonGas);
            return resources;
        }

        public float electricityUse
        {
            get
            {
                float ElecUsedTmp = 0f;
                foreach (Propellant propellant in propellants)
                {
                    if (propellant.name == ElecChge)
                    {
                        ElecUsedTmp = (float)propellant.currentRequirement;
                    }
                }
                return ElecUsedTmp;
            }
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            foreach (Propellant propellant in propellants)
            {
                if (propellant.name == ElecChge)
                {
                    powerRatio = propellant.ratio;
                }
                if (propellant.name == Xenon)
                {
                    xenonRatio = propellant.ratio;
                }
            }
        }

        public new void FixedUpdate()
        {
            base.FixedUpdate();
            ElecUsed = electricityUse;
        }
    }
}