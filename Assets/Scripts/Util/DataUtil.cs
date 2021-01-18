using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{

    public class DataUtil
    {
        public static void LoadData(GameObjects.NPC npcData, string npcName)
        {
            var dataPath = "GameData/" + npcName;
            var loadedData = Resources.Load(dataPath, typeof(ScriptableObject)) as CharacterData;

            Debug.Log(loadedData.MaxHp);
            if (!loadedData)
            {
                Debug.LogError(dataPath + " NullData");
                return;
            }

            npcData.ObjName = loadedData.ObjName;
            npcData.MaxHp = loadedData.MaxHp;
            npcData.CurHp = loadedData.CurHp;
            npcData.Atk = loadedData.Atk;
            npcData.AtkRange = loadedData.AtkRange;
            npcData.AtkSpeed = loadedData.AtkSpeed;
            npcData.DetectionRange = loadedData.DetectionRange;
            npcData.Def = loadedData.Def;
            npcData.MoveSpeed = loadedData.MoveSpeed;
        }
    }

}