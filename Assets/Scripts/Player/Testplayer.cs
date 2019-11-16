using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testplayer : MonoBehaviour
{
    public static GameObject NewPlayer(int v)
    {
        GameObject acaca = null;
        switch(v)
        {
            case 0:
              acaca = Resources.Load<GameObject>("Parrudinho");
                break;
            case 1:
                acaca = Resources.Load<GameObject>("Boba da Corte");
                break;
            case 2:
                acaca = Resources.Load<GameObject>("Conjurador");
                break;
            case 3:
                acaca = Resources.Load<GameObject>("Cozinheiro");
                break;
        }
    return acaca;
    }
}
