using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public enum ItemType
        {
            EquipmentItem = 0,
            Artifact,
            Rune,

        }

        public enum EquipPart
        {
            Robe = 0,
            Bracelet_Right,
            Bracelet_Left,
            Scroll,
        }

        public enum RuneType
        {
            Red = 0,
            Green,
            Blue,
        }
    }
}