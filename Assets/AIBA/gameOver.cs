using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン遷移を行うために追加している


public class gameOver : MonoBehaviour
{
    public void GameOverFallArth()
    {
        SceneManager.LoadScene("GAME OVER");
    }
}
