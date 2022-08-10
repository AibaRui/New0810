using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    [Header("移動の速さ")]
    [SerializeField] float _speedJet = 1;
    [SerializeField] float _speedDobleJet = 1;


    [Header("傾き角度")]
    [SerializeField] float _rotation = 0.001f;

    [Header("速度制限")]
    [SerializeField] float _speedLimitY = 5;
    [SerializeField] float _speedLimitX = 4;

    [SerializeField] float _responTime = 2;


    [SerializeField] GameObject _upPos;

    float _hitPosY = 0;

    bool _isRestart;
    bool _isDamage;
    bool _isJet1;
    bool _isJet2;

    Quaternion nowRotation;

    Rigidbody2D _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDamage)
        {
            _isDamage = false;
            transform.position = new(0, _hitPosY, 0);       //ポジションを真ん中に移動
            transform.eulerAngles = new(0, 0, 0);           //角度を０に
            _rb.freezeRotation = true;                      //回転をしない                           
            _rb.velocity = new Vector2(0, 0);               //速度を0
            _rb.isKinematic = true;                        //動かなくする
            _isRestart = true;
            StartCoroutine(Hit());
        }

        if (!_isRestart)
        {
            Player1JetMove();
            Player2JetMove();
            nowRotation = transform.rotation;
        }
        Fi();

    }
    /// <summary>復帰までの時間</summary>
    IEnumerator Hit()
    {
        yield return new WaitForSeconds(_responTime);
        _rb.isKinematic = false;
        _rb.freezeRotation = false;
        _isRestart = false;
    }


    void Fi()
    {

        if (!_isRestart)
        {

            if (!_isDamage)
            {
                if (_isJet1 && _isJet2)
                {
                    Vector3 _pos = _upPos.transform.position - transform.position;

                  //  _rb.freezeRotation = true;

                    transform.rotation = nowRotation;
                    _rb.AddForce(transform.up* _speedDobleJet);
                   // transform.position += new Vector3(0, 0.1f,0);

                    Vector3 worldAngle = this.transform.eulerAngles;
                    worldAngle.z -= 0;

                    transform.eulerAngles = worldAngle; // 回転角度を設定
                }
                else if(_isJet1||_isJet2)
                {
                    //_rb.freezeRotation = false;
                    if (_isJet1)
                    {
                        Vector3 worldAngle = this.transform.eulerAngles;

                            worldAngle.z -= _rotation;
                        
                        transform.eulerAngles = worldAngle; // 回転角度を設定

                        //Vector3 _pos = _upPos.transform.position - transform.position;
                       // _rb.AddForce(_pos.normalized * _speedDobleJet);

                        _rb.AddForce(transform.up * _speedJet);
                        _rb.AddForce(transform.right * _speedJet/2);
                    }

                    if (_isJet2)
                    {
                        Vector3 worldAngle = this.transform.eulerAngles;



                        //if (transform.eulerAngles.z < -10)
                        //{
                        //    worldAngle.z += _rotation * 2;
                        //}
                        //else
                        
                            worldAngle.z += _rotation;
                        
                        transform.eulerAngles = worldAngle; // 回転角度を設定

                       // Vector3 _pos = _upPos.transform.position-transform.position;

                        //_rb.AddForce(_pos.normalized * _speedDobleJet);

                        _rb.AddForce(transform.up * _speedJet);
                       _rb.AddForce(-1*transform.right * _speedJet/2);

                    }
                }




                //if (_rb.velocity.x > _speedLimitX)
                //{
                //    Vector2 velo = new Vector2(_speedLimitX, _rb.velocity.y);
                //    _rb.velocity = velo;
                //}
                //else if (_rb.velocity.x < _speedLimitX)
                //{
                //    Vector2 velo = new Vector2(-_speedLimitX, _rb.velocity.y);
                //    _rb.velocity = velo;
                //}

                //if (_rb.velocity.y > _speedLimitY)
                //{
                //    Vector2 velo = new Vector2(_rb.velocity.x, _speedLimitY);
                //    _rb.velocity = velo;
                //}
            }
        }
    }



    void Player1JetMove()
    {
        if (Input.GetKey(KeyCode.W))
        {

            _isJet1 = true;
        }
        else
        {

            _isJet1 = false;
        }

    }


    void Player2JetMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Debug.Log("p2,On");
            _isJet2 = true;
        }
        else
        {
            // Debug.Log("p2,Off");
            _isJet2 = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isDamage = true;
            _hitPosY = transform.position.y;
        }

    }

}
