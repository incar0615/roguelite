using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using P1.GameObjects;

namespace P1
{
    public enum RoomType
    {
        EMPTY = 0,
        START,
        BATTLE,
        EVENT,
        SHOP,
        TREASURE,
        TRAP,
        BOSS
    }
    enum Direction
    {
        RIGHT = 0b1000,
        LEFT = 0b0100,
        TOP = 0b0010,
        BOTTOM = 0b0001,

        ALL_OPEN = 0b1111,
        ALL_CLOSE = 0b0000,
    }

    public enum TileState
    {
        EMPTY,
        WALL,
        ROOM,
        CORRIDOR,
        DOOR
    }

    

    public class DungeonGenerator : MonoBehaviour
    {
        // TODO. GameInfo 같은거 만들어서 거기서 관리 필요
        public int randomSeed = -1; // 랜덤 시드 -1이면 랜덤 TODO. 

        public int mapSize; // 던전의 규모 
        public int maxRoomCnt; // 던전 내 방의 최대 개수

        public int maxRoomSizeX; // 방의 최대 가로 크기
        public int maxRoomSizeY; // 방의 최대 세로 크기

        public int maxShopCnt; // 상점 최대 개수
        public List<int> validShopDepthList; // 상점의 DepthFactor

        public int blockStartAdjacentCnt; // 시작방에서 막을 인접 방 개수

        private Room[,] dungeonRooms;
        private List<Room> activeRoomList = new List<Room>();
        
        private TileState[,] tileMap;
        private List<int> wallTilelist = new List<int>();

        public GameObject wallPrefap;
        public GameObject doorPrefap;
        public GameObject corridorPrefap;

        public GameObject roomPrefap;
        public GameObject bigRoomPrefap;

        public bool generateOnLaunch = false;
        // Start is called before the first frame update
       
        void Start()
        {
            if(generateOnLaunch) ProceedGenerate();
        }

        public void Init()
        {
            activeRoomList.Clear();
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject); 
            }

            if (randomSeed == -1)
            {
                Debug.Log("randomSeed");
                // TODO. 랜덤 시드 어디서 정할건지 체크하고 변경.    
                randomSeed = UnityEngine.Random.Range(0, int.MaxValue);
            }

            // 시드 설정
            UnityEngine.Random.InitState(randomSeed);

