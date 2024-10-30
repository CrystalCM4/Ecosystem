using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject car;
    public float randomTime;
    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(0.1f,1);
    }

    // Update is called once per frame
    void Update()
    {
        randomTime -= Time.deltaTime;   

        if (randomTime <= 0){
            Instantiate(car, new Vector3(0, 10, 0), Quaternion.identity);
            randomTime = Random.Range(0.1f,5);
        }
    }
}
