using SQLite;

[Table("CharacterStatus")]
public class CharacterStatus {
    [PrimaryKey, AutoIncrement]
    public int CharacterStatusIndex { get; set; } // 캐릭터는 하나지만 인덱스가 있어야 값을 가져올 수 있음
    // 체력
    public int HP { get; set; }
    // 지능
    public int Intelligence { get; set; }
    // 행복
    public int Happiness { get; set; }
    // 명성
    public int Charm { get; set; }
    // 재력
    public int Money { get; set; }
}
