using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MasterM : MonoBehaviour {
    [SerializeField] private GameObject _masterMButton;
    [SerializeField] private GameObject _menuBackGround;
    private bool _isCommand = false;
    private bool _isDown = false;
    private bool _isUp = true;
    private float _crosskey;

    private enum Command {
        Null,
        Up,
        Down,
        RB,
        LB

    }
    private Command _command;
    // Start is called before the first frame update
    void Start() {
       
    }

    // Update is called once per frame
    void Update() {
        print(_command);
        _crosskey = Input.GetAxis("Debug Vertical");
        switch (_command) {
              
            case Command.Null://隠しコマンド1個目
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
                _menuBackGround.SetActive(false);
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
        if (_crosskey > 0) {//上の処理
            StartCoroutine(WaitUp());
        }
    }
    private void Command2() {
        if (_crosskey < 0) {//下の処理

            StartCoroutine(WaitDown());
        } else if (_crosskey > 0 || Input.GetButtonDown("RB") || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }

    }
    private void Command3() {
        if (Input.GetButtonDown("RB")) {//RBの処理
            _command = Command.RB;
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }
    }
    private void Command4() {
        if (Input.GetButtonDown("LB")) {//LBの処理
            _command = Command.LB;
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("RB")) {
            _command = Command.Null;
        }
    }
    
}
