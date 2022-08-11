
using UnityEngine;


public class RESTART : MonoBehaviour
{
    public void OnClick()
    {
        FadeManager.Instance.LoadScene("Rocet",1.0f);
    }
}