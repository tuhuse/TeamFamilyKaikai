using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    //��Q���ɓ����������̋���
    private float _speedDown = 75f;
    [SerializeField] private PlayercontrollerScript[] _playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�v���C���[�p
        if (collision.gameObject.layer == 12&& !_playerScript[0]._isInvincivle &&
            !_playerScript[0]._waterEffect.activeSelf) {
            _playerScript[0].Yuuya(true);
            _playerScript[0].ObstacleCollision(_speedDown);
        } //�v���C���[2�p
        
        if (collision.gameObject.layer == 13 && !_playerScript[1]._isInvincivle &&
            !_playerScript[1]._waterEffect.activeSelf) {
            _playerScript[1].Yuuya(true);
            _playerScript[1].ObstacleCollision(_speedDown);
        }//�v���C���[3�p
        if (collision.gameObject.layer == 10 && !_playerScript[2]._isInvincivle &&
            !_playerScript[2]._waterEffect.activeSelf) {
            _playerScript[2].Yuuya(true);
            _playerScript[2].ObstacleCollision(_speedDown);
        }//�v���C���[44�p
        if (collision.gameObject.layer == 8 && !_playerScript[3]._isInvincivle &&
            !_playerScript[3]._waterEffect.activeSelf) {
            _playerScript[3].Yuuya(true);
            _playerScript[3].ObstacleCollision(_speedDown);
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
