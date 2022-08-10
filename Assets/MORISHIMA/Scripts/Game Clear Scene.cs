using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ホーム画面に戻るボタンコンポーネント
/// </summary>
public class ReturnHome : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}