using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private PlayerInfo PI;

    private void Awake()
    {
        PI = FindObjectOfType<PlayerInfo>();
    }

    public void OnClickCharacterPick(int whichCharacter)
    {
        if (PI != null)
        {
            PlayerInfo.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }
}
