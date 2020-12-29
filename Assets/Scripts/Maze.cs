using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                go.GetComponent<Cell>().pos = new Vector3(i,j,0);
                listCell[i,j] = go;
            }
        }

    }

    private void BFS()
    {
        Queue <GameObject> q = new Queue<GameObject>();
        q.Enqueue(listCell[0,0]);

    }
}
