using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン遷移を行うために追加している
using UnityEngine.UI;

public class Stage1Button : MonoBehaviour
{
    [SerializeField] GameObject g;
    public void OnClickStartButton()
    {
        g.SetActive(true);
        FadeManager.Instance.LoadScene("Stage1", 1.0f);
    }





}
