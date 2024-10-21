using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;

public class LoadSceneWindow : EditorWindow {
    [SerializeField]
    private Vector2 scrollPos = Vector2.zero;

    [MenuItem("Window/LoadScene Window")]
    public static void ShowWindow() {
        GetWindow(typeof(LoadSceneWindow));
    }

    void OnEnable() {

    }

    void OpenScene(string scenePath) {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
            EditorSceneManager.OpenScene(scenePath);
        }
    }

    private void OnGUI() {
        //scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        //메인 메뉴로 이동 한다는 안내 문구
        GUILayout.Label("메인씬", EditorStyles.boldLabel);
        if (GUILayout.Button("MainScene", GUILayout.Height(30))) {
            OpenScene("Assets/Scenes/MainScene.unity");
        }
        // 로딩 화면으로 이동하는 버튼
        GUILayout.Label("로딩 화면", EditorStyles.boldLabel);
        if (GUILayout.Button("LoadingScene", GUILayout.Height(30))) {
            OpenScene("Assets/Scenes/LoadingScene.unity");
        }
        // streamingAssetsPath : 읽기 전용 데이터가 저장되는 폴더
        GUILayout.Label("읽기 전용 데이터가 저장되는 폴더", EditorStyles.boldLabel);
        if (GUILayout.Button("streamingAssetsPath Folder", GUILayout.Height(40))) {
            Application.OpenURL("file:///" + Application.streamingAssetsPath);
        }
        // persistentDataPath : 읽기/쓰기가 가능한 데이터가 저장되는 폴더
        GUILayout.Label("읽기/쓰기가 가능한 데이터가 저장되는 폴더", EditorStyles.boldLabel);
        if (GUILayout.Button("persistentDataPath Folder", GUILayout.Height(40))) {
            Application.OpenURL("file:///" + Application.persistentDataPath);
        }
        // 게임 데이터베이스 파일을 열 수 있는 버튼
        GUILayout.Label("게임 데이터베이스 파일", EditorStyles.boldLabel);
        if (GUILayout.Button("GameDB", GUILayout.Height(40))) {
            Application.OpenURL("file:///" + Application.persistentDataPath + "/DB/GameDB.db");
        }
    }
}
