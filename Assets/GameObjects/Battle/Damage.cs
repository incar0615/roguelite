using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Damage
        {
            public static float CalcDamage(IAttacker attacker, IDefender defender)
            {
                return attacker.GetAtk() - defender.GetDef();
            }
        }
    }
    
}
