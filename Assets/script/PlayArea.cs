using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] private FloatVariable curHight;
    [SerializeField] private FloatReference maxHight;
    private bool startg;
    private float gYpos;
    private void Start()
    {
        gYpos = transform.position.y;
        if (curHight != null) curHight.value = 0;
    }

    public void TriggerStartGame()
    {
        startg = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startg && GameManager.get.state == GameManager.gameState.GAME)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if(curHight != null) curHight.value = transform.position.y - gYpos;
            if (transform.position.y > maxHight.variable.value)
            {
                GameManager.get.WinLevel();
                startg = false;
            }
        }
    }
}
