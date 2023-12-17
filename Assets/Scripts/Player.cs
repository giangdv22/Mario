using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] float jumpTimeLimit = 1.0f;
    [SerializeField] private LayerMask layerMask;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Normal,
        Big,
        Fire,
        Death
    }

    private State state;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    Vector2 inputVector;
    private bool isWalking;
    private bool isJumping;
    private float jumpStartTimer;
    private float groundStartTimer;
    private float groundStartTimerMax = 0.1f;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        state = State.Normal;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleMovement();
    }
    //private void FixedUpdate()
    //{
    //    if (rb != null)
    //    {
    //        float jumpDistance = jumpSpeed;
    //        if (isJumping)
    //        {
    //            jumpDistance = 0f;
    //        }

    //        rb.AddForce(new Vector2(0, inputVector.y * jumpDistance));
    //    }
    //}

    private void HandleMovement()
    {
        if (boxCollider != null)
        {
            inputVector = GameInput.Instance.GetMovement();

            float moveDistance = moveSpeed * Time.deltaTime;
            float rayCastY = boxCollider.size.y / 2.0f + 0.02f;

            bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastY, layerMask);

            //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, Vector2.down, boxCollider.size.y / 2.0f - 2.0f * Time.fixedDeltaTime, layerMask);
            if (isGrounded)
            {
                groundStartTimer -= Time.deltaTime;
                if (groundStartTimer <= 0 && inputVector.y != 0)
                {
                    isJumping = true;
                    jumpStartTimer = Time.time;
                }
            }
            if(!isGrounded)
            {
                groundStartTimer = groundStartTimerMax;
                bool cantJump = Physics2D.Raycast(transform.position, Vector2.up, rayCastY, layerMask);
                if (inputVector.y == 0 || cantJump)
                {
                    isJumping = false;
                }
            }
            ApplyJump();
            if (inputVector.x > 0)
            {
                transform.localScale = Vector3.one;
                isWalking = true;
            }
            else if (inputVector.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
            transform.position += new Vector3(moveDistance * inputVector.x, 0, 0);
        }
    }

    void ApplyJump()
    {
        if (isJumping)
        {
            float elapsedTime = Time.time - jumpStartTimer;
            if(elapsedTime < jumpTimeLimit)
            {
                float jumpFactor = 1.0f - (elapsedTime / jumpTimeLimit);
                float yVelocity = jumpForce * jumpFactor;
                transform.Translate(Vector2.up * yVelocity * Time.deltaTime);
            }
            else
            {
                isJumping = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactNormal = collision.contacts[0].normal;
        if (collision.gameObject.TryGetComponent<Contactable>(out Contactable contactable))
        {
            contactable.OnContact(contactNormal);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public void SetState(State state)
    {
        this.state = state;
        ChangeBoxCollider(state);
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
    }

    public State GetState()
    {
        return state;
    }

    void ChangeBoxCollider(State state)
    {
        float bigBoxColliderY = 0.32f;
        float normalBoxColliderY = 0.16f;
        if(state == State.Big || state == State.Fire)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, bigBoxColliderY);
        }
        else if(state == State.Normal)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, normalBoxColliderY);
        }
    }
}
