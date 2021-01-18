using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public abstract class NPCBehaviour : CharacterBehaviour
    {
        protected float atkCooldown; // 공격 대기 시간
        public CharacterState State { get; set; }

        protected SpriteRenderer sprRenderer;

        public GameObjects.NPC npcData;
        public string npcName;

        // Use this for initialization
        protected virtual void Start()
        {
            atkCooldown = 0;
            sprRenderer = GetComponent<SpriteRenderer>();
            npcData = new GameObjects.NPC();

            // FIXME. 임시코드 
            DataUtil.LoadData(npcData, "Enemy01");
            npcName = npcData.ObjName;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Move(Vector3 targetPos, float moveSpeed)
        {
            // 목표 지점에 가까워지면 대기 상태로 변경
            Vector3 dist = targetPos - transform.position;
            if (dist.magnitude < 0.1)
            {
                State = CharacterState.IDLE;
            }
            else
            {
                transform.Translate(dist.normalized * moveSpeed * Time.deltaTime);
            }
        }

        public override GameObjects.Character GetCharData()
        {
            return npcData;
        }
    }

}
