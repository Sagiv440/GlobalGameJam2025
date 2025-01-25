using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ScriptableObjects;

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
    [SerializeField] private SoundEffectSO MainMenuTheme;

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
        MainMenuTheme.Play();
    }

    public void GameOver()
    {
        if (state != gameState.END)
        {
            state = gameState.END;
            //Time.timeScale = 0f;
            StartCoroutine(DelayGameOver());
            Debug.Log("Game Over");
        }
    }

    public void WinLevel()
    {
        if (state != gameState.END)
        {
            state = gameState.END;
            //Time.timeScale = 0f;
            StartCoroutine(DelayEnd());
            Debug.Log("You Win");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("space") && state != gameState.END)
        {
            state = gameState.GAME;
            OnGameStart.Invoke();
        }
        StartSwitch.Update(state == gameState.GAME);
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
