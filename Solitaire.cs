using namespace CardGames.Card;
using namespace CardGames.Deck;
using System.Collections.Generic;

namespace CardGames.Solitaire;
public class Solitaire{
    private Deck deck;
    private LinkedList<Card>[] piles;
    private LinkedList<Card>[] foundations;
    private LinkedList<Card> stock;
    private LinkedList<Card> waste;
    public Solitaire(){
        deck = new Deck();
        deck.shuffle();
        piles = new LinkedList<Card>[7];
        foundations = new LinkedList<Card>[4];
        stock = new LinkedList<Card>();
        waste = new LinkedList<Card>();
        for(int i = 0; i < 7; i++){
            piles[i] = new LinkedList<Card>();
            for(int j = 0; j < i+1; j++){
                Card c = deck.dealCard();
                if(j == i){
                    c.setFaceUp(true);
                }
                piles[i].AddLast(c);
            }
        }
        for(int i = 0; i < 4; i++){
            foundations[i] = new LinkedList<Card>();
        }
        for(int i = 0; i < 24; i++){
            Card c = deck.dealCard();
            stock.AddLast(c);
        }
    }
}