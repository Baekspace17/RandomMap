using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        T, B, L, R, TL, TR, BL, BR, TB, LR, TBR, TBL, TLR, BLR, ALL
    }

    public RoomType type;
    public List<GameObject> nextPoints;
    private bool portalSet;
    private RoomManager rm;

    private void Start()
    {
        rm = RoomManager.instance;
    }

    private void Update()
    {
        if (rm.isDone == true && portalSet == false) PortalClose();
    } 

    private void PortalClose()
    {
        bool isSame = false;
        foreach(GameObject dp in nextPoints)
        {
            foreach (GameObject room in rm.rooms)
            {
                isSame = false;
                if (dp.transform.position == room.transform.position)
                {
                    isSame = true;
                    break;
                }
            }
            if(!isSame)
            {
                GameObject obj = Instantiate(rm.closePortals[dp.GetComponent<RoomCreater>().roomDir], dp.transform.position, Quaternion.identity);
                rm.doors.Add(obj);
            }
        }
        portalSet = true;
    }
}
