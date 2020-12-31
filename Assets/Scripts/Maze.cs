using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Maze : MonoBehaviour
{
    public GameObject wallprefab;
    public GameObject cellPrefab;
    public GameObject cellHolder;
    public int rows = 13;
    public int columns = 10;
    
    public GameObject[,] listCell;

    int k=0;
   
    //private int[4] _X = {};
    //private int[4] _Y = {};

    // Start is called before the first frame update
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

        //DestroyRandomWall(listCell[3,3]);
        //BFS();
        GenerateMap();
        //int[] x = new int [4];
        //x = suffefArray();
        //Debug.Log(x[0] + " " + x[1] + " " + x[2] + " " + x[3]);
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
        DFS(firstCell);
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

    private void DFS(Cell cell)
    {
        //cell.GetComponent<Image>().color = Color.red;
        cell.isVisited = true;

        int[] randomWay = suffefArray();
        
        k++;
        if (k>=130)
        {
            return;
        }

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
                DFS(nextCell);
            }
            
            
        }

    }


    private void BFS()
    {
        float[] _X = {1f,-1f,0f,0f};
        float[] _Y = {0f,0f,-1f,1f};
        Queue <GameObject> q = new Queue<GameObject>();
        q.Enqueue(listCell[0,0]);
        while(q.Count!=0)
        {
            GameObject go = q.Peek();
            Cell cell = go.GetComponent<Cell>();
            q.Dequeue();
            Debug.Log(cell.pos);
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
                    
                    
                    if (!tempCell.isVisited)
                    {
                        tempCell.pos.z = pos.z;
                        //tempGo.GetComponent<Image>().color = Color.red;
                        tempCell.isVisited = true;
                        q.Enqueue(tempGo);
                    }
                }
                catch
                {
                     //Debug.Log("Horrible things happened!");
                }
            }
        }
    }
}
