using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float CharacterLife;
    public Texture Blood,Line;
    public int LifeFull = 100;

    void Start()

    {
        CharacterLife = LifeFull;
    }

    void Update()
    {

        if (CharacterLife >= LifeFull)
        {
            CharacterLife = LifeFull;

        }

        else if (CharacterLife <= 0)
        {
            CharacterLife = 0;

        }

        void OnGUI ()
        {
            GUI.DrawTexture (new Rect (Screen.width/40, Screen.height /40, Screen.width / 5, Screen.height / 8), Line);
            GUI.DrawTexture(new Rect(Screen.width / 40, Screen.height / 40, Screen.width / 8, Screen.height / 8), Line);
        }
    }
}