﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceGauge : MonoBehaviour
{
    private const float GAUGE_MAX = 1.0f;

    private Image iceGauge; //アタッチされているオブジェクトのイメージコンポーネント
    private GeneratableIceCounter ctrl;
    [SerializeField]
    private float increaseQuantity; //ゲージの1秒当たりの増加量

    void Start()
    {
        iceGauge = GetComponent<Image>();
        iceGauge.fillAmount = 0;
    }

    void Update()
    {
        GaugeUp();
    }

    private void GaugeUp()
    {
        iceGauge.fillAmount += increaseQuantity * Time.deltaTime;
        if(iceGauge.fillAmount >= GAUGE_MAX)
        {
            ctrl.generatableIceQuantity++;
            iceGauge.fillAmount = 0;
        }
    }
}
