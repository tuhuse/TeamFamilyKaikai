using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameClearSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0��go,1��return�̃e�L�X�g������
    [SerializeField] GameObject _pauseManager = default; //canvasmanager������
    private int _selectButton = 2; //�����Ă���{�^��
    public GameObject _preButton = default; //�ЂƂO�ɑI�����Ă����{�^��
    [SerializeField] private Animator[] _animator;
    private bool _isSelect;

    private bool _isFarstSelect = false;


    // Start is called before the first frame update
    private void Start() {
        _selectButton = 0;
        StartCoroutine(Wait());
    }
    // Update is called once per frame
    void Update() {
        if (_selectButton == 0) {
            _animator[0].SetBool("Change", true);
            _animator[1].SetBool("Change", false);

        } else if (_selectButton == 1) {
            _animator[0].SetBool("Change", false);
            _animator[1].SetBool("Change", true);

        }
        float lstick = Input.GetAxis("L_Stick_Horizontal");
        float crosskey = Input.GetAxis("Debug Horizontal");
        //�E������������
        if ((lstick > 0 && !_isSelect) || (crosskey > 0 && !_isSelect)) {

            _isSelect = true;
            _selectButton = 1;

        }

        //�������������ꍇ
        if ((lstick < 0 && !_isSelect) || (crosskey < 0 && !_isSelect)) {

            _isSelect = true;
            _selectButton = 0;


        }

        //���{�^���𗣂�����
        if (lstick == 0 && crosskey == 0) {
            //�܂��I���ł���悤�ɂ���
            _isSelect = false;
        }

        //A�{�^������������
        if ((Input.GetButton("Fire2") && _isFarstSelect) || (Input.GetKeyDown(KeyCode.Return) && _isFarstSelect)) {
            _isFarstSelect = false;
            if (_selectButton == 0) //return�����Ă����ꍇ
            {
                //�^�C�g���ɖ߂�

                _pauseManager.GetComponent<ClearMan>().ClearRetryButton();
            } else //return�����Ă����ꍇ
              {

                //�Q�[�����I������
                _pauseManager.GetComponent<ClearMan>().ClearTitleButton();
            }
        }
    }
    private IEnumerator Wait() {

        yield return new WaitForSeconds(1);
        _isFarstSelect = true;
    }
}