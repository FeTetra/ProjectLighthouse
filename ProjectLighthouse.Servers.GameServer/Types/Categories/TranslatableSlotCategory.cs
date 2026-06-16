using LBPUnion.ProjectLighthouse.Localization;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;

public abstract class TranslatableSlotCategory : SlotCategory
{
    public override string Name { get; set; } = "";
    public override string Description { get; set; } = "";
    public abstract TranslatableString LocalizedName { get; set; }
    public abstract TranslatableString LocalizedDescription { get; set; }

    public override string ResolveName(string language) => this.LocalizedName.Translate(language);
    public override string ResolveDescription(string language) => this.LocalizedDescription.Translate(language);
}