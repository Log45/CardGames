// This is the main file for the solitaire game
using CardGames.common;
using System.Collections;
using System.Text;
using System;
using System.Data;

namespace CardGames.Solitaire;
public class Solitaire{
    private Deck deck;
    private Stack<Card>[] piles;
    private Stack<Card>[] foundations;
    private Stack<Card> stock;
    private Stack<Card> waste;
    public Solitaire(){
        deck = new Deck();
        deck.Shuffle();
        piles = new Stack<Card>[7];
        foundations = new Stack<Card>[4];
        stock = new Stack<Card>();
        waste = new Stack<Card>();
        for(int i = 0; i < 7; i++){
            piles[i] = new Stack<Card>();
            for(int j = 0; j < i+1; j++){
                Card c = deck.DealCard();
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
            Card c = deck.DealCard();
            stock.Push(c);
        }
    }

    public override string ToString(){
        StringBuilder sb = new StringBuilder();
        StringBuilder[] rows = new StringBuilder[7];

        for(int i = 0; i<7; i++){
            rows[i] = new StringBuilder();
        }

        sb.Append("S  W     FC FS FD FH\n");

        if (stock.Count == 0) {
            sb.Append("__ ");
        }
        else {
            sb.Append(stock.First().ToString() + " "); 
        }

        if (waste.Count == 0) {
            sb.Append("__    ");
        }
        else {
            sb.Append(waste.First().ToString() + "    "); 
        }

        for (int i = 0; i < 4; i++)
        {
            if (foundations[i].Count == 0) {
                sb.Append("__");
            }
            else {
                sb.Append(foundations[i].First().ToString()); 
            }
            if (i == 3) {
                sb.Append('\n');
            }
            else {
                sb.Append(' '); 
            }
        }
        for(int i = 0; i < 7; i++)
        {
            int count = 0;
            foreach(Card c in piles[i])
            {
                rows[count].Append(c.ToString() + " ");
                count++;
            }
        }
        for(int i = 0; i < 7; i++)
        {
            sb.Append(rows[i].ToString() + "\n");
        }
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