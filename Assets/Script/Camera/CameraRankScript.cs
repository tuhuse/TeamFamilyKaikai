using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRankScript : MonoBehaviour {
    [Header("�J����")] [SerializeField] GameObject _camera = default;//�J��������

    [SerializeField] private GameObject _cameraEdgeObject = default;
    [SerializeField] private GameObject _cameraLowerLimit = default;

    private GameObject _dummy = default;

    [Header("�J�G��4�̂�����")] [SerializeField] private List<GameObject> _frogs = new List<GameObject>();

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


    private string _1Pname = "���낱";
    private string _2Pname = "���낹��";
    private string _3Pname = "����ꂨ";
    private string _4Pname = "�����";

    private const float CAMPOSIY = -10f;
    private const float CAMPOSIZ = -10;
    private const float CAMERAMOVEVALUE = 50;

    //�z��̓[���I���W���̂��߁A0����X�^�[�g
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

    [SerializeField] private StageRoopManFixed _stageRoopManage;
    [SerializeField] private CommentScript _commentText = default;

    [Header("�ʂ����܂����{�C�X"), SerializeField] private AudioClip _frogVoice = default;

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
            //�����L���O�̔z���frogs�̂ɓ����Ă���I�u�W�F�N�g������
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

                    _frogs[_rankingValue].GetComponent<PlayercontrollerScript>().RankChange(FIRST);
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
        string pullOutPlayer = default;
        string bePullOutPlayer = default;
        if (alive) {
            {
                //�P�ʂ̏ꏊ�̌v�Z
                _firstPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINFIRST].transform.position.x;

                //�Q�ʂ̃J�G�����P�ʂ̃J�G�������O�ɍs������
                if (_firstPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x) {


                    //�v���C���[����ʂ�������
                    if (_ranking[ORIGINFIRST].gameObject.CompareTag(_playerTag)) {
                        //�v���C���[���Q�ʂɉ�����
                        _ranking[ORIGINFIRST].GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                    }
                    //�v���C���[���Q�ʂ�������
                    if (_ranking[ORIGINSECOND].gameObject.CompareTag(_playerTag)) {
                        //�v���C���[���P�ʂɏグ��
                        _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(FIRST);
                    }




                    _dummy = _ranking[ORIGINFIRST];
                    _ranking[ORIGINFIRST] = _ranking[ORIGINSECOND];
                    _ranking[ORIGINSECOND] = _dummy;

                    if (_ranking[ORIGINFIRST].name == _1Pname) {
                        pullOutPlayer = "<color=green>" + _ranking[ORIGINFIRST].name + "</color>";
                    } else if (_ranking[ORIGINFIRST].name == _2Pname) {
                        pullOutPlayer = "<color=#F0F121>" + _ranking[ORIGINFIRST].name + "</color>";
                    } else if (_ranking[ORIGINFIRST].name == _3Pname) {
                        pullOutPlayer = "<color=blue>" + _ranking[ORIGINFIRST].name + "</color>";
                    } else if (_ranking[ORIGINFIRST].name == _4Pname) {
                        pullOutPlayer = "<color=#E030C4>" + _ranking[ORIGINFIRST].name + "</color>";
                    }

                    if (_ranking[ORIGINSECOND].name == _1Pname) {
                        bePullOutPlayer = "<color=green>" + _ranking[ORIGINSECOND].name + "</color>";
                    } else if (_ranking[ORIGINSECOND].name == _2Pname) {
                        bePullOutPlayer = "<color=#F0F121>" + _ranking[ORIGINSECOND].name + "</color>";
                    } else if (_ranking[ORIGINSECOND].name == _3Pname) {
                        bePullOutPlayer = "<color=blue>" + _ranking[ORIGINSECOND].name + "</color>";
                    } else if (_ranking[ORIGINSECOND].name == _4Pname) {
                        bePullOutPlayer = "<color=#E030C4>" + _ranking[ORIGINSECOND].name + "</color>";
                    }


                    _commentText.CommentatorCommentChange("������" + pullOutPlayer + "��" + bePullOutPlayer + "�����������A1���ɂ��ǂ�ł܂���!!!", false, _frogVoice);

                    _stageRoopManage.FirstChange(_ranking[ORIGINFIRST]);
                }


                //�Q�ʂ̏ꏊ�̌v�Z
                _secondPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x;

                //3�ʂ̃J�G����2�ʂ̃J�G�������O�ɍs������
                if (_secondPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x) {


                    //�v���C���[����ʂ�������
                    if (_ranking[ORIGINSECOND].gameObject.CompareTag(_playerTag)) {
                        //�v���C���[���R�ʂɉ�����
                        _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                    }

                    //�v���C���[��3�ʂ�������

                    if (_ranking[ORIGINTHIRD].gameObject.CompareTag(_playerTag)) {
                        //�v���C���[���Q�ʂɏグ��
                        _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                    }



                    _dummy = _ranking[ORIGINSECOND];
                    _ranking[ORIGINSECOND] = _ranking[ORIGINTHIRD];
                    _ranking[ORIGINTHIRD] = _dummy;


                    //_commentText.CommentatorCommentChange("�������ƁI" + _ranking[ORIGINSECOND].name + "��" + _ranking[ORIGINTHIRD].name + "���ʂ����܂����I�I�I", false, _frogVoice);
                }






                _thirdPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x;
                //4�ʂ̃J�G����3�ʂ̃J�G�������O�ɍs������
                if (_thirdPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINFORTH].transform.position.x) {



                    //�v���C���[���R�ʂ�������
                    if (_ranking[ORIGINTHIRD].gameObject.CompareTag(_playerTag)) {

                        //�v���C���[���S�ʂɉ�����
                        _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(FORTH);
                    }

                    //�v���C���[���S�ʂ�������
                    if (_ranking[ORIGINFORTH].gameObject.CompareTag(_playerTag)) {

                        //�v���C���[���R�ʂɏグ��
                        _ranking[ORIGINFORTH].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                    }


                    _dummy = _ranking[ORIGINTHIRD];
                    _ranking[ORIGINTHIRD] = _ranking[ORIGINFORTH];
                    _ranking[ORIGINFORTH] = _dummy;

                    //_commentText.CommentatorCommentChange("�������ƁI" + _ranking[ORIGINTHIRD].name + "��" + _ranking[ORIGINFORTH].name + "���ʂ����܂����I�I�I", false, _frogVoice);
                }




                //�J�����̈��̏ꏊ�܂ŗ�����擪�̃v���C���[��ǂ�
                if (_ranking[ORIGINFIRST].transform.position.x >= this.transform.position.x + CAMERAMOVEVALUE) {
                    _camera.transform.position = new Vector3(_ranking[ORIGINFIRST].transform.position.x - CAMERAMOVEVALUE, _camposiChangeY, CAMPOSIZ);
                }


                //�N�����W�����v������J������������ɏグ��
                if ((_rbs[ORIGINFIRST].velocity.y > 0 || _rbs[ORIGINTHIRD].velocity.y > 0 || _rbs[ORIGINFORTH].velocity.y > 0 || _rbs[ORIGINFORTH].velocity.y > 0) && !_isDown) {
                    _isUp = true;
                    //������ɏグ��
                    if (this.transform.position.y < CAMPOSIY + MAXUPCAMERAYMOVE) {
                        _camposiChangeY += CAMERAYMOVE;
                        this.transform.position += new Vector3(0, CAMERAYMOVE, 0);
                    }
                } else {
                    //���ɖ߂�
                    if (this.transform.position.y > CAMPOSIY && _isUp) {
                        _camposiChangeY -= CAMERAYMOVE;
                        this.transform.position -= new Vector3(0, CAMERAYMOVE, 0);
                    } else {
                        _isUp = false;
                    }
                }

                //�N�������Ƃ����ɗ�������

                if (_ranking[0].transform.position.y < _cameraLowerLimit.transform.position.y ||
                   _ranking[1].transform.position.y < _cameraLowerLimit.transform.position.y ||
                   _ranking[2].transform.position.y < _cameraLowerLimit.transform.position.y ||
                   _ranking[3].transform.position.y < _cameraLowerLimit.transform.position.y) {
                    _isDown = true;
                    if (this.transform.position.y > CAMPOSIY - MAXDOWNCAMERAYMOVE) {
                        _camposiChangeY -= CAMERAYDOWNMOVE;
                        this.transform.position -= new Vector3(0, CAMERAYDOWNMOVE, 0);
                    } else {
                    }
                } else {
                    //���ɖ߂�
                    if (this.transform.position.y < CAMPOSIY && _isDown) {
                        _camposiChangeY += CAMERAYDOWNMOVE;
                        this.transform.position += new Vector3(0, CAMERAYDOWNMOVE, 0);
                    } else {
                        _isDown = false;
                    }
                }

            }
        } else {
            _isGameStart = false;
        }

    }
}
