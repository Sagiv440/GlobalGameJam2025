using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public float maxHight = 10f;
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.get.state == GameManager.gameState.GAME)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y > maxHight)
            {
                GameManager.get.WinLevel();
            }
        }
    }
}
