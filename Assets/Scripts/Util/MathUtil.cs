using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public static class MathUtil
    {
        public static float GetAngle(Vector2 vec1, Vector2 vec2)
        {
            float angle = (Mathf.Atan2(vec2.y, vec2.x) - Mathf.Atan2(vec1.y, vec1.x)) * Mathf.Rad2Deg;
            return angle;
        }

        public static Vector2 V2Rotate(Vector2 aPoint, float aDegree)
        {
            float rad = aDegree * Mathf.Deg2Rad;
            float s = Mathf.Sin(rad);
            float c = Mathf.Cos(rad);

            return new Vector2(
                aPoint.x * c - aPoint.y * s,
                aPoint.y * c + aPoint.x * s);
        }

        public static Vector3 V3Rotate(this Vector3 aPoint, float aDegree)
        {
            float rad = aDegree * Mathf.Deg2Rad;
            float s = Mathf.Sin(rad);
            float c = Mathf.Cos(rad);

            return new Vector3(
                aPoint.x * c - aPoint.y * s,
                aPoint.y * c + aPoint.x * s,
                aPoint.z);
        }

        public static Vector2 Get4WayContactNormal(Vector2 contactPoint, Vector3 trPos)
        {
            Vector2 v = contactPoint - new Vector2(trPos.x, trPos.y);

            return Mathf.Abs(v.x) > Mathf.Abs(v.y) ?
                (v.x > 0 ? Vector2.right : Vector2.left) :
                (v.y > 0 ? Vector2.up : Vector2.down);
        }
        /// <summary>
        /// flag에서 값이 1인 비트의 카운트를 리턴
        /// </summary>
        /// <param name="bit"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int GetBitCnt(int bit, int length)
        {
            int retVal = 0;

            for(int i = 0; i < length; i++)
            {
                if ( (bit | (1 << i)) == bit) retVal++;
            }

            return retVal;
        }
    }
}

