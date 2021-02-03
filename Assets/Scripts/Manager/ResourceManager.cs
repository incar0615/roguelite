using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    // TODO 나중에 에셋번들 사용하는쪽으로 변경해야 좋을듯 
    public class ResourceManager : MonoBehaviour
    {
        #region singleton
        private static ResourceManager instance;
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<ResourceManager>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        var newSingleton = new GameObject("ResourceManager").AddComponent<ResourceManager>();
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
    
        public T LoadResourceByPath<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}