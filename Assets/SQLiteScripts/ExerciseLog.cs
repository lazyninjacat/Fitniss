using SQLite4Unity3d;
using System;

public class ExerciseLog
{
    [PrimaryKey]
    public DateTime Timestamp { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseAmount { get; set; }
    public string ExerciseType { get; set; }
   

    public override string ToString()
    {
        return string.Format("[ExerciseLog: Timestamp={0}, ExerciseName={1}, ExerciseAmount={2}, ExerciseType={3}]", Timestamp, ExerciseName, ExerciseAmount, ExerciseType);
    }
}
