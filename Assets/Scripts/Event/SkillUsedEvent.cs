using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public class SkillUsedEvent : MessageEvent
    {
        public EquipPart skillPart;
        public float cooldown;

        public SkillUsedEvent(EquipPart skillPart, float cooldown)
        {
            this.skillPart = skillPart;
            this.cooldown = cooldown;
        }
    }
}