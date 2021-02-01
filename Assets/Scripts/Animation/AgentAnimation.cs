using UnityEngine;

namespace P1.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AgentAnimation : MonoBehaviour
    {
        protected Animator agentAnimator;
        private Vector3 direction;

        //애니메이션 파라미터
        #region 
        private const string moveXAnimationDirection = "moveX";
        private const string moveYAnimationDirection = "moveY";
        private const string faceDirectionX = "faceDirectionX";
        private const string faceDirectionY = "faceDirectionY";
        private const string isWalking = "walking";
        #endregion

        private void Awake()
        {
            agentAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// 캐릭터 이동 애니메이션
        /// </summary>
        /// <param name="moveInput">이동키(방향키)입력값</param>
        public void AnimateMovement(Vector2 moveInput)
        {
            if (moveInput != Vector2.zero)
            {
                agentAnimator.SetBool(isWalking, true);
                agentAnimator.SetFloat(moveXAnimationDirection, direction.x);
                agentAnimator.SetFloat(moveYAnimationDirection, direction.y);
            }
            else
            {
                agentAnimator.SetBool(isWalking, false);
                agentAnimator.SetFloat(faceDirectionX, direction.x);
                agentAnimator.SetFloat(faceDirectionY, direction.y);
            }
        }

        /// <summary>
        /// 마우스 위치에 따른 캐릭터 얼굴 방향 애니메이션
        /// </summary>
        /// <param name="pointerinput">마우스 위치값</param>
        public void FaceDirection(Vector2 pointerinput)
        {
            direction = (Vector3)pointerinput - transform.position;
        }
    }
}
