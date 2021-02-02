using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1.Events
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class ItemRemoveEvent : MessageEvent
    {
        public ItemBase item;

        public ItemRemoveEvent(ItemBase item)
        {
            this.item = item;
        }
    }
}
