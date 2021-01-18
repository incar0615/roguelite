using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public interface IDefender
        {
            float GetDef();

            void Hitted(float damage);
            void Die();
        }
    }
}