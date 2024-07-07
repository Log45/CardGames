namespace CardGames.common;
public class Deck{
    private Card?[] cards;
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
    public void Shuffle(){
        var rand = new Random();
        for(int i = 0; i < 52; i++){
            int j = i + rand.Next(52 - i);
            (cards[j], cards[i]) = (cards[i], cards[j]);
        }
    }
    public Card? DealCard(){
        if(cards.Length == 0){
            return null;
        }
        Card? c = cards[0];
        if(c == null){
            return null;
        }
        c.SetAvailable(false);
        for(int i = 0; i < cards.Length - 1; i++){
            cards[i] = cards[i+1];
        }
        cards[cards.Length - 1] = null;
        return c;
    }
}