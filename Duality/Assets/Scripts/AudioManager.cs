using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultAmbience;

    private AudioSource track01, track02;
    private bool isPlayingTrack01;

    private void Start() {
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();

        track01.loop = true;
        track01.volume = 0.3f;

        track02.loop = true;

        isPlayingTrack01 = true;

        SwapTrack(defaultAmbience);
    }

    public void SwapTrack(AudioClip newClip) {
        
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));

        isPlayingTrack01 = !isPlayingTrack01;
    }

    IEnumerator FadeTrack(AudioClip newClip) {
        float timeToFade = 1.25f;
        float timeElapsed = 0f;

        if(isPlayingTrack01) {
            track02.clip = newClip;
            track02.Play();

            while (timeElapsed < timeToFade) {
                track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track01.volume = Mathf.Lerp(0.3f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track01.Stop();
        } else {

            track01.clip = newClip;
            track01.Play();

            while (timeElapsed < timeToFade) {
                track01.volume = Mathf.Lerp(0, 0.3f, timeElapsed / timeToFade);
                track02.volume = Mathf.Lerp(1f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track02.Stop();
        }
    }
}
