using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beard : MonoBehaviour {

    private Rigidbody2D _rb;
    [SerializeField] private float _movebeard;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //����
        _rb.velocity = Vector2.right * _movebeard * 2;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flor"))//���ɕE������
       {
            this.gameObject.SetActive(false);//�E������
        }
        if (collision.gameObject.layer == 12) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._isGetWater) {
                if (player._projectile == null) {
                    player.BeardCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.BeardCollision();
                }
            } 
        }
        if (collision.gameObject.layer == 13) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._isGetWater) 
            {
                if (player._projectile == null) 
                {
                    player.BeardCollision();
                }

               else if (this.gameObject != player._projectile.gameObject) {
                    player.BeardCollision();
                }
            }  
           
        }
        if (collision.gameObject.layer == 10) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._isGetWater) {
                if (player._projectile == null) {
                    player.BeardCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.BeardCollision();
                }
            } 
        }
        if (collision.gameObject.layer == 8) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle && !player._isGetWater) {
                if (player._projectile == null) {
                    player.BeardCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.BeardCollision();
                }
            } 
        }
    }

}
