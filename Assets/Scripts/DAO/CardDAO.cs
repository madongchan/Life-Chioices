using System.Collections.Generic;
using SQLite;
using Unity.VisualScripting;
using UnityEngine;

public class CardDAO : BaseDAO<Card> {
    public static Card GetCard(int Index) {
        var card = CardDAO.GetItem(Index);
        return card;
    }
}
