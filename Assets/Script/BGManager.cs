using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject bg;
    public bool isScroll;

    private GameObject currentBg;
    public float ballSpeed = 0;

    private GameObject player;
    public BattleManagerScript battleManagerScript;

    private void Start()
    {
        currentBg = Instantiate(bg);
        currentBg.transform.Find("Iwa").gameObject.SetActive(false);
        player = currentBg.transform.Find("player").gameObject;
        battleManagerScript = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManagerScript>();
        battleManagerScript.charAnime = player.GetComponent<Animator>();
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

        if (currentBg.transform.position.x <= 0)
        {
            Vector3 currentPosition = currentBg.transform.position;
            ballSpeed = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManagerScript>().ballSpeed;
            currentBg = Instantiate(bg, new Vector3(currentPosition.x +1080*2 -ballSpeed, 0, 0), Quaternion.identity);
            currentBg.transform.Find("player").gameObject.SetActive(false);
        }
    }
}
