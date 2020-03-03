using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBallButtonController : MonoBehaviour
{
    public void OnClick()
    {
        transform.root.GetComponent<Animator>().SetTrigger("isCatch");
        
    }
}
