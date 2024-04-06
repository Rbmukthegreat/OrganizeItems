using System.Collections.Generic;
using UnityEngine;

namespace OrganizeItems
{
    internal class ItemList
    {
        public static readonly Dictionary<string, Vector3> GoodItems = new() {
            {"Hairdryer", new Vector3(-0.69f, 0.35f, -12.76f)},
            {"Hairbrush", new Vector3(3.02f, 0.38f, -13.46f)},
            { "EasterEgg", new Vector3(-5.05f, 0.35f, -14.60f)},
            { "Mug", new Vector3(-1.18f, 0.35f, -14.37f)},
            { "Dentures", new Vector3(-1.87f, 0.36f, -13.35f)},
            { "FancyLamp", new Vector3(2.89f, 0.29f, -14.23f)},
            { "ComedyMask", new Vector3(-2.65f, 0.35f, -14.50f)},
            { "FancyRing", new Vector3(-2.06f, 0.35f, -14.41f)},
            { "TragedyMask", new Vector3(-2.81f, 0.35f, -13.96f)},
            { "BinFullOfBottles", new Vector3(-3.76f, 0.35f, -14.61f)},
            { "ToyCube", new Vector3(-2.21f, 0.35f, -12.43f)},
            { "FancyGlass", new Vector3(-4.63f, 0.35f, -12.38f)},
            { "FishTestProp", new Vector3(-5.07f, 0.35f, -15.20f)},
            { "PerfumeBottle", new Vector3(0.02f, 0.35f, -15.96f)},
            { "Painting", new Vector3(2.56f, 0.29f, -15.86f)},
            { "Airhorn", new Vector3(1.21f, 0.35f, -13.02f)},
            { "RedSodaCan", new Vector3(-0.61f, 0.35f, -13.66f)},
            { "RubberDucky", new Vector3(9.65f, 1.73f, -13.98f)},
            { "MagnifyingGlass", new Vector3(-2.66f, 0.38f, -14.95f)},
            { "Toothpaste", new Vector3(-3.68f, 0.35f, -15.28f)},
            { "Candy", new Vector3(-2.30f, 0.35f, -15.96f)},
            { "RedLocustHive", new Vector3(7.59f, 0.29f, -12.63f)},
            { "TeaKettle", new Vector3(-3.71f, 0.35f, -13.75f)},
            { "HandBell", new Vector3(-3.21f, 0.35f, -13.04f)},
            { "RobotToy", new Vector3(1.02f, 0.29f, -15.95f)},
            { "Clownhorn", new Vector3(1.21f, 0.35f, -13.02f)},
            { "OldPhone", new Vector3(-0.53f, 0.35f, -14.55f)},
            { "LaserPointer",  new Vector3(-5.07f, 0.35f, -14.35f)},
            { "Magic7Ball", new Vector3(-4.93f, 0.37f, -13.61f)} };

        public static readonly Dictionary<string, Vector3> BadItems = new()
        {
            {"StopSign", new Vector3(4.13f, 0.37f, -13.58f)},
            {"YieldSign", new Vector3(4.13f, 0.37f, -13.58f)},
            {"CookieMoldPan", new Vector3(4.13f, 0.37f, -13.58f)},
            {"DiyFlashbang", new Vector3(4.13f, 0.37f, -13.58f)},
            {"PillBottle", new Vector3(4.13f, 0.37f, -13.58f)},
            {"Dustpan", new Vector3(4.13f, 0.37f, -13.58f)},
            {"SteeringWheel", new Vector3(4.13f, 0.37f, -13.58f)},
            {"Remote", new Vector3(4.13f, 0.37f, -13.58f)},
            {"ChemicalJug", new Vector3(4.13f, 0.37f, -13.58f)},
            {"Flask", new Vector3(4.13f, 0.37f, -13.58f)},
            {"EnginePart", new Vector3(4.13f, 0.37f, -13.58f)},
            {"EggBeater", new Vector3(4.13f, 0.37f, -13.58f)},
            {"BigBolt", new Vector3(4.13f, 0.37f, -13.58f)},
            {"MetalSheet", new Vector3(4.13f, 0.37f, -13.58f)},
            {"WhoopieCushion", new Vector3(9.73f, 1.74f, -15.20f)}
        };
    }
}
