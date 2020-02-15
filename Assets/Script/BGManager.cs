using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject bg;
    public bool isScroll;
    private GameObject currentBg;
    public float ballSpeed = 0;

    private void Start()
    {
        currentBg = Instantiate(bg);
        currentBg.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isScroll)
        {
            ScrollBG();
        }
    }

    public void ScrollBG()
    {
        currentBg.GetComponent<BGController>().isScroll = true;

        if (currentBg.transform.position.x <= 0)
        {
            Vector3 currentPosition = currentBg.transform.position;
            ballSpeed = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>().ballSpeed;
            currentBg = Instantiate(bg, new Vector3(currentPosition.x +1080*2 -ballSpeed, 0, 0), Quaternion.identity);
        }
    }
}
