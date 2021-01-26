using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public abstract class Robe : EquipmentItem
        {
            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                Debug.Log("Use Robe");
            }
        }

    }
}