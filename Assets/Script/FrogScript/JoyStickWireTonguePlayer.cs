using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JoyStickWireTonguePlayer : MonoBehaviour {
    private bool _isExtension = false;
    private bool _isAttack = false;

    private bool _isFrogCatch = false;
    private bool _isJustOnes = false;
    private bool _underAttack = default;

    private Vector3 _playerposition = default;

    private float _tongueScaleY = default;

    [SerializeField]
    private int _joynumber;

    private const float TIMEDELTATIME = 1000f;
    private const float PLUSSCALESPEEDY = 0.15f;
    private const float FAILEDTONGUECATCH = 8f;
    private const float SUCCESSTONGUECATCH = 8f;

    private const float TONGUESCALEX = 3f;
    private const float TONGUESCALEY = 0.01f;
    private const float MAXTANGUEEXTENSION = 10f;
    [SerializeField] private TongueGageScript _aqua;
    [Header("Player")] [SerializeField] private GameObject _player = default;

    private Rigidbody2D _playerRB = default;


    [SerializeField] private GameObject _dashSmoke = default;
    private GameObject _dashSmokeClone = default;

    private SpriteRenderer _dashSmokeRenderer = default;
    private Animator _dashSmokeAnim = default;

    // Update is called once per frame
    private void Start() {
        _playerRB = GetComponentInParent<Rigidbody2D>();
        _tongueScaleY = this.transform.localScale.y;
    }
    void Update() {

        if (_isExtension) {
            //�x����L�΂�
            this.transform.localScale += new Vector3(0, 0.05f, 0) * Time.deltaTime * TIMEDELTATIME;

        }
        //�x�����ǂɓ�����Ȃ�������
        else if (!_isFrogCatch) {
            //�x���̏k��
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes && _isAttack) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * TIMEDELTATIME;
            } else if (_isAttack && !_isJustOnes) {
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                _isJustOnes = true;
                StartCoroutine(Failed());
            }


        } else {
            _underAttack = false;
            if (this.transform.localScale.y >= _tongueScaleY && !_isJustOnes && _isAttack) {
                this.transform.localScale -= new Vector3(0, PLUSSCALESPEEDY, 0) * Time.deltaTime * TIMEDELTATIME;
            } else if (_isAttack && !_isJustOnes) {
                _isJustOnes = true;
                this.transform.localScale = new Vector3(TONGUESCALEX, TONGUESCALEY, 0);
                StartCoroutine(Success());
            }
        }

        if (this.transform.localScale.y > MAXTANGUEEXTENSION) {
            //�x�����k�ߎn�߂�
            _isExtension = false;
        }



        if (_player.gameObject != null) {
            
                if (Input.GetButtonDown(_joynumber+"pRB")) {
                    //�x����L�΂��n�߂�
                    if (!_isAttack) {
                    _aqua.TongueUIStartCooldown();
                    _aqua.TongueCoolDownFloat(SUCCESSTONGUECATCH);
                    _isAttack = true;
                        _isExtension = true;
                        _underAttack = true;
                    }

                }
             
            //else if (_player.gameObject.layer == 13) {
            //    if (Input.GetButtonDown("2pRB")) {
            //        //�x����L�΂��n�߂�
            //        if (!_isAttack) {
            //            _isAttack = true;
            //            _isExtension = true;
            //            _underAttack = true;
            //        }

            //    }

            //}
        }
         
      


    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Handle" && _isAttack) {
            ControllScriptOff();
            _isExtension = false;
        }
        if (collision.gameObject.CompareTag("Flor")) {
            _isExtension = false;
        }

        if (collision.gameObject.tag == "CPU" && _underAttack) 
        {
            

            
            if (_player.GetComponent<PlayercontrollerScript>() != null)
            {

                //�_�b�V���G�t�F�N�g�������Ă��Ȃ�������
                if (_dashSmokeClone == null) {
                    //���𐶐����āA�R���|�[�l���g���擾����
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;


                _player.GetComponent<PlayercontrollerScript>().SpeedUp(true);

            }
           
            _isExtension = false;
            _isFrogCatch = true;
        } 
        if (collision.gameObject.tag == "Player" && _underAttack) 
        {
            


            if (_player.GetComponent<PlayercontrollerScript>() != null&&!collision.gameObject.GetComponent<PlayercontrollerScript>()._isInvincivle)
            {

                //�_�b�V���G�t�F�N�g�������Ă��Ȃ�������
                if (_dashSmokeClone == null) {
                    //���𐶐����āA�R���|�[�l���g���擾����
                    _dashSmokeClone = Instantiate(_dashSmoke);
                    _dashSmokeAnim = _dashSmokeClone.GetComponent<Animator>();
                    _dashSmokeRenderer = _dashSmokeClone.GetComponent<SpriteRenderer>();
                }
                _dashSmokeClone.transform.position = this.transform.position;
                _dashSmokeAnim.SetBool("Dash", true);
                _dashSmokeRenderer.enabled = true;

                _player.GetComponent<PlayercontrollerScript>().SpeedUp(true);
            }
           
            _isExtension = false;
            _isFrogCatch = true;
        }
    }

    private void ControllScriptOff() {
        //�R���g���[���X�N���v�g�̈ꎞ��~
        if (_player != null) {
            GetComponentInParent<PlayercontrollerScript>().enabled = false;
        }


        //�J�G���̈ꎞ��~
        _playerRB.velocity = Vector2.zero;
    }

    private void ControllScriptOn() {
        
        //�R���g���[���X�N���v�g�̕���
        if (_player != null) {
            if (_player.GetComponent<PlayercontrollerScript>() != null) {
                GetComponentInParent<PlayercontrollerScript>().enabled = true;
            }
           
        }
        

    }

    private IEnumerator Failed() {
        yield return new WaitForSeconds(FAILEDTONGUECATCH);
        _aqua.TongueUIStopCooldown();
        _isAttack = false;
        _isJustOnes = false;
        
    }

    private IEnumerator Success() {
        yield return new WaitForSeconds(SUCCESSTONGUECATCH);
        
        _aqua.TongueUIStopCooldown();
        _isAttack = false;
        _isJustOnes = false;
        _isFrogCatch = false;
    }
}
