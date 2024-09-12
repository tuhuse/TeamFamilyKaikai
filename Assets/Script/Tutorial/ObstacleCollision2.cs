using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision2 : MonoBehaviour {//障害物に当たった時の挙動
    private float _speedDown = 60f;
    private Animator _thisAnim = default;
    [SerializeField] private ParticleSystem _hitParticle = default;
    private EdgeCollider2D _thisCollider = default;
    private int _breakLayer = 22;

    private void Start() {
        _thisCollider = this.GetComponent<EdgeCollider2D>();
        _thisAnim = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Reset") && _hitParticle != null) {

            this.gameObject.layer = 0;
            _thisAnim.SetBool("Break", false);
        }

        //プレイヤー用
        if (collision.gameObject.CompareTag("Player")) {


            if (_hitParticle != null) {

                this.gameObject.layer = _breakLayer;
                _thisAnim.SetBool("Break", true);
                _hitParticle.Play();
            }

            Player2 player = collision.gameObject.GetComponent<Player2>();
            if (!player._isInvincivle && !player._waterEffect.activeSelf) {
                player.SpeedDown(true);
                player.ObstacleCollision(_speedDown);
            }

        }




     

        


    }
}
