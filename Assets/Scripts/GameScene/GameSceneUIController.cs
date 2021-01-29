using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using P1.GameObjects;

namespace P1
{
    public class GameSceneUIController : EventHandler
    {
        public Text hpText;
        public Image hpProgressImg;

        public Text goldText;
        public Text gemText;
        public Text contractText;

        public Image leftMagicImg;
        public Image rightMagicImg;

        public Image scrollMagicIcon;
        public Image scrollMagicProgressImg;


        Coroutine hpChangedCoroutine;
        Coroutine skillUsedCoroutine;
        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<HpChangedEvent>(OnHpChanged);
        }

        public override void UnsubscribeEvents()
        {
            EventManager.Instance.RemoveListener<HpChangedEvent>(OnHpChanged);
        }
        #region HpChanged
        // HpChanged
        void OnHpChanged(HpChangedEvent e)
        {
            hpChangedCoroutine = StartCoroutine(HpChangedRoutine(e));   
        }

        IEnumerator HpChangedRoutine(HpChangedEvent e)
        {
            yield return null;

            BattleObject objectData = e.objectData;

            float maxHp = e.maxHp;
            float curHp = e.curHp;

            hpText.text = curHp + " / " + maxHp;

            hpProgressImg.fillAmount = curHp / maxHp;
        }
        #endregion

        #region SkillUsed
        void OnSkillUsed()
        {

        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }


    }

}
