using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ClearMan : MonoBehaviour {
    [SerializeField] private GameObject _cpuParent; // CPUオブジェクトの親オブジェクト
    [SerializeField] private GameObject _playerParent = default; // プレイヤーオブジェクトの親オブジェクト
    [SerializeField] private GameObject _camera; // カメラオブジェクト
    [SerializeField] private GameObject _gameClearUI; // ゲームクリアUI
    [SerializeField] private TMPro.TMP_Text _clearText; // ゲームクリアのテキスト
    [SerializeField] private GameObject _outLine = default; // ゲーム画面の境界線オブジェクト
    [SerializeField] private GameObject _outLineParent; // 境界線の親オブジェクト
    //[SerializeField] private float _cameraRimit = default; // カメラ制限（未使用）
    [SerializeField] private float _cameraSizeAdjust = default; // カメラサイズ調整の値
    [SerializeField] private List<GameObject> _frogs = new List<GameObject>(); // プレイヤーおよびCPUオブジェクトのリスト
    private List<Rigidbody2D> _frogsrb2d = new List<Rigidbody2D>(); // プレイヤーおよびCPUオブジェクトのRigidbody2Dコンポーネントのリスト
    [SerializeField] private GameObject _skip;
    [SerializeField] private List<GameObject> _anotherEnemys = new List<GameObject>(); // 他の敵オブジェクトのリスト
    [SerializeField] private List<GameObject> _anotherPlayers = new List<GameObject>(); // 他のプレイヤーオブジェクトのリスト
    private List<GameObject> _rankingList = new List<GameObject>(); // ランキングのリスト
    [SerializeField] private Image[] _podiumfrog;
    [SerializeField] private Image _doubleObjectImage = default;//倍速の矢印
    [SerializeField] private Image[] _nameFrog;
    [SerializeField] private List<Sprite> _doubleSpeedImage = new List<Sprite>();
    [SerializeField] private GameObject[] _podiumfrogs;
    private float _fallMin = -60f; // オブジェクトが落下する最小Y座標
    public int _switchNumber = 0; // スイッチ番号（状態管理）
    private float _sizeLimit = 20; // サイズ制限（未使用）
    private int _maxplayer = 4; // 最大プレイヤー数
    private int _playerNumber = 1; // 現在のプレイヤー数
    private int _rankNumber = 1; // ランキングの番号
    private int _playerCount = 4; // プレイヤーの総数
    private int _cpuCount = 3; // CPUの総数
    private int _alivePlayersCount = 0; // 生存しているプレイヤーの数
    private int _gameSpeed = 2;//ゲームのスピード

    private bool _cameraScale; // カメラスケールの状態
    private bool _isPlayerDeth = false; // プレイヤーの死亡状態
    private bool _threeOrMorePeople = false; // 3人以上のプレイヤーがいるかどうか
    private bool _isSpeedUP = false;//倍速になったか
    private bool _isSelect = false;//一度選択しているか

    [SerializeField] private SneakAnim _sneak; // Sneakアニメーションのスクリプト
    [SerializeField] private JoyStickGameClearSelect _joyStickGameClear = default; // ゲームクリア時のジョイスティック選択スクリプト
    [SerializeField] private CameraShake _cameraShake; // カメラシェイクのスクリプト
    [SerializeField] private CameraRankScript _cameraRank = default; // カメラランキングのスクリプト
    [SerializeField] private StageRoopManFixed _stageRoopScript = default; // ステージループ管理スクリプト
    
    [SerializeField] private CommentScript _clearCommentScript = default;

    private enum Rank {
        First, Second, Three, Four
    }
    private Rank _rank = default;

    [SerializeField] private AudioClip _commentatorAudio = default;
    [SerializeField] private AudioClip _liveCommentatorAudio = default;
    // Start is called before the first frame update
    void Start() {
        // CPUとプレイヤーオブジェクトをリストに追加
        for (int number = 0; number < _maxplayer - _playerNumber; number++) {
            _anotherEnemys.Add(_cpuParent.transform.GetChild(number).gameObject);
        }
        for (int number = 0; number < _playerNumber; number++) {
            _anotherPlayers.Add(_playerParent.transform.GetChild(number).gameObject);
        }
        _rank = Rank.First;
    }

    // Update is called once per frame
    void Update() {

        if (_isSpeedUP) 
        {
            _skip.SetActive(true);
            _clearCommentScript.enabled = false;
            int maxGameSpeed = 3;
            int minGameSpeed = 0;
            if (Input.GetAxis("1pLstickHorizontal") > 0 && !_isSelect && _gameSpeed < maxGameSpeed) {
                _isSelect = true;
                _gameSpeed++;
                Time.timeScale = _gameSpeed;
                _doubleObjectImage.sprite = _doubleSpeedImage[_gameSpeed];
            } else if (Input.GetAxis("1pLstickHorizontal") < 0 && !_isSelect && _gameSpeed > minGameSpeed) {
                _isSelect = true;
                _gameSpeed--;
                Time.timeScale = _gameSpeed;
                _doubleObjectImage.sprite = _doubleSpeedImage[_gameSpeed];
            } else if (Input.GetAxis("1pLstickHorizontal") == 0 && _isSelect) {
                _isSelect = false;
            }
            if (Input.GetButtonDown("Skip")) {
                while (_frogs.Count > 1) {
                    int randomValue = Random.Range(0, _frogs.Count - 1);

                    _rankingList.Insert(0, _frogs[randomValue]);
                    _frogs.Remove(_frogs[randomValue]);
                    _frogs[randomValue].SetActive(false);
                    _switchNumber = 5;

                }
                _isSpeedUP = false;
                _doubleObjectImage.GetComponent<Image>().enabled = false;
                // ランキングリストに残りのFrogを追加し、タイムスケールをリセットする
                _rankingList.Insert(0, _frogs[0]);
                _cameraRank.CameeeraRank(false);
                _sneak.Access(true);
                _skip.SetActive(false);
                _cameraShake.StopCameraShake(true);
            }


        }
        // カメラスケールの調整
        if (_cameraScale) {
            _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust * Time.deltaTime * 10;
            if (_camera.GetComponent<Camera>().orthographicSize <= _cameraSizeAdjust) {
                _camera.GetComponent<Camera>().orthographicSize = _cameraSizeAdjust;
                _cameraScale = false;
            }
        }

        // スイッチ番号に応じた処理
        switch (_switchNumber) {
            case 0:
                // 敵CPUが全て排除されて、プレイヤーが1人以下の場合、またはプレイヤーが全員排除された場合
                if ((_anotherEnemys.Count == 0 && _anotherPlayers.Count <= 1) || _anotherPlayers.Count == 0) {
                    _switchNumber = 2;
                }
                break;

            case 2:
                // 状態を3に変更
                _switchNumber = 3;
                break;

            case 3:
                // スイッチ番号が3のときの処理（現在は空）
                break;

            case 5:
                _podiumfrog[16].enabled = true;
                // ランキングの表示
                Time.timeScale = 1f;
                foreach (GameObject rank in _rankingList) {

                    switch (_rank) {
                        case Rank.First:
                            if (_rankingList[0] == _podiumfrogs[0]) {
                                _podiumfrog[0].enabled = true;
                                _podiumfrog[0].GetComponent<Animator>().enabled = true;
                                _nameFrog[0].enabled = true;
                            } else if (_rankingList[0] == _podiumfrogs[1] || _rankingList[0] == _podiumfrogs[4]) {
                                _podiumfrog[1].enabled = true;
                                _podiumfrog[1].GetComponent<Animator>().enabled = true;
                                _nameFrog[1].enabled = true;
                            } else if (_rankingList[0] == _podiumfrogs[2] || _rankingList[0] == _podiumfrogs[5]) {
                                _podiumfrog[2].enabled = true;
                                _podiumfrog[2].GetComponent<Animator>().enabled = true;
                                _nameFrog[2].enabled = true;
                            } else if (_rankingList[0] == _podiumfrogs[3] || _rankingList[0] == _podiumfrogs[6]) {
                                _podiumfrog[3].enabled = true;
                                _podiumfrog[3].GetComponent<Animator>().enabled = true;
                                _nameFrog[3].enabled = true;
                            }

                            _rank = Rank.Second;


                            break;
                        case Rank.Second:
                            if (_rankingList[1] == _podiumfrogs[0]) {
                                _podiumfrog[4].enabled = true;
                                _podiumfrog[4].GetComponent<Animator>().enabled = true;
                                _nameFrog[4].enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[1] || _rankingList[1] == _podiumfrogs[4]) {
                                _podiumfrog[5].enabled = true;
                                _podiumfrog[5].GetComponent<Animator>().enabled = true;
                                _nameFrog[5].enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[2] || _rankingList[1] == _podiumfrogs[5]) {
                                _podiumfrog[6].enabled = true;
                                _podiumfrog[6].GetComponent<Animator>().enabled = true;
                                _nameFrog[6].enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[3] || _rankingList[1] == _podiumfrogs[6]) {
                                _podiumfrog[7].enabled = true;
                                _podiumfrog[7].GetComponent<Animator>().enabled = true;
                                _nameFrog[7].enabled = true;
                            }

                            _rank = Rank.Three;

                            break;
                        case Rank.Three:
                            if (_rankingList[2] == _podiumfrogs[0]) {
                                _podiumfrog[8].enabled = true;
                                _podiumfrog[8].GetComponent<Animator>().enabled = true;
                                _nameFrog[8].enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[1] || _rankingList[2] == _podiumfrogs[4]) {
                                _podiumfrog[9].enabled = true;
                                _podiumfrog[9].GetComponent<Animator>().enabled = true;
                                _nameFrog[9].enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[2] || _rankingList[2] == _podiumfrogs[5]) {
                                _podiumfrog[10].enabled = true;
                                _podiumfrog[10].GetComponent<Animator>().enabled = true;
                                _nameFrog[10].enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[3] || _rankingList[2] == _podiumfrogs[6]) {
                                _podiumfrog[11].enabled = true;
                                _podiumfrog[11].GetComponent<Animator>().enabled = true;
                                _nameFrog[11].enabled = true;
                            }

                            _rank = Rank.Four;
                            break;
                        case Rank.Four:
                            if (_rankingList[3] == _podiumfrogs[0]) {
                                _podiumfrog[12].enabled = true;
                                _nameFrog[12].enabled = true;

                            } else if (_rankingList[3] == _podiumfrogs[1] || _rankingList[3] == _podiumfrogs[4]) {
                                _podiumfrog[13].enabled = true;
                                _nameFrog[13].enabled = true;
                            } else if (_rankingList[3] == _podiumfrogs[2] || _rankingList[3] == _podiumfrogs[5]) {
                                _podiumfrog[14].enabled = true;
                                _nameFrog[14].enabled = true;
                            } else if (_rankingList[3] == _podiumfrogs[3] || _rankingList[3] == _podiumfrogs[6]) {
                                _podiumfrog[15].enabled = true;
                                _nameFrog[15].enabled = true;

                            }

                            break;
                    }
                    _rankingList[0].SetActive(false);
                    _rankNumber++;
                }
                _gameClearUI.SetActive(true); // ゲームクリアUIを表示
                _joyStickGameClear.enabled = true; // ジョイスティックのゲームクリア選択を有効にする
                _switchNumber = 6;
                break;

            default:
                break;
        }
    }

    // ゲームクリア後のリトライボタン処理（スペースキー押下）
    public void ClearRetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // タイトルシーンに戻るボタン処理
    public void ClearTitleButton() {
        SceneManager.LoadScene("TitleScene"); // タイトルシーンに遷移
    }

    // Frogsリストに新しいFrogを追加
    public void InFrogs(GameObject frog) {
        _frogs.Add(frog);
        _frogsrb2d.Add(frog.GetComponent<Rigidbody2D>());
    }

    // プレイヤー数の設定
    public void MaxPlayer(int playerNumber) {
        _playerNumber = playerNumber;
    }

    // プレイヤーまたはCPUの脱落処理
    public void DropOuts(GameObject dropOutFrog) {
        // プレイヤーがタグ「Player」の場合、人数をカウント
        foreach (GameObject players in _frogs) {
            if (players.CompareTag("Player")) {
                _alivePlayersCount++;
            }
        }
        if (dropOutFrog.CompareTag("Player")) {
            _isPlayerDeth = true;
        }

        // プレイヤーが3人以上残っている場合のフラグ設定
        if (_alivePlayersCount == 3 || _alivePlayersCount == 4) {
            _threeOrMorePeople = true;
        }

        // 脱落したFrogをランキングリストに追加
        _rankingList.Insert(0, dropOutFrog);

        // Frogsリストから脱落したFrogを削除
        for (int frogCount = 0; frogCount < _frogs.Count; frogCount++) {
            if (_frogs[frogCount] == dropOutFrog) {
                _frogs.Remove(_frogs[frogCount]);
            }
        }

        // プレイヤーまたはCPUのカウントを減らす
        if (dropOutFrog.gameObject.CompareTag("Player")) {
            _playerCount--;
        } else if (dropOutFrog.gameObject.CompareTag("CPU")) {
            _cpuCount--;
        }

        // プレイヤーとCPUの合計が4を超える場合、Frogを非アクティブにする
        if (_cpuCount + _playerCount > 4) {

            dropOutFrog.SetActive(false);
        }

        // ゲームの終了条件に達した場合の処理
        int gameEndPlayerCount = 1;

        if (_cpuCount + _playerCount <= 4) 
        {
            _clearCommentScript.CommentatorCommentChange("ここでレースのしょうしゃがきまりました！！！", false, _commentatorAudio);
            _isSpeedUP = false;
            // ランキングリストに残りのFrogを追加し、タイムスケールをリセットする
            _rankingList.Insert(0, _frogs[0]);
            StartCoroutine(TimeScaleReset(dropOutFrog));
            _cameraRank.CameeeraRank(false);
            _sneak.Access(true);
            _cameraShake.StopCameraShake(true);

        } else if (_isPlayerDeth && _alivePlayersCount == gameEndPlayerCount && !_threeOrMorePeople) {
            Time.timeScale = _gameSpeed;
            _isSpeedUP = true;
            _doubleObjectImage.GetComponent<Image>().enabled = true;
            _stageRoopScript.CompulsionHarryUP();
        }

        // リセット
        _alivePlayersCount = 0;
        _isPlayerDeth = false;
    }

    // タイムスケールをリセットするコルーチン
    private IEnumerator TimeScaleReset(GameObject leaveFrog) {
        Rigidbody2D rb = leaveFrog.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // カメラをFrogの位置に移動
        _camera.transform.position = new Vector3(leaveFrog.transform.position.x, leaveFrog.transform.position.y, -10);
        _cameraScale = true;
        leaveFrog.GetComponent<SpriteRenderer>().enabled = false;
        // 脱落したFrogの周りにアウトラインを表示
        _outLineParent.transform.SetParent(leaveFrog.transform, true);
        _outLineParent.transform.position = new Vector3(leaveFrog.transform.position.x + 5, leaveFrog.transform.position.y, 0);

        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.1f;

        yield return new WaitForSeconds(0.3f);
        _clearCommentScript.LiveCommentatorCommentChange("それでは、こんかいのけっかをみてみるケロ", false, _liveCommentatorAudio);
        yield return new WaitForSeconds(0.4f);
        leaveFrog.SetActive(false);
        _switchNumber = 5;
        _clearCommentScript.enabled = false;

    }

}
