using ChainCube.Managers;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (_audioSource != null && GameSettingManager.IsSoundOn)
        {
            _audioSource.Play();
        }
    }
}
