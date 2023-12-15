using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    [SerializeField]private GameObject voidBlock;
    [SerializeField]private GameObject coin;
    [SerializeField]private GameObject mushroom;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            if (contactNormal == Vector2.up)
            {
                Instantiate(mushroom, new Vector3(0, 0.16f, 0), Quaternion.identity, transform);
                Instantiate(voidBlock, transform.position, Quaternion.identity);
                Destroy(gameObject);

            }
        }
    }
}
