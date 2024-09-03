using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DushSmokeAnimScript : MonoBehaviour
{
    private SpriteRenderer _dushSmokeSpriterenderer = default;
    private Animator _dushSmokeAnim = default;
    private void Start() {
        _dushSmokeAnim = this.GetComponent<Animator>();
        _dushSmokeSpriterenderer = this.GetComponent<SpriteRenderer>();
    }
    public void DushEffectOff() 
    {
        _dushSmokeAnim.SetBool("Dash", false);
        _dushSmokeSpriterenderer.enabled = false;
    }
}
