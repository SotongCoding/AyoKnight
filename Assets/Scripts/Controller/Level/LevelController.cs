using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    [SerializeField] int killAmount;
    [SerializeField] int targetKill;
    int curentTier;
    List<EnemyData> enemys = new List<EnemyData> ();
    UIControl_Battle ui;
    LootDropControler dropControler;

    private void Awake () {
        dropControler = FindObjectOfType<LootDropControler> ();
        ui = FindObjectOfType<UIControl_Battle> ();
        InitialData (LevelLoader.getLevelData ());
    }
    private void Start () {
        BattleController._instance.OnEnemyDeath += IncreaseKillAmount;
        BattleController._instance.OnEnemyDeath += SetEnemy;
    }
    void InitialData (LevelData level) {
        targetKill = level.GetBattleRoomData ().killAmount;
        curentTier = level.GetBattleRoomData ().tierUse;
        enemys = level.GetBattleRoomData ().enemies;
        ui.ChangeAmout (targetKill);
        dropControler.Initial (level.GetBattleRoomData ().GetDropData ());
    }
    public void SetEnemy () {
        int ran = Random.Range (0, enemys.Count);
        BattleData_Enemy enemyData = new BattleData_Enemy (curentTier, enemys[ran]);
        BattleController.setEnemyData (enemyData);
        ui.NewTime ((enemys[ran].noteAmount * 0.4f) + 3);
        ui.SetEnemyData (enemyData);
    }
    void IncreaseKillAmount () {
        killAmount++;
        ui.ChangeAmout (targetKill - killAmount);
        if (killAmount == targetKill) { BattleController._instance.CallOnBattleDone (true); }
    }
}