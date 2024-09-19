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
        _password = "ã‰ºRBLB";
    }

    // Update is called once per frame
    void Update()
    {
        float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;
        float crosskey = Input.GetAxis("Debug Vertical");
        
        if ((controllerStick > 0) || (crosskey > 0)) {//ã‚Ìˆ—
            _command = "ã";
            _isCommand = true;
        }
        if (_isCommand) {
            if ((controllerStick < 0) || (crosskey < 0)) {//‰º‚Ìˆ—
                _command = _command + "‰º";
            }
            if (Input.GetButtonDown("RB")){//RB‚Ìˆ—
                _command = _command + "RB";
            }
            if (Input.GetButtonDown("LB")) {//LB‚Ìˆ—
                _command = _command + "LB";
            }
        }
        if (_command == "ã‰ºRBLB") {
            _masterMButton.SetActive(true);
        }
    }
}
