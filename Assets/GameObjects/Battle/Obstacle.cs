using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Obstacle : BattleObject, IDefender
        {
            public float GetDef()
            {
                return Def;
            }

            public void Hitted(float damage)
            {
                CurHp -= damage;
                Debug.Log(CurHp);
            }

            public void Die()
            {

            }
        }
    }
}