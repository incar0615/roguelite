using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using P1.Events;

namespace P1.UI
{
    public class EquipmentSlot : EventHandler
    {
        [Header("EquipmentSlot Properties")]

        [SerializeField]
        protected EquipPart part;

        public Image equipmentImg;
        public Text equipmentNameTxt;

        protected ItemBase item;

        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<ItemEquipEvent>(OnEquipmentChanged);
        }

        public override void UnsubscribeEvents()
        {
            EventManager.Instance.RemoveListener<ItemEquipEvent>(OnEquipmentChanged);
        }

        protected virtual void OnEquipmentChanged(ItemEquipEvent iee)
        {
            // 해당 부위가 아니면 리턴
            if (iee.part != part) return;

            if(iee.equippedItem == null)
            {
                // 장착 아이템이 없으면 해제만 시키면 되니까 비워주기
                equipmentImg.sprite = null;
                equipmentNameTxt.text = "Empty";
                item = null;
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
