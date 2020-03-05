using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGageScript : MonoBehaviour
{
    void Start()
    {
        int gameObjectCount = FindObjectsOfType<HpGageScript>().Length;

        if (gameObjectCount > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
