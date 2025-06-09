using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audClips;

    AudioSource audSource;
    AudioSource cursorAudSource;


    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
        audSource = GetComponent<AudioSource>();
        cursorAudSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 0.4f;
        audSource.PlayOneShot(audClips[0]);
    }

    public void PointSound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 0.5f;
        audSource.PlayOneShot(audClips[1]);
    }

    public void PageRustlingSound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 0.5f;
        audSource.PlayOneShot(audClips[2]);
    }

    public void AddingEntrySound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 1f;
        audSource.PlayOneShot(audClips[3]);
    }

    public void CursorSound()
    {
        cursorAudSource.pitch = Random.Range(2.90f, 3.00f);
        cursorAudSource.volume = 0.3f;
        cursorAudSource.PlayOneShot(audClips[4]);
    }

    public void LeverSound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 0.9f;
        audSource.PlayOneShot(audClips[5]);
    }

    public void WallShiftSound()
    {
        audSource.pitch = Random.Range(0.95f, 1.05f);
        audSource.volume = 0.9f;
        audSource.PlayOneShot(audClips[6]);
    }
}
