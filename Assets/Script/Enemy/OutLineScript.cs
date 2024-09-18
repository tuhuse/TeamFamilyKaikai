using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLineScript : MonoBehaviour {
    [SerializeField] SneakAnim _sneakAnimScript;
    [SerializeField] private GameObject _cpu1;
    [SerializeField] private GameObject _cpu2;
    [SerializeField] private GameObject _cpu3;
    [SerializeField] private CommentScript _commentScript = default;

    [SerializeField] private ParticleSystem _deathBomb;
    [SerializeField] private ParticleSystem _eatFrogEffect;
    [SerializeField] private CameraRankScript _cameraRank = default;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _minCameraSize;
    [SerializeField] private ClearMan _clearMan;
    [SerializeField] private Sprite _grayFrog = default;
    [SerializeField] private List<Image> _coolDownGages = new List<Image>();
    private GameObject[] _deathFrog;

    private int _countEat = 0;

    private BoxCollider2D _thisCollider = default;

    [Header("脱落ボイス"),SerializeField] private AudioClip _frogVoice = default;
    private string _1pName = "けろこ";
    private string _2pName = "けろせい";
    private string _3pName = "けろれお";
    private string _4pName = "けろや";
    // Start is called before the first frame update
    void Start() {
        _thisCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {


        //collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //collision.gameObject.GetComponent<PlayercontrollerScript>().enabled = false;
        if (((collision.gameObject.tag == "Player") || collision.gameObject.tag == "CPU") && _countEat < 3) {
            _thisCollider.enabled = false;
            _countEat++;
            _clearMan.DropOuts(collision.gameObject);
            //collision.gameObject.SetActive(false);
            _deathBomb.Play();
            _eatFrogEffect.Play();
          
            if (collision.gameObject.layer == 12) {//緑
              
                _coolDownGages[0].GetComponent<TongueGageScript>().DethFrog();
            }
            if (collision.gameObject.layer == 13) {//黄色
                
                _coolDownGages[1].GetComponent<TongueGageScript>().DethFrog();
                
            }
            if (collision.gameObject.layer == 10) {//青
                
                _coolDownGages[2].GetComponent<TongueGageScript>().DethFrog();
              
            }
            if (collision.gameObject.layer == 8) {//ピンク
                
                _coolDownGages[3].GetComponent<TongueGageScript>().DethFrog();
               
            }
            


            string outFrogName = default;

            if (collision.gameObject.name == _1pName) {
                outFrogName = "<color=green>" + collision.gameObject.name + "</color>";
            } else if (collision.gameObject.name == _2pName) {
                outFrogName = "<color=#F0F121>" + collision.gameObject.name + "</color>";
            } else if (collision.gameObject.name == _3pName) {
                outFrogName = "<color=blue>" + collision.gameObject.name + "</color>";
            } else if (collision.gameObject.name == _4pName) {
                outFrogName = "<color=#E030C4>" + collision.gameObject.name + "</color>";
            }
            _commentScript.CommentatorCommentChange("ここで" + outFrogName + "がだつらく！！！", false, _frogVoice);

            //collision.gameObject.SetActive(false);
            _sneakAnimScript.Attack();
        }
    }
}
