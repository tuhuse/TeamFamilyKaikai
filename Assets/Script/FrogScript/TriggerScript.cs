using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
 
    [SerializeField] GameObject _cpu1;
    [SerializeField] GameObject _cpu2;
    [SerializeField] GameObject _cpu3;
  
    // Start is called before the first frame update
    void Start() {
   
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (_cpu1.GetComponent<FrogCpu>() != null) {
            if (collision.gameObject.CompareTag("Enemy") &&
                 !_cpu1.GetComponent<FrogCpu>()._isEnemyJump) {

                _cpu1.GetComponent<FrogCpu>()._isEnemyJump = true;

            }
        } else if (collision.gameObject.GetComponent<FrogCpuMulti>()!= null) {
            if (collision.gameObject.CompareTag("Enemy") &&
                    !_cpu1.GetComponent<FrogCpuMulti>()._isEnemyJump) {

                _cpu1.GetComponent<FrogCpuMulti>()._isEnemyJump = true;

            }
        }
        if (_cpu2.GetComponent<FrogCpu>() != null) {
            if (collision.gameObject.CompareTag("Enemy") &&
             !_cpu2.GetComponent<FrogCpu>()._isEnemyJump) {

                _cpu2.GetComponent<FrogCpu>()._isEnemyJump = true;

            }
        } else if (collision.gameObject.GetComponent<FrogCpuMulti>()!= null) {
            if (collision.gameObject.CompareTag("Enemy") &&
                 !_cpu2.GetComponent<FrogCpuMulti>()._isEnemyJump) {

                _cpu2.GetComponent<FrogCpuMulti >()._isEnemyJump = true;

            }
        }
        if (_cpu3.activeSelf) {
            if (collision.gameObject.CompareTag("Enemy") &&
               !_cpu3.GetComponent<FrogCpu>()._isEnemyJump) {
                _cpu3.GetComponent<FrogCpu>()._isEnemyJump = true;
            }
        }

    }
}
