using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1
{
    
    public class ProjectileBehaviour : AttackObject
    {
        [SerializeField]
        private Vector3 direction;
        public Vector3 Direction { get { return direction; } set { direction = value; } }

        [SerializeField]
        private float projectileSpeed;
        public float ProjectileSpeed { get { return projectileSpeed; } set { projectileSpeed = value; } }

        [SerializeField]
        private List<ProjectileModifiers> projectileMods;
        public List<ProjectileModifiers> ProjectileMods { get { return projectileMods; } set { projectileMods = value; } }

        [SerializeField]
        private int depth = 0;
        public int Depth { get { return depth; } set { depth = value; } }

        [SerializeField]
        private float maxTravelDist;
        public float MaxTravelDist { get { return maxTravelDist; } set { maxTravelDist = value; } }

        // FIXME. 임시코드
        float modChainSpeed = 0.4f;

        // XXX. Check. magic을 들고 있어야 하는지?
        private GameObjects.Magic magic;
        public GameObjects.Magic Magic { get { return magic; } }

        private bool isImmediatelyMod = false;
        private float travelDist;
        private Coroutine modChainRoutine;
        private Rigidbody2D rigidBody;

        public GameObjects.ProjectileAttack projectileData;
        public override GameObjects.Attack GetAttackData()
        {
            return projectileData;
        }

        private void Awake()
        {
            // TODO. 임시코드
            projectileData = new GameObjects.ProjectileAttack();
        }
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();    
        }

        void OnEnable()
        {
            travelDist = 0;
        }

        public void initSelf()
        {
            travelDist = 0;
        }

        public void Shoot(Vector3 direction, float maxTravelDist = -1, int depth = 0)
        {
            initSelf();

            this.direction = direction;

            /*this.magic = magic;

            this.projectileSpeed = magic.projectileSpeed;
            this.maxTravelDist = magic.maxTravelDist;
            this.depth = depth;
            this.maxTravelDist = (maxTravelDist != -1) ? maxTravelDist : magic.maxTravelDist;*/
        }
        public void Shoot(Vector3 direction, GameObjects.Magic magic, float maxTravelDist = -1, int depth = 0)
        {
            initSelf();

            this.direction = direction;

            this.magic = magic;

            this.projectileSpeed = magic.projectileSpeed;
            this.maxTravelDist = magic.maxTravelDist;
            this.depth = depth;
            this.maxTravelDist = (maxTravelDist != -1) ? maxTravelDist : magic.maxTravelDist;

            Debug.Log(this.maxTravelDist);
            this.projectileMods = magic.runes;

            // FIXME. 임시 코드. 기존 거리와 현재 거리가 같을때만 모드체인 코루틴 시작
            this.modChainRoutine = StartCoroutine(ModChainRoutine());
        }

        void DestroyBullet()
        {
            if (!gameObject.activeSelf) return;

            // 투사체 하위 자식들 해제 
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform tr = gameObject.transform.GetChild(i);
                // XXX. SendMessage랑 비교
                tr.GetComponent<P1.GameObjects.IAttackModifierObj>().ReturnObject();
            }
            PoolManager.ReturnObject(this);
        }

        void ModChain()
        {
            switch (ProjectileMods[depth])
            {
                case ProjectileModifiers.None:
                    break;
                case ProjectileModifiers.SplitShot:
                    this.SpiltShot(2, 30);
                    break;
                case ProjectileModifiers.Split3:
                    this.SpiltShot(3, 30);
                    break;
                case ProjectileModifiers.Boomerang:
                    this.ChangeDirShot(180, maxTravelDist * 0.33f);
                    break;
                case ProjectileModifiers.FireTrail:
                    this.TrailShot(GameObjects.Element.FIRE, 0.05f, 3.0f);
                    break;
                default:
                    break;
            }
            PoolManager.ReturnObject(this);
        }

        void Update()
        {
            //rigidBody.MovePosition(direction * projectileSpeed * Time.deltaTime);
            transform.Translate(direction * projectileSpeed * Time.deltaTime);

            // FIXME. 임시코드
            travelDist += projectileSpeed * Time.deltaTime;
            if (travelDist >= maxTravelDist)
            {
                Debug.Log("최대 사거리 도달" + maxTravelDist);

                //PoolManager.ReturnObject(this);
                if (isActiveAndEnabled && projectileMods.Count > depth)
                {
                    StopCoroutine(modChainRoutine);
                    ModChain();
                }
                else
                {
                    DestroyBullet();
                }
            }
        }

        private void FixedUpdate()
        {
            //transform.Translate(direction * projectileSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("GameObject") || col.CompareTag("Enemy"))
            {
                Vector2 contactPos = col.ClosestPoint(transform.position);
                Vector2 normal = MathUtil.Get4WayContactNormal(contactPos, col.transform.position);

                float drawLineTime = 1.0f;
                Debug.DrawLine(contactPos, contactPos - new Vector2(direction.x, direction.y), Color.green, drawLineTime);
                Debug.DrawLine(contactPos, contactPos + normal, Color.yellow, drawLineTime);

                direction = Vector2.Reflect(direction, normal);
                Debug.DrawLine(contactPos, contactPos + new Vector2(direction.x, direction.y), Color.red, drawLineTime);

                transform.position += direction * 0.15f;

                GameObjects.IDefender defender = col.GetComponent<CharacterBehaviour>()?.GetCharData();
                if(defender != null) BattleUtil.DamageProcess(projectileData, defender);

                DestroyBullet();
            }
        }

        IEnumerator ModChainRoutine()
        {
            yield return new WaitForSeconds(modChainSpeed);

            if(isActiveAndEnabled && projectileMods.Count > depth)
            {
                Debug.Log( (depth+1) + " 번째 룬 발동 - " + ProjectileMods[depth] );
                ModChain();
            } 
        }
    }

}
