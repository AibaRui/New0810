
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        FadeManager.Instance.LoadScene("Credit",1.0f);
    }
}
