using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool isGameOver = collision.gameObject.GetComponent<playController>().isGameOver;
            if (isGameOver)
            {
                return;
            }
            collision.gameObject.GetComponent<playController>().isGameOver = true;
            collision.gameObject.GetComponent<Animator>().SetTrigger("isDeath");
        }
    }
}
