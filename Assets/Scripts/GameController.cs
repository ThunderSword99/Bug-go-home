using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject bugHolder;
    public GameObject bug;
    public GameObject targetHolder;
    public GameObject target;

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
        //bug.transform.position = Maze.instance.listCell[randomRow,randomColumn].transform.position;
        //Debug.Log(randomRow + " " + randomColumn);
        //Debug.Log(Maze.instance.listCell[randomRow,randomColumn].gameObject.transform.localPosition);
        targetHolder = Maze.instance.listCell[randomRow,randomColumn];
        GameObject go = Instantiate(target,targetHolder.transform.position,Quaternion.identity);
        go.transform.SetParent(targetHolder.transform,false);
    }

    
}
