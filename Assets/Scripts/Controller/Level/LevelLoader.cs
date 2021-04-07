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
    [SerializeField] LevelData selectedLevel;
    int curentLevel;

    public static void LoadLevel (int level) {
        _instance.selectedLevel = DB_LevelData.GetLevel (level).levelData;
        _instance.curentLevel = level;
    }
    public static void ReLoadLevel (out bool canReload) {
        int[] eqStat = FindObjectOfType<PlayerData_Battle> ().GetEqStatus ();

        if (eqStat[0] != -1 && eqStat[1] != -1) {
            SceneLoader.UnloadScene (3);
            SceneLoader.LoadScene (3);
            LoadLevel (_instance.curentLevel);
            canReload = true;

        }
        else {
            PopUpControler.CallPopUp (
                "notice",
                "Check Equipment",
                "Some your equipment has destroy, Please check your Equipment again",
                "");
            canReload = false;
        }
    }

    public static LevelData getLevelData () {
        return _instance.selectedLevel;
    }
}