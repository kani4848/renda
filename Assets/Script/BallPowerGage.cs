using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPowerGage : MonoBehaviour
{
    public Vector2 gageSaize;
    public BattleManagerScript battleManager;
    
    void Update()
    {

        if(battleManager.state == BattleManagerScript.STATE.CHARGE && gameObject.GetComponent<RectTransform>().sizeDelta.y > 0)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta -= new Vector2(0,1);
        }
    }
}
