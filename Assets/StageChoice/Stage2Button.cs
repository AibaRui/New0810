using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Button : MonoBehaviour
{

    public void OnClickStartButton()
    {

        FadeManager.Instance.LoadScene("Stage2", 1.0f);
    }

}
