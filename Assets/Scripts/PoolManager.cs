using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public class PoolManager : MonoBehaviour
    {
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

        [SerializeField]
        private GameObject projectilePrefap = null;

        [SerializeField]
        private GameObject trailSpawnerPrefap = null;

        [SerializeField]
        private GameObject trailObjectPrefap = null;


        Queue<ProjectileBehaviour> projectileQue = new Queue<ProjectileBehaviour>();
        Queue<TrailSpawner> trailSpawnerQue = new Queue<TrailSpawner>();
        Queue<TrailObject> trailObjectQue = new Queue<TrailObject>();


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

            Initialize(100);
        }

        void Initialize(int initCnt)
        {
            // Projectile
            for (int i = 0; i < initCnt; i++)
            {
                var newProjectile = Instantiate(projectilePrefap).GetComponent<ProjectileBehaviour>();
                newProjectile.transform.SetParent(transform);
                newProjectile.gameObject.SetActive(false);

                projectileQue.Enqueue(newProjectile);
            }

            // TrailSpanwer
            for (int i = 0; i < initCnt; i++)
            {
                var newTrail = Instantiate(trailSpawnerPrefap).GetComponent<TrailSpawner>();
                newTrail.transform.SetParent(transform);
                newTrail.gameObject.SetActive(false);

                trailSpawnerQue.Enqueue(newTrail);
            }

            // TrailObject
            for (int i = 0; i < initCnt * 5; i++)
            {
                var newTrail = Instantiate(trailObjectPrefap).GetComponent<TrailObject>();
                newTrail.transform.SetParent(transform);
                newTrail.gameObject.SetActive(false);

                trailObjectQue.Enqueue(newTrail);
            }
        }

        // --------------------- projectilePrefap -------------------------------------------------------------------------
        ProjectileBehaviour CreateNewProjectileObj()
        {
            var newObj = Instantiate(projectilePrefap).GetComponent<ProjectileBehaviour>();
            newObj.transform.SetParent(transform);
            newObj.gameObject.SetActive(false);

            return newObj;
        }
        public static ProjectileBehaviour GetProjectileObject()
        {
            if (Instance.projectileQue.Count > 1)
            {
                var obj = Instance.projectileQue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);

                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewProjectileObj();
                newObj.transform.SetParent(null);
                newObj.gameObject.SetActive(true);

                return newObj;
            }
        }
        public static void ReturnObject(ProjectileBehaviour obj)
        {
            
            obj.gameObject.SetActive(false);

            obj.transform.SetParent(Instance.transform);

            Instance.projectileQue.Enqueue(obj);
        }

        // ---------------------- trailSpawnerPrefap --------------------------------------------------------------------
        TrailSpawner CreateNewTrailSpawner()
        {
            var newObj = Instantiate(projectilePrefap).GetComponent<TrailSpawner>();
            newObj.transform.SetParent(transform);
            newObj.gameObject.SetActive(false);

            return newObj;
        }
        public static TrailSpawner GetTrailSpawner()
        {
            if (Instance.trailSpawnerQue.Count > 1)
            {
                var obj = Instance.trailSpawnerQue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);

                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewTrailSpawner();
                newObj.transform.SetParent(null);
                newObj.gameObject.SetActive(true);

                return newObj;
            }
        }
        public static void ReturnObject(TrailSpawner obj)
        {
            obj.gameObject.SetActive(false);

            obj.transform.SetParent(Instance.transform);

            Instance.trailSpawnerQue.Enqueue(obj);
        }

        // ---------------------- trailObjectPrefap --------------------------------------------------------------------
        TrailObject CreateNewTrailObj()
        {
            var newObj = Instantiate(trailObjectPrefap).GetComponent<TrailObject>();
            newObj.transform.SetParent(transform);
            newObj.gameObject.SetActive(false);

            return newObj;
        }
        public static TrailObject GetTrailObject()
        {
            if (Instance.trailObjectQue.Count > 1)
            {
                var obj = Instance.trailObjectQue.Dequeue();
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);

                return obj;
            }
            else
            {
                var newObj = Instance.CreateNewTrailObj();
                newObj.transform.SetParent(null);
                newObj.gameObject.SetActive(true);

                return newObj;
            }
        }
        public static void ReturnObject(TrailObject obj)
        {
            obj.gameObject.SetActive(false);

            obj.transform.SetParent(Instance.transform);

            Instance.trailObjectQue.Enqueue(obj);
        }
    }
}