using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetting : MonoBehaviour
{
    private void Awake()
    {
        /*
        Camera cam = GetComponent<Camera>();
        if (Screen.width < 1080)
        {
            cam.orthographicSize = 
        }
        */
        Camera cam = GetComponent<Camera>();

        float targetRatio = 9f/16f;

        float currentRatio = Screen.width * 1f / Screen.height;

        float ratio = targetRatio / currentRatio;
        /*
        float rectX = (1.0f - ratio) / 2f;
        cam.rect = new Rect(rectX, 0f, ratio, 1f);
        */
        if(targetRatio > currentRatio)
        {
            cam.orthographicSize = 960 * ratio;
        }
    }
    private void Update()
    {
        Camera cam = GetComponent<Camera>();

        float targetRatio = 9f / 16f;

        float currentRatio = Screen.width * 1f / Screen.height;

        float ratio = targetRatio / currentRatio;
        /*
        float rectX = (1.0f - ratio) / 2f;
        cam.rect = new Rect(rectX, 0f, ratio, 1f);
        */
        if (targetRatio > currentRatio)
        {
            cam.orthographicSize = 960 * ratio;
        }
    }
}