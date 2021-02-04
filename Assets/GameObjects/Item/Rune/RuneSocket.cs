using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P1.Events;

namespace P1
{
    namespace GameObjects
    {
        public class RuneSocket
        {
            RuneType runeType;
            public RuneType RuneType { get => runeType; set => runeType = value; }
            
            RuneItem rune;
            public RuneItem Rune { get => rune; set => rune = value; }

            public RuneSocket(RuneType runeType, RuneItem runeItem = null)
            {
                this.runeType = runeType;
                rune = runeItem;
            }

            public bool IsEmpty()
            {
                return rune == null; ;
            }

            public void EquipRune(RuneItem runeItem)
            {
                rune = runeItem;
            }

            public void UnequipRune()
            {
                rune = null;
            }
        }

    }
}