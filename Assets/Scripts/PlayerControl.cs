using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using ProcGenMusic;


public class PlayerControl : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public float slopeDetectionLimit;

    private bool m_IsGrounded;
    private Vector2 m_Movement;
    private Vector2 m_MovementInput;
    
    Animator m_Animator;
    CapsuleCollider2D m_Collider;
    GameManager m_GameManager;
    MusicGenerator m_MusicGenerator;
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;

    private float slopeDownAngle;
    private Vector2 slopeNormalPerpendicular;
    private bool isOnSlope;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<CapsuleCollider2D>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        // m_MusicGenerator = FindObjectOfType<MusicGenerator>();
        // m_MusicGenerator.SetVolume(-20f);

        m_GameManager = GameManager.Instance;
    }

    void Update()
    {
        if (m_IsGrounded && m_Movement != m_MovementInput) {
            m_Movement = m_MovementInput;
        } else if (!m_IsGrounded && m_Rigidbody.velocity.y < 0) {
            m_Animator.SetBool("IsJumping", false);
        }

        if (m_Movement.x != 0) {
            m_SpriteRenderer.flipX = m_Movement.x == -1;
        }

        if (m_Movement.x != 0) {
            m_Animator.SetBool("IsRunning", true);
            // m_Rigidbody.velocity = new Vector2(
            //     m_Movement.x * walkSpeed * -slopeNormalPerpendicular.x, 
            //     m_Movement.x * walkSpeed * -slopeNormalPerpendicular.y
            // );
            m_Rigidbody.AddForce(-slopeNormalPerpendicular * walkSpeed);
        } else {
            m_Animator.SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        SlopeCheck();
    }

    void OnMove(InputValue value)
    {
        m_MovementInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (m_IsGrounded) {
            m_Rigidbody.AddForce(Vector2.up * jumpForce);
            m_Animator.SetBool("IsJumping", true);
            // m_MusicGenerator.PlayNote(
            //     m_MusicGenerator.InstrumentSet,
            //     50.0f,
            //     "Bells",
            //     1,
            //     10
            // );
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground")) {
            m_IsGrounded = true;
            m_Animator.SetBool("IsInAir", false);
            // m_MusicGenerator.PlayNote(
            //     m_MusicGenerator.InstrumentSet,
            //     50.0f,
            //     "Bells",
            //     1,
            //     11
            // );
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Ground") {
            m_IsGrounded = false;
            m_Animator.SetBool("IsInAir", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) {
            m_GameManager.RemoveFromScene(other.gameObject);
            // m_MusicGenerator.PlayNote(
            //     m_MusicGenerator.InstrumentSet,
            //     5.0f,
            //     "Bells",
            //     30,
            //     9
            // );
        }
    }

    void PlayAudio(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(transform.position.x, transform.position.y, -5));
    }

    private void SlopeCheck()
    {
        Vector2 checkPosition = transform.position - new Vector3(0, m_Collider.size.y / 2);
        SlopeCheckVertical(checkPosition); 
    }

    private void SlopeCheckHorizontal(Vector2 checkPosition)
    {

    }

    private void SlopeCheckVertical(Vector2 checkPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeDetectionLimit);

        if (hit) {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
        }
    }
}
