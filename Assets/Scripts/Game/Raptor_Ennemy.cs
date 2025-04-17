using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Raptor_Ennemy : MonoBehaviour
{
    [SerializeField] private float m_EnnemySpeed;
    private Rigidbody2D m_Rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
        Player.OnDeath();
        }
    }

    private void Move()
    {
        Vector3 newVelocity = m_Rigidbody2D.velocity;
        newVelocity.x = -m_EnnemySpeed; //Le "-" est pour aller a gauche au lieux de droite.
        m_Rigidbody2D.velocity = newVelocity;
    }
}
