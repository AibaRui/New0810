using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��s�����߂ɒǉ����Ă���


public class gameOver : MonoBehaviour
{
    public void GameOverFallArth()
    {
        SceneManager.LoadScene("GAME OVER");
    }
}
