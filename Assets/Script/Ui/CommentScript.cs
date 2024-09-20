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

    private string _1Pname = "けろこ";
    private string _2Pname = "けろせい";
    private string _3Pname = "けろれお";
    private string _4Pname = "けろや";

    [SerializeField] GameObject _commentator = default;
    [SerializeField] GameObject _liveCommentator = default;

    private RectTransform _getCommentatorRecttransform = default;
    private RectTransform _getLiveCommentatorRecttransform = default;

    private Image _commentatorImage = default;
    private Image _liveCommentatorImage = default;

    private const float MAXCOLORVALUE = 255f;
    private const float HURFCOLOR = 0.5f;//半透明の時のRGBカラーの値
    private const float COLOR_A = 0.6f; //半透明の時のAのカラー値

    private const float COMMANTATORPOSITIONX = 97;
    private const float COMMANTATORPOSITIONY = 58;

    private const float COMMANTATORSCALEX = 1.5f;
    private const float COMMANTATORSCALEY = 1.2f;
    private const float COMMANTATORSCALEZ = 1f;

    private Animator _commentatorAnim = default;
    private Animator _liveCommentatorAnim = default;

    private AudioSource _frogVoice = default;

    [Header("レース中の実況者のボイス"), SerializeField] private List<AudioClip> _commentatorVoices = default;
    [Header("レース中の実況者のボイス"), SerializeField] private List<AudioClip> _liveCommentatorVoices = default;

    [Header("チュートリアル中の声"), SerializeField] private List<AudioClip> _tutolialVoices = default;

    [SerializeField] private Player2 _player2Script = default;
    [SerializeField] private GameObject _xButton = default;

    [SerializeField] private CameraRankScript _rankScript = default;
    [Header("トップカエル名ボイス"), SerializeField] private List<AudioClip> _topFrogNameVoices = default;
    [Header("蛇に近いカエル名ボイス"), SerializeField] private List<AudioClip> _lowestFrogNameVoices = default;
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

                    LiveCommentatorCommentChange("そうさほうほうをおぼえて、デスレースでいきのこれるようにするケロ", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 2:
                    _frogVoice.Stop();
                    CommentatorCommentChange("Lスティックをみぎにたおしていどう、" + "\n" + "ひだりにたおすとブレーキがかかるケロ", false, _tutolialVoices[_nextCommentNumber]);
                    _nextCommentNumber++;
                    break;

                case 3:
                    _frogVoice.Stop();
                    LiveCommentatorCommentChange("ブレーキをかけてわざととまるなど、かわったせんぽうがたのしめるケロ", false, _tutolialVoices[_nextCommentNumber]);
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
                    LiveCommentatorCommentChange("タイミングをみてしょうがいぶつをよけるケロ！さっそくとんでみるケロ", false, _tutolialVoices[_nextCommentNumber]);

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
                    LiveCommentatorCommentChange("いちどだすとクールダウンがはっせいするので、ここぞというタイミングでつかうケロ", false, _tutolialVoices[_nextCommentNumber]);

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
                    LiveCommentatorCommentChange("アイテムはぜんぶで4しゅるい、じぶんのあしがはやくなるなど、こうかはさまざまケロ。", false, _tutolialVoices[_nextCommentNumber]);

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
                    LiveCommentatorCommentChange("ここでまなんだことをかつようして、たべられないようにがんばるケロ～！！", false, _tutolialVoices[_nextCommentNumber]);
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
            //５秒に一回コメントを変更する
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
                //実況者
                CommentatorCommentChange("さあ、せっせんがつづいているケロ！", false, _commentatorVoices[0]);

                break;
            case 1:
                //実況者
                CommentatorCommentChange("こんかいはだれがいきのこるケロ！？", false, _commentatorVoices[1]);
                break;

            case 2:
                //実況者
                LiveCommentatorCommentChange("いわやとげにきをつけてがんばるケロ！", false, _liveCommentatorVoices[0]);
                break;

            case 3:

                //解説者
                LiveCommentatorCommentChange("あぶないとき、いかにしたをつかうかがたいせつケロ！", false, _liveCommentatorVoices[1]);

                break;

            case 4:

                //解説者
                LiveCommentatorCommentChange("どのタイミングでアイテムをつかうか、ちゅうもくケロ", false, _liveCommentatorVoices[2]);

                break;

            case 5:

                //解説者
                LiveCommentatorCommentChange("このコースには、おとしあながあるケロ！きをつけて！", false, _liveCommentatorVoices[3]);

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


                CommentatorCommentChange("げんざい、トップをはしっているのは" + topFrogName + "だケロ", false, _commentatorVoices[case6VoiceNumber]);
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


                CommentatorCommentChange("げんざい、へびにもっともちかいのは" + lowestFrogName + "だケロ", false, _commentatorVoices[case7VoiceNumber]);
                StartCoroutine(CommentDialogue());
                break;

            case 8:
                _isCommentStop = true;
                CommentatorCommentChange("じっきょうは、わたくし「けろてつ」と", false, _commentatorVoices[4]);
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
                    LiveCommentatorCommentChange("このままはしりぬいてほしいケロ！", false, _liveCommentatorVoices[4]);
                }

                break;
            case 7:
                _isCommentStop = false;
                _commentChangeTime = 0;
                if (!_isInterrupt) {
                    LiveCommentatorCommentChange("もうあとがないケロ、いそいでまえへでるケロ！！", false, _liveCommentatorVoices[5]);
                }


                break;
            case 8:
                _isCommentStop = false;
                _commentChangeTime = 0;
                if (!_isInterrupt) {
                    LiveCommentatorCommentChange("かいせつのけろちーばでおおくりするケロ！", false, _liveCommentatorVoices[6]);
                }

                break;
        }
    }

    /// <summary>
    /// 実況者の立ち絵の拡大
    /// </summary>
    private void CommentatorExpansion() {
        _commentatorAnim.SetBool("Speak", true);
        _getCommentatorRecttransform.position += new Vector3(COMMANTATORPOSITIONX, COMMANTATORPOSITIONY, 0);// -776 -398 0

        _getCommentatorRecttransform.localScale += new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ); //4 3 1

        _isCommentatorSpeak = true;

        //色を濃くする
        _commentatorImage.color = new Color(MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE);

    }

    /// <summary>
    /// 実況者の縮小
    /// </summary>
    private void CommentatorReduction() {
        _commentatorAnim.SetBool("Speak", false);
        _getCommentatorRecttransform.position -= new Vector3(COMMANTATORPOSITIONX, COMMANTATORPOSITIONY, 0); // -873 -456 0

        _getCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isCommentatorSpeak = false;

        //色を薄くする
        _commentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }

    /// <summary>
    ///　解説者の拡大
    /// </summary>
    private void LiveCommentatorExpansion() {
        _liveCommentatorAnim.SetBool("Speak", true);
        _getLiveCommentatorRecttransform.position += new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0);// 810 -392 0

        _getLiveCommentatorRecttransform.localScale += new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ); //4 3 1

        _isLiveCommentatorSpeak = true;

        //色を濃くする
        _liveCommentatorImage.color = new Color(MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE, MAXCOLORVALUE);
    }

    /// <summary>
    /// 解説者の縮小
    /// </summary>
    private void LiveCommentatorReduction() {
        _liveCommentatorAnim.SetBool("Speak", true);
        _getLiveCommentatorRecttransform.position -= new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0); // 868 -450 0

        _getLiveCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isLiveCommentatorSpeak = false;

        //色を薄くする
        _liveCommentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }






    public void TutorialCommentChange() {

        switch (_nextCommentNumber) {
            case 0:
                _frogVoice.Stop();
                CommentatorCommentChange("ここではゲームのそうさほうほうについてせつめいしていくケロ", false, _tutolialVoices[_nextCommentNumber]);
                _isNextComment = true;
                _xButton.SetActive(true);
                _nextCommentNumber++;
                break;


            case 5:
                _frogVoice.Stop();
                CommentatorCommentChange("Aボタンでジャンプができるケロ", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 8:
                _frogVoice.Stop();
                CommentatorCommentChange("Rボタンでベロをだすケロ。あいてにあてることができればけいせいぎゃくてんのチャンス！" + "\n" + "いっきにまえへすすむことができるケロ", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;


                break;
            case 11:
                _frogVoice.Stop();
                CommentatorCommentChange("レースちゅうでつかえるさまざまなアイテムがあるケロ。にじいろにひかるハエをとり、アイテムをゲットするケロ", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 14:
                _frogVoice.Stop();
                LiveCommentatorCommentChange("いままでのふくしゅうをしてみるケロ！", false, _tutolialVoices[_nextCommentNumber]);
                _xButton.SetActive(true);
                _isNextComment = true;
                _nextCommentNumber++;
                break;
            case 16:
                _frogVoice.Stop();
                CommentatorCommentChange("そうさほうほうはいじょうケロ。" + "\n" + "さあ、デスレースかいじょうにとうちゃくケロ！", false, _tutolialVoices[_nextCommentNumber]);
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
                //実況者を大きくする
                CommentatorExpansion();
            }


            //解説者が大きくなっていたら
            if (_isLiveCommentatorSpeak) {
                //解説者を小さくする
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
                //実況者を小さくする

                CommentatorReduction();
            }


            //解説者が大きくなっていたら
            if (!_isLiveCommentatorSpeak) {
                //解説者を大きくする
                LiveCommentatorExpansion();
            }

            _isStartComment = isGameStart;
            _commentChangeTime = 0;
            _commentText.text = message;
        }
    }
}
