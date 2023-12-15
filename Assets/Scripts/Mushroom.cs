using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Contactable
{
    public override void OnContact(Vector2 contactNormal)
    {
        Player.Instance.SetState(Player.State.Big);
        Destroy(gameObject);
    }
}
