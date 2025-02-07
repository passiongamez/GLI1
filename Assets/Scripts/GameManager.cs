using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioClip _trackCompleted;

    public void TrackFinish()
    {
        _audio.PlayOneShot(_trackCompleted);
    }
}
