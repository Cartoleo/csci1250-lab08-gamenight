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

System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");
//for loop to run through all of the names and print them out
for(int i = 0; i < points.Length; i++)
{
    System.Console.WriteLine(players[i].PadRight(11) + points[i]);
}
System.Console.WriteLine($"Leader: {players[leader]} with {points[leader]}");
System.Console.WriteLine("Season total: " + TotalPoints(points));



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