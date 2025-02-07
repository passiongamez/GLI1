using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _gunShot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunShot()
    {
        _audioSource.PlayOneShot(_gunShot);
        _audioSource.PlayDelayed(.5f);
    }
}
