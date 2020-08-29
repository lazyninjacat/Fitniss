using SQLite4Unity3d;

public class Circuits
{
    [PrimaryKey]
    public int CircuitID { get; set; }
    public string CircuitName { get; set; }
    public string TableName { get; set; }



    public override string ToString()
    {
        return string.Format("[Circuits: CircuitID={0}, CircuitName={1}, TableName={2}]", CircuitID, CircuitName, TableName);
    }
}
