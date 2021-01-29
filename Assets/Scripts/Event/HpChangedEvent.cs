using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class HpChangedEvent : Event
    {
        public float maxHp;
        public float curHp;

        public HpChangedEvent(float maxHp, float curHp)
        {
            this.maxHp = maxHp;
            this.curHp = curHp;
        }
    }

}