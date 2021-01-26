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

            var name = type.Name;
            magicDict[id] = type;
        }

        private static void Register<T>() where T : Magic
        {

        }

        public static Magic Create(int magicId)
        {
            if(!magicDict.TryGetValue(magicId, out var magic))
            {
                return null;
            }

            return (Magic)Activator.CreateInstance(magic);
        }

        static MagicRegistry()
        {
            // TODO temp id 나중에 마법 데이터 불러오면서 레지스트리 하도록 변경
            Register<TempMagic>(1);
        }
    }

}
 