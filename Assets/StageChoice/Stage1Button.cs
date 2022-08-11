using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Button : MonoBehaviour
{

    public void OnClickStartButton()
    {

        FadeManager.Instance.LoadScene("Stage1", 1.0f);
    }

}
