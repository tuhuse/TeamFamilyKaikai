using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{

    
    public int _cpunumber = default;
    [Header("canvasmanager")]
    [SerializeField] private SelectCharacter _selectCharacterScript = default;
    [SerializeField] private MoveButtonScript _move;
    [SerializeField] private HumanPeople _human;
    private enum Judges {
    One,
    EveryOne
    }
    private enum Mode {
    Easy,
    Nomal,
    Hard
    }
    private Mode _modes = default;
    private bool _select = false;
    private Judges _judges = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (_select) {
            if (Input.GetButtonDown("1pA") && _judges == Judges.One) {
                Switch();
                _selectCharacterScript.SummonSneak();

            } else if(Input.GetButtonDown("1pA") && _judges == Judges.EveryOne) {
                Switch();
                _human.Selecet(true);
                _human.SelectNumberPeople(true);
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
        } else if(number==2){
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
        _human.SelectNumberPeople(false);
        yield return new WaitForSeconds(1f);
        _move.Diffculty(true);
        _select = true;
    }
    public void JudgeHumanPeople(int judge) {
        if (judge == 0) {
            _judges = Judges.One;
        } else {
            _judges = Judges.EveryOne;
        }
    }
    
}

