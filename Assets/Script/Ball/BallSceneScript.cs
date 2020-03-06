using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("DontDestroyPara").GetComponent<DontDestroyPara>().isOpponent)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
