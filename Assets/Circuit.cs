using SQLite4Unity3d;

public class Circuit
{
    [PrimaryKey]
    public int OrderID { get; set; }
    public string ExerciseName { get; set; }
    public string ExerciseAmount { get; set; }



    public override string ToString()
    {
        return string.Format("[Circuit: OrderID={0}, ExerciseName={1}, ExerciseAmount={2}]", OrderID, ExerciseName, ExerciseAmount);
    }
}
