using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Photon.Pun;

[Serializable]
public class LaserOuMira2
{
    public bool AtivarMiraComum = true;
}

[Serializable]
public class Arma9192
{
    [HideInInspector]
    public int balasExtra, balasNoPente;
    //
    public int danoPorTiro = 40;
    [Range(0, 500)]
    public int numeroDeBalas = 240;
    [Range(1, 50)]
    public int balasPorPente = 30;
    [Range(0.01f, 5.0f)]
    public float tempoPorTiro = 0.3f;
    [Range(0.01f, 5.0f)]
    public float tempoDaRecarga = 0.5f;
    [Space(10)]
    public LaserOuMira Miras;
    [Space(10)]
    public GameObject objetoArma;
    public GameObject lugarParticula;
    public GameObject particulaFogo;
}
[RequireComponent(typeof(AudioSource))]
public class Tiros2 : MonoBehaviourPun, IPunObservable
{

    public KeyCode botaoRecarregar = KeyCode.R;
    public int armaInicial = 0;
    public string TagInimigo = "Inimigo";
    public Text BalasPente, BalasExtra;
    public Arma919[] armas;
    //
    int armaAtual;
    AudioSource emissorSom;
    bool recarregando, atirando;
    LineRenderer linhaDoLaser;
    GameObject luzColisao;

    public GameObject bullet;

    void Start()
    {
        if (photonView.IsMine)
        {
            for (int x = 0; x < armas.Length; x++)
            {
                armas[x].objetoArma.SetActive(false);
                armas[x].lugarParticula.SetActive(false);
                armas[x].balasExtra = armas[x].numeroDeBalas - armas[x].balasPorPente;
                armas[x].balasNoPente = armas[x].balasPorPente;
            }
            if (armaInicial > armas.Length - 1)
            {
                armaInicial = armas.Length - 1;
            }
            armas[armaInicial].objetoArma.SetActive(true);
            armas[armaInicial].lugarParticula.SetActive(true);
            armaAtual = armaInicial;
            emissorSom = GetComponent<AudioSource>();
            recarregando = atirando = false;
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            //UI
            BalasExtra.text = "BalasExtra: " + armas[armaAtual].balasExtra;
            BalasPente.text = "BalasNoPente: " + armas[armaAtual].balasNoPente;
            //troca de armas
            if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0 && recarregando == false && atirando == false)
            {
                if (Input.GetAxis("Y") > 0)
                {
                    armaAtual++;
                }
                if (Input.GetAxis("Y") < 0)
                {
                    armaAtual--;
                }
                if (armaAtual < 0)
                {
                    armaAtual = armas.Length - 1;
                }
                if (armaAtual > armas.Length - 1)
                {
                    armaAtual = 0;
                }
                AtivarArmaAtual();
            }
            //atirar
            if (Input.GetButtonDown("Fire1") && armas[armaAtual].balasNoPente > 0 && recarregando == false && atirando == false)
            {
                atirando = true;
                StartCoroutine(TempoTiro(armas[armaAtual].tempoPorTiro));
                emissorSom.clip = armas[armaAtual].somTiro;
                emissorSom.PlayOneShot(emissorSom.clip);
                armas[armaAtual].balasNoPente--;
                GameObject balaTemp = PhotonNetwork.Instantiate("Bullet", transform.position + transform.forward, transform.rotation);
                Destroy(balaTemp, 0.5f);
            }
            //recarregar
            if (Input.GetKeyDown(botaoRecarregar) && recarregando == false && atirando == false && (armas[armaAtual].balasNoPente < armas[armaAtual].balasPorPente) && (armas[armaAtual].balasExtra > 0))
            {
                emissorSom.clip = armas[armaAtual].somRecarga;
                emissorSom.PlayOneShot(emissorSom.clip);
                int todasAsBalas = armas[armaAtual].balasNoPente + armas[armaAtual].balasExtra;
                if (todasAsBalas >= armas[armaAtual].balasPorPente)
                {
                    armas[armaAtual].balasNoPente = armas[armaAtual].balasPorPente;
                    armas[armaAtual].balasExtra = todasAsBalas - armas[armaAtual].balasPorPente;
                }
                else
                {
                    armas[armaAtual].balasNoPente = todasAsBalas;
                    armas[armaAtual].balasExtra = 0;
                }
                recarregando = true;
                StartCoroutine(TempoRecarga(armas[armaAtual].tempoDaRecarga));
            }
            //laser da arma
            if (recarregando == false)
            {

                //checar limites da municao
                if (armas[armaAtual].balasNoPente > armas[armaAtual].balasPorPente)
                {
                    armas[armaAtual].balasNoPente = armas[armaAtual].balasPorPente;
                }
                else if (armas[armaAtual].balasNoPente < 0)
                {
                    armas[armaAtual].balasNoPente = 0;
                }
                int numBalasExtra = armas[armaAtual].numeroDeBalas - armas[armaAtual].balasPorPente;
                if (armas[armaAtual].balasExtra > numBalasExtra)
                {
                    armas[armaAtual].balasExtra = numBalasExtra;
                }
                else if (armas[armaAtual].balasExtra < 0)
                {
                    armas[armaAtual].balasExtra = 0;
                }
            }
        }
    }
    void AtivarArmaAtual()
    {
        for (int x = 0; x < armas.Length; x++)
        {
            armas[x].objetoArma.SetActive(false);
            armas[x].lugarParticula.SetActive(false);
        }
        armas[armaAtual].objetoArma.SetActive(true);
        armas[armaAtual].lugarParticula.SetActive(true);

    }
    IEnumerator TempoTiro(float tempoDoTiro)
    {
        yield return new WaitForSeconds(tempoDoTiro);
        atirando = false;
    }

    IEnumerator TempoRecarga(float tempoAEsperar)
    {
        yield return new WaitForSeconds(tempoAEsperar);
        recarregando = false;
    }

    void OnGUI()
    {
        if (armas[armaAtual].Miras.AtivarMiraComum == true)
        {
            GUIStyle stylez = new GUIStyle();
            stylez.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 20;
            GUI.Label(new Rect(Screen.width / 2 - 6, Screen.height / 2 - 12, 12, 22), "+");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
