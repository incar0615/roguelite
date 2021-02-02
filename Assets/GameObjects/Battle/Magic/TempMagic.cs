using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class TempMagic : Magic
        {
            int cnt = 0;

            public TempMagic()
            {

            }

            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                var bullet = PoolManager.GetProjectileObject();

                bullet.GetAttackData().Atk = 4; // FIXME. 임시코드
                bullet.MaxTravelDist = 10.0f;
                bullet.ProjectileSpeed = 3.0f;
                
                bullet.transform.position = ownerObj.transform.position + dir.normalized * 0.1f;
                bullet.Shoot(dir.normalized);
            }
        }

    }
}