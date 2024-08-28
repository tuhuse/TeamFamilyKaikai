using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineScript : MonoBehaviour
{
    [SerializeField] SneakAnim _sneakAnimScript;
    [SerializeField] private GameObject _cpu1;
    [SerializeField] private GameObject _cpu2;
    [SerializeField] private GameObject _cpu3;
    [SerializeField] private GameObject _cpu1DeathAnim;
    [SerializeField] private GameObject _cpu2DeathAnim;
    [SerializeField] private GameObject _cpu3DeathAnim;
    [SerializeField] private GameObject _playerDeathAnim;
    [SerializeField] private CommentScript _commentScript = default;
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private CameraRankScript _cameraRank = default;
     private BoxCollider2D _box;

    // Start is called before the first frame update
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "CPU") 
        {
            _box.enabled = false;
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
            if (collision.gameObject == _cpu1) {
                _cpu1DeathAnim.SetActive(true);
            }
            if (collision.gameObject == _cpu2) {
                _cpu2DeathAnim.SetActive(true);
            }
            if (collision.gameObject == _cpu3) {
                _cpu3DeathAnim.SetActive(true);
            }
            if (collision.gameObject.tag == "Player") 
            {
                _playerDeathAnim.SetActive(true);
            }
            collision.gameObject.SetActive(false);
            _sneakAnimScript.Attack();
        }
    }
}
