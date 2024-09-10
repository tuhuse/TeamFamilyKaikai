using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {
    //  //障害物に当たった時の挙動
    private float _speedDown = 60f;
    

    private void OnCollisionEnter2D(Collision2D collision) {
        //プレイヤー用
        if (collision.gameObject.CompareTag("Player"))//Mucusflog用
        {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle) {
                player.ObstacleCollision(_speedDown);
            }
                        
        }
        if (collision.gameObject.layer == 14) {

            if (collision.gameObject.GetComponent<FrogCpu>() != null) {
                FrogCpu cpu = collision.gameObject.GetComponent<FrogCpu>();
                if (cpu._isPridictionStart) {
                    cpu.ObstacleCollision(_speedDown);
                }               
            } else if (collision.gameObject.GetComponent<FrogCpuMulti>() != null) {
                FrogCpuMulti cpu = collision.gameObject.GetComponent<FrogCpuMulti>();
                if (cpu._isPridictionStart) {
                    cpu.ObstacleCollision(_speedDown);
                }
                
            } else if (collision.gameObject.GetComponent<FrogCpuMulti2>() != null) {
                FrogCpuMulti2 cpu=collision.gameObject.GetComponent<FrogCpuMulti2>();
                if (cpu._isPridictionStart) {
                    cpu.ObstacleCollision(_speedDown);
                }
            }
        }
    }

}


