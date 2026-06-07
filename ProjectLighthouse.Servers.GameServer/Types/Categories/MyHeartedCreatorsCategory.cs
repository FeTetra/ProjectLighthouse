using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Localization;
using LBPUnion.ProjectLighthouse.Localization.StringLists;
using LBPUnion.ProjectLighthouse.Types.Entities.Profile;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;
using Microsoft.EntityFrameworkCore;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;

public class MyHeartedCreatorsCategory : UserCategory
{
    public override string Name { get; set; } = "My Hearted Creators";
    public override string Description { get; set; } = "Creators you've hearted";
    public TranslatableString LocalizedName { get; set; } = CategoryStrings.MyHeartedCreatorsName;
    public TranslatableString LocalizedDescription { get; set; } = CategoryStrings.MyHeartedCreatorsDesc;
    public override string IconHash { get; set; } = "g820612";
    public override string Endpoint { get; set; } = "favourite_creators";
    public override string Tag => "favourite_creators";

    public override IQueryable<UserEntity> GetItems(DatabaseContext database, GameTokenEntity token) =>
        database.HeartedProfiles.Where(h => h.UserId == token.UserId)
            .OrderByDescending(h => h.UserId)
            .Include(h => h.HeartedUser)
            .Select(h => h.HeartedUser);
            
    public override string ResolveName(string language) => this.LocalizedName.Translate(language);
    public override string ResolveDescription(string language) => this.LocalizedDescription.Translate(language);
}