using SQLite4Unity3d;

public class ExerciseLog
{
    [PrimaryKey]
    public int Timestamp { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseAmount { get; set; }
   

    public override string ToString()
    {
        return string.Format("[ExerciseLog: Timestamp={0}, ExerciseName={1}, ExerciseAmount={2}]", Timestamp, ExerciseName, ExerciseAmount);
    }
}
