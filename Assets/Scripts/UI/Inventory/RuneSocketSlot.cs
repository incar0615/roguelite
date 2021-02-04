using P1.GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace P1.UI
{
    public class RuneSocketSlot : MonoBehaviour
    {
        [SerializeField]
        Image TypeImg;

        [SerializeField]
        Image itemImg;

        [SerializeField]
        Text itemNameTxt;

        public void SetActiveSocket(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void InitWithRuneSocket(RuneSocket runeSocket)
        {
            SetTypeImg(runeSocket.RuneType);

            if(runeSocket.Rune != null)
            {
                itemImg.sprite = ResourceManager.Instance.LoadResourceByPath<Sprite>(runeSocket.Rune.ImgPath);
                itemNameTxt.text = runeSocket.Rune.ItemName;
            }
        }

        void SetTypeImg(RuneType runeType)
        {
            // TODO 나중에 이미지로 교체해야함
            if(runeType == RuneType.Red) TypeImg.color = Color.red;
            else if (runeType == RuneType.Blue) TypeImg.color = Color.blue;
            else if (runeType == RuneType.Green) TypeImg.color = Color.green;
        }
    }
}