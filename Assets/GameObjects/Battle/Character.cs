using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Character : BattleObject, IAttacker, IDefender
        {
            public float GetAtk()
            {
                return Atk;
            }

            public float GetDef()
            {
                return Def;
            }

            public void Attack(IDefender target)
            {
                // TODO
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