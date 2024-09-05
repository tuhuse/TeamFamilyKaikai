using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanPeople : MonoBehaviour {
    [Header("canvasmanager")]
    [SerializeField] private List<GameObject> _frogsButton = new List<GameObject>();  // �L�����N�^�[�I���{�^���̃��X�g
    [SerializeField] private SelectCharacter _selectCharacterScript = default;  // �L�����N�^�[�I���̃X�N���v�g
    //[SerializeField] private MoveButtonScript _move;  // �ړ��{�^���̃X�N���v�g
    [SerializeField] private Difficulty _difficulty;  // ��Փx�ݒ�̃X�N���v�g
    private AudioSource _audio;
    private int _humanPeople = default;  // ���ݑI������Ă���l���i�{�^���C���f�b�N�X�j
    private bool _isMoveButton = false;  // �ړ��{�^����������Ă��邩�ǂ���
    [SerializeField] private SpriteRenderer[] _frog;

    private enum Human {
        One,    // 1�l�v���C
        Two,    // 2�l�v���C
        Three,  // 3�l�v���C
        Four    // 4�l�v���C
    }
    private Human _humans = default;  // ���ݑI������Ă���v���C���[�l��
    private bool _select = false;  // �I����Ԃ��Ǘ�
    private Image _image;  // �{�^����Image�R���|�[�l���g�i���g�p�j

    // Start is called before the first frame update
    void Start() {
        _audio = GetComponent<AudioSource>();
        // �����������i���݂͋�j
        Selecet(true);
        SelectNumberPeople(true);
    }

    // Update is called once per frame
    void Update() {
        if (_select) {
            // �uFire1�v�{�^���������ꂽ�Ƃ��̏���
            //if (Input.GetButtonDown("Fire1")) {
            //    SelectNumberPeople(false);  // �{�^���̕\�����\���ɂ���
            //    _move._next = false;  // �ړ��̃t���O�����Z�b�g
            //    Selecet(false);  // �I����Ԃ�����
            //}

            // ���X�e�B�b�N�̐��������̓��͂��擾
            float controllerStick = Input.GetAxis("L_Stick_Vartical") * -1;

            // �X�e�B�b�N�����S�ɖ߂����Ƃ��̏���
            if (controllerStick == 0) {
                _isMoveButton = false;
            }

            // �X�e�B�b�N�����ɓ������Ƃ��̏���
            if (controllerStick < 0 && !_isMoveButton) {
                // ���݂̃{�^���̐F��ύX
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople++;
                // ���X�g�͈̔͊O�ɏo����ŏ��ɖ߂�
                if (_frogsButton.Count <= _humanPeople) {
                    _humanPeople = 0;
                }
                // �V�����{�^���̐F��ύX
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;
            }

            // �X�e�B�b�N����ɓ������Ƃ��̏���
            if (controllerStick > 0 && !_isMoveButton) {
                // ���݂̃{�^���̐F��ύX
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                _humanPeople--;
                // ���X�g�͈̔͊O�ɏo����Ō�ɖ߂�
                if (0 > _humanPeople) {
                    _humanPeople = _frogsButton.Count - 1;
                }
                // �V�����{�^���̐F��ύX
                _frogsButton[_humanPeople].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _isMoveButton = true;
            }

            // ���݂̐l���ݒ�ɉ���������
            HumanSwitch();

            // �u1pA�v�{�^���������ꂽ�Ƃ��̏���
            if (Input.GetButtonDown("1pA")) {
                
                if (_humans == Human.Four) {
                    // 4�l�v���C�̐ݒ�
                    Switch();
                    SelectNumberPeople(false);
                    _selectCharacterScript.SummonSneak();
                    Selecet(false);
                } else {
                    _audio.PlayOneShot(_audio.clip);
                    // 1�l�A2�l�܂���3�l�v���C�̐ݒ�
                    Switch();
                    _difficulty.Selecet(true);
                    _difficulty.Diffculty(true);
                    SelectNumberPeople(false);
                    Selecet(false);
                }
            }
        }
    }

    // �{�^�����N���b�N���ꂽ�Ƃ��̏���
    public void OnClick() {
        Switch();
    }

    // ���݂̃v���C���[�l���ɉ��������������s
    private void Switch() {
        switch (_humans) {
            case Human.One:
                _selectCharacterScript.Humans(1);�@// 1�l�v���C�ݒ�
                break;
            case Human.Two:
                _selectCharacterScript.Humans(2);  // 2�l�v���C�ݒ�
                break;
            case Human.Three:
                _selectCharacterScript.Humans(3);  // 3�l�v���C�ݒ�
                break;
            case Human.Four:
                _selectCharacterScript.Humans(4);  // 4�l�v���C�ݒ�
                break;
        }
    }

    // ���݂̃{�^���C���f�b�N�X�ɉ������v���C���[�l���̐ݒ�
    private void HumanSwitch() {
        switch (_humanPeople) {
            case 0:
                _humans = Human.One;
                _frog[1].enabled=false;
                _frog[2].enabled = false;
                _frog[6].enabled = false;
                _frog[7].enabled = false;
                _frog[8].enabled = false;
                _frog[9].enabled = false;
                _frog[0].enabled = true;
                break;
            case 1:
                _humans = Human.Two;
                _frog[3].enabled = false;
                _frog[4].enabled = false;
                _frog[5].enabled = false;
                _frog[0].enabled = false;
                _frog[1].enabled = true;
                _frog[2].enabled = true;
                break;
            case 2:
                _humans = Human.Three;
                _frog[1].enabled = false;
                _frog[2].enabled = false;
                _frog[6].enabled = false;
                _frog[7].enabled = false;
                _frog[8].enabled = false;
                _frog[9].enabled = false;
                _frog[3].enabled = true;
                _frog[4].enabled = true;
                _frog[5].enabled = true;
                break;
            case 3:
                _humans = Human.Four;
                _frog[0].enabled = false;
                _frog[3].enabled = false;
                _frog[4].enabled = false;
                _frog[5].enabled = false;
                _frog[6].enabled = true;
                _frog[7].enabled = true;
                _frog[8].enabled = true;
                _frog[9].enabled = true;
                break;
        }
    }

    // �I����Ԃ̐ݒ�
    public void Selecet(bool on) {
        if (on) {
            StartCoroutine(Boot());  // �I����Ԃɂ��邽�߂̃R���[�`�����J�n
        } else {
            _select = false;  // �I����Ԃ�����
        }
    }

    // �I����Ԃɂ��邽�߂̃R���[�`��
    private IEnumerator Boot() {
        yield return new WaitForSeconds(0.1f);  // 0.1�b�ҋ@
        _select = true;  // �I����Ԃɐݒ�
    }

    // �{�^���̕\��/��\����ݒ�
    public void SelectNumberPeople(bool buttonActive) {
        if (buttonActive) {
            // �{�^����\��
            _frogsButton[0].SetActive(true);
            _frogsButton[1].SetActive(true);
            _frogsButton[2].SetActive(true);
            _frogsButton[3].SetActive(true);
        }
        if (!buttonActive) {
            // �{�^����\��
            _frogsButton[0].SetActive(false);
            _frogsButton[1].SetActive(false);
            _frogsButton[2].SetActive(false);
            _frogsButton[3].SetActive(false);
        }

    }
}
