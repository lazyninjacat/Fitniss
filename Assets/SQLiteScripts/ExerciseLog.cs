using SQLite4Unity3d;
using System;
using System.Dynamic;

public class ExerciseLog
{
    [PrimaryKey, AutoIncrement]
    public string ID { get; set; }
    public DateTime Date { get; set; }
    public string CircuitName { get; set; }
    public string Duration { get; set; }
   

    public override string ToString()
    {
        return string.Format("[ExerciseLog: ID={0}, Date={1}, CircuitName={2}, Duration={3}]", ID, Date, CircuitName, Duration);
    }
}
