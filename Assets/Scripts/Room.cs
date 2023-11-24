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
    private char check;
    private bool portalSet;
    private RoomManager rm;

    private void Start()
    {
        rm = RoomManager.instance;
    }

    private void Update()
    {
        if (rm.isDone == true && portalSet == false) OutterPortalClear();
    } 

    private void OutterPortalClear()
    {
        bool isSame = false;
        for(int i = nextPoints.Count - 1; i >= 0; i--)
        {
            foreach (GameObject room in rm.rooms)
            {
                isSame = false;
                if (nextPoints[i].transform.position == room.transform.position)
                {
                    isSame = true;
                    break;
                }
            }
            if(!isSame)
            {
                GameObject obj = Instantiate(rm.closePortals[nextPoints[i].GetComponent<RoomCreater>().roomDir], nextPoints[i].transform.position, Quaternion.identity);
                rm.doors.Add(obj);
                GameObject tempObj = nextPoints[i];
                nextPoints.Remove(tempObj);
                Destroy(tempObj);
            }
        }

        if (nextPoints.Count > 0)
        {
            for (int i = nextPoints.Count - 1; i >= 0; i--)
            {
                foreach (GameObject room in rm.rooms)
                {
                    if (nextPoints[i].transform.position == room.transform.position)
                    {
                        if(nextPoints[i].GetComponent<RoomCreater>() != null)
                        {
                            switch (nextPoints[i].GetComponent<RoomCreater>().roomDir)
                            {
                                case 0:
                                    check = 'B';
                                    break;
                                case 1:
                                    check = 'R';
                                    break;
                                case 2:
                                    check = 'T';
                                    break;
                                case 3:
                                    check = 'L';
                                    break;
                            }                            
                            foreach (char c in room.name)
                            {
                                if (check == c)
                                {
                                    GameObject tempObj = nextPoints[i];
                                    nextPoints.Remove(tempObj);
                                    Destroy(tempObj);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            GameObject tempObj = nextPoints[i];
                            nextPoints.Remove(tempObj);
                            Destroy(tempObj);                            
                        }
                        break;
                    }
                }
            }
        }
        InnerPortalClear();
        portalSet = true;
    }

    private void InnerPortalClear()
    {
        if (nextPoints.Count == 0) return;

        for(int i = nextPoints.Count - 1; i >= 0; i--)
        {
            GameObject obj = Instantiate(rm.closePortals[nextPoints[i].GetComponent<RoomCreater>().roomDir], nextPoints[i].transform.position, Quaternion.identity);
            rm.doors.Add(obj);
            GameObject tempObj = nextPoints[i];
            nextPoints.Remove(tempObj);
            Destroy(tempObj);
        }
    }
}
