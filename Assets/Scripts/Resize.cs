using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = this.gameObject.GetComponent<RectTransform>();
        this.gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(rt.sizeDelta.x/10,rt.sizeDelta.y/13);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
