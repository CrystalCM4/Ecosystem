using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public enum CarState{
        Normal,
        Stop,
        Spin,
        Explode
    }

    public CarState _currentState = CarState.Normal;

    private void Update(){
        UpdateState();
    }

    //stuff that happens once when beginning new state
    private void StartState(CarState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){
            case CarState.Normal: 

                if (randomDir == 1){
                    transform.eulerAngles = new Vector3(0,0,-90);  
                }
                else {
                    transform.eulerAngles = new Vector3(0,0,90);  
                }

                break;

            case CarState.Stop: 
                honk.Play();
                timer = 2;
                break;

            case CarState.Spin:
                if (!slip.isPlaying)
                {
                    slip.Play();
                }
                break;

            case CarState.Explode: 
                Instantiate(explosion,new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case CarState.Normal: 
                speed = baseSpeed;
                int rando = 0;
                if (randomSpawn == 1){
                    rando = -3;
                }
                else if (randomSpawn == 2){
                    rando = 2;
                }
                else if (randomSpawn == 3){
                    rando = 7;
                }

                if (randomDir == 1){
                    if (yPos <= -15){
                        Destroy(gameObject);
                    }
                }
                else {
                    if (yPos >= 15){
                        Destroy(gameObject);
                    }
                }

                transform.position = new Vector2(rando,yPos);
                if (randomDir == 1){
                    yPos -= speed * Time.deltaTime;
                }
                else yPos += speed * Time.deltaTime;
                

                break;

            case CarState.Stop: 
                speed = 0;
                StartCoroutine(Countdown(2));
                break;

            case CarState.Spin: 
                speed = baseSpeed / 3;
                transform.eulerAngles = new Vector3(0,0,angle);
                angle += 90 + 1000 * Time.deltaTime;  
                StartCoroutine(Countdown(2));
                break;

            case CarState.Explode: 
                Destroy(gameObject);
                break;
        }
    }

    public void EndState(CarState oldState){
        switch(oldState){
            case CarState.Normal: 
                

                break;

            case CarState.Stop:     

                break;

            case CarState.Spin: 
                
                break;

            case CarState.Explode: 
                
                break;
        }
    }

    public Sprite carRed;
    public Sprite carBlue;
    public SpriteRenderer colorCheck;
    public GameObject explosion;

    public AudioSource slip;
    public AudioSource honk;

    public float yPos;
    public int randomDir;
    public float speed;
    public float baseSpeed;
    public int randomSprite;
    public int randomSpawn;
    public float timer = 0;
    public float angle = -90;

    void Start(){
        baseSpeed = speed;
        randomDir = Random.Range(1,3); //1~2
        randomSprite = Random.Range(1,3); //1~2
        randomSpawn = Random.Range(1,4); //1~3

        if (randomDir == 1){
            yPos = 10;
            transform.eulerAngles = new Vector3(0,0,-90);  
        }
        else {
            yPos = -10;
            transform.eulerAngles = new Vector3(0,0,90);  
        }

        /*
        if (randomSprite == 1){
            colorCheck.sprite = carRed;
        }
        else {
            colorCheck.sprite = carBlue;
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        */
        
    }

    private IEnumerator Countdown (int seconds) {
        int counter = seconds;
            while (counter > 0) {
                yield return new WaitForSeconds(1);
                counter--;
        }
        StartState(CarState.Normal);
    }

    void OnCollisionEnter2D(Collision2D collObj){
        if (collObj.gameObject.CompareTag("Steve")){
            //print(gameObject.name + ": fuck");
            StartState(CarState.Stop);
        }

        else if (collObj.gameObject.CompareTag("Car")){
            StartState(CarState.Explode);
        }

        else if (collObj.gameObject.CompareTag("Horse")){
            StartState(CarState.Spin);
        }
    }
}
