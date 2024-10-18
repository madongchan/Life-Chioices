using System.Collections.Generic;
using SQLite;
using UnityEngine;

public abstract class BaseDAO<T> where T : new() {
    protected static SQLiteConnection _db;

    public BaseDAO(string dbPath = null) {
        if (dbPath == null) {
            dbPath = $"{Application.persistentDataPath}/MyDb.db";
        }

        if (_db == null) {
            _db = new SQLiteConnection(dbPath);
            // DB가 생성 되었는지 확인하는 로그

            _db.CreateTable<T>();
        }
    }

    // DB를 open 확인하는 static 메소드
    public static void OpenDB(string dbPath = null) {
        if (dbPath == null) {
            dbPath = $"{Application.persistentDataPath}/MyDb.db";
        }
        if (_db == null) {
            _db = new SQLiteConnection(dbPath);
        }
    }
    // DB를 close 확인하는 static 메소드
    public static void CloseDB() {
        if (_db != null) {
            _db.Close();
            _db = null;
        }
    }


    public static void Insert(T entity) {
        _db.Insert(entity);
    }

    public static List<T> GetList() {
        return _db.Table<T>().ToList();
    }

    public static T Get(int id) {
        return _db.Find<T>(id);
    }
}
