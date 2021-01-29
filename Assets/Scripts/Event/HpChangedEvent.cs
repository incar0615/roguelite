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
        public BattleObject objectData;
        public float maxHp;
        public float curHp;
    }

}