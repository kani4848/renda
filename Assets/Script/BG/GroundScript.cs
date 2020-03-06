using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public bool isOpponent = false;
    public Renderer ground1;
    public Renderer ground2;
    // Start is called before the first frame update
    void Start()
    {
        ground1.enabled = false;
        ground2.enabled = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isOpponent)
        {
            if (gameObject.transform.position.x < 0)
            {
                ground1.enabled = true;
                ground2.enabled = true;
            }
        }
        else
        {
            if (gameObject.transform.position.x > 0)
            {
                ground1.enabled = true;
                ground2.enabled = true;
            }
        }
        
    }
}
