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
        Die
    }

    private SteveState _currentState = SteveState.Normal;

    private void Update(){
        UpdateState();
    }

    //stuff that happens once when beginning new state
    private void StartState(SteveState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){
            case SteveState.Normal: 
                

                break;

            case SteveState.Scared: 
                
                break;

            case SteveState.Die: 
                timer = 3;
                break;
        }

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case SteveState.Normal: 

                transform.position = new Vector2(xPos,randomSpawn);
                xPos += speed * Time.deltaTime;

                if (xPos >= 15){
                    StartState(SteveState.Die);
                }

                break;

            case SteveState.Scared: 
               
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

            case SteveState.Die: 
                
                break;
        }
    }

    public Sprite steveSprite;
    public Sprite johnSprite;
    public SpriteRenderer colorCheck;
    public Color color = Color.red;

    public float xPos = -9;
    public float speed;
    public int randomSprite;
    public float randomSpawn;
    public int randomScared;
    public float timer = 0;

    void Start(){
        randomSprite = UnityEngine.Random.Range(1,3); //1~2
        randomSpawn = UnityEngine.Random.Range(-4,4);
        randomScared = UnityEngine.Random.Range(1,9); //1~8

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

    void OnCollisionEnter2D(Collision2D collObj){
        if (collObj.gameObject.CompareTag("Car")){
            StartState(SteveState.Die);
        }
    }

}


