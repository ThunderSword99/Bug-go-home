using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isVisited;
    public bool isNeighborVisited;
    public Vector3 pos;

    public List<GameObject> listWall;
    public GameObject leftWall;// 1
    public GameObject downWall;// 2
    public GameObject rightWall;// 3
    public GameObject upWall;// 4

    public int getWallNumb()
    {
        return listWall.Count;
    }

    public bool isWallDestroyed(int way)
    {
        if (way==1)
        {
            if (leftWall==null)
            {
                return true;
            }
        }
        else if (way==2)
        {
            if(downWall==null)
            {
                return true;
            }
        }
        else if (way==3)
        {
            if (rightWall==null)
            {
                return true;
            }
        }
        else if (way==4)
        {
            if (upWall==null)
            {
                return true;
            }
        }
        return false;
    }

    public void DestroyWall(int i)
    {
        
        if (i==1)
        {
            listWall.Remove(leftWall);
            Destroy(leftWall);
        }
        else if (i==2)
        {
            listWall.Remove(downWall);
            Destroy(downWall);
        }
        else if (i==3)
        {
            listWall.Remove(rightWall);
            Destroy(rightWall);
        }
        else if (i==4)
        {
            listWall.Remove(upWall);
            Destroy(upWall);
        }
        
    }


}
