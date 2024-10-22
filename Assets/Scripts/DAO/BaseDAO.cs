using System.Collections.Generic;
using System.Data.Common;
using SQLite;
using UnityEngine;

public abstract class BaseDAO<T> where T : new() {
    // DB 경로를 반환하는 메서드
    public static string GetDbPath(string _dbPath = null) {
        return _dbPath ?? $"{Application.persistentDataPath}/DB/GameDB.db";
    }

    // 삽입 메서드
    public static void Insert(T entity) {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            _db.Insert(entity);
        }
    }

    // 모든 아이템 조회 메서드
    public static List<T> GetAllItems() {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            Debug.Log($"Table {typeof(T).Name} created");
            return _db.Table<T>().ToList();
        }
    }
    // Query 문으로 모든 아이템 조회 메서드
    public static List<T> GetAllItems(string query) {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            return _db.Query<T>(query);
        }
    }
    // 특정 아이템 조회 메서드
    public static T GetItem(int id) {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            return _db.Find<T>(id);
        }
    }
    // 업데이트 메서드
    public static void Update(T entity) {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            _db.Update(entity);
        }
    }
    // 삭제 메서드
    public static void Delete(T entity) {
        using (var _db = new SQLiteConnection(GetDbPath())) {
            _db.CreateTable<T>(); // 테이블이 없는 경우 생성
            _db.Delete(entity);
        }
    }
}
