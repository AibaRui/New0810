using UnityEngine;


public class TitleButton : MonoBehaviour
{
   

public void OnClickStartButton()
    {
        FadeManager.Instance.LoadScene("StageChoice",1.0f);
    }

}
