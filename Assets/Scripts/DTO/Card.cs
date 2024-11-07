using SQLite;
using UnityEngine;

[Table("Card")]
public class Card {
    [PrimaryKey, AutoIncrement]
    public int CardIndex { get; set; }
    public string Description { get; set; }
    public int NextIndex { get; set; }
    public int HP { get; set; }
    public int Intelligence { get; set; }
    public int Happiness { get; set; }
    public int Charm { get; set; }
    public int Money { get; set; }
}
