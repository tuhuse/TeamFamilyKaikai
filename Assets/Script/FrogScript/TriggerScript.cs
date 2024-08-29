using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
 
    [SerializeField]private GameObject _cpu1;
    
    // Start is called before the first frame update
    void Start() {
   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemy")){
            if (_cpu1.GetComponent<FrogCpu>() != null) {
                if (!_cpu1.GetComponent<FrogCpu>()._isEnemyJump) {
                    _cpu1.GetComponent<FrogCpu>()._isEnemyJump = true;
                    
                }
            } else if (_cpu1.GetComponent<FrogCpuMulti>() != null) {
                if (!_cpu1.GetComponent<FrogCpuMulti>()._isEnemyJump) {

                    _cpu1.GetComponent<FrogCpuMulti>()._isEnemyJump = true;

                }
            } else if (_cpu1.GetComponent<FrogCpuMulti2>() != null) {
                if (!_cpu1.GetComponent<FrogCpuMulti2>()._isEnemyJump) {

                    _cpu1.GetComponent<FrogCpuMulti2>()._isEnemyJump = true;

                }
            }
        }

     
         
           
      

    }
}
