using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>シーンをロードするコンポーネント</summary>
    [SerializeField] SceneLoader m_sceneLoader = null;
    /// <summary>残機数</summary>
    [SerializeField] int m_life = 3;
    /// <summary>得点</summary>
    int m_score;
    /// <summary>自機のプレハブを指定する</summary>
    [SerializeField] GameObject m_playerObject = null;
    /// <summary>ゲームの初期化が終わってからゲームが始まるまでの待ち時間</summary>
    [SerializeField] float m_waitTimeUntilGameStarts = 5f;
    /// <summary>自機がやられてからゲームの再初期化をするまでの待ち時間</summary>
    [SerializeField] float m_waitTimeAfterPlayerDeath = 5f;
    /// <summary>残機表示をする PlayerCounter を保持しておく変数</summary>
    PlayerCounter m_playerCounter;
    /// <summary>スコア表示用 Text</summary>
   // [SerializeField] Text m_scoreText = null;
    /// <summary>GameOver 表示用 Text</summary>
    [SerializeField] Text m_gameoverText = null;
    [SerializeField] GameObject _gameOverZone;
    /// <summary>タイマー</summary>
    float m_timer;

    bool _isStart;

    [SerializeField] Transform _startPos;
    /// <summary>隕石（上）を生成するオブジェクト</summary>
    [SerializeField] GameObject _enemyGeneration;
    EnemyGenerater enemyGenerater;

    [SerializeField] GameObject[] _wideGene = new GameObject[0];

    [SerializeField] Text _startText;

    [SerializeField] Rigidbody2D _pRb;

    /// <summary>ゲームの状態</summary>
    GameState m_status = GameState.NonInitialized;
    void Start()
    {
        _pRb = _pRb.GetComponent<Rigidbody2D>();
        // ゲームオーバーの表示を消す
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
        switch (m_status)   // ゲームの状態によって処理を分ける
        {
            case GameState.NonInitialized:
                Debug.Log("Initialize.");
             m_playerCounter.Refresh(m_life);    // 残機表示を更新する
             m_playerCounter.Refresh(m_life);    // 残機表示を更新する
                StartCoroutine(CountStart());

                m_status = GameState.StartCount;
                m_playerObject.transform.position = _startPos.position;
                break;
            case GameState.StartCount:




                break;

            case GameState.Initialized:
                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeUntilGameStarts)    // 待つ
                {
                    Debug.Log("Game Start.");
                    m_timer = 0f;   // タイマーをリセットする
                    m_status = GameState.InGame;   // ステータスをゲーム中にする
                }
                break;
            case GameState.PlayerDead:
                // 残機がなかったらゲームオーバーを表示する
                if (m_gameoverText && m_life < 2)
                {
                    m_gameoverText.enabled = true;
                    if (_enemyGeneration)
                    {
                        _enemyGeneration.SetActive(false);
                    }

                }

                m_timer += Time.deltaTime;
                if (m_timer > m_waitTimeAfterPlayerDeath)   // 待つ
                {
                    if (m_life > 0) // 残機がまだある場合
                    {
                        Debug.Log("Restart Game.");
                        m_timer = 0f;   // タイマーをリセットする
                        m_status = GameState.InGame;   // 初期化するためにステータスを更新する
                    }
                    else
                    {
                        GameOver(); // 残機がもうない場合はゲームオーバーにする
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

        m_status = GameState.Initialized;   // ステータスを初期化済みにする


        yield return new WaitForSeconds(1);
        _startText.text = " ";
    }

    public void PlayerDead()
    {
        Debug.Log("Player Dead.");
        m_life -= 1;    // 残機を減らす
        m_playerCounter.Refresh(m_life);    // 残機表示を更新する
        m_status = GameState.PlayerDead;   // ステータスをプレイヤーがやられた状態に更新する
    }

    /// <summary>
    /// シーン上にある敵と敵の弾を消す
    /// </summary>
    void ClearScene()
    {
        // 敵を消す
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("EnemyTag");
        foreach (var go in goArray)
        {
            Destroy(go);
        }
    }

    /// <summary>
    /// ゲームオーバー時に呼び出す
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
        /// <summary>ゲーム初期化前</summary>
        NonInitialized,
        /// <summary>ゲーム初期化済み、ゲーム開始前</summary>
        Initialized,
        /// <summary>ゲーム中</summary>
        InGame,
        /// <summary>プレイヤーがやられた</summary>
        PlayerDead,

        StartCount,
    }
}