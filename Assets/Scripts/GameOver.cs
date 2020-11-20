using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;
    
    public Text roundsText;
    void OnEnable() {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry(){
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu(){
        sceneFader.FadeTo(menuSceneName);   
    }
}
