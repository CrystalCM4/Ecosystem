using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public enum HorseState{
        Normal,
        Jump,
        Reproduce
    }

    public HorseState _currentState = HorseState.Normal;

    private void Update(){
        UpdateState();
    }

    //stuff that happens once when beginning new state
    private void StartState(HorseState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){
            case HorseState.Normal: 

                transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                
                speedX = Random.Range(-5, 5);
                speedY = Random.Range(-5, 5);
                randomTime = Random.Range(1,3);

                StartCoroutine(Countdown(randomTime));

                break;

            case HorseState.Jump: 
                if (!jump.isPlaying)
                {
                    //jump.time = 0.5f;
                    jump.Play();
                }
                jumpTimer = 1;

                break;

            case HorseState.Reproduce: 
                reproduce.Play();
                Instantiate(babyHorse, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                break;
        }

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case HorseState.Normal: 
                
                transform.position = new Vector2(xPos,yPos);
                xPos += speedX * Time.deltaTime;
                yPos += speedY * Time.deltaTime;
     
                if (Mathf.Abs(xPos) >= 10){      
                    Destroy(gameObject);    
                }

                
                if (Mathf.Abs(yPos) >= 10){
                    Destroy(gameObject);
                }

                break;

            case HorseState.Jump: 
               
                transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                jumpTimer -= Time.deltaTime;
                if (jumpTimer <= 0) {
                    StartState(HorseState.Normal);
                }

                break;

            case HorseState.Reproduce: 

                break;
        }
    }

    public void EndState(HorseState oldState){
        switch(oldState){
            case HorseState.Normal: 

                break;

            case HorseState.Jump: 
                
                break;

            case HorseState.Reproduce: 
                
                break;
        }
    }
    
    public float xPos;
    public float yPos;
    public float speedX;
    public float speedY;
    public int randomTime;
    public float jumpTimer;

    public GameObject babyHorse;

    public AudioSource jump;
    public AudioSource reproduce;
    

    void Start(){
        xPos = transform.position.x;
        yPos = transform.position.y;
        speedX = Random.Range(-5, 5);
        speedY = Random.Range(-5, 5);
        randomTime = Random.Range(1,3);

        StartState(HorseState.Normal);
    }

    void OnCollisionEnter2D(Collision2D collObj){
        if (collObj.gameObject.CompareTag("Car")){
            StartState(HorseState.Jump);
        }

        if (collObj.gameObject.CompareTag("Horse")){
            StartState(HorseState.Reproduce);
        }
    }

    private IEnumerator Countdown (int seconds) {
        int counter = seconds;
            while (counter > 0) {
                yield return new WaitForSeconds(1);
                counter--;
        }
        StartState(HorseState.Normal); 
    }
}
