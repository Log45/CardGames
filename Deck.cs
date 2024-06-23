using CardGame.Card;
using System;

namespace CardGame.Deck;
public class Deck{
    private Card[] cards;
    public Deck(){
        cards = new Card[52];
        int count = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 13; j++){
                cards[count] = new Card(j+2, (Suit)i, (Rank)j);
                count++;
            }
        }
    }
    public void shuffle(){
        for(int i = 0; i < 52; i++){
            int j = i + (int)(Math.random() * (52 - i));
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }
}