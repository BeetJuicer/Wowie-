using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    //Gameobjects
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private RectTransform fader;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cat;

    //----Respawn Values---
    [SerializeField]
    private float respawnTime;

    [SerializeField] 
    private Transform[] respawnPoints;

    public int checkpointCount;

    private static GameManager instance;

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
   //     SetPlayerPosition();
        //Inward scene fade.
        fader.gameObject.SetActive(true);
        AudioManager.instance.Play("Open");
        AudioManager.instance.Play("Theme_2");
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }

    private void Update()
    {

    }

    public void Respawn()
    {
        PauseMenu.gamePaused = true;
        //Outward
        fader.gameObject.SetActive(true);
        AudioManager.instance.Play("Close");
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            Invoke("LoadScene", 0.3f);//LoadScene previously.
        });
    }

    private void SetPlayerPosition()
    {
        player.transform.position = respawnPoints[checkpointCount].position;
        cat.transform.position = respawnPoints[checkpointCount].position;
    }

    private void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        PauseMenu.gamePaused = false;
    }
}
