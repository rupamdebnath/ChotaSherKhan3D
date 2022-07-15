using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public TextMeshProUGUI winLoseText;
    public GameObject gameOverUI;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShowGameMenuAtWin()
    {
        winLoseText.SetText("Yayy! You have reached home!");
        gameOverUI.SetActive(true);        

    }
    public void ShowGameMenuAtDeath()
    {
        SoundManager.Instance.PlaySound(4);
        winLoseText.SetText("Alas! Sher Khan is dead! Try again");
        gameOverUI.SetActive(true);        
    }
}
