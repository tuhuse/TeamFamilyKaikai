using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PtidictionTriggerScript : MonoBehaviour
{
    [SerializeField]private GameObject _cpu1;
    [SerializeField]private GameObject _cpu2;
    [SerializeField]private GameObject _cpu3;
  
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
  
    private void OnTriggerEnter2D(Collider2D collision) {

        if (_cpu1.GetComponent<FrogCpu>() != null) {
            if (collision.gameObject.layer == 9 &&
               !_cpu1.GetComponent<FrogCpu>()._isBehindTrigger) {
                _cpu1.GetComponent<FrogCpu>()._isBehindTrigger = true;
            }
        } else if (_cpu1.GetComponent<FrogCpuMulti>() != null) {
            if (collision.gameObject.layer == 9 &&
                  !_cpu1.GetComponent<FrogCpuMulti>()._isBehindTrigger) {
                _cpu1.GetComponent<FrogCpuMulti>()._isBehindTrigger = true;
            }
        }

        if (_cpu2.GetComponent<FrogCpu>() != null) {
           if (collision.gameObject.layer == 9 &&
                !_cpu2.GetComponent<FrogCpu>()._isBehindTrigger) {
                _cpu2.GetComponent<FrogCpu>()._isBehindTrigger = true;
            }
        }else if(_cpu2.GetComponent<FrogCpuMulti>() != null) {
            if (collision.gameObject.layer == 9 &&
                   !_cpu2.GetComponent<FrogCpuMulti>()._isBehindTrigger) {
                _cpu2.GetComponent<FrogCpuMulti>()._isBehindTrigger = true;
            }
        }



        if (_cpu3.GetComponent<FrogCpu>() != null) {
            if (collision.gameObject.layer == 9 &&
                     !_cpu3.GetComponent<FrogCpu>()._isBehindTrigger) {
                _cpu3.GetComponent<FrogCpu>()._isBehindTrigger = true;
            }
        }

    }

}
