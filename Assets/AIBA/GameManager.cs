using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>�V�[�������[�h����R���|�[�l���g</summary>
    [SerializeField] SceneLoader m_sceneLoader = null;
    /// <summary>�c�@��</summary>
    [SerializeField] int m_life = 3;
    /// <summary>���_</summary>
    int m_score;
    /// <summary>���@�̃v���n�u���w�肷��</summary>
    [SerializeField] GameObject m_playerObject = null;
    /// <summary>�Q�[���̏��������I����Ă���Q�[�����n�܂�܂ł̑҂�����</summary>
    [SerializeField] float m_waitTimeUntilGameStarts = 5f;
    /// <summary>���@������Ă���Q�[���̍ď�����������܂ł̑҂�����</summary>
    [SerializeField] float m_waitTimeAfterPlayerDeath = 5f;
    /// <summary>�c�@�\�������� PlayerCounter ��ێ����Ă����ϐ�</summary>
    PlayerCounter m_playerCounter;
    /// <summary>�X�R�A�\���p Text</summary>
   // [SerializeField] Text m_scoreText = null;
    /// <summary>GameOver �\���p Text</summary>
    [SerializeField] Text m_gameoverText = null;
    [SerializeField] GameObject _gameOverZone;
    /// <summary>�^�C�}�[</summary>
    float m_timer;

    bool _isStart;

    [SerializeField] Transform _startPos;
    /// <summary>覐΁i��j�𐶐�����I�u�W�F�N�g</summary>
    [SerializeField] GameObject _enemyGeneration;
    EnemyGenerater enemyGenerater;

    [SerializeField] GameObject[] _wideGene = new GameObject[0];

    [SerializeField] Text _startText;

    [SerializeField] Rigidbody2D _pRb;

    /// <summary>�Q�[���̏��</summary>
    GameState m_status = GameState.NonInitialized;
    void Start()
    {
        _pRb = _pRb.GetComponent<Rigidbody2D>();
        // �Q�[���I�[�o�[�̕\��������
        if (m_gameoverText)
        {
            m_gameoverText.enabled = false;
        }





        enemyGenerater = GetComponent<EnemyGenerater>();
        m_playerCounter = GetComponent<PlayerCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_status)   // �Q�[���̏�Ԃɂ���ď����𕪂���
        {
            case GameState.NonInitialized:
                Debug.Log("Initialize.");
             m_playerCounter.Refresh(m_life);    // �c�@�\�����X�V����
             m_playerCounter.Refresh(m_life);    // �c�@�\�����X�V����
                StartCoroutine(CountStart());

                m_status = GameState.StartCount;
                m_playerObject.transform.position = _startPos.position;
                break;
            case GameState.StartCount:




                break;

            case GameState.Initialized:
                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeUntilGameStarts)    // �҂�
                {
                    Debug.Log("Game Start.");
                    m_timer = 0f;   // �^�C�}�[�����Z�b�g����
                    m_status = GameState.InGame;   // �X�e�[�^�X���Q�[�����ɂ���
                }
                break;
            case GameState.PlayerDead:
                // �c�@���Ȃ�������Q�[���I�[�o�[��\������
                if (m_gameoverText && m_life < 2)
                {
                    m_gameoverText.enabled = true;
                    if (_enemyGeneration)
                    {
                        _enemyGeneration.SetActive(false);
                    }

                }

                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeAfterPlayerDeath)   // �҂�
                {
                    if (m_life > 0) // �c�@���܂�����ꍇ
                    {
                        Debug.Log("Restart Game.");
                        m_timer = 0f;   // �^�C�}�[�����Z�b�g����
                        m_status = GameState.InGame;   // ���������邽�߂ɃX�e�[�^�X���X�V����
                    }
                    else
                    {
                        GameOver(); // �c�@�������Ȃ��ꍇ�̓Q�[���I�[�o�[�ɂ���
                    }
                }
                break;
        }
    }



    IEnumerator CountStart()
    {
        _pRb.isKinematic = true;
        yield return new WaitForSeconds(1);
        _startText.text = "   3";
        yield return new WaitForSeconds(1);
        _startText.text = "   2";
        yield return new WaitForSeconds(1);
        _startText.text = "   1";
        yield return new WaitForSeconds(1);
        _startText.text = "Go!";

        _isStart = true;
        _pRb.isKinematic = false;
     
        if (enemyGenerater)
        {
            enemyGenerater._countTime = 0;
        }

        for (int i = 0; i < _wideGene.Length; i++)
        {
            _wideGene[i].gameObject.SetActive(true);
        }
        _gameOverZone.SetActive(false);
        if (_enemyGeneration)
        {
            _enemyGeneration.SetActive(true);
        }

        m_status = GameState.Initialized;   // �X�e�[�^�X���������ς݂ɂ���


        yield return new WaitForSeconds(1);
        _startText.text = " ";
    }

    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_life -= 1;    // �c�@�����炷
        m_playerCounter.Refresh(m_life);    // �c�@�\�����X�V����
        m_status = GameState.PlayerDead;   // �X�e�[�^�X���v���C���[�����ꂽ��ԂɍX�V����
    }

    /// <summary>
    /// �V�[����ɂ���G�ƓG�̒e������
    /// </summary>
    void ClearScene()
    {
        // �G������
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("EnemyTag");
        foreach (var go in goArray)
        {
            Destroy(go);
        }
    }

    /// <summary>
    /// �Q�[���I�[�o�[���ɌĂяo��
    /// </summary>
    void GameOver()
    {
        Debug.Log("Game over. Load scene.");
        if (m_sceneLoader)
        {
            m_sceneLoader.LoadScene();
        }
    }

    enum GameState
    {
        /// <summary>�Q�[���������O</summary>
        NonInitialized,
        /// <summary>�Q�[���������ς݁A�Q�[���J�n�O</summary>
        Initialized,
        /// <summary>�Q�[����</summary>
        InGame,
        /// <summary>�v���C���[�����ꂽ</summary>
        PlayerDead,

        StartCount,
    }
}