using SQLite;

public class CharacterStatus
{
    [PrimaryKey, AutoIncrement]
    public int Index { get; set; } // 캐릭터는 하나지만 인덱스가 있어야 여러 기능을 구현할 수 있음
    // 체력
    public int HP { get; set; }
    // 지능
    public int Intelligence { get; set; }
    // 행복
    public int Happiness { get; set; }
    // 명성
    public int Honor { get; set; }
    // 재력
    public int Money { get; set; }
}
