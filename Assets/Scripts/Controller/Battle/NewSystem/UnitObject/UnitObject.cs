using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_ActionModule;

namespace FH_BattleModule
{
    public class UnitObject : MonoBehaviour
    {
        public UnitCombat_Data combatData;
        public UnitActionManager actionManager;

        [Space]
        [SerializeField] SpriteRenderer picture;
        public bool Initialized { private set; get; }
        EnemyData data;
        public void Initial(EnemyData enemData)
        {
            data = enemData;
            SetPictureImage();

            Initialized = true;
        }
        public void DeIntitialize()
        {
            Initialized = false;
        }

        void SetPictureImage()
        {
            picture.sprite = data.picture;
        }

    }
}
