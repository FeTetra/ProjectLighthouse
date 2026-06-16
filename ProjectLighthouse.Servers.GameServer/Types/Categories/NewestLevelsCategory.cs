#nullable enable
using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Extensions;
using LBPUnion.ProjectLighthouse.Filter;
using LBPUnion.ProjectLighthouse.Filter.Sorts;
using LBPUnion.ProjectLighthouse.Localization;
using LBPUnion.ProjectLighthouse.Localization.StringLists;
using LBPUnion.ProjectLighthouse.Types.Entities.Level;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;

public class NewestLevelsCategory : TranslatableSlotCategory
{
    public override TranslatableString LocalizedName { get; set; } = CategoryStrings.NewestLevelsName;
    public override TranslatableString LocalizedDescription { get; set; } = CategoryStrings.NewestLevelsDesc;
    public override string IconHash { get; set; } = "g820623";
    public override string Endpoint { get; set; } = "newest";
    public override string Tag => "newest";

    public override IQueryable<SlotEntity> GetItems(DatabaseContext database, GameTokenEntity token, SlotQueryBuilder queryBuilder) =>
        database.Slots.Where(queryBuilder.Build())
            .ApplyOrdering(new SlotSortBuilder<SlotEntity>().AddSort(new FirstUploadedSort()));
}