using UnityEngine;

public class PlayLoopBGM : MonoBehaviour
{
    public AudioClip[] clips;

    void Start()
    {
        AudioClip clip = AudioManager.GetRandomSound(clips);
        AudioManager.Instance.Play2DSound(clip, "BGM", 1, 1, true);
    }
}
