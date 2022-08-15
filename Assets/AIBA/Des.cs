using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des : MonoBehaviour
{
    float c = 0;

    [SerializeField] GameObject g;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        c += Time.deltaTime;

        if(c>8)
        {
            g.SetActive(true);
            Destroy(gameObject);
        }
        if (Input.anyKey)
        {
            Destroy(gameObject);
            g.SetActive(true);
        }

    }
}
