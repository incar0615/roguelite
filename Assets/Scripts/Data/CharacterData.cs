using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/Character Data", order = int.MaxValue)]
    public class CharacterData : ObjectData
    {
        
        [SerializeField]
        private float moveSpeed = 0;
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    }
}