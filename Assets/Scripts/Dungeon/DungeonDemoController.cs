using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    /// <summary>
    /// 던전 생성 씬 UI 대응 컨트롤러
    /// </summary>
    public class DungeonDemoController : MonoBehaviour
    {
        public DungeonGenerator generator;

        public bool seedChanged = false;

        public UnityEngine.UI.Text seedDisplayText;

        // Start is called before the first frame update
        void Start()
        {
            generator = FindObjectOfType<DungeonGenerator>();
        }


        public void ChangeRandomSeed(UnityEngine.UI.Text text)
        {
            if (text.text.Length == 0)
            {
                seedChanged = false;
                generator.randomSeed = -1;
                return;
            }

            int value = -1;
            if (int.TryParse(text.text, out value))
            {
                generator.randomSeed = value;
                seedChanged = true;
            }
        }

        public void ChangeDungeonSize(UnityEngine.UI.Text text)
        {
            int value = 7;
            if (int.TryParse(text.text, out value))
            {
                if (value > 1000) value = 0;
                generator.mapSize = value;
            }
        }

        public void ChangeRoomCnt(UnityEngine.UI.Text text)
        {
            int value = 16;
            if (int.TryParse(text.text, out value))
            {
                generator.maxRoomCnt = value;
            }
        }

        public void ClickedGenerateBtn()
        {
            if (!seedChanged)
            {
                generator.randomSeed = -1;
            }

            generator.ProceedGenerate();

 
            seedDisplayText.text = "현재 시드 값\n" + generator.randomSeed.ToString();
        }

        public void ChangedBlockStartAdjacentCnt(UnityEngine.UI.Text text)
        {
            int value = 0;
            if (int.TryParse(text.text, out value))
            {
                generator.blockStartAdjacentCnt = value;
            }
        }

        public void ChangedShopCnt(UnityEngine.UI.Text text)
        {
            int value = 0;
            if (int.TryParse(text.text, out value))
            {
                generator.maxShopCnt = value;
            }
        }

        public void ChangedValidShopDepths(UnityEngine.UI.Text text)
        {
            List<int> depthList = new List<int>();
            string[] s = text.text.Split(',');

            foreach(string str in s)
            {
                int value = 0;
                if(int.TryParse(str, out value))
                {
                    depthList.Add(value);
                }
            }

            generator.validShopDepthList = depthList;
        }
    }

}
