using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ƒV[ƒ“‘JˆÚ‚ğs‚¤‚½‚ß‚É’Ç‰Á‚µ‚Ä‚¢‚é

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
