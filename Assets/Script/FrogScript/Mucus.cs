using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : MonoBehaviour {
    private Animator _mucus;
    private Rigidbody2D _rb;
    [SerializeField] private float _movemucus;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _mucus = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() { //����
        _rb.velocity = (Vector2.right * _movemucus * 3) + (Vector2.down * _movemucus);
    }
    private void OnTriggerStay2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Flor")) {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Flor")) {
            _movemucus = 0;
            _mucus.SetBool("Start", true);

        }
        if (collision.gameObject.CompareTag("Reset")) {
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.layer==12) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle) {
                if (player._projectile == null) {
                    player.MucusCollision();
                } else if (this.gameObject !=player._projectile.gameObject) {
                    player.MucusCollision();
                }
            }
        }
        if (collision.gameObject.layer == 13) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle) {
                if (player._projectile == null) {
                    player.MucusCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.MucusCollision();
                }
            }
        }
        if (collision.gameObject.layer == 10) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle) {
                if (player._projectile == null) {
                    player.MucusCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.MucusCollision();
                }
            }
        }
        if (collision.gameObject.layer == 8) {
            PlayercontrollerScript player = collision.gameObject.GetComponent<PlayercontrollerScript>();
            if (!player._isInvincivle) {
                if (player._projectile == null) {
                    player.MucusCollision();
                } else if (this.gameObject != player._projectile.gameObject) {
                    player.MucusCollision();
                }
            }
        }
    }
    
}
