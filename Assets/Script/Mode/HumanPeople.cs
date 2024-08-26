using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HumanPeople : MonoBehaviour {
    [Header("canvasmanager")]
    [SerializeField] private List<GameObject> _frogsButton=new List<GameObject>();
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private MoveButtonScript _move;
    [SerializeField] private Difficulty _difficulty;
    private int _humanPeople = default;
    private bool _isMoveButton = false;
    
    private enum Human {
       Two,
       Three,
       Four
    }
    private Human _humans = default;
    private bool _select = false;
    private Image _image;
    
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
     
        if (_select) {
            if (Input.GetButtonDown("Fire1")) {
                
                SelectNumberPeople(false);                             
                _move._next = false;
                Selecet(false);

            }
            float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;
            if (controllerStick == 0) {
                _isMoveButton = false;
            }
            if (controllerStick < 0 && !_isMoveButton) {//��]
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople++;
                if (_frogsButton.Count <= _humanPeople) {

                    _humanPeople = 0;

                }

                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;

            }
            if (controllerStick > 0 && !_isMoveButton) {//��
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople--;
                if (0 > _humanPeople) {

                    _humanPeople = _frogsButton.Count - 1;
                }

                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;

            }
            HumanSwitch();
            if (Input.GetButtonDown("1pA") ) {
                if (_humans == Human.Four) {
                    Switch();
                    SelectNumberPeople(false);
                    _selectCharacterScript.SummonSneak();
                    Selecet(false);


                } else {
                    Switch();
                    _difficulty.Selecet(true);
                    _difficulty.Diffculty(true);
                    SelectNumberPeople(false);
                    Selecet(false);
                }
               
            }

        }

    }
    public void OnClick() {
        Switch();
    }
    private void Switch() {

        switch (_humans) {
            
            case Human.Two:
                _selectCharacterScript.Humans(1);
                break;
            case Human.Three:
                _selectCharacterScript.Humans(2);
                break;
            case Human.Four:
                _selectCharacterScript.Humans(3);
                break;
        }

    }
  private void HumanSwitch() {
        switch (_humanPeople) {
            case 0:
                _humans = Human.Two;
                break;
            case 1:
                _humans = Human.Three;
                break;
            case 2:
                _humans = Human.Four;
                break;
            
        }
       }

  
    public void Selecet(bool on) {
        if (on) {
            StartCoroutine(Boot());
        } 
        else {
            _select = false;
        }
        
          
    }
    
    private IEnumerator Boot() {
        
        yield return new WaitForSeconds(0.1f);
        _select = true;
    }
    public void SelectNumberPeople(bool buttonActive) {
        if (buttonActive) {
            
            _frogsButton[0].SetActive(true);
            _frogsButton[1].SetActive(true);
            _frogsButton[2].SetActive(true);
        } else {
            
           
            _frogsButton[0].SetActive(false);
            _frogsButton[1].SetActive(false);
            _frogsButton[2].SetActive(false);
            
        }

    }
}
