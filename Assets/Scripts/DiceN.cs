using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceN : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetFloat("number", Dice.GetInstance().randomNumber);
        gameObject.GetComponent<Animator>().SetBool("roll", Dice.GetInstance().roll);
    }

    public void FinishRolling()
    {
        Dice.GetInstance().NewRound();
    }

}
