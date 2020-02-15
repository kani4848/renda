using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    public bool isCharge = false;
    public bool isThrow = false;

    public BattleManager battleManager;

    void Update()
    {
        if(battleManager.state == BattleManager.STATE.THROW)
        {
            isThrow = true;
        }
        if (isThrow)
        {
            gameObject.transform.position -= new Vector3(battleManager.ballSpeed,0,0);
        }
    }
}
