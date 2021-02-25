using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_MainMenu : MonoBehaviour {
    int selectedLevel = -1;
    int curChapter;
    int curDificult;

    [Header ("LevelAvaiable")]
    public List<Button> level1_5;

    private void Start () {
        UpdateLevelLoad (1, 1);
    }

    void UpdateLevelLoad (int chapter, int dificult) {
        for (int i = 1; i <= level1_5.Count; i++) {
            int value = (chapter * 100) + (i * 10) + dificult;
            level1_5[i - 1].onClick.RemoveAllListeners ();
            level1_5[i - 1].onClick.AddListener (delegate { SelectLevel (value); });
            level1_5[i - 1].interactable = DB_LevelData.GetLevel (value).IsOpen ();
        }
        curDificult = dificult;
        curChapter = chapter;
    }

    public void OpenChapter (int chapter) {
        curChapter = chapter;
        UpdateLevelLoad (curChapter, curDificult);
    }
    public void OpenDificult (int dificult) {
        curDificult = dificult;
        UpdateLevelLoad (curChapter, curDificult);
    }
    public void SelectLevel (int level) {
        selectedLevel = level;
    }
    public void LoadLevel () {

        int[] eqStat = FindObjectOfType<PlayerData_Battle> ().GetEqStatus ();
        if (eqStat[0] != -1 && eqStat[1] != -1) {
            if (selectedLevel != -1) {
                ResourcesUIControl.TurnUION(false);
                SceneLoader.LoadScene (3);
                LevelLoader.LoadLevel (selectedLevel);
            }
            else
                PopUpControler.CallPopUp (
                    "notice",
                    "Select A Level",
                    "Please select Avaiable level before battle Start",
                    "");
        }
        else PopUpControler.CallPopUp (
            "notice",
            "Check Equipment",
            "Please wear some equipment before battle, wear a WEAPON and ARMOR",
            "");
    }
    public void OpenInvetory () {
        SceneLoader.LoadScene (2);
        ResourcesUIControl.TurnUION(true);
    }
}