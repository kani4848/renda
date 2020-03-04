using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public float scrollSpeed = 0;

    public bool isFirst = false;
    public bool isOpponent = false;
    public bool isScroll = false;
    public bool isGround = false;

    public GameObject iwa;
    public GameObject ground0;
    public GameObject ground1;
    public GameObject ground2;
    public GameObject ground3;
    public GameObject ground4;
    public GameObject ground5;
    public GameObject ground6;
    public GameObject ground7;

    private List<GameObject> groundList = new List<GameObject>();

    private void Start()
    {
        if (isFirst)
        {
            if (isOpponent)
            {
                Destroy(ground0);
                Destroy(ground1);
                Destroy(ground2);
                Destroy(ground3);
                groundList.Add(ground4);
                groundList.Add(ground5);
                groundList.Add(ground6);
                groundList.Add(ground7);
            }
            else
            {
                groundList.Add(ground0);
                groundList.Add(ground1);
                groundList.Add(ground2);
                groundList.Add(ground3);
                Destroy(ground4);
                Destroy(ground5);
                Destroy(ground6);
                Destroy(ground7);
            }
        }
        else
        {
            groundList.Add(ground0);
            groundList.Add(ground1);
            groundList.Add(ground2);
            groundList.Add(ground3);
            groundList.Add(ground4);
            groundList.Add(ground5);
            groundList.Add(ground6);
            groundList.Add(ground7);
        }
    }

    void FixedUpdate()
    {
        if (isOpponent)
        {
            foreach (GameObject ground in groundList)
            {
                ground.GetComponent<GroundScript>().isOpponent = true;
            }
        }
        if (isScroll)
        {
            gameObject.transform.position -= new Vector3(scrollSpeed,0,0);
        }
        if (isGround)
        {
            foreach (GameObject ground in groundList)
            {
                ground.SetActive(true);
            }
        }
        /*
        if (gameObject.transform.position.x < Screen.width*-3 || gameObject.transform.position.x > Screen.width * 3)
        {
           Destroy(gameObject);
        }
        */
    }
}