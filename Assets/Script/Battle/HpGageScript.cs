using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "KO")
        {
            Destroy(gameObject);
        }
    }
}
