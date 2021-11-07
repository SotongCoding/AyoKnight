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
                combatUI.SetTimerAmount(currentPickTime, pickArrowTime);

                if (currentPickTime <= 0)
                {
                    beginPickArrowTime = false;
                    comboHandler.SendCombo();
                    Debug.Log("Time Ups");
                    
                }
            }
        }

        #region Testing
        public LevelDatav2 levelData;
        #endregion

        #region  Wave Handler
        [Header("Wave Handler")]
        private LevelWave[] waves;
        public LevelWave currentWave { private set; get; }
        #endregion

        #region Object Loader
        [Header("Loader Enemy")]
        int currentEnemyIndex = 0;
        [SerializeField] List<EnemyObject> enemyPlaces;

        int currentPlayerIndex = 0;
        [SerializeField] List<PlayerObject> playerPlaces;
        #endregion

        #region UI Handler
        [Space]
        [SerializeField] FH_UIControl.UIControl_MainCombatUI combatUI;
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
            SetWaves(0);

        }
        #region Waves
        public void SetWaves(int currentWaveNumber)
        {
            currentWave = new LevelWave(waves[currentWaveNumber]);
            currentEnemyIndex = 0;

            SetEnemy();
        }
        #endregion

        #region Unit Object
        public void SetEnemy()
        {
            int aliveEnemy = 0;
            List<EnemyObject> avaiablePlace = new List<EnemyObject>();
            List<int> placedIndex = new List<int>();

            foreach (var enemy in enemyPlaces)
            {
                if (currentEnemyIndex > currentWave.enemys.Length) break;
                if (enemy.Initialized) aliveEnemy++;
                else
                {
                    avaiablePlace.Add(enemy);
                }

            }
            for (int i = 0; i < avaiablePlace.Count; i++)
            {
                placedIndex.Add(i);
            }


            for (int i = 0; i < enemyPlaces.Count - aliveEnemy; i++)
            {
                //Random
                // int randomPlace = Random.Range(0, placedIndex.Count);
                // avaiablePlace[placedIndex[randomPlace]].Initial(currentWave.enemys[currentEnemyIndex]);
                // placedIndex.RemoveAt(randomPlace);

                //OnOrder
                avaiablePlace[i].Initial(currentWave.enemys[currentEnemyIndex]);
                //placedIndex.RemoveAt(i);

                currentEnemyIndex++;

                //PoolObjectSystem.Instance.GetObject(PoolObject.PoolTag.Battle_Enemy).Intial(item);

            }
        }
        public void SetPlayer()
        {
            int alivePlayer = 0;
            List<PlayerObject> avaiablePlace = new List<PlayerObject>();
            List<int> placedIndex = new List<int>();

            foreach (var player in playerPlaces)
            {
                if (currentEnemyIndex > currentWave.enemys.Length) break;
                if (player.Initialized) alivePlayer++;
                else
                {
                    avaiablePlace.Add(player);
                }

            }
            for (int i = 0; i < avaiablePlace.Count; i++)
            {
                placedIndex.Add(i);
            }


            for (int i = 0; i < enemyPlaces.Count - alivePlayer; i++)
            {
                //Random
                // int randomPlace = Random.Range(0, placedIndex.Count);
                // avaiablePlace[placedIndex[randomPlace]].Initial(currentWave.enemys[currentEnemyIndex]);
                // placedIndex.RemoveAt(randomPlace);

                //OnOrder
                avaiablePlace[i].Initial(currentWave.enemys[currentPlayerIndex]);
                //placedIndex.RemoveAt(i);

                currentPlayerIndex++;

                //PoolObjectSystem.Instance.GetObject(PoolObject.PoolTag.Battle_Enemy).Intial(item);

            }
        }

        #endregion

        #region Combo
        public void SetCombo()
        {
            combatUI.SetArrow(comboHandler.SetCombo(currentWave.NoteVariation, currentWave.noteAmount));
        }
        #endregion

        #region Timer
        public void SetPickArrowTime()
        {
            pickArrowTime = currentPickTime = (currentWave.noteAmount * 2f) + 2;
            combatUI.SetTimerAmount(currentPickTime, pickArrowTime);
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
        public void NextSwitchUnit()
        {
            SetNextPlayer();
            SetNextEnemy();
        }
        public void SetUnitPriority(bool isPlayer)
        {
            playerPriority = isPlayer;
        }

        void SetNextPlayer()
        {

            actionPlayerIndex++;
            actionPlayerIndex = actionPlayerIndex >= playerPlaces.Count ? 0 : actionPlayerIndex;

            while (playerPlaces[actionPlayerIndex].combatData.isDead)
            {
                actionPlayerIndex++;
                actionPlayerIndex = actionPlayerIndex >= playerPlaces.Count ? 0 : actionPlayerIndex;
            }

            currentPlayerPlay = playerPlaces[actionPlayerIndex];
            UIHandler.CombatUI.Debug("Current PLAYER unit Play : " + currentPlayerPlay.name);
        }
        void SetNextEnemy()
        {
            actionEnemyIndex++;
            actionEnemyIndex = actionEnemyIndex >= enemyPlaces.Count ? 0 : actionEnemyIndex;

            while (enemyPlaces[actionEnemyIndex].combatData.isDead)
            {
                actionEnemyIndex++;
                actionEnemyIndex = actionEnemyIndex >= enemyPlaces.Count ? 0 : actionEnemyIndex;

            }
            currentEnemyPlay = enemyPlaces[actionEnemyIndex];
            UIHandler.CombatUI.Debug("Current ENEMY unit Play : " + currentEnemyPlay.name);
        }

        #endregion
    }
}
