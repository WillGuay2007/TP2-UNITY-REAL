using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("e");
        if (collision.gameObject.tag == "Player")
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.OnDeath();
        }
    }
}
