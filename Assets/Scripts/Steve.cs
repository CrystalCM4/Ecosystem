using System.Collections;
using System.Collections.Generic;
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
    public void StartState(SteveState newState){

        //run "end state" of current state to start new state
        EndState(_currentState);

        switch(newState){
            case SteveState.Normal: 
                

                break;

            case SteveState.Scared: 
                
                break;

            case SteveState.Die: 
                
                break;
        }

        _currentState = newState;
    }

    private void UpdateState(){
        switch(_currentState){

            case SteveState.Normal: 
   
                break;

            case SteveState.Scared: 
               
                break;

            case SteveState.Die: 

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

    public int randomSprite = Random.Range(1,3); //1~2
    public float randomSpawn = Random.Range(-4,4);
    public int randomScared = Random.Range(1,9); //1~8
    public float timer = 0;

}


