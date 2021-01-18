using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public class EnemySpawner : MonoBehaviour
    {
        private static EnemySpawner instance = null;
        public static EnemySpawner Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<EnemySpawner>();
                    if (!instance)
                    {
                        instance = new GameObject("EnemySpawner").AddComponent<EnemySpawner>();
                    }
                }
                return instance;
            }
        }

        public void SpawnEnemy()
        {
            // enemy.EnemyData = Resources.Load("GameData/" + name, typeof(ScriptableObject)) as EnemyCharacterData;
        }
    }
}