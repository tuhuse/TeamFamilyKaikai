using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //��Q���ɓ����������̋���
    private float _speedDown = 75f;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�p
        if (collision.gameObject.CompareTag("Player")){
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._waterEffect.activeSelf) {
                player.SpeedDown(true);
                player.ObstacleCollision(_speedDown);
            }
            
        } 
    
       
            

        //CPU�p
        if (collision.gameObject.layer == 14)//Mucusflog�p
        {
            if (collision.gameObject.GetComponent<FrogCpu>() != null){
                collision.gameObject.GetComponent<FrogCpu>().ObstacleCollision(_speedDown);
            } 
            else if (collision.gameObject.GetComponent<FrogCpuMulti>()!=null){
                collision.gameObject.GetComponent<FrogCpuMulti>().ObstacleCollision(_speedDown);
            }else if (collision.gameObject.GetComponent<FrogCpuMulti2>() != null) {
                collision.gameObject.GetComponent<FrogCpuMulti2>().ObstacleCollision(_speedDown);
            }
        }

    }
}
