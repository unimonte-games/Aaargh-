using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviTeste : MonoBehaviour
{
    public float velocidade;

    // otimização :3 (evita coleta de lixo desnecessário)
    Vector3 deltaMovimento = Vector3.zero;
    public Transform raioRef;
    public float raioDist, chaoDist;
    Transform meu_transform;

    void Awake()
    {
        meu_transform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // guarda de antemão a rotação
        var rotacaoAntes = meu_transform.rotation;

        // aplica movimento
        {
            // reseta rotação para que a função Translate não nos faça voar
            meu_transform.rotation = Quaternion.Euler(0, meu_transform.localEulerAngles.y, 0);

            // define eixos usando entrada do usuário
            deltaMovimento.x = Input.GetAxis("Horizontal");
            deltaMovimento.z = Input.GetAxis("Vertical");

            // normaliza se necessário
            if (deltaMovimento.magnitude > 1)
                deltaMovimento.Normalize();

            // multiplica pela velocidade e delta tempo
            deltaMovimento *= velocidade * Time.deltaTime;

            // aplica movimento
            meu_transform.Translate(deltaMovimento);
        }

        // acompanhar chão
        {
            // faz raycast pra baixo
            Ray raio = new Ray(raioRef.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(raio, out hit, raioDist))
            {
                // acompanha distância do chão
                Vector3 pos = meu_transform.position;
                pos.y = hit.point.y + chaoDist;
                meu_transform.position = pos;
            }
        }

        // redefine a rotação que tinha antes
        meu_transform.rotation = rotacaoAntes;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // se não se esqueceu de referenciar a referencia do raio
        if (raioRef)
        {
            // cria raio e hit
            Ray raio = new Ray(raioRef.position, Vector3.down);
            RaycastHit hit;

            // faz raycast (é um raio (linha) matemática que colide em colliders, o resultado é passado pra hit)
            if (Physics.Raycast(raio, out hit, raioDist))
            {
                // se fez contato, troca a cor de Gizmos pra verde
                // (Gizmos são as linhas exclusivas do editor)
                Gizmos.color = Color.green;

                // desenha ponto de colisão do raycast disparado (isso pega a superfície colidida do collider)
                Gizmos.DrawWireCube(hit.point, Vector3.one * 0.1f);

                // pega posição do próprio transform e
                // define a altura da posição como a altura do ponto colidido do raycast + distância do chão
                Vector3 pos = transform.position;
                pos.y = hit.point.y + chaoDist;

                // desenha essa posição calculada
                Gizmos.DrawWireSphere(pos, 0.1f);
            }
            else // senão colidiu em nada, então desenha Gizmos vermelhos (só a linha vai ser desenhada)
            {
                Gizmos.color = Color.red;
            }

            // desenha linha representando Raycast
            Gizmos.DrawLine(raioRef.position, raioRef.position + Vector3.down * raioDist);
        }
    }
#endif
}

