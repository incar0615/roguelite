using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{
    using GameObjects;
    public class PCBehaviour : CharacterBehaviour
    {

        private Camera mainCam;
        private Rigidbody2D rigidBody;
        private SpriteRenderer sprRenderer;

        private float horizontal;
        private float vertical;

        public float moveLimiter = 0.71f; // 대각선 이동속도 제한
        public float runSpeed = 5.0f;

        public Player playerData;
        public List<Magic> magics;

        // FIXME. 임시 코드
        public int currentMagic = 0;

        public ProjectileModifiers[] tempMods;
        // 임시. 추후에 마법 정보의 것으로 교체 
        [SerializeField]
        private float projectileSpeed = 5.0f;

        public override Character GetCharData()
        {
            return playerData;
        }

        private void OnEnable()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
            mainCam = Camera.main;
            rigidBody = GetComponent<Rigidbody2D>();
            sprRenderer = GetComponent<SpriteRenderer>();

            magics = new List<Magic>();

            // TODO 임시코드
            Magic magic = new Magic(5757, "기본마법", 10.0f, 12.0f,1.0f, "none");

            List<ProjectileModifiers> runes = new List<ProjectileModifiers>();
            foreach(var rune in tempMods)
            {
                runes.Add(rune);
            }
            magic.SetRune(runes);
            magics.Add(magic);
        }

        // Update is called once per frame
        void Update()
        {
            // 투사체 발사
            if (Input.GetMouseButtonDown(0))
            {
                Ray2D ray = new Ray2D(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hitResult = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitResult.collider != null)
                {
                    var direction = new Vector3(hitResult.point.x, hitResult.point.y) - transform.position;
                    Attack(direction);
                }
            }

            // 이동 관련
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        }

        public void Attack(Vector3 dir)
        {
            var bullet = PoolManager.GetProjectileObject();

            bullet.GetAttackData().Atk = 4; // FIXME. 임시코드

            bullet.transform.position = transform.position + dir.normalized * 0.1f;
            bullet.Shoot(dir.normalized, magics[currentMagic]);
        }

        void FixedUpdate()
        {
            // 대각선 이동시 이동속도 감소
            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            rigidBody.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }

        public void Damaged(float dmg)
        {
            StartCoroutine(Hitted());
        }

        // 임시 피격 효과
        IEnumerator Hitted()
        {
            sprRenderer.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            sprRenderer.color = Color.white;
        }

        IEnumerator LifeTimeCheck(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);

            if(isActiveAndEnabled)
            {

            }
        }

    }

}
