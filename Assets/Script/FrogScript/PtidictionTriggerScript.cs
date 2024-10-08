using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtidictionTriggerScript : MonoBehaviour {
    [SerializeField] private GameObject _cpu1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 9) {
            if (_cpu1.GetComponent<FrogCpu>() != null) {
                if (!_cpu1.GetComponent<FrogCpu>()._isBehindTrigger) {
                    _cpu1.GetComponent<FrogCpu>()._isBehindTrigger = true;
                }
            } else if (_cpu1.GetComponent<FrogCpuMulti>() != null) {
                if (!_cpu1.GetComponent<FrogCpuMulti>()._isBehindTrigger) {
                    _cpu1.GetComponent<FrogCpuMulti>()._isBehindTrigger = true;
                }
            } else if (_cpu1.GetComponent<FrogCpuMulti2>() != null) {
                if (!_cpu1.GetComponent<FrogCpuMulti2>()._isBehindTrigger) {
                    _cpu1.GetComponent<FrogCpuMulti2>()._isBehindTrigger = true;
                }
            }
        }



    }

}

