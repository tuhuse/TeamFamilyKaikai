using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveButtonScript : MonoBehaviour {

    private float _jumpHeight = 200f; // 放物線の最大高さ
    private float _jumpDuration = 0.7f; // 放物線の運動にかかる時間
    [SerializeField] private AnimationCurve _jumpCurve; // 放物線の高さのカーブ
    [SerializeField] private ButtonMethod _multiButtonMethod;
    [SerializeField] private ButtonMethod _soroButtonMethod;
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private HumanPeople _human;
    
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
    [SerializeField] private Difficulty _difficulty;
    private Image _image;
    private Color _color;
    private Animator _anim;
   
   
    private bool _one = false;
    public bool _everyone;
    private bool _difficult = false;
   public bool _next = false;

    
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
        _anim = this.gameObject.GetComponent<Animator>();
    }

    void Update() {

        if (!_next) {
            float horizontalInput = Input.GetAxis("1pLstickHorizontal");//右移動
            if (horizontalInput > 0 && !_isButtonSelect && !_isWaitSelect) {
                _isButtonSelect = true;
                _isWaitSelect = true;
                
                if (_situation == Situation.Every) {

                    _soroButtonMethod.OnButtonClick();

                } else if (_situation == Situation.One) {

                    _multiButtonMethod.OnButtonClick();

                }


            } else if (horizontalInput < 0 && !_isButtonSelect && !_isWaitSelect)//左移動
                                                                                 {
                
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

            if (Input.GetButtonDown("1pA") && !_isWaitSelect && !_isButtonSelect ) {
                if (_situation == Situation.One) {//一人で
                    
                    _selectCharacterScript.SITUATION(true);
                    _difficulty.Selecet(true);
                    _difficulty.Diffculty(true);
                    _next = true;
                    _difficulty.Judge(0);
                } else {//みんなで
                        //_read.StartGame(2);                
                    _human.Selecet(true);
                    _human.SelectNumberPeople(true);
                    _next = true;
                    _difficulty.Judge(1);
                    //_selectCharacterScript.SummonSneak();

                }

            }
            //}

            SwSituation();
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
            //_anim.SetBool("MainMenuJump", true);
            _isSelect = false;

        }
        if (_jumpTimer <= 0f) {
            // 移動完了
            StartCoroutine(Cool());
            _isCurve = false;
            _isSelect = true;
            _multiButtonMethod.Landing();
            //_anim.SetBool("MainMenuJump", false);
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
            //_anim.SetBool("MainMenuJump", true);
            _isSelect = false;
            
        }
        if (_jumpTimer <= 0f) {
            // 移動完了
            StartCoroutine(Cool2());
            _isCurve = false;
            _isSelect = true;
            _soroButtonMethod.Landing();
            //_anim.SetBool("MainMenuJump", false);
            _jumpTimer = _jumpDuration;

        }
    }

}
