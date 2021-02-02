using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        // 로브 공통
        public abstract class RobeBase : EquipmentItem
        {
            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                Debug.Log("Use Robe");
            }
        }

    }
}