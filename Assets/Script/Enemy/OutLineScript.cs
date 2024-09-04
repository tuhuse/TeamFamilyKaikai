using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineScript : MonoBehaviour {
    [SerializeField] SneakAnim _sneakAnimScript;
    [SerializeField] private GameObject _cpu1;
    [SerializeField] private GameObject _cpu2;
    [SerializeField] private GameObject _cpu3;
    [SerializeField] private CommentScript _commentScript = default;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _deathBomb;
    [SerializeField] private CameraRankScript _cameraRank = default;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _minCameraSize;
    [SerializeField] private ClearMan _clearMan;
    private GameObject[] _deathFrog;

    private int _countEat = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {


        //collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //collision.gameObject.GetComponent<PlayercontrollerScript>().enabled = false;
        if (((collision.gameObject.tag == "Player") || collision.gameObject.tag == "CPU")&&_countEat<3) 
        {
            _countEat++;
            _clearMan.DropOuts(collision.gameObject);
            //collision.gameObject.SetActive(false);
            _deathBomb.Play();

            ParticleSystem.MainModule main = _particle.main;
            if (collision.gameObject.layer == 12) {//緑
                main.startColor = new Color(0.625f, 0.772f, 0.47f, 1f);
                _particle.Play();
            }
            if (collision.gameObject.layer == 13) {//黄色
                main.startColor = new Color(1f, 0.827f, 0.243f, 1f);
                _particle.Play();
            }
            if (collision.gameObject.layer == 10) {//青
                main.startColor = new Color(0.47f, 0.662f, 0.772f, 1f);
                _particle.Play();
            }
            if (collision.gameObject.layer == 8) {//ピンク
                main.startColor = new Color(0.96f, 0.713f, 0.839f, 1f);
                _particle.Play();
            }
            if (collision.gameObject == _cpu1) {//CPUピンク
                main.startColor = new Color(0.96f, 0.713f, 0.839f, 1f);
                _particle.Play();
            }
            if (collision.gameObject == _cpu2) {//CPU青
                main.startColor = new Color(0.47f, 0.662f, 0.772f, 1f);
                _particle.Play();
            }
            if (collision.gameObject == _cpu3) {//CPU黄色
                main.startColor = new Color(1f, 0.827f, 0.243f, 1f);
                _particle.Play();
            }


            _commentScript.CommentChange("ここで" + collision.gameObject.name + "がだつらく！！！", false);
           
            //collision.gameObject.SetActive(false);
            _sneakAnimScript.Attack();
        }
    }
}
