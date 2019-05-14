﻿using UnityEngine;
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
    }

    public PlayerData(Type type, int life, int maxEnergy) : this(type) {
        this.life = life;
        this.maxEnergy = maxEnergy;
        this.CurrentEnergy = maxEnergy;
    }

    #region Serializables
    [SerializeField] private Type currentType;
    [SerializeField] private int life = 20;
    [SerializeField] private int maxEnergy = 0;
    [SerializeField] private int energy = 0;
    [SerializeField] private int shield;
    [SerializeField] public enum Type { one, two }
    #endregion

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
        get { return life; }
        set {
            life = value;
            if (OnLifeChange != null)
                OnLifeChange();
            if (life <= 0) {
                if (Lost != null)
                    Lost(this);
            }
        }
    }

    public int MaxEnergy {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }

    public int CurrentEnergy {
        get { return energy; }
        set {
            energy = value;
            if (OnEnergyChange != null)
                OnEnergyChange();
        }
    }

    public DeckData Hand { get; internal set; }
    public DeckData Deck { get; internal set; }

    #endregion

    #region events
    public event PlayerEvent.PlayerLost Lost;
    public event PlayerEvent.DataChange OnEnergyChange;
    public event PlayerEvent.DataChange OnLifeChange;
    #endregion



}

