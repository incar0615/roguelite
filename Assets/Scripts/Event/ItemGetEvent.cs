using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1.Events
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class ItemGetEvent : MessageEvent
    {
        public ItemBase item;

        public ItemGetEvent(ItemBase item)
        {
            this.item = item;
        }
    }
}
