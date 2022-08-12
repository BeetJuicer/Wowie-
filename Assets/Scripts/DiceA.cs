using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Animator>().SetInteger("ability", Dice.GetInstance().randomAbilityNumber);
        gameObject.GetComponent<Animator>().SetBool("roll", Dice.GetInstance().roll);
    }

    public void FinishRolling()
    {
        //sadasd
    }
}
