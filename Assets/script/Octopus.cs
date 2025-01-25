using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Octopus : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayArea pArea;
    [SerializeField] private UnityEvent OnSnees;
    [Range(0, 1)]
    [SerializeField] float complit = 0.7f;
    private SmartSwitch gameSwitch;
    private void Update()
    {
        gameSwitch.Update(GameManager.get.state == GameManager.gameState.GAME);
        if (gameSwitch.OnPress())
        {
            anim.Play("start");
            StartCoroutine(OnAnimEnd(anim, complit, () => { OnSnees.Invoke(); }));
            StartCoroutine(OnAnimEnd(anim, 1f, ()=> { pArea.TriggerStartGame(); }));
        }
    }

    IEnumerator OnAnimEnd(Animator anim, float complit, Action action)
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < complit)
        {
            yield return null;
        }
        action.Invoke();
    }
}
