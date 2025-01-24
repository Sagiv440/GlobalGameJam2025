using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{ 
    public static GameManager get;
    [SerializeField] private FloatVariable PlayerDistance;
    [SerializeField] private float DeathTheashHold = 10f;
    [SerializeField] private UnityEvent OnGameOver;

    SmartSwitch threshHoldSwitch;

    private void Awake()
    {
        get = this;
    }

    public void GameOver()
    {
        OnGameOver.Invoke();
        Debug.Log("Game Over");
    }

    private void Update()
    {
        threshHoldSwitch.Update(PlayerDistance.value > DeathTheashHold);
        if (threshHoldSwitch.OnPress())
        {
            GameOver();
        }
    }
}
