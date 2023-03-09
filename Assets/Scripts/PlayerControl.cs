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

    private bool m_IsGrounded;
    private Vector2 m_Movement;
    private Vector2 m_MovementInput;
    
    Animator m_Animator;
    GameManager m_GameManager;
    MusicGenerator m_MusicGenerator;
    Rigidbody2D m_Rigidbody;
    SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
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
            transform.Translate(new Vector3(m_Movement.x * walkSpeed * Time.deltaTime, 0, 0));
        } else {
            m_Animator.SetBool("IsRunning", false);
        }
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
}
