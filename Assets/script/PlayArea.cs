using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] private FloatVariable curHight;
    [SerializeField] private FloatReference maxHight;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.get.state == GameManager.gameState.GAME)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if(curHight != null) curHight.value = transform.position.y;
            if (transform.position.y > maxHight.variable.value)
            {
                GameManager.get.WinLevel();
            }
        }
    }
}
