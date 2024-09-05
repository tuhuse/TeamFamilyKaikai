using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextScript : MonoBehaviour
{[SerializeField]
    private AudioClip[] _audio;
    private AudioSource[] _audioSource;
    [SerializeField]
    private ClearMan _clear;
    [SerializeField]
    private GameOverMan _gameOver;
    private bool _isPlay=true;
    [SerializeField]
    private CountDowntext _count;
    [SerializeField] CommentScript _commentScript = default;
    [SerializeField] private StageRoopManFixed _stage;
    // Start is called before the first frame update

    private void Start() 
    {
        _audioSource = GetComponents<AudioSource>();
        
      _audioSource[0].clip = _audio[0];
        _audioSource[1].clip = _audio[1];      
        _count._bgm.Play();
        //_audioSource[0].PlayOneShot(_audio[0]);
        _commentScript.CommentChange("さあ、だい" + Random.Range(1, 101) + "かい、カエルデスゲームがスタート！！！", true);
        _stage.ReadyGo(true);
    }
    private void Update() 
    {
        if (_isPlay) {
            if (_clear._switchNumber == 6) {
                _audioSource[0].Play();
                _count._bgm.Stop();
                _isPlay = false;
            }
            if (_gameOver._switchNumber == 4) {
                _count._bgm.Stop();
                _audioSource[1].PlayOneShot(_audio[1]);
            }

        }
        
    }
 
}
