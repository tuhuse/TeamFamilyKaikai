using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCharacter : MonoBehaviour {
    [SerializeField] private GameObject _commentator = default;
    //起動するカエル
    [SerializeField] private GameObject[] _frog;
    [SerializeField] private GameObject _countDown;
    [SerializeField] private GameObject _stage;
    [SerializeField] private GameObject _birdCanvas;
    [SerializeField] private GameObject _sneak = default;
    public int _enemyNumber;
    private float _positionY = 440f;
    private AudioSource _audiosource;
    [SerializeField]
    private AudioClip[] _audioClip;
    public GameObject _player;
    public GameObject _cpu;
    [SerializeField] private GameObject _pauseManager;
    [SerializeField] private AudioManager _audiomanager;
    [SerializeField] private ClearMan _clearManScript;
    [SerializeField] private GameOverMan _gameoverManScript;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _frogcutin;
    [SerializeField] private Image[] _playerIcon;
    [SerializeField] private Image[] _playerImage;
    [SerializeField] private GameObject[] _eye;
    [SerializeField] CameraRankScript _cameraScript = default;

    private enum Sitiuation {
        One,
        Two,
        Three,
        Four
    }
    private Sitiuation _sitiuation = default;
    ////[SerializeField] private GameObject[] _flogButtons; f
    // Start is called before the first frame update
    void Start() {
        _audiosource = GetComponent<AudioSource>();

        _audiosource.PlayOneShot(_audioClip[0]);

    }

    // Update is called once per frame
    void Update() {

    }


    public void GoTxt()//GoButtonを押したらメイン画面に行く
    {
        _countDown.SetActive(true);
        _birdCanvas.SetActive(true);
        _commentator.SetActive(true);
        StartCoroutine(FrogInstantiate());
    }

    public void SummonSneak() {
        StartCoroutine(CutIn());
    }

    private IEnumerator CutIn() {

        _audiosource.Stop();
        _frogcutin.SetActive(true);
        yield return new WaitForSeconds(1.2f);

        _sneak.SetActive(true);
        _menu.SetActive(false);
        _stage.SetActive(true);
    }
    public void Humans(int humans) {
        switch (humans) {
            case 1:
                _sitiuation = Sitiuation.One;
                break;
            case 2:
                _sitiuation = Sitiuation.Two;
                break;
            case 3:
                _sitiuation = Sitiuation.Three;
                break;
            case 4:
                _sitiuation = Sitiuation.Four;
                break;
        }
    }
    public void DiffcultNumberHarf(int number) {
        if (number == 0) {
            _enemyNumber = 0;
        }
        if (number == 1) {
            _enemyNumber = 1;
        }
        if (number == 2) {
            _enemyNumber = 2;
        }
    }
    private IEnumerator FrogInstantiate() {
        float firstpositionX = -360;
        float secondPositionX = -117;
        float thirdPositionX = 127;
        float forthPositionX = 360;


        yield return new WaitForSeconds(0.1f);
        switch (_sitiuation) {
            case Sitiuation.One:
                _audiomanager.PlayAudio(3);

                _frog[0].SetActive(true);
                _playerIcon[0].enabled = true;
                _playerImage[0].enabled = true;
                _playerImage[4].enabled = true;
                _eye[0].GetComponent<Animator>().enabled = true;
                _clearManScript.InFrogs(_frog[0]);
                _gameoverManScript.InFrogs(_frog[0]);
                _player = _frog[0];
                Destroy(_frog[1].GetComponent<FrogCpuMulti2>());
                Destroy(_frog[1].GetComponent<FrogCpuMulti>());
                _frog[1].SetActive(true);

                _gameoverManScript.InFrogs(_frog[1]);
                _clearManScript.InFrogs(_frog[1]);
                _cpu = _frog[1];

                Destroy(_frog[2].GetComponent<FrogCpuMulti>());
                _frog[2].SetActive(true);

                _gameoverManScript.InFrogs(_frog[2]);
                _clearManScript.InFrogs(_frog[2]);
                _cpu = _frog[2];

                _frog[3].SetActive(true);

                _gameoverManScript.InFrogs(_frog[3]);
                _clearManScript.InFrogs(_frog[3]);
                _cpu = _frog[3];

                _pauseManager.SetActive(true);


                _playerImage[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, _positionY, 0);


                _cameraScript.SceneStart();

                _clearManScript.MaxPlayer(1);

                break;
            case Sitiuation.Two:
                _audiomanager.PlayAudio(3);

                _frog[0].SetActive(true);
                _playerIcon[0].enabled = true;
                _playerImage[0].enabled = true;
                _playerImage[4].enabled = true;
                _eye[0].GetComponent<Animator>().enabled = true;
                _clearManScript.InFrogs(_frog[0]);
                _gameoverManScript.InFrogs(_frog[0]);
                _player = _frog[0];

                _playerIcon[1].enabled = true;
                _playerImage[1].enabled = true;
                _playerImage[5].enabled = true;
                _eye[1].GetComponent<Animator>().enabled = true;
                _frog[4].SetActive(true);
                _cameraScript.SecondPlayerOn(_frog[4]);

                _gameoverManScript.InFrogs(_frog[4]);
                _clearManScript.InFrogs(_frog[4]);
                _player = _frog[4];

                Destroy(_frog[1].GetComponent<FrogCpuMulti2>());
                Destroy(_frog[1].GetComponent<FrogCpu>());
                _frog[1].GetComponent<FrogCpuMulti>().enabled = true;
                _frog[1].SetActive(true);
                _gameoverManScript.InFrogs(_frog[1]);
                _clearManScript.InFrogs(_frog[1]);
                _cpu = _frog[1];

                _pauseManager.SetActive(true);


                Destroy(_frog[2].GetComponent<FrogCpu>());
                _frog[2].GetComponent<FrogCpuMulti>().enabled = true;
                _frog[2].SetActive(true);

                _gameoverManScript.InFrogs(_frog[2]);
                _clearManScript.InFrogs(_frog[2]);
                _cpu = _frog[2];

                _cameraScript.SceneStart();

                _clearManScript.MaxPlayer(2);


                _playerImage[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(secondPositionX, _positionY, 0);
                _playerImage[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(thirdPositionX, _positionY, 0);

                break;
            case Sitiuation.Three:
                _audiomanager.PlayAudio(3);

                _frog[0].SetActive(true);
                _playerIcon[0].enabled = true;
                _playerImage[0].enabled = true;
                _playerImage[4].enabled = true;
                _eye[0].GetComponent<Animator>().enabled = true;
                _clearManScript.InFrogs(_frog[0]);
                _gameoverManScript.InFrogs(_frog[0]);
                _player = _frog[0];

                _frog[4].SetActive(true);
                _playerIcon[1].enabled = true;
                _playerImage[1].enabled = true;
                _playerImage[5].enabled = true;
                _eye[1].GetComponent<Animator>().enabled = true;
                _cameraScript.SecondPlayerOn(_frog[4]);
                _cameraScript.ThirdPlayerOn(_frog[5]);

                _gameoverManScript.InFrogs(_frog[4]);
                _clearManScript.InFrogs(_frog[4]);
                _player = _frog[4];


                _frog[5].SetActive(true);
                _playerIcon[2].enabled = true;
                _playerImage[2].enabled = true;
                _playerImage[6].enabled = true;
                _eye[2].GetComponent<Animator>().enabled = true;
                _gameoverManScript.InFrogs(_frog[5]);
                _clearManScript.InFrogs(_frog[5]);
                _cpu = _frog[5];


                Destroy(_frog[1].GetComponent<FrogCpu>());
                Destroy(_frog[1].GetComponent<FrogCpuMulti>());
                _frog[1].GetComponent<FrogCpuMulti2>().enabled = true;
                _frog[1].SetActive(true);
                _gameoverManScript.InFrogs(_frog[1]);
                _clearManScript.InFrogs(_frog[1]);
                _cpu = _frog[1];

                _pauseManager.SetActive(true);


                _clearManScript.MaxPlayer(3);
                _cameraScript.SceneStart();

                float firstPositionX2 = -250;
                float thirdPositionX2 = 250;

                _playerImage[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(firstPositionX2, _positionY, 0);
                _playerImage[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, _positionY, 0);
                _playerImage[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(thirdPositionX2, _positionY, 0);


                break;
            case Sitiuation.Four:
                _audiomanager.PlayAudio(3);
                _playerIcon[0].enabled = true;
                _playerImage[0].enabled = true;
                _playerImage[4].enabled = true;
                _eye[0].GetComponent<Animator>().enabled = true;
                _frog[0].SetActive(true);

                _clearManScript.InFrogs(_frog[0]);
                _gameoverManScript.InFrogs(_frog[0]);
                _player = _frog[0];

                _frog[4].SetActive(true);
                _playerIcon[1].enabled = true;
                _playerImage[1].enabled = true;
                _playerImage[5].enabled = true;
                _eye[1].GetComponent<Animator>().enabled = true;
                _cameraScript.SecondPlayerOn(_frog[4]);
                _cameraScript.ThirdPlayerOn(_frog[5]);
                _cameraScript.FirthPlayerOn(_frog[6]);

                _gameoverManScript.InFrogs(_frog[4]);
                _clearManScript.InFrogs(_frog[4]);
                _player = _frog[4];




                _frog[5].SetActive(true);
                _playerIcon[2].enabled = true;
                _playerImage[2].enabled = true;
                _playerImage[6].enabled = true;
                _eye[2].GetComponent<Animator>().enabled = true;
                _gameoverManScript.InFrogs(_frog[5]);
                _clearManScript.InFrogs(_frog[5]);
                _player = _frog[5];


                _pauseManager.SetActive(true);


                _frog[6].SetActive(true);
                _playerIcon[3].enabled = true;
                _playerImage[3].enabled = true;
                _playerImage[7].enabled = true;
                _eye[3].GetComponent<Animator>().enabled = true;
                _gameoverManScript.InFrogs(_frog[6]);
                _clearManScript.InFrogs(_frog[6]);

                _player = _frog[6];

                _cameraScript.SceneStart();

                _clearManScript.MaxPlayer(4);

                _playerImage[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(firstpositionX, _positionY, 0);
                _playerImage[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(secondPositionX, _positionY, 0);
                _playerImage[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(thirdPositionX, _positionY, 0);
                _playerImage[3].GetComponent<RectTransform>().anchoredPosition = new Vector3(forthPositionX, _positionY, 0);


                break;
        }



    }
}
