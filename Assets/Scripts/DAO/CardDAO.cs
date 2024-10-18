using System.Collections.Generic;
using SQLite;
using UnityEngine;

public class CardDAO : BaseDAO<Card> {
    public CardDAO(string dbPath) : base(dbPath) { }

    public static Card GetCard(int cardIndex) {
        BaseDAO<Card>.OpenDB();
        try {
            var card = CardDAO.GetList().Find(c => c.Index == cardIndex);
            return card;
        }
        finally {
            BaseDAO<Card>.CloseDB(); // 작업이 끝난 후 DB 연결 닫기
        }
    }

}
