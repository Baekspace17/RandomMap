using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreater : MonoBehaviour
{
    public enum CreateRoomDirection
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public int roomDir = -1;
    private RoomManager rm;
    private int rand;
    public GameObject tempObj;

    private void Awake()
    {
        rm = RoomManager.instance;
    }
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (roomDir == -1) return;
        if (rm.mapsize.x < transform.position.x || rm.mapsize.y < transform.position.y || transform.position.x < 0 || transform.position.y < 0) return;
        if (rm.rooms.Count < rm.maxRoomCount)
        {
            if (rm.rooms != null)
            {
                foreach (GameObject obj in rm.rooms)
                {
                    if (obj.transform.position == transform.position) return;
                }
            }

            switch (roomDir)
            {
                case 0:
                    rand = Random.Range(0, rm.topRooms.Length);
                    tempObj = Instantiate(rm.topRooms[rand], transform.position, rm.topRooms[rand].transform.rotation);
                    rm.rooms.Add(tempObj);
                    break;
                case 1:
                    rand = Random.Range(0, rm.leftRooms.Length);
                    tempObj = Instantiate(rm.leftRooms[rand], transform.position, rm.leftRooms[rand].transform.rotation);
                    rm.rooms.Add(tempObj);
                    break;
                case 2:
                    rand = Random.Range(0, rm.bottomRooms.Length);
                    tempObj = Instantiate(rm.bottomRooms[rand], transform.position, rm.bottomRooms[rand].transform.rotation);
                    rm.rooms.Add(tempObj);
                    break;
                case 3:
                    rand = Random.Range(0, rm.rightRooms.Length);
                    tempObj = Instantiate(rm.rightRooms[rand], transform.position, rm.rightRooms[rand].transform.rotation);
                    rm.rooms.Add(tempObj);
                    break;
            }
            
            transform.parent.parent.GetComponent<Room>().nextPoints.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
