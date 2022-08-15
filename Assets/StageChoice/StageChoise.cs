using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �V�[���J�ڂ��s�����߂ɒǉ����Ă���

public class StageChoise : MonoBehaviour
{
    [SerializeField] string name = "";
    [SerializeField] float time = 5;
    float timeCount = 0;
    bool Go = false;


    private void Update()
    {
        if (Go)
        {
            timeCount += Time.deltaTime;

            if (timeCount > time)
            {
                 SceneManager.LoadScene(name);
            }
        }

    }


}
