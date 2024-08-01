using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDowntext : MonoBehaviour {
 
     public AudioSource _bgm;
    // Start is called before the first frame update
    void Start() {
        _bgm = GetComponent<AudioSource>();
        _bgm.Stop();
       
    }

    // Update is called once per frame
    void Update() {
       
    }
   
}
