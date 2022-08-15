using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // ƒV[ƒ“‘JˆÚ‚ğs‚¤‚½‚ß‚É’Ç‰Á‚µ‚Ä‚¢‚é
using UnityEngine.UI;

public class Stage1Button : MonoBehaviour
{
    [SerializeField] GameObject g;
    public void OnClickStartButton()
    {
        g.SetActive(true);
        FadeManager.Instance.LoadScene("Stage1", 1.0f);
    }





}
