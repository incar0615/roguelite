using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public abstract class AttackObject : PoolObject
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public abstract GameObjects.Attack GetAttackData();
    }
}
