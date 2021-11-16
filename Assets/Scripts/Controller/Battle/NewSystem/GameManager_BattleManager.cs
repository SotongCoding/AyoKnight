using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_PlayerControl;

namespace FH_BattleModule
{
    public class GameManager_BattleManager : MonoBehaviour
    {
        #region Singleton Intialization
        public static GameManager_BattleManager Instance;

        #endregion
        private void Start()
        {
            if (Instance == null) { Instance = this; }
            Initial(levelData.waves);
        }
        private void Update()
        {
            if (beginPickArrowTime)
            {
                currentPickTime -= Time.deltaTime;
                UIHandler.CombatUI.SetTimerAmount(currentPickTime, pickArrowTime);

                if (currentPickTime <= 0)
                {
                    beginPickArrowTime = false;
                    comboHandler.SendCombo();
                    UIHandler.CombatUI.ShowPayerControlUI(false);
                    Debug.Log("Time Ups");

                }
            }
        }

        #region Testing
        public LevelDatav2 levelData;
        public List<PlayerData> playerDatas;
        #endregion

        #region  Wave Handler
        [Header("Wave Handler")]
        private LevelWave[] waves;
        public LevelWave currentWave { private set; get; }
        #endregion

        #region Object Loader
        [Header("Loader Enemy")]
        [SerializeField] List<EnemyObject> enemyPlaces;
        [SerializeField] List<PlayerObject> playerPlaces;
        #endregion

        #region  Combo Handler
        public ComboChecker comboHandler { get; private set; }
        #endregion

        #region Timer Handler
        float pickArrowTime = 0;
        float currentPickTime = 0;
        bool beginPickArrowTime = false;

        public float remainingTime { get => (currentPickTime / pickArrowTime); }
        #endregion

        #region  UnitAction Handler
        public PlayerObject currentPlayerPlay { private set; get; }
        public EnemyObject currentEnemyPlay { private set; get; }

        public bool playerPriority { private set; get; } = false;
        int actionPlayerIndex = -1;
        int actionEnemyIndex = -1;

        #endregion
        public void Initial(LevelWave[] waves)
        {
            comboHandler = new ComboChecker();
            this.waves = waves;
            SetPlayer();
            SetWaves(0);
        }

        #region Waves
        public void SetWaves(int currentWaveNumber)
        {
            currentWave = new LevelWave(waves[currentWaveNumber]);
            SetEnemy();
        }
        #endregion

        #region Unit Object
        public void SetEnemy()
        {
            foreach (var enemy in currentWave.enemys)
            {
                int slotCheck = 0;
                while (enemyPlaces[enemy.slotPosition + slotCheck].Initialized)
                {
                    slotCheck++;
                }

                enemyPlaces[enemy.slotPosition + slotCheck].Initial(enemy.data);
            }

            foreach (var item in enemyPlaces)
            {
                item.gameObject.SetActive(item.Initialized);
            }
        }
        public void SetPlayer()
        {
            foreach (var player in playerDatas)
            {
                int slotCheck = 0;
                while (playerPlaces[player.slotPosition + slotCheck].Initialized)
                {
                    slotCheck++;
                }

                playerPlaces[player.slotPosition + slotCheck].Initial(player);
            }

            foreach (var item in playerPlaces)
            {
                item.gameObject.SetActive(item.Initialized);
            }
        }

        #endregion

        #region Combo
        public void SetCombo()
        {
            UIHandler.CombatUI.ShowPayerControlUI(true);
            UIHandler.CombatUI.SetArrow(comboHandler.SetCombo(currentWave.NoteVariation, currentWave.NoteAmount));
        }
        #endregion

        #region Timer
        public void SetPickArrowTime()
        {
            pickArrowTime = currentPickTime = (currentWave.NoteAmount * 2f);
            UIHandler.CombatUI.SetTimerAmount(currentPickTime, pickArrowTime);
        }
        public void BeginPickArrowTime()
        {
            beginPickArrowTime = true;
        }
        public void EndPickArrowTime()
        {
            beginPickArrowTime = false;
            currentPickTime = pickArrowTime = 0;
        }
        #endregion

        #region  Unit Action
        public bool NextSwitchUnit()
        {
            bool setPlayer = false, setEnemy = false;
            
            currentEnemyPlay?.unitUI.TurnPriorityUI(false);
            currentPlayerPlay?.unitUI.TurnPriorityUI(false);

            setPlayer = SetNextPlayer();
            setEnemy = SetNextEnemy();

            return setPlayer && setEnemy;
        }
        public void SetUnitPriority(bool isPlayer)
        {

            playerPriority = isPlayer;

            currentPlayerPlay.unitUI.UpdatePriorityIcon(isPlayer);
            currentEnemyPlay.unitUI.UpdatePriorityIcon(!isPlayer);
        }

        bool SetNextPlayer()
        {
            bool doneSet = false;
            actionPlayerIndex++;
            actionPlayerIndex = actionPlayerIndex >= playerPlaces.Count ? 0 : actionPlayerIndex;

            while (playerPlaces[actionPlayerIndex].isDead || !playerPlaces[actionPlayerIndex].Initialized)
            {
                actionPlayerIndex++;
                actionPlayerIndex = actionPlayerIndex >= playerPlaces.Count ? 0 : actionPlayerIndex;
            }
            doneSet = true;
            currentPlayerPlay = playerPlaces[actionPlayerIndex];
            Debug.Log("Current PLAYER unit Play : " + currentPlayerPlay.name);

            return doneSet;
        }
        bool SetNextEnemy()
        {
            bool doneSet = false;
            actionEnemyIndex++;
            actionEnemyIndex = actionEnemyIndex >= enemyPlaces.Count ? 0 : actionEnemyIndex;

            while (enemyPlaces[actionEnemyIndex].isDead || !enemyPlaces[actionEnemyIndex].Initialized)
            {
                actionEnemyIndex++;
                actionEnemyIndex = actionEnemyIndex >= enemyPlaces.Count ? 0 : actionEnemyIndex;

            }
            doneSet = true;
            currentEnemyPlay = enemyPlaces[actionEnemyIndex];
            Debug.Log("Current ENEMY unit Play : " + currentEnemyPlay.name);

            return doneSet;
        }

        #endregion
    }
}
