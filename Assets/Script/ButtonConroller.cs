using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonConroller : MonoBehaviour
{
    public bool pushed = false;
    public BattleManagerScript battleManager;
    public AudioSource audioSource;
    public AudioClip chargeSE;
    public Text buttonText;

    private void Start()
    {
        buttonText.text = "連打しろ！";
    }

    private void Update()
    {
        if (battleManager.state == BattleManagerScript.STATE.THROW)
        {
            gameObject.transform.position = new Vector3(0, -2000, 0);
        }

        if (battleManager.state == BattleManagerScript.STATE.END)
        {
            gameObject.transform.position = new Vector3(0, -680, 0);
            buttonText.text = "再挑戦";
        }
    }

    public void OnClick()
    {
        if(battleManager.state == BattleManagerScript.STATE.CHARGE || battleManager.state == BattleManagerScript.STATE.IDLE)
        {
            pushed = true;
            battleManager.pushCount++;
            audioSource.PlayOneShot(chargeSE);
        }
 

        if (battleManager.state == BattleManagerScript.STATE.END)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}