using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    public GameObject bg;
    public bool isScroll = false;
    public bool isReverse = false;
    public bool isIwa = false;
    public bool isGround = false;


    private GameObject currentBg;
    private float ballSpeed = 20;

    private void Start()
    {
        currentBg = Instantiate(bg);
        currentBg.transform.Find("Iwa").gameObject.SetActive(false);

        if (isReverse)
        {
            ballSpeed = - ballSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (isScroll)
        {
            ScrollBG();
        }
    }

    public void ScrollBG()
    {
        currentBg.GetComponent<BGController>().isScroll = true;
        currentBg.GetComponent<BGController>().scrollSpeed = ballSpeed;
        if (isIwa)
        {
            currentBg.transform.Find("Iwa").gameObject.SetActive(true);
        }
        if (isGround)
        {

        }

        if (currentBg.transform.position.x <= 0)
        {
            Vector3 currentPosition = currentBg.transform.position;
            currentBg = Instantiate(bg, new Vector3(currentPosition.x +1080*2 -ballSpeed, 0, 0), Quaternion.identity);
        }
    }
}