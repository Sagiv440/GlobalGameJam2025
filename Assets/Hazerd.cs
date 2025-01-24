using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazerd : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.get.GameOver();
        }
    }
}
