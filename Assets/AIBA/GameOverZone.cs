using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��s�����߂ɒǉ����Ă���

public class GameOverZone : MonoBehaviour
{
    /// <summary>���[�h����V�[����</summary>
    [SerializeField] string m_sceneNameToBeLoaded = "SceneNameToBeLoaded";



 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("IN");
        SceneManager.LoadScene(m_sceneNameToBeLoaded);
        }

    }



}
