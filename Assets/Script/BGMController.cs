using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private void Start()
    {
        int BGMControllers = FindObjectsOfType<BGMController>().Length;

        if (BGMControllers > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
