using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Bracelet : EquipmentItem
        {
            private int magicId;
            public int MagicId { get { return magicId; } set { magicId = value; } }

            private List<RuneItem> runeList;
            public List<RuneItem> RuneList { get { return runeList; } set { runeList = value; } }

            private Magic magic;
            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                if(magic != null) magic.Use(ownerObj, dir);
            }

            public Bracelet(int magicId, List<RuneItem> runes, EquipPart part)
            {
                EquipPart = part;
                this.magicId = magicId;
                runeList = runes;

                // XXX. 생성자에서 할지 Use에서 magic 이 null 일때 생성할지 고민
                magic = MagicRegistry.Create(magicId);
            }
        }

    }
}