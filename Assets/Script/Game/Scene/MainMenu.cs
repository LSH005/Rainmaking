using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        ScreenTransition.JustLoadScene("Eva", "LoadingScene2");
    }
}
