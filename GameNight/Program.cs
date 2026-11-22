/*
 * Name:        Jonathan Mann
 * Course:      CSCI 1250, Section 002
 * Assignment:  Lab 08, Game Night
 * Date:        November 22, 2026
 * Description: Keeps a game night leaderboard in three text files. Shows the
 *              season so far, records tonight's points, saves the new totals,
 *              and adds tonight's result to a history that grows every week.
 */

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

string[] points = File.ReadAllLines(POINTS_FILE);
string[] players = File.ReadAllLines(PLAYERS_FILE);

int leader = LeaderIndex(points);
int bestTonight = -1;
string winnerTonight = "";

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
//for loop to run through all of the names and print them out
for(int i = 0; i < points.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}
System.Console.WriteLine($"Leader: {players[leader]} with {points[leader]}");
System.Console.WriteLine("Season total: " + TotalPoints(points));

// for loop because I need the index to update the correct player's season total
for(int i = 0; i < players.Length; i++)
{
    int tonight = -1;

// While: keep asking until the score is between 0 and 20
    while(tonight < 0 || tonight > MAX_POINTS)
    {
        System.Console.Write($"Points tonight for {players[i]}: ");
        tonight = Convert.ToInt32(Console.ReadLine());
        if (tonight < 0 || tonight > MAX_POINTS)
        {
            System.Console.WriteLine("Points must be from 0 to 20.");
        }
    }
    int season = Convert.ToInt32(points[i]) + tonight;
    points[i] = season.ToString();
    if(tonight > bestTonight)
    {
        bestTonight = tonight;
        winnerTonight = players[i];
    }
}

System.Console.WriteLine($"Tonight's winner: {winnerTonight} with {bestTonight}");
File.WriteAllLines(POINTS_FILE, points);
System.Console.WriteLine("Saved points.txt.");

static int LeaderIndex(string[] points)
{
    int leaderPoints = 0;
    int leaderIndex = 0;
    int currentPoints = Convert.ToInt32(points[0]);

// I need to control the index for this loop so I chose a for loop
    for (int i = 0; i < points.Length; i++)
    {
        currentPoints = Convert.ToInt32(points[i]);

        if (currentPoints > leaderPoints)
        {
            leaderPoints = currentPoints;
            leaderIndex = i;
        }
    }

    return leaderIndex;
}

static int TotalPoints(string[] points)
{
    int total = 0;
    // used a foreach loop because I need it to run for each number in the points array
    foreach(string point in points)
    {
        total += Convert.ToInt32(point);
    }
    return total;
}

// static string HistoryEntry(int night, string winner, int points);