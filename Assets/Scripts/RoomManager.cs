using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject[] closePortals;
    public GameObject[] startRooms;

    public List<GameObject> doors;
    public List<GameObject> rooms;
    private int rand;

    public Vector3 mapsize;
    private Vector2 offset = Vector2.zero;
    public int maxRoomCount;
    private float createTime;
    public bool createStart;
    public bool isDone;

    public TMP_InputField mapSizeX;
    public TMP_InputField mapSizeY;
    public TMP_InputField targetRoomCount;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isDone) return;
        createTime += Time.deltaTime;
        if (createTime > 0.1f)
        {
            if (rooms.Count != maxRoomCount)
            {
                MapReset();
                MapCreate();
                createTime = 0f;
            }
        }

        if (rooms.Count == maxRoomCount)
        {
            isDone = true;
        }
    }
    void MapReset()
    {
        foreach (GameObject obj in rooms)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in doors)
        {
            Destroy(obj);
        }
    }

    void MapCreate()
    {
        isDone = false;
        rooms = new List<GameObject>();
        doors = new List<GameObject>();
        rand = Random.Range(0, startRooms.Length);

        GameObject obj = Instantiate(startRooms[rand], new Vector3((mapsize.x / 2) - offset.x, (mapsize.y / 2) - offset.y, 0f), Quaternion.identity);
        rooms.Add(obj);
    }

    public void CreateBtn()
    {
        MapReset();

        mapsize = new Vector3(float.Parse(mapSizeX.text), float.Parse(mapSizeY.text), 0f);

        if (mapsize.x % 2 == 0) offset.x = 5f;
        if (mapsize.y % 2 == 0) offset.y = 5f;
        mapsize *= 10f;

        maxRoomCount = int.Parse(targetRoomCount.text);
        if (maxRoomCount < 1) Debug.Log("Reset Room Count!");
        else
        {
            MapCreate();
        }
    }
}
