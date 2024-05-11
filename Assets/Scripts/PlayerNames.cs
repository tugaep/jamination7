using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNames
{
    public static string player1Name = "NoName1";
    public static string player2Name = "NoName2";

    public (string, string) GetNames()
    {
        return (player1Name, player2Name);
    }

    public void SetNames(string name1, string name2)
    {
        player1Name = name1;
        player2Name = name2;    
    }
}
