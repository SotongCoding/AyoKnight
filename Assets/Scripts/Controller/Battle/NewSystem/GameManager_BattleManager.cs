using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_BattleModule
{
    public class GameManager_BattleManager : MonoBehaviour
    {
        #region Singleton Intialization
        public static GameManager_BattleManager Instance;
        private void Start()
        {
            if (Instance == null) { Instance = this; }
            Initial(levelData.waves);
        }
        #endregion

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
        #endregion

        #region UI Handler
        [SerializeField] FH_UIControl.UIControl_MainCombatUI combatUI;
        #endregion

        #region  Combo Handler
        public ComboChecker comboHandler {get; private set;}
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

        #region EnemyObject
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

        #endregion

        #region Combo
        public void SetCombo(){
            combatUI.SetArrow(comboHandler.SetCombo(currentWave.NoteVariation, currentWave.noteAmount));
        }
        #endregion
    }
}
