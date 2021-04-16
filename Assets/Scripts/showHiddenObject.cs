using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showHiddenObject : MonoBehaviour
{
    public GameObject item;
    public float showTime = 0.5f;
    float dt = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt += Time.deltaTime;
        if (dt >= showTime)
        {
            dt = 0.0f;
            item.SetActive(!item.activeInHierarchy);
        }
    }
}
