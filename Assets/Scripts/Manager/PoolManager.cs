using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    /// <summary>
    /// 기존 오브젝트 풀에서 확장성을 위해 
    /// easy Object Pooling 참고해서 변경 
    /// </summary>
    /// 

    [System.Serializable]
    public class PoolInfo
    {
        public string poolName;
        public GameObject prefab;
        public int poolSize;
        public bool fixedSize;
        
        public PoolInfo(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize)
        {
            this.poolName = poolName;
            this.prefab = poolObjectPrefab;
            this.poolSize = initialCount;
            this.fixedSize = fixedSize;
        }
    }

    class Pool
    {
        private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();

        PoolInfo poolInfo;

        public Pool(string poolName, GameObject poolObjectPrefab, int initialCount, bool fixedSize)
        {

            poolInfo = new PoolInfo(poolName, poolObjectPrefab, initialCount, fixedSize);
            //populate the pool
            for (int index = 0; index < initialCount; index++)
            {
                AddObjectToPool(NewObjectInstance());
            }
        }

        private void AddObjectToPool(PoolObject po)
        {
            //add to pool
            po.gameObject.SetActive(false);
            availableObjStack.Push(po);
            po.isPooled = true;
        }

        private PoolObject NewObjectInstance()
        {
            GameObject go = (GameObject)GameObject.Instantiate(poolInfo.prefab);
            PoolObject po = go.GetComponent<PoolObject>();
            if (po == null)
            {
                po = go.AddComponent<PoolObject>();
            }
            //set name
            po.poolName = poolInfo.poolName;
            return po;
        }

        public GameObject NextAvailableObject(Vector3 position, Quaternion rotation)
        {
            PoolObject po = null;
            if (availableObjStack.Count > 0)
            {
                po = availableObjStack.Pop();
            }
            else if (poolInfo.fixedSize == false)
            {
                //increment size var, this is for info purpose only
                poolInfo.poolSize++;
                Debug.Log(string.Format("Growing pool {0}. New size: {1}", poolInfo.poolName, poolInfo.poolSize));
                //create new object
                po = NewObjectInstance();
            }
            else
            {
                Debug.LogWarning("No object available & cannot grow pool: " + poolInfo.poolName);
            }

            GameObject result = null;
            if (po != null)
            {
                po.isPooled = false;
                result = po.gameObject;
                result.SetActive(true);

                result.transform.position = position;
                result.transform.rotation = rotation;
            }

            return result;
        }

        public void ReturnObjectToPool(PoolObject po)
        {

            if (poolInfo.poolName.Equals(po.poolName))
            {
                if (!po.isPooled)
                {
                    Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");
                }
                else
                {
                    AddObjectToPool(po);
                }

            }
            else
            {
                Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.poolName, poolInfo.poolName));
            }
        }
    }
    public class PoolManager : MonoBehaviour
    {
        #region singleton
        private static PoolManager instance;
        public static PoolManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<PoolManager>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        var newSingleton = new GameObject("PoolManager").AddComponent<PoolManager>();
                        instance = newSingleton;
                    }
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        #endregion

        public PoolInfo[] poolInfo;
        private Dictionary<string, Pool> poolDictionary = new Dictionary<string, Pool>();

        private void Awake()
        {
            var objs = FindObjectsOfType<PoolManager>();
            if (objs.Length != 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Instance = this;

            CreatePools();
        }

        private void CreatePools()
        {
            foreach (PoolInfo currentPoolInfo in poolInfo)
            {

                Pool pool = new Pool(currentPoolInfo.poolName, currentPoolInfo.prefab,
                                     currentPoolInfo.poolSize, currentPoolInfo.fixedSize);


                Debug.Log("Creating pool: " + currentPoolInfo.poolName);
                //add to mapping dict
                poolDictionary[currentPoolInfo.poolName] = pool;
            }
        }

        public GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
        {
            GameObject result = null;

            if (poolDictionary.ContainsKey(poolName))
            {
                Pool pool = poolDictionary[poolName];
                result = pool.NextAvailableObject(position, rotation);
                //scenario when no available object is found in pool
                if (result == null)
                {
                    Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
                }

            }
            else
            {
                Debug.LogError("Invalid pool name specified: " + poolName);
            }

            return result;
        }

        public void ReturnObjectToPool(GameObject go)
        {
            PoolObject po = go.GetComponent<PoolObject>();
            
            ReturnObjectToPool(po);
        }

        public void ReturnObjectToPool(PoolObject po)
        {
            
            if (po != null)
            {
                if (poolDictionary.ContainsKey(po.poolName))
                {
                    Pool pool = poolDictionary[po.poolName];
                    pool.ReturnObjectToPool(po);
                }
                else
                {
                    Debug.LogWarning("No pool available with name: " + po.poolName);
                }
            }
        }
    }
}