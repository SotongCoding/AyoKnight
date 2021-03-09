using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_LevelData : MonoBehaviour {
    public static DB_LevelData _instance;
    [SerializeField] List<LevelStatus> levels = new List<LevelStatus> ();

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }
    }

    public static LevelStatus GetLevel (int id) {
        foreach (var item in _instance.levels) {
            if (item.GetID () == id) return item;
        }

        return null;
    }
    public static LevelStatus GetLevel (LevelData level) {
        foreach (var item in _instance.levels) {
            if (item.levelData.Equals (level)) {
                return item;
            }
        }

        return null;
    }

    #region SaveLoad
    static string savePath { get => Application.dataPath + "/SaveData/"; }
    static string fileName { get => "levelData.akdat"; }

    public static void SaveLevelData () {
        SaveLoadManager.SaveData<LevelStatus> (_instance.levels, savePath, fileName);
    }
    public static void LoadLevelData () {
        SaveLoadManager.LoadData<LevelStatus> (savePath, fileName, out List<LevelStatus> loadedData);
        _instance.levels = loadedData;
    }
    #endregion
}

[Serializable]
public class LevelStatus {
    [SerializeField] int levelID;
    public LevelData levelData;
    [SerializeField] bool isOpen;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dificult"> 0 : Normal 1 : Hard</param>
    /// <returns></returns>
    public bool IsOpen () { return isOpen; }
    public int GetID () { return levelID; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dificult"> 0 : Normal 1 : Hard</param>
    /// <returns></returns>
    public void Unlock () {
        isOpen = true;
    }
}