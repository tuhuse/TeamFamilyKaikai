using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterM : MonoBehaviour
{
    [SerializeField] private GameObject _masterMButton;
    private bool _isCommand = false;
    private string _password;
    private string _command;
    // Start is called before the first frame update
    void Start()
    {
        _password = "�㉺RBLB";
    }

    // Update is called once per frame
    void Update()
    {
        float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;
        float crosskey = Input.GetAxis("Debug Vertical");
        
        if ((controllerStick > 0) || (crosskey > 0)) {//��̏���
            _command = "��";
            _isCommand = true;
        }
        if (_isCommand) {
            if ((controllerStick < 0) || (crosskey < 0)) {//���̏���
                _command = _command + "��";
            }
            if (Input.GetButtonDown("RB")){//RB�̏���
                _command = _command + "RB";
            }
            if (Input.GetButtonDown("LB")) {//LB�̏���
                _command = _command + "LB";
            }
        }
        if (_command == "�㉺RBLB") {
            _masterMButton.SetActive(true);
        }
    }
}
