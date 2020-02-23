using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public GameObject iwa;
    public bool isScroll = false;
    public bool isCrush = false;
    public float ballSpeed = 0;
    public AudioSource crushSE;

    void FixedUpdate()
    {
        if (isScroll)
        {
            ballSpeed = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>().ballSpeed;
            gameObject.transform.position -= new Vector3(ballSpeed,0,0);
            
            if (gameObject.transform.position.x < 0 + 300 && !isCrush)
            {
                isCrush = true;
                crushSE.Play();
                iwa.GetComponent<Animator>().SetTrigger("isCrush");
            }
        }


        if (gameObject.transform.position.x < -1080*3)
        {
            Destroy(gameObject);
        }
    }
}
