using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveButtonScript : MonoBehaviour {

    private float _jumpHeight = 200f; // �������̍ő卂��
    private float _jumpDuration = 1f; // �������̉^���ɂ����鎞��
    [SerializeField] private AnimationCurve _jumpCurve; // �������̍����̃J�[�u
    [SerializeField] private ButtonMethod _multiButtonMethod;
    [SerializeField] private ButtonMethod _soroButtonMethod;
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private List<GameObject> _difficultButton = new List<GameObject>();
    [SerializeField] private Difficulty _difficulty;
    private float _buttonMethodY;
    private bool _isCurve;
    private bool _isSelect = true;
    private bool _isButtonSelect = false;
    private bool _isWaitSelect = false;
    private string _nameJoyStick = default;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _jumpTimer;
    public int _playernumber;// �v���C���[�ԍ��i1����n�܂�
    private int _selectDefficultButton = default;
    private Image _image;
    private Color _color;
    private bool _isButtonMove = false;
    private bool _isFrogMove = true;//�����Ȃ��Ƃ�false
    private bool _one = false;
    private bool _everyone;
    private bool _difficult = false;
    
    //[SerializeField] private ReadControll _read;
    [SerializeField] private GameObject _menuImage = default;
    private enum Situation {
        One,
        Every
    }
    private Situation _situation = default;
    void Start() {
        _jumpTimer = 1f;
        _situation = Situation.One;
        _image = GetComponent<Image>();
        _color = _image.color;
        //string[] joyStickName = Input.GetJoystickNames();

        //if (_playernumber <= joyStickName.Length && _playernumber > 0) {
        //    _nameJoyStick = joyStickName[_playernumber - 1];
        //    Debug.Log("Player " + _playernumber + " is assigned to " + _nameJoyStick);
        //} else {
        //    Debug.LogWarning("No joystick for Player " + _playernumber);
        //}
    }

    void Update() { // ���蓖�Ă�ꂽ�R���g���[���[�̓��͂�����
                    //if (!string.IsNullOrEmpty(_nameJoyStick)) {
                    // ���X�e�B�b�N�̐������͂��擾


        #region �R���g���[���[�̓�Փx�Z���N�g
        float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;
        if (_selectDefficultButton == 0) {
            _difficulty.DiffcultyNumber(0);
        }
        if (_selectDefficultButton == 1) {
            _difficulty.DiffcultyNumber(1);
        }
        if (_selectDefficultButton == 2) {
            _difficulty.DiffcultyNumber(2);
        }

        if (controllerStick == 0){
        _isButtonMove = false;
        }
        if (Input.GetButtonDown("Fire1") && !_isFrogMove&&_one) {
            _difficultButton[0].SetActive(false);
            _difficultButton[1].SetActive(false);
            _difficultButton[2].SetActive(false);
            _difficulty.Selecet(false);
            _isFrogMove = true;
        }

        if (controllerStick < 0 && !_isButtonMove) //���ɍs��
         {
            
            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            _selectDefficultButton++;
            if (_difficultButton.Count <= _selectDefficultButton) 
                {
                print(_selectDefficultButton);
                _selectDefficultButton = 0;
                
            }

            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _isButtonMove = true;
           
        }

        if (controllerStick > 0 && !_isButtonMove) //��ɍs��
        {

            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            _selectDefficultButton--;

            //print(_selectDefficultButton);

            if (0 > _selectDefficultButton) 
            {
                
                _selectDefficultButton = _difficultButton.Count -1;
            }
            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _isButtonMove = true;
        }
       
        #endregion

        if (_isFrogMove) {
            float horizontalInput = Input.GetAxis("1pLstickHorizontal");
            if (horizontalInput > 0 && !_isButtonSelect && !_isWaitSelect) {
                _isButtonSelect = true;
                _isWaitSelect = true;
                if (_situation == Situation.Every) {

                    _soroButtonMethod.OnButtonClick();

                } else if (_situation == Situation.One) {

                    _multiButtonMethod.OnButtonClick();

                }


            } else if (horizontalInput < 0 && !_isButtonSelect && !_isWaitSelect) {

                _isButtonSelect = true;
                _isWaitSelect = true;
                if (_situation == Situation.Every) {

                    _soroButtonMethod.OnButtonClick();
                } else if (_situation == Situation.One) {

                    _multiButtonMethod.OnButtonClick();
                }
            } else if (horizontalInput == 0) {
                _isButtonSelect = false;
            }
        }
      
        SwSituation();
        
        if (Input.GetButtonDown("1pA") && !_isWaitSelect && !_isButtonSelect) {
            if (_situation == Situation.One) {//��l��
                _isFrogMove = false;
                _selectCharacterScript.SITUATION(true);
                _one = true;
                _difficulty.Selecet(true);
                _difficultButton[0].SetActive(true);
                _difficultButton[1].SetActive(true);
                _difficultButton[2].SetActive(true);

            } else {//�݂�Ȃ�
                //_read.StartGame(2);
                _difficulty.Selecet(true);
                _isFrogMove = false;
                _difficultButton[0].SetActive(true);
                _difficultButton[1].SetActive(true);
                _difficultButton[2].SetActive(true);

                //_selectCharacterScript.SummonSneak();

            }

        }
        //}

        SwSituation();
    }

    public void JumpToPosition(Vector3 target) {
        if (_isSelect) {
            _startPosition = transform.position;
            _targetPosition = target;
            _jumpTimer = _jumpDuration;

            _isCurve = true;

        }

    }
    private void SwSituation() {
        switch (_situation) {
            case Situation.One:
                RightSpeed();

                break;
            case Situation.Every:

                LeftSpeed();
                break;
        }

    }
    private IEnumerator Cool() {
        yield return new WaitForSeconds(1f);
        _situation = Situation.Every;
        _isWaitSelect = false;
        ;
    }
    private IEnumerator Cool2() {
        yield return new WaitForSeconds(1f);
        _situation = Situation.One;
        _isWaitSelect = false;
    }

    private void RightSpeed() {

        if (_isCurve) {

            transform.rotation = Quaternion.Euler(0, 0, 0);
            _jumpTimer -= Time.deltaTime;
            // �������̉^��
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;

        }
        if (_jumpTimer <= 0f) {
            // �ړ�����
            StartCoroutine(Cool());
            _isCurve = false;
            _isSelect = true;
            _multiButtonMethod.Landing();

            _jumpTimer = _jumpDuration;

        }
    }
    private void LeftSpeed() {

        if (_isCurve) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _jumpTimer -= Time.deltaTime;
            // �������̉^��
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;

        }
        if (_jumpTimer <= 0f) {
            // �ړ�����
            StartCoroutine(Cool2());
            _isCurve = false;
            _isSelect = true;
            _soroButtonMethod.Landing();

            _jumpTimer = _jumpDuration;

        }
    }
    public void DifficultButton() {
        _difficult = true;
    }
}
