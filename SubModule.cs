using TaleWorlds.MountAndBlade;
using HarmonyLib;

namespace FlyingRagdolls

{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("mod.bannerlord.whatever").PatchAll();
        }
    }
}
