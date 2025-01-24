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

    [SerializeField] private float endDelayTime = 2f;
    [SerializeField] private UnityEvent OnGameStart;
    [SerializeField] private UnityEvent OnGameOver;
    [SerializeField] private UnityEvent OnGameWin;

    SmartSwitch StartSwitch;
    SmartSwitch threshHoldSwitch;

    private void Awake()
    {
        state = gameState.START;
        get = this;
    }

    public void GameOver()
    {
        state = gameState.END;
        //Time.timeScale = 0f;
        StartCoroutine(DelayGameOver());
        Debug.Log("Game Over");
    }

    public void WinLevel()
    {
        state = gameState.END;
        //Time.timeScale = 0f;
        StartCoroutine(DelayEnd());
        Debug.Log("You Win");
    }

    private void Update()
    {
        if(Input.GetKeyDown("space") && state != gameState.END)
        {
            state = gameState.GAME;
            OnGameStart.Invoke();
        }
        StartSwitch.Update(state == gameState.GAME);
        threshHoldSwitch.Update(PlayerDistance.value > DeathTheashHold);
        if (threshHoldSwitch.OnPress())
        {
            GameOver();
        }
    }

    IEnumerator DelayEnd()
    {
        yield return new WaitForSeconds(endDelayTime);
        OnGameWin.Invoke();
    }
    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(endDelayTime);
        OnGameOver.Invoke();
    }
}
