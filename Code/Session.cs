namespace Celeste.Mod.TrollGrabBag;

public sealed class Session : EverestModuleSession
{
    public bool Roses_Playing { get; set; } = false;
    public string Roses_LastColorGrade { get; set; } = null;

    public void Roses_Cancel(Level level)
    {
        Roses_Playing = false;
        if (!string.IsNullOrEmpty(Roses_LastColorGrade))
        {
            level.SnapColorGrade(Roses_LastColorGrade);
            Roses_LastColorGrade = null;
        }
    }
}
