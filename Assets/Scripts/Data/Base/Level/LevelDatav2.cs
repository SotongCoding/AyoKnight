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

    [Header("Note On Battle")]
    [SerializeField] int minArrow;
    [SerializeField] int maxArrow;
    public int noteAmount { get { return Random.Range(minArrow, maxArrow); } }
    [SerializeField] ArrowType[] arrowVariation;
    public int[] NoteVariation {get{
        int[] data = new int[arrowVariation.Length];
        for (int i = 0; i < arrowVariation.Length; i++)
        {
            data[i] = (int) arrowVariation[i];
        }
        return data;
    }
    }

    public LevelWave(LevelWave newData)
    {
        this.enemys = newData.enemys;
        this.minArrow = newData.minArrow;
        this.maxArrow = newData.maxArrow;
        this.arrowVariation = newData.arrowVariation;
    }
}
public enum ArrowType
{
    up = 0,
    down = 1,
    left = 2,
    right = 3
}
