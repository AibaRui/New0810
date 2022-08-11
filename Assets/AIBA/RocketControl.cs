using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class RocketControl : MonoBehaviour
{
    [Header("�ړ��̑���")]
    [SerializeField] float _speedJet = 1;
    [SerializeField] float _speedDobleJet = 1;


    [Header("�X���p�x")]
    [SerializeField] float _rotation = 0.001f;

    [Header("���X�|�[������܂ł̎���")]
    [SerializeField] float _responTime = 2;

    [Header("�{�^���̃C���[�W")]

 �@  /// <summary>�v���C���[�P�̃{�^���̉摜</summary>  
    [SerializeField] GameObject _p1Button; 
    /// <summary>�v���C���[�Q�̃{�^���̉摜</summary>  
    [SerializeField] GameObject _p2Button;

    [Header("��_���[�W���̃A�j���[�V�����̖��O")]
    [SerializeField] string _animhit = "RoketDamaged";

    /// <summary>���X�^�[�g���鎞�̃X�e�[�W�̐^��</summary>  
    float _hitPosY = 0;


    /// <summary>���X�^�[�g�����ǂ����̔���</summary>
    bool _isRestart;
    /// <summary>Pl�_���[�W���󂯂����ǂ����̔���</summary>
    bool _isDamage;
    /// <summary>Player1���{�^���������Ă��邩�ǂ����̔���</summary>
    bool _isJet1;
    /// <summary>Player2���{�^���������Ă��邩�ǂ����̔���</summary>
    bool _isJet2;

    Quaternion nowRotation;
    Animator _anim;
    Rigidbody2D _rb;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDamage)
        {
            _isDamage = false;
            transform.position = new(0, _hitPosY, 0);       //�|�W�V������^�񒆂Ɉړ�
            transform.eulerAngles = new(0, 0, 0);           //�p�x���O��
            _rb.freezeRotation = true;                      //��]�����Ȃ�                           
            _rb.velocity = new Vector2(0, 0);               //���x��0
            _rb.isKinematic = true;                        //�����Ȃ�����
            _isRestart = true;
            StartCoroutine(Hit());
            _anim.Play(_animhit);
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            gm.PlayerDead();
        }

        if (!_isRestart)
        {
            Player1JetMove();
            Player2JetMove();
            nowRotation = transform.rotation;
        }


    }
    /// <summary>���A�܂ł̎���</summary>
    IEnumerator Hit()
    {
        yield return new WaitForSeconds(_responTime);
        _rb.isKinematic = false;
        _rb.freezeRotation = false;
        _isRestart = false;
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
                    transform.eulerAngles = worldAngle; // ��]�p�x��ݒ�


                    _p1Button.SetActive(true);
                    _p2Button.SetActive(true);

                }
                else if (_isJet1 || _isJet2)
                {
                    if (_isJet1)
                    {
                        // ��]�p�x��ݒ�
                        Vector3 worldAngle = this.transform.eulerAngles;
                        worldAngle.z -= _rotation;
                        transform.eulerAngles = worldAngle;


                        _rb.AddForce(transform.up * _speedJet);
                        _rb.AddForce(transform.right * _speedJet / 2);

                        _p1Button.SetActive(true);
                        _p2Button.SetActive(false);
                    }

                    if (_isJet2)
                    {
                        // ��]�p�x��ݒ�
                        Vector3 worldAngle = this.transform.eulerAngles;
                        worldAngle.z += _rotation;
                        transform.eulerAngles = worldAngle;

                        _rb.AddForce(transform.up * _speedJet);
                        _rb.AddForce(-1 * transform.right * _speedJet / 2);

                        _p1Button.SetActive(false);
                        _p2Button.SetActive(true);
                    }
                }
                else
                {
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
