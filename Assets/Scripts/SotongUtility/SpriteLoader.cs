using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.U2D;

namespace SotongUtility
{
    public class SpriteLoader : MonoBehaviour
    {
        #region Initialization
        public static SpriteLoader Instance;
        private void Start()
        {
            if (Instance == null) { 
            Instance = this; 
            RegistedData();
            }
        }
        // Start is called before the first frame update
        #endregion
        [SerializeField] AtlasData[] spriteAtlases;
        Dictionary<string, AtlasData> allData_IdKey = new Dictionary<string, AtlasData>();

        void RegistedData(){
            foreach (var data in spriteAtlases)
            {
                allData_IdKey.Add(data.Id, data);
            }
        }

        public Sprite GetSprite(string id, string spriteCode){
            return allData_IdKey[id].GetSprite(spriteCode);
        }
        public Sprite[] GetSprites(string id, params string[] spriteCodes){
            return allData_IdKey[id].GetSprites(spriteCodes);
        }
        [System.Serializable]
        internal struct AtlasData{
            public string Id;
            //public string[] tags;

            public SpriteAtlas spriteAtlas;

            public Sprite GetSprite(string spriteCode){
                return spriteAtlas.GetSprite(spriteCode);
            }
            public Sprite[] GetSprites(params string[] spriteCodes){

                Sprite[] collectedSprite = new Sprite[spriteCodes.Length];
                for (int i = 0; i < spriteCodes.Length; i++)
                {
                    collectedSprite[i] = spriteAtlas.GetSprite(spriteCodes[i]);
                }
                return collectedSprite;
            }

        }
    }
}
