using P1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public abstract class CharacterBehaviour : MonoBehaviour
    {
        public enum CharacterState
        {
            IDLE,
            MOVE,
            FOLLOW,
            BATTLE
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public abstract GameObjects.Character GetCharData();
    }

}
