using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    public static LevelLoader _instance;
    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
    }
    LevelData selectedLevel;
    int curentLevel;

    public static void LoadLevel (int level) {
        _instance.selectedLevel = DB_LevelData.GetLevel (level).levelData;
        _instance.curentLevel = level;
    }
    public static void ReLoadLevel () {
        SceneLoader.UnloadScene (3);
        SceneLoader.LoadScene (3);
        LoadLevel (_instance.curentLevel);
    }

    public static LevelData getLevelData () {
        return _instance.selectedLevel;
    }
}