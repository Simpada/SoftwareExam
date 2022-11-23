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
    }
}
