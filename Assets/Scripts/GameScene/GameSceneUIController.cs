using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using P1.GameObjects;
using P1.Events;

namespace P1
{
    public class GameSceneUIController : EventHandler
    {
        public Text hpText;
        public Image hpProgressImg;

        public Text goldText;
        public Text gemText;
        public Text contractText;

        public Image leftMagicIconImg;
        public Image leftMagicIconProgressImg;
        public Image rightMagicIconImg;
        public Image rightMagicIconProgressImg;

        public Image scrollMagicIcon;
        public Image scrollMagicProgressImg;


        Coroutine hpChangedCoroutine;
        Dictionary<EquipPart, Coroutine> skillCoroutineDict = new Dictionary<EquipPart, Coroutine>();
        Coroutine skillUsedCoroutine;
        public override void SubscribeEvents()
        {
            EventManager.Instance.AddListener<HpChangedEvent>(OnHpChanged);
            EventManager.Instance.AddListener<SkillUsedEvent>(OnSkillUsed);
        }

        public override void UnsubscribeEvents()
        {
            EventManager.Instance.RemoveListener<HpChangedEvent>(OnHpChanged);
        }
        #region HpChanged
        // HpChanged
        void OnHpChanged(HpChangedEvent hce)
        {
            if (hpChangedCoroutine != null) StopCoroutine(hpChangedCoroutine);

            hpChangedCoroutine = StartCoroutine(HpChangedRoutine(hce));   
        }

        IEnumerator HpChangedRoutine(HpChangedEvent hce)
        {
            yield return null;

            // TODO. 연출 추가 
            float maxHp = hce.maxHp;
            float curHp = hce.curHp;

            hpText.text = curHp + " / " + maxHp;

            hpProgressImg.fillAmount = curHp / maxHp;
        }
        #endregion

        #region SkillUsed
        void OnSkillUsed(SkillUsedEvent sue)
        {
            EquipPart ep = sue.skillPart;
            if (ep == EquipPart.Bracelet_Left ||
                ep == EquipPart.Bracelet_Right)
            {
                if (skillCoroutineDict.ContainsKey(ep)) StopCoroutine(skillCoroutineDict[ep]);
                skillCoroutineDict[ep] = StartCoroutine(BraceletSkillRoutine(sue));
                
            }
            else if(ep == EquipPart.Scroll)
            {
                if (skillCoroutineDict.ContainsKey(ep)) StopCoroutine(skillCoroutineDict[ep]);
                //skillCoroutineDict[ep] = StartCoroutine(ScrollSkillRoutine(sue));
            }
            else if(ep == EquipPart.Robe)
            {
                if (skillCoroutineDict.ContainsKey(ep)) StopCoroutine(skillCoroutineDict[ep]);
                //skillCoroutineDict[ep] = StartCoroutine(RobeSkillRoutine(sue));
            }
        }

        IEnumerator BraceletSkillRoutine(SkillUsedEvent sue)
        {
            yield return null;

            EquipPart ep = sue.skillPart;
            float cooldown = sue.cooldown;
            Image progressImg = (ep == EquipPart.Bracelet_Left ? leftMagicIconProgressImg : rightMagicIconProgressImg);

            while (cooldown > 0)
            {
                progressImg.fillAmount = cooldown / sue.cooldown;
                cooldown -= Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            progressImg.fillAmount = 0.0f;
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            
        }


    }

}
