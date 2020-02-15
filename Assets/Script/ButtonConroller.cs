using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonConroller : MonoBehaviour
{
    public bool pushed = false;
    public BattleManager battleManager;
    public AudioSource audioSource;
    public AudioClip chargeSE;
    public Text buttonText;

    private void Start()
    {
        buttonText.text = "連打しろ！";
    }

    private void Update()
    {
        if (battleManager.state == BattleManager.STATE.THROW)
        {
            gameObject.transform.position = new Vector3(0, -2000, 0);
        }

        if (battleManager.state == BattleManager.STATE.END)
        {
            gameObject.transform.position = new Vector3(0, -680, 0);
            buttonText.text = "再挑戦";
        }
    }

    public void OnClick()
    {
        if(battleManager.state == BattleManager.STATE.CHARGE || battleManager.state == BattleManager.STATE.IDLE)
        {
            pushed = true;
            battleManager.pushCount++;
            audioSource.PlayOneShot(chargeSE);
        }
 

        if (battleManager.state == BattleManager.STATE.END)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}