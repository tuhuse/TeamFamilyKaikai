using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleSceneMove : MonoBehaviour
{
    
    [SerializeField] private GameObject _titlePanel;
    private bool _isStart = true;
    [SerializeField] List<GameObject> _gameClearText = new List<GameObject>(); //0��go,1��return�̃e�L�X�g������
    private int _selectButton = 2; //�����Ă���{�^��
    public GameObject _preButton = default;�@//�ЂƂO�ɑI�����Ă����{�^��

    private bool _isSelect;

    private bool _isFarstSelect = false;

    //[SerializeField] private GameObject _selectParent;
    //[SerializeField] private GameObject _titleParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        if (!_isStart)
        {
            return;
        }
        float lstick = Input.GetAxis("L_Stick_Horizontal");
        float crosskey = Input.GetAxis("Debug Horizontal");
        //�E������������
        if ((lstick > 0 && !_isSelect) || (crosskey > 0 && !_isSelect)) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 1;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);


            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);


        }

        //�������������ꍇ
        if ((lstick < 0 && !_isSelect)||(crosskey < 0 && !_isSelect)) {
            _isFarstSelect = true;
            _isSelect = true;
            _selectButton = 0;
            _preButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);


            _gameClearText[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);

            _gameClearText[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);

        }

        //���{�^���𗣂�����
        if (lstick == 0&&crosskey==0) {
            //�܂��I���ł���悤�ɂ���
            _isSelect = false;
        }

    
        if (Input.GetButtonDown("Fire2") && _isFarstSelect) {

            _isFarstSelect = false;
            if (_selectButton == 0) //return�����Ă����ꍇ
            {
                _titlePanel.gameObject.GetComponent<Animator>().enabled = true;

                _isStart = false;
                StartCoroutine(TitleButtonStay());            
            } else //return�����Ă����ꍇ
              {
                Application.Quit();
                //�Q�[�����I������
                
            }
          
            
        }
        
      

    }



   private IEnumerator TitleButtonStay()
    {
        _gameClearText[0].SetActive(false);
        _gameClearText[1].SetActive(false);
        _gameClearText[2].SetActive(false);
        float stayTime = 1;
        //�t�F�[�h�A�E�g�I����ɃV�[���ړ�
        yield return new WaitForSeconds(stayTime);
        SceneManager.LoadScene("Tutorial");
    }
}
