using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{ 
    public enum gameState
    { 
        START,
        GAME,
        END
    }

    public gameState state { get; private set; }

    public static GameManager get;
    [SerializeField] private FloatVariable PlayerDistance;
    [SerializeField] private float DeathTheashHold = 10f;
    [SerializeField] private UnityEvent OnGameOver;
    [SerializeField] private UnityEvent OnGameWin;

    SmartSwitch threshHoldSwitch;

    private void Awake()
    {
        state = gameState.START;
        get = this;
    }

    public void GameOver()
    {
        state = gameState.END;
        Time.timeScale = 0f;
        OnGameOver.Invoke();
        Debug.Log("Game Over");
    }

    public void WinLevel()
    {
        state = gameState.END;
        Time.timeScale = 0f;
        OnGameWin.Invoke();
        Debug.Log("You Win");
    }

    private void Update()
    {
        if(Input.GetKeyDown("space") && state != gameState.END)
        {
            state = gameState.GAME;
        }
        threshHoldSwitch.Update(PlayerDistance.value > DeathTheashHold);
        if (threshHoldSwitch.OnPress())
        {
            GameOver();
        }
    }
}
