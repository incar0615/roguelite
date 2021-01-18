using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class BattleObject
        {
            private string objName = "";
            public string ObjName { get { return objName; } set { objName = value; } }

            private float maxHp = 0;
            public float MaxHp { get { return maxHp; } set { maxHp = value; } }

            private float curHp = 0;
            public float CurHp { get { return curHp; } set { curHp = value; } }

            private float atk = 0;
            public float Atk { get { return atk; } set { atk = value; } }

            private float atkRange = 0;
            public float AtkRange { get { return atkRange; } set { atkRange = value; } }

            private float atkSpeed = 0;
            public float AtkSpeed { get { return atkSpeed; } set { atkSpeed = value; } }

            private float moveSpeed = 0;
            public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

            private float def = 0;
            public float Def { get { return def; } set { def = value; } }

            private float detectionRange = 0;
            public float DetectionRange { get { return detectionRange; } set { detectionRange = value; } }

            private bool isImmortal = false;
            public bool IsImmortal { get { return isImmortal; } set { isImmortal = value; } }

            private bool isMovable = true;
            public bool IsMovable { get { return isMovable; } set { isMovable = value; } }

            private bool isAttackable = true;
            public bool IsAttackable { get { return isAttackable; } set { isAttackable = value; } }
        }

    }

    public class TrapInfo
    {
        private string objName = "";
        public string ObjName { get { return objName; } set { objName = value; } }

        private float atk = 0;
        public float Atk { get { return atk; } set { atk = value; } }

        private float atkRange = 0;
        public float AtkRange { get { return atkRange; } set { atkRange = value; } }

        private float atkSpeed = 0;
        public float AtkSpeed { get { return atkSpeed; } set { atkSpeed = value; } }

        private float moveSpeed = 0;
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        private float def = 0;
        public float Def { get { return def; } set { def = value; } }

        private float detectionRange = 0;
        public float DetectionRange { get { return detectionRange; } set { detectionRange = value; } }

        private bool isImmortal = false;
        public bool IsImmortal { get { return isImmortal; } set { isImmortal = value; } }

        private bool isMovable = true;
        public bool IsMovable { get { return isMovable; } set { isMovable = value; } }

        private bool isAttackable = true;
        public bool IsAttackable { get { return isAttackable; } set { isAttackable = value; } }
    }

    public class ObstacleInfo
    {
        private string objName = "";
        public string ObjName { get { return objName; } set { objName = value; } }

        private float maxHp = 0;
        public float MaxHp { get { return maxHp; } set { maxHp = value; } }

        private float curHp = 0;
        public float CurHp { get { return curHp; } set { curHp = value; } }

        private float moveSpeed = 0;
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

        private float def = 0;
        public float Def { get { return def; } set { def = value; } }

        private bool isImmortal = false;
        public bool IsImmortal { get { return isImmortal; } set { isImmortal = value; } }

        private bool isMovable = true;
        public bool IsMovable { get { return isMovable; } set { isMovable = value; } }
    }
}