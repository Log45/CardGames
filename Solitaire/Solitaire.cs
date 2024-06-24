// This is the main file for the solitaire game
using CardGames.common;
using System.Collections;
using System.Text;
using System;

namespace CardGames.Solitaire;
public class Solitaire{
    private Deck deck;
    private Stack<Card>[] piles;
    private Stack<Card>[] foundations;
    private Stack<Card> stock;
    private Stack<Card> waste;
    public Solitaire(){
        deck = new Deck();
        deck.shuffle();
        piles = new Stack<Card>[7];
        foundations = new Stack<Card>[4];
        stock = new Stack<Card>();
        waste = new Stack<Card>();
        for(int i = 0; i < 7; i++){
            piles[i] = new Stack<Card>();
            for(int j = 0; j < i+1; j++){
                Card c = deck.dealCard();
                if(j == i){
                    c.setFaceUp(true);
                }
                piles[i].Push(c);
            }
        }
        for(int i = 0; i < 4; i++){
            foundations[i] = new Stack<Card>();
        }
        for(int i = 0; i < 24; i++){
            Card c = deck.dealCard();
            stock.Push(c);
        }
    }
    public override string ToString(){
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < 7; i++){
            sb.Append("Pile " + i + ": ");
            foreach(Card c in piles[i]){
                sb.Append(c.toString() + ", ");
            }
            sb.Append("\n");
        }
        for(int i = 0; i < 4; i++){
            sb.Append("Foundation " + i + ": ");
            foreach(Card c in foundations[i]){
                sb.Append(c.toString() + ", ");
            }
            sb.Append("\n");
        }
        sb.Append("Stock: ");
        foreach(Card c in stock){
            sb.Append(c.toString() + ", ");
        }
        sb.Append("\n");
        sb.Append("Waste: ");
        foreach(Card c in waste){
            sb.Append(c.toString() + ", ");
        }
        sb.Append("\n");
        return sb.ToString();
    }
    public void Play(){
        Card c;
        while(true){
            if(stock.Count == 0){
                if(waste.Count == 0){
                    break;
                }
                while(waste.Count > 0){
                    c = waste.Pop();
                    c.setFaceUp(false);
                    stock.Push(c);
                }
            }
            c = stock.Pop();
            c.setFaceUp(true);
            waste.Push(c); 
        }
    }
}