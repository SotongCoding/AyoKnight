using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleController : MonoBehaviour {
    public event Action OnEnemyDeath;
    public event Action OnBattleStart;

    public event Action<Phase> OnGetPhase;

    public event Action<bool> onBattleDone;
    public event Action<bool> OnGetDamage;

    public static BattleController _instance;
    public static bool isStartBattle;
    public static bool isBattlePause;

    [SerializeField] BattleData_Enemy enemy;
    [SerializeField] BattleData_Player player;

    NoteControl noteControl;
    UIControl_Battle uiControl;

    Phase curentPhase;
    [SerializeField] List<int> turnOrder = new List<int> ();
    int turnAdded = 0;
    int curTurnAdded = 0;

    private void Awake () {
        if (_instance == null) {
            _instance = this;
        }
        else if (_instance != this) {
            Destroy (gameObject);
        }

        noteControl = FindObjectOfType<NoteControl> ();
        uiControl = FindObjectOfType<UIControl_Battle> ();

        setPlayerData (FindObjectOfType<PlayerData_Battle> ().GetPlayerData ());
        FindObjectOfType<LevelController> ().SetEnemy ();

        OnBattleStart += SetNewTurn;
        OnBattleStart += CallOnGetPhase;
        OnBattleStart += BattleStart;

        onBattleDone += BattleEnd;
        OnEnemyDeath += SetNewTurn;

    }
    private void Start () {
        isStartBattle = false;
        isBattlePause = true;
        Invoke ("CallOnBattleStart", 3);
    }
    private void OnDestroy () {
        OnGetPhase = null;
        OnBattleStart = OnEnemyDeath = null;
        OnGetDamage = onBattleDone = null;
    }

    public void CallOnGetPhase () {
        if (OnGetPhase != null) {
            OnGetPhase (getPhase ());
        }
    }
    public void CallOnEnemyDeath () {
        if (OnEnemyDeath != null) {
            OnEnemyDeath ();
        }
    }
    public void CallOnBattleDone (bool isWin) {
        if (onBattleDone != null) onBattleDone (isWin);
    }
    public void CallOnBattleStart () {
        if (OnBattleStart != null) OnBattleStart ();
    }
    public void CallOnGetDamage (bool isPlayer) {
        if (OnGetDamage != null) {
            OnGetDamage (isPlayer);

            uiControl.UpdateStatUI ();
        }
    }
    #region Turn Order

    //0 = player, 1 = enemy
    void DecideTurn () {
        if (curTurnAdded == 0) {
            turnOrder.Add (curTurnAdded);
            turnAdded += 1;
            if (turnAdded >= player.turnAmount) {
                turnAdded = 0;
                curTurnAdded = 1;
            }
        }
        else if (curTurnAdded == 1) {
            turnOrder.Add (curTurnAdded);
            turnAdded += 1;
            if (turnAdded >= enemy.turnAmount) {
                turnAdded = 0;
                curTurnAdded = 0;
            }
        }
    }
    void setTurn () {
        if (turnOrder.Count < 5) {
            DecideTurn ();
        }
    }
    Phase getPhase () {
        int result = _instance.turnOrder[0];

        curentPhase = (Phase) result;
        turnOrder.RemoveAt (0);
        setTurn ();

        return (Phase) result;
    }

    void SetNewTurn () {
        turnOrder.Clear ();
        curTurnAdded = 0;
        for (int i = 0; i < 5; i++) {
            setTurn ();
        }
    }
    #endregion

    public static BattleData_Player getPlayerData () {
        return _instance.player;
    }
    public static BattleData_Enemy getEnemyData () {
        return _instance.enemy;
    }

    public static void setEnemyData (BattleData_Enemy data) {
        _instance.enemy = data;
        _instance.noteControl.setNoteAmout (data.noteAmount);
    }
    void setPlayerData (BattleData_Player data) {
        _instance.player = data;
        _instance.noteControl.setNoteCode (data.activeNote_weap, data.activeNote_armor);
    }

    void BattleStart () {
        isStartBattle = true;
        isBattlePause = false;
        uiControl.SetPlayerData (player);
        // uiControl.SetEnemyData (enemy);
    }
    void BattleEnd (bool isWin) {
        isStartBattle = false;
        if (isWin) {

            PopUpControler.CallPopUp ("winlose", "WIN", "", "");

            foreach (int levelID in LevelLoader.getLevelData ().unlockedlevelID) {
                DB_LevelData.GetLevel (levelID).Unlock ();
            }

        }
        else {
            PopUpControler.CallPopUp ("winlose", "LOSE", "", "");
        }

        FindObjectOfType<PlayerData_Battle> ().ReduceItemDurability (isWin);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#elif UNITY_ANDROID

#endif
    }
}