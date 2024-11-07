using System.Collections.Generic;
using SQLite;
using UnityEngine;

public class CharacterStatusDAO : BaseDAO<CharacterStatus> {
    public static CharacterStatus GetCharacterStatus(int Index = 1) {
        var characterStatus = CharacterStatusDAO.GetItem(Index);
        return characterStatus;
    }
    // 정보 업데이트
    public static void UpdateCharacterStatus(CharacterStatus characterStatus) {
        CharacterStatusDAO.Update(characterStatus);
    }
    // 요소들을 매개변수로 받아 초기화 후 업데이트
    public static void UpdateCharacterStatus(int HP, int Intelligence, int Happiness, int Charm, int Money) {
        CharacterStatus characterStatus = GetCharacterStatus();
        characterStatus.HP += HP;
        characterStatus.Intelligence += Intelligence;
        characterStatus.Happiness += Happiness;
        characterStatus.Charm += Charm;
        characterStatus.Money += Money;
        CharacterStatusDAO.Update(characterStatus);
    }
}