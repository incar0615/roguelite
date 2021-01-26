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

        private Player playerData;
        public override Character GetCharData()
        {
            return playerData;
        }

        public Inventory inventory;

        private void OnEnable()
        {
            
        }

        void InitPlayerData()
        {
            playerData = new Player();
            playerData.MaxHp = 100;
            playerData.IsMovable = true;
            playerData.MoveSpeed = runSpeed;
        }
        // Start is called before the first frame update
        void Start()
        {
            InitPlayerData();

            mainCam = Camera.main;
            rigidBody = GetComponent<Rigidbody2D>();
            sprRenderer = GetComponent<SpriteRenderer>();

            // TODO 임시 코드 
            inventory = new Inventory(new List<EquipmentItem>(), new List<ArtifactItem>(), new List<RuneItem>(), new Dictionary<EquipPart, EquipmentItem>()); ;

            Bracelet bracelet = new Bracelet(1, new List<RuneItem>(), EquipPart.Bracelet_Left);
            DashRobe robe = new DashRobe(); 
            inventory.SetEquippedItem(bracelet);
            inventory.SetEquippedItem(robe);
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
                    UseEquippedItem(EquipPart.Bracelet_Left, direction);
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Ray2D ray = new Ray2D(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hitResult = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitResult.collider != null)
                {
                    var direction = new Vector3(hitResult.point.x, hitResult.point.y) - transform.position;
                    UseEquippedItem(EquipPart.Bracelet_Right, direction);
                }
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                Ray2D ray = new Ray2D(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hitResult = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitResult.collider != null)
                {
                    var direction = new Vector3(hitResult.point.x, hitResult.point.y) - transform.position;
                    UseEquippedItem(EquipPart.Robe, direction);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Ray2D ray = new Ray2D(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hitResult = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitResult.collider != null)
                {
                    var direction = new Vector3(hitResult.point.x, hitResult.point.y) - transform.position;
                    UseEquippedItem(EquipPart.Scroll, direction);
                }
            }

            // 이동 관련
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        }

        public void UseEquippedItem(EquipPart part, Vector3 dir)
        {
            inventory.GetEquippedItem(part).Use(gameObject, dir);
        }

        void FixedUpdate()
        {
            // 대각선 이동시 이동속도 감소
            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            if (playerData.IsMovable)
            {
                rigidBody.velocity = new Vector2(horizontal * playerData.MoveSpeed, vertical * playerData.MoveSpeed);
            }
            else
            {
                rigidBody.velocity = Vector2.zero;
            }
            
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
