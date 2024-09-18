using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Difficulty : MonoBehaviour {
    [SerializeField] private List<GameObject> _difficultButton = new List<GameObject>();
    [SerializeField] private List<Image> _difficultFrog = new List<Image>();
    [SerializeField] private GameObject _difficultText;
    private int _selectDefficultButton = default;
    private Image _image;
    private bool _isButtonMove = false;
    private int _judgenumber;

    [Header("canvasmanager")]
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    //[SerializeField] private MoveButtonScript _move;
    [SerializeField] private HumanPeople _human;
    [SerializeField] private Animator[] _change;
    private enum Mode {
        Easy,
        Nomal,
        Hard,
        Return
    } private enum ModeColor {
        Easy,
        Nomal,
        Hard,
        Return
    }
    private Mode _modes = default;
    private ModeColor _modeColor = default;
    private bool _select = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (_select) {
            #region コントローラーの難易度セレクト
            float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;
            float crosskey = Input.GetAxis("Debug Vertical");
            if (controllerStick == 0&&crosskey==0) {
                _isButtonMove = false;
            }

            if ((controllerStick < 0 && !_isButtonMove) || (crosskey < 0 && !_isButtonMove)) //下に行く
             {


                _selectDefficultButton++;
                if (_difficultButton.Count <= _selectDefficultButton) {
                    _selectDefficultButton = 0;

                }

           
                _isButtonMove = true;

            }

            if (((controllerStick > 0 && !_isButtonMove) || (crosskey > 0 && !_isButtonMove))) //上に行く
            {

            
                _selectDefficultButton--;

                //print(_selectDefficultButton);

                if (0 > _selectDefficultButton) {

                    _selectDefficultButton = _difficultButton.Count - 1;
                }
                
                _isButtonMove = true;
            }

            #endregion



            if ((Input.GetButtonDown("1pA") && _judgenumber == 0 && _modes != Mode.Return) ||
                (Input.GetKeyDown(KeyCode.Return) && _judgenumber == 0 && _modes != Mode.Return)) {
                Switch();
                _selectCharacterScript.SummonSneak();
                Diffculty(false);
                Selecet(false);

            } else if ((Input.GetButtonDown("1pA") && _judgenumber == 1 && _modes != Mode.Return) ||
                (Input.GetKeyDown(KeyCode.Return) && _judgenumber == 1 && _modes != Mode.Return)) {
                Switch();
                _selectCharacterScript.SummonSneak();
                Diffculty(false);
                Selecet(false);

            } else if ((Input.GetButtonDown("1pA") && _modes == Mode.Return) ||
                (Input.GetKeyDown(KeyCode.Return) && _modes == Mode.Return)) {
               
                Diffculty(false);
                _human.Selecet(true);
                _human.SelectNumberPeople(true);
                Selecet(false);
            }
        }
        DiffcultyNumber(_selectDefficultButton);
        ChangeNumber();
        ChangeColor();
    }
    private void ChangeNumber() {
        if (_selectDefficultButton == 0) {
            _modeColor = ModeColor.Easy;
        } else if (_selectDefficultButton == 1) {
            _modeColor = ModeColor.Nomal;
        } else if (_selectDefficultButton == 2) {
            _modeColor = ModeColor.Hard;
        } else if (_selectDefficultButton == 3) {
            _modeColor = ModeColor.Return;
        }
    }
    private void ChangeColor() {
        switch (_modeColor) {
            case ModeColor.Easy:
                _change[0].SetBool("Change", true);
                _change[1].SetBool("Change", false);
                _change[2].SetBool("Change", false);
                _change[3].SetBool("Change", false);

                break;
            case ModeColor.Nomal:
                _change[0].SetBool("Change", false);
                _change[1].SetBool("Change", true);
                _change[2].SetBool("Change", false);
                _change[3].SetBool("Change", false);
                break;
            case ModeColor.Hard:
                _change[0].SetBool("Change", false);
                _change[1].SetBool("Change", false);
                _change[2].SetBool("Change", true);
                _change[3].SetBool("Change", false);
                break;
            case ModeColor.Return:
                _change[0].SetBool("Change", false);
                _change[1].SetBool("Change", false);
                _change[2].SetBool("Change", false);
                _change[3].SetBool("Change", true);
                break;
        }
    }
    public void OnClick() {
        Switch();
    }
    private void Switch() {

        switch (_modes) {
            case Mode.Easy:

                _selectCharacterScript.DiffcultNumberHarf(0);
                break;
            case Mode.Nomal:
                // Normalモードの処理

                _selectCharacterScript.DiffcultNumberHarf(1);
                break;
            case Mode.Hard:
                // Hardモードの処理

                _selectCharacterScript.DiffcultNumberHarf(2);
                break;

        }

    }

    private void DiffcultyNumber(int number) {
        if (number == 0) {
            _difficultFrog[0].enabled = true;
            _difficultFrog[1].enabled = false;
            _difficultFrog[2].enabled = false;
            _modes = Mode.Easy;
        } else if (number == 1) {
            _difficultFrog[0].enabled = false;
            _difficultFrog[1].enabled = true;
            _difficultFrog[2].enabled = false;
            _modes = Mode.Nomal;
        } else if (number == 2) {
            _difficultFrog[0].enabled = false;
            _difficultFrog[1].enabled = false;
            _difficultFrog[2].enabled = true;
            _modes = Mode.Hard;
        } else if (number == 3) {
            _difficultFrog[0].enabled = false;
            _difficultFrog[1].enabled = false;
            _difficultFrog[2].enabled = false;
            _modes = Mode.Return;
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
            _difficultButton[0].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            DiffcultyNumber(0);
            _difficultButton[1].SetActive(true);
            _difficultButton[2].SetActive(true);
            _difficultButton[3].SetActive(true);
            _difficultText.SetActive(true);
        } else {
            _selectDefficultButton = 0;
            _difficultButton[0].SetActive(false);
            _difficultButton[1].SetActive(false);
            _difficultButton[2].SetActive(false);
            _difficultButton[3].SetActive(false);
            _difficultText.SetActive(false);
        }
    }
    public void Judge(int judge) {
        _judgenumber = judge;
    }

}

