using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    namespace GameObjects
    {
        public class Room
        {
            public int gridX;
            public int gridY;
            public RoomType type;
            public int depth;

            // Right, Left, Top, Bottom 순서  
            // 0 = closed, 1 = opened
            public int doorMask = (int)Direction.ALL_CLOSE;

            public Room(int xPos, int yPos, RoomType rType, int depth = 9999)
            {
                gridX = xPos;
                gridY = yPos;
                type = rType;
                this.depth = depth;
            }

            public void ChangeDoorMask(int direction)
            {
                doorMask |= direction;
            }

            public bool IsOpenedDirection(int direction)
            {
                return (doorMask | direction) == doorMask ? true : false;
            }

        }
    }
}