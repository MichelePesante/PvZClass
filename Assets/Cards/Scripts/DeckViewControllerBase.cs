using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckViewControllerBase : MonoBehaviour {

    DeckData _data;
    public DeckData Data {
        get { return _data; }
        protected set {
            _data = value;
        }
    }

    protected virtual void OnDataChanged()
    {

    }

    protected virtual void LateSetup() {

    }

    #region API

    public void SetData(DeckData _data)
    {
        Data = _data;
        OnDataChanged();
    }

    public DeckViewControllerBase Setup(DeckData _deck) {
        if (_deck == null)
            return null;

        Data = _deck;

        LateSetup();

        return this;
    }

    public void Shuffle() {
        Data = DeckController.Shuffle(Data);
    }

    #endregion
    
}