// Purpose: Contains the Card class which represents a single card in a deck of cards.
using System;
namespace CardGames.common;

public class Card{
    public Dictionary<Suit, string> suitToString = new Dictionary<Suit, string>();
    public Dictionary<Rank, string> rankToString = new Dictionary<Rank, string>();
    private int value;
    private Suit suit;
    private Rank rank;
    private bool isAvailable;
    private bool isFaceUp;
    public Card(int c, Suit s, Rank r){
        value = c;
        suit = s;
        rank = r;
        isAvailable = true;
        isFaceUp = false;
        suitToString.Add(Suit.Spades, "S");
        suitToString.Add(Suit.Hearts, "H");
        suitToString.Add(Suit.Diamonds, "D");
        suitToString.Add(Suit.Clubs, "C");
        for(int i=0; i<9; i++){
            rankToString.Add((Rank)i, (i+2).ToString());
        }
        rankToString.Add(Rank.Jack, "J");
        rankToString.Add(Rank.Queen, "Q");
        rankToString.Add(Rank.King, "K");
        rankToString.Add(Rank.Ace, "A");
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
    public bool getFaceUp(){
        return isFaceUp;
    }
    public void setAvailable(bool available){
        isAvailable = available;
    }
    public void setValue(int v){
        value = v;
    }
    public void setFaceUp(bool faceUp){
        isFaceUp = faceUp;
    }
    public override string ToString(){
        if(isFaceUp){
            return rankToString[rank] + suitToString[suit];
        }
        else{
            return "**";
        }
    }
}