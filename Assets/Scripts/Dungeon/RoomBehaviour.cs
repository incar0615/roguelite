using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using P1.GameObjects;

namespace P1
{
    public class RoomBehaviour : MonoBehaviour
    {
        Room room;
        public Room Room { get { return room; } set { room = value; } }

        public GameObject rightDoor;
        public GameObject leftDoor;
        public GameObject topDoor;
        public GameObject bottomDoor;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitDoors()
        {
            if (room.IsOpenedDirection((int)Direction.RIGHT)) rightDoor.SetActive(false);
            if (room.IsOpenedDirection((int)Direction.LEFT)) leftDoor.SetActive(false);
            if (room.IsOpenedDirection((int)Direction.TOP)) topDoor.SetActive(false);
            if (room.IsOpenedDirection((int)Direction.BOTTOM)) bottomDoor.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Debug.Log("ChangeRoom");
                FollowingCamera.Instance.ChangeRoom(this);
            }
        }
    }

}
