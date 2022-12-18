﻿using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Utils;
using System.IO;
using BTD_Mod_Helper.Api.Helpers;
using UnityEngine;

namespace BTD_Mod_Helper.Extensions;

/// <summary>
/// Extension for dumping textures from UnityDisplayNodes
/// </summary>
public static partial class DumpNodeExt
{
    /// <summary>
    /// Only runs when tower placed.
    /// Dumps any textures inside of a display node into Ninja Kiwi directory
    /// </summary>
    public static void Dump(this UnityDisplayNode node)
    {
        if (!Directory.Exists($"{FileIOHelper.sandboxRoot}DumpedTextures/"))
        {
            Directory.CreateDirectory($"{FileIOHelper.sandboxRoot}DumpedTextures/");
        }
        foreach (var item in node.genericRenderers)
        {
            if (item.materials.Length > 0)
            {
                if (item.material.mainTexture)
                {
                    item.material.mainTexture.TrySaveToPNG($"{FileIOHelper.sandboxRoot}DumpedTextures/{item.material.mainTexture.name}.png");
                }
            }
        }

        if (node.isSprite)
        {
            foreach (var spriteRenderer in node.gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite.texture.TrySaveToPNG($"{FileIOHelper.sandboxRoot}DumpedTextures/{spriteRenderer.sprite.texture.name}.png");
            }
        }
    }
}