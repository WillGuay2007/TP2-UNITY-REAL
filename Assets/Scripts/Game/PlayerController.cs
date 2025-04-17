using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour // J'ai renommé le script pour PlayerController 
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpForce;
    [SerializeField] private float m_DashForce;
    private int m_Coins;
    private bool m_WasDashing = false;
    private Rigidbody2D m_Rigidbody;
    private bool m_GameStarted = false;
    private int m_JumpCounter;
    private Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameStarted && Input.GetKeyDown(KeyCode.E)) m_GameStarted = true; //Appuyer sur espace pour commencer

        if (m_GameStarted)
        {
            movePlayer();
            jumpHandler();
            DashDown();
        }
    }

    private void movePlayer()
    {
        Vector3 newVelocity = m_Rigidbody.velocity;
        newVelocity.x = m_Speed;
        m_Rigidbody.velocity = newVelocity;
        m_Animator.SetBool("IsRunning", true);
    }

    private void jumpHandler()
    {
        if (m_JumpCounter == 0 && Input.GetKeyDown(KeyCode.Space)) // Premier saut
        {
            CancelYForce();
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
            m_JumpCounter += 1;
        }
        else if (m_JumpCounter == 1 && Input.GetKeyDown(KeyCode.Space)) // Deuxieme saut
        {
            m_JumpCounter += 1;
            CancelYForce();
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            m_JumpCounter = 0;
        }
    }

    private void CancelYForce()
    {
        Vector3 PlrVelocityCancelYForce = m_Rigidbody.velocity;
        PlrVelocityCancelYForce.y = 0;
        m_Rigidbody.velocity = PlrVelocityCancelYForce;
    }

    private void DashDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            m_WasDashing = true;
            CancelYForce();
            m_Rigidbody.AddForce(Vector2.down * m_DashForce, ForceMode2D.Impulse);
        }
        else if (m_WasDashing)
        {
            //Ca sert a ce que si le joueur etais en dash la frame précédente et qu'il ne dash pas cette frame, il ne va pas continuer a tomber avec la force du dash précédent.
            m_WasDashing = false;
            CancelYForce();
        }
    }

    public void OnDeath()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public void OnCoinCollected()
    {
        m_Coins += 1;
    }

}

