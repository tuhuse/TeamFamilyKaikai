using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMan : MonoBehaviour {
   
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private float _cameraSizeAdjust = default;
    [SerializeField] private float _cameraRimit = default;
    [SerializeField] private GameObject _outLine = default;
   
    private List<GameObject> _frogs = new List<GameObject>();
    private List<Rigidbody2D> _frogsrb2d = new List<Rigidbody2D>();
    [SerializeField] private List<GameObject> _players = new List<GameObject>();
    [SerializeField]
    private GameObject _playerObject = default;
    [SerializeField]
    private GameObject _playerParent = default;

    private float _playerFallMin = -60f;
    private float _sizeLimit = 20;
    private float _countTime = default;
    private float _time = 80;
    public int _switchNumber = 0;

    [SerializeField] private JoyStickGameOverSelect _joyStickGameOver = default;
   // Start is called before the first frame update
   void Start() {
        for (int number = 0; number < _playerParent.transform.childCount; number++) {
            _players.Add(_playerParent.transform.GetChild(number).gameObject);
        }

    }

    // Update is called once per frame
    void Update() {

        _countTime += Time.deltaTime;



        //プレイヤーがゲームオーバーしたら、カメラをズームアップ
        switch (_switchNumber) {
            //プレイヤーが落下、もしくはカメラの左端に当たればゲームオーバー処理開始
            case 0:

                //プレイヤーオブジェクトの入った配列の中身がnullか
                if (_players.Count == 0) {
                    _switchNumber = 2;
                } else {
                    _switchNumber = 1;
                }
                break;
            case 1:
                if (_countTime >= _time) {
                    _cameraRimit = 80f;
                }
                //プレイヤーオブジェクトを取得
                foreach (GameObject arrayPlayer in _players) {
                    //落下もしくは画面端にぶつかると配列から削除
                    if (arrayPlayer.transform.position.y < _playerFallMin ||
                                arrayPlayer.transform.position.x < _outLine.transform.position.x) {
                        _players.Remove(arrayPlayer);
                        arrayPlayer.SetActive(false);
                        _switchNumber = 0;
                        break;
                    }
                }
                break;
            case 2:

                _frogsrb2d[0].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[1].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[2].constraints = RigidbodyConstraints2D.FreezePosition;
                _frogsrb2d[3].constraints = RigidbodyConstraints2D.FreezePosition;
                _switchNumber = 3;

                break;

            case 3:

                _camera.transform.position -= Vector3.up;

                _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust;
             
                if (_camera.GetComponent<Camera>().orthographicSize <= _sizeLimit) {
                    _switchNumber = 4;
                   
                }

                break;
            case 4:

                _joyStickGameOver.enabled = true;
                _gameOverUI.GetComponent<Canvas>().enabled = true;

                break;

            default:
                break;
        }




    }
    public void GameOverRetryButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //もう一度同じ場所をプレイ
        }
    }


    public void GameOverTitleButton() {

        SceneManager.LoadScene("TitleScene");

    }
    public void InFrogs(GameObject frog) {
        _frogs.Add(frog);
        _frogsrb2d.Add(frog.GetComponent<Rigidbody2D>());

    }
}
