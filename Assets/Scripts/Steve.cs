using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Steve : MonoBehaviour
{
    public enum SteveState{
        Normal,
        Scared,
        Knocked,
        Die
    }

    private SteveState _currentState = SteveState.Normal;

    private void Update(){
        UpdateState();
    }

    //stuff that happens once when beginning new state
    private void StartState(SteveState newState){

        if (_currentState == SteveState.Die){
            return;
        }

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){
            case SteveState.Normal: 
                transform.eulerAngles = new Vector3(0,0,0);

                break;

            case SteveState.Scared: 
                scared.Play();
                
                break;

            case SteveState.Knocked:
                knock.time = 0.5f;
                if (!knock.isPlaying)
                {
                    knock.Play();
                }
                knockTime = 2;

                break;

            case SteveState.Die: 
                die.time = 0.5f;
                die.Play();
                timer = 3;
                break;
        }

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case SteveState.Normal: 
                speed = baseSpeed;
                transform.position = new Vector2(xPos,randomSpawn);
                xPos += speed * Time.deltaTime;

                if (xPos >= 15){
                    Destroy(gameObject);
                }

                for (int i = 0; i < GameObject.FindGameObjectsWithTag("Explosion").Length; i++){
                    explosion = GameObject.FindGameObjectsWithTag("Explosion")[i];
                    if (Vector3.Distance(explosion.transform.position, gameObject.transform.position) <= 5){
                        
                        if (explosion.transform.position.y - gameObject.transform.position.y >= 0){
                            scaredDir = 2;
                        }
                        else scaredDir = 1;
                        
                        StartState(SteveState.Scared);
                    }
                }

                break;

            case SteveState.Scared: 
                speed = 0;
                yPos = transform.position.y;
                
                if (scaredDir == 1){
                    yPos += scaredSpeed * Time.deltaTime;
                }
                else yPos -= scaredSpeed * Time.deltaTime;;
                

                transform.position = new Vector2(transform.position.x,yPos);

                if (Mathf.Abs(yPos) >= 15)
                {
                    Destroy(gameObject);
                }

                break;

            case SteveState.Knocked:

                speed--;
                if (speed <= 0){
                    speed = 0;
                }
                transform.eulerAngles = new Vector3(0,0,90);
                Knocked();  
                break;

            case SteveState.Die: 

                speed--;
                if (speed <= 0){
                    speed = 0;
                }
                colorCheck.color = color;
                transform.eulerAngles = new Vector3(0,0,90);
                Death();    

                break;
        }
    }

    public void EndState(SteveState oldState){
        switch(oldState){
            case SteveState.Normal: 
                

                break;

            case SteveState.Scared: 

                break;

            case SteveState.Knocked:

            case SteveState.Die: 
                
                break;
        }
    }

    public Sprite steveSprite;
    public Sprite johnSprite;
    public SpriteRenderer colorCheck;
    public Color color = Color.red;

    public GameObject explosion;

    public AudioSource die;
    public AudioSource knock;
    public AudioSource scared;

    public float xPos = -9;
    public float yPos;
    public float speed;
    public float baseSpeed;
    public float scaredSpeed;
    public int randomSprite;
    public float randomSpawn;
    public int scaredDir;
    public float timer = 0;
    public float knockTime = 0;

    void Start(){
        baseSpeed = speed;
        randomSprite = UnityEngine.Random.Range(1,3); //1~2
        randomSpawn = UnityEngine.Random.Range(-4,4);
        //randomScared = UnityEngine.Random.Range(1,3); //1~2

        /*
        if (randomSprite == 1){
            colorCheck.sprite = steveSprite;
        }
        else {
            colorCheck.sprite = johnSprite;
            //transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        */
    }
    

    public void Death(){
        timer -= Time.deltaTime;
        if (timer <= 0){
            Destroy(gameObject);
        }
    }

    public void Knocked(){
        knockTime -= Time.deltaTime;
        if (knockTime <= 0){
            StartState(SteveState.Normal);
        }
    }

    void OnCollisionEnter2D(Collision2D collObj){
        if (collObj.gameObject.CompareTag("Car")){
            StartState(SteveState.Die);
        }

        else if (collObj.gameObject.CompareTag("Horse")){
            StartState(SteveState.Knocked);
        }
    }

}


