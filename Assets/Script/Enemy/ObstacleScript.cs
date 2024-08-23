using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //��Q���ɓ����������̋���
    private float _speedDown = 75f;
    [SerializeField] private PlayercontrollerScript _playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�p
        if (collision.gameObject.layer == 12&& !_playerScript._isInvincivle && !_playerScript._waterEffect.activeSelf) {
            _playerScript.Yuuya(true);
            collision.gameObject.GetComponent<PlayercontrollerScript>().ObstacleCollision(_speedDown);
        } //�v���C���[�p
        //if (collision.gameObject.layer == 13)
        //{
        //    collision.gameObject.GetComponent<PlayercontrollerScript>().ObstacleCollision(_speedDown);
        //} 
       
        //CPU�p
        if (collision.gameObject.layer == 14)//Mucusflog�p
        {
            if (collision.gameObject.GetComponent<FrogCpu>() != null){
                collision.gameObject.GetComponent<FrogCpu>().ObstacleCollision(_speedDown);
            } 
            else if (collision.gameObject.GetComponent<FrogCpuMulti>()!=null){
                collision.gameObject.GetComponent<FrogCpuMulti>().ObstacleCollision(_speedDown);
            }else if (collision.gameObject.GetComponent<FrogCpuMulti>()!=null&&
                collision.gameObject.GetComponent<FrogCpu>() != null) {
                collision.gameObject.GetComponent<FrogCpuMulti2>().ObstacleCollision(_speedDown);
            }
        }

    }
}
