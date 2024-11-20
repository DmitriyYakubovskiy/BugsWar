using System.Collections.Generic;
using System.Linq;

public static class GameDataHelper
{
    private static Dictionary<Tags, string> tags = new Dictionary<Tags, string>()
    {
        { Tags.BlueTeam,"BlueTeam" },
        { Tags.RedTeam,"RedTeam" },

    };

    public static string GetTag(Tags tag)
    {
        return tags[tag];
    }

    public static Tags GetTag(string tag)
    {
        return tags.Keys.Where(x => tags[x]==tag).FirstOrDefault();
    }
}
