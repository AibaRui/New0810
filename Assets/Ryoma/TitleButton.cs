using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Rocet");
    }

}