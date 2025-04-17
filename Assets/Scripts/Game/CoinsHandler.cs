using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            Player.OnCoinCollected();
            Destroy(gameObject);
        }
    }
}
