using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] Cat cat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(cat.transform.position.x - transform.position.x) > .2f)
        {
            cat.Tempt(transform);
            Debug.Log("Fish is calling for cat");
        }
    }
}
