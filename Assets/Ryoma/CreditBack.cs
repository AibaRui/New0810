using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

    }
}
