using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    public enum Sound
    {
        //Defaults
        ArmaDefault,
        HitDoPlayer,
        HitDoInimigo,
        Passos,
        //Inimigos
        Galinha,
        Praga,
        //Habilidades
        Boba1,
        Boba2,
        Conjurador1,
        Conjurador2,
        Cozinheiro1,
        Cozinheiro2,
        ExMarinheiro1,
        ExMarinheiro2
    }
    [PunRPC]
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));

    }
    private static AudioClip GetAudioClip(Sound sound)
    {
      foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
            { 
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found!");
        return null;
    }
}
