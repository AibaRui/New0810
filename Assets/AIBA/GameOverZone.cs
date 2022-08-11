using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン遷移を行うために追加している

public class GameOverZone : MonoBehaviour
{
    /// <summary>ロードするシーン名</summary>
    [SerializeField] string m_sceneNameToBeLoaded = "SceneNameToBeLoaded";
    [SerializeField] GameObject _canvasGameOverFallArth;

    [SerializeField] string Animationname = "";
    [SerializeField] Animator _anim;


 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Debug.Log("IN");
            _canvasGameOverFallArth.SetActive(true);
            _anim = _anim.gameObject.GetComponent<Animator>();
            _anim.Play(Animationname);
        }

    }



    public void GameOverFallArth()
    {
        _canvasGameOverFallArth.SetActive(false);
        SceneManager.LoadScene(m_sceneNameToBeLoaded);
    }

}
