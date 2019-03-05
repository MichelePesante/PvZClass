
public static class CardController {

    public static CardData ResetOriginalLife(CardData _data) {
        _data.ResetOriginalLife();
        return _data;
    }

    public static CardData UpdateLife(CardData _data, int _value) {
        _data.Life = _value;
        return _data;
    }

}