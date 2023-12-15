using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contactable : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private LayerMask layerMask;

    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        transform.Translate(moveDistance, 0, 0);
        hit = Physics2D.Raycast(transform.position, new Vector2(moveSpeed, 0),boxCollider.size.x / 2.0f, layerMask);
        if (hit.collider != null)
        {
            Debug.Log("true");
            moveSpeed = -moveSpeed;
        }
    }
    public virtual void OnContact(Vector2 contactNormal)
    {

    }
}
