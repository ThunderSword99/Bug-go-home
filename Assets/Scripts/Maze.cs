using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;
    public int rows = 13;
    public int columns = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i< rows;i++)
        {
            for (int j=0;j<columns;j++)
            {
                Instantiate(floor,new Vector3((j * Screen.width)/rows, 0, (i* Screen.height)/columns), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
