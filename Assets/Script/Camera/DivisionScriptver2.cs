using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionScriptver2 : MonoBehaviour {
    [SerializeField] private int _stageNumber = default;
    [SerializeField] private StageRoopManFixed _roopScript = default;
    // Start is called before the first frame update
    public void GiveNumber() 
    {
        _roopScript.DivisionStageMove(_stageNumber);
    }
}

