using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject wallprefab;
    public GameObject cellPrefab;
    public GameObject cellHolder;
    public int rows = 13;
    public int columns = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i< rows;i++)
        {
            for (int j=0;j<columns;j++)
            {
                GameObject go = Instantiate(cellPrefab,Vector3.zero, Quaternion.identity);
                go.transform.SetParent(cellHolder.transform,false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
