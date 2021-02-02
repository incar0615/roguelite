using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P1
{
    using P1.Events;
    using P1.GameObjects;
    using P1.UI;
    using System;

    public class InventoryUIController : EventHandler
    {
        public GridLayoutGroup inventoryGrid;
        public GameObject InventorySlotPrefap;

        public List<InventorySlot> inventorySlotList;

        private static readonly Dictionary<string, Type> invenStrCatDict = new Dictionary<string, Type>()
            {
                { "All", typeof(ItemBase) },
                { "Bracelet", typeof(Bracelet) },
                { "Robe", typeof(Robe)  },
                { "Rune", typeof(RuneItem) },
                { "Scroll", typeof(Scroll) },
            };

        #region Events
        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<ItemEquipEvent>(OnEquipmentChanged);
            EventManager.Instance.AddListener<ItemGetEvent>(OnItemGetEvent);
        }

        public override void UnsubscribeEvents()
        {
            EventManager.Instance.RemoveListener<ItemEquipEvent>(OnEquipmentChanged);
            EventManager.Instance.RemoveListener<ItemGetEvent>(OnItemGetEvent);
        }

        void OnEquipmentChanged(ItemEquipEvent iee)
        {
            // TODO
            StartCoroutine(EquipmentChangeRoutine(iee));
        }
        IEnumerator EquipmentChangeRoutine(ItemEquipEvent iee)
        {
            // inventory의 갱신을 위해 한프레임 쉬어줌
            yield return new WaitForEndOfFrame();

            // 장비 될 아이템
            if(iee.equippedItem != null)
            {
                // 인벤토리
                InventorySlot equipSlot = inventorySlotList.Find(element => element.item == iee.equippedItem);
                equipSlot.SetEquipped(true);

                // TODO 장비창
            }

            // 해제 될 아이템
            if (iee.unEquippedItem != null)
            {
                // 인벤토리
                InventorySlot unEquipSlot = inventorySlotList.Find(element => element.item == iee.unEquippedItem);
                unEquipSlot.SetEquipped(false);

                // TODO 장비창
            }
        }

        void OnItemGetEvent(ItemGetEvent ige)
        {
            InsertItem(ige.item);
        }
        #endregion

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
               EventManager.Instance.Raise(new ItemGetEvent(new DashRobe()));
            }
        }
        

        public void InsertItem(ItemBase item)
        {
            // TODO
            InventorySlot newSlot = Instantiate(InventorySlotPrefap, inventoryGrid.transform).GetComponent<InventorySlot>();
            newSlot.item = item;
            inventorySlotList.Add(newSlot.GetComponent<InventorySlot>());
        }

        /// <summary>
        /// 하드코딩 부분 주의
        /// tg object name을 카테고리로 사용하기 때문에 gameObject Name에 주의 해야함
        /// TODO. 추후에 좋은 아이디어 있으면 수정
        /// </summary>
        /// <param name="tg"></param>
        public void CategorySelected(Toggle tg)
        {
            if(tg.isOn)
            {
                AlignInventory(tg.name);
            }
        }

        public void AlignInventory(string cat)
        {
            Type t = null;
            if (invenStrCatDict.TryGetValue(cat, out t))
            {
                foreach(InventorySlot slot in inventorySlotList)
                {
                    if(slot.item.GetType() == t || slot.item.GetType().IsSubclassOf(t))
                    {
                        slot.enabled = true;
                    }
                    else
                    {
                        slot.enabled = false;
                    }
                }
            }


            switch (cat)
            {
                case "All":
                    for(int i = 0; i < inventoryGrid.transform.childCount; i++)
                    {
                        inventoryGrid.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    break;
                case "Bracelet":
                    
                    break;
                case "Robe":
                    break;
                case "Rune":
                    break;
                case "Scroll":
                    break;
            }
        }
    }

}
