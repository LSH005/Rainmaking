using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private float volume = 1.0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        ResetAllSoundVolume();
    }

    private void ResetAllSoundVolume()
    {
       
    }

    public void PlaysoundAtPoint
        (AudioClip clip, Vector3 point, string soundName = "TempAudio",
        float volumeMultiple = 1.0f, float pitch = 1.0f)
    {
        if (clip == null) return;

        GameObject tempGO = new GameObject(soundName);
        tempGO.transform.position = point;

        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.spatialBlend = 1.0f;

        audioSource.volume = volume * volumeMultiple;

        audioSource.Play();
        Object.Destroy(tempGO, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale) + 1);
    }
}
