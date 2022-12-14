using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RocketControl : MonoBehaviour
{
    [Header("移動の速さ")]
    [SerializeField] float _speedJet = 1;
    [SerializeField] float _speedDobleJet = 1;


    [Header("傾き角度")]
    [SerializeField] float _rotation = 0.001f;

    [Header("リスポーンするまでの時間")]
    [SerializeField] float _responTime = 2;
    [SerializeField] float _mutekiTime=5;

    [Header("ボタンのイメージ")]

 　  /// <summary>プレイヤー１のボタンの画像</summary>  
    [SerializeField] GameObject _p1Button; 
    /// <summary>プレイヤー２のボタンの画像</summary>  
    [SerializeField] GameObject _p2Button;

    [Header("被ダメージ時のアニメーションの名前")]
    [SerializeField] string _animhit = "RoketDamaged";


    [SerializeField] GameObject _fire1;
    [SerializeField] GameObject _fire2;
    [SerializeField] GameObject _fireStrong;

    [SerializeField] GameObject _baria;

    /// <summary>リスタートする時のステージの真ん中</summary>  
    float _hitPosY = 0;


    /// <summary>リスタート中かどうかの判定</summary>
    bool _isRestart;
    /// <summary>Plダメージを受けたかどうかの判定</summary>
    bool _isDamage;
    /// <summary>Player1がボタンが押しているかどうかの判定</summary>
    bool _isJet1;
    /// <summary>Player2がボタンが押しているかどうかの判定</summary>
    bool _isJet2;

    [SerializeField] AudioSource _oto1;
    [SerializeField] AudioSource _oto2;
    [SerializeField] AudioSource _exeprosion;


    Quaternion nowRotation;
    Animator _anim;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();

        _oto1 = _oto1.GetComponent<AudioSource>();
        _oto2 = _oto2.GetComponent<AudioSource>();
        _exeprosion= gameObject.GetComponent<AudioSource>();

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
            gameObject.layer = 8;
            _isRestart = true;
            StartCoroutine(Hit());
            _anim.Play(_animhit);
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            gm.PlayerDead();
            _baria.SetActive(true);
        }

        if (!_isRestart)
        {
            Player1JetMove();
            Player2JetMove();
            nowRotation = transform.rotation;
        }





    }
    /// <summary>復帰までの時間</summary>
    IEnumerator Hit()
    {
        yield return new WaitForSeconds(_responTime);
        _rb.isKinematic = false;
        _rb.freezeRotation = false;
        _isRestart = false;
        _baria.SetActive(false);
        gameObject.layer = 7;
    }




    void FixedUpdate()
    {
        if (!_isRestart)
        {
            if (!_isDamage)
            {
                if (_isJet1 && _isJet2)
                {
                    transform.rotation = nowRotation;
                    _rb.AddForce(transform.up * _speedDobleJet);

                    Vector3 worldAngle = this.transform.eulerAngles;
                    worldAngle.z -= 0;
                    transform.eulerAngles = worldAngle; // 回転角度を設定



                    _fire1.SetActive(true);
                    _fire2.SetActive(true);
                    _fireStrong.SetActive(true);

                    _p1Button.SetActive(true);
                    _p2Button.SetActive(true);

                }
                else if (_isJet1 || _isJet2)
                {
                    if (_isJet1)
                    {
                        // 回転角度を設定
                        Vector3 worldAngle = this.transform.eulerAngles;
                        worldAngle.z -= _rotation;
                        transform.eulerAngles = worldAngle;


                        _rb.AddForce(transform.up * _speedJet);
                        _rb.AddForce(transform.right * _speedJet / 2);

                        

                        _fire1.SetActive(true);
                        _fire2.SetActive(false);
                        _fireStrong.SetActive(false);

                        _p1Button.SetActive(true);
                        _p2Button.SetActive(false);
                    }

                    if (_isJet2)
                    {
                        // 回転角度を設定
                        Vector3 worldAngle = this.transform.eulerAngles;
                        worldAngle.z += _rotation;
                        transform.eulerAngles = worldAngle;

                        _rb.AddForce(transform.up * _speedJet);
                        _rb.AddForce(-1 * transform.right * _speedJet / 2);

                        

                        _fire1.SetActive(false);
                        _fire2.SetActive(true);
                        _fireStrong.SetActive(false);

                        _p1Button.SetActive(false);
                        _p2Button.SetActive(true);
                    }
                }
                else
                {
                    _fire1.SetActive(false);
                    _fire2.SetActive(false);
                    _fireStrong.SetActive(false);

                    _p1Button.SetActive(false);
                    _p2Button.SetActive(false);
                }
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

        if(Input.GetKeyDown(KeyCode.W))
        {
            _oto1.Play();
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            _oto1.Stop();
        }


    }


    void Player2JetMove()
    {
        if (Input.GetKey(KeyCode.O))
        {
            // Debug.Log("p2,On");
            _isJet2 = true;
        }
        else
        {
            // Debug.Log("p2,Off");
            _isJet2 = false;
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            _oto2.Play();
        }


        if (Input.GetKeyUp(KeyCode.O))
        {
            _oto2.Stop();
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall"||collision.gameObject.tag=="Enemy")
        {
            _exeprosion.Play();
            _isDamage = true;
            _hitPosY = transform.position.y;
        }

    }

}
