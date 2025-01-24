using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSponner : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 10f;



    public Transform destinaton;
    public List<GameObject> fishs;
    public AnimationCurve flowCurve;
    [SerializeField] private FloatVariable curHight;
    private GameObject fish;
    private SmartSwitch SetTrigger;

    private bool Sswitch = false;
    Vector3 pos1, pos2;

    private void Awake()
    {
        pos1 = this.transform.position;
        pos2 = destinaton.position;
        fish = Instantiate(fishs[UnityEngine.Random.Range(0, fishs.Count)], this.transform);
        Sswitch = !Sswitch;
    }

    private void Update()
    {
        SetTrigger.Update(Sswitch);
        if(SetTrigger.OnEvent())
        {
            Vector3 curPos = pos1;
            pos1 = pos2;
            pos2 = curPos;
            StartCoroutine(MoveFish(pos1, pos2,()=> { Sswitch = !Sswitch; }));
        }
    }


    IEnumerator MoveFish(Vector3 start, Vector3 end, Action action)
    {
        float t = 0;
        float speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        fish.transform.rotation = Quaternion.LookRotation((start - end).normalized, Vector3.up);
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            fish.transform.position = Vector3.LerpUnclamped(start, end, flowCurve.Evaluate(t));
            yield return null;
        }

        action.Invoke();
    }
}
