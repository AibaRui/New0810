using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    [SerializeField] GameObject[] meteorite = new GameObject[0];

    [SerializeField] float _lateTime = 5;
    public float _countTime = 0;

    [SerializeField] Transform[] _pos = new Transform[0];

    void Start()
    {

    }


    void Update()
    {
        _countTime += Time.deltaTime;


        if (_countTime > _lateTime)
        {
            _countTime = 0;
            var r = Random.Range(0, meteorite.Length);
            var go = Instantiate(meteorite[r]);
            var rr = Random.Range(0, _pos.Length);
            go.transform.position = _pos[rr].position;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
