using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��s�����߂ɒǉ����Ă���

public class Goal : MonoBehaviour
{
    [SerializeField] string _rezaltName = "";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            SceneManager.LoadScene(_rezaltName);
        }

    }
}
