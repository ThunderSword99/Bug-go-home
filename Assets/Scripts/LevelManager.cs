using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    private int currentLevelSelected;
    private int totalStar=0;

    private void SetTotalStarText()
    {
        GameObject.FindGameObjectWithTag("TotalStarText").GetComponent<TMP_Text>().text = totalStar.ToString();
    }

    void LoadLevel()
    {
        for (int i=2;i<=numberOfLevels;i++)
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

    private void GetRandomCurrentLevel()
    {
        currentLevelSelected = Random.Range(1,999);
    }

    private void UnlockStages()
    {
        foreach (GameObject go in StageList)
        {
            Stage newStage = go.GetComponent<Stage>();
            if (newStage.getCurrentLevel()<=currentLevelSelected)
            {
                newStage.UnlockStage();
                int star = Random.Range(1,4);
                newStage.GetComponent<Stage>().SetStar(star);
                totalStar+= star;
            }
        }
    }

    void Awake()
    {
        GetRandomCurrentLevel();
    }

    void Start()
    {
        LoadLevel();
        UnlockStages();
        SetTotalStarText();
    }

    void Update()
    {
        
    }
}
