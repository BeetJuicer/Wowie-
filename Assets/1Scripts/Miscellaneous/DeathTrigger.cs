using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private float startTime;
    private int damageValue;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NormalDifficulty") == 1)
        {
            damageValue = 2;
        }
        else
        {
            damageValue = 5;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
  
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time > startTime + 0.6f)
            {
                startTime = Time.time;
                
                //collision.gameObject.GetComponent<PlayerHandler>().AddTime(-damageValue);
                collision.gameObject.GetComponent<Combat>().Knockback(new Vector2(1f,1f), 5f, -collision.gameObject.GetComponent<Movement>().FacingDirection);
                collision.gameObject.GetComponent<PlayerHandler>().DamageFeedback();
                
            }
        }
    }
}
