using YamlDotNet.Serialization;

namespace LBPUnion.ProjectLighthouse.Configuration;

public class EnforceEmailConfiguration : ConfigurationBase<EnforceEmailConfiguration>
{
    public override int ConfigVersion { get; set; } = 1;

    public override string ConfigName { get; set; } = "enforce-email.yml";

    public override bool NeedsConfiguration { get; set; } = false;

    public bool EmailEnforcementEnabled = false;

    public override ConfigurationBase<EnforceEmailConfiguration> Deserialize
        (IDeserializer deserializer, string text) =>
        deserializer.Deserialize<EnforceEmailConfiguration>(text);
}