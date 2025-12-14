using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    float volume = 1.0f;
    Dictionary<AudioSource, float> activeAudio = new Dictionary<AudioSource, float>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        ResetActiveAudioVolume();
    }

    public float GetVolume() => volume;

    void ResetActiveAudioVolume()
    {
        var keysToRemove = new List<AudioSource>();

        foreach (var AS in activeAudio)
        {
            if (AS.Key == null)
            {
                keysToRemove.Add(AS.Key);
                continue;
            }
            AS.Key.volume = volume * AS.Value;
        }

        foreach (var key in keysToRemove)
        {
            activeAudio.Remove(key);
        }
    }

    void Playsound
        (AudioClip clip, Vector3 point, string soundName = "TempAudio",
        float volumeMultiple = 1.0f, float pitch = 1.0f, bool is3D = true, bool isLoop = false)
    {
        if (clip == null) return;

        var tempGO = new GameObject(soundName);
        tempGO.transform.position = point;
        tempGO.transform.SetParent(transform);

        var AS = tempGO.AddComponent<AudioSource>();
        AS.clip = clip;
        AS.volume = volume * volumeMultiple;
        AS.pitch = pitch;
        AS.spatialBlend = is3D ? 1.0f : 0.0f;
        AS.loop = isLoop;
        AS.Play();

        activeAudio.Add(AS, volumeMultiple);

        if (!isLoop)
        {
            float soundDelay = clip.length + 0.3887f;
            StartCoroutine(CleanupAudioSource(AS, soundDelay));
            Object.Destroy(tempGO, soundDelay);
        }
    }
    IEnumerator CleanupAudioSource(AudioSource AS, float delay)
    {
        yield return new WaitForSeconds(delay);
        activeAudio.Remove(AS);
    }

    public void Play3DSound
        (AudioClip clip, Vector3 point, string soundName = "TempAudio",
        float volumeMultiple = 1.0f, float pitch = 1.0f, bool isLoop = false)
    {
        Playsound(clip, point, soundName, volumeMultiple, pitch, true, isLoop);
    }

    public void Play2DSound(AudioClip clip, string soundName = "TempAudio",
        float volumeMultiple = 1.0f, float pitch = 1.0f, bool isLoop = false)
    {
        Playsound(clip, Vector3.zero, soundName, volumeMultiple, pitch, false, isLoop);
    }

    public void StopSound(string soundName = "")
    {
        var keysToRemove = new List<AudioSource>();

        foreach (var AS in activeAudio.Keys)
        {
            if (AS == null)
            {
                keysToRemove.Add(AS);
                continue;
            }

            if (string.IsNullOrEmpty(soundName))
            {
                Destroy(AS.gameObject);
                keysToRemove.Add(AS);
                continue;
            }

            if (AS.gameObject.name == soundName)
            {
                Destroy(AS.gameObject);
                keysToRemove.Add(AS);
            }
        }

        foreach (var key in keysToRemove)
        {
            activeAudio.Remove(key);
        }
    }

    public AudioClip GetRandomSound(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return null;
        int randomIndex = Random.Range(0, clips.Length);
        return clips[randomIndex];
    }

}
