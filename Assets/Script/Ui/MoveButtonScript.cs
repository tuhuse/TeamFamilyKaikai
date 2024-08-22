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
    public int _playernumber;// プレイヤー番号（1から始まる
    private int _selectDefficultButton = default;
    private Image _image;
    private Color _color;
    private bool _isButtonMove = false;
    public bool _isFrogMove = true;//動かないときfalse
    private bool _one = false;
    public bool _everyone;
    private bool _difficult = false;
   public bool _next = false;

    private bool _nexts = false;
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
        if ((Input.GetButtonDown("Fire1") && !_isFrogMove)){
            Diffculty(false);
            _difficulty.Selecet(false);
            _isFrogMove = true;
            _selectCharacterScript.SITUATION(false);
            _nexts = false;
        }

        if (controllerStick < 0 && !_isButtonMove&&!_next) //下に行く
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

        if (controllerStick > 0 && !_isButtonMove && !_next) //上に行く
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
        
        if (Input.GetButtonDown("1pA") && !_isWaitSelect && !_isButtonSelect&&!_nexts) {
            if (_situation == Situation.One) {//一人で
                _isFrogMove = false;
                _selectCharacterScript.SITUATION(true);
                _nexts = true;
                _difficulty.Selecet(true);
                Diffculty(true);
                _difficulty.JudgeHumanPeople(0);

            } else {//みんなで
                //_read.StartGame(2);
                _difficulty.Selecet(true);
                _isFrogMove = false;
                _nexts = true;
                Diffculty(true);
                _difficulty.JudgeHumanPeople(1);
                

                //_selectCharacterScript.SummonSneak();

            }

        }
        //}

        SwSituation();
    }
    public void Diffculty(bool returns) {
        if (returns) {
            _difficultButton[0].SetActive(true);
            _difficultButton[1].SetActive(true);
            _difficultButton[2].SetActive(true);
        } else {
            _difficultButton[0].SetActive(false);
            _difficultButton[1].SetActive(false);
            _difficultButton[2].SetActive(false);
        }
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
    public void DifficultButton() {
        _difficult = true;
    }
}
