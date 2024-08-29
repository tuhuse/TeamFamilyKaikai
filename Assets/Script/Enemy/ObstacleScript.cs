using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //障害物に当たった時の挙動
    private float _speedDown = 60f;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤー用
        if (collision.gameObject.CompareTag("Player")){
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._waterEffect.activeSelf) {
                player.SpeedDown(true);
                player.ObstacleCollision(_speedDown);
            }
            
        } 
    
       
            

        //CPU用
        if (collision.gameObject.layer == 14)//Mucusflog用
        {
            if (collision.gameObject.GetComponent<FrogCpu>() != null) 
            {
                FrogCpu cpu1 = collision.gameObject.GetComponent<FrogCpu>();
             
                if (cpu1._isPridictionAbility) {
                    cpu1.PriictionAbility();
                }
                if (collision.gameObject.activeSelf && !cpu1._isWaterAbility && !cpu1._isPridictionAbility) {
                    cpu1.ObstacleCollision(_speedDown);
                    cpu1.SmokeStart();
                }
            } 
            else if (collision.gameObject.GetComponent<FrogCpuMulti>() != null) 
            {
                FrogCpuMulti cpu2 = collision.gameObject.GetComponent<FrogCpuMulti>();
                
                if (cpu2._isPridictionAbility) {
                    cpu2.PriictionAbility();
                }
                if (collision.gameObject.activeSelf && !cpu2._isWaterAbility && !cpu2._isPridictionAbility) {
                    cpu2.ObstacleCollision(_speedDown);
                    cpu2.SmokeStart();
                }
            } 
            else if (collision.gameObject.GetComponent<FrogCpuMulti2>() != null) 
            {
                FrogCpuMulti2 cpu3 = collision.gameObject.GetComponent<FrogCpuMulti2>();
                
                if (cpu3._isPridictionAbility) {
                    cpu3.PriictionAbility();
                }
                if (collision.gameObject.activeSelf && !cpu3._isWaterAbility && !cpu3._isPridictionAbility) {
                    cpu3.ObstacleCollision(_speedDown);
                    cpu3.SmokeStart();
                }
            }

                
        }


    }
}
