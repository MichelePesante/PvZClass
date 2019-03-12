using System;
using UnityEngine;

public static class TurnManager
{
    #region Delegate
    public static Action<IPlayer> OnturnChange;
    #endregion

    private static IPlayer activePlayer;

    /// <summary>
    /// Funzione che imposta il player attivo con quello passato come parametro
    /// </summary>
    /// <param name="_player"></param>
    public static void SetActivePlayer(Player.Type _player)
    {
        IPlayer newActivePlayer = GameplaySceneManager.GetPlayer(_player);
        if (newActivePlayer != null)
        {
            activePlayer = newActivePlayer;
            if (OnturnChange != null)
                OnturnChange(activePlayer);
        }
        else
            Debug.LogError("Player Inesistente");
    }

    /// <summary>
    /// Funzione che ritorna il player attivo
    /// </summary>
    /// <returns></returns>
    public static IPlayer GetActivePlayer()
    {
        return activePlayer;
    }
}
