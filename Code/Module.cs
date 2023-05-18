global using System;
global using Monocle;
global using Microsoft.Xna.Framework;
using Celeste.Mod.TrollGrabBag.Effects;

namespace Celeste.Mod.TrollGrabBag;

public sealed class Module : EverestModule
{
    public static Module Instance { get; private set; }

    public override Type SettingsType => typeof(Settings);
    public static Settings Settings => (Settings)Instance._Settings;

    public override Type SessionType => typeof(Session);
    public static Session Session => (Session)Instance._Session;

    public Module()
    {
        Instance = this;
    }

    public override void Load()
    {
        Hooks_Roses.Load();
    }

    public override void Unload()
    {
        Hooks_Roses.Unload();
    }
}