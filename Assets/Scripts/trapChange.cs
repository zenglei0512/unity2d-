using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform parent = transform.parent;
            Animator animator = parent.GetComponent<Animator>();
            animator.SetBool("isChange", true);
        }
    }
}
