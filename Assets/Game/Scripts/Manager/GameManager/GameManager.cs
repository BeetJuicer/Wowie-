using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    private static GameManager instance;

    //---> Gameobjects
    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private RectTransform fader;

    [SerializeField]
    private GameObject player;

    //---> Respawn Values
    [SerializeField]
    private float respawnTime;

    [SerializeField] 
    private Transform[] respawnPoints;

    public int checkpointCount;

    public void LoadData(GameData data)
    {
        this.checkpointCount = data.checkpointCount;
    }

    public void SaveData(GameData data)
    {
        data.checkpointCount = this.checkpointCount;
    }

    private void Awake()
    {
        instance = this;

    }

    public static GameManager GetInstance()
    {
        return instance;
    } 

    private void Start()
    {
        //----> Inward scene fade.
        fader.gameObject.SetActive(true);
        AudioManager.instance.Play("Open");
        AudioManager.instance.Play("Theme_2");
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }

    public void Respawn()
    {
        PauseMenu.gamePaused = true;
        //---> Outward Scene Fade
        fader.gameObject.SetActive(true);
        AudioManager.instance.Play("Close");
        LeanTween.scale(fader, Vector3.zero, 0);
        Invoke("LoadScene", 0.3f);
        //uncomment below after playmode
        //LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
        //    Invoke("LoadScene", 0.3f);
        //});
    }

    private void SetPlayerPosition()
    {
        player.transform.position = respawnPoints[checkpointCount].position;
    }

    private void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        PauseMenu.gamePaused = false;
    }
}
