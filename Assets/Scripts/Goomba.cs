// Ignore Spelling: Goomba

using UnityEngine;

public class Goomba : Contactable
{
    [SerializeField] private GameObject goombaDeath;

    private float goombaDeathTimer = 1f;

    public override void OnContact(Vector2 contactNormal)
    {
        if (contactNormal == Vector2.up)
        {
            GameObject go = Instantiate(goombaDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(go, goombaDeathTimer);
        }
        else
        {
            Player.Instance.SetState(Player.State.Death);
        }
    }
}
