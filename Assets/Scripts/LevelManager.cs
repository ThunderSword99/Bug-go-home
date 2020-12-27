using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject rowLineHolder;
    public GameObject columnLineLeftHolder;
    public GameObject columnLineRightHolder;
    public GameObject levelHolder;
    public GameObject levelPrefab;
    public GameObject rowLine;
    public GameObject columnLine;
    public int numberOfLevels = 999;

    private List<GameObject> StageList = new List<GameObject>();

    void LoadLevel()
    {
        for (int i=1;i<=numberOfLevels;i++)
        {
            GameObject go = Instantiate(levelPrefab, transform.position, Quaternion.identity);
            go.GetComponent<Stage>().SetLevel(i);
            go.transform.SetParent(levelHolder.transform,false);
            StageList.Add(go);
            if (i%4==1)
            {
                GameObject newRowLine = Instantiate(rowLine, transform.position, Quaternion.identity);
                newRowLine.transform.SetParent(rowLineHolder.transform,false);
            }
            if (i%8==0)
            {
                GameObject newColumnLeftLine = Instantiate(columnLine, transform.position, Quaternion.identity);
                newColumnLeftLine.transform.SetParent(columnLineLeftHolder.transform,false);

                GameObject newColumnRightLine = Instantiate(columnLine, transform.position, Quaternion.identity);
                newColumnRightLine.transform.SetParent(columnLineRightHolder.transform,false);
            }
            
        }
        
    }

    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        
    }
}
