using LBPUnion.ProjectLighthouse.Database;
using LBPUnion.ProjectLighthouse.Servers.GameServer.Types.Categories;
using LBPUnion.ProjectLighthouse.Types.Entities.Level;
using LBPUnion.ProjectLighthouse.Types.Entities.Token;
using LBPUnion.ProjectLighthouse.Localization;
using LBPUnion.ProjectLighthouse.Localization.StringLists;

namespace LBPUnion.ProjectLighthouse.Servers.GameServer.Types;

public class MyPlaylistsCategory : PlaylistCategory
{
    public override string Name { get; set; } = "My Playlists";
    public override string Description { get; set; } = "Your playlists";
    public TranslatableString LocalizedName { get; set; } = CategoryStrings.MyPlaylistsName;
    public TranslatableString LocalizedDescription { get; set; } = CategoryStrings.MyPlaylistsDesc;
    public override string IconHash { get; set; } = "g820613";
    public override string Endpoint { get; set; } = "my_playlists";
    public override string Tag => "my_playlists";
    public override string[] Types { get; } = { "playlist", };

    public override IQueryable<PlaylistEntity> GetItems(DatabaseContext database, GameTokenEntity token) =>
        database.Playlists.Where(p => p.CreatorId == token.UserId).OrderByDescending(p => p.PlaylistId);

    public override string ResolveName(string language) => this.LocalizedName.Translate(language);
    public override string ResolveDescription(string language) => this.LocalizedDescription.Translate(language);
}