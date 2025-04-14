using UnityEngine;

public enum GameFlowState
{
    Ready,
    Field

}
public class GameFlowController : MonoBehaviour
{

    public GameFlowState gameState;

    private void Start()
    {
        gameState = GameFlowState.Ready;    
    }

    public void SetGameState( GameFlowState requestGameState)
    {
        gameState = requestGameState;
    }
}


