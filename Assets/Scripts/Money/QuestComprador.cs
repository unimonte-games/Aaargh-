using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuestComprador : MonoBehaviour
{
    Money script;
    public GameObject QuestVendUI;
    public GameObject[] objToCreate;
    public Transform posToCreate;
    public int cost;
    void Start()
    {
        script = GameObject.FindWithTag("GameController").GetComponent<Money>();
    }
    void OnTriggerEnter()
    {
        QuestVendUI.SetActive(true);
        Cursor.visible = true;
    }
    void OnTriggerExit()
    {
        QuestVendUI.SetActive(false);
        Cursor.visible = false;
    }
    public void QuestItem()
    {
        script.gold -= cost;
        PhotonNetwork.Instantiate(objToCreate[4].name, posToCreate.position, posToCreate.rotation);
    }
}
