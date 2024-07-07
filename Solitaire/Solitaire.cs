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
                Card? c = deck.DealCard();
                if(c == null){
                    break;
                }
                if(j == i){
                    c.SetFaceUp(true);
                }
                piles[i].Push(c);
            }
        }
        for(int i = 0; i < 4; i++){
            foundations[i] = new Stack<Card>();
        }
        for(int i = 0; i < 24; i++){
            Card? c = deck.DealCard();
            if(c == null){
                break;
            }
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
                c.SetFaceUp(false);
                stock.Push(c);
            }
        }
        else{
            Card c = stock.Pop();
            c.SetFaceUp(true);
            waste.Push(c);
        }
    }

    public static bool IsCompatible(Card a, Card b){
        if(a.GetValue() == b.GetValue() + 1 || (a.GetValue() == 14 && b.GetValue() == 2)){
            if(a.GetSuit() == Suit.Diamonds || a.GetSuit() == Suit.Hearts){
                if(b.GetSuit() == Suit.Clubs || b.GetSuit() == Suit.Spades){
                    return true;
                }
            }
            else if(a.GetSuit() == Suit.Clubs || a.GetSuit() == Suit.Spades){
                if(b.GetSuit() == Suit.Diamonds || b.GetSuit() == Suit.Hearts){
                    return true;
                }
            }
        }
        return false;
    }

    public void Move(int from, int to){
        if(from == -1){
            if(waste.Count == 0){
                Console.WriteLine("Waste pile is empty");
                return;
            }
            Card c = waste.Pop();
            if(to < 7){
                if(piles[to].Count == 0){
                    if(c.GetRank() == Rank.King){
                        piles[to].Push(c);
                    }
                    else{
                        Console.WriteLine("Invalid move");
                        waste.Push(c);
                        return;
                    }
                }
                else{
                    if(IsCompatible(c, piles[to].First())){
                        piles[to].Push(c);
                    }
                    else{
                        Console.WriteLine("Invalid move");
                        waste.Push(c);
                        return;
                    }
                }
            }
            else{
                if(foundations[to-7].Count == 0){
                    if(c.GetRank() == Rank.Ace){
                        foundations[to-7].Push(c);
                    }
                    else{
                        Console.WriteLine("Invalid move");
                        waste.Push(c);
                        return;
                    }
                }
                else{
                    if(IsCompatible(c, foundations[to-7].First())){
                        foundations[to-7].Push(c);
                    }
                    else{
                        Console.WriteLine("Invalid move");
                        waste.Push(c);
                        return;
                    }
                }
            }
        }
        else{
            if(from < 7){
                if(piles[from].Count == 0){
                    Console.WriteLine("Pile is empty");
                    return;
                }
                Card c = piles[from].Pop();
                if(to < 7){
                    if(piles[to].Count == 0){
                        if(c.GetRank() == Rank.King){
                            piles[to].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            piles[from].Push(c);
                            return;
                        }
                    }
                    else{
                        if(IsCompatible(c, piles[to].First())){
                            piles[to].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            piles[from].Push(c);
                            return;
                        }
                    }
                }
                else{
                    if(foundations[to-7].Count == 0){
                        if(c.GetRank() == Rank.Ace){
                            foundations[to-7].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            piles[from].Push(c);
                            return;
                        }
                    }
                    else{
                        if(IsCompatible(c, foundations[to-7].First())){
                            foundations[to-7].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            piles[from].Push(c);
                            return;
                        }
                    }
                }
            }
            else{
                if(foundations[from-7].Count == 0){
                    Console.WriteLine("Foundation is empty");
                    return;
                }
                Card c = foundations[from-7].Pop();
                if(to < 7){
                    if(piles[to].Count == 0){
                        if(c.GetRank() == Rank.King){
                            piles[to].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            foundations[from-7].Push(c);
                            return;
                        }
                    }
                    else{
                        if(IsCompatible(c, piles[to].First())){
                            piles[to].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            foundations[from-7].Push(c);
                            return;
                        }
                    }
                }
                else{
                    if(foundations[to-7].Count == 0){
                        if(c.GetRank() == Rank.Ace){
                            foundations[to-7].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            foundations[from-7].Push(c);
                            return;
                        }
                    }
                    else{
                        if(IsCompatible(c, foundations[to-7].First())){
                            foundations[to-7].Push(c);
                        }
                        else{
                            Console.WriteLine("Invalid move");
                            foundations[from-7].Push(c);
                            return;
                        }
                    }
                }
            }
        }
    }

    public static string Help(){
        StringBuilder sb = new StringBuilder();
        sb.Append("Commands: move, iterate, help, quit\n");
        sb.Append("move <from> <to> - Move a card from one pile to another\n");
        sb.Append("iterate - Iterate the stock pile, either move to the waste pile, or rest the stock pile\n");
        sb.Append("help - Display this help message\n");
        sb.Append("quit - Quit the game\n");
        sb.Append("Move codes:\n");
        sb.Append("Waste Pile: -1\t Pile 1: 0\t Pile 2: 1\t Pile 3: 2\t Pile 4: 3\t Pile 5: 4\t Pile 6: 5\t Pile 7: 6\n");
        sb.Append("Foundation 1: 7\t Foundation 2: 8\t Foundation 3: 9\t Foundation 4: 10\n");
        sb.Append("C: Clubs\t S: Spades\t H: Hearts\t D: Diamonds\n");
        return sb.ToString();
    }

    public void Play(){
        Console.Clear();
        Console.WriteLine(Help());
        while(true){
            Console.WriteLine(ToString());
            Console.WriteLine("Enter command: ");
            string? command = Console.ReadLine();
            if(command == null){
                break;
            }
            string[] commands = command.Split(" ");
            int from;
            int to;
            if(commands[0] == "quit" || commands[0].ToLower() == "q"){
                break;
            }
            else if(commands[0] == "move"){
                from = Int32.Parse(commands[1]);
                to = Int32.Parse(commands[2]);
                Move(from, to);
            }
            else if(commands[0] == "iterate"){
                IterateStock();
            }
            else if(commands[0] == "help" || commands[0] == "h"){
                Console.WriteLine(Help());
            }
            else{
                Console.WriteLine("Invalid command");
            }
        }
        
    }
}