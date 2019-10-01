using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRota : MonoBehaviour
{
    #region variaveis
    public float velocidade;
    [Range(0, 85)] public float limiteRotacao;

    Vector3 deltaRotacao = Vector3.zero;
    #endregion

    #region componentes
    Transform meu_transform;
    #endregion

    #region callbacks
    void Awake()
    {
        meu_transform = GetComponent<Transform>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // guarda rotacao local
        var rotacao_local = meu_transform.localEulerAngles;

        // define eixo y de delta rotacao através do movimento do mouse em X
        deltaRotacao.y = Input.GetAxis("Mouse X");

        // se estiver dentro dos limites ou fora dos limites, mas com movimento do mouse voltando pra dentro
        // então define eixo x de delta rotacao através do movimento do mouse em X
        // senão, define eixo x de delta rotacao em 0
        {
            // obtem movimento de mouse em Y
            float mouse_y = Input.GetAxis("Mouse Y");

            // verificação se pode rotacionar ou não
            bool pode_rotacionar =
                (rotacao_local.x < limiteRotacao || rotacao_local.x > (360f - limiteRotacao))         // rotacao dentro dos limites
                || (rotacao_local.x > limiteRotacao && rotacao_local.x < 180f && mouse_y > 0) // rotacao fora do limite positivo, mas mouse voltando pra dentro
                || (rotacao_local.x < (360f - limiteRotacao) && rotacao_local.x > 180f && mouse_y < 0); // rotacao fora do limite negativo, mas mouse voltando pra dentro

            // define o valor apropriado
            deltaRotacao.x = pode_rotacionar ? -mouse_y : 0f;
        }

        // normaliza delta rotacao se necessário
        if (deltaRotacao.magnitude > 1)
            deltaRotacao.Normalize();

        // multiplica-o por velocidade e delta tempo
        deltaRotacao *= velocidade * Time.deltaTime;

        // aplica rotação através de soma de rotação local
        meu_transform.localEulerAngles = rotacao_local + deltaRotacao;
    }

    #endregion
}
