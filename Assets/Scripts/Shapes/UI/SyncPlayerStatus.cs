using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Shapes.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class SyncPlayerStatus : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI graze;
    public TextMeshProUGUI miss;
    public TextMeshProUGUI power;
    public TextMeshProUGUI bomb;

    public Slider health;
    public TextMeshProUGUI healthTex;

    public Color healLowColor;
    public Color healMaxColor;

    private float scoreLerp;
    private float healthLerp;

    private Image fillImg;

    // Start is called before the first frame update
    void Start()
    {
        scoreLerp = 0;
        healthLerp = GlobalVars.player.health;
        fillImg = health.fillRect.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLerp = Mathf.Lerp(scoreLerp, GlobalVars.player.score, Time.deltaTime*6.5f);
        healthLerp = Mathf.Lerp(healthLerp, GlobalVars.player.health, Time.deltaTime*6.5f);

        score.text = Mathf.RoundToInt(scoreLerp).ToString().PadLeft(11, '0');
        graze.text = GlobalVars.player.graze.ToString();
        miss.text = GlobalVars.player.miss.ToString();
        power.text = $"{GlobalVars.player.power:f2}";
        bomb.text = string.Concat(Enumerable.Repeat("\u25bc", GlobalVars.player.bombs));

        health.value = healthLerp / GlobalVars.player.maxHealth;
        fillImg.color = Color.Lerp(healLowColor, healMaxColor, health.value);
        healthTex.text = Mathf.RoundToInt(healthLerp).ToString();
    }
}
