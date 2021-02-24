using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl_Battle : MonoBehaviour {
    #region Time Control
    [Header ("Timer")]
    public float timeValue;
    float setTime = 5;
    public Image leftBar_time, rightBar_time;
    public Gradient gradient_time;
    #endregion

    #region Player Contorl
    [Header ("Player")]
    [SerializeField] BattleData_Player player;
    float healthValue_player;
    float setHealth_player;

    public Image leftBar_health, rightBar_health;
    public Gradient gradient_health;

    public Sprite[] submitImages;
    public Image submitPict;

    public Text attackValue;
    public Text defenseValue;

    #endregion
    #region Enemy Contorl
    [Header ("Enemy")]
    [SerializeField] BattleData_Enemy enemy;
    float healthValue_enemy;
    float setHealth_enemy;

    public Image leftBar_health_e, rightBar_health_e;
    public Gradient gradient_health_e;

    public Text attackValue_e;
    public Text noteValue;
    public Text enemyAmount;
    #endregion

    private void Awake () {
        BattleController._instance.OnGetPhase += SetSubmitPict;
        BattleController._instance.OnGetPhase += reTime;
    }
    private void Start () {
        timeValue = setTime;
    }

    private void Update () {
        //Change Value;
        //healthValue = Mathf.Clamp (healthValue -= Time.deltaTime, 0, setHealth);
        if (BattleController.isStartBattle && !BattleController.isBattlePause) {
            timeValue = Mathf.Clamp (timeValue -= Time.deltaTime, 0, setTime);

            //Timer
            leftBar_time.fillAmount = rightBar_time.fillAmount = timeValue / setTime;
            leftBar_time.color = rightBar_time.color = gradient_time.Evaluate (timeValue / setTime);
            if (timeValue <= 0) { FindObjectOfType<PlayerControl> ().Submit (true); }

            //health
            //Player
            healthValue_player = player.heatlth_fix;
            leftBar_health.fillAmount = rightBar_health.fillAmount = healthValue_player / setHealth_player;
            leftBar_health.color = rightBar_health.color = gradient_health.Evaluate (healthValue_player / setHealth_player);

            //Enemy
            healthValue_enemy = enemy.health_fix;
            leftBar_health_e.fillAmount = rightBar_health_e.fillAmount = healthValue_enemy / setHealth_enemy;
            leftBar_health_e.color = rightBar_health_e.color = gradient_health_e.Evaluate (healthValue_enemy / setHealth_enemy);

        }
    }

    void SetSubmitPict (Phase phase) {
        submitPict.sprite = phase == Phase.player ? submitImages[1] : submitImages[0];
    }
    public void SetPlayerData (BattleData_Player player) {
        this.player = player;
        setHealth_player = player.heatlth_fix;
        healthValue_player = setHealth_player;

        UpdateAtkDef_player ();
    }
    private void UpdateAtkDef_player () {
        attackValue.text = player.attack_fix.ToString ();
        defenseValue.text = player.defense_fix.ToString ();

    }

    public void SetEnemyData (BattleData_Enemy enemy) {
        this.enemy = enemy;
        setHealth_enemy = enemy.health_fix;
        healthValue_enemy = setHealth_enemy;

        UpdateAtkDef_enemy ();
    }
    private void UpdateAtkDef_enemy () {
        attackValue_e.text = enemy.attack_fix.ToString ();
        noteValue.text = enemy.noteAmount.ToString ();
    }

    public void NewTime (float time) {
        setTime = time;
        timeValue = setTime;
    }
    void reTime (Phase pase) {
        timeValue = setTime;
    }

    public void ChangeAmout (int amout) {
        enemyAmount.text = amout.ToString();
    }
}