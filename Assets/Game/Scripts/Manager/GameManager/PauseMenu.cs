using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField]
    private RectTransform fader;

    public static bool gamePaused;
    public static float pauseVolume = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputHandler.GetInstance().GetEscapePressed())
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame(); 
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        AudioManager.instance.Mute();
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        AudioManager.instance.Unmute();
        Time.timeScale = 1f;
        gamePaused = false;
    }
    
    public void GoToMainMenu()
    {
        //---> Outward Scene Fade
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            Invoke("LoadScene", 0.3f);
        });
        Time.timeScale = 1f;
        
        gamePaused = false;
    }
    private void LoadScene()
    {
        SceneManager.LoadSceneAsync("Menu_Scene");
        gamePaused = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
			Application.Quit();
        #endif
    }
}
