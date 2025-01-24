using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    public GameObject[] players;
    public Camera cam;

    public FloatVariable totalDistance;
    private float targetSize = 5f;
    public float minFrameSize = 5f;
    public float maxFrameSize = 8f;
    [Range(1f, 2f)]
    public float powerFactor = 1.5f;

    private void Awake()
    {
        totalDistance.value = 0f;
        cam = Camera.main;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void CalcualteDistance()
    {
        float min = Mathf.Infinity;
        float max = 0f;
        foreach(var player in players)
        {
            min = min > player.transform.position.y ? player.transform.position.y : min;
            max = max < player.transform.position.y ? player.transform.position.y : max;
        }
        totalDistance.value = (max - min)*0.5f;
    }

    private void Update()
    {
        CalcualteDistance();
        if(totalDistance.value * powerFactor < (minFrameSize))
        {
            targetSize = minFrameSize;
        }else if(totalDistance.value * powerFactor > maxFrameSize)
        {
            targetSize = maxFrameSize;
        }
        else
        {
            targetSize = totalDistance.value * powerFactor;
        }
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime);
    }
}
