using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentScript : MonoBehaviour {
    private Text _commentText = default;

    private float _commentChangeTime = default;
    private int _randomValue = default;
    private bool _isStart = false;
    private bool _isStartComment = false;

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

    // Start is called before the first frame update
    void Start() {
        _commentText = this.GetComponent<Text>();
        _getCommentatorRecttransform = _commentator.GetComponent<RectTransform>();
        _getLiveCommentatorRecttransform = _liveCommentator.GetComponent<RectTransform>();

        _commentatorImage = _commentator.GetComponent<Image>();
        _liveCommentatorImage = _liveCommentator.GetComponent<Image>();

        _commentatorAnim = _commentator.GetComponent<Animator>();
        _liveCommentatorAnim = _liveCommentator.GetComponent<Animator>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _isStart = true;
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
            CommentChange("�����A�������񂪂Â��Ă��܂��I", false);

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
            CommentChange("���񂩂��͂��ꂪ�����̂���̂ł��傤���I", false);

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
            CommentChange("�����Ƃ��ɂ������Ă���΂��Ă�������", false);

            if (!_isCommentatorSpeak) {
                //�����҂�傫������
                CommentatorExpansion();
            }

            //����҂��傫���Ȃ��Ă�����
            if (_isLiveCommentatorSpeak) {
                //����҂�����������
                LiveCommentatorReduction();
            }

        } else if (_randomValue == 4) {
            //�����
            CommentChange("���ԂȂ��Ƃ��A�����ɂ��������������������ł���", false);

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
            CommentChange("�ǂ̃^�C�~���O�ŃA�C�e�����������A���イ�����ł�", false);

            if (!_isLiveCommentatorSpeak) {
                //����҂��g��
                LiveCommentatorExpansion();
            }

            //�����҂��傫���Ȃ��Ă�����
            if (_isCommentatorSpeak) {
                //�����҂��k��
                CommentatorReduction();
            }
        } else if (_randomValue == 6) {
            //�����
            CommentChange("���̃R�[�X�ɂ́A���������邻���ł��I�������āI", false);

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
        _getLiveCommentatorRecttransform.position -= new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0); // 868 -450 0

        _getLiveCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isLiveCommentatorSpeak = false;

        //�F�𔖂�����
        _liveCommentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }




    public void CommentChange(string message, bool isGameStart) {

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
}