using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contactable : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private LayerMask layerMask;

    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    private float raycastSize;
    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        raycastSize = boxCollider.size.x / 2.0f +0.01f;
    }
    protected virtual void Update()
    {
        hit = Physics2D.Raycast(transform.position, new Vector2(moveSpeed, 0), raycastSize);
        float moveDistance = moveSpeed * Time.deltaTime;
        if (hit.collider != null)
        {
            Debug.Log("true");
            moveSpeed = -moveSpeed;
        }
            transform.Translate(moveDistance, 0, 0);
    }

    public virtual void OnContact(Vector2 contactNormal)
    {

    }
}
