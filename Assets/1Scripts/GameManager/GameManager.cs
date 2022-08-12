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

    private GameObject player;

    //----Respawn Values---
    [SerializeField]
    private float respawnTime;

    [SerializeField] 
    private Vector2[] respawnPoints = new Vector2[3];

    public int checkPointCount;

    private static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    } 

    public void LoadData(GameData data)
    {
        this.checkPointCount = data.checkpointCount;
    }

    public void SaveData(GameData data)
    {
        data.checkpointCount = this.checkPointCount;
    }

    private void Start()
    {
        SetPlayerPosition();

        //Inward scene fade.
        fader.gameObject.SetActive(true);
        AudioManager.instance.Play("Open");
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
        player.transform.position = respawnPoints[checkPointCount];
    }

    private void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        PauseMenu.gamePaused = false;
    }
}
