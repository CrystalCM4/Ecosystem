using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : MonoBehaviour
{
    public GameObject horse;
    public float randomTime;
    // Start is called before the first frame update
    void Start()
    {
        randomTime = Random.Range(5,10);
    }

    // Update is called once per frame
    void Update()
    {
        randomTime -= Time.deltaTime;   

        if (randomTime <= 0){
            Instantiate(horse, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            randomTime = Random.Range(5,10);
        }
    }
}
