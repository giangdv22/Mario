using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
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
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    Vector2 inputVector;
    private bool isWalking;
    private bool isJumping;
    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        state = State.Normal;
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }
    private void FixedUpdate()
    {
        if (rb != null)
        {
            float jumpDistance = jumpSpeed;
            if (isJumping)
            {
                jumpDistance = 0f;
            }

            rb.AddForce(new Vector2(0, inputVector.y * jumpDistance));
        }
    }

    private void HandleMovement()
    {
        if (boxCollider != null)
        {
            inputVector = GameInput.Instance.GetMovement();

            float moveDistance = moveSpeed * Time.deltaTime;

            bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.size.y, layerMask);

            //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, Vector2.down, boxCollider.size.y / 2.0f - 2.0f * Time.fixedDeltaTime, layerMask);
            if (isGrounded)
            {
                isJumping = false;
            }
            else
            {
                isJumping = true;
            }
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(inputVector.x, 0), moveDistance, layerMask);
            if (hit.collider == null)
            {
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

            }
            else
            {
                Debug.Log(hit.collider.gameObject.transform.position);
                isWalking = false;
            }
            transform.position += new Vector3(moveDistance * inputVector.x, 0, 0);
            Debug.Log(state);
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
