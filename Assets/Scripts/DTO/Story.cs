using System.Collections.Generic;
using SQLite;

public class Story {
    [PrimaryKey, AutoIncrement]
    public int Index { get; set; }
    public string Description { get; set; }
    public int NextIndex { get; set; }
    public string Cards { get; set; }

    public static List<int> GetCardList(Story story) {
        return story.Cards.ToIntList();
    }
}
