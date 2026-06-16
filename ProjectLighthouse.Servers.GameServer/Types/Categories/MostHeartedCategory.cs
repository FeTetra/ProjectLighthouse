#nullable enable
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Extensions;
using LBPUnion.ProjectLighthouse.Filter;
using LBPUnion.ProjectLighthouse.Filter.Sorts.Metadata;
using LBPUnion.ProjectLighthouse.Localization;
using LBPUnion.ProjectLighthouse.Localization.StringLists;
using LBPUnion.ProjectLighthouse.Types.Entities.Level;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;
using LBPUnion.ProjectLighthouse.Types.Misc;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;

public class MostHeartedCategory : TranslatableSlotCategory
{
    public override TranslatableString LocalizedName { get; set; } = CategoryStrings.MostHeartedName;
    public override TranslatableString LocalizedDescription { get; set; } = CategoryStrings.MostHeartedDesc;
    public override string IconHash { get; set; } = "g820607";
    public override string Endpoint { get; set; } = "most_hearted";
    public override string Tag => "most_hearted";

    public override IQueryable<SlotEntity> GetItems(DatabaseContext database, GameTokenEntity token, SlotQueryBuilder queryBuilder) =>
        database.Slots.Select(s => new SlotMetadata
            {
                Slot = s,
                Hearts = database.HeartedLevels.Count(r => r.SlotId == s.SlotId),
            })
            .ApplyOrdering(new SlotSortBuilder<SlotMetadata>().AddSort(new HeartsSort()))
            .Select(s => s.Slot)
            .Where(queryBuilder.Build());
}