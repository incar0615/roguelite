using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        /// <summary>
        /// AttackObject 하위에 Child 로 들어가는 오브젝트 인터페이스
        /// </summary>
        public interface IAttackModifierObj
        {
            void ReturnObject();

        }
    }

}