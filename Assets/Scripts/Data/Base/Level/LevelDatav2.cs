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
    public WaveEnemyData[] enemys;

    [Header("Note On Battle")]
    [SerializeField] int noteAmount;
    public int NoteAmount { get => noteAmount; }
    [SerializeField] ArrowType[] arrowVariation;
    public int[] NoteVariation
    {
        get
        {
            int[] data = new int[arrowVariation.Length];
            for (int i = 0; i < arrowVariation.Length; i++)
            {
                data[i] = (int)arrowVariation[i];
            }
            return data;
        }
    }

    public LevelWave(LevelWave newData)
    {
        this.enemys = newData.enemys;
        this.noteAmount = newData.noteAmount;
        this.arrowVariation = newData.arrowVariation;
    }
}
[System.Serializable]
public struct WaveEnemyData{
    public EnemyData data;
    public int slotPosition;
}
public enum ArrowType
{
    up = 0,
    down = 1,
    left = 2,
    right = 3
}
