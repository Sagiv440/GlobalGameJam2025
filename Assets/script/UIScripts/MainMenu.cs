using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private SoundEffectSO MainMusicTheme;

    private Animator[] allAnim;
    private void Start()
    {
        if(!MainMusicTheme.IsPlaying())
        {
            MainMusicTheme.Play();
        }
        allAnim = GetComponentsInChildren<Animator>(true);

        foreach (var anim in allAnim)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); //Need to be Updated//
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        
        // If running in the editor, it won't quit the editor
#if UNITY_EDITOR
        // If running in the Unity Editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a build, quit the application
        Application.Quit();
#endif
    }
}
