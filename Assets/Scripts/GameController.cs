using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float bugSpeed = 5f;

    public GameObject bugHolder;
    public GameObject bug;
    public GameObject targetHolder;
    public GameObject target;

    private bool isBugMoving = false;

    public Transform targetPos;
    public int idx = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetBug()
    {
       bugHolder = Maze.instance.listCell[0,0];
       GameObject go = Instantiate(bug,bugHolder.transform.position,Quaternion.identity);
       go.transform.SetParent(bugHolder.transform,false);
    }

    public void SetRandomTarget()
    {
        int randomRow = Random.Range(0,13);
        int randomColumn = Random.Range(0,10);
        targetHolder = Maze.instance.listCell[randomRow,randomColumn];
        GameObject go = Instantiate(target,targetHolder.transform.position,Quaternion.identity);
        go.transform.SetParent(targetHolder.transform,false);
    }

    public void AutoMove()
    {
        isBugMoving = true;
    }

    private bool isGetToDestination(Transform x, Transform y)
    {
        return new Vector2(x.position.x,x.position.y) == new Vector2(y.position.x,y.position.y);
    }

    void Update()
    {
        Debug.Log(bug.GetComponent<RectTransform>().anchoredPosition);
        if (isBugMoving)
        {
            float step = bugSpeed*Time.deltaTime;
            if (LineController.instance.points.Length>0)
            {
                targetPos = LineController.instance.points[idx];
                if (!isGetToDestination(bug.transform,targetPos))
                {
                    
                    bug.transform.position = Vector3.MoveTowards(bug.transform.position,targetPos.position,step);
                }
                else
                {
                    Debug.Log(bug.transform.position + " - " + targetPos.position);
                    idx++;
                }
            }
            
        }
    }
}
