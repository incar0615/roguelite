using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1.Events
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class ItemEquipEvent : MessageEvent
    {
        public EquipPart part;
        public ItemBase equippedItem;
        public ItemBase unEquippedItem;

        public ItemEquipEvent(EquipPart part, ItemBase equippedItem, ItemBase unEquippedItem)
        {
            this.part = part;
            this.equippedItem = equippedItem;
            this.unEquippedItem = unEquippedItem;
        }
    }
}
