using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoukEditManager : MonoBehaviour
{
    [System.Serializable]
    public class PhoukEditSounds
    {
        public AudioClip music;
        public string musicName;
    }

    [Header("Post Process")]
    public GameObject postProcess;

    [Header("이미지")]
    public Image image;
    public TextMeshProUGUI text;
    public Sprite defaultSprite;
    public Sprite[] sprites;

    [Header("사운드")]
    public PhoukEditSounds[] sounds;

    bool isActived = false;
    const string soundName = "PhoukEditMusic";

    void Start()
    {
        StopPhoukEdit();
    }

    void Update()
    {
        if (!isActived)
        {
            if (ReleaseConditions()) return;

            if (Pressing(KeyCode.N))
            {
                StartPhoukEdit();
            }
        }
        else
        {
            if (!Pressing(KeyCode.N) || ReleaseConditions())
            {
                StopPhoukEdit();
            }
        }
    }

    void StartPhoukEdit()
    {
        isActived = true;

        PhoukEditSounds currnetPhoukEditSounds = GetRandomPhoukEditSound(sounds);

        postProcess.SetActive(true);
        image.sprite = GetRandomSprite(sprites);
        text.text = currnetPhoukEditSounds.musicName;
        AudioManager.Instance.Play2DSound(currnetPhoukEditSounds.music, soundName);
        Time.timeScale = 0.0f;
    }

    void StopPhoukEdit()
    {
        isActived = false;

        postProcess.SetActive(false);
        image.sprite = defaultSprite;
        text.text = "";
        AudioManager.Instance.StopSound(soundName);
        Time.timeScale = 1.0f;
    }

    PhoukEditSounds GetRandomPhoukEditSound(PhoukEditSounds[] phoukEditSounds)
    {
        if (phoukEditSounds == null || phoukEditSounds.Length == 0) return null;
        int randomIndex = Random.Range(0, phoukEditSounds.Length);
        return phoukEditSounds[randomIndex];
    }

    Sprite GetRandomSprite(Sprite[] sprites)
    {
        if (sprites == null || sprites.Length == 0) return null;

        int randomIndex = Random.Range(0, sprites.Length);
        return sprites[randomIndex];
    }

    bool Pressing(KeyCode key) => Input.GetKey(key);

    bool ReleaseConditions()
    {
        return ScreenTransition.isTransitioning || GamePause.isPaused;
    }
}
