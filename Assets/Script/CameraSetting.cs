using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetting : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        float targetRatio = 9f/16f;
        float currentRatio = Screen.width * 1f / Screen.height;
        float ratio = targetRatio / currentRatio;

        if(targetRatio > currentRatio)
        {
            cam.orthographicSize = 960 * ratio;
        }
    }
}