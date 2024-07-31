using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRankScript : MonoBehaviour
{
    [Header("�J����")] [SerializeField] GameObject _camera = default;//�J��������

    [SerializeField] GameObject _cameraEdgeObject = default;

    private GameObject _dummy = default;

    [Header("�J�G��4�̂�����")] [SerializeField] private List<GameObject> _frogs = new List<GameObject>();

    [SerializeField] List<Rigidbody2D> _rbs = new List<Rigidbody2D>();

    [SerializeField]public List<GameObject> _ranking = new List<GameObject>();
    private int _rankingValue = 0;

    private bool _isGameStart = false;
    private bool _isUp = false;
    private bool _isStart = false;


    //private float _playerDistance = default;
    private float _camposiChangeY = default;


    private float _firstPosition = default;
    private float _secondPosition = default;
    private float _thirdPosition = default;
    private float _forthPosition = default;


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
    private const float MAXCAMERAYMOVE = 5f;

    private const int PLAYERLAYER = 12;
    private const int SECONDPLAYERLAYER = 13;

    // Start is called before the first frame update
    void Start()
    {
        _camposiChangeY = CAMPOSIY;
        SceneStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameStart) 
        {
            //�P�ʂ̏ꏊ�̌v�Z
            _firstPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINFIRST].transform.position.x;

            //�Q�ʂ̃J�G�����P�ʂ̃J�G�������O�ɍs������
            if (_firstPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x) 
            {
                _firstPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x;


                //�v���C���[����ʂ�������
                if (_ranking[ORIGINFIRST].gameObject.layer==PLAYERLAYER)
                {
                    //�v���C���[���Q�ʂɉ�����
                    _ranking[ORIGINFIRST].GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                }
                //�v���C���[���Q�ʂ�������
                else if (_ranking[ORIGINSECOND].gameObject.layer==PLAYERLAYER) 
                {
                    //�v���C���[���P�ʂɏグ��
                    _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(FIRST);
                }


                //�v���C���[2����ʂ�������
                if (_ranking[ORIGINFIRST].gameObject.layer == SECONDPLAYERLAYER) 
                {
                    //�v���C���[2���Q�ʂɉ�����
                    _ranking[ORIGINFIRST].GetComponent<Player2>().RankChange(SECOND);
                }
                //�v���C���[2���Q�ʂ�������
                else if (_ranking[ORIGINSECOND].gameObject.layer == SECONDPLAYERLAYER) 
                {
                    //�v���C���[2���P�ʂɏグ��
                    _ranking[ORIGINSECOND].GetComponent<Player2>().RankChange(FIRST);
                }
                _dummy = _ranking[ORIGINFIRST];
                _ranking[ORIGINFIRST] = _ranking[ORIGINSECOND];
                _ranking[ORIGINSECOND] = _dummy;
            }

            //�Q�ʂ̏ꏊ�̌v�Z
            _secondPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINSECOND].transform.position.x;

            //3�ʂ̃J�G����2�ʂ̃J�G�������O�ɍs������
            if (_secondPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x) 
            {
                _secondPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x;


                //�v���C���[����ʂ�������
                if (_ranking[ORIGINSECOND].gameObject.layer == PLAYERLAYER) 
                {
                    //�v���C���[���R�ʂɉ�����
                   _ranking[ORIGINSECOND].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                }
                //�v���C���[��3�ʂ�������
                else if (_ranking[ORIGINTHIRD].gameObject.layer == PLAYERLAYER) 
                {
                    //�v���C���[���Q�ʂɏグ��
                   _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(SECOND);
                }

                //�v���C���[2����ʂ�������
                if (_ranking[ORIGINSECOND].gameObject.layer==SECONDPLAYERLAYER) {
                    //�v���C���[2���R�ʂɉ�����
                    _ranking[ORIGINSECOND].GetComponent<Player2>().RankChange(THIRD);
                }
                //�v���C���[2��3�ʂ�������
                else if (_ranking[ORIGINTHIRD].gameObject.layer == SECONDPLAYERLAYER) {
                    //�v���C���[2���Q�ʂɏグ��
                    _ranking[ORIGINTHIRD].GetComponent<Player2>().RankChange(SECOND);
                }

                _dummy = _ranking[ORIGINSECOND];
                _ranking[ORIGINSECOND] = _ranking[ORIGINTHIRD];
                _ranking[ORIGINTHIRD] = _dummy;



            }

            //�R�ʂ̏ꏊ�̌v�Z
            _thirdPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x;

            //4�ʂ̃J�G����3�ʂ̃J�G�������O�ɍs������
            if (_thirdPosition >= _cameraEdgeObject.transform.position.x - _ranking[ORIGINFORTH].transform.position.x) 
            {
                _thirdPosition = _cameraEdgeObject.transform.position.x - _ranking[ORIGINTHIRD].transform.position.x;


                //�v���C���[���R�ʂ�������
                if (_ranking[ORIGINTHIRD].gameObject.layer==PLAYERLAYER)
                {
                    //�v���C���[���S�ʂɉ�����
                    _ranking[ORIGINTHIRD].GetComponent<PlayercontrollerScript>().RankChange(FORTH);
                }
                //�v���C���[���S�ʂ�������
                else if (_ranking[ORIGINFORTH].gameObject.layer==PLAYERLAYER) 
                {
                    //�v���C���[���R�ʂɏグ��
                   _ranking[ORIGINFORTH].GetComponent<PlayercontrollerScript>().RankChange(THIRD);
                }


                //�v���C���[2���R�ʂ�������
                if (_ranking[ORIGINTHIRD].gameObject.layer==SECONDPLAYERLAYER) 
                {
                    //�v���C���[2���S�ʂɉ�����
                    _ranking[ORIGINTHIRD].GetComponent<Player2>().RankChange(FORTH);
                }
                //�v���C���[2���S�ʂ�������
                else if (_ranking[ORIGINFORTH].gameObject.layer==SECONDPLAYERLAYER) 
                {
                    //�v���C���[2���R�ʂɏグ��
                    _ranking[ORIGINFORTH].GetComponent<Player2>().RankChange(THIRD);
                }

                _dummy = _ranking[ORIGINTHIRD];
                _ranking[ORIGINTHIRD] = _ranking[ORIGINFORTH];
                _ranking[ORIGINFORTH] = _dummy;
            }


            //�J�����̈��̏ꏊ�܂ŗ�����擪�̃v���C���[��ǂ�
            if (_ranking[ORIGINFIRST].transform.position.x >= this.transform.position.x + CAMERAMOVEVALUE) 
            {
                _camera.transform.position = new Vector3(_ranking[ORIGINFIRST].transform.position.x - CAMERAMOVEVALUE, _camposiChangeY, CAMPOSIZ);
            }


            //�N�����W�����v������J������������ɏグ��
            if (_rbs[ORIGINFIRST].velocity.y > 0 || _rbs[ORIGINSECOND].velocity.y > 0 || _rbs[ORIGINTHIRD].velocity.y > 0 || _rbs[ORIGINFORTH].velocity.y > 0)
            {
                _isUp = true;
                //������ɏグ��
                if (this.transform.position.y < CAMPOSIY + MAXCAMERAYMOVE) 
                {
                    _camposiChangeY += CAMERAYMOVE;
                    this.transform.position += new Vector3(0, CAMERAYMOVE, 0);
                }
            } 
            else 
            {
                //���ɖ߂�
                if (this.transform.position.y > CAMPOSIY && _isUp) 
                {
                    _camposiChangeY -= CAMERAYMOVE;
                    this.transform.position -= new Vector3(0, CAMERAYMOVE, 0);
                }
            }

        }
    }
    public void SceneStart() 
    {
        if (!_isStart) 
        {
            //�����L���O�̔z���frogs�̂ɓ����Ă���I�u�W�F�N�g������
            while (_rankingValue <= 3) {

               
                _ranking.Add(_frogs[_rankingValue]);
                _rbs.Add(_frogs[_rankingValue].GetComponent<Rigidbody2D>());

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
                } else if (_rankingValue == 3) {
                    _forthPosition = _cameraEdgeObject.transform.position.x - _frogs[_rankingValue].transform.position.x;
                }
                _rankingValue++;
            }
        }
        else
        {
            while (_rankingValue <= 3) 
            {
                _ranking[_rankingValue] = _frogs[_rankingValue];
                _rankingValue++;
            }
        }


        _isStart = true;
        _rankingValue = 0;
        _isGameStart = true;
    }

    public void SecondPlayerOn(GameObject player2) 
    {
        _frogs[1] = player2;
    }
}
