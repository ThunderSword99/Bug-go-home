using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public static LineController instance;

    private LineRenderer lr;
    public Transform[] points;

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
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine()
    {
        //Debug.Log(Maze.instance.pathList.Count);
        points = Maze.instance.pathList.ToArray();
        lr.positionCount = points.Length;
    }

    private void Update()
    {
        for (int i=0;i<points.Length;i++)
        {
            lr.SetPosition(i,points[i].position);
        }
    }
}
