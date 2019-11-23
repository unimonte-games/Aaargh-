using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsCenes2 : MonoBehaviour
{
    public string levelToLoad;
    public float timer = 10f;
    private Text timerSeconds;
    void Start()
    {
        timerSeconds = GetComponent<Text>();
        StartCoroutine(Tempo());
    }
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f0");
    }
    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(timer);
        PhotonNetwork.LoadLevel(2);
    }
}
