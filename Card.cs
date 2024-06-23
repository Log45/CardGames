public enum Suit{
    Spades;
    Hearts;
    Diamonds;
    Clubs;
}

public enum Rank{
    One;
    Two;
    Three;
    Four;
    Five;
    Six;
    Seven;
    Eight;
    Nine;
    Ten;
    Jack;
    Queen;
    King;
    Ace;
}

public class Card{
    public int faceValue;
    public Suit suit;
    public Card(int c, Suit s){
        faceValue = c;
        suit = s;
    }
}