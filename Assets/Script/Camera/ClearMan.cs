using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ClearMan : MonoBehaviour {
    [SerializeField] private GameObject _cpuParent; // CPU�I�u�W�F�N�g�̐e�I�u�W�F�N�g
    [SerializeField] private GameObject _playerParent = default; // �v���C���[�I�u�W�F�N�g�̐e�I�u�W�F�N�g
    [SerializeField] private GameObject _camera; // �J�����I�u�W�F�N�g
    [SerializeField] private GameObject _gameClearUI; // �Q�[���N���AUI
    [SerializeField] private TMPro.TMP_Text _clearText; // �Q�[���N���A�̃e�L�X�g
    [SerializeField] private GameObject _outLine = default; // �Q�[����ʂ̋��E���I�u�W�F�N�g
    [SerializeField] private GameObject _outLineParent; // ���E���̐e�I�u�W�F�N�g
    //[SerializeField] private float _cameraRimit = default; // �J���������i���g�p�j
    [SerializeField] private float _cameraSizeAdjust = default; // �J�����T�C�Y�����̒l
    [SerializeField] private List<GameObject> _frogs = new List<GameObject>(); // �v���C���[�����CPU�I�u�W�F�N�g�̃��X�g
    private List<Rigidbody2D> _frogsrb2d = new List<Rigidbody2D>(); // �v���C���[�����CPU�I�u�W�F�N�g��Rigidbody2D�R���|�[�l���g�̃��X�g

    [SerializeField] private List<GameObject> _anotherEnemys = new List<GameObject>(); // ���̓G�I�u�W�F�N�g�̃��X�g
    [SerializeField] private List<GameObject> _anotherPlayers = new List<GameObject>(); // ���̃v���C���[�I�u�W�F�N�g�̃��X�g
    private List<GameObject> _rankingList = new List<GameObject>(); // �����L���O�̃��X�g
   [SerializeField] private Image[] _podiumfrog;
   [SerializeField] private GameObject[] _podiumfrogs;
    private float _fallMin = -60f; // �I�u�W�F�N�g����������ŏ�Y���W
    public int _switchNumber = 0; // �X�C�b�`�ԍ��i��ԊǗ��j
    private float _sizeLimit = 20; // �T�C�Y�����i���g�p�j
    private int _maxplayer = 4; // �ő�v���C���[��
    private int _playerNumber = 1; // ���݂̃v���C���[��
    private int _rankNumber = 1; // �����L���O�̔ԍ�
    private int _playerCount = 4; // �v���C���[�̑���
    private int _cpuCount = 3; // CPU�̑���
    private int _alivePlayersCount = 0; // �������Ă���v���C���[�̐�

    private bool _cameraScale; // �J�����X�P�[���̏��
    private bool _isPlayerDeth = false; // �v���C���[�̎��S���
    private bool _threeOrMorePeople = false; // 3�l�ȏ�̃v���C���[�����邩�ǂ���

    [SerializeField] private SneakAnim _sneak; // Sneak�A�j���[�V�����̃X�N���v�g
    [SerializeField] private JoyStickGameClearSelect _joyStickGameClear = default; // �Q�[���N���A���̃W���C�X�e�B�b�N�I���X�N���v�g
    [SerializeField] private CameraShake _cameraShake; // �J�����V�F�C�N�̃X�N���v�g
    [SerializeField] private CameraRankScript _cameraRank = default; // �J���������L���O�̃X�N���v�g
    [SerializeField] private StageRoopManFixed _stageRoopScript = default; // �X�e�[�W���[�v�Ǘ��X�N���v�g

    private enum Rank {First,Second,Three,Four}
    private Rank _rank = default;
    // Start is called before the first frame update
    void Start() {
        // CPU�ƃv���C���[�I�u�W�F�N�g�����X�g�ɒǉ�
        for (int number = 0; number < _maxplayer - _playerNumber; number++) {
            _anotherEnemys.Add(_cpuParent.transform.GetChild(number).gameObject);
        }
        for (int number = 0; number < _playerNumber; number++) {
            _anotherPlayers.Add(_playerParent.transform.GetChild(number).gameObject);
        }
        _rank = Rank.First;
    }

    // Update is called once per frame
    void Update() {
        // �J�����X�P�[���̒���
        if (_cameraScale) {
            _camera.GetComponent<Camera>().orthographicSize -= _cameraSizeAdjust * Time.deltaTime * 10;
            if (_camera.GetComponent<Camera>().orthographicSize <= _cameraSizeAdjust) {
                _camera.GetComponent<Camera>().orthographicSize = _cameraSizeAdjust;
                _cameraScale = false;
            }
        }

        // �X�C�b�`�ԍ��ɉ���������
        switch (_switchNumber) {
            case 0:
                // �GCPU���S�Ĕr������āA�v���C���[��1�l�ȉ��̏ꍇ�A�܂��̓v���C���[���S���r�����ꂽ�ꍇ
                if ((_anotherEnemys.Count == 0 && _anotherPlayers.Count <= 1) || _anotherPlayers.Count == 0) {
                    _switchNumber = 2;
                }
                break;

            case 2:
                // ��Ԃ�3�ɕύX
                _switchNumber = 3;
                break;

            case 3:
                // �X�C�b�`�ԍ���3�̂Ƃ��̏����i���݂͋�j
                break;

            case 5:
                _podiumfrog[16].enabled = true;
                // �����L���O�̕\��
                Time.timeScale = 1f;
                foreach (GameObject rank in _rankingList) {
                    
                    switch (_rank) {
                        case Rank.First:
                            if (_rankingList[0] == _podiumfrogs[0]) {
                                _podiumfrog[0].enabled=true;
                            } else if (_rankingList[0] == _podiumfrogs[1] || _rankingList[0] == _podiumfrogs[4]) {
                                _podiumfrog[1].enabled = true;
                                _podiumfrog[1].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[0] == _podiumfrogs[2] || _rankingList[0] == _podiumfrogs[5]) {
                                _podiumfrog[2].enabled = true;
                                _podiumfrog[2].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[0] == _podiumfrogs[3] || _rankingList[0] == _podiumfrogs[6]) {
                                _podiumfrog[3].enabled = true;
                                _podiumfrog[3].GetComponent<Animator>().enabled = true;
                            }
                          
                            _rank = Rank.Second;

                            print(rank+"1");
                            break;
                        case Rank.Second:
                            if (_rankingList[1] == _podiumfrogs[0]) {
                                _podiumfrog[4].enabled = true;
                                _podiumfrog[4].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[1] || _rankingList[1] == _podiumfrogs[4]) {
                                _podiumfrog[5].enabled = true;
                                _podiumfrog[5].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[2] || _rankingList[1] == _podiumfrogs[5]) {
                                _podiumfrog[6].enabled = true;
                                _podiumfrog[6].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[1] == _podiumfrogs[3] || _rankingList[1] == _podiumfrogs[6]) {
                                _podiumfrog[7].enabled = true;
                                _podiumfrog[7].GetComponent<Animator>().enabled = true;
                            }
                            print(rank + "2");
                            _rank = Rank.Three;
                            
                            break;
                        case Rank.Three:
                            if (_rankingList[2] == _podiumfrogs[0]) {
                                _podiumfrog[8].enabled = true;
                                _podiumfrog[8].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[1] || _rankingList[2] == _podiumfrogs[4]) {
                                _podiumfrog[9].enabled = true;
                                _podiumfrog[9].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[2] || _rankingList[2] == _podiumfrogs[5]) {
                                _podiumfrog[10].enabled = true;
                                _podiumfrog[10].GetComponent<Animator>().enabled = true;
                            } else if (_rankingList[2] == _podiumfrogs[3] || _rankingList[2] == _podiumfrogs[6]) {
                                _podiumfrog[11].enabled = true;
                                _podiumfrog[11].GetComponent<Animator>().enabled = true;
                            }
                            print(rank + "3");
                            _rank = Rank.Four;
                            break;
                        case Rank.Four:
                            if (_rankingList[3] == _podiumfrogs[0]) {
                                _podiumfrog[12].enabled = true;
                              
                            } else if (_rankingList[3] == _podiumfrogs[1] || _rankingList[3] == _podiumfrogs[4]) {
                                _podiumfrog[13].enabled = true;
                                
                            } else if (_rankingList[3] == _podiumfrogs[2] || _rankingList[3] == _podiumfrogs[5]) {
                                _podiumfrog[14].enabled = true;
                               
                            } else if (_rankingList[3] == _podiumfrogs[3] || _rankingList[3] == _podiumfrogs[6]) {
                                _podiumfrog[15].enabled = true;
                              
                            }
                            print(rank + "4");
                            break;
                    }
                    _rankingList[0].SetActive(false);
                    _clearText.SetText(_clearText.text += "\n" + _rankNumber +"��"+" "+ rank.name);
                    _rankNumber++;
                }
                _gameClearUI.SetActive(true); // �Q�[���N���AUI��\��
                _joyStickGameClear.enabled = true; // �W���C�X�e�B�b�N�̃Q�[���N���A�I����L���ɂ���
                _switchNumber = 6;
                break;

            default:
                break;
        }
    }

    // �Q�[���N���A��̃��g���C�{�^�������i�X�y�[�X�L�[�����j
    public void ClearRetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // �^�C�g���V�[���ɖ߂�{�^������
    public void ClearTitleButton() {
        SceneManager.LoadScene("TitleScene"); // �^�C�g���V�[���ɑJ��
    }

    // Frogs���X�g�ɐV����Frog��ǉ�
    public void InFrogs(GameObject frog) {
        _frogs.Add(frog);
        _frogsrb2d.Add(frog.GetComponent<Rigidbody2D>());
    }

    // �v���C���[���̐ݒ�
    public void MaxPlayer(int playerNumber) {
        _playerNumber = playerNumber;
    }

    // �v���C���[�܂���CPU�̒E������
    public void DropOuts(GameObject dropOutFrog) 
     {
        // �v���C���[���^�O�uPlayer�v�̏ꍇ�A�l�����J�E���g
        foreach (GameObject players in _frogs) {
            if (players.CompareTag("Player")) 
            {
                _alivePlayersCount++;
            }
        }
        if (dropOutFrog.CompareTag("Player")) {
            _isPlayerDeth = true;
        }

        // �v���C���[��3�l�ȏ�c���Ă���ꍇ�̃t���O�ݒ�
        if (_alivePlayersCount == 3 || _alivePlayersCount == 4) 
        {
            _threeOrMorePeople = true;
        }

        // �E������Frog�������L���O���X�g�ɒǉ�
        _rankingList.Insert(0, dropOutFrog);

        // Frogs���X�g����E������Frog���폜
        for (int frogCount = 0; frogCount < _frogs.Count; frogCount++) {
            if (_frogs[frogCount] == dropOutFrog) 
            {
              
                _frogs.Remove(_frogs[frogCount]);
            }
        }

        // �v���C���[�܂���CPU�̃J�E���g�����炷
        if (dropOutFrog.gameObject.CompareTag("Player")) 
        {
            _playerCount--;
        } 
        else if (dropOutFrog.gameObject.CompareTag("CPU"))
        {
            _cpuCount--;
        }

        // �v���C���[��CPU�̍��v��4�𒴂���ꍇ�AFrog���A�N�e�B�u�ɂ���
        if (_cpuCount + _playerCount > 4) {
            dropOutFrog.SetActive(false);
        }

        // �Q�[���̏I�������ɒB�����ꍇ�̏���
        int gameEndPlayerCount = 1;
       
        if (_cpuCount + _playerCount <= 4) 
        {
            // �����L���O���X�g�Ɏc���Frog��ǉ����A�^�C���X�P�[�������Z�b�g����
            _rankingList.Insert(0, _frogs[0]);
            StartCoroutine(TimeScaleReset(dropOutFrog));
            _cameraRank.CameeeraRank(false);
            _sneak.Access(true);
            _cameraShake.StopCameraShake(true);
         
        }
        else if (_isPlayerDeth && _alivePlayersCount == gameEndPlayerCount && !_threeOrMorePeople) {
            float gameSpeed = 3;
            Time.timeScale = gameSpeed;
            _stageRoopScript.CompulsionHarryUP();
        }

        // ���Z�b�g
        _alivePlayersCount = 0;
        _isPlayerDeth = false;
    }

    // �^�C���X�P�[�������Z�b�g����R���[�`��
    private IEnumerator TimeScaleReset(GameObject leaveFrog) {
        leaveFrog.GetComponent<SpriteRenderer>().enabled=false;
        // �E������Frog�̎���ɃA�E�g���C����\��
        _outLineParent.transform.SetParent(leaveFrog.transform, true);
        _outLineParent.transform.position = new Vector3(leaveFrog.transform.position.x + 5, leaveFrog.transform.position.y, 0);
        // �J������Frog�̈ʒu�Ɉړ�
        _camera.transform.position = new Vector3(leaveFrog.transform.position.x, leaveFrog.transform.position.y, -10);
        _cameraScale = true;
        Rigidbody2D rb = leaveFrog.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.4f);
        leaveFrog.SetActive(false);
        _switchNumber = 5;
        
    }

}
