using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SotongUtility;

namespace FH_UIControl
{
    public class CombatUI_Arrow : MonoBehaviour, PoolObjectSystem.IPoolObject
    {
        #region Pool Var
        public PoolObjectSystem.PoolTag poolTag => PoolObjectSystem.PoolTag.UIArrow;
        public GameObject thisObject => this.gameObject;
        #endregion
        const string UISpriteId_Arrow = "arrow";
        const string CombatAtlasCode = "atlasCombat";

        private void OnDisable() {
            picture.color = Color.white;
        }

        [SerializeField] Image picture;
        public void Initial(int arrowCode)
        {
            picture.sprite = SpriteLoader.Instance.GetSprite(CombatAtlasCode, UISpriteId_Arrow + arrowCode);
        }
        public void SetCorrect(bool isActive){
            picture.color = isActive? Color.green : Color.red;
        }
    }
}
