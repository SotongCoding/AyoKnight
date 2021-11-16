using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitData
{
    public Sprite unitSprite;

    public UnitData(EnemyData enemyData)
    {
        unitSprite = enemyData.picture;
    }
    public UnitData (PlayerData playerData){
        unitSprite = playerData.picture;
    }
}
