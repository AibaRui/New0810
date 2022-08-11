using UnityEngine;


public class TitleButton : MonoBehaviour
{
   

public void OnClickStartButton()
    {
        FadeManager.Instance.LoadScene("Rocet",1.0f);
    }

}
