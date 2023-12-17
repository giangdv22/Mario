using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    [SerializeField]private GameObject voidBlock;
    [SerializeField]private GameObject coin;
    [SerializeField]private GameObject flower;
    [SerializeField]private GameObject mushroom;
    [SerializeField] private Transform spawnSpot;

    public enum BlockType
    {
        Coin,
        PowerUp
    }
    [SerializeField] private BlockType type;
    private void Awake()
    {
        type = BlockType.Coin;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            if (contactNormal == Vector2.up)
            {
                Instantiate(voidBlock, transform.position, Quaternion.identity);
                if (type == BlockType.PowerUp)
                {
                    if (Player.Instance.GetState() == Player.State.Normal)
                    {
                        Instantiate(mushroom, spawnSpot.position, Quaternion.identity);
                    }
                    else if(Player.Instance.GetState() == Player.State.Big)
                    {
                        Instantiate(flower, spawnSpot.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(coin, spawnSpot.position, Quaternion.identity);
                    }
                }
                else if(type == BlockType.Coin)
                {
                    GameObject go = Instantiate(coin, spawnSpot.position, Quaternion.identity);
                    go.GetComponent<Coin>().CollectCoin();
                }
                Destroy(gameObject);

            }
        }
    }
}
