using SQLite;
using UnityEngine;

[Table("Card")]
public class Card {
    [PrimaryKey, AutoIncrement]
    public int CardIndex { get; set; }
    public string Description { get; set; }
    public int NextIndex { get; set; }
}
