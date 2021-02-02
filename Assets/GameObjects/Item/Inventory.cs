using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P1.Events;

namespace P1
{
    namespace GameObjects
    {
        public class Inventory
        {
            List<EquipmentItem> equipmentList;
            List<ArtifactItem> artifactList;
            List<RuneItem> runeList;
            Dictionary<EquipPart, EquipmentItem> equippedItemDict;

            public Inventory(List<EquipmentItem> equipmentItems, 
                List<ArtifactItem> artifacts, 
                List<RuneItem> runes, 
                Dictionary<EquipPart, EquipmentItem> equippedItems)
            {
                equipmentList = equipmentItems;
                artifactList = artifacts;
                runeList = runes;
                equippedItemDict = equippedItems;
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
