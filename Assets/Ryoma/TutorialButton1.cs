using UnityEngine;



public class TutorialButton1 : MonoBehaviour
{
    public AudioClip se;
    private object audiosource;

    public void OnClickStartButton()
    {

        FadeManager.Instance.LoadScene("Tutorial",1.0f);
    }

}