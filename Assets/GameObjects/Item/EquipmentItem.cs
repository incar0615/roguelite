using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public abstract class EquipmentItem : ItemBase
        {
            private EquipPart equipPart;
            public EquipPart EquipPart { get { return equipPart; } set { equipPart = value; } }

            public virtual void Use(GameObject ownerObj, Vector3 dir)
            {

            }
        }
    }
}