using Celeste.Mod.Entities;
using Celeste.Mod.TrollGrabBag.Utils;
using FMOD.Studio;
using System.Collections;

namespace Celeste.Mod.TrollGrabBag.Effects;

[CustomEntity("trolls_effect_grab_bag/roses")]
public sealed class Roses : Trigger
{
    private readonly bool once;

    public Roses(EntityData data, Vector2 offset)
        : base(data, offset)
    {
        once = data.Bool("once", true);
    }

    public override void OnEnter(Player player)
    {
        if (Module.Session.Roses_Playing)
            return;

        Scene.Add(new RosesHUD(Scene as Level));

        if (once)
            RemoveSelf();
    }
}

file sealed class RosesHUD : Entity
{
    private readonly MTexture rose, vignette;
    private float opacity;

    private readonly EventInstance jingle;

    public RosesHUD(Level level)
    {
        Tag = Tags.HUD;
        rose = GFX.Gui["troll_effects/rose"];
        vignette = GFX.Gui["troll_effects/rose_vignette"];

        jingle = Audio.Play("event:/SFX/Trollpack/Roses");

        Add(new Coroutine(Sequence(level)));
    }

    public override void Removed(Scene scene)
    {
        Audio.Stop(jingle);
        base.Removed(scene);
    }

    public override void Render()
    {
        Level level = Scene as Level;
        if (level.Paused)
            return;

        Vector2 center = new Vector2(320, 180) / 2;

        float rotation = level.TimeActive * 0.1f;

        const int count = 12;
        const float rad = 124f;
        for (int i = 0; i < count; i++)
        {
            float angle = rotation + MathHelper.TwoPi * i / count;
            Vector2 pos = center + Calc.AngleToVector(angle, rad);

            float spin = level.TimeActive * 0.2f * (i % 2 == 0 ? 1 : -1) + i;
            float scale = i % 2 == 0
                ? 0.3f
                : -0.25f;

            Color color = i % 2 == 0
                ? Calc.HexToColor("cca1b5")
                : Calc.HexToColor("ad343c");

            rose.DrawCentered(pos * 6, color * opacity, scale, spin);
        }

        vignette.Draw(Vector2.Zero, Vector2.Zero, Color.White * 0.5f * opacity);
    }

    private IEnumerator Sequence(Level level)
    {
        Module.Session.Roses_Playing = true;
        Module.Session.Roses_LastColorGrade = level.Session.ColorGrade;

        level.NextColorGrade("trolls_effect_grab_bag/roses", 2f);

        yield return Routines.Interpolate(0.5f, percent => opacity = percent);
        yield return 13f;

        Module.Session.Roses_Playing = false;
        level.NextColorGrade(Module.Session.Roses_LastColorGrade ?? string.Empty);
        Module.Session.Roses_LastColorGrade = null;

        yield return Routines.Interpolate(1f, percent => opacity = 1 - percent);
    }
}

internal static class Hooks_Roses
{
    public static void Load()
    {
        On.Celeste.Level.LoadLevel += Level_LoadLevel;
    }

    public static void Unload()
    {
        On.Celeste.Level.LoadLevel -= Level_LoadLevel;
    }

    private static void Level_LoadLevel(On.Celeste.Level.orig_LoadLevel orig, Level self, Player.IntroTypes playerIntro, bool isFromLoader)
    {
        orig(self, playerIntro, isFromLoader);
        Module.Session.Roses_Cancel(self);
    }
}
