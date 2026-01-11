using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip menuSelectClip;
    public AudioClip menuSwitchClip;
    AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    public void StartButton()
    {
        SceneManager.LoadScene(1);
        _source.PlayOneShot(menuSelectClip);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void buttonHover()
    {
        _source.PlayOneShot(menuSwitchClip);
    }
}
