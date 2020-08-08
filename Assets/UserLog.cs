using SQLite4Unity3d;

public class UserLog
{
    [PrimaryKey]
    public int Timestamp { get; set; }
    public string Weight { get; set; }
    public string Waist { get; set; }
    public string Picture { get; set; }



    public override string ToString()
    {
        return string.Format("[UserLog: Timestamp={0}, Weight={1}, Waist={2}, Picture={3}]", Timestamp, Weight, Waist, Picture);
    }
}
