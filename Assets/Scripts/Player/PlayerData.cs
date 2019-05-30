using UnityEngine;
using System;
/* ===== MVC =====
 * - Utilizzare Snippet _MVC_Class per creare la struttura classi MVC
 * - Le classi View possono esistere o meno e possono essere più di una.
 * - Nella classe data devono esserci solo i dati e funzioni/eventi/delegate che agiscono solo sui dati
 * - La classe controller contiene solo le logiche di funzionamento. Non ha dati ma gli devono essere passati per poter espletare le sue logiche.
 * 
 */
[Serializable]
public class PlayerData {

    public PlayerData(Type type) {
        currentType = type;
        isAlive = true;
    }

    public PlayerData(Type type, int _life, int _maxEnergy) : this(type) {
        currentLife = _life;
        currentEnergy = currentMaxEnergy = _maxEnergy;
    }

    #region Serializables
    [SerializeField] private Type currentType;
    [SerializeField] private int life = 20;
    [SerializeField] private int maxEnergy = 0;
    [SerializeField] private int energy = 0;
    [SerializeField] private int shield;
    [SerializeField] public enum Type { one, two }
    #endregion

    private int currentLife = 20;
    private int currentMaxEnergy = 0;
    private int currentEnergy = 0;
    private bool isAlive;

    #region Properties
    public Type CurrentType {
        get { return currentType; }
        set { currentType = value; }
    }

    public int Shield {
        get { return shield; }
        set { shield = value; }
    }

    public int CurrentLife {
        get { return currentLife; }
        set {
            currentLife = Mathf.Clamp(value, 0, life);
            if (OnLifeChange != null)
                OnLifeChange();
            if (currentLife == 0) {
                IsAlive = false;
            }
        }
    }

    public int MaxEnergy {
        get { return currentMaxEnergy; }
        set { currentMaxEnergy = value; }
    }

    public int CurrentEnergy {
        get { return currentEnergy; }
        set {
            currentEnergy = value;
            if (OnEnergyChange != null)
                OnEnergyChange();
        }
    }

    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
            if (OnDeath != null)
                OnDeath();
        }
    }

    public DeckData Hand { get; internal set; }
    public DeckData Deck { get; internal set; }
    public CardData.Faction Faction { get; internal set; }

    #endregion

    #region events
    public event PlayerEvent.DataChange OnEnergyChange;
    public event PlayerEvent.DataChange OnLifeChange;
    public Action OnDeath;
    #endregion
}