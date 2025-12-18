using UnityEngine;

public class MulmeoggaeTerrains : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScreenTransition.JustLoadScene("Mulmeoggae", "LoadingScene");
        }
    }
}
