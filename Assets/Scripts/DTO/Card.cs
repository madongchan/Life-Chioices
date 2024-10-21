using SQLite;
using UnityEngine;

public class Card {
    [PrimaryKey, AutoIncrement]
    public int Index { get; set; }
    public string Description { get; set; }
}
