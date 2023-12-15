using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Contactable
{
    [SerializeField] private Sprite turtleSquash;

    public enum State
    {
        ALive,
        Squash
    }

    protected override void Update()
    {
        base.Update();
        if(moveSpeed < 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void OnContact(Vector2 contactNormal)
    {
        if(contactNormal == Vector2.up)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = turtleSquash;
            Destroy(gameObject.GetComponent<Animator>());
        }

        else
        {
            Player.Instance.SetState(Player.State.Death);
        }
    }
}
