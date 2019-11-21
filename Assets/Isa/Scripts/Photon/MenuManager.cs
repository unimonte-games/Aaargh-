using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    public GameObject playButton;

    public void OnPlayButtonClicked()
    {
        PhotonNetwork.LoadLevel(1);
        //aqui, ao invés de já ir pra uma sala que já carrega a fase e cria o seu player, tocar cutscene
        //depois dessa cutscene, PhotonNetwork.LoadLevel(cena que estiver para seleção de personagens), e dessa cena, cada botão instancia tal personagem na cena do jogo
    }
}
