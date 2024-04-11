using System.Collections;
using System.Collections.Generic;
using Shapes.Utils;
using UnityEngine;

public class SyncSettings : MonoBehaviour
{
    public Camera parent;

    private Camera self;

    void Start()
    {
        self = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        self.orthographicSize = parent.orthographicSize;
        self.aspect = parent.aspect;
        self.fieldOfView = parent.fieldOfView;
        self.orthographic = GlobalVars.isOrthographicView;
    }
}
