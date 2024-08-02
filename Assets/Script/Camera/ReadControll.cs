using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadControll : MonoBehaviour
{
    public GameObject _playerPrefab; // プレイヤープレハブをアサインする
    public int _maxPlayers = 4; // 最大プレイヤー数

    private string[] _joystickNames;
    // Start is called before the first frame update
    void Start()
    {
        string[] joyStickName = Input.GetJoystickNames();
        for (int number = 0; number < joyStickName.Length; number++) {
            Debug.Log("Joysticks " + (number + 1) + "is " + joyStickName[number]);

            if (number < _maxPlayers) {
                CreatePlayer(number + 1); // プレイヤー番号は1から始める
            }
        }
    }
    public void CreatePlayer(int playerNumber) {
        GameObject newPlayer = Instantiate(_playerPrefab);
        PlayercontrollerScript controller = newPlayer.GetComponent<PlayercontrollerScript>();
        controller._playernumber = playerNumber; // プレイヤー番号を設定
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   

}
