using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickGameClearSelect : MonoBehaviour {
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0��go,1��return�̃e�L�X�g������
    [SerializeField] GameObject _pauseManager = default; //canvasmanager������
    private int _selectButton = 2; //�����Ă���{�^��
    public GameObject _preButton = default;�@//�ЂƂO�ɑI�����Ă����{�^��

    private bool _isSelect;

    private bool _isFarstSelect = false;

    private bool _isGameOver = false;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update() {
     
        float lstick = Input.GetAxis("L_Stick_Horizontal");
        //�E������������
        if (lstick > 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 1;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            

            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);


        }

        //�������������ꍇ
        if (lstick  < 0 && !_isSelect) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 0;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
           

            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        }

        //���{�^���𗣂�����
        if (lstick == 0) {
            //�܂��I���ł���悤�ɂ���
            _isSelect = false;
        }

        //A�{�^������������
        if (Input.GetButton("Fire2") && _isFarstSelect) {
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
}