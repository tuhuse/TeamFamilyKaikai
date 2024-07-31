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
    [SerializeField] private Mode[] _modes = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClick() {
        Switch();
    }
    private void Switch() {
        foreach (Mode mode in _modes) {
            switch (mode) {
                case Mode.Easy:
                    // Easy���[�h�̏���
                    _cpunumber = 1;
                    break;
                case Mode.Nomal:
                    // Normal���[�h�̏���
                    _cpunumber = 2;
                    break;
                case Mode.Hard:
                    // Hard���[�h�̏���
                    _cpunumber = 3;
                    break;
                
            }
        }
    }
}

