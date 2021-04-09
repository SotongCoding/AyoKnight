using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStatus {
    [SerializeField] int levelID;
    public LevelData levelData;
    [SerializeField] bool isOpen;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dificult"> 0 : Normal 1 : Hard</param>
    /// <returns></returns>
    public bool IsOpen () { return isOpen; }
    public int GetID () { return levelID; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dificult"> 0 : Normal 1 : Hard</param>
    /// <returns></returns>
    public void Unlock () {
        isOpen = true;
    }
}