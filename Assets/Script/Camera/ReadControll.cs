using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadControll : MonoBehaviour
{
  
    private int _maxPlayers = 2;
    private GameObject[] _players;

    // Start is called before the first frame update
    void Start()
    {
        _players = new GameObject[_maxPlayers];
        for (int number = 0; number < _maxPlayers; number++) {
            //Debug.Log("Joysticks " + (number + 1) + "is " + joyStickName[number]);
            _players[number].SetActive(false);
          
        }
    }
    public void StartGame(int playerCount) {
        for (int i = 0; i < playerCount; i++) {
            _players[i].SetActive(true);  // 選択したプレイヤー数に応じてアクティブにする
            PlayercontrollerScript controller = _players[i].GetComponent<PlayercontrollerScript>();
            controller._playernumber = i + 1; // プレイヤー番号を設定
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   

}
