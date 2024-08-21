using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveButtonScript : MonoBehaviour {

    private float _jumpHeight = 200f; // 放物線の最大高さ
    private float _jumpDuration = 1f; // 放物線の運動にかかる時間
    [SerializeField] private AnimationCurve _jumpCurve; // 放物線の高さのカーブ
    [SerializeField] private ButtonMethod _multiButtonMethod;
    [SerializeField] private ButtonMethod _soroButtonMethod;
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private List<GameObject> _difficultButton = new List<GameObject>();
    private float _buttonMethodY;
    private bool _isCurve;
    private bool _isSelect = true;
    private bool _isButtonSelect = false;
    private bool _isWaitSelect = false;
    private string _nameJoyStick = default;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _jumpTimer;
    public int _playernumber;// プレイヤー番号（1から始まる
    private int _selectDefficultButton = default;
    private Image _image;
    private Color _color;
    private bool _isButtonMove = false;
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

    void Update() { // 割り当てられたコントローラーの入力を処理
                    //if (!string.IsNullOrEmpty(_nameJoyStick)) {
                    // 左スティックの水平入力を取得


        #region コントローラーの難易度セレクト
        

        if (Input.GetAxis("L_Stick_Vartical") == 0){
        _isButtonMove = false;
        }

        if (Input.GetAxis("L_Stick_Vartical") > 0 && !_isButtonMove) {
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

        if (Input.GetAxis("L_Stick_Vartical") < 0 && !_isButtonMove) {
            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            _selectDefficultButton--;

            print(_selectDefficultButton);

            if (0 < _selectDefficultButton) 
            {
                
                _selectDefficultButton = _difficultButton.Count;
            }
            _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _isButtonMove = true;
        }
        #endregion
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
        SwSituation();

        if (Input.GetButtonDown("1pA") && !_isWaitSelect && !_isButtonSelect) {
            if (_situation == Situation.One) {
                _selectCharacterScript.SITUATION(true);
                //_selectCharacterScript.SummonSneak();
                _difficultButton[0].SetActive(true);
                _difficultButton[1].SetActive(true);
                _difficultButton[2].SetActive(true);

            } else {
                //_read.StartGame(2);
                _selectCharacterScript.SITUATION(false);
                _selectCharacterScript.SummonSneak();

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
            // 放物線の運動
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;

        }
        if (_jumpTimer <= 0f) {
            // 移動完了
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
            // 放物線の運動
            float t = 1 - (_jumpTimer / _jumpDuration);
            float height = _jumpCurve.Evaluate(t) * _jumpHeight;
            Vector3 newPosition = Vector3.Lerp(_startPosition, _targetPosition + new Vector3(0, 70), t) + (Vector3.up * height);
            transform.position = newPosition;
            _isSelect = false;

        }
        if (_jumpTimer <= 0f) {
            // 移動完了
            StartCoroutine(Cool2());
            _isCurve = false;
            _isSelect = true;
            _soroButtonMethod.Landing();

            _jumpTimer = _jumpDuration;

        }
    }
}
