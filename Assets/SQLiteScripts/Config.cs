using SQLite4Unity3d;

public class Config
{
    [PrimaryKey]
    public int Setting1 { get; set; }

    public override string ToString()
    {
        return string.Format("[Config: Setting1={0}]", Setting1);
    }
}
