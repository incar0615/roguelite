using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace P1
{
    public class MonsterBehaviour : NPCBehaviour
    {
       
        public GameObject targetPlayer;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            switch (State)
            {
                case CharacterState.IDLE:
                    if(targetPlayer == null)
                    {
                        DetectClosestPlayerChar();
                    }
                    else
                    {
                        // detectionRange 보다 가까우면 전투 돌입
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) < npcData.DetectionRange)
                        {
                            EnterBattle();
                        }
                    }
                    
                    break;

                case CharacterState.MOVE:
                    Move(targetPlayer.transform.position, npcData.MoveSpeed);
                    break;

                case CharacterState.BATTLE:
                    if (targetPlayer)
                    {
                        if (atkCooldown > 0)
                        {
                            atkCooldown -= Time.deltaTime;
                        }

                        // 사거리에 안닿으면 이동
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) > npcData.AtkRange)
                        {
                            Move(targetPlayer.transform.position, npcData.MoveSpeed);
                        }
                        // 사거리에 닿은 경우 공격
                        else if (atkCooldown <= 0)
                        {
                            //Attack(targetPlayer.GetComponent<Character>());
                        }
                    }
                    break;
            }
        }

        /*public void Attack(Character target)
        {
            Debug.Log("Attack");
            // TODO 공격 애니

            target.Damaged(enemyData.Damage);

            // 공격 쿨다운 초기화
            atkCooldown = enemyData.AtkSpeed;
        }*/


        IEnumerator Hitted()
        {
            // 임시 피격 처리
            sprRenderer.color = Color.red;

            yield return new WaitForSeconds(0.05f);

            sprRenderer.color = Color.black;
        }

        public void EnterBattle()
        {
            State = CharacterState.BATTLE;
            DetectClosestPlayerChar();
        }

        // 가장 가까운 플레이어 캐릭터를 타겟으로 설정
        public void DetectClosestPlayerChar()
        {
            // 멀티시에는 가장 가까운 캐릭터를 포커싱할지? 고려해봐야할듯 
            /*

            float minDist = 10000.0f;
            foreach (Character c in PartyManager.Instance.playerPartyList)
            {
                float dist = Vector3.Distance(c.transform.position, transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    targetPlayer = c.gameObject;
                }
            }
            */
        }
    }


}
