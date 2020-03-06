using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour
{
    public GameObject bg;
    public bool isScroll = false;
    public bool isOpponent = false;
    public bool isIwa = false;
    public bool isGround = false;

    private List<GameObject> bgList = new List<GameObject>();

    private GameObject currentBg;
    public float scrollSpeed = 0;

    private void Start()
    {
        currentBg = Instantiate(bg);
        currentBg.GetComponent<BGController>().isFirst = true;
        if (!isOpponent)
        {

            scrollSpeed = -scrollSpeed;
        }

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
            currentBg.GetComponent<BGController>().isGround = true;
        }

        foreach (GameObject bg in bgList)
        {
            bg.GetComponent<BGController>().isScroll = true;
            bg.GetComponent<BGController>().scrollSpeed = scrollSpeed;
        }

        if (isOpponent)
        {
            currentBg.GetComponent<BGController>().isOpponent = true;
            if (currentBg.transform.position.x <= 0)
            {
                Vector3 currentPosition = currentBg.transform.position;
                currentBg = Instantiate(bg, new Vector3(currentPosition.x + 1080 * 2 - scrollSpeed, 0, 0), Quaternion.identity);
                bgList.Add(currentBg);
            }
        }
        else
        {
            if (currentBg.transform.position.x >= 0)
            {
                Vector3 currentPosition = currentBg.transform.position;
                currentBg = Instantiate(bg, new Vector3(currentPosition.x - 1080 * 2 - scrollSpeed, 0, 0), Quaternion.identity);
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