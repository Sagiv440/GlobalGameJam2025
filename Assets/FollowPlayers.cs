using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayers : MonoBehaviour
{
    [SerializeField] private Transform startTarget;
    [SerializeField] private Transform camTarget;
    public GameObject[] players;
    public Camera cam;

    public FloatVariable totalDistance;
    private float targetSize = 5f;
    public float acceloration = 10f;
    public float minFrameSize = 5f;
    public float maxFrameSize = 8f;
    [Range(1f, 2f)]
    public float powerFactor = 1.5f;

    [Header("Start Sequence")]
    [SerializeField] private float startTime = 1f;
    [SerializeField] private float lerpTime = 1f;

    private SmartSwitch gameSwitch;
    private bool follow = false;

    private void Awake()
    {
        totalDistance.value = 0f;
        cam = Camera.main;
        follow = false;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    private void CalcualteDistance()
    {
        float minY = Mathf.Infinity;
        float maxY = 0f;

        float minX = Mathf.Infinity;
        float maxX = 0f;

        foreach (var player in players)
        {
            minY = minY > player.transform.position.y ? player.transform.position.y : minY;
            maxY = maxY < player.transform.position.y ? player.transform.position.y : maxY;

            minX = minX > player.transform.position.x ? player.transform.position.x : minX;
            maxX = maxX < player.transform.position.x ? player.transform.position.x : maxX;
        }
        totalDistance.value = (maxY - minY)*0.5f;
    }

    private void Update()
    {
        gameSwitch.Update(GameManager.get.state == GameManager.gameState.GAME);
        if (gameSwitch.OnPress())
        {
            StartCoroutine(Startfollow());
        }
        if (gameSwitch.OnHold())
        {
            CalcualteDistance();
            if (totalDistance.value * powerFactor < (minFrameSize))
            {
                targetSize = minFrameSize;
            }
            else if (totalDistance.value * powerFactor > maxFrameSize)
            {
                targetSize = maxFrameSize;
            }
            else
            {
                targetSize = totalDistance.value * powerFactor;
            }
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime);

            if (follow)
                this.transform.position = camTarget.transform.position;
        }
    }
    IEnumerator Startfollow()
    {
        float t = 0f;
        yield return new WaitForSeconds(startTime);

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            this.transform.position = Vector3.Lerp(startTarget.position, camTarget.position, t / lerpTime);
            yield return null;
        }
        follow = true;
    }
}
