using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Sprite normalMario;
    [SerializeField] private Sprite bigMario;
    [SerializeField] private Sprite fireMario;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bigMario;
        Player.Instance.OnStateChanged += Player_OnStateChanged;
    }

    private void Player_OnStateChanged(object sender, Player.OnStateChangedEventArgs e)
    {
        if (e.state == Player.State.Normal)
        {
            spriteRenderer.sprite = normalMario;
            spriteRenderer.size = new Vector2(normalMario.bounds.size.x, normalMario.bounds.size.y);

        }
        else if (e.state == Player.State.Big)
        {
            spriteRenderer.sprite = bigMario;
            spriteRenderer.size = new Vector2(bigMario.bounds.size.x, bigMario.bounds.size.y);

        }
        else if (e.state == Player.State.Fire)
        {
            spriteRenderer.sprite = fireMario;
        }
    }
}
