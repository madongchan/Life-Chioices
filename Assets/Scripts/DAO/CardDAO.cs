using System.Collections.Generic;
using SQLite;
using UnityEngine;

public class CardDAO : BaseDAO<Card>
{
    public CardDAO(string dbPath) : base(dbPath) { }

    public static Card GetCard(int cardIndex){
        var card = new Card();
        card = CardDAO.GetList().Find(c => c.Index == cardIndex);
        return card;
    }
}
