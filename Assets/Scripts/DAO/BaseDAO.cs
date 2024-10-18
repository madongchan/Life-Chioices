using System.Collections.Generic;
using SQLite;
using UnityEngine;

public abstract class BaseDAO<T> where T : new() {
    // DB 경로를 반환하는 메서드
    public static string GetDbPath(string dbPath = null) {
        return dbPath ?? $"{Application.persistentDataPath}/DB/GameDB.db";
    }
    // 삽입 메서드
    public static void Insert(T entity) {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            db.Insert(entity);
        }
    }
    // 모든 아이템 조회 메서드
    public static List<T> GetAllItems() {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            return db.Table<T>().ToList();
        }
    }
    // Query 문으로 모든 아이템 조회 메서드
    public static List<T> GetAllItems(string query) {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            return db.Query<T>(query);
        }
    }
    // 특정 아이템 조회 메서드
    public static T GetItem(int id) {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            return db.Find<T>(id);
        }
    }
    // 업데이트 메서드
    public static void Update(T entity) {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            db.Update(entity);
        }
    }
    // 삭제 메서드
    public static void Delete(T entity) {
        using (var db = new SQLiteConnection(GetDbPath())) {
            db.CreateTable<T>(); // 테이블이 없는 경우 생성
            db.Delete(entity);
        }
    }
}
