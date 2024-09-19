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
              
            case Command.Null://�B���R�}���h1��
                Command1();
                break;
            case Command.Up:  //�B���R�}���h2��
                Command2();
                break;
            case Command.Down://�B���R�}���h3��
                Command3();
                break;
            case Command.RB:  //�B���R�}���h4��
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
        if (_crosskey > 0) {//��̏���
            StartCoroutine(WaitUp());
        }
    }
    private void Command2() {
        if (_crosskey < 0) {//���̏���

            StartCoroutine(WaitDown());
        } else if (_crosskey > 0 || Input.GetButtonDown("RB") || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }

    }
    private void Command3() {
        if (Input.GetButtonDown("RB")) {//RB�̏���
            _command = Command.RB;
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("LB")) {
            _command = Command.Null;
        }
    }
    private void Command4() {
        if (Input.GetButtonDown("LB")) {//LB�̏���
            _command = Command.LB;
        } else if (_crosskey > 0 || _crosskey < 0 || Input.GetButtonDown("RB")) {
            _command = Command.Null;
        }
    }
    
}
