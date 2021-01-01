using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float bugSpeed = 5f;

    public GameObject bug;
    public GameObject target;

    public GameObject bugHolder;
    public GameObject bugPrefab;
    public GameObject targetHolder;
    public GameObject targetPrefab;

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
       bug = Instantiate(bugPrefab,bugHolder.transform.position,Quaternion.identity);
       bug.transform.SetParent(bugHolder.transform,false);
    }

    public void SetRandomTarget()
    {
        int randomRow = Random.Range(0,13);
        int randomColumn = Random.Range(0,10);
        targetHolder = Maze.instance.listCell[randomRow,randomColumn];
        target = Instantiate(targetPrefab,targetHolder.transform.position,Quaternion.identity);
        target.transform.SetParent(targetHolder.transform,false);
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
        if (isBugMoving)
        {
            if (LineController.instance.points.Length>0)
            {
                try
                {
                    targetPos = LineController.instance.points[idx];
                }
                catch
                {
                    LineController.instance.gameObject.SetActive(false);
                    return;
                }
                if (!isGetToDestination(bug.transform,targetPos))
                {
                    float step = bugSpeed*Time.deltaTime;
                    bug.transform.position = Vector3.MoveTowards(bug.transform.position,targetPos.position,step);
                }
                else
                {
                    idx++;
                }
            }
            
        }
    }
}
