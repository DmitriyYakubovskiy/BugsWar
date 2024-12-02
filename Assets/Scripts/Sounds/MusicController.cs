using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private float volume =0.5f;
    private AudioSource musicSource;

    private float time = 0;

    private void Start()
    {
        musicSource=GetComponent<AudioSource>();
        musicSource.volume= volume; 
    }

    private void Update()
    {
        if (time <= 0) PlaySimpleMusic();
        time -= Time.deltaTime;
    }

    private void PlaySimpleMusic()
    {
        int index = Random.Range(0, musics.Length);
        musicSource.clip = musics[index];

        musicSource.Play();
        time = musics[index].length;
    }
}
