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


        BFS();
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
                        tempGo.GetComponent<Image>().color = Color.red;
                        tempCell.isVisited = true;
                        q.Enqueue(tempGo);
                    }
                }
                catch
                {
                     Debug.Log("Horrible things happened!");
                }
            }
        }
    }
}
