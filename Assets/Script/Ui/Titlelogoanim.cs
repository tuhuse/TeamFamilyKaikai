using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titlelogoanim : MonoBehaviour
{
    private float _logoTime = default;
    private float _logotimelimit = 5f;
    private float _movieTime = default;
    private float _movieTimeLimit = 20f;
    private Animation _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        _movieTime += Time.deltaTime;
        _logoTime += Time.deltaTime;
        if (_logoTime >= _logotimelimit) {
            _logoTime = 0;
            _anim.Play("TitlePikoAnim");
        }
        if (_movieTime >= _movieTimeLimit) {
        
        }
    }
}
