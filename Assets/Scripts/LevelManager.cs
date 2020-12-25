using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelPrefab;
    public int numberOfLevels = 999;

    void LoadLevel()
    {
        for (int i=2;i<=numberOfLevels;i++)
        {
            GameObject go = Instantiate(levelPrefab, transform.position, Quaternion.identity);
            go.GetComponent<Stage>().SetLevel(i);
            go.transform.SetParent(levelHolder.transform,false);
                
            
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
