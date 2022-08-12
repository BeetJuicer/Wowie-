using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private Transform shootPos;

    public GameObject warningPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Fire()
    {
        Instantiate(bulletPrefab, shootPos.position, gameObject.transform.rotation);
        Instantiate(smokePrefab, shootPos.position, gameObject.transform.rotation);
        gameObject.GetComponent<Animator>().SetBool("fire", false);
    }
}
