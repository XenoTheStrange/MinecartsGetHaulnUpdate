﻿using Mlie;
using UnityEngine;
using Verse;

namespace Minecart;

[StaticConstructorOnStartup]
internal class MinecartMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static MinecartMod instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public MinecartMod(ModContentPack content) : base(content)
    {
        instance = this;
        Settings = GetSettings<MinecartSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal MinecartSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Minecarts";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Gap();
        listing_Standard.Label("MGHU.DropAllRange".Translate(Settings.DropAllRange), -1,
            "MGHU.DropAllRangeTT".Translate());
        listing_Standard.IntAdjuster(ref Settings.DropAllRange, 1, 1);
        if (Settings.DropAllRange > 20)
        {
            Settings.DropAllRange = 20;
        }

        listing_Standard.Gap();

        listing_Standard.Label("MGHU.FreeSpaceRange".Translate(Settings.FreeSpaceRange), -1,
            "MGHU.FreeSpaceRangeTT".Translate());
        listing_Standard.IntAdjuster(ref Settings.FreeSpaceRange, 1, 1);
        if (Settings.FreeSpaceRange > 20)
        {
            Settings.FreeSpaceRange = 20;
        }

        listing_Standard.Gap();

        listing_Standard.Label("MGHU.StorageRange".Translate(Settings.StorageRange), -1,
            "MGHU.StorageRangeTT".Translate());
        listing_Standard.IntAdjuster(ref Settings.StorageRange, 1, 1);
        if (Settings.StorageRange > 20)
        {
            Settings.StorageRange = 20;
        }

        listing_Standard.Gap();
        var resetPlace = listing_Standard.GetRect(25f);
        if (Widgets.ButtonText(resetPlace.RightHalf().RightHalf().RightHalf(), "MGHU.Reset".Translate()))
        {
            Settings.Reset();
        }

        listing_Standard.Gap();
        listing_Standard.CheckboxLabeled("MGHU.VerboseLogging".Translate(), ref Settings.VerboseLogging);

        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("MGHU.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}