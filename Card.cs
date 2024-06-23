namespace CardGames.Card;
public enum Suit{
    Spades;
    Hearts;
    Diamonds;
    Clubs;
}

public enum Rank{
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
    private int value;
    private Suit suit;
    private Rank rank;
    private bool isAvailable;
    public Card(int c, Suit s, Rank r){
        value = c;
        suit = s;
        rank = r;
        isAvailable = true;
    }
    public int getValue(){
        return value;
    }
    public Suit getSuit(){
        return suit;
    }
    public Rank getRank(){
        return rank;
    }
    public bool getStatus(){
        return isAvailable;
    }
    public void setStatus(bool status){
        isAvailable = status;
    }
    public void setValue(int v){
        value = v;
    }
}