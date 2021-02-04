using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace P1
{
    using GameObjects;
    using P1.Events;

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
            List<RuneSocket> leftRunes = new List<RuneSocket>();
            leftRunes.Add(new RuneSocket(RuneType.Blue));
            leftRunes.Add(new RuneSocket(RuneType.Red));
            
            List<RuneSocket> rightRunes = new List<RuneSocket>();
            leftRunes.Add(new RuneSocket(RuneType.Green));
            leftRunes.Add(new RuneSocket(RuneType.Green));
            leftRunes.Add(new RuneSocket(RuneType.Blue));

            Bracelet bracelet_left = new Bracelet(1, leftRunes, EquipPart.Bracelet_Left);
            Bracelet bracelet_right = new Bracelet(1, rightRunes, EquipPart.Bracelet_Right);

            DashRobe robe = new DashRobe();

            Inventory.Instance.GetItem(bracelet_left);
            Inventory.Instance.GetItem(bracelet_right);
            Inventory.Instance.GetItem(robe);

            Inventory.Instance.EquipItem(bracelet_left);
            Inventory.Instance.EquipItem(bracelet_right);
            Inventory.Instance.EquipItem(robe);
        }

        // Update is called once per frame
        void Update()
        {
            InputCommand();

        }

        void InputCommand()
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
            else if (Input.GetMouseButtonDown(1))
            {
                Ray2D ray = new Ray2D(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                RaycastHit2D hitResult = Physics2D.Raycast(ray.origin, ray.direction);
                if (hitResult.collider != null)
                {
                    var direction = new Vector3(hitResult.point.x, hitResult.point.y) - transform.position;
                    UseEquippedItem(EquipPart.Bracelet_Right, direction);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
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
            // TODO. 체력바 UI 확인용 임시 코드 
            else if (Input.GetKeyDown(KeyCode.F))
            {
                int maxHp = Random.Range(10, 100);
                int curHp = Random.Range(1, maxHp);
                HpChangedEvent e = new HpChangedEvent(maxHp, curHp);
                EventManager.Instance.Raise(e);
            }
            // TODO. 스킬 쿨타임 UI 확인용 임시 코드 
            else if (Input.GetKeyDown(KeyCode.R))
            {
                SkillUsedEvent e = new SkillUsedEvent(EquipPart.Bracelet_Left, 1);
                EventManager.Instance.Raise(e);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                SkillUsedEvent e = new SkillUsedEvent(EquipPart.Bracelet_Right, 5);
                EventManager.Instance.Raise(e);
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                InventoryUIController.Instance.SwitchActiveInventoryUI();
            }

            // 이동 관련
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        }

        public void UseEquippedItem(EquipPart part, Vector3 dir)
        {
            Inventory.Instance.GetEquippedItem(part)?.Use(gameObject, dir);
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
