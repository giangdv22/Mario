using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private const string BREAKABLE = "Breakable";

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            if (contactNormal == Vector2.up)
            {
                Debug.Log(contactNormal);
                GetComponent<Animator>().SetTrigger(BREAKABLE);
            }
        }
    }
}
