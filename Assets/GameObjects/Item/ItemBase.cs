using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public abstract class ItemBase
        {
            private int itemID = 0;
            public int ItemID { get { return itemID; } set { itemID = value; } }

            private string itemName;
            public string ItemName { get { return itemName; } set { itemName = value; } }

            private string imgPath;
            public string ImgPath { get { return imgPath; } set { imgPath = value; } }

            private string descr;
            public string Descr { get { return descr; } set { descr = value; } }

            private ItemType itemType;
            public ItemType ItemType { get { return itemType; } set { itemType = value; } }
        }

    }
}