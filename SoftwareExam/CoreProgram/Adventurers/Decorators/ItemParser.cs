using SoftwareExam.CoreProgram.Adventurers.Decorators.Armors;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Hats;
using SoftwareExam.CoreProgram.Adventurers.Decorators.OffHands;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Trinkets;
using SoftwareExam.CoreProgram.Adventurers.Decorators.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareExam.CoreProgram.Adventurers.Decorators {
    public static class ItemParser {

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
        
        public static BaseDecoratedAdventurer GetItem(int id) {

            return id switch {
                101 => new ArmorPlateArmor(new Warrior()),
                102 => new ArmorElvenRobe(new Warrior()),
                103 => new ArmorLeatherSuit(new Warrior()),
                104 => new ArmorFourCloverSuit(new Warrior()),
                105 => new ArmorOfGod(new Warrior()),

                201 => new HatPlateHelmet(new Warrior()),
                202 => new HatElvenTopHat(new Warrior()),
                203 => new HatHarlequinCrest(new Warrior()),
                204 => new HatJuggernautsHelmet(new Warrior()),
                205 => new HatLeatherHat(new Warrior()),

                301 => new OffHandWoodenShield(new Warrior()),
                302 => new OffHandSteelArrows(new Warrior()),
                303 => new OffHandBookOfWisdom(new Warrior()),
                304 => new OffHandMythrilShield(new Warrior()),
                305 => new OffHandGiantsShield(new Warrior()),

                401 => new TrinketRabbitsFoot(new Warrior()),
                402 => new TrinketTheChickens(new Warrior()),
                403 => new TrinketMimir(new Warrior()),
                404 => new TrinketRingOfPower(new Warrior()),
                405 => new TrinketFairyDust(new Warrior()),

                501 => new WeaponIronSword(new Warrior()),
                502 => new WeaponBinosKitchenKnife(new Warrior()),
                503 => new WeaponEldersStaff(new Warrior()),
                504 => new WeaponOrbOfDarkness(new Warrior()),
                505 => new WeaponWindripperBow(new Warrior()),
                506 => new WeaponSwordOfOmens(new Warrior()),
                _ => throw new ArgumentOutOfRangeException(id.ToString() + " is not a valid item code"),
            };
        }
    }
}
