using UnityEngine;

public enum GameState
{
    Playing,
    IsTalking,
    BossLoading,
    BossFight,
    Shopping,
    Won,
    Lost,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameState CurrentState = GameState.Playing;

    public static bool IsFrozen =>
        CurrentState == GameState.IsTalking ||
        CurrentState == GameState.Shopping ||
        CurrentState == GameState.Won ||
        CurrentState == GameState.Lost ||
        CurrentState == GameState.Paused ||
        CurrentState == GameState.BossLoading;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
