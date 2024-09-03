using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearMan : MonoBehaviour {
    [SerializeField] private GameObject _cpuParent;
    [SerializeField] private GameObject _playerParent = default;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _gameClearUI;
    [SerializeField] TMPro.TMP_Text _clearText;
    [SerializeField] private GameObject _outLine = default;
    [SerializeField] private GameObject _outLineParent;
    //[SerializeField] private float _cameraRimit = default;
    [SerializeField] private float _cameraSizeAdjust = default;
    [SerializeField] private List<GameObject> _frogs = new List<GameObject>();
    private List<Rigidbody2D> _frogsrb2d = new List<Rigidbody2D>();

    [SerializeField] private List<GameObject> _anotherEnemys = new List<GameObject>();
    [SerializeField] private List<GameObject> _anotherPlayers = new List<GameObject>();
    private List<GameObject> _rankingList = new List<GameObject>();
    private float _fallMin = -60f;
    public int _switchNumber = 0;
    private float _sizeLimit = 20;
    private int _maxplayer = 4;
    private int _playerNumber = 1;
    private int _rankNumber = 1;
    private bool _cameraScale;
    [SerializeField] private SneakAnim _sneak;
    [SerializeField] private JoyStickGameClearSelect _joyStickGameClear = default;

    [SerializeField] private CameraRankScript _cameraRank = default;


    private int _playerCount = 4;
    private int _cpuCount = 3;

    // Start is called before the first frame update
    void Start() {


        for (int number = 0; number < _maxplayer - _playerNumber; number++) {
            _anotherEnemys.Add(_cpuParent.transform.GetChild(number).gameObject);
        }
        for (int number = 0; number < _playerNumber; number++) {
            _anotherPlayers.Add(_playerParent.transform.GetChild(number).gameObject);
        }

    }

    // Update is called once per frame
    void Update() {
        //print(_anotherEnamys.Count);
        if (_cameraScale) {
            _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust * Time.deltaTime * 10;
            if (_camera.GetComponent<Camera>().orthographicSize <= _cameraSizeAdjust) {
                _camera.GetComponent<Camera>().orthographicSize = _cameraSizeAdjust;
                _cameraScale = false;
            }
        }
        //�Q�[����ʂ��~�A�J�������Y�[���A�b�v�������UI��\��        
        switch (_switchNumber) {
            case 0:

                //�GCPU����0���c��v���C���[����1 �������͎c��v���C���[����0
                if ((_anotherEnemys.Count == 0 && _anotherPlayers.Count <= 1) || _anotherPlayers.Count == 0) {
                    _switchNumber = 2;
                }

                break;
            //case 1:
            //    //�G�I�u�W�F�N�g���擾
            //    foreach (GameObject arrayEnemy in _anotherEnemys) {
            //        //�����������͉�ʒ[�ɂԂ���Ɣz�񂩂�폜
            //        if (_anotherPlayers.Count != 0 && (arrayEnemy.transform.position.y < _fallMin ||
            //                    arrayEnemy.transform.position.x <= _outLine.transform.position.x)) {
            //            //�I�u�W�F�N�g�������L���O�z��̐擪�֑}��
            //            _rankingList.Insert(0, arrayEnemy);
            //            _anotherEnemys.Remove(arrayEnemy);

            //            _switchNumber = 0;

            //            break;

            //        }
            //    }
            //    //�v���C���[�I�u�W�F�N�g���擾
            //    foreach (GameObject arrayPlayer in _anotherPlayers) {
            //        //�����������͉�ʒ[�ɂԂ���Ɣz�񂩂�폜
            //        if (arrayPlayer.transform.position.x <= _outLine.transform.position.x) {
            //            //�I�u�W�F�N�g�������L���O�z��̐擪�֑}��
            //            _rankingList.Insert(0, arrayPlayer);
            //            _anotherPlayers.Remove(arrayPlayer);

            //            _switchNumber = 0;



            //            break;
            //        }
            //    }
            //    break;
            case 2:


                _switchNumber = 3;

                break;



            case 3:
                //StartCoroutine(TimeScaleReset());
                break;

            //case 4:
            //    �z����Ɏc���Ă���CPU�������L���O�ֈړ�


            //    �v���C���[�z��̒��g��0(�S�v���C���[��CPU�ɕ�����)�ꍇ�A�G�l�~�[�z����Ɏc���Ă���CPU�������L���O�z��ֈړ�
            //    if (_anotherPlayers.Count == 0) {

            //        foreach (GameObject arrayEnemy in _anotherEnemys) {
            //            _rankingList.Insert(0, arrayEnemy);
            //            _anotherEnemys.Remove(arrayEnemy); 
            //            break;
            //        }
            //    }
            //    �����v���C���[������ꍇ�A�v���C���[�z����Ɏc����1�v���C���[�������L���O�z��ֈړ�
            //    else {
            //        _rankingList.Insert(0, _anotherPlayers[0]);
            //        if (_rankingList.Count > _maxplayer) {
            //            _rankingList.Remove(_rankingList[4]);
            //        }
            //    }

            //    if (_anotherEnemys.Count == 0) {
            //        _switchNumber = 5;
            //    }

            //    break;

            case 5:
                Time.timeScale = 1f;
                foreach (GameObject rank in _rankingList) {
                    _clearText.SetText(_clearText.text += "\n" + _rankNumber + rank.name);
                    _rankNumber++;
                }
                
                _gameClearUI.SetActive(true);
                _joyStickGameClear.enabled = true;

                _switchNumber = 6;

                break;

            default:

                break;
        }

    }

    public void ClearRetryButton() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //������x�����ꏊ���v���C
        }
    }


    public void ClearTitleButton() {

        SceneManager.LoadScene("TitleScene");

    }
    public void InFrogs(GameObject frog) {
        _frogs.Add(frog);
        _frogsrb2d.Add(frog.GetComponent<Rigidbody2D>());

    }
    public void MaxPlayer(int playerNumber) {
        _playerNumber = playerNumber;
    }

    public void DropOuts(GameObject dropOutFrog) 
    {
        
        _rankingList.Insert(0, dropOutFrog);
       
        for (int frogCount = 0; frogCount < _frogs.Count; frogCount++) {
            if (_frogs[frogCount] == dropOutFrog) {
                _frogs.Remove(_frogs[frogCount]);
            }
        }
        //�v���C���[��������l���i�v���C���[�j�����炷
        if (dropOutFrog.gameObject.CompareTag("Player")) {
            _playerCount--;

        }
        //CPU��������l���iCPU�j�����炷
        else if (dropOutFrog.gameObject.CompareTag("CPU")) {
            _cpuCount--;


        }
        if (_cpuCount + _playerCount > 4) {
            dropOutFrog.SetActive(false);
        }
        //���̃��\�b�h���R��Ăяo���ꂽ��Q�[���I��
        if (_cpuCount + _playerCount <= 4) {
            Time.timeScale = 0.05f;
            print("���\�b�h3��Ăяo���ꂽ��");
            _rankingList.Insert(0, _frogs[0]);
            StartCoroutine(TimeScaleReset(dropOutFrog));
            _cameraRank.CameeeraRank(false);
            _sneak.Access(true);
        }
    }
    private IEnumerator TimeScaleReset(GameObject leaveFrog) {
        _outLineParent.transform.SetParent(leaveFrog.transform, true);
        _outLineParent.transform.position = new Vector3(leaveFrog.transform.position.x+5, leaveFrog.transform.position.y,0);
          print(leaveFrog.gameObject.name);
        _camera.transform.position =new Vector3 (leaveFrog.transform.position.x, leaveFrog.transform.position.y,-10);
       
        //_camera.transform.position -= Vector3.down;

        _cameraScale = true;
        
        yield return new WaitForSeconds(0.6f);

        leaveFrog.SetActive(false);
        _switchNumber = 5;

    }

}
