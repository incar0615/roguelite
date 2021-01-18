using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using P1.GameObjects;

namespace P1
{
    public class BattleUtil
    {
        public static void DamageProcess(IAttacker attacker, IDefender defender)
        {
            float dmg = Damage.CalcDamage(attacker, defender);

            defender.Hitted(dmg);
        }

       
    }
}

