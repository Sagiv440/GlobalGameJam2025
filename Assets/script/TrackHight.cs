using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackHight : MonoBehaviour
{
    [SerializeField] private Slider hightSlider;
    [SerializeField] private FloatReference MaxHight;
    [SerializeField] private FloatVariable curhight;

    private void Awake()
    {
        hightSlider = hightSlider == null ? GetComponent<Slider>() : hightSlider;
    }

    private void Update()
    {
        hightSlider.value = curhight.value/ MaxHight.variable.value;
    }
}
