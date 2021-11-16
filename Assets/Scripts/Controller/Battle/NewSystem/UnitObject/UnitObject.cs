using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FH_ActionModule;

namespace FH_BattleModule
{
    public class UnitObject : MonoBehaviour, IDamageAbleUnit
    {
        [SerializeField] TesterCombatParameter testerUnitParam;
        public UnitParameter unitParameter { private set; get; } = new UnitParameter();

        UnitActionManager actionManager;
        public UnitActionManager ActionManager
        {
            get
            {
                if (actionManager == null) actionManager = GetComponent<UnitActionManager>();
                return actionManager;
            }
        }

        public FH_UIControl.UIControl_UnitUI unitUI;

        [Space]
        [SerializeField] SpriteRenderer picture;
        public bool Initialized { private set; get; } = false;

        public bool isInvicible { private set; get; }

        public bool isDead
        {
            get
            {
                return unitParameter.finalParameter.Health <= 0;
            }
        }

        UnitData data;
        public void Initial(EnemyData enemyData)
        {
            data = new UnitData(enemyData);
            BaseIntial();
        }
        public void Initial(PlayerData playerData)
        {
            data = new UnitData(playerData);
            BaseIntial();
        }
        void BaseIntial()
        {
            SetPictureImage();
            unitParameter.originalParameter.SetAllParam(testerUnitParam.GetParameter());
            unitParameter.finalParameter.SetAllParam(testerUnitParam.GetParameter());
            Initialized = true;
        }
        public void DeIntitialize()
        {
            Initialized = false;
        }

        void SetPictureImage()
        {
            picture.sprite = data.unitSprite;
        }

        public virtual bool TakeDamage(int value)
        {
            int fixValue = value < 0 ? 0 : value;

            unitParameter.finalParameter.Modif_Health(-fixValue);
            unitUI.UpdateHealthBar(unitParameter.finalParameter.Health, unitParameter.originalParameter.Health);
            return true;
        }

        public void ChangeInvicibility(bool isInvicible)
        {
            this.isInvicible = isInvicible;
        }
    }
}
