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

    private List<GameObject> bgList = new List<GameObject>();

    private GameObject currentBg;
    private float ballSpeed = 40;

    private void Start()
    {
        currentBg = Instantiate(bg);
        Destroy(currentBg.transform.Find("Ground7").gameObject);
        Destroy(currentBg.transform.Find("Ground6").gameObject);
        Destroy(currentBg.transform.Find("Ground5").gameObject);
        Destroy(currentBg.transform.Find("Ground4").gameObject);

        bgList.Add(currentBg);
    }

    private void FixedUpdate()
    {
        if (isScroll)
        {
            ScrollBG();
        }
        if (!isScroll)
        {
            foreach(GameObject bg in bgList)
            {
                bg.GetComponent<BGController>().isScroll = false;
            }
        }
    }

    public void ScrollBG()
    {
        if (isIwa)
        {
            currentBg.transform.Find("Iwa").gameObject.SetActive(true);
        }

        if (isGround)
        {
            currentBg.transform.Find("Ground0").gameObject.SetActive(true);
            currentBg.transform.Find("Ground1").gameObject.SetActive(true);
            currentBg.transform.Find("Ground2").gameObject.SetActive(true);
            currentBg.transform.Find("Ground3").gameObject.SetActive(true);
            currentBg.transform.Find("Ground4").gameObject.SetActive(true);
            currentBg.transform.Find("Ground5").gameObject.SetActive(true);
            currentBg.transform.Find("Ground6").gameObject.SetActive(true);
            currentBg.transform.Find("Ground7").gameObject.SetActive(true);
        }

        if (isReverse)
        {
            if (ballSpeed >0)
            {
                ballSpeed = -ballSpeed;
            }
            foreach (GameObject bg in bgList)
            {
                bg.GetComponent<BGController>().isScroll = true;
                bg.GetComponent<BGController>().scrollSpeed = ballSpeed;
            }

            if (currentBg.transform.position.x >= 0)
            {
                Vector3 currentPosition = currentBg.transform.position;
                currentBg = Instantiate(bg, new Vector3(currentPosition.x - 1080 * 2 - ballSpeed, 0, 0), Quaternion.identity);
                bgList.Add(currentBg);
            }

        }
        else
        {
            if (ballSpeed < 0)
            {
                ballSpeed = -ballSpeed;
            }

            foreach (GameObject bg in bgList)
            {
                bg.GetComponent<BGController>().isScroll = true;
                bg.GetComponent<BGController>().scrollSpeed = ballSpeed;
            }

            if (currentBg.transform.position.x <= 0)
            {
                Vector3 currentPosition = currentBg.transform.position;
                currentBg = Instantiate(bg, new Vector3(currentPosition.x + 1080 * 2 - ballSpeed, 0, 0), Quaternion.identity);
                bgList.Add(currentBg);
            }
        }

        GameObject deleteBg = null ;
        foreach (GameObject bg in bgList)
        {
            if (bg.transform.position.x < Screen.width * -3 || bg.transform.position.x > Screen.width * 3)
            {
                deleteBg = bg;
            }
        }
        if (deleteBg != null)
        {
            bgList.Remove(deleteBg);
        }
        Destroy(deleteBg);
    }
}