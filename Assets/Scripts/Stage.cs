using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    private int currentLevel;
    private int star=0;
    private bool isLock=true;

    public GameObject TextGo;
    public GameObject LockGo;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    

    public void UnlockStage()
    {
        isLock = false;
        LockGo.active = false;
    }

    public void SetLevel(int i)
    {
        currentLevel = i;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public void SetStar(int i)
    {
        switch(i)
        {
            case 1:
                Star1.active = true;
                Star2.active = false;
                Star3.active = false;
                break;
            case 2:
                Star1.active = true;
                Star2.active = true;
                Star3.active = false;
                break;
            case 3:
                Star1.active = true;
                Star2.active = true;
                Star3.active = true;
                break;
            default:
                break;
        }
    }


    void Start()
    {
       TextGo.GetComponent<Text>().text = currentLevel.ToString();
       if (isLock)
       {
            LockGo.active = true;
            Star1.active = false;
            Star2.active = false;
            Star3.active = false;
       }
    }

}
