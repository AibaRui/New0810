
using UnityEngine;


public class RESTART : MonoBehaviour
{
    public void OnClick()
    {
        FadeManager.Instance.LoadScene("StageChoice",1.0f);
    }
}