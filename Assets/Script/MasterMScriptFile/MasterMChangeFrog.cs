using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMChangeFrog : MonoBehaviour {
  
    [SerializeField] private GameObject _player = default;
   
 


    [SerializeField] private GameObject _brake;
    private Rigidbody2D _rb;

    private bool _isAlive = false;
    private bool _isJump = false;
    private bool _isOneshot = false;
    private bool _isJumping = false;

 
    private bool _isFrogjump = false; //�΂ߔ�т����Ă��邩
  
    private string _nameJoyStick = default;

    [SerializeField] private int _playernumber;// �v���C���[�ԍ��i1����n�܂�j
    [SerializeField] private int _rank = default;
    [SerializeField]
    private int _joynumber;

    
    

    [Header("�v���C���[���x")] [SerializeField] private float _movespeed = 100;
    [Header("�v���C���[�W�����v")] [SerializeField] private float _jumppower = 200f;


    private const float MOVESPEED = 100f;//�v���C���[�̃X�s�[�h�̊

    private const float JUMPMAX = 200f;

    private const float TIMEDELTATIME = 1000f;

    private SpriteRenderer _pridictionSpriterenderer = default;



    [SerializeField] AudioClip _jumpSE = default;
 
    private AudioSource _frogSE = default;

    private Vector3 _cpuposition = default;

    // Start is called before the first frame update

    private Animator _pridictionFrogAnim;
    void Start() {

        _isAlive = true;
        _frogSE = this.GetComponent<AudioSource>();
        _pridictionSpriterenderer = this.GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _pridictionFrogAnim = this.GetComponent<Animator>();
      
    }

    private void FixedUpdate() {
        if (!_isJump) {
            _brake.SetActive(false);
            //_jumppower -= JUMPMAX / 40f;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - (_jumppower /  30 ));//* Time.deltaTime)
        }
       
    }
    // Update is called once per frame
    void Update() {

        HandlePlayerInput(_playernumber);


    }
    void HandlePlayerInput(int playerNumber) {

        string xbutton = _playernumber + "pA";
        if (!_isAlive) {
            return;
        }
        if (_isAlive && !_isFrogjump)//�����Ă鎞�ɓ�����悤��
        {
            // ���蓖�Ă�ꂽ�R���g���[���[�̓��͂�����
            //if (!string.IsNullOrEmpty(_nameJoyStick)) {
            // ���X�e�B�b�N�̐������͂��擾
            float horizontalInput = Input.GetAxis(_joynumber + "pLstickHorizontal");
            //�ړ�
            if (Input.GetKey(KeyCode.A) || horizontalInput < 0) {
                _brake.SetActive(true);

                MoveLeftControll();
            }
            if (Input.GetKey(KeyCode.D) || horizontalInput > 0) {

                _brake.SetActive(false);
                MoveRightControll();
            }
            //�W�����v


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(xbutton)) {

                if (_isJump) {

                    _isJump = false;

                    _frogSE.PlayOneShot(_jumpSE);
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumppower);
                }
            }

          
            //}


            if (!_isJump && !_isJumping) {
                _isJump = true;
            }
            //else {
            //    _isJump = false;
            //}

            if (_movespeed <= MOVESPEED - 10) {

               

            }
            if (this._rb.velocity.x == 0) {
                _brake.SetActive(false);
                _pridictionFrogAnim.SetBool("Brake", false);
                _pridictionFrogAnim.SetBool("Run", false);
               
            } 
        }
        

    }
   
  
    
    private void MoveLeftControll() {
        ////���΂Ɍ�������
        //if (!_pridictionSpriterenderer.flipX) {
        //    _pridictionSpriterenderer.flipX = true;
        //}

        if (!_pridictionFrogAnim.GetBool("Jump")) {

            _pridictionFrogAnim.SetBool("Brake", true);
            _pridictionFrogAnim.SetBool("Run", false);
        } else {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", false);
        }

        float brake = 50;
        //�ʏ�̈ړ�
        _rb.velocity = new Vector3( brake, _rb.velocity.y, 0); //* Time.deltaTime ;
    }
    private void MoveRightControll() {
        //���΂Ɍ�������
        if (_pridictionSpriterenderer.flipX) {
            _pridictionSpriterenderer.flipX = false;
        }
        //�ʏ�̈ړ�       
        if (!_pridictionFrogAnim.GetBool("Jump")) {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", true);
        } else {
            _pridictionFrogAnim.SetBool("Brake", false);
            _pridictionFrogAnim.SetBool("Run", false);
        }
        _rb.velocity = new Vector3(_movespeed, _rb.velocity.y, 0); //* Time.deltaTime ;



    }
    private void OnCollisionStay2D(Collision2D collision) {
        //���ɑ��������Ă�Ƃ�
        if (collision.gameObject.CompareTag("Flor")) {
            _rb.gravityScale = 10;
            _jumppower = JUMPMAX;
            _pridictionFrogAnim.SetBool("Jump", false);
            _isJumping = false;

        }


    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Flor") {
            _isFrogjump = false;

        }

    }

    private void OnCollisionExit2D(Collision2D collision) {

        //�����瑫�����ꂽ�Ƃ�
        if (collision.gameObject.CompareTag("Flor")) {
            //_isJump = false;
            _isJumping = true;
            _isJump = false;
            _pridictionFrogAnim.SetBool("Jump", true);
            if (_player.activeSelf) {
                StartCoroutine(JUMP());
            }


        }

    }
    private IEnumerator JUMP() {
        yield return new WaitForSeconds(0.28f);
        _rb.gravityScale = 40f;
    }



 

    

   

   

  
  

    
   
  

    public void RankChange(int nowrank) {
        _rank = nowrank;
    }


  
   

 
}

