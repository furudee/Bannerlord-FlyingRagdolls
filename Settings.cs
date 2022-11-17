using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Attributes;

namespace FlyingRagdolls
{
    class Settings : AttributeGlobalSettings<Settings>
    {
        public override string Id => "FlyingRagdolls";
        public override string DisplayName => "Flying Ragdolls " + typeof(Settings).Assembly.GetName().Version.ToString(3);
        public override string FolderName => "FlyingRagdolls";
        public override string FormatType => "xml";

        [SettingPropertyBool("Enable Mod", HintText = "Enables the mod", RequireRestart = false, IsToggle = true)]
        [SettingPropertyGroup("Enable Mod")]
        public bool Enabled { get; set; } = true;

        [SettingPropertyBool("Random Velocity", HintText = "Random velocity for ragdolls, disables set multiplier", RequireRestart = false, Order = 1)]
        [SettingPropertyGroup("Enable Mod")]
        public bool RandomVelocity { get; set; } = false;

        [SettingPropertyBool("Random Angle", HintText = "Random angle for ragdolls, disables set multiplier", RequireRestart = false, Order = 2)]
        [SettingPropertyGroup("Enable Mod")]
        public bool RandomAngle { get; set; } = false;

        [SettingPropertyFloatingInteger("Melee Velocity Multiplier", 0f, 25000f, HintText = "Too little and they won't fly, too much and they die too hard to fly", RequireRestart = false, Order = 3)]
        [SettingPropertyGroup("Enable Mod")]
        public float MeleeVelocityMultiplier { get; set; } = 10000f;

        [SettingPropertyFloatingInteger("Ranged Velocity Multiplier", 0f, 1000f, HintText = "Too little and they won't fly, too much and they die too hard to fly", RequireRestart = false, Order = 4)]
        [SettingPropertyGroup("Enable Mod")]
        public float RangedVelocityMultiplier { get; set; } = 100f;

        [SettingPropertyFloatingInteger("Angle", 0.01f, 89.99f, HintText = "Angle in degrees", RequireRestart = false, Order = 5)]
        [SettingPropertyGroup("Enable Mod")]
        public int Angle { get; set; } = 45;

    }
}
