using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class MasterM : MonoBehaviour {
    [SerializeField] private GameObject _masterMButton;
    [SerializeField] private GameObject _menuBackGround;
    [SerializeField] private GameObject _challengeText;
    [SerializeField] private GameObject _menu;
    [SerializeField] private List<GameObject> _yesNoButton = new List<GameObject>();
    [SerializeField] private ChallengeTextScript _challengeTextScript;
    private int _buttonValue = 0;
    private float _crosskey;
    private bool _isButton  = false;
    private bool _isSousa = false;

    private enum Command {
        Null,
        Up,
        Down,
        RB,
        LB,
        selectYesNo,
        MasterM

    }
    private Command _command;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {


        _crosskey = Input.GetAxis("Debug Vertical");
        switch (_command) {

            case Command.Null://隠しコマンド1個目
                _isSousa = false;
                Command1();
                break;
            case Command.Up:  //隠しコマンド2個目
                Command2();
                break;
            case Command.Down://隠しコマンド3個目
                Command3();
                break;
            case Command.RB:  //隠しコマンド4個目
                Command4();
                break;
            case Command.LB:
                _masterMButton.SetActive(true);
                _challengeText.SetActive(true);
                _challengeTextScript.RestartText();
                _menu.SetActive(false);
                _command = Command.selectYesNo;
                break;
            case Command.selectYesNo:
                if (_isSousa) {
                    if (Input.GetAxis("1pLstickHorizontal") < 0 && _isButton)//左
                   {
                        _isButton = false;
                        _yesNoButton[_buttonValue].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

                        _buttonValue = 0;

                        _yesNoButton[_buttonValue].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    }
                    if (Input.GetAxis("1pLstickHorizontal") > 0 && _isButton)//右
                   {
                        _isButton = false;
                        _yesNoButton[_buttonValue].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

                        _buttonValue = 1;

                        _yesNoButton[_buttonValue].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    }
                    if (Input.GetAxis("1pLstickHorizontal") == 0) {
                        _isButton = true;
                    }
                    if (_buttonValue == 1 && Input.GetButtonDown("1pA"))//no
                                                                        {
                        _masterMButton.SetActive(false);
                        _challengeText.SetActive(false);
                        _menu.SetActive(true);
                        _command = Command.Null;
                    }
                    if (_buttonValue == 0 && Input.GetButtonDown("1pA")) //yes
                       {
                        _isSousa = false;
                        _command = Command.Null;
                        _challengeText.SetActive(false);
                        _command = Command.MasterM;
                    }
                }
               

                break;
            case Command.MasterM:
                break;
        }



    }
    private IEnumerator WaitUp() {
        yield return new WaitForSeconds(0.1f);
        _command = Command.Up;
    }
    private IEnumerator WaitDown() {
        yield return new WaitForSeconds(0.1f);
        _command = Command.Down;
    }
    private void Command1() {
        if (_crosskey > 0 || Input.GetKeyDown(KeyCode.UpArrow)) {//上の処理
            StartCoroutine(WaitUp());
        }
    }
    private void Command2() {
        if (_crosskey < 0 || Input.GetKeyDown(KeyCode.DownArrow)) {//下の処理

            StartCoroutine(WaitDown());
        } else if (_crosskey > 0 || Input.GetButtonDown("RB") || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }

    }
    private void Command3() {
        if (Input.GetButtonDown("RB") || Input.GetKeyDown(KeyCode.RightArrow)) {//RBの処理
            _command = Command.RB;
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }
    }
    private void Command4() {

        if (Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.LeftArrow)) {//LBの処理
            _command = Command.LB;
            _menu.SetActive(false);
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("RB")) {
            _command = Command.Null;
        }
    }

    public void Button() {
        _isSousa = true;
    }

}
