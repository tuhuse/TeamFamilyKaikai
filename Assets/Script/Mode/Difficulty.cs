using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Difficulty : MonoBehaviour {
    [SerializeField] private List<GameObject> _difficultButton = new List<GameObject>();
    private int _selectDefficultButton = default;
    private Image _image;
    private bool _isButtonMove = false;
    private int _judgenumber;
    public int _cpunumber = default;
    [Header("canvasmanager")]
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private MoveButtonScript _move;
    [SerializeField] private HumanPeople _human;


    private enum Mode {
        Easy,
        Nomal,
        Hard
    }
    private Mode _modes = default;
    private bool _select = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (_select) {
            #region コントローラーの難易度セレクト
            float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;

            if (controllerStick == 0) {
                _isButtonMove = false;
            }
            if ((Input.GetButtonDown("Fire1"))) {
                if (_judgenumber == 0) {
                    _move._next = false;
                    _selectCharacterScript.SITUATION(false);
                    Diffculty(false);
                    Selecet(false);
                } else {
                    Diffculty(false);
                    _human.Selecet(true);
                    _human.SelectNumberPeople(true);
                    Selecet(false);
                }



            }

            if (controllerStick < 0 && !_isButtonMove) //下に行く
             {

                _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _selectDefficultButton++;
                if (_difficultButton.Count <= _selectDefficultButton) {
                    print(_selectDefficultButton);
                    _selectDefficultButton = 0;

                }

                _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isButtonMove = true;

            }

            if (controllerStick > 0 && !_isButtonMove) //上に行く
            {

                _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _selectDefficultButton--;

                //print(_selectDefficultButton);

                if (0 > _selectDefficultButton) {

                    _selectDefficultButton = _difficultButton.Count - 1;
                }
                _difficultButton[_selectDefficultButton].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isButtonMove = true;
            }

            #endregion

            if (Input.GetButtonDown("1pA")&&_judgenumber==0) {
                Switch();
                _selectCharacterScript.SummonSneak();
                Diffculty(false);
                Selecet(false);

            } else if (Input.GetButtonDown("1pA") && _judgenumber == 1) {
                Switch();
                _selectCharacterScript.SummonSneak();
                Diffculty(false);             
                Selecet(false);

            }
        }
    }
    public void OnClick() {
        Switch();
    }
    private void Switch() {

        switch (_modes) {
            case Mode.Easy:
                // Easyモードの処理
                _cpunumber = 1;
                break;
            case Mode.Nomal:
                // Normalモードの処理
                _cpunumber = 2;
                break;
            case Mode.Hard:
                // Hardモードの処理
                _cpunumber = 3;
                break;

        }

    }

    public void DiffcultyNumber(int number) {
        if (number == 0) {
            _modes = Mode.Easy;
        } else if (number == 1) {
            _modes = Mode.Nomal;
        } else if (number == 2) {
            _modes = Mode.Hard;
        }
    }
    public void Selecet(bool on) {
        if (on) {
            StartCoroutine(Sine());
        } else {
            _select = false;

        }

    }
    private IEnumerator Sine() {
        yield return new WaitForSeconds(0.01f);
        _select = true;
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
    public void Judge(int judge) {
        _judgenumber = judge;
    }

}

