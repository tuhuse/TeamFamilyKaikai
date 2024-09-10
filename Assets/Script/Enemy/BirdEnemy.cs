using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {
    //  //��Q���ɓ����������̋���
    private float _speedDown = 60f;
    

    private void OnCollisionEnter2D(Collision2D collision) {
        //�v���C���[�p
        if (collision.gameObject.CompareTag("Player"))//Mucusflog�p
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


