using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWarning : MonoBehaviour
{
    [SerializeField] float delay;
    bool deac = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (!deac)
            {
                StartCoroutine(Deactivate());
                deac = true;
            }
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(delay);
        gameObject.SetActive(false);
    }
}
