using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class DashRobe : RobeBase
        {
            float speed = 11.0f;

            public DashRobe()
            {
                EquipPart = EquipPart.Robe;
            }

            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                Debug.Log("Use DashRobe, Dir = " + dir);
                MoveExecutor.Instance.Move(ownerObj, dir, speed, 0.25f, true);
                
            }
        }

    }
}