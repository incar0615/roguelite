using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using P1.Events;

namespace P1
{
    namespace GameObjects
    {
        public class Inventory
        {
            public static Inventory Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = LoadInventory();
                    }

                    return instance;
                }
            }
            private static Inventory instance = null;

            List<ItemBase> itemList;
            Dictionary<EquipPart, EquipmentItem> equippedItemDict;

            public Inventory(List<ItemBase> items, Dictionary<EquipPart, EquipmentItem> equippedItems)
            {
                this.itemList = items;
                this.equippedItemDict = equippedItems;
            }
            public List<ItemBase> GetItemList(Type t)
            {
                List<ItemBase> list = new List<ItemBase>();

                list = itemList.
                    Where(item => (item.GetType() == t || item.GetType().IsSubclassOf(t))).
                    ToList();

                return list;
            }

            public static Inventory LoadInventory()
            {
                Inventory inven = new Inventory(new List<ItemBase>(), 
                    new Dictionary<EquipPart, EquipmentItem>());

                return inven;
            }

            /// <summary>
            /// part 에 해당하는 장비를 반환
            /// 아이템 없을시 null 반환
            /// </summary>
            /// <param name="part"></param>
            /// <returns></returns>
            public EquipmentItem GetEquippedItem(EquipPart part)
            {
                EquipmentItem equipItem;
                if(equippedItemDict.TryGetValue(part, out equipItem))
                {
                    return equipItem;
                }
                else
                {
                    // TODO 장착된 아이템이 없을시 처리
                    return null;
                }
            }

            /// <summary>
            /// 아이템 장착
            /// </summary>
            /// <param name="item"></param>
            public void SetEquippedItem(EquipmentItem item)
            {
                EquipmentItem equipItem;
                if (equippedItemDict.TryGetValue(item.EquipPart, out equipItem))
                {
                    // 장착된 아이템이 있을경우 장착 해제
                    equipmentList.Add(equipItem);
                }

                equippedItemDict[item.EquipPart] = item;
                equipmentList.Remove(item);

                // TODO EventManager 머지 이후 스테이터스 갱신 처리 필요. 

            }
        }

    }
}
