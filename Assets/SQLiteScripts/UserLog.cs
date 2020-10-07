using SQLite4Unity3d;
using System;

public class UserLog
{
    [PrimaryKey]
    public DateTime Date { get; set; }
    public float Weight { get; set; }
    public float Waist { get; set; }
    public string Picture { get; set; }



    public override string ToString()
    {
        return string.Format("[UserLog: Date={0}, Weight={1}, Waist={2}, Picture={3}]", Date, Weight, Waist, Picture);
    }
}
