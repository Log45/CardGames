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
    private Dictionary<int, String> moveCodes = new Dictionary<int, String>();
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
        moveCodes.Add(-1, "Waste Pile");
        moveCodes.Add(0, "Pile 1");
        moveCodes.Add(1, "Pile 2");
        moveCodes.Add(2, "Pile 3");
        moveCodes.Add(3, "Pile 4");
        moveCodes.Add(4, "Pile 5");
        moveCodes.Add(5, "Pile 6");
        moveCodes.Add(6, "Pile 7");
        moveCodes.Add(7, "Foundation 1");
        moveCodes.Add(8, "Foundation 2");
        moveCodes.Add(9, "Foundation 3");
        moveCodes.Add(10, "Foundation 4");
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

    public void IterateStock(){
        if(stock.Count == 0){
            while(waste.Count > 0){
                Card c = waste.Pop();
                c.setFaceUp(false);
                stock.Push(c);
            }
        }
        else{
            Card c = stock.Pop();
            c.setFaceUp(true);
            waste.Push(c);
        }
    }

    public static bool IsCompatible(Card a, Card b){
        if(a.getRank() == b.getRank() + 1){
            if(a.getSuit() == Suit.Diamonds || a.getSuit() == Suit.Hearts){
                if(b.getSuit() == Suit.Clubs || b.getSuit() == Suit.Spades){
                    return true;
                }
            }
            else if(a.getSuit() == Suit.Clubs || a.getSuit() == Suit.Spades){
                if(b.getSuit() == Suit.Diamonds || b.getSuit() == Suit.Hearts){
                    return true;
                }
            }
        }
        return false;
    }

    public void Move(int from, int to){
        if(from == -1){
            if(waste.Count == 0){
                return;
            }
            Card c = waste.Pop();
            if(to < 7){
                if(piles[to].Count == 0){
                    if(c.getRank() == Rank.King){
                        piles[to].Push(c);
                    }
                }
                else{
                    if(IsCompatible(c, piles[to].First())){
                        piles[to].Push(c);
                    }
                }
            }
            else{
                if(foundations[to-7].Count == 0){
                    if(c.getRank() == Rank.Ace){
                        foundations[to-7].Push(c);
                    }
                }
                else{
                    if(IsCompatible(c, foundations[to-7].First())){
                        foundations[to-7].Push(c);
                    }
                }
            }
        }
        else{
            if(from < 7){
                if(piles[from].Count == 0){
                    return;
                }
                Card c = piles[from].Pop();
                if(to < 7){
                    if(piles[to].Count == 0){
                        if(c.getRank() == Rank.King){
                            piles[to].Push(c);
                        }
                        else{
                            piles[from].Push(c);
                        }
                    }
                    else{
                        if(IsCompatible(c, piles[to].First())){
                            piles[to].Push(c);
                        }
                        else{
                            piles[from].Push(c);
                        }
                    }
                }
                else{
                    if(foundations[to-7].Count == 0){
                        if(c.getRank() == Rank.Ace){
                            foundations[to-7].Push(c);
                        }
                        else{
                            piles[from].Push(c);
                        }
                    }
                    else{
                        if(IsCompatible(c, foundations[to-7].First())){
                            foundations[to-7].Push(c);
                        }
                        else{
                            piles[from].Push(c);
                        }
                    }
                }
            }
            else{
                if(foundations[from-7].Count == 0){
                    return;
                }
                Card c = foundations[from-7].Pop();
                if(to < 7){
                    if(piles[to].Count == 0){
                        if(c.getRank() == Rank.King){
                            piles[to].Push(c);
                        }
                        else{
                            foundations[from-7].Push(c);
                        }
                    }
                    else{
                        if(IsCompatible(c, piles[to].First())){
                            piles[to].Push(c);
    }}}
                else{
                    if(foundations[to-7].Count == 0){
                        if(c.getRank() == Rank.Ace){
                            foundations[to-7].Push(c);
                        }
                        else{
                            foundations[from-7].Push(c);
                        }
                    }
                    else{
                        if(IsCompatible(c, foundations[to-7].First())){
                            foundations[to-7].Push(c);
                        }
                        else{
                            foundations[from-7].Push(c);
                        }
                    }
                }
            }
        }
    }

    public void Play(){
        Card c;
    }
}