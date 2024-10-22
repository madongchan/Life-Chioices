using System.Collections.Generic;
using SQLite;

[Table("Story")]
public class Story {
    [PrimaryKey, AutoIncrement]
    public int StoryIndex { get; set; }
    public string Description { get; set; }
    public string Cards { get; set; }
}
