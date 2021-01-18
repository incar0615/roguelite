using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Attack : IAttacker
        {
            private float atk = 0;
            public float Atk { get { return atk; } set { atk = value; } }

            public float GetAtk()
            {
                return Atk;
            }
        }

    }
}