using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerGage : MonoBehaviour
{
    public float gageHeight;
    public bool isCharge = false;

    void FixedUpdate()
    {
        if (isCharge)
        {
            if (gameObject.GetComponent<RectTransform>().sizeDelta.y > 0)
            {
                gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 1);
            }
        }
    }
}