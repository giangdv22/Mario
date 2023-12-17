using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ContactFilter2D contactFilter;

    [SerializeField] private GameObject coinVisual;
    private Animator animator;
    private BoxCollider2D boxCollider;
    Collider2D[] hits = new Collider2D[5];
    private const string COLLECT_COIN = "CollectCoin";
    private void Awake()
    {
        animator = coinVisual.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (boxCollider != null)
        {
            boxCollider.OverlapCollider(contactFilter, hits);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] == null || hits[i].gameObject != Player.Instance.gameObject)
                {
                    continue;
                }
                CollectCoin();
                hits[i] = null;
            }
        }
    }

    public void CollectCoin()
    {

            animator.SetTrigger(COLLECT_COIN);
            Destroy(boxCollider);
            Destroy(gameObject, 1);

    }
}
