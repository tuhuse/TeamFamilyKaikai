using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{

    [Header("canvasmanager")]
    [SerializeField] private SelectCharacter _select;
    public int _cpunumber = default;
    private enum Mode {
    Easy,
    Nomal,
    Hard
    }
    private Mode _modes = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnClick() {
        Switch();
    }
    public void Switch() {
  
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
    public void EasyMode() {
      _modes = Mode.Easy;
    }
    public void NoramlMode() {
        _modes = Mode.Nomal;
    }
    public void HardMode() {
        _modes = Mode.Hard;
    }
}

