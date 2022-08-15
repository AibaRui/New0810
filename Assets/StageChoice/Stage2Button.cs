using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Button : MonoBehaviour
{
    [SerializeField] GameObject g;
    public void OnClickStartButton()
    {
        g.SetActive(true);
        FadeManager.Instance.LoadScene("Stage2", 1.0f);
    }

}
