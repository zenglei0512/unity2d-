using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class soilHidden : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 colliderPos = transform.TransformPoint(collision.transform.position);
            Vector3 transformPos = transform.TransformPoint(transform.position);
            if (colliderPos.y > transformPos.y)
            {
                transform.GetComponent<TilemapRenderer>().enabled = false;
                transform.GetComponent<TilemapCollider2D>().isTrigger = true;
            }
        }
    }
}
