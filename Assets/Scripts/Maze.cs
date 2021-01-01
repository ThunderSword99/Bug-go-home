﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Maze : MonoBehaviour
{
    public static Maze instance;

    public GameObject wallprefab;
    public GameObject cellPrefab;
    public GameObject cellHolder;
    public int rows = 13;
    public int columns = 10;
    
    public GameObject[,] listCell;

    public Stack<Transform> pathList = new Stack<Transform>();
   
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

    void Start()
    {
        listCell = new GameObject[rows,columns];
        for (int i=0;i< rows;i++)
        {
            for (int j=0;j<columns;j++)
            {
                GameObject go = Instantiate(cellPrefab,Vector3.zero, Quaternion.identity);
                go.transform.SetParent(cellHolder.transform,false);
                go.GetComponent<Cell>().pos = new Vector3(i,j,0f);
                listCell[i,j] = go;
            }
        }

        GenerateMap();
        GameController.instance.SetBug();
        GameController.instance.SetRandomTarget();
        //FindTheWayToTarget();
    }
    
    private void DestroyNeighborWall(Cell currentCell,int way)
    {
        try
        {
            if (way == 1) // left
            {
                Cell neighborCell = listCell[(int)currentCell.pos.x,(int)currentCell.pos.y-1].GetComponent<Cell>();
                neighborCell.DestroyWall(3);
            }
            else if (way == 2) // down
            {
                Cell neighborCell = listCell[(int)currentCell.pos.x+1,(int)currentCell.pos.y].GetComponent<Cell>();
                neighborCell.DestroyWall(4);
            }
            else if (way == 3) // right
            {
                Cell neighborCell = listCell[(int)currentCell.pos.x,(int)currentCell.pos.y+1].GetComponent<Cell>();
                neighborCell.DestroyWall(1);
            }
            else if (way == 4) // up
            {
                Cell neighborCell = listCell[(int)currentCell.pos.x-1,(int)currentCell.pos.y].GetComponent<Cell>();
                neighborCell.DestroyWall(2);
            }
        }
        catch
        {
        
        }
        
    }

    private void DestroyWall(Cell cell,int way)
    {
        cell.DestroyWall(way);
        DestroyNeighborWall(cell,way);
    }

    private void GenerateMap()
    {
        Cell firstCell = listCell[0,0].GetComponent<Cell>();
        DFS_DeleteWall(firstCell);
    }
    
    private Cell getNextCell(Cell currentCell,int way)
    {
        try
        {
            if (way == 1) // left
            {
                return listCell[(int)currentCell.pos.x,(int)currentCell.pos.y-1].GetComponent<Cell>();
            }
            else if (way == 2) // down
            {
                return listCell[(int)currentCell.pos.x+1,(int)currentCell.pos.y].GetComponent<Cell>();
            }
            else if (way == 3) // right
            {
                return listCell[(int)currentCell.pos.x,(int)currentCell.pos.y+1].GetComponent<Cell>();
            }
            else if (way == 4) // up
            {
                return listCell[(int)currentCell.pos.x-1,(int)currentCell.pos.y].GetComponent<Cell>();
            }
        }
        catch
        {
            return null;
        }
        return null;
    }

    private int[] suffefArray()
    {
        int[] temp = new int[4];
        temp[0] = UnityEngine.Random.Range(1,5);
        do
        {
            temp[1] = UnityEngine.Random.Range(1,5);
        }while(temp[1]==temp[0]);   
        do
        {
            temp[2] = UnityEngine.Random.Range(1,5);
        }while(temp[2]==temp[1] || temp[2]==temp[0]);
        temp[3] = 10 - temp[0] - temp[1] - temp[2];
        return temp;
    }

    private void DFS_DeleteWall(Cell cell)
    {
        //cell.GetComponent<Image>().color = Color.red;
        cell.isVisited = true;

        int[] randomWay = suffefArray();
        

        for (int i=0;i<4;i++)
        {
            Cell nextCell = getNextCell(cell,randomWay[i]);
            
            if (nextCell==null)
            {
                continue;
            }
            
            if (!nextCell.isVisited)
            {
                DestroyWall(cell,randomWay[i]);
                DFS_DeleteWall(nextCell);
            }
            
            
        }

    }

    private void ResetListCell()
    {
        for (int i=0;i< rows;i++)
        {
            for (int j=0;j<columns;j++)
            {
                listCell[i,j].GetComponent<Cell>().isVisited = false;   
            }
        }
    }

    public void FindTheWayToTarget()
    {
        ResetListCell();
        pathList.Clear();
        Cell firstCell = listCell[0,0].GetComponent<Cell>();
        BFS_FindPathToTarget();
        BackTracking_GetPath(GameController.instance.targetHolder.GetComponent<Cell>(),(int) GameController.instance.targetHolder.GetComponent<Cell>().pos.z);
    }

    /*private void DFS_FindWayToTarget(Cell cell)
    {
        if (cell.transform == GameController.instance.targetHolder.transform)
        {
            return;
        }
        cell.GetComponent<Image>().color = Color.red;
        pathList.Add(cell.transform);
        cell.isVisited = true;

        int[] randomWay = suffefArray();

        for (int i=0;i<4;i++)
        {
            Cell nextCell = getNextCell(cell,randomWay[i]);
            
            if (nextCell==null)
            {
                continue;
            }

            if (cell.isWallDestroyed(randomWay[i])  && !nextCell.isVisited)
            {
                DFS_FindWayToTarget(nextCell);
            }


           
        }

    }*/


    private void BFS_FindPathToTarget()
    {
        float[] _X = {0f,1f,0f,-1f};
        float[] _Y = {-1f,0f,1f,0f};
        Queue <GameObject> q = new Queue<GameObject>();
        q.Enqueue(listCell[0,0]);
        while(q.Count!=0)
        {
            GameObject go = q.Peek();
            Cell cell = go.GetComponent<Cell>();
            q.Dequeue();
            if (cell.transform == GameController.instance.targetHolder.transform)
            {
                return;
            }
            if (!cell.isVisited)
            {
                cell.isVisited = true;
            }

            for (int i=0;i<4;i++)
            {
                //GameObject tempGo = new GameObject();
                Vector3 pos = new Vector3(cell.pos.x + _X[i],cell.pos.y + _Y[i],cell.pos.z + 1);
                
                try
                {
                    
                    GameObject tempGo = listCell[(int)pos.x,(int)pos.y];
                    Cell tempCell = tempGo.GetComponent<Cell>();
                    
                    
                    if (!tempCell.isVisited && cell.isWallDestroyed(i+1))
                    {
                        tempCell.pos.z = pos.z;
                        tempGo.GetComponent<Image>().color = Color.red;
                        tempCell.isVisited = true;
                        q.Enqueue(tempGo);
                    }
                }
                catch
                {
                     
                }
            }
        }
    }

    private void BackTracking_GetPath(Cell cell,int val)
    {
        //Debug.Log(val);
        if (val==0)
        {
            pathList.Push(listCell[0,0].transform);
            return;
        }
        pathList.Push(cell.transform);
        cell.GetComponent<Image>().color = Color.green;
        float[] _X = {0f,1f,0f,-1f};
        float[] _Y = {-1f,0f,1f,0f};
        for (int i=0;i<4;i++)
        {
            try
            {
                Cell nextCell = listCell[(int)(cell.pos.x+_X[i]),(int)(cell.pos.y+_Y[i])].GetComponent<Cell>();
                if ((int)nextCell.pos.z==val-1)
                {
                    BackTracking_GetPath(nextCell,(int)(nextCell.pos.z));
                }
            }
            catch
            {
                continue;
            }
        }
    }
}
