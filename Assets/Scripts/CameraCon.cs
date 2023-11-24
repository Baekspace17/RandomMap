using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    private RoomManager rm;

    private void Start()
    {
        rm = RoomManager.instance;
    }

    private void Update()
    {
        if (rm.rooms.Count != 0) transform.position = rm.rooms[0].transform.position;
    }
}
