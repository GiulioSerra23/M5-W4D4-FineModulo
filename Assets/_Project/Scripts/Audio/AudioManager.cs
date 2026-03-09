using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [Header ("Sound Data")]
    [SerializeField] private SoundData[] _sounds;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(SoundID id)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.PlayOneShot(clip);
                return;
            }
        }
    }

    public void Play3D(SoundID id, Transform emitter)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];

                AudioSource source = emitter.GetComponent<AudioSource>();

                if (source == null)
                {
                    source = emitter.gameObject.AddComponent<AudioSource>();
                    source.spatialBlend = 1f;
                }

                source.volume = 0.05f;
                source.pitch = Random.Range(0.95f, 1.05f);
                source.PlayOneShot(clip);

                return;
            }
        }
    }
}
