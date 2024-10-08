using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageRoopManFixed : MonoBehaviour {
    [SerializeField] private List<GameObject> _stageUpperRightCorner = new List<GameObject>();
    [SerializeField] private List<GameObject> _prefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _addPrefabs = new List<GameObject>();
    [SerializeField] private GameObject _playerObjct = null;
    [SerializeField] private GameObject _stageParents;
    [SerializeField] private GameObject _outLineObject = default;
    [SerializeField] private float _cameraTargetY;
    [SerializeField] private GameObject[] _hurryUpText;
    [SerializeField] private GameOverMan _gameOverMan;
    [SerializeField] private ClearMan _gameClearMan;
    [SerializeField] private GameObject _farstStage = default;
    [SerializeField] private GameObject _preStage = default;
    [SerializeField] private CommentScript _commentText = default;
    private GameObject _first;


    [SerializeField] CameraShake _cameraShake;
    private Camera _camera;
    private bool _isCameraSize = false;
    private float _startSize;
    private Vector3 _startPosition;
    private float _timer;
    private float _changeDuration = 10f;
    [SerializeField] private CountDowntext _count;
    private bool _isOneShot = false;

    [SerializeField] AudioClip _warningSE = default;

    private AudioSource _audio = default;

    //ステージの移動距離定数
    [SerializeField] private float _stageXPosition = default;
    //ステージの現在位置
    [SerializeField] float _stageNowPosition = default;

    //配列番号
    private int _arrayNumber = 10;
    private int _randomMax = default;
    private int _randomMin = 0;
    private int _prefabNumber = 7;
    private int _switchNumber = 0;
    private int _divisionNumber = default;
    //private float _playerNowposition = default;
    private float _countTime = default;
    private float _time = 45;
    private float _nextTime = 55f;

    public bool _isRoop = false;
    private bool _isReadyGo;
    private bool _isDivisionStage = default;
    private bool _isCameraShake = true;
    private void Start() {

        _camera = Camera.main;
        _startSize = _camera.orthographicSize;
        _startPosition = _camera.transform.position;
        _audio = this.GetComponent<AudioSource>();
        
        _first = _camera.GetComponent<CameraRankScript>()._ranking[0];
        _preStage = _farstStage;
        _stageNowPosition = _preStage.transform.position.x -
                            (_preStage.GetComponent<SpriteRenderer>().size.x *
                            _preStage.transform.localScale.x);

    }
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            _countTime += 100;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _countTime += 100;
        }
        if (_isReadyGo) {
            _countTime += Time.deltaTime;
        }


        float playerNowposition = _first.transform.position.x;

        if (_gameClearMan._switchNumber == 3) {
            _isCameraSize = false;
            _isCameraShake = false;
        }
        if (_isCameraSize) {
            _timer += Time.deltaTime; // 経過時間を加算する
            // 時間が経過した割合を計算する
            float timeRatio = _timer / _changeDuration;
            timeRatio = Mathf.Clamp01(timeRatio); // 0から1の間にクランプ

            //if (_isCameraShake) {
            //    float targetY = _cameraTargetY;//cameraが向かうY座標
            //    Vector3 targetPosition = new Vector3(_camera.transform.position.x, Mathf.Lerp(_startPosition.y, targetY, timeRatio), _camera.transform.position.z);
            //    _camera.transform.position = targetPosition;
            //}


        }




        //ランダムの値を保持
        //プレイヤーが先頭のひとつ前のステージについたらランダムに続きを移動
        if (playerNowposition >= _stageNowPosition) {

            //現在の最後尾を避けて配列数を取得
            _randomMax = _prefabs.Count - 2;

            _arrayNumber = Random.Range(_randomMin, _randomMax);


            if (_isDivisionStage) {
                float preStageScale = _stageUpperRightCorner[_divisionNumber].GetComponent<SpriteRenderer>().size.x
                       * _prefabs[_arrayNumber].transform.lossyScale.x;

                float nextStageScale = _prefabs[_arrayNumber].GetComponent<SpriteRenderer>().size.x
                 * _prefabs[_arrayNumber].transform.localScale.x;




                //ステージのポジション移動
                _prefabs[_arrayNumber].transform.position =
                    new Vector2(_stageUpperRightCorner[_divisionNumber].transform.position.x + (preStageScale / 2) + (nextStageScale / 2), _stageUpperRightCorner[_divisionNumber].transform.position.y);




                _preStage = _prefabs[_arrayNumber];

                _stageNowPosition = _preStage.transform.position.x -
                                    (_preStage.GetComponent<SpriteRenderer>().size.x *
                                     _preStage.transform.localScale.x / 2);
                //移動したステージを配列の末尾に追加し、行を詰める
                _prefabs.Add(_prefabs[_arrayNumber]);

                _prefabs.Remove(_prefabs[_arrayNumber]);



                _isDivisionStage = false;
                if (_preStage.GetComponent<DivisionScriptver2>()) {
                    _isDivisionStage = true;
                    _preStage.GetComponent<DivisionScriptver2>().GiveNumber();
                }
            }
            else 
            {
                float preStageScale = _preStage.GetComponent<SpriteRenderer>().size.x
                        * _preStage.transform.localScale.x;
                float nextStageScale = _prefabs[_arrayNumber].GetComponent<SpriteRenderer>().size.x
                 * _prefabs[_arrayNumber].transform.localScale.x;




                //ステージのポジション移動
                _prefabs[_arrayNumber].transform.position =
                    new Vector2(_preStage.transform.position.x + (preStageScale / 2) + (nextStageScale / 2), _preStage.transform.position.y);

                _preStage = _prefabs[_arrayNumber];

                _stageNowPosition = _preStage.transform.position.x -
                                    (_preStage.GetComponent<SpriteRenderer>().size.x *
                                     _preStage.transform.localScale.x / 2);
                //移動したステージを配列の末尾に追加し、行を詰める
                _prefabs.Add(_prefabs[_arrayNumber]);

                _prefabs.Remove(_prefabs[_arrayNumber]);
                if (_preStage.GetComponent<DivisionScriptver2>()) {
                    _isDivisionStage = true;
                    _preStage.GetComponent<DivisionScriptver2>().GiveNumber();
                }
            }




        } else {

            _isRoop = false;
        }


        switch (_switchNumber) {
            case 0:
                if (_countTime >= _time) {

                    for (int number = 0; number < _prefabNumber; number++) {
                        _prefabs.Insert(_randomMax, _addPrefabs[number]);

                    }

                    _switchNumber = 1;
                }
                break;

            case 1:


                if (_countTime >= _nextTime) {

                    StartCoroutine(HurryUpText());
                    

                    StartCoroutine(_outLineObject.GetComponent<ProgressScript>().StartProgress());
                    

                    _isCameraSize = true;

                    _switchNumber = 2;
                }
                break;
            default:
                break;
        }
    }

    
    public void CameraHurryUp() {
        float timeRatio = _timer / _changeDuration;
        float targetY = _cameraTargetY;//cameraが向かうY座標
        Vector3 targetPosition = new Vector3(_startPosition.x, Mathf.Lerp(_startPosition.y, targetY, timeRatio), _startPosition.z);
        _camera.transform.position = targetPosition;
    }
    private IEnumerator HurryUpText() {
        yield return new WaitForSeconds(1);
        _cameraShake.StartCameraShake();
        _hurryUpText[0].SetActive(true);

        if (!_isOneShot) {
            _isOneShot = true;
            _audio.PlayOneShot(_warningSE);

        }
        yield return new WaitForSeconds(3);
        for (int number = _prefabNumber; number < _addPrefabs.Count; number++) {
            _prefabs.Insert(_randomMax, _addPrefabs[number]);

        }


        _cameraShake.StopCameraShake(false);
        _hurryUpText[0].SetActive(false);
        
        
        _count._bgm.pitch = 1.4f;
        _count._bgm.volume = 0.7f;
    }
    public void ReadyGo(bool isGo) {
        if (isGo) {
            _isReadyGo = true;
        }

    }

    public void FirstChange(GameObject newFarstFrog) {
        _first = newFarstFrog;
    }

    public void DivisionStageMove(int stageNumber) {
        _divisionNumber = stageNumber;
    }
    public void Camerashake(bool camerashakeY) {
        if (camerashakeY) {

            _isCameraShake = false;
        } else {
            _isCameraShake = true;
        }
    }

    public void CompulsionHarryUP() {
        _countTime += 100;

    }
}

