using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SotongUtility
{
    public class PoolObjectSystem : MonoBehaviour
    {
        #region InspectorField
        [SerializeField] PoolRegisterData[] registerData;
        #endregion
        #region Initialization
        public static PoolObjectSystem Instance;
        private void Start()
        {
            if (Instance == null) { Instance = this; }
            RegisterPoolPerfab();
        }
        // Start is called before the first frame update
        #endregion
        Dictionary<PoolTag, IPoolObject> registeredPool = new Dictionary<PoolTag, IPoolObject>();
        public void RegisterPoolPerfab()
        {
            foreach (var item in registerData)
            {
                if (!registeredPool.ContainsKey(item.tag))
                {
                    registeredPool.Add(item.tag, item.poolBaseObject.GetComponent<IPoolObject>());
                }
            }

        }
        Dictionary<PoolTag, Queue<GameObject>> allData = new Dictionary<PoolTag, Queue<GameObject>>();

        public GameObject RequestObject(PoolTag tag)
        {
            if (!allData.ContainsKey(tag))
            {

                GameObject newObj = Instantiate(registeredPool[tag].thisObject);
                Queue<GameObject> newQueue = new Queue<GameObject>();
                newQueue.Enqueue(newObj);

                allData.Add(tag, newQueue);
            }
            if (allData[tag].Count <= 0)
            {
                GameObject newObj = Instantiate(registeredPool[tag].thisObject);
                allData[tag].Enqueue(newObj);
            }

            return allData[tag].Dequeue();
        }

        public void StoreObject(IPoolObject target)
        {
            allData[target.poolTag].Enqueue(target.thisObject);
        }

        #region Model Data
        public enum PoolTag
        {
            UIArrow,
        }

        [System.Serializable]
        struct PoolRegisterData
        {
            public PoolTag tag;
            public GameObject poolBaseObject;
        }
        public interface IPoolObject
        {
            PoolTag poolTag { get; }
            GameObject thisObject { get; }
        }
        #endregion
    }
}
