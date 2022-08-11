using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{

 void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) { 
            SceneManager.LoadScene("Title");
        }
        
    }

}