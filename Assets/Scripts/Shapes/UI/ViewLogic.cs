using System;
using System.Collections;
using System.Collections.Generic;
using Shapes.Utils;
using UnityEngine;

public class ViewLogic : MonoBehaviour
{
    public RectTransform gamePanel;
    public RenderTexture viewTexture;
    public float viewScale = 10;
    public float padding;

    [NonSerialized] public Canvas view;
    [NonSerialized] public RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<Canvas>();
        rect = GetComponent<RectTransform>();

        viewTexture.width = (int)(GlobalVars.world.viewPort.width*viewScale);
        viewTexture.height = (int)(GlobalVars.world.viewPort.height*viewScale);

        resize();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (view != null) resize();
    }

    private void resize()
    {
        var r = GlobalVars.world.viewPort;
        float whr = r.width / r.height;

        var height = rect.sizeDelta.y - padding*2;
        var width = height * whr;
        gamePanel.sizeDelta = new Vector2(width, height);
    }
}
