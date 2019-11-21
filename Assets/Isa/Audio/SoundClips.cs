using UnityEngine;
using System.Collections;

public class SoundClips : MonoBehaviour
{
    public AudioClip arma;
    public AudioClip andar;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //audioSource.PlayOneShot(arma, 0.7F);
            AudioSource.PlayClipAtPoint(arma, new Vector3(5, 3, 1));
        }
    }

}
