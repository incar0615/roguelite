using P1.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    public static class MagicRegistry
    {
        public static Dictionary<int, Type> magicDict = new Dictionary<int, Type>();

        public static void Register<T>(int id) where T : Magic
        {
            var type = typeof(T);

            magicDict[id] = type;
        }

    }

}
 