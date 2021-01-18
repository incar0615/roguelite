using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "Scriptable Object/Object Data", order = int.MaxValue)]
    public class ObjectData : ScriptableObject
    {
        [SerializeField]
        private string objName = "";
        public string ObjName { get { return objName; } set { objName = value; } }

        [SerializeField]
        private float maxHp = 0;
        public float MaxHp { get { return maxHp; } set { maxHp = value; } }

        [SerializeField]
        private float curHp = 0;
        public float CurHp { get { return curHp; } set { curHp = value; } }

        [SerializeField]
        private float atk = 0;
        public float Atk { get { return atk; } set { atk = value; } }

        [SerializeField]
        private float atkRange = 0;
        public float AtkRange { get { return atkRange; } set { atkRange = value; } }

        [SerializeField]
        private float atkSpeed = 0;
        public float AtkSpeed { get { return atkSpeed; } set { atkSpeed = value; } }

        [SerializeField]
        private float def = 0;
        public float Def { get { return def; } set { def = value; } }

        [SerializeField]
        private float detectionRange = 0;
        public float DetectionRange { get { return detectionRange; } set { detectionRange = value; } }

        [SerializeField]
        private bool isImmortal = false;
        public bool IsImmortal { get { return isImmortal; } set { isImmortal = value; } }
    }
}