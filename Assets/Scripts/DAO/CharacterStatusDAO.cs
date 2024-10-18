using System.Collections.Generic;
using SQLite;
using UnityEngine;

public class CharacterStatusDAO : BaseDAO<CharacterStatus> {
    public static CharacterStatus GetCharacterStatus(int Index) {
        var characterStatus = CharacterStatusDAO.GetItem(Index);
        return characterStatus;
    }
}