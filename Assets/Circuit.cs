using SQLite4Unity3d;

public class Circuit
{
    [PrimaryKey]
    public int Order { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseAmount { get; set; }



    public override string ToString()
    {
        return string.Format("[Circuit: Order={0}, ExerciseName={1}, ExerciseAmount={2}]", Order, ExerciseName, ExerciseAmount);
    }
}
