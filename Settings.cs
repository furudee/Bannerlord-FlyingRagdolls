using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Settings.Base.Global;

namespace FlyingRagdolls
{
    class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "FlyingRagdolls";
        public override string DisplayName => "Flying Ragdolls " + typeof(Settings).Assembly.GetName().Version.ToString(3);
        public override string FolderName => "FlyingRagdolls";
        public override string FormatType => "xml";

        [SettingPropertyFloatingInteger("Melee Velocity Multiplier", 0f, 100000f, HintText = "Too little and they won't fly, too much and they die too hard to fly", RequireRestart = false)]
        public float MeleeVelocityMultiplier { get; set; } = 10000f;
        [SettingPropertyFloatingInteger("Ranged Velocity Multiplier", 0f, 100000f, HintText = "Too little and they won't fly, too much and they die too hard to fly", RequireRestart = false)]
        public float RangedVelocityMultiplier { get; set; } = 100f;
        [SettingPropertyFloatingInteger("Angle", 0.01f, 89.99f, HintText = "Angle in degrees", RequireRestart = false)]
        public int Angle { get; set; } = 45;
     
    }
}
