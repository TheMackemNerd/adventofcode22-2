
try
{
    Games games = new Games();
    StreamReader sr = new StreamReader("C:\\adventofcode\\day2\\data.txt");    

    //Read the file one line at a time
    while (!sr.EndOfStream)
    {
        //Add each line as a new game in the collection of games
        string line = sr.ReadLine();
        games.items.Add(new Game(line));
    }

    //Output the totals of the games
    Console.WriteLine("Games: " + games.items.Count());
    Console.WriteLine("Part 1 Score: " + games.Total(1));
    Console.WriteLine("Part 2 Score: " + games.Total(2));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);  
}


public class Games
{
    public List<Game> items { get; set; }   

    public Games()
    {
        //Instantiate a new list of games
        items = new List<Game>();
    }

    public int Total(int puzzlePart)
    {
        // There are two potential totals, depending on which part of the puzzle we are doing 

        int total = 0;
        foreach (Game item in items)
        {
            total = total + item.Result(puzzlePart);
        }
        return total;
    }
}


public class Game
{
    enum Outcome
    {
        WIN = 6,
        DRAW = 3,
        LOSE = 0
    }

    enum Hand
    {
        ROCK = 1,
        PAPER = 2,
        SCISSORS = 3
    }

    private Hand opponentHand { get; set; } // Used in both parts, this is the hand (Rock, Paper, Scissors) the opponent plays
    private Hand myHand { get; set; } //Used in part 1, this is corresponding hand that I play
    private Outcome intendedOutcome { get; set; } //Used in part 2, this is whether I must Win, Draw or Lose the game   
    
    public Game(string line)
    {
        //A game line is three characters long, with the first part and second part separated by a space
        string[] split = line.Split(new char[] { ' ' });
        string firstPart = split[0];
        string secondPart = split[1];

        //The first part is always the opponent's hand
        switch (firstPart)
        {
            case "A":
                opponentHand = Hand.ROCK;
                break;
            case "B":
                opponentHand = Hand.PAPER;
                break;
            case "C":
                opponentHand = Hand.SCISSORS;
                break;
            default:
                throw new Exception("Unrecognised value: " + firstPart);
        }

        //The second part is either the intended outcome, or my hand, depending on which part of the puzzle we are doing
        switch (secondPart)
        {
            case "X":
                intendedOutcome = Outcome.LOSE; //For Part 2
                myHand = Hand.ROCK; //For Part 1
                break;
            case "Y":
                intendedOutcome = Outcome.DRAW; //For Part 2
                myHand = Hand.PAPER; //For Part 1
                break;
            case "Z":
                intendedOutcome = Outcome.WIN; //For Part 2
                myHand = Hand.SCISSORS; //For Part 1
                break;
            default:
                throw new Exception("Unrecognised value: " + secondPart);
        }

    }

    public int Result(int puzzlePart)
    {
        // There are two potential results, depending on which part of the puzzle we are doing 

        if (puzzlePart == 1)
        {
            return ResultPart1();
        }
        else
        {
            return ResultPart2();
        }
    }

    private int ResultPart1()
    {
        /*
        *  A straight game of ROCK, PAPER, SCISSORS. The result is a combination of:
        *
        ** The outcome of the game (WIN=6, DRAW=3, LOSE=0)
        ** The value of the hand that I play (ROCK=1, PAPER=2, SCISSORS=3)
        *
        */

        try
        {
            Outcome outcome;

            switch (myHand)
            {
                case Hand.ROCK: //I Play ROCK

                    switch (opponentHand)
                    {
                        case Hand.ROCK: // Opponent Plays ROCK
                            outcome = Outcome.DRAW;
                            break;
                        case Hand.PAPER: // Opponent Plays PAPER
                            outcome = Outcome.LOSE;
                            break;
                        default: // Opponent Plays SCISSORS
                            outcome = Outcome.WIN;
                            break;
                    }
                    break;

                case Hand.PAPER: //I Play PAPER

                    switch (opponentHand)
                    {
                        case Hand.ROCK: // Opponent Plays ROCK
                            outcome = Outcome.WIN;
                            break;
                        case Hand.PAPER: // Opponent Plays PAPER
                            outcome = Outcome.DRAW;
                            break;
                        default: // Opponent Plays SCISSORS
                            outcome = Outcome.LOSE;
                            break;
                    }
                    break;

                default: // I Play SCISSORS

                    switch (opponentHand)
                    {
                        case Hand.ROCK: // Opponent Plays ROCK
                            outcome = Outcome.LOSE;
                            break;
                        case Hand.PAPER: // Opponent Plays PAPER
                            outcome = Outcome.WIN;
                            break;
                        default: // Opponent Plays SCISSORS
                            outcome = Outcome.DRAW;
                            break;
                    }
                    break;

            }

            return (int)myHand + (int)outcome;


        } catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private int ResultPart2()
    {
        /*
        * The Intended outcome determines what my hand should be based on the opponents hand.
        * i.e. if the intended outcome is 'LOSE', and the opponents hand is ROCK, my hand must be SCISSORS
        * The score is a combination of:
        * 
        ** The intended outcome of the game (WIN=6, DRAW=3, LOSE=0)
        ** The value of the hand that I must play (ROCK=1, PAPER=2, SCISSORS=3)
        *
        */

        try
        {
           Hand myHand;

           switch (intendedOutcome)
            {
                case Outcome.LOSE: //I must Lose

                    switch (opponentHand)
                    {
                        case Hand.ROCK: //Opponent plays Rock
                            myHand = Hand.SCISSORS;
                            break;
                        case Hand.PAPER: //Opponent plays Paper
                            myHand = Hand.ROCK;
                            break;
                        default: //Opponent plays Scissors
                            myHand = Hand.PAPER;
                            break;
                    }
                    break;

                case Outcome.DRAW: //I must Draw

                    switch (opponentHand)
                    {
                        case Hand.ROCK: //Opponent plays Rock
                            myHand = Hand.ROCK;
                            break;
                        case Hand.PAPER: //Opponent plays Paper
                            myHand = Hand.PAPER;
                            break;
                        default: //Opponent plays Scissors
                            myHand = Hand.SCISSORS;
                            break;
                    }
                    break;

                default: //I must Win

                    switch (opponentHand)
                    {
                        case Hand.ROCK: //Opponent plays Rock
                            myHand = Hand.PAPER;
                            break;
                        case Hand.PAPER: //Opponent plays Rock
                            myHand = Hand.SCISSORS;
                            break;
                        default: //Opponent plays Scissors
                            myHand = Hand.ROCK;
                            break;
                    }
                    break;
            }

            return (int)intendedOutcome + (int)myHand;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
