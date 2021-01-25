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

            private List<Rune> runeList;
            public List<Rune> RuneList { get { return runeList; } set { runeList = value; } }

            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                base.Use(ownerObj, dir);

                magic.Use(ownerObj, dir);
            }
            // TODO 나중에 itemID로 데이터 찾아서 생성하도록 변경
            public Magic magic;
            public Bracelet(Magic magic, List<Rune> runes, EquipPart part)
            {
                EquipPart = part;
                this.magic = magic;
                runeList = runes;
            }
            //
        }

    }
}