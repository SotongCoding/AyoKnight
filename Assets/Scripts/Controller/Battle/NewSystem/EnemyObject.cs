using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FH_BattleModule{
public class EnemyObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer picture;
    public bool Initialized{private set; get;}
    EnemyData data;
    public void Initial(EnemyData enemData){
        data = enemData;
        SetPictureImage();

        Initialized = true;
    }
    public void DeIntitialize(){
        Initialized = false;
    }

    void SetPictureImage(){
        picture.sprite = data.picture;
    }

}
}
