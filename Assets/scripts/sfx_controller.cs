using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source for playing sounds")]
    public AudioSource audioSource;

    [Header("audio clips")]
    public AudioClip clickSound;
    public AudioClip attackWithWeapon;

    public AudioClip punch;
    //public AudioClip hitSound;
    //public AudioClip explosionSound;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// playing sound by name
    /// </summary>
    public void PlaySound(string soundName)
    {
        AudioClip clip = soundName switch
        {
            "enemyKill" => clickSound,
            "attackWithWeapon" => attackWithWeapon,
            "punch" => punch,
            //"hit" => hitSound,
            //"explosion" => explosionSound,
            _ => null
        };

        if (clip != null)
            audioSource.PlayOneShot(clip);
        else
            Debug.LogWarning($"Sound '{soundName}' not found!");
    }
}
