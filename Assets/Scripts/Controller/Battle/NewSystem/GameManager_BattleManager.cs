using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_BattleModule
{
    public class GameManager_BattleManager : MonoBehaviour
    {
        [Header("Wave Handler")]
        private LevelWave[] waves;
        public LevelWave currentWave { private set; get; }

        [Header("Loader Enemy")]

        int currentEnemyIndex =0;
        [SerializeField] List<EnemyObject> enemyPlaces;




        public void Initial(LevelWave[] waves)
        {
            this.waves = waves;
            SetWaves(0);
        }

        #region Waves
        public void SetWaves(int currentWaveNumber)
        {
            currentWave = new LevelWave(waves[currentWaveNumber]);
            currentEnemyIndex =0;

            SetEnemy();
        }
        #endregion

        #region EnemyObject
        public void SetEnemy()
        {
            int aliveEnemy = 0;
            List<EnemyObject> avaiablePlace = new List<EnemyObject>();

            foreach (var enemy in enemyPlaces)
            {
                if (enemy.Initialized) aliveEnemy++;
                else
                {
                    if(currentEnemyIndex > currentWave.enemys.Length) break;

                    avaiablePlace.Add(enemy);

                    int randomPlace = Random.Range(0, avaiablePlace.Count);
                    avaiablePlace[randomPlace].Initial(currentWave.enemys[currentEnemyIndex]);

                    currentEnemyIndex++;
                }
            }


            for (int i = 0; i < enemyPlaces.Count - aliveEnemy; i++)
            {
                
                //PoolObjectSystem.Instance.GetObject(PoolObject.PoolTag.Battle_Enemy).Intial(item);

            }
        }
        
        #endregion
    }
}
