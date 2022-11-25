using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Hats;
using SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using SoftwareExam.CoreProgram.Economy;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// A large class, that takes in an item id, and returns different information about the item it refers to
    /// </summary>
    public static class ItemParser {

        // The Ugly Class

        public static string[] GetAllowedClasses(int id) {

            return id switch {
                101 => ArmorPlateArmor.AllowedClasses,
                102 => ArmorElvenRobe.AllowedClasses,
                103 => ArmorLeatherSuit.AllowedClasses,
                104 => ArmorFourCloverSuit.AllowedClasses,
                105 => ArmorOfGod.AllowedClasses,

                201 => HatPlateHelmet.AllowedClasses,
                202 => HatElvenTopHat.AllowedClasses,
                203 => HatHarlequinCrest.AllowedClasses,
                204 => HatJuggernautsHelmet.AllowedClasses,
                205 => HatLeatherHat.AllowedClasses,

                301 => OffHandWoodenShield.AllowedClasses,
                302 => OffHandSteelArrows.AllowedClasses,
                303 => OffHandBookOfWisdom.AllowedClasses,
                304 => OffHandMythrilShield.AllowedClasses,
                305 => OffHandGiantsShield.AllowedClasses,

                401 => TrinketRabbitsFoot.AllowedClasses,
                402 => TrinketTheChickens.AllowedClasses,
                403 => TrinketMimir.AllowedClasses,
                404 => TrinketRingOfPower.AllowedClasses,
                405 => TrinketFairyDust.AllowedClasses,

                501 => WeaponIronSword.AllowedClasses,
                502 => WeaponBinosKitchenKnife.AllowedClasses,
                503 => WeaponEldersStaff.AllowedClasses,
                504 => WeaponOrbOfDarkness.AllowedClasses,
                505 => WeaponWindripperBow.AllowedClasses,
                506 => WeaponSwordOfOmens.AllowedClasses,
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }

        public static string GetItemName(int id) {

            return id switch {
                101 => ArmorPlateArmor.GetItemName(),
                102 => ArmorElvenRobe.GetItemName(),
                103 => ArmorLeatherSuit.GetItemName(),
                104 => ArmorFourCloverSuit.GetItemName(),
                105 => ArmorOfGod.GetItemName(),

                201 => HatPlateHelmet.GetItemName(),
                202 => HatElvenTopHat.GetItemName(),
                203 => HatHarlequinCrest.GetItemName(),
                204 => HatJuggernautsHelmet.GetItemName(),
                205 => HatLeatherHat.GetItemName(),

                301 => OffHandWoodenShield.GetItemName(),
                302 => OffHandSteelArrows.GetItemName(),
                303 => OffHandBookOfWisdom.GetItemName(),
                304 => OffHandMythrilShield.GetItemName(),
                305 => OffHandGiantsShield.GetItemName(),

                401 => TrinketRabbitsFoot.GetItemName(),
                402 => TrinketTheChickens.GetItemName(),
                403 => TrinketMimir.GetItemName(),
                404 => TrinketRingOfPower.GetItemName(),
                405 => TrinketFairyDust.GetItemName(),

                501 => WeaponIronSword.GetItemName(),
                502 => WeaponBinosKitchenKnife.GetItemName(),
                503 => WeaponEldersStaff.GetItemName(),
                504 => WeaponOrbOfDarkness.GetItemName(),
                505 => WeaponWindripperBow.GetItemName(),
                506 => WeaponSwordOfOmens.GetItemName(),
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }

        public static string GetItemDescription(int id) {

            return id switch {
                101 => ArmorPlateArmor.GetItemDescription(),
                102 => ArmorElvenRobe.GetItemDescription(),
                103 => ArmorLeatherSuit.GetItemDescription(),
                104 => ArmorFourCloverSuit.GetItemDescription(),
                105 => ArmorOfGod.GetItemDescription(),

                201 => HatPlateHelmet.GetItemDescription(),
                202 => HatElvenTopHat.GetItemDescription(),
                203 => HatHarlequinCrest.GetItemDescription(),
                204 => HatJuggernautsHelmet.GetItemDescription(),
                205 => HatLeatherHat.GetItemDescription(),

                301 => OffHandWoodenShield.GetItemDescription(),
                302 => OffHandSteelArrows.GetItemDescription(),
                303 => OffHandBookOfWisdom.GetItemDescription(),
                304 => OffHandMythrilShield.GetItemDescription(),
                305 => OffHandGiantsShield.GetItemDescription(),

                401 => TrinketRabbitsFoot.GetItemDescription(),
                402 => TrinketTheChickens.GetItemDescription(),
                403 => TrinketMimir.GetItemDescription(),
                404 => TrinketRingOfPower.GetItemDescription(),
                405 => TrinketFairyDust.GetItemDescription(),

                501 => WeaponIronSword.GetItemDescription(),
                502 => WeaponBinosKitchenKnife.GetItemDescription(),
                503 => WeaponEldersStaff.GetItemDescription(),
                504 => WeaponOrbOfDarkness.GetItemDescription(),
                505 => WeaponWindripperBow.GetItemDescription(),
                506 => WeaponSwordOfOmens.GetItemDescription(),
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }

        public static Currency GetItemCost(int id) {

            return id switch {
                101 => ArmorPlateArmor.Cost,
                102 => ArmorElvenRobe.Cost,
                103 => ArmorLeatherSuit.Cost,
                104 => ArmorFourCloverSuit.Cost,
                105 => ArmorOfGod.Cost,

                201 => HatPlateHelmet.Cost,
                202 => HatElvenTopHat.Cost,
                203 => HatHarlequinCrest.Cost,
                204 => HatJuggernautsHelmet.Cost,
                205 => HatLeatherHat.Cost,

                301 => OffHandWoodenShield.Cost,
                302 => OffHandSteelArrows.Cost,
                303 => OffHandBookOfWisdom.Cost,
                304 => OffHandMythrilShield.Cost,
                305 => OffHandGiantsShield.Cost,

                401 => TrinketRabbitsFoot.Cost,
                402 => TrinketTheChickens.Cost,
                403 => TrinketMimir.Cost,
                404 => TrinketRingOfPower.Cost,
                405 => TrinketFairyDust.Cost,

                501 => WeaponIronSword.Cost,
                502 => WeaponBinosKitchenKnife.Cost,
                503 => WeaponEldersStaff.Cost,
                504 => WeaponOrbOfDarkness.Cost,
                505 => WeaponWindripperBow.Cost,
                506 => WeaponSwordOfOmens.Cost,
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }

        public static BaseDecoratedAdventurer GetItem(int id, Adventurer adventurer) {

            return id switch {
                100 => new BasicArmor(adventurer),
                101 => new ArmorPlateArmor(adventurer),
                102 => new ArmorElvenRobe(adventurer),
                103 => new ArmorLeatherSuit(adventurer),
                104 => new ArmorFourCloverSuit(adventurer),
                105 => new ArmorOfGod(adventurer),

                200 => new BasicHat(adventurer),
                201 => new HatPlateHelmet(adventurer),
                202 => new HatElvenTopHat(adventurer),
                203 => new HatHarlequinCrest(adventurer),
                204 => new HatJuggernautsHelmet(adventurer),
                205 => new HatLeatherHat(adventurer),

                300 => new BasicOffHand(adventurer),
                301 => new OffHandWoodenShield(adventurer),
                302 => new OffHandSteelArrows(adventurer),
                303 => new OffHandBookOfWisdom(adventurer),
                304 => new OffHandMythrilShield(adventurer),
                305 => new OffHandGiantsShield(adventurer),

                400 => new BasicTrinket(adventurer),
                401 => new TrinketRabbitsFoot(adventurer),
                402 => new TrinketTheChickens(adventurer),
                403 => new TrinketMimir(adventurer),
                404 => new TrinketRingOfPower(adventurer),
                405 => new TrinketFairyDust(adventurer),

                500 => new BasicWeapon(adventurer),
                501 => new WeaponIronSword(adventurer),
                502 => new WeaponBinosKitchenKnife(adventurer),
                503 => new WeaponEldersStaff(adventurer),
                504 => new WeaponOrbOfDarkness(adventurer),
                505 => new WeaponWindripperBow(adventurer),
                506 => new WeaponSwordOfOmens(adventurer),
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }
    }
}
