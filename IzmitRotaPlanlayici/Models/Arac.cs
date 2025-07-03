public abstract class Arac
{
    public string AracId { get; set; }
    public abstract string Tip { get; }
}
public class Otobus : Arac
{
    public override string Tip => "Otobus";
}
public class Tramvay : Arac
{
    public override string Tip => "Tramvay";
}
