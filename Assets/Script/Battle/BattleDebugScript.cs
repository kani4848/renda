using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDebugScript : MonoBehaviour
{
    public BattleManagerScript battleManager;
    public DontDestroyPara dontDestroyPara;
    public Text ballSpeed;
    public Text damage;
    public Text updateDamage;
    public Text catchFrame;
    public Text bgScrollSpeed;
    public Text preventDamage;
    public Text state;
    // Start is called before the first frame update
    void Start()
    {
        dontDestroyPara = GameObject.FindGameObjectWithTag("DontDestroyPara").gameObject.GetComponent<DontDestroyPara>();
    }

    // Update is called once per frame
    void Update()
    {
        ballSpeed.text = "BallSpeed:" + dontDestroyPara.ballSpeed;
        damage.text = "Damage:" + dontDestroyPara.damage;
        updateDamage.text = "UpdateDamage:" + dontDestroyPara.updateDamage;
        catchFrame.text ="Count:" + battleManager.count;
        bgScrollSpeed.text = "BgScrollSpeed:" + battleManager.bgScrollSpeed;
        //preventDamage.text = "PreventDamage:" + battleManager.catchGage.preventDamage;
        preventDamage.text = "PreventDamage:" + battleManager.catchGage.preventDamage;
        state.text = "State:" + battleManager.state;
    }
}
