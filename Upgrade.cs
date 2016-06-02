using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaveUpgradePipeline;

namespace IONRCS
{
    [UpgradeModule(LoadContext.SFS | LoadContext.Craft, sfsNodeUrl = "GAME/FLIGHTSTATE/VESSEL/PART",
        craftNodeUrl = "PART")]
    public class ModuleAmpYeartoIONRCS : UpgradeScript
    {
        public override string Name
        {
            get { return "AmpYear to IONRCS Part converter"; }
        }

        public override string Description
        {
            get
            {
                return
                    "Upgrades old AmpYear Parts ModuleAmpYearPPTRCS and ModuleAmpYearPoweredRCS to IONRCS ModulePPTPoweredRCS and ModuleIONPoweredRCS.";
            }
        }

        public override Version EarliestCompatibleVersion
        {
            get { return new Version(0, 21, 0); }
        }

        public override Version TargetVersion
        {
            get { return new Version(1, 1, 0); }
        }

        public override TestResult OnTest(ConfigNode node, LoadContext loadContext, ref string nodeName)
        {
            nodeName = NodeUtil.GetPartNodeName(node, loadContext);
            TestResult testResult = TestResult.Pass;
            ConfigNode[] nodes = node.GetNodes("MODULE");
            TestResult result;
            int num;
            for (int i = nodes.Length - 1; i >= 0; i = num)
            {
                string value = nodes[i].GetValue("name");
                string a = value;
                if (a == "ModuleAmpYearPPTRCS" || a == "ModuleAmpYearPoweredRCS")
                {
                    result = TestResult.Upgradeable;
                    return result;
                }
                num = i - 1;
            }
            result = testResult;
            return result;
        }

        public override void OnUpgrade(ConfigNode node, LoadContext loadContext)
        {
            ConfigNode[] nodes = node.GetNodes("MODULE");
            int num;
            for (int i = nodes.Length - 1; i >= 0; i = num)
            {
                string value = nodes[i].GetValue("name");
                string a = value;
                if (a == "ModuleAmpYearPPTRCS")
                {
                    nodes[i].SetValue("name", "ModulePPTPoweredRCS", false);
                }
                else
                {
                    if (a == "ModuleAmpYearPoweredRCS")
                    {
                        nodes[i].SetValue("name", "ModuleIONPoweredRCS", false);
                    }
                }
                num = i - 1;
            }
        }
    }
}