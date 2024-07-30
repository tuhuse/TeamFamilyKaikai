using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {
    //  //��Q���ɓ����������̋���
    private float _speedDown = 80f;


    private void OnCollisionEnter2D(Collision2D collision) {
        //�v���C���[�p
        if (collision.gameObject.layer == 12)//Mucusflog�p
        {
            collision.gameObject.GetComponent<PlayercontrollerScript>().ObstacleCollision(_speedDown);
        }//�v���C���[�Q
        if (collision.gameObject.layer == 13) {
            collision.gameObject.GetComponent<Player2>().ObstacleCollision(_speedDown);
        }
        if (collision.gameObject.layer == 14) {

            if (collision.gameObject.GetComponent<FrogCpu>() != null) {
                collision.gameObject.GetComponent<FrogCpu>().ObstacleCollision(_speedDown);
            } else if (collision.gameObject.GetComponent<FrogCpuMulti>() != null) {
                collision.gameObject.GetComponent<FrogCpuMulti>().ObstacleCollision(_speedDown);
            }
        }

    }

}

