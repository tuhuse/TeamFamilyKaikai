using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ExitButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
   public void Onclick() 
   {

        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I���i�r���h����Ƃ��폜���Ȃ���G���[�o��j
        Application.Quit();//�Q�[���v���C�I��
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (_animator != null) {
            _animator.SetBool("ExitButton", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (_animator != null) {
            _animator.SetBool("ExitButton", false);
        }
    }

    public void EnableCanvasAndTriggerAnimation(Canvas canvas) {
        _audioSource.PlayOneShot(_audioClip);
        canvas.enabled = true;

        // �K�v�ł���΁A�����ŃA�j���[�V�����������I�ɍăg���K�[����
        if (_animator != null) {
            // ���݂̏�Ԃ����Z�b�g�܂��͋����I�ɐݒ肷��
            _animator.SetBool("ExitButton", false);
        }
    }
}
