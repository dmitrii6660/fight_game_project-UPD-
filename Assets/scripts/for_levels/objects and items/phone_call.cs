using UnityEngine;

public class phone_call : MonoBehaviour
{
    public CameraShake cam;
    public FadeController fade;
    public AudioSource source;
    public AudioClip phoneCall;

    private bool soundHasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void startNewSound()
    {
        if(soundHasPlayed == false)
        {
            source.Play();
            soundHasPlayed = true;
        }
    }

    private void Start()
    {
        fade.FadeOut();
        cam.Shake(0.2f, 0.1f); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("sound is stoped");
            source.loop = false;
            source.Stop();
        }
    }
   
    void Update()
    {
        if(level_storage.isLevelCleared == true)
        {
            startNewSound();
        }
    }
}
