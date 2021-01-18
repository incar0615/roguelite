using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Trap : BattleObject, IAttacker
        {
            public float GetAtk()
            {
                return Atk;
            }

            public void Attack(IDefender target)
            {
                // TODO
            }
        }
    }
}