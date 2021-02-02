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
        #region singleton
        public static InventoryUIController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<InventoryUIController>();
                    if(!instance) Debug.LogError("InventoryUI가 씬에 없음");
                }

                return instance;
            }
        }
        private static InventoryUIController instance = null;
        #endregion

        public GameObject InventoryUI;
        public GridLayoutGroup inventoryGrid;
        public GameObject InventorySlotPrefap;
        public List<InventorySlot> inventorySlotList;

        // TODO. 아이템 타입 추가시 여기도 추가 해야함
        private static readonly Dictionary<string, Type> invenStrCatDict = new Dictionary<string, Type>()
            {
                { "All", typeof(ItemBase) },
                { "Bracelet", typeof(Bracelet) },
                { "Robe", typeof(RobeBase)  },
                { "Rune", typeof(RuneItem) },
                { "Scroll", typeof(ScrollBase) },
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
            AddItem(ige.item);
        }

        void OnItemRemoveEvent(ItemRemoveEvent ire)
        {
            RemoveItem(ire.item);
        }    
        #endregion

        void Update()
        {
            // TODO. 임시코드 
            if (Input.GetMouseButtonDown(1))
            {
               EventManager.Instance.Raise(new ItemGetEvent(new DashRobe()));
            }
        }

        public void SwitchActiveInventoryUI()
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }

        public void AddItem(ItemBase item)
        {
            // TODO 카운트에 따른 GridGroup 변경 코드 추가해야함
            InventorySlot newSlot = Instantiate(InventorySlotPrefap, inventoryGrid.transform).GetComponent<InventorySlot>();
            newSlot.item = item;
            inventorySlotList.Add(newSlot);
        }

        public void RemoveItem(ItemBase item)
        {
            // TODO 카운트에 따른 GridGroup 변경 코드 추가해야함
            InventorySlot removeSlot = Instantiate(InventorySlotPrefap, inventoryGrid.transform).GetComponent<InventorySlot>();
            removeSlot.item = item;

            if(removeSlot.IsEquipped)
            {
                Inventory.Instance.UnequipItem(removeSlot.item as EquipmentItem, false);
            }
            inventorySlotList.Remove(removeSlot);
        }

        /// <summary>
        /// !!! 하드코딩 부분 주의 !!!
        /// tg object name을 카테고리로 사용하기 때문에 gameObject Name에 주의 해야함
        /// FIXME. 추후에 좋은 아이디어 있으면 수정
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
                    if (slot.item.GetType() == t || slot.item.GetType().IsSubclassOf(t))
                    {
                        slot.gameObject.SetActive(true);
                    }
                    else
                    {
                        slot.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
