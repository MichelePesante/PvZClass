using System;
using UnityEngine;

public static class TurnManager
{
    #region Delegate
    // TODO: non endrebbe usata una view ma un data quì
    public static Action<PlayerView> OnTurnChange;
    #endregion

    // TODO: non endrebbe usata una view ma un data quì
    private static PlayerView activePlayer;

    /// <summary>
    /// Funzione che imposta il player attivo con quello passato come parametro
    /// </summary>
    /// <param name="_player"></param>
    public static void SetActivePlayer(PlayerData.Type _player)
    {
        // TODO: non endrebbe usata una view ma un data quì
        PlayerView newActivePlayer = GameplaySceneManager.GetPlayer(_player);
        if (newActivePlayer != null)
        {
            activePlayer = newActivePlayer;
            if (OnTurnChange != null)
                OnTurnChange(activePlayer);
        }
        else
            Debug.LogError("Player Inesistente");
    }

    /// <summary>
    /// Funzione che ritorna il player attivo
    /// </summary>
    /// <returns></returns>
    public static PlayerView GetActivePlayer() // TODO: non endrebbe usata una view ma un data quì
    {
        return activePlayer;
    }
}
