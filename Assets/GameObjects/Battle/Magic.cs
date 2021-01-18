using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Magic
        {
            // 접근한정자 나중에 수정
            public int id;
            public string name;
            public float projectileSpeed;
            public float maxTravelDist;
            public float atkDelay;
            public string element;
            public List<ProjectileModifiers> runes;

            // FIXME 
            public Magic(int id, string name, float projectileSpeed, 
                float maxTravelDist, float atkDelay, string element)
            {
                this.id = id;
                this.name = name;
                this.projectileSpeed = projectileSpeed;
                this.maxTravelDist = maxTravelDist;
                this.atkDelay = atkDelay;
                this.element = element;

                runes = new List<ProjectileModifiers>();
            }

            public void SetRune(List<ProjectileModifiers> _runes)
            {
                foreach(var rune in _runes)
                {
                    runes.Add(rune);
                }
            }
        }

    }
}