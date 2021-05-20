using Scripts.DevelopingSupporting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public enum GameState : int
{
    PreStart, Start, Over
}


public class GlobalGameEvents : SupportingScripts.MonoBehSinglton<GlobalGameEvents>
{
    [SerializeField, Min(0)] private int pointsToOver;
    [SerializeField] private List<GameStateEvent> gameStateEvents = new List<GameStateEvent>();

    private GameState currentState;

    #region PROPERTIES

    private GameState CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            OnStateChanged?.Invoke(value);
        }
    }

    public int PointsToOver { get => pointsToOver; }

    #endregion


    #region Events

    public delegate void OnEnemySpawnStartHandler();
    public event OnEnemySpawnStartHandler OnEnemySpawnStart;

    private event Action<GameState> OnStateChanged;


    #endregion

    private void OnEnable()
    {
        OnStateChanged += GlobalGameEvents_OnStateChanged;
    }

    private void OnDestroy()
    {
        OnStateChanged -= GlobalGameEvents_OnStateChanged;
    }


    private void Start()
    {
        SwitchState(GameState.PreStart);
    }


    private void GlobalGameEvents_OnStateChanged(GameState state)
    {
        if(gameStateEvents.Count > 0)
        {
            for (int i = 0; i < gameStateEvents.Count; i++)
            {
                if(gameStateEvents[i].CurrentGameState == state)
                {
                    StartCoroutine(gameStateEvents[i].Invoke());
                }
            }
        }
    }

    #region SWITCHING GAME STATES

    public void SwitchState(int stateIndex)
    {
        CurrentState = (GameState)stateIndex;
    }

    public void SwitchState(GameState nextState)
    {
        CurrentState = nextState;
    }

    #endregion

    public void EnemySpawnStart()
    {
        Debug.Log("Оповестили о спавне");
        OnEnemySpawnStart?.Invoke();
    }

}

[Serializable]
public class GameStateEvent
{
    [SerializeField] private GameState currentGameState;
    [SerializeField] private UnityEvent OnStateStart;
    [SerializeField] private float timeToInvoke;

    #region PROPERITES

    public GameState CurrentGameState { get => currentGameState; }

    #endregion

    public IEnumerator Invoke()
    {
        yield return new WaitForSeconds(timeToInvoke);
        OnStateStart?.Invoke();
    }
}