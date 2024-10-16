using System.Collections.Generic;
using SQLite;
using UnityEngine;

public abstract class BaseDAO<T> where T : new()
{
    protected static SQLiteConnection _db;

    public BaseDAO(string dbPath = null)
    {
        if (dbPath == null)
        {
            dbPath = $"{Application.persistentDataPath}/MyDb.db";
        }

        if (_db == null)
        {
            _db = new SQLiteConnection(dbPath);
            // DB가 생성 되었는지 확인하는 로그
            
            _db.CreateTable<T>();
        }
    }

    public static void Insert(T entity)
    {
        _db.Insert(entity);
    }

    public static List<T> GetList()
    {
        return _db.Table<T>().ToList();
    }

    public static T Get(int id)
    {
        return _db.Find<T>(id);
    }
}
