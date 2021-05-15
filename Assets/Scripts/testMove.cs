using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour
{
    float h = 0, v = 0, speed = 5f;

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if(h!=0 || v!=0)
        {
            Debug.Log("移动中！");
            transform.Translate(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0);
        }
    }
}
