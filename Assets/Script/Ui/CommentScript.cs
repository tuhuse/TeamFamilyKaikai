using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentScript : MonoBehaviour {
    private Text _commentText = default;

    private float _commentChangeTime = default;
    private int _randomValue = default;
    private int _nextCommentNumber = 0;
    private bool _isStart = false;
    private bool _isStartComment = false;
    private bool _isNextComment = false;

    private bool _isCommentatorSpeak = false;
    private bool _isLiveCommentatorSpeak = false;
    private bool _isCommentStop = false;
    private bool _isInterrupt = false;

    private string _1Pname = "���낱";
    private string _2Pname = "���낹��";
    private string _3Pname = "����ꂨ";
    private string _4Pname = "�����";

    [SerializeField] GameObject _commentator = default;
    [SerializeField] GameObject _liveCommentator = default;

    private RectTransform _getCommentatorRecttransform = default;
    private RectTransform _getLiveCommentatorRecttransform = default;

    private Image _commentatorImage = default;
    private Image _liveCommentatorImage = default;

    private const float MAXCOLORVALUE = 255f;
    private const float HURFCOLOR = 0.5f;//�������̎���RGB�J���[�̒l
    private const float COLOR_A = 0.6f; //�������̎���A�̃J���[�l

    private const float COMMANTATORPOSITIONX = 97;
    private const float COMMANTATORPOSITIONY = 58;

    private const float COMMANTATORSCALEX = 1.5f;
    private const float COMMANTATORSCALEY = 1.2f;
    private const float COMMANTATORSCALEZ = 1f;

    private Animator _commentatorAnim = default;
    private Animator _liveCommentatorAnim = default;

    private AudioSource _frogVoice = default;

    [Header("���[�X���̎����҂̃{�C�X"), SerializeField] private List<AudioClip> _commentatorVoices = default;
    [Header("���[�X���̎����҂̃{�C�X"), SerializeField] private List<AudioClip> _liveCommentatorVoices = default;

    [Header("�`���[�g���A�����̐�"), SerializeField] private List<AudioClip> _tutolialVoices = default;

    [SerializeField] private Player2 _player2Script = default;
    [SerializeField] private GameObject _xButton = default;

    [SerializeField] private CameraRankScript _rankScript = default;
    [Header("�g�b�v�J�G�����{�C�X"), SerializeField] private List<AudioClip> _topFrogNameVoices = default;
    [Header("�ւɋ߂��J�G�����{�C�X"), SerializeField] private List<AudioClip> _lowestFrogNameVoices = default;
    // Start is called before the first frame update
    private void Awake() {
        _commentatorAnim = _commentator.GetComponent<Animator>();
        _liveCommentatorAnim = _liveCommentator.GetComponent<Animator>();
        _commentText = this.GetComponent<Text>();
        _getCommentatorRecttransform = _commentator.GetComponent<RectTransform>();
        _getLiveCommentatorRecttransform = _liveCommentator.GetComponent<RectTransform>();

        _commentatorImage = _commentator.GetComponent<Image>();
        _liveCommentatorImage = _liveCommentator.GetComponent<Image>();
        _frogVoice = this.GetComponent<AudioSource>();

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _isStart = true;
        }

        if (_isNextComment && Input.GetButtonDown("Fire1")) {
            switch (_nextCommentNumber) {
                case 1:
                    _frogVoice.Stop();

                    LiveCommentatorCommentChange("�������ق��ق������ڂ��āA�f�X���[�X�ł����̂����悤�ɂ���P��", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 2:
                    _frogVoice.Stop();
                    CommentatorCommentChange("L�X�e�B�b�N���݂��ɂ������Ă��ǂ��A" + "\n" + "�Ђ���ɂ������ƃu���[�L��������P��", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 3:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�u���[�L�������Ă킴�ƂƂ܂�ȂǁA�����������ۂ������̂��߂�P��", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;



                    break;

                case 4:
                    _frogVoice.Stop();
                    _commentText.text = "";

                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }

                    _player2Script.StartWait();
                    _isNextComment = false;
                    _xButton.SetActive(false);


                    _nextCommentNumber++;
                    break;
                case 6:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�^�C�~���O���݂Ă��傤�����Ԃ��悯��P���I���������Ƃ�ł݂�P��", false, _tutolialVoices[_nextCommentNumber]);

                    _nextCommentNumber++;
                    break;
                case 7:
                    _frogVoice.Stop();
                    _commentText.text = "";
                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }
                    _player2Script.StartWait();
                    _isNextComment = false;
                    _xButton.SetActive(false);

                    _nextCommentNumber++;

                    break;
                case 9:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�����ǂ����ƃN�[���_�E�����͂���������̂ŁA�������Ƃ����^�C�~���O�ł����P��", false, _tutolialVoices[_nextCommentNumber]);

                    _nextCommentNumber++;

                    break;
                case 10:
                    _frogVoice.Stop();
                    _commentText.text = "";
                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }
                    _player2Script.StartWait();
                    _isNextComment = false;
                    _nextCommentNumber++;
                    _xButton.SetActive(false);

                    break;
                case 12:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�A�C�e���͂���Ԃ�4����邢�A���Ԃ�̂������͂₭�Ȃ�ȂǁA�������͂��܂��܃P���B", false, _tutolialVoices[_nextCommentNumber]);

                    _nextCommentNumber++;
                    break;
                case 13:
                    _frogVoice.Stop();
                    _commentText.text = "";
                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }
                    _player2Script.StartWait();
                    _isNextComment = false;
                    _nextCommentNumber++;
                    _xButton.SetActive(false);
                    break;
                case 15:
                    _frogVoice.Stop();
                    _commentText.text = "";
                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }
                    _player2Script.StartWait();
                    _isNextComment = false;
                    _nextCommentNumber++;
                    _xButton.SetActive(false);
                    break;
                case 17:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�����ł܂Ȃ񂾂��Ƃ����悤���āA���ׂ��Ȃ��悤�ɂ���΂�P���`�I�I", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;
                case 18:
                    _frogVoice.Stop();
                    _commentText.text = "";
                    if (_isCommentatorSpeak) {
                        CommentatorReduction();
                    }
                    if (_isLiveCommentatorSpeak) {
                        LiveCommentatorReduction();
                    }
                    _player2Script.StartWait();
                    _isNextComment = false;
                    _nextCommentNumber++;
                    _xButton.SetActive(false);
                    break;
            }
        }

        _commentChangeTime += Time.deltaTime;

        if (_commentChangeTime >= 5 && _isStart && !_isCommentStop) {
            //�T�b�Ɉ��R�����g��ύX����
            _commentChangeTime = 0;
            GiveMessage();
        } else if (_commentChangeTime >= 5 && _isStartComment && !_isCommentStop) {
            _isStart = true;
            _commentChangeTime = 0;
            _isStartComment = false;
            GiveMessage();
        }

    }

    private void GiveMessage() {
        int kerokoVoice = 0;
        int keroseiVoice = 1;
        int keroreoVoice = 2;
        int keroyaVoice = 3;

        _isInterrupt = false;
        _randomValue = Random.Range(0, 9);
        switch (_randomValue) {
            case 0:
                //������
                CommentatorCommentChange("�����A�������񂪂Â��Ă���P���I", false, _commentatorVoices[0]);

                break;
            case 1:
                //������
                CommentatorCommentChange("���񂩂��͂��ꂪ�����̂���P���I�H", false, _commentatorVoices[1]);
                break;

            case 2:
                //������
                LiveCommentatorCommentChange("�����Ƃ��ɂ������Ă���΂�P���I", false, _liveCommentatorVoices[0]);
                break;

            case 3:

                //�����
                LiveCommentatorCommentChange("���ԂȂ��Ƃ��A�����ɂ��������������������P���I", false, _liveCommentatorVoices[1]);

                break;

            case 4:

                //�����
                LiveCommentatorCommentChange("�ǂ̃^�C�~���O�ŃA�C�e�����������A���イ�����P��", false, _liveCommentatorVoices[2]);

                break;

            case 5:

                //�����
                LiveCommentatorCommentChange("���̃R�[�X�ɂ́A���Ƃ����Ȃ�����P���I�������āI", false, _liveCommentatorVoices[3]);

                break;

            case 6:
                _isCommentStop = true;
                string topFrogName = _rankScript._ranking[0].name;




                int case6VoiceNumber = 2;
                if (topFrogName == _1Pname) {
                    topFrogName = "<color=green>" + topFrogName + "</color>";
                    _commentatorVoices[case6VoiceNumber] = _topFrogNameVoices[kerokoVoice];
                } else if (topFrogName == _2Pname) {
                    topFrogName = "<color=#F0F121>" + topFrogName + "</color>";
                    _commentatorVoices[case6VoiceNumber] = _topFrogNameVoices[keroseiVoice];
                } else if (topFrogName == _3Pname) {
                    topFrogName = "<color=blue>" + topFrogName + "</color>";
                    _commentatorVoices[case6VoiceNumber] = _topFrogNameVoices[keroreoVoice];
                } else if (topFrogName == _4Pname) {
                    topFrogName = "<color=#E030C4>" + topFrogName + "</color>";
                    _commentatorVoices[case6VoiceNumber] = _topFrogNameVoices[keroyaVoice];
                }


                CommentatorCommentChange("���񂴂��A�g�b�v���͂����Ă���̂�" + topFrogName + "���P��", false, _commentatorVoices[case6VoiceNumber]);
                StartCoroutine(CommentDialogue());
                break;

            case 7:
                _isCommentStop = true;
                string lowestFrogName = _rankScript._ranking[3].name;
                if (!_rankScript._ranking[3].activeSelf) {
                    lowestFrogName = _rankScript._ranking[2].name;
                }
                if (!_rankScript._ranking[2].activeSelf) {
                    lowestFrogName = _rankScript._ranking[1].name;
                }




                int case7VoiceNumber = 3;
                if (lowestFrogName == _1Pname) {
                    lowestFrogName = "<color=green>" + lowestFrogName + "</color>";
                    _commentatorVoices[case7VoiceNumber] = _lowestFrogNameVoices[kerokoVoice];
                } else if (lowestFrogName == _2Pname) {
                    lowestFrogName = "<color=#F0F121>" + lowestFrogName + "</color>";
                    _commentatorVoices[case7VoiceNumber] = _lowestFrogNameVoices[keroseiVoice];
                } else if (lowestFrogName == _3Pname) {
                    lowestFrogName = "<color=blue>" + lowestFrogName + "</color>";
                    _commentatorVoices[case7VoiceNumber] = _lowestFrogNameVoices[keroreoVoice];
                } else if (lowestFrogName == _4Pname) {
                    lowestFrogName = "<color=#E030C4>" + lowestFrogName + "</color>";
                    _commentatorVoices[case7VoiceNumber] = _lowestFrogNameVoices[keroyaVoice];
                }


                CommentatorCommentChange("���񂴂��A�ւтɂ����Ƃ��������̂�" + lowestFrogName + "���P��", false, _commentatorVoices[case7VoiceNumber]);
                StartCoroutine(CommentDialogue());
                break;

            case 8:
                _isCommentStop = true;
                CommentatorCommentChange("�������傤�́A�킽�����u����Ăv��", false, _commentatorVoices[4]);
                StartCoroutine(CommentDialogue());
                break;
        }

    }
    private IEnumerator CommentDialogue() {
        float waitTime = 3f;
        yield return new WaitForSeconds(waitTime);
        switch (_randomValue) {

            case 6:
                _isCommentStop = false;
                _commentChangeTime = 0;
                if (!_isInterrupt) {
                    LiveCommentatorCommentChange("���̂܂܂͂���ʂ��Ăق����P���I", false, _liveCommentatorVoices[4]);
                }

                break;
            case 7:
                _isCommentStop = false;
                _commentChangeTime = 0;
                if (!_isInterrupt) {
                    LiveCommentatorCommentChange("�������Ƃ��Ȃ��P���A�������ł܂��ւł�P���I�I", false, _liveCommentatorVoices[5]);
                }


                break;
            case 8:
                _isCommentStop = false;
                _commentChangeTime = 0;
                if (!_isInterrupt) {
                    LiveCommentatorCommentChange("�������̂��낿�[�΂ł������肷��P���I", false, _liveCommentatorVoices[6]);
                }

                break;
        }
    }

    /// <summary>
    /// �����҂̗����G�̊g��
    /// </summary>
    private void CommentatorExpansion() {
        _commentatorAnim.SetBool("Speak", true);
        _getCommentatorRecttransform.position += new Vector3(COMMANTATORPOSITIONX, COMMANTATORPOSITIONY, 0);// -776 -398 0

        _getCommentatorRecttransform.localScale += new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ); //4 3 1

        _isCommentatorSpeak = true;

        //�F��Z������
        _commentatorImage.color = new Color(MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE);

    }

    /// <summary>
    /// �����҂̏k��
    /// </summary>
    private void CommentatorReduction() {
        _commentatorAnim.SetBool("Speak", false);
        _getCommentatorRecttransform.position -= new Vector3(COMMANTATORPOSITIONX, COMMANTATORPOSITIONY, 0); // -873 -456 0

        _getCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isCommentatorSpeak = false;

        //�F�𔖂�����
        _commentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }

    /// <summary>
    ///�@����҂̊g��
    /// </summary>
    private void LiveCommentatorExpansion() {
        _liveCommentatorAnim.SetBool("Speak", true);
        _getLiveCommentatorRecttransform.position += new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0);// 810 -392 0

        _getLiveCommentatorRecttransform.localScale += new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ); //4 3 1

        _isLiveCommentatorSpeak = true;

        //�F��Z������
        _liveCommentatorImage.color = new Color(MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE);
    }

    /// <summary>
    /// ����҂̏k��
    /// </summary>
    private void LiveCommentatorReduction() {
        _liveCommentatorAnim.SetBool("Speak", true);
        _getLiveCommentatorRecttransform.position -= new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0); // 868 -450 0

        _getLiveCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isLiveCommentatorSpeak = false;

        //�F�𔖂�����
        _liveCommentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }






    public void TutorialCommentChange() {

        switch (_nextCommentNumber) {
            case 0:
                _frogVoice.Stop();
                CommentatorCommentChange("�����ł̓Q�[���̂������ق��ق��ɂ��Ă��߂����Ă����P��", false, _tutolialVoices[_nextCommentNumber]);
                _isNextComment = true;
                _xButton.SetActive(true);
                _nextCommentNumber++;
                break;


            case 5:
                _frogVoice.Stop();
                CommentatorCommentChange("A�{�^���ŃW�����v���ł���P��", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 8:
                _frogVoice.Stop();
                CommentatorCommentChange("R�{�^���Ńx���������P���B�����Ăɂ��Ă邱�Ƃ��ł���΂����������Ⴍ�Ă�̃`�����X�I" + "\n" + "�������ɂ܂��ւ����ނ��Ƃ��ł���P��", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;


                break;
            case 11:
                _frogVoice.Stop();
                CommentatorCommentChange("���[�X���イ�ł����邳�܂��܂ȃA�C�e��������P���B�ɂ�����ɂЂ���n�G���Ƃ�A�A�C�e�����Q�b�g����P��", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 14:
                _frogVoice.Stop();
                LiveCommentatorCommentChange("���܂܂ł̂ӂ����イ�����Ă݂�P���I", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 16:
                _frogVoice.Stop();
                CommentatorCommentChange("�������ق��ق��͂����傤�P���B" + "\n" + "�����A�f�X���[�X�������傤�ɂƂ����Ⴍ�P���I", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
        }

    }

    public void Interrupt() {
        _isInterrupt = true;
    }

    public void CommentatorCommentChange(string message, bool isGameStart, AudioClip frogVoiceClip) {
        _commentChangeTime = 0;
        _frogVoice.Stop();
        _frogVoice.PlayOneShot(frogVoiceClip);
        if (!_isStartComment) {

            if (!_isCommentatorSpeak) {
                //�����҂�傫������
                CommentatorExpansion();
            }


            //����҂��傫���Ȃ��Ă�����
            if (_isLiveCommentatorSpeak) {
                //����҂�����������
                LiveCommentatorReduction();
            }

            _isStartComment = isGameStart;

            _commentText.text = message;
        }


    }

    public void LiveCommentatorCommentChange(string message, bool isGameStart, AudioClip frogVoiceClip) {
        _frogVoice.Stop();

        if (!_isStartComment) {
            _frogVoice.PlayOneShot(frogVoiceClip);
            if (_isCommentatorSpeak) {
                //�����҂�����������

                CommentatorReduction();
            }


            //����҂��傫���Ȃ��Ă�����
            if (!_isLiveCommentatorSpeak) {
                //����҂�傫������
                LiveCommentatorExpansion();
            }

            _isStartComment = isGameStart;
            _commentChangeTime = 0;
            _commentText.text = message;
        }
    }
}
