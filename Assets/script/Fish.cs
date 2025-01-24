using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float minSpeed = 5f;
    public float maxSpeed = 5f;



    public AnimationCurve flowCurve;

    IEnumerator MoveFish()
    {
        yield return null;
    }
}
