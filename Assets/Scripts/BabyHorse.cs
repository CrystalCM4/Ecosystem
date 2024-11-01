using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyHorse : MonoBehaviour
{
     public enum HorseState{
        Normal,
        Die
    }

    public HorseState _currentState = HorseState.Normal;

    private void Update(){
        UpdateState();
    }

    //stuff that happens once when beginning new state
    private void StartState(HorseState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        if (_currentState == HorseState.Die){
            return;
        }

        switch(newState){
            case HorseState.Normal: 

                speedX = Random.Range(-5, 5);
                speedY = Random.Range(-5, 5);
                randomTime = Random.Range(1,3);

                StartCoroutine(Countdown(randomTime));

                break;

            case HorseState.Die: 
                //die.time = 0.5f;
                die.Play();
                timer = 2;   
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

            case HorseState.Die: 
                speedX = 0;
                speedY = 0;
                colorCheck.color = color;
                transform.eulerAngles = new Vector3(0,0,90);
                Death(); 
                break;
        }
    }

    public void EndState(HorseState oldState){
        switch(oldState){
            case HorseState.Normal: 

                break;

            case HorseState.Die: 
                
                break;

        }
    }
    
    public float xPos;
    public float yPos;
    public float speedX;
    public float speedY;
    public int randomTime;
    public float timer;
    public SpriteRenderer colorCheck;
    public Color color = Color.red;

    public AudioSource die;

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
            StartState(HorseState.Die);
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

    private IEnumerator JumpCount (int seconds) {
        int counter = seconds;
            while (counter > 0) {
                yield return new WaitForSeconds(1);
                counter--;
        }
        StartState(HorseState.Normal); 
    }

    public void Death(){
        timer -= Time.deltaTime;
        if (timer <= 0){
            Destroy(gameObject);
        }
    }
}
