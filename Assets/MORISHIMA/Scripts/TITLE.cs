
using UnityEngine;


public class TITLE : MonoBehaviour
{

    public void OnClick()
    {
        FadeManager.Instance.LoadScene("Title",1.0f);
    }

}
