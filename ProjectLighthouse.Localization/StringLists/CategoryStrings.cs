namespace LBPUnion.ProjectLighthouse.Localization.StringLists;

public static class CategoryStrings
{
    public static readonly TranslatableString TeamPicksName = create("team_picks_name");
    public static readonly TranslatableString MostHeartedName = create("most_hearted_name");
    public static readonly TranslatableString NewestLevelsName = create("newest_levels_name");
    public static readonly TranslatableString MostPlayedName = create("most_played_name");
    public static readonly TranslatableString HighestRatedName = create("highest_rated_name");
    public static readonly TranslatableString MyHeartedCreatorsName = create("my_hearted_creators_name");
    public static readonly TranslatableString MyPlaylistsName = create("my_playlists_name");
    public static readonly TranslatableString QueueName = create("queue_name");
    public static readonly TranslatableString HeartedName = create("hearted_name");
    public static readonly TranslatableString LuckyDipName = create("lucky_dip_name");

    public static readonly TranslatableString TeamPicksDesc = create("team_picks_desc");
    public static readonly TranslatableString MostHeartedDesc = create("most_hearted_desc");
    public static readonly TranslatableString NewestLevelsDesc = create("newest_levels_desc");
    public static readonly TranslatableString MostPlayedDesc = create("most_played_desc");
    public static readonly TranslatableString HighestRatedDesc = create("highest_rated_desc");
    public static readonly TranslatableString MyHeartedCreatorsDesc = create("my_hearted_creators_desc");
    public static readonly TranslatableString MyPlaylistsDesc = create("my_playlists_desc");
    public static readonly TranslatableString QueueDesc = create("queue_desc");
    public static readonly TranslatableString HeartedDesc = create("hearted_desc");
    public static readonly TranslatableString LuckyDipDesc = create("lucky_dip_desc");

    private static TranslatableString create(string key) => new(TranslationAreas.Categories, key);
}