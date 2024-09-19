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

                    LiveCommentatorCommentChange("�������ق��ق������ڂ��āA�f�X�Q�[���ł����̂����悤�ɂ���P��", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 2:
                    _frogVoice.Stop();
                    CommentatorCommentChange("L�X�e�B�b�N���݂��ɂ������Ă��ǂ��A�Ђ���ɂ������ƃu���[�L��������P��", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 3:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("�u���[�L�������Ă킴�Ƃ�����ɂ�����ȂǁA�����������ۂ������̂��߂�P��", false, _tutolialVoices[_nextCommentNumber]);
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

        if (_commentChangeTime >= 5 && _isStart) {
            //�T�b�Ɉ��R�����g��ύX����
            _commentChangeTime = 0;
            GiveMessage();
        } else if (_commentChangeTime >= 5 && _isStartComment) {
            _isStart = true;
            _commentChangeTime = 0;
            _isStartComment = false;
            GiveMessage();
        }

    }

    private void GiveMessage() {
        _randomValue = Random.Range(1, 6);
        if (_randomValue == 1) {
            //�R�����g�̕\��
            CommentatorCommentChange("�����A�������񂪂Â��Ă��܂��I", false, _commentatorVoices[0]);

            if (!_isCommentatorSpeak) {
                //�����҂�傫������
                CommentatorExpansion();
            }

            //����҂��傫���Ȃ��Ă�����
            if (_isLiveCommentatorSpeak) {
                //����҂�����������
                LiveCommentatorReduction();
            }

        } else if (_randomValue == 2) {
            //������
            CommentatorCommentChange("���񂩂��͂��ꂪ�����̂���̂ł��傤���I", false, _commentatorVoices[1]);

            if (!_isCommentatorSpeak) {
                //�����҂�傫������
                CommentatorExpansion();
            }


            //����҂��傫���Ȃ��Ă�����
            if (_isLiveCommentatorSpeak) {
                //����҂�����������
                LiveCommentatorReduction();
            }
        } else if (_randomValue == 3) {
            //������
            LiveCommentatorCommentChange("�����Ƃ��ɂ������Ă���΂��Ă�������", false, _liveCommentatorVoices[0]);

            //if (!_isCommentatorSpeak) {
            //    //�����҂�傫������
            //    CommentatorExpansion();
            //}

            ////����҂��傫���Ȃ��Ă�����
            //if (_isLiveCommentatorSpeak) {
            //    //����҂�����������
            //    LiveCommentatorReduction();
            //}

        } else if (_randomValue == 4) {
            //�����
            CommentatorCommentChange("���ԂȂ��Ƃ��A�����ɂ��������������������ł���", false, _liveCommentatorVoices[1]);

            if (!_isLiveCommentatorSpeak) {
                //����҂��g��
                LiveCommentatorExpansion();
            }



            //�����҂��傫���Ȃ��Ă�����
            if (_isCommentatorSpeak) {
                //�����҂��k��
                CommentatorReduction();

            }

        } else if (_randomValue == 5) {
            //�����
            LiveCommentatorCommentChange("�ǂ̃^�C�~���O�ŃA�C�e�����������A���イ�����ł�", false, _liveCommentatorVoices[2]);

            //if (!_isLiveCommentatorSpeak) {
            //    //����҂��g��
            //    LiveCommentatorExpansion();
            //}

            ////�����҂��傫���Ȃ��Ă�����
            //if (_isCommentatorSpeak) {
            //    //�����҂��k��
            //    CommentatorReduction();
            //}
        } else if (_randomValue == 6) {
            //�����
            LiveCommentatorCommentChange("���̃R�[�X�ɂ́A���Ƃ����Ȃ��邻���ł��I�������āI", false, _liveCommentatorVoices[1]);

            if (!_isLiveCommentatorSpeak) {
                //����҂��g��
                LiveCommentatorExpansion();
            }

            //�����҂��傫���Ȃ��Ă�����
            if (_isCommentatorSpeak) {
                //�����҂��k��
                CommentatorReduction();
            }
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
                CommentatorCommentChange("R�{�^���Ńx���������P���B�����Ăɂ��Ă邱�Ƃ��ł���΂����������Ⴍ�Ă�̃`�����X�I�������ɂ܂��ւ����ނ��Ƃ��ł���P��", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;


                break;
            case 11:
                _frogVoice.Stop();
                CommentatorCommentChange("�X�e�[�W���イ�ɂ͂��܂��܂ȃA�C�e��������P���B�ɂ�����ɂЂ���n�G���Ƃ�A�A�C�e�����Q�b�g����P��", false, _tutolialVoices[_nextCommentNumber]);
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
                CommentatorCommentChange("�������ق��ق��͂����傤�P���B�����A�f�X�Q�[���������傤�ɂƂ����Ⴍ�P���I", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
        }

    }

    public void CommentatorCommentChange(string message, bool isGameStart, AudioClip frogVoiceClip) {

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
            _commentChangeTime = 0;
            _commentText.text = message;
        }


    }

    public void LiveCommentatorCommentChange(string message, bool isGameStart, AudioClip frogVoiceClip) {
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
