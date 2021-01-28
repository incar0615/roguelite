using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using P1.GameObjects;

namespace P1
{
    public class MoveExecutor : MonoBehaviour
    {
        private static MoveExecutor instance;
        public static MoveExecutor Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<MoveExecutor>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        var newSingleton = new GameObject("MoveExecutor").AddComponent<MoveExecutor>();
                        instance = newSingleton;
                    }
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"> 움직일 오브젝트 </param>
        /// <param name="dir"> 방향 </param>
        /// <param name="speed"> 속도 </param>
        /// <param name="duration"> 이동시간 </param>
        /// <param name="stopMovement"> 이동시간 도중에 움직임 여부</param>
        public void Move(GameObject obj, Vector3 dir, float speed, float duration, bool stopMovement = false)
        {
            StartCoroutine(MoveRoutine(obj, dir.normalized, speed, duration, stopMovement));
        }
        
        IEnumerator MoveRoutine(GameObject obj, Vector3 dir, float speed, float duration, bool stopMovement)
        {
            Vector3 destPos = obj.transform.position + (dir * speed * duration);
            
            Ray2D ray = new Ray2D(obj.transform.position, dir);
            int layerMask = (1 << LayerMask.NameToLayer("CollisionObject"));
            RaycastHit2D hitResult = Physics2D.Raycast(obj.transform.position, dir, speed * duration, layerMask);
            if (hitResult.collider != null)
            {
                destPos = hitResult.point;
                Debug.DrawLine(obj.transform.position, destPos, Color.green, 2.0f); 
            }
            else
            {
                Debug.DrawLine(obj.transform.position, destPos, Color.red, 2.0f);
            }

            CharacterBehaviour cb = null;
            if (stopMovement)
            {
                 cb = obj.GetComponent<CharacterBehaviour>();
                if (cb)
                {
                    cb.GetCharData().IsMovable = false;
                }
            }

            float dt = 0;
            while(dt < duration)
            {
                dt += Time.deltaTime;

                if(Vector3.Distance(obj.transform.position, destPos) < 0.2f)
                {
                    Debug.Log(55);
                    break;
                }
                else
                {
                    Debug.Log(Vector3.Distance(obj.transform.position, destPos));
                    obj.transform.Translate(dir * speed * Time.deltaTime);
                }

                yield return new WaitForFixedUpdate();
            }

            if (stopMovement && cb)
            {
                cb.GetCharData().IsMovable = true;
            }
        }
    }
}