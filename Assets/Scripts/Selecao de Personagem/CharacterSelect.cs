using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayableCharacterType
{
    BobadCorte,
    Tritao,
    Conjurador,
    Novocap,
} 
[CreateAssetMenu(fileName = "characterSelect", menuName = "Roundbeargames/CharacterSelect/CharacterSelect")]
public class CharacterSelect : MonoBehaviour
{
    public PlayableCharacterType selectedCharacterType;  
    
    
}
