using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapMove : MonoBehaviour
{
    public float moveTime = 5f;
    public Vector3 dirVector;
    bool isMove = false;
    Transform parent;

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            parent.localPosition = Vector3.MoveTowards(parent.localPosition, dirVector, moveTime * Time.deltaTime);
        }
    }

    public bool IsMove {
        get { return isMove; }
        set { isMove = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            parent = transform.parent;
            isMove = true;
            //StartCoroutine(destroyParent());
        }
    }

    IEnumerator destroyParent()
    {
        yield return new WaitForSeconds(moveTime);
        GameObject.Destroy(parent.gameObject);
    }
}
