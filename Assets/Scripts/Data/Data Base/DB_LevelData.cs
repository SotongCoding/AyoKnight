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
#if UNITY_ANDROID
    static string savePath { get => Application.persistentDataPath + "/"; }
#elif UNITY_EDITOR
    static string savePath { get => Application.dataPath + "/SaveData/"; }
#else
    static string savePath { get => Application.persistentDataPath + "/"; }
#endif

    static string fileName { get => "levelData.akdat"; }

    public static void SaveLevelData () {
        List<SaveData_DBLevel> saved = new List<SaveData_DBLevel> ();
        foreach (var item in _instance.levels) {
            saved.Add (new SaveData_DBLevel (item));
        }
        SaveLoadManager.SaveData<SaveData_DBLevel> (saved, savePath, fileName);
    }
    public static void LoadLevelData () {
        SaveLoadManager.LoadData<SaveData_DBLevel> (savePath, fileName, out List<SaveData_DBLevel> loadedData);
        if (loadedData.Count > 0) {
            foreach (var item in loadedData) {
                int index = _instance.levels.IndexOf (GetLevel (item.id));
                if (item.isOpen) {
                    _instance.levels[index].Unlock ();
                }
            }
        }
    }
    #endregion
}

[Serializable]
public class SaveData_DBLevel {
    public int id;
    public bool isOpen;

    public SaveData_DBLevel () { }

    public SaveData_DBLevel (LevelStatus data) {
        this.id = data.GetID ();
        this.isOpen = data.IsOpen ();
    }
}