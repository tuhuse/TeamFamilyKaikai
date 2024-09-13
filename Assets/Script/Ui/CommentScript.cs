using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentScript : MonoBehaviour {
    private Text _commentText = default;

    private float _commentChangeTime = default;
    private int _randomValue = default;
    private int _commentNumber =0;
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
    private const float HURFCOLOR = 0.5f;//半透明の時のRGBカラーの値
    private const float COLOR_A = 0.6f; //半透明の時のAのカラー値

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
            //５秒に一回コメントを変更する
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
            //コメントの表示
            CommentatorCommentChange("さあ、せっせんがつづいています！", false);

            if (!_isCommentatorSpeak) {
                //実況者を大きくする
                CommentatorExpansion();
            }

            //解説者が大きくなっていたら
            if (_isLiveCommentatorSpeak) {
                //解説者を小さくする
                LiveCommentatorReduction();
            }

        } else if (_randomValue == 2) {
            //実況者
            CommentatorCommentChange("こんかいはだれがいきのこるのでしょうか！", false);

            if (!_isCommentatorSpeak) {
                //実況者を大きくする
                CommentatorExpansion();
            }


            //解説者が大きくなっていたら
            if (_isLiveCommentatorSpeak) {
                //解説者を小さくする
                LiveCommentatorReduction();
            }
        } else if (_randomValue == 3) {
            //実況者
            CommentatorCommentChange("いわやとげにきをつけてがんばってください", false);

            if (!_isCommentatorSpeak) {
                //実況者を大きくする
                CommentatorExpansion();
            }

            //解説者が大きくなっていたら
            if (_isLiveCommentatorSpeak) {
                //解説者を小さくする
                LiveCommentatorReduction();
            }

        } else if (_randomValue == 4) {
            //解説者
            CommentatorCommentChange("あぶないとき、いかにしたをつかうかがたいせつですね", false);

            if (!_isLiveCommentatorSpeak) {
                //解説者を拡大
                LiveCommentatorExpansion();
            }



            //実況者が大きくなっていたら
            if (_isCommentatorSpeak) {
                //実況者を縮小
                CommentatorReduction();

            }

        } else if (_randomValue == 5) {
            //解説者
            CommentatorCommentChange("どのタイミングでアイテムをつかうか、ちゅうもくです", false);

            if (!_isLiveCommentatorSpeak) {
                //解説者を拡大
                LiveCommentatorExpansion();
            }

            //実況者が大きくなっていたら
            if (_isCommentatorSpeak) {
                //実況者を縮小
                CommentatorReduction();
            }
        } else if (_randomValue == 6) {
            //解説者
            CommentatorCommentChange("このコースには、がけがあるそうです！きをつけて！", false);

            if (!_isLiveCommentatorSpeak) {
                //解説者を拡大
                LiveCommentatorExpansion();
            }

            //実況者が大きくなっていたら
            if (_isCommentatorSpeak) {
                //実況者を縮小
                CommentatorReduction();
            }
        }

    }

    /// <summary>
    /// 実況者の立ち絵の拡大
    /// </summary>
    private void CommentatorExpansion() 
    {
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
    private void LiveCommentatorReduction() 
    {
        _liveCommentatorAnim.SetBool("Speak", true);
        _getLiveCommentatorRecttransform.position -= new Vector3(-COMMANTATORPOSITIONY, COMMANTATORPOSITIONY, 0); // 868 -450 0

        _getLiveCommentatorRecttransform.localScale -= new Vector3(COMMANTATORSCALEX, COMMANTATORSCALEY, COMMANTATORSCALEZ);// 2.5, 1.8, 1

        _isLiveCommentatorSpeak = false;

        //色を薄くする
        _liveCommentatorImage.color = new Color(HURFCOLOR, HURFCOLOR, HURFCOLOR, COLOR_A);
    }




    public void CommentatorCommentChange(string message, bool isGameStart) {

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
            _commentChangeTime = 0;
            _commentText.text = message;
        }


    }

    public void LiveCommentatorCommentChange(string message, bool isGameStart) 
    {
        if (!_isStartComment) {

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

    public void TutorialCommentChange() 
    {
        _commentNumber++;

        switch (_commentNumber) 
        {
            case 1:
                CommentatorCommentChange("ここではゲームのそうさほうほうについてせつめいしていくケロ", false);
                StartCoroutine(LiveComment(2));
                break;

            case 2:
               
                    CommentatorCommentChange("Lスティックをみぎにたおしていどう、ひだりにたおすとブレーキがかかるケロ", false);
                    StartCoroutine(LiveComment(2));
                    
                
                break;
            case 3:
               
                    CommentatorCommentChange("Aボタンでジャンプができるケロ", false);
                    StartCoroutine(LiveComment(2));
                    
                
                break;
            case 4:
              
                    CommentatorCommentChange("Rボタンでベロをだすケロ。あいてにあてることができればけいせいぎゃくてんのチャンス！いっきにまえへすすむことができるケロ", false);
                    StartCoroutine(LiveComment(3));
                    
                

                break;
            case 5:
               
                   CommentatorCommentChange("ステージちゅうにはさまざまなアイテムがあるケロ。にじいろにひかるハエをとり、アイテムをゲットするケロ", false);
                    StartCoroutine(LiveComment(3));
                    
                
                break;
            case 6:

                StartCoroutine(LiveComment(0));

                break; case 7:
               
                    CommentatorCommentChange("そうさほうほうはいじょうケロ。さあ、デスゲームかいじょうにとうちゃくケロ！", false);
                    StartCoroutine(LiveComment(2));
                   
                break;
        }
       
    }
    private IEnumerator LiveComment(float waitTime) 
    { 
       

        yield return new WaitForSeconds(waitTime);

            switch (_commentNumber) {
            case 1:

                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("そうさほうほうをおぼえて、デスゲームでいきのこれるようにするケロ", false);
                print("a");
                break;

            case 2:
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("ブレーキをかけてわざとうしろにさがるなど、かわったせんぽうがたのしめるケロ", false);

                break;

            case 3:
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("タイミングをみてしょうがいぶつをよけるケロ！さっそくとんでみるケロ", false);

                break;

            case 4:
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("いちどだすとクールダウンがはっせいするので、ここぞというタイミングでつかうケロ", false);
                break;
            case 5:
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("アイテムはぜんぶで4しゅるい、じぶんのあしがはやくなるなど、こうかはさまざまケロ。", false);
                break;
            case 6:
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("いままでのふくしゅうをしてみるケロ！", false);
                break;
            case 7:               
                yield return new WaitForSeconds(waitTime);
                LiveCommentatorCommentChange("ここでまなんだことをかつようして、たべられないようにがんばるケロ〜！！", false);
                break;
        }


    }
}
 