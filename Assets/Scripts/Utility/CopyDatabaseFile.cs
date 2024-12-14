using System.IO;
using UnityEngine;

public class CopyDatabaseFile {
    // 게임 시작 시 실행되도록 설정
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void CopyFile() {
        // StreamingAssets의 원본 경로
        string sourcePath = Path.Combine(Application.streamingAssetsPath, "GameDB.db");

        // 대상 경로
        string destinationDirectory = Path.Combine(Application.persistentDataPath, "DB");
        string destinationPath = Path.Combine(destinationDirectory, "GameDB.db");

        try {
            // 대상 폴더가 존재하지 않으면 생성
            if (!Directory.Exists(destinationDirectory)) {
                Directory.CreateDirectory(destinationDirectory);
            }

            // 파일 복사
            if (File.Exists(sourcePath)) {
                File.Copy(sourcePath, destinationPath, true); // true: 덮어쓰기
                Debug.Log("GameDB.db 파일이 성공적으로 복사되었습니다.");
            }
            else {
                Debug.LogWarning($"파일이 존재하지 않습니다: {sourcePath}. 빈 파일을 생성합니다.");
                File.WriteAllText(destinationPath, ""); // 빈 파일 생성
            }
        }
        catch (IOException e) {
            Debug.LogError($"파일 복사 중 오류 발생: {e.Message}");
        }
    }
}
