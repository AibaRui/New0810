using System.Collections;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    public AudioClip se;

    void OnMouseEnter()
    {
        GetComponent<AudioSource>().PlayOneShot(se);
    }
    public void OnClickStartButton2()
  {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
     }
}


