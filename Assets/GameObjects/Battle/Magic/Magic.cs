using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public abstract class Magic
        {
            // 접근한정자 나중에 수정
            public int id;
            public string name;
            public float projectileSpeed;
            public float maxTravelDist;
            public float atkDelay;
            public string element;
            public List<ProjectileModifiers> runes;

            public abstract void Use(GameObject ownerObj, Vector3 dir);
        }

    }
}