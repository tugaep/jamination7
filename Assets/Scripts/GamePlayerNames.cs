using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerNames : MonoBehaviour
{
    [SerializeField] Text player1Text;
    [SerializeField] Text player2Text;
    PlayerNames playerNames = new PlayerNames();

    private void Start()
    {
        var names = playerNames.GetNames();
        player1Text.text = names.Item1;
        player2Text.text = names.Item2;
    }
}
