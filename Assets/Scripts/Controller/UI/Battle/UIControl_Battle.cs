using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    public Text healthValue;
    public Text enemyAmount;

    //Enemy Pict
    public UI_EnemyPictControl enemyPictTemplate;
    public Transform enemyPictPlace;
    #endregion
    #region DMGFX
    [Header ("DMG FX")]
    public Image playerDamageFX;
    public Image enemyDamageFX;
    #endregion
    private void Awake () {
        BattleController._instance.OnGetPhase += SetSubmitPict;
        BattleController._instance.OnGetPhase += reTime;
        BattleController._instance.OnGetDamage += ShowDamagedFX;
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

        }
    }

    void SetSubmitPict (Phase phase) {
        submitPict.sprite = phase == Phase.player ? submitImages[1] : submitImages[0];
    }
    public void SetPlayerData (BattleData_Player player) {
        this.player = player;
        setHealth_player = player.heatlth_fix;
        healthValue_player = setHealth_player;

        UpdateStat_player ();
    }
    private void UpdateStat_player () {
        attackValue.text = player.attack_fix.ToString ();
        defenseValue.text = player.defense_fix.ToString ();
        healthValue_player = player.heatlth_fix;

        leftBar_health.fillAmount = rightBar_health.fillAmount = (healthValue_player / setHealth_player);
        leftBar_health.color = rightBar_health.color = gradient_health.Evaluate ((healthValue_player / setHealth_player));

    }

    public void SetEnemyData (BattleData_Enemy enemy) {
        this.enemy = enemy;
        setHealth_enemy = enemy.health_fix;
        healthValue_enemy = setHealth_enemy;

        //Set pict of enemy
        if (enemyPictPlace.transform.GetChild (0).GetComponent<UI_EnemyPictControl> () != null) {
            Destroy (enemyPictPlace.transform.GetChild (0).gameObject);
        }
        UI_EnemyPictControl pict = Instantiate (enemyPictTemplate, enemyPictPlace);
        pict.SetPict (enemy.enemyPict);
        pict.transform.SetAsFirstSibling ();

        UpdateStat_enemy ();
    }
    private void UpdateStat_enemy () {
        attackValue_e.text = enemy.attack_fix.ToString ();
        noteValue.text = enemy.noteAmount.ToString ();
        healthValue.text = enemy.health_fix.ToString ();

        healthValue_enemy = enemy.health_fix;
        leftBar_health_e.fillAmount = rightBar_health_e.fillAmount = (healthValue_enemy / setHealth_enemy);
        leftBar_health_e.color = rightBar_health_e.color = gradient_health_e.Evaluate ((healthValue_enemy / setHealth_enemy));
    }

    void ShowDamagedFX (bool isPlayer) {
        if (isPlayer) {
            playerDamageFX.DOFade (1, 0.1f).OnComplete (
                delegate { playerDamageFX.DOFade (0, 0.1f); });
        }
        else {
            enemyDamageFX.DOFade (1, 0.1f).OnComplete (
                delegate { enemyDamageFX.DOFade (0, 0.1f); });
        }
    }
    public void UpdateStatUI () {
        UpdateStat_enemy ();
        UpdateStat_player ();
    }
    public void NewTime (float time) {
        setTime = time;
        timeValue = setTime;
    }
    void reTime (Phase pase) {
        timeValue = setTime;
    }

    public void ChangeAmout (int amout) {
        enemyAmount.text = amout.ToString ();
    }
}