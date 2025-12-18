using System.Collections;
using UnityEngine;

public class MulmeoggaeEscape : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitOneHourRoutine());
    }
    
    IEnumerator WaitOneHourRoutine()
    {
        yield return new WaitForSeconds(3600f);
        OnTimerFinished();
    }

    void OnTimerFinished()
    {
        ScreenTransition.JustLoadScene("Eva", "LoadingScene");
    }
}
