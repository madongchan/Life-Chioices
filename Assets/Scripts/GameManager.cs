using System.Collections.Generic;
using SQLite;
using UnityEngine;
// The library contains simple attributes that you can use
// to control the construction of tables, ORM style
public class Player
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    void Start()
    {
        CardDAO cardDAO = new CardDAO(null);
        Card newCard = new Card { Index = 1, Name = "Ace", Description = "This is an ace card." };
        CardDAO.Insert(newCard);

        // 모든 카드 조회
        var card = CardDAO.GetCard(1);
        Debug.Log($"Card: {card.Name}, Description: {card.Description}");
    }
}