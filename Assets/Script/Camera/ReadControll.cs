using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadControll : MonoBehaviour
{
    public GameObject _playerPrefab; // �v���C���[�v���n�u���A�T�C������
    public int _maxPlayers = 4; // �ő�v���C���[��

    private string[] _joystickNames;
    // Start is called before the first frame update
    void Start()
    {
        string[] joyStickName = Input.GetJoystickNames();
        for (int number = 0; number < joyStickName.Length; number++) {
            Debug.Log("Joysticks " + (number + 1) + "is " + joyStickName[number]);

            if (number < _maxPlayers) {
                CreatePlayer(number + 1); // �v���C���[�ԍ���1����n�߂�
            }
        }
    }
    public void CreatePlayer(int playerNumber) {
        GameObject newPlayer = Instantiate(_playerPrefab);
        PlayercontrollerScript controller = newPlayer.GetComponent<PlayercontrollerScript>();
        controller._playernumber = playerNumber; // �v���C���[�ԍ���ݒ�
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   

}
