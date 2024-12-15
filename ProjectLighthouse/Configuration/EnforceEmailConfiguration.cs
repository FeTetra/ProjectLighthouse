#nullable enable
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace LBPUnion.ProjectLighthouse.Configuration;

public class EnforceEmailConfiguration : ConfigurationBase<EnforceEmailConfiguration>
{
    public override int ConfigVersion { get; set; } = 1;

    public override string ConfigName { get; set; } = "enforce-email.yml";

    public override bool NeedsConfiguration { get; set; } = false;

    public static bool EmailEnforcementEnabled = false;

    // No blacklist by default, add path to blacklist
    private static string BlacklistFilePath = Path.GetFullPath("");

    public static readonly HashSet<string> BlackListedDomains = new(File.ReadAllLines(BlacklistFilePath));

    public override ConfigurationBase<EnforceEmailConfiguration> Deserialize
        (IDeserializer deserializer, string text) =>
        deserializer.Deserialize<EnforceEmailConfiguration>(text);
}