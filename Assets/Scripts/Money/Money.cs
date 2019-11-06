using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public float gold;

    public GameObject moneyUI;

    void Start()
    {
        moneyUI = GameObject.Find("Coin");
    }
    void Update()
    {
        moneyUI.GetComponent<Text>().text = gold.ToString();
        if(gold < 0)
        {
            gold = 0;
        }
    }
}
