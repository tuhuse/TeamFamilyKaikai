using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMmodeCamera : MonoBehaviour {
    [Header("カメラ")] [SerializeField] GameObject _camera = default;//カメラ入れ

    [SerializeField] private GameObject _cameraEdgeObject = default;

    private GameObject _dummy = default;

    [Header("カエル4体を入れる")] [SerializeField] private List<GameObject> _frogs = new List<GameObject>();

    [SerializeField] List<Rigidbody2D> _rbs = new List<Rigidbody2D>();

    [SerializeField] public List<GameObject> _ranking = new List<GameObject>();
    private int _rankingValue = 0;

    private bool _isGameStart = false;
    private bool _isUp = false;
    private bool _isDown = default;
    private bool _isStart = false;



    //private float _playerDistance = default;
    private float _camposiChangeY = default;


    private float _firstPosition = default;
    private float _secondPosition = default;
    private float _thirdPosition = default;
    private float _forthPosition = default;



    private const float CAMPOSIY = 0f;
    private const float CAMPOSIZ = -10;
    private const float CAMERAMOVEVALUE = 50;

    //配列はゼロオリジンのため、0からスタート
    private const int ORIGINFIRST = 0;
    private const int ORIGINSECOND = 1;
    private const int ORIGINTHIRD = 2;
    private const int ORIGINFORTH = 3;

    private const int FIRST = 1;
    private const int SECOND = 2;
    private const int THIRD = 3;
    private const int FORTH = 4;

    private const float CAMERAYMOVE = 0.05f;
    private const float CAMERAYDOWNMOVE = 0.5f;
    private const float MAXUPCAMERAYMOVE = 5f;
    private const float MAXDOWNCAMERAYMOVE = 25f;


    private string _playerTag = "Player";
    // Start is called before the first frame update
    void Awake() {
        _camposiChangeY = CAMPOSIY;
        SceneStart();

    }

    // Update is called once per frame
    void Update() {


        if (_isGameStart) {
            CameeeraRank(true);
        }
    }
    public void SceneStart() {
        _isStart = false;
        if (!_isStart) {
            //ランキングの配列にfrogsのに入っているオブジェクトを入れる
            while (_rankingValue <= 3) {
                if (_ranking.Count == 4) {
                    _ranking[_rankingValue] = _frogs[_rankingValue];
                    _rbs[_rankingValue] = _frogs[_rankingValue].GetComponent<Rigidbody2D>();

                } else {
                    _ranking.Add(_frogs[_rankingValue]);
                    _rbs.Add(_frogs[_rankingValue].GetComponent<Rigidbody2D>());
                }


                if (_rankingValue == 0) {
                    _firstPosition = _cameraEdgeObject.transform.position.x - _frogs[_rankingValue].transform.position.x;

                    _frogs[_rankingValue].GetComponent<MasterMChangeFrog>().RankChange(FIRST);
                } else if (_rankingValue == 1) {
                    _secondPosition = _cameraEdgeObject.transform.position.x - _frogs[_rankingValue].transform.position.x;
                    if (_frogs[_rankingValue].gameObject.CompareTag("Player")) {
                        _frogs[_rankingValue].gameObject.GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                    }

                } else if (_rankingValue == 2) {
                    _thirdPosition = _cameraEdgeObject.transform.position.x - _frogs[_rankingValue].transform.position.x;
                    if (_frogs[_rankingValue].gameObject.CompareTag("Player")) {
                        _frogs[_rankingValue].gameObject.GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                    }
                } else if (_rankingValue == 3) {
                    _forthPosition = _cameraEdgeObject.transform.position.x - _frogs[_rankingValue].transform.position.x;
                    if (_frogs[_rankingValue].gameObject.CompareTag("Player")) {
                        _frogs[_rankingValue].gameObject.GetComponent<PlayercontrollerScript>().RankChange(FORTH);
                    }
                }
                _rankingValue++;
            }
        }



        _isStart = true;
        _rankingValue = 0;
        _isGameStart = true;

    }

    public void SecondPlayerOn(GameObject player2) {
        GameObject dummy = default;
        _frogs[3] = player2;

        dummy = _frogs[1];
        _frogs[1] = _frogs[3];
        _frogs[3] = dummy;

    }

    public void ThirdPlayerOn(GameObject player3) {
        _frogs[2] = player3;
    }

    public void FirthPlayerOn(GameObject player4) {
        _frogs[3] = player4;
    }
    public void CameeeraRank(bool alive) {
        if (alive) {
            {
                //１位の場所の計算
                _firstPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINFIRST].transform.position.x;

                //２位のカエルが１位のカエルよりも前に行ったら
                if (_firstPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x) {


                    //プレイヤーが一位だったら
                    if (_ranking[ORIGINFIRST].gameObject.CompareTag(_playerTag)) {
                        //プレイヤーを２位に下げる
                        _ranking[ORIGINFIRST].GetComponent<Player2>().RankChange(SECOND);
                    }
                    //プレイヤーが２位だったら
                    if (_ranking[ORIGINSECOND].gameObject.CompareTag(_playerTag)) {
                        //プレイヤーを１位に上げる
                        _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(FIRST);
                    }




                    _dummy = _ranking[ORIGINFIRST];
                    _ranking[ORIGINFIRST] = _ranking[ORIGINSECOND];
                    _ranking[ORIGINSECOND] = _dummy;


                }


                //２位の場所の計算
                _secondPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x;

                //3位のカエルが2位のカエルよりも前に行ったら
                if (_secondPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x) {


                    //プレイヤーが二位だったら
                    if (_ranking[ORIGINSECOND].gameObject.CompareTag(_playerTag)) {
                        //プレイヤーを３位に下げる
                        _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                    }

                    //プレイヤーが3位だったら

                    if (_ranking[ORIGINTHIRD].gameObject.CompareTag(_playerTag)) {
                        //プレイヤーを２位に上げる
                        _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                    }



                    _dummy = _ranking[ORIGINSECOND];
                    _ranking[ORIGINSECOND] = _ranking[ORIGINTHIRD];
                    _ranking[ORIGINTHIRD] = _dummy;


                }






                _thirdPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x;
                //4位のカエルが3位のカエルよりも前に行ったら
                if (_thirdPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINFORTH].transform.position.x) {



                    //プレイヤーが３位だったら
                    if (_ranking[ORIGINTHIRD].gameObject.CompareTag(_playerTag)) {

                        //プレイヤーを４位に下げる
                        _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(FORTH);
                    }

                    //プレイヤーが４位だったら
                    if (_ranking[ORIGINFORTH].gameObject.CompareTag(_playerTag)) {

                        //プレイヤーを３位に上げる
                        _ranking[ORIGINFORTH].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                    }


                    _dummy = _ranking[ORIGINTHIRD];
                    _ranking[ORIGINTHIRD] = _ranking[ORIGINFORTH];
                    _ranking[ORIGINFORTH] = _dummy;


                }




                //カメラの一定の場所まで来たら先頭のプレイヤーを追う
                if (_ranking[ORIGINFIRST].transform.position.x >= this.transform.position.x /*+ CAMERAMOVEVALUE*/) {
                    _camera.transform.position = new Vector3(_ranking[ORIGINFIRST].transform.position.x /*- CAMERAMOVEVALUE*/, _camposiChangeY, CAMPOSIZ);
                }


                //誰かがジャンプしたらカメラを少し上に上げる
                if ((_rbs[ORIGINFIRST].velocity.y > 0 || _rbs[ORIGINTHIRD].velocity.y > 0 || _rbs[ORIGINFORTH].velocity.y > 0 || _rbs[ORIGINFORTH].velocity.y > 0) && !_isDown) {
                    _isUp = true;
                    //少し上に上げる
                    if (this.transform.position.y < CAMPOSIY + MAXUPCAMERAYMOVE) {
                        _camposiChangeY += CAMERAYMOVE;
                        this.transform.position += new Vector3(0, CAMERAYMOVE, 0);
                    }
                } else {
                    //元に戻す
                    if (this.transform.position.y > CAMPOSIY && _isUp) {
                        _camposiChangeY -= CAMERAYMOVE;
                        this.transform.position -= new Vector3(0, CAMERAYMOVE, 0);
                    } else {
                        _isUp = false;
                    }
                }



            }
        } else {
            _isGameStart = false;
        }

    }
}