            // 0 ~ 3 으로 Clamp
            blockStartAdjacentCnt = Mathf.Clamp(blockStartAdjacentCnt, 0, 3);
        }

        public void ProceedGenerate()
        {
            Init();
            GenerateDungeon(mapSize);

            //DisplayMap();
            InstantiateRooms();
        }

        void InstantiateRooms()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    RoomType type = dungeonRooms[x, y].type;
                    switch (type)
                    {
                        case RoomType.BOSS:
                            break;

                        case RoomType.START:
                            break;

                        case RoomType.BATTLE:
                            break;

                        case RoomType.EVENT:
                            break;

                        case RoomType.SHOP:
                            break;
                        case RoomType.TREASURE:
                            break;
                        case RoomType.TRAP:
                            break;

                        case RoomType.EMPTY:
                        default:
                            continue;
                    }

                    // 임시코드
                    bool isBigRoom = UnityEngine.Random.Range(0.0f, 1.0f) < 0.0f;
                    GameObject room = Instantiate(isBigRoom ? bigRoomPrefap : roomPrefap, new Vector3(x * maxRoomSizeX, y * maxRoomSizeY), roomPrefap.transform.rotation, transform);
                    RoomBehaviour roomBehaviour = room.GetComponent<RoomBehaviour>();
                    roomBehaviour.Room = dungeonRooms[x, y];

                    roomBehaviour.InitDoors();
                }
            }
            int startPos = (int)((mapSize - 1) * 0.5f);
            // TODO. 임시코드 
            GameObject.Find("Character").transform.position = new Vector3(startPos * maxRoomSizeX, startPos * maxRoomSizeY, 0);
        }

        void DisplayMap()
        {
            for(int y = 0; y < mapSize; y++)
            {
                for(int x = 0; x < mapSize; x++)
                {
                    RoomType type = dungeonRooms[x, y].type;
                    Sprite spr = null;
                    switch (type)
                    {
                        case RoomType.BOSS:
                            spr = Resources.Load("Textures/Boss", typeof(Sprite)) as Sprite;
                            break;

                        case RoomType.START:
                            break;

                        case RoomType.BATTLE:
                            spr = Resources.Load("Textures/Battle", typeof(Sprite)) as Sprite;
                            break;

                        case RoomType.EVENT:
                            spr = Resources.Load("Textures/Event", typeof(Sprite)) as Sprite;
                            break;

                        case RoomType.SHOP:
                            spr = Resources.Load("Textures/Shop", typeof(Sprite)) as Sprite;
                            break;
                        case RoomType.TREASURE:
                            break;
                        case RoomType.TRAP:
                            break;

                        case RoomType.EMPTY:
                        default:
                            continue;
                    }

                    GameObject room = Instantiate(wallPrefap, new Vector3(x * 3, y * 3), wallPrefap.transform.rotation, transform);
                    if(spr != null) room.GetComponent<SpriteRenderer>().sprite = spr;
                    if (dungeonRooms[x, y].IsOpenedDirection((int)Direction.RIGHT)) Instantiate(corridorPrefap, new Vector3(x * 3 + 1, y * 3), doorPrefap.transform.rotation, room.transform);
                    if (dungeonRooms[x, y].IsOpenedDirection((int)Direction.LEFT)) Instantiate(corridorPrefap, new Vector3(x * 3 - 1, y * 3), doorPrefap.transform.rotation, room.transform);
                    if (dungeonRooms[x, y].IsOpenedDirection((int)Direction.TOP)) Instantiate(corridorPrefap, new Vector3(x * 3, y * 3 + 1), doorPrefap.transform.rotation, room.transform);
                    if (dungeonRooms[x, y].IsOpenedDirection((int)Direction.BOTTOM)) Instantiate(corridorPrefap, new Vector3(x * 3, y * 3 - 1), doorPrefap.transform.rotation, room.transform);
                }
            }

            int startPos = (int)((mapSize - 1) * 0.5f);
            // TODO. 임시코드 
            GameObject.Find("Character").transform.position = new Vector3(startPos * 3, startPos * 3, 0);
            Camera.main.orthographicSize = mapSize < 10 ? mapSize * 2 : mapSize < 20 ? 20 * (mapSize * 0.1f) : 30;
        }

        void GenerateDungeon(int mapSize)
        {
            int startPos = (int)((mapSize - 1) * 0.5f);

            dungeonRooms = new Room[mapSize, mapSize];

            // 방을 모두 빈방으로 초기화
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    dungeonRooms[x, y] = new Room(x, y, RoomType.EMPTY);
                }
            }

            // XXX. 던전 한가운데를 시작방으로 
            dungeonRooms[startPos, startPos].type = RoomType.START;
            dungeonRooms[startPos, startPos].depth = 0;

            // 시작방은 인접한 통로 카운트에 따라서 사전처리
            while(true)
            {

                int closeDirection = UnityEngine.Random.Range(0b00000, 0b1111);

                if (MathUtil.GetBitCnt(closeDirection, 4) == blockStartAdjacentCnt)
                {
                    Debug.Log("CloseDir = " + closeDirection);
                    dungeonRooms[startPos, startPos].ChangeDoorMask(closeDirection);
                    break;
                }
            }
            // 1088744408
            activeRoomList.Add(dungeonRooms[startPos, startPos]);

            // FIXME. repeatCnt 는 안정성을 위해 추가해둠. 더 나은 방법이 떠오르면 수정
            int repeatCnt = 0;
            while (activeRoomList.Count < maxRoomCnt && repeatCnt < 10000)
            {
                repeatCnt++;

                Room selectedRoom = activeRoomList.ElementAt(UnityEngine.Random.Range(0, activeRoomList.Count));
                
                MakeRoom(selectedRoom);
            }

            // 던전 구조 완성 후 방으로
            SettingBossRoom();

            SettingShopRoom();

            SettingBattleRoom();
            Debug.Log("RepeatCnt = " + repeatCnt + "  createdRoomCnt = " + activeRoomList.Count);
        }

        void MakeRoom(Room room)
        {
            int rand = UnityEngine.Random.Range(0, 4);

            int direction = 0b1000;
            direction >>= rand;

            // 해당 방향에 이미 방이 생성되어 있으면 리턴
            if(room.IsOpenedDirection(direction)) return;

            int gridX = room.gridX;
            int gridY = room.gridY;

            switch (direction)
            {
                case (int)Direction.RIGHT:
                    gridX += 1;
                    break;

                case (int)Direction.LEFT:
                    gridX -= 1;
                    break;

                case (int)Direction.TOP:
                    gridY += 1;
                    break;

                case (int)Direction.BOTTOM:
                    gridY -= 1;
                    break;

                default:
                    Debug.LogError("WrongCode");
                    return;
            }

            int flipedDirection = (direction == (int)Direction.RIGHT || direction == (int)Direction.TOP) ?
                direction >> 1 : direction << 1;

            if (!IsValidGridPos(gridX, gridY, flipedDirection)) return;

            // 도어 플래그 수정
            room.ChangeDoorMask(direction);
            dungeonRooms[gridX, gridY].ChangeDoorMask(flipedDirection);
            
            // 뎁스 설정
            room.depth = Mathf.Min(dungeonRooms[gridX, gridY].depth + 1, room.depth);
            dungeonRooms[gridX, gridY].depth = Mathf.Min(dungeonRooms[gridX, gridY].depth, room.depth + 1);

            // 방 리스트에 없으면 추가
            if (!activeRoomList.Contains(dungeonRooms[gridX, gridY]))
            {
                activeRoomList.Add(dungeonRooms[gridX, gridY]);
            }
        }

        void SettingBattleRoom()
        {
            List<Room> filterList = new List<Room>();

            // 보스방 설정
            // 뎁스가 1보다 큰 최말단의 방들을 가져와서 랜덤
            filterList = activeRoomList.
                Where(room => room.type == RoomType.EMPTY).
                ToList();

            Debug.Log("Battle Filter Cnt = " + filterList.Count);

            foreach(Room r in filterList)
            {
                r.type = RoomType.BATTLE;
            }
        }

        void SettingShopRoom()
        {
            List<Room> filterList = new List<Room>();

            // 빈방 & 알맞은 뎁스에 있는 방
            filterList = activeRoomList.
                Where(room => room.type == RoomType.EMPTY).
                Where(room => validShopDepthList.Contains(room.depth)).
                ToList();

            // 상점 개수 
            int repeatCnt = Mathf.Min(filterList.Count, maxShopCnt);

            for(int i = 0; i < repeatCnt; i++)
            {
                Room selectedRoom = filterList.ElementAt(UnityEngine.Random.Range(0, filterList.Count));
                filterList.Remove(selectedRoom);
                selectedRoom.type = RoomType.SHOP;
            }
        }

        void SettingBossRoom()
        {
            List<Room> filterList = new List<Room>();

            // 보스방 설정
            // 뎁스가 1보다 큰 최말단의 방들을 가져와서 랜덤
            filterList = activeRoomList.
                Where(room => room.type == RoomType.EMPTY).
                Where(room => MathUtil.GetBitCnt(room.doorMask, 4) == 1).
                Where(room => room.depth > 1).
                ToList();

            Debug.Log("Boss Filter Cnt = " + filterList.Count);

            Room selectedRoom = filterList.ElementAt(UnityEngine.Random.Range(0, filterList.Count));
            selectedRoom.type = RoomType.BOSS;
        }

        /// <summary>
        /// dungeonRooms[gridX, gridY] 가 생성 조건에 적합한지 체크 
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool IsValidGridPos(int gridX, int gridY, int direction)
        {
            return (gridX >= 0 && gridX < mapSize &&
                gridY >= 0 && gridY < mapSize &&
                !dungeonRooms[gridX, gridY].IsOpenedDirection(direction));
        }
    }

}