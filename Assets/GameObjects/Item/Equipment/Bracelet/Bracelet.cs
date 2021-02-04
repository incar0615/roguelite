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

            private List<RuneSocket> runeSockets;
            public List<RuneSocket> RuneSockets { get { return runeSockets; } set { runeSockets = value; } }

            private Magic magic;
            public override void Use(GameObject ownerObj, Vector3 dir)
            {
                magic?.Use(ownerObj, dir);
            }

            public Bracelet(int magicId, List<RuneSocket> runeSockets, EquipPart part)
            {
                EquipPart = part;
                this.magicId = magicId;
                this.runeSockets = runeSockets;

                // 임시 경로 설정
                ImgPath = "Image/Item/Bracelet";

                // XXX. 생성자에서 할지 Use에서 magic 이 null 일때 생성할지 고민
                magic = MagicRegistry.Create(magicId);
            }
        }

    }
}