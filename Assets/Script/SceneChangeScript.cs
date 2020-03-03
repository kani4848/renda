using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeScript : MonoBehaviour
{
    public string sceneName;
    private float nextTIme = 1.5f;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ball")
        {
            nextTIme += Time.time;
        }
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "Ball" && nextTIme < Time.time)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void SceneChange()
    {    
        SceneManager.LoadScene(sceneName);
    }
}
