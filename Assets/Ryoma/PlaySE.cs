using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PlaySE : MonoBehaviour
{
    public AudioClip se;
    void OnMouseEnter()
    {
        GetComponent<AudioSource>().PlayOneShot(se);
    }
    void Update()
    {

       
    }
}