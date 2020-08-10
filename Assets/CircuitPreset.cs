using SQLite4Unity3d;

public class CircuitPreset
{
    [PrimaryKey]
    public string PresetName { get; set; }
    public string Circuit { get; set; }



    public override string ToString()
    {
        return string.Format("[CircuitPreset: PresetName={0}, Circuit={1}]", PresetName, Circuit);
    }
}
