using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpEnemyMove : MonoBehaviour
{
    [SerializeField] float _dawnSpeed;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new(0, _dawnSpeed);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="GameOverZone"|| collision.gameObject.tag == "Baria")
        {
            Destroy(gameObject);
        }
    }

}
