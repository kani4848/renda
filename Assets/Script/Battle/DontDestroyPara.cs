using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPara : MonoBehaviour
{
    public bool isLanded = false;
    public bool isOpponent = false;
    public bool isCatch = false;

    // Start is called before the first frame update
    void Start()
    {
        int gameObjectCount = FindObjectsOfType<BGMController>().Length;

        if (gameObjectCount > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
