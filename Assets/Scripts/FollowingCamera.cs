using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public class FollowingCamera : MonoBehaviour
    {
        private static FollowingCamera instance;
        public static FollowingCamera Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<FollowingCamera>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        var newSingleton = new GameObject("FollowingCamera").AddComponent<FollowingCamera>();
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

        public GameObject target;
        public RoomBehaviour currentRoom;
        
        public float minX;
        public float minY;
        public float maxX;
        public float maxY;

        // 임시코드 
        public Vector2 minRoomSize = new Vector2(16, 9);

        bool cameraWorking = false;
        // Start is called before the first frame update
        void Start()
        {
        }

        public void ChangeRoom(RoomBehaviour roomBehaviour)
        {
            StartCoroutine(MoveRoomRoutine(roomBehaviour));
            
        }

        void SetClampPosition()
        {
            Vector2 lb = new Vector2(currentRoom.leftDoor.transform.position.x - currentRoom.leftDoor.transform.localScale.x * 0.5f,
                    currentRoom.bottomDoor.transform.position.y - currentRoom.bottomDoor.transform.localScale.y * 0.5f);

            Vector2 rt = new Vector2(currentRoom.rightDoor.transform.position.x + currentRoom.rightDoor.transform.localScale.x * 0.5f,
                currentRoom.topDoor.transform.position.y + currentRoom.topDoor.transform.localScale.y * 0.5f);


            minX = lb.x + minRoomSize.x * 0.5f;
            minY = lb.y + minRoomSize.y * 0.5f;

            maxX = rt.x - minRoomSize.x * 0.5f;
            maxY = rt.y - minRoomSize.y * 0.5f;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (cameraWorking) return;
            float xClamp = Mathf.Clamp(target.transform.position.x, minX, maxX);
            float yClamp = Mathf.Clamp(target.transform.position.y, minY, maxY);

            transform.position = new Vector3(xClamp, yClamp, transform.position.z);
        }

        IEnumerator MoveRoomRoutine(RoomBehaviour roomBehaviour)
        {
            yield return new WaitForEndOfFrame();
            if (currentRoom)
            {
                cameraWorking = true;

                Vector3 moveTo = roomBehaviour.transform.position - currentRoom.transform.position;
                moveTo.Normalize();

                bool isHorizontalMove = Mathf.Abs(moveTo.x) > Mathf.Abs(moveTo.y);

                for (int i = 0; i < 20; i++)
                {
                    transform.Translate(isHorizontalMove ? moveTo * minRoomSize.x * 0.05f : moveTo * minRoomSize.y * 0.05f);
                    yield return new WaitForFixedUpdate();
                }
            }

            currentRoom = roomBehaviour;
            SetClampPosition();

            cameraWorking = false;
        }
    }

}
