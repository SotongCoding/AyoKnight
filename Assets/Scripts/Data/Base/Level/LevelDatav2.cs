using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data", fileName = "LD_")]
public class LevelDatav2 : ScriptableObject
{
    public string levelId;

    public string levelName;
    [TextArea]
    public string LevelDescription;
    public LevelWave[] waves;



}
[System.Serializable]
public struct LevelWave
{
    [Header("Enemys")]
    public EnemyData[] enemys;
    public int maxEnemyOnStage;

    [Header("Note On Battle")]
    [SerializeField] int minArrow;
    [SerializeField] int maxArrow;
    public int noteAmount { get { return Random.Range(minArrow, maxArrow); } }
    public NoteType[] noteVariation;

    public LevelWave(LevelWave newData)
    {
        this.enemys = newData.enemys;
        this.minArrow = newData.minArrow;
        this.maxArrow = newData.maxArrow;
        this.noteVariation = newData.noteVariation;
        this.maxEnemyOnStage = newData.maxEnemyOnStage;

    }
}
public enum NoteType
{
    up = 1,
    down = 2,
    left = 3,
    right = 4
}
