using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace FH_BattleModule
{
    [System.Serializable]
    public struct TesterCombatParameter
    {
        public int attack;
        public int defense;
        public int health;
        public int energy;

        public CombatParamater GetParameter()
        {
            return new CombatParamater(attack, defense, health, energy);
        }

    }
    [System.Serializable]
    public class UnitParameter
    {
        public CombatParamater originalParameter { private set; get; } = new CombatParamater();
        public CombatParamater finalParameter { private set; get; } = new CombatParamater();

    }

    [System.Serializable]
    public class CombatParamater : IUnitBasicParameter
    {
        public CombatParamater()
        {

        }
        public CombatParamater(CombatParamater newParameter)
        {
            Attack = newParameter.Attack;
            Defense = newParameter.Defense;
            Health = newParameter.Health;
            Energy = newParameter.Energy;
        }

        public CombatParamater(int attack, int defense, int health, int energy)
        {
            Attack = attack;
            Defense = defense;
            Health = health;
            Energy = energy;
        }
        #region  Basic Param

        public int Attack { private set; get; }
        public int Defense { private set; get; }
        public int Health { private set; get; }
        public int Energy { private set; get; }

        public int Modif_Attack(int addingValue)
        {
            Attack += addingValue;
            return Attack;
        }

        public int Modif_Defense(int addingValue)
        {
            Defense += addingValue;
            return Defense;
        }

        public int Modif_Health(int addingValue)
        {
            Debug.Log("Health : " + Health);
            Health += addingValue;
            Debug.Log("Health New : " + Health);
            return Health;
        }

        public int Modif_Energy(int addingValue)
        {
            Energy += addingValue;
            return Energy;
        }

        public void SetAllParam(CombatParamater newValue)
        {
            this.Attack = newValue.Attack;
            this.Defense = newValue.Defense;
            this.Health = newValue.Health;
            this.Energy = newValue.Energy;
        }
        #endregion
    }

    #region  Interface Basic Parameter
    public interface IUnitBasicParameter
    {
        int Attack { get; }
        int Defense { get; }
        int Health { get; }
        int Energy { get; }

        int Modif_Attack(int addingValue);
        int Modif_Defense(int addingValue);
        int Modif_Health(int addingValue);
        int Modif_Energy(int addingValue);
    }
    #endregion

    #region  Interface Unit Status
    public interface IDamageAbleUnit
    {
        bool isDead { get; }
        bool isInvicible { get; }
        bool TakeDamage(int value);
        void ChangeInvicibility(bool isInvicible);
    }
    #endregion
}
