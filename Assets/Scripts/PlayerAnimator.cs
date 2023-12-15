using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string DEATH = "Death";

    [SerializeField]RuntimeAnimatorController normalAnimator;
    [SerializeField]RuntimeAnimatorController bigAnimator;
    [SerializeField]RuntimeAnimatorController fireAnimator;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Player.Instance.OnStateChanged += Player_OnStateChanged;
    }

    private void Player_OnStateChanged(object sender, Player.OnStateChangedEventArgs e)
    {
        if(e.state == Player.State.Normal)
        {
            animator.runtimeAnimatorController = normalAnimator;
        }
        else if(e.state == Player.State.Big)
        {
            animator.runtimeAnimatorController = bigAnimator;
        }
        else if (e.state == Player.State.Fire)
        {
            animator.runtimeAnimatorController = fireAnimator;
        }
        else
        {
            Destroy(Player.Instance.gameObject.GetComponent<BoxCollider2D>());
            Destroy(Player.Instance.gameObject.GetComponent<Rigidbody2D>());
            animator.SetTrigger(DEATH);
        }
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, Player.Instance.IsWalking());
        animator.SetBool(IS_JUMPING, Player.Instance.IsJumping());

    }
}
