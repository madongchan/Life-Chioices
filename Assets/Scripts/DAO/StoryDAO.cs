using System.Collections;
using System.Collections.Generic;
using SQLite;
using UnityEngine;

public class StoryDAO : BaseDAO<Story> {
    public static Story GetStory(int Index) {
        var story = StoryDAO.GetItem(Index);
        return story;
    }
    public static List<int> GetStoryCardList(int Index) {
        return GetStory(Index).Cards.ToIntList();
    }
}
