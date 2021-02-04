using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using P1.Events;

namespace P1.UI
{
    public class BraceletEquipmentSlot : EquipmentSlot
    {
        [Header("EquipmentSlot Properties")]
        [SerializeField]
        List<RuneSocketSlot> runeSocketSlotList;

        List<RuneSocket> runeSocketsList;
        Bracelet bracelet;

        private void Awake()
        {
            List<RuneSocket> runeSocketsList = new List<RuneSocket>();
        }

        public override void SubscribeEvents()
        {
            base.SubscribeEvents();
            //EventManager.Instance.AddListener<ItemEquipEvent>(OnEquipmentChanged);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            //EventManager.Instance.RemoveListener<ItemEquipEvent>(OnEquipmentChanged);
        }

        protected override void OnEquipmentChanged(ItemEquipEvent iee)
        {
            base.OnEquipmentChanged(iee);
            // 해당 부위가 아니면 리턴
            if (iee.part != part) return;

            if (iee.equippedItem != null)
            {
                // TODO 소켓 처리
                bracelet = item as Bracelet;
                runeSocketsList = bracelet.RuneSockets;

                // 룬 소켓 이미지 순회
                for (int i = 0; i < runeSocketSlotList.Count; i++)
                {
                    // 룬 소켓만큼 이미지 활성화
                    if(i < runeSocketsList.Count)
                    {
                        runeSocketSlotList[i].SetActiveSocket(true);
                        runeSocketSlotList[i].InitWithRuneSocket(runeSocketsList[i]);
                    }
                    else
                    {
                        runeSocketSlotList[i].SetActiveSocket(false);
                    }
                }

            }
            else
            {
                item = iee.equippedItem;
                equipmentNameTxt.text = item.ItemName;
                equipmentImg.sprite = ResourceManager.Instance.LoadResourceByPath<Sprite>(item.ImgPath);
            }
        }
    }
}
