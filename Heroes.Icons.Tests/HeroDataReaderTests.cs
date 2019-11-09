﻿using Heroes.Models;
using Heroes.Models.AbilityTalents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Heroes.Icons.Tests
{
    [TestClass]
    public class HeroDataReaderTests : IDataReader
    {
        private readonly string _dataFile = Path.Combine("JsonData", "herodata_76003_kokr.json");
        private readonly string _jsonGameStringFileKOKR = Path.Combine("JsonGameStrings", "gamestrings_76893_kokr.json");
        private readonly string _jsonGameStringFileFRFR = Path.Combine("JsonGameStrings", "gamestrings_76893_frfr.json");

        private readonly HeroDataReader _heroDataReader;

        public HeroDataReaderTests()
        {
            _heroDataReader = new HeroDataReader(LoadJsonTestData(), Localization.ENUS);
        }

        [DataTestMethod]
        [DataRow("Alarak", false, false, false, false)]
        [DataRow("Alarak", true, false, false, false)]
        [DataRow("Alarak", false, true, false, false)]
        [DataRow("Alarak", true, true, false, false)]
        [DataRow(null, true, true, false, false)]
        [DataRow("asdf", true, true, false, false)]
        public void GetHeroByIdTest(string id, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (id is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.GetHeroById(id!, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (id == "asdf")
            {
                Assert.ThrowsException<KeyNotFoundException>(() =>
                {
                    _ = _heroDataReader.GetHeroById(id, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }

            Hero hero = _heroDataReader.GetHeroById(id, abilities, subAbilities, talents, heroUnits);

            Assert.AreEqual("Alarak", hero.CHeroId);
            Assert.AreEqual("Alarak", hero.Name);

            Assert.AreEqual("Alarak", hero.HyperlinkId);
            Assert.AreEqual("Alar", hero.AttributeId);
            Assert.AreEqual("Hard", hero.Difficulty);
            Assert.AreEqual(HeroFranchise.Starcraft, hero.Franchise);
            Assert.AreEqual(UnitGender.Male, hero.Gender);
            Assert.AreEqual("Highlord of the Tal'darim", hero.Title);
            Assert.AreEqual(0.8125, hero.InnerRadius);
            Assert.AreEqual(0.8125, hero.Radius);
            Assert.AreEqual(new DateTime(2016, 9, 13), hero.ReleaseDate);
            Assert.AreEqual(12.0, hero.Sight);
            Assert.AreEqual(4.8398, hero.Speed);
            Assert.AreEqual("Melee", hero.Type);
            Assert.AreEqual(Rarity.Epic, hero.Rarity);
            Assert.AreEqual("ExcellentMana", hero.ScalingBehaviorLink);
            Assert.AreEqual("Alarak Ascendant Protoss SC SC2 StarCraft Star2 Starcraft2 II 2 Legacy of the Void LotV Covert Ops CO", hero.SearchText);
            Assert.AreEqual("A combo Assassin that can move enemies around and punish mistakes.", hero.Description!.RawDescription);
            Assert.AreEqual("Escaper", hero.HeroDescriptors.ToList()[1]);
            Assert.AreEqual("AbathurEvolvedMonstrosity", hero.UnitIds.ToList()[0]);
            Assert.AreEqual("AbathurLocustAssaultStrain", hero.UnitIds.ToList()[1]);

            Assert.AreEqual("storm_ui_ingame_heroselect_btn_alarak.png", hero.HeroPortrait.HeroSelectPortraitFileName);
            Assert.AreEqual("storm_ui_ingame_hero_leaderboard_alarak.png", hero.HeroPortrait.LeaderboardPortraitFileName);
            Assert.AreEqual("storm_ui_ingame_hero_loadingscreen_alarak.png", hero.HeroPortrait.LoadingScreenPortraitFileName);
            Assert.AreEqual("storm_ui_ingame_partypanel_btn_alarak.png", hero.HeroPortrait.PartyPanelPortraitFileName);
            Assert.AreEqual("ui_targetportrait_hero_alarak.png", hero.HeroPortrait.TargetPortraitFileName);
            Assert.AreEqual("storm_ui_glues_draft_portrait_alarak.png", hero.HeroPortrait.DraftScreenFileName);
            Assert.AreEqual("storm_ui_ingame_partyframe_alarak.png", hero.HeroPortrait.PartyFrameFileName.ToList()[0]);
            Assert.AreEqual("storm_ui_minimapicon_alarak.png", hero.UnitPortrait.MiniMapIconFileName);
            Assert.AreEqual("storm_ui_ingame_partyframe_alarak.png", hero.UnitPortrait.TargetInfoPanelFileName);

            Assert.AreEqual(755, hero.Life.LifeMax);
            Assert.AreEqual(0.04, hero.Life.LifeScaling);
            Assert.AreEqual("Life", hero.Life.LifeType);
            Assert.AreEqual(5.4256, hero.Life.LifeRegenerationRate);
            Assert.AreEqual(0.04, hero.Life.LifeRegenerationRateScaling);

            Assert.AreEqual(755, hero.Shield.ShieldMax);
            Assert.AreEqual(0.04, hero.Shield.ShieldScaling);
            Assert.AreEqual("Shield", hero.Shield.ShieldType);
            Assert.AreEqual(5, hero.Shield.ShieldRegenerationDelay);
            Assert.AreEqual(5.245, hero.Shield.ShieldRegenerationRate);
            Assert.AreEqual(0.04, hero.Shield.ShieldRegenerationRateScaling);

            Assert.AreEqual(755, hero.Energy.EnergyMax);
            Assert.AreEqual("Ammo", hero.Energy.EnergyType);
            Assert.AreEqual(5.4256, hero.Energy.EnergyRegenerationRate);

            List<UnitArmor> heroArmorList = hero.Armor.ToList();

            Assert.AreEqual("structure", heroArmorList[0].Type);
            Assert.AreEqual(30, heroArmorList[0].BasicArmor);
            Assert.AreEqual(20, heroArmorList[0].AbilityArmor);
            Assert.AreEqual(30, heroArmorList[0].SplashArmor);

            Assert.AreEqual("hero", heroArmorList[1].Type);
            Assert.AreEqual(10, heroArmorList[1].BasicArmor);
            Assert.AreEqual(0, heroArmorList[1].AbilityArmor);
            Assert.AreEqual(0, heroArmorList[1].SplashArmor);

            Assert.AreEqual("Assassin", hero.Roles.ToList()[0]);
            Assert.AreEqual("Melee Assassin", hero.ExpandedRole);

            Assert.AreEqual(8, hero.Ratings.Complexity);
            Assert.AreEqual(7, hero.Ratings.Damage);
            Assert.AreEqual(6, hero.Ratings.Survivability);
            Assert.AreEqual(7, hero.Ratings.Utility);

            List<UnitWeapon> heroWeapons = hero.Weapons.ToList();

            Assert.AreEqual("AllianceSuperCavalryWeapon", heroWeapons[0].WeaponNameId);
            Assert.AreEqual(5, heroWeapons[0].Range);
            Assert.AreEqual(3.9, heroWeapons[0].Period);
            Assert.AreEqual(454, heroWeapons[0].Damage);
            Assert.AreEqual(0.65, heroWeapons[0].DamageScaling);
            Assert.AreEqual(3.9, heroWeapons[0].Period);
            Assert.AreEqual("minion", heroWeapons[0].AttributeFactors.ToList()[0].Type);
            Assert.AreEqual(1.5, heroWeapons[0].AttributeFactors.ToList()[0].Value);

            Assert.AreEqual("AlteracBossWeaponParent", heroWeapons[1].WeaponNameId);
            Assert.AreEqual(4, heroWeapons[1].Range);
            Assert.AreEqual(40.1, heroWeapons[1].Period);
            Assert.AreEqual("summoned", heroWeapons[1].AttributeFactors.ToList()[1].Type);
            Assert.AreEqual(1.0, heroWeapons[1].AttributeFactors.ToList()[1].Value);

            if (abilities)
            {
                Ability ability = hero.GetAbility(new AbilityTalentId("AlarakDeadlyChargeActivate", "AlarakDeadlyCharge")
                {
                    AbilityType = AbilityType.Heroic,
                });

                Assert.AreEqual(AbilityTier.Heroic, ability.Tier);
                Assert.AreEqual("AlarakDeadlyChargeActivate", ability.AbilityTalentId?.ReferenceId);
                Assert.AreEqual("AlarakDeadlyCharge", ability.AbilityTalentId?.ButtonId);
                Assert.AreEqual("Deadly Charge", ability.Name);
                Assert.AreEqual("storm_ui_icon_alarak_recklesscharge.png", ability.IconFileName);
                Assert.AreEqual(0.5, ability.Tooltip.Cooldown.ToggleCooldown);
                Assert.AreEqual("<s val=\"bfd4fd\" name=\"StandardTooltipDetails\">Mana: 60</s>", ability.Tooltip.Energy.EnergyTooltip?.RawDescription);
                Assert.AreEqual("Cooldown: 45 seconds", ability.Tooltip.Cooldown.CooldownTooltip?.RawDescription);
                Assert.AreEqual("Channel to charge a long distance", ability.Tooltip.ShortTooltip?.RawDescription);
                Assert.AreEqual("After channeling, Alarak charges forward dealing <c val=\"bfd4fd\">200~~0.04~~</c> damage to all enemies in his path. Distance is increased based on the amount of time channeled, up to <c val=\"bfd4fd\">1.5</c> seconds.<n/><n/>Issuing a Move order while this is channeling will cancel it at no cost. Taking damage will interrupt the channeling.", ability.Tooltip.FullTooltip?.RawDescription);
                Assert.AreEqual(AbilityType.Heroic, ability!.AbilityTalentId!.AbilityType);

                ability = hero.GetAbility(new AbilityTalentId("AlarakSadism", "AlarakSadism")
                {
                    AbilityType = AbilityType.Trait,
                    IsPassive = true,
                });

                Assert.AreEqual(AbilityTier.Trait, ability.Tier);
                Assert.AreEqual("Sadism", ability.Name);
                Assert.AreEqual("storm_ui_icon_alarak_sadism.png", ability.IconFileName);
                Assert.AreEqual(AbilityType.Trait, ability!.AbilityTalentId!.AbilityType);
                Assert.IsTrue(ability!.AbilityTalentId!.IsPassive);
                Assert.IsFalse(ability.IsActive);

                ability = hero.GetAbility(new AbilityTalentId("Mount", "SummonMount")
                {
                    AbilityType = AbilityType.Z,
                });

                Assert.AreEqual(AbilityTier.Mount, ability.Tier);
                Assert.AreEqual(AbilityType.Z, ability!.AbilityTalentId!.AbilityType);

                ability = hero.GetAbility(new AbilityTalentId("LootSpray", "LootSpray")
                {
                    AbilityType = AbilityType.Spray,
                });

                Assert.AreEqual(AbilityTier.Spray, ability.Tier);
                Assert.AreEqual(AbilityType.Spray, ability!.AbilityTalentId!.AbilityType);

                ability = hero.GetAbility(new AbilityTalentId("LootYellVoiceLine", "LootYellVoiceLine")
                {
                    AbilityType = AbilityType.Voice,
                });

                Assert.AreEqual(AbilityTier.Voice, ability.Tier);
                Assert.AreEqual(AbilityType.Voice, ability!.AbilityTalentId!.AbilityType);
                Assert.IsTrue(ability.IsActive);
                Assert.IsTrue(ability.IsQuest);

                ability = hero.GetAbility(new AbilityTalentId("GallTalentEyeOfKilrogg", "GallEyeofKilroggHotbar")
                {
                    AbilityType = AbilityType.Active,
                });

                Assert.AreEqual(AbilityTier.Activable, ability.Tier);
                Assert.AreEqual(AbilityType.Active, ability!.AbilityTalentId!.AbilityType);
                Assert.AreEqual(2, ability.Tooltip.Charges.CountMax);
                Assert.AreEqual(1, ability.Tooltip.Charges.CountUse);
                Assert.AreEqual(2, ability.Tooltip.Charges.CountStart);
                Assert.AreEqual(1, ability.Tooltip.Charges.RecastCooldown);
                Assert.IsTrue(ability.Tooltip.Charges.IsHideCount!.Value);
                Assert.IsTrue(ability.Tooltip.Charges.HasCharges);
                Assert.AreEqual("sappity sap", ability.Tooltip.Life.LifeCostTooltip!.RawDescription);

                ability = hero.GetAbility(new AbilityTalentId("tauntId", "tauntId")
                {
                    AbilityType = AbilityType.Taunt,
                });

                Assert.AreEqual(AbilityTier.Taunt, ability.Tier);
                Assert.AreEqual(AbilityType.Taunt, ability!.AbilityTalentId!.AbilityType);
                Assert.IsNull(ability.Tooltip.Charges.CountMax);
                Assert.IsNull(ability.Tooltip.Charges.CountUse);
                Assert.IsNull(ability.Tooltip.Charges.CountStart);
                Assert.IsNull(ability.Tooltip.Charges.RecastCooldown);
                Assert.IsNull(ability.Tooltip.Charges.IsHideCount);
                Assert.IsFalse(ability.Tooltip.Charges.HasCharges);

                ability = hero.GetAbility(new AbilityTalentId("danceId", "danceId")
                {
                    AbilityType = AbilityType.Dance,
                });

                Assert.AreEqual(AbilityTier.Dance, ability.Tier);
                Assert.AreEqual(AbilityType.Dance, ability!.AbilityTalentId!.AbilityType);

                ability = hero.GetAbility(new AbilityTalentId("unknownId", "unknownId")
                {
                    AbilityType = AbilityType.Unknown,
                });

                Assert.AreEqual(AbilityTier.Unknown, ability.Tier);
                Assert.AreEqual(AbilityType.Unknown, ability!.AbilityTalentId!.AbilityType);
                Assert.IsTrue(ability.IsActive);
                Assert.IsFalse(ability.IsQuest);
            }

            if (subAbilities)
            {
                IEnumerable<Ability> subAbilitiesList = hero.SubAbilities().ToList();
                Assert.AreEqual(2, subAbilitiesList.Count());

                Ability ability = hero.GetAbility(new AbilityTalentId("Dismount", "UnsummonMount")
                {
                    AbilityType = AbilityType.Z,
                });

                Assert.AreEqual("Unsummon Mount", ability.Name);
            }
        }

        [DataTestMethod]
        [DataRow("Ragnaros", false, false, false, false)]
        [DataRow("Ragnaros", false, false, true, false)]
        [DataRow("Ragnaros", false, false, false, true)]
        [DataRow("Ragnaros", true, false, true, true)]
        [DataRow(null, false, false, true, true)]
        [DataRow("asdf", false, false, true, true)]
        public void TryGetHeroByIdTest(string id, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (id is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.TryGetHeroById(id!, out _, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (id == "asdf")
            {
                Assert.IsFalse(_heroDataReader.TryGetHeroById(id, out _, abilities, subAbilities, talents, heroUnits));

                return;
            }

            Assert.IsTrue(_heroDataReader.TryGetHeroById(id, out Hero hero, abilities, subAbilities, talents, heroUnits));
            Assert.AreEqual("Ragnaros", hero.CHeroId);
            Assert.AreEqual("Ragnaros", hero.Name);
            Assert.AreEqual("HeroRagnaros", hero.CUnitId);

            Assert.AreEqual("Ragnaros", hero.HyperlinkId);
            Assert.AreEqual("Ragn", hero.AttributeId);
            Assert.AreEqual("Medium", hero.Difficulty);
            Assert.AreEqual(HeroFranchise.Unknown, hero.Franchise);
            Assert.AreEqual(UnitGender.Neutral, hero.Gender);
            Assert.AreEqual(Rarity.Unknown, hero.Rarity);

            if (talents)
            {
                Talent talent = hero.GetTalent("RagnarosEmpowerSulfurasSulfurasHungers");
                Assert.AreEqual("RagnarosEmpowerSulfurasSulfurasHungers", talent.AbilityTalentId.ReferenceId);
                Assert.AreEqual("RagnarosEmpowerSulfurasSulfurasHungers", talent.AbilityTalentId.ButtonId);
                Assert.AreEqual("Sulfuras Hungers", talent.Name);
                Assert.AreEqual("storm_ui_icon_ragnaros_empowersulfuras.png", talent.IconFileName);
                Assert.AreEqual("<c val=\"e4b800\">Quest:</c> Kill Minions with Empower Sulfuras to increase its damage", talent.Tooltip.ShortTooltip!.ColoredText);
                Assert.AreEqual("This is a full tooltip", talent.Tooltip.FullTooltip!.ColoredText);
                Assert.AreEqual(AbilityType.Q, talent.AbilityTalentId.AbilityType);
                Assert.IsTrue(talent.IsQuest);
                Assert.AreEqual(1, talent.Column);
                Assert.AreEqual(2, talent.AbilityTalentLinkIdsCount);
                Assert.AreEqual("RagnarosEmpowerSulfurasActive", talent.AbilityTalentLinkIds.ToList()[0]);
                Assert.AreEqual("RagnarosEmpowerSulfuras", talent.AbilityTalentLinkIds.ToList()[1]);

                talent = hero.GetTalent("RagnarosLivingMeteorShiftingMeteor");
                Assert.AreEqual(AbilityType.W, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(2, talent.Column);

                talent = hero.GetTalent("RagnarosCatchingFire");
                Assert.AreEqual(AbilityType.Active, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(3, talent.Column);
                Assert.IsTrue(talent.IsActive);

                talent = hero.GetTalent("RagnarosEmpowerSulfurasHandOfRagnaros");
                Assert.AreEqual(AbilityType.Q, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(1, talent.Column);
                Assert.IsFalse(talent.IsActive);

                talent = hero.GetTalent("RagnarosSulfurasSmash");
                Assert.AreEqual(AbilityType.Heroic, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(1, talent.Column);

                talent = hero.GetTalent("RagnarosEmpowerSulfurasCauterizeWounds");
                Assert.AreEqual(AbilityType.Q, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(1, talent.Column);

                talent = hero.GetTalent("RagnarosLivingMeteorMeteorBomb");
                Assert.AreEqual(AbilityType.W, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(2, talent.Column);

                talent = hero.GetTalent("RagnarosSulfurasSmashFlamesOfSulfuron");
                Assert.AreEqual(AbilityType.Heroic, talent.AbilityTalentId.AbilityType);
                Assert.AreEqual(1, talent.Column);
                Assert.AreEqual(1, talent.PrerequisiteTalentIdCount);
                Assert.AreEqual("RagnarosSulfurasSmash", talent.PrerequisiteTalentIds.ToList()[0]);
            }
            else
            {
                Assert.AreEqual(0, hero.TalentsCount);
            }

            if (heroUnits)
            {
                List<Hero> heroUnitList = hero.HeroUnits.ToList();
                Hero ragnarosHero = heroUnitList[0];

                Assert.AreEqual("Ragnaros", ragnarosHero.Name);
                Assert.AreEqual("RagnarosBigRag", ragnarosHero.HyperlinkId);
                Assert.AreEqual(3, ragnarosHero.Radius);
                Assert.AreEqual(12, ragnarosHero.Sight);
                Assert.AreEqual(4.8398, ragnarosHero.Speed);
            }
            else
            {
                Assert.AreEqual(0, hero.HeroUnitCount);
            }
        }

        [TestMethod]
        public void UpdateGameStringsTest()
        {
            Hero hero = new Hero
            {
                CUnitId = "HeroAlarak",
                CHeroId = "Alarak",
                Id = "Alarak",
            };

            hero.AddAbility(new Ability()
            {
                AbilityTalentId = new AbilityTalentId("AlarakDiscordStrike", "AlarakDiscordStrike")
                {
                    AbilityType = AbilityType.Q,
                    IsPassive = false,
                },
            });

            using GameStringReader gameStringReader = new GameStringReader(LoadEnusLocalizedStringData());
            gameStringReader.UpdateGameStrings(hero);

            Assert.AreEqual("Hard", hero.Difficulty);
            Assert.AreEqual("Melee Assassin", hero.ExpandedRole);
            Assert.AreEqual("Assassin", hero.Roles.ToList()[0]);
            Assert.AreEqual("Warrior", hero.Roles.ToList()[1]);
            Assert.AreEqual("Alarak Ascendant Protoss SC SC2 StarCraft Star2 Starcraft2 II 2 Legacy of the Void LotV Covert Ops CO", hero.SearchText);
            Assert.AreEqual("Highlord of the Tal'darim", hero.Title);
            Assert.AreEqual("Melee", hero.Type);

            Ability ability = hero.GetAbility(new AbilityTalentId("AlarakDiscordStrike", "AlarakDiscordStrike")
            {
                AbilityType = AbilityType.Q,
            });

            Assert.AreEqual("Discord Strike", ability.Name);
            Assert.AreEqual("Cooldown: 8 seconds", ability.Tooltip.Cooldown.CooldownTooltip!.RawDescription);
            Assert.AreEqual("<s val=\"bfd4fd\" name=\"StandardTooltipDetails\">Mana: 55</s>", ability.Tooltip.Energy.EnergyTooltip!.RawDescription);
            Assert.AreEqual("After a <c val=\"bfd4fd\">0.5</c> second delay, enemies in front of Alarak take <c val=\"bfd4fd\">175~~0.04~~</c> damage and are silenced for <c val=\"bfd4fd\">1.5</c> seconds. ", ability.Tooltip.FullTooltip!.RawDescription);
            Assert.AreEqual("Damage and silence enemies in an area", ability.Tooltip.ShortTooltip!.RawDescription);
            Assert.AreEqual("No life", ability.Tooltip.Life.LifeCostTooltip!.RawDescription);
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void TryGetHeroByNameTest(string name, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (name is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.TryGetHeroByName(name!, out _, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (name == "asdf")
            {
                Assert.IsFalse(_heroDataReader.TryGetHeroByName(name!, out _, abilities, subAbilities, talents, heroUnits));

                return;
            }

            Assert.IsTrue(_heroDataReader.TryGetHeroByName(name, out Hero hero, abilities, subAbilities, talents, heroUnits));
            BasicRagnarosAsserts(hero);
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void GetHeroByNameTest(string name, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (name is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.GetHeroByName(name!, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (name == "asdf")
            {
                Assert.ThrowsException<KeyNotFoundException>(() =>
                {
                    _ = _heroDataReader.GetHeroByName(name, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }

            BasicRagnarosAsserts(_heroDataReader.GetHeroByName(name, abilities, subAbilities, talents, heroUnits));
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("HeroRagnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void TryGetHeroByUnitIdTest(string unitId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (unitId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.TryGetHeroByUnitId(unitId!, out _, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (unitId == "asdf")
            {
                Assert.IsFalse(_heroDataReader.TryGetHeroByUnitId(unitId!, out _, abilities, subAbilities, talents, heroUnits));

                return;
            }

            Assert.IsTrue(_heroDataReader.TryGetHeroByUnitId(unitId, out Hero hero, abilities, subAbilities, talents, heroUnits));
            BasicRagnarosAsserts(hero);
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("HeroRagnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void GetHeroByUnitIdTest(string unitId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (unitId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.GetHeroByUnitId(unitId!, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (unitId == "asdf")
            {
                Assert.ThrowsException<KeyNotFoundException>(() =>
                {
                    _ = _heroDataReader.GetHeroByUnitId(unitId, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }

            BasicRagnarosAsserts(_heroDataReader.GetHeroByUnitId(unitId, abilities, subAbilities, talents, heroUnits));
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void TryGetHeroByHyperlinkIdTest(string hyperlinkId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (hyperlinkId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.TryGetHeroByHyperlinkId(hyperlinkId!, out _, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (hyperlinkId == "asdf")
            {
                Assert.IsFalse(_heroDataReader.TryGetHeroByHyperlinkId(hyperlinkId!, out _, abilities, subAbilities, talents, heroUnits));

                return;
            }

            Assert.IsTrue(_heroDataReader.TryGetHeroByHyperlinkId(hyperlinkId, out Hero hero, abilities, subAbilities, talents, heroUnits));
            BasicRagnarosAsserts(hero);
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragnaros", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void GetHeroByHyperlinkIdTest(string hyperlinkId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (hyperlinkId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.GetHeroByHyperlinkId(hyperlinkId!, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (hyperlinkId == "asdf")
            {
                Assert.ThrowsException<KeyNotFoundException>(() =>
                {
                    _ = _heroDataReader.GetHeroByHyperlinkId(hyperlinkId, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }

            BasicRagnarosAsserts(_heroDataReader.GetHeroByHyperlinkId(hyperlinkId, abilities, subAbilities, talents, heroUnits));
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragn", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void TryGetHeroByAttributeIdTest(string attributeId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (attributeId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.TryGetHeroByAttributeId(attributeId!, out _, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (attributeId == "asdf")
            {
                Assert.IsFalse(_heroDataReader.TryGetHeroByAttributeId(attributeId!, out _, abilities, subAbilities, talents, heroUnits));

                return;
            }

            Assert.IsTrue(_heroDataReader.TryGetHeroByAttributeId(attributeId, out Hero hero, abilities, subAbilities, talents, heroUnits));
            BasicRagnarosAsserts(hero);
        }

        [DataTestMethod]
        [DataRow(null, true, true, true, true)]
        [DataRow("Ragn", true, true, true, true)]
        [DataRow("asdf", true, true, true, true)]
        public void GetHeroByAttributeIdTest(string attributeId, bool abilities, bool subAbilities, bool talents, bool heroUnits)
        {
            if (attributeId is null)
            {
                Assert.ThrowsException<ArgumentNullException>(() =>
                {
                    _ = _heroDataReader.GetHeroByAttributeId(attributeId!, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }
            else if (attributeId == "asdf")
            {
                Assert.ThrowsException<KeyNotFoundException>(() =>
                {
                    _ = _heroDataReader.GetHeroByAttributeId(attributeId, abilities, subAbilities, talents, heroUnits);
                });

                return;
            }

            BasicRagnarosAsserts(_heroDataReader.GetHeroByAttributeId(attributeId, abilities, subAbilities, talents, heroUnits));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetNameFromHeroIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetNameFromHeroId(null!));
            Assert.AreEqual("MurkyName", heroDataReader.GetNameFromHeroId("Murky"));
            Assert.IsNull(heroDataReader.GetNameFromHeroId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetNameFromUnitIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("unitId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("unitId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("unitId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetNameFromUnitId(null!));
            Assert.AreEqual("MurkyName", heroDataReader.GetNameFromUnitId("hero3"));
            Assert.IsNull(heroDataReader.GetNameFromUnitId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetNameFromHyperlinkIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("hyperlinkId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("hyperlinkId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("hyperlinkId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetNameFromHyperlinkId(null!));
            Assert.AreEqual("MurkyName", heroDataReader.GetNameFromHyperlinkId("hero3"));
            Assert.IsNull(heroDataReader.GetNameFromHyperlinkId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetNameFromAttributeIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("attributeId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("attributeId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("attributeId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetNameFromAttributeId(null!));
            Assert.AreEqual("MurkyName", heroDataReader.GetNameFromAttributeId("hero3"));
            Assert.IsNull(heroDataReader.GetNameFromAttributeId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroIdFromNameTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetHeroIdFromName(null!));
            Assert.AreEqual("Alarak", heroDataReader.GetHeroIdFromName("AlarakName"));
            Assert.IsNull(heroDataReader.GetHeroIdFromName("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroIdFromUnitIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("unitId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("unitId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("unitId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetHeroIdFromUnitId(null!));
            Assert.AreEqual("Murky", heroDataReader.GetHeroIdFromUnitId("hero3"));
            Assert.IsNull(heroDataReader.GetHeroIdFromUnitId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroIdFromHyperlinkIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("hyperlinkId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("hyperlinkId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("hyperlinkId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetHeroIdFromHyperlinkId(null!));
            Assert.AreEqual("Murky", heroDataReader.GetHeroIdFromHyperlinkId("hero3"));
            Assert.IsNull(heroDataReader.GetHeroIdFromHyperlinkId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetGetHeroIdFromAttributeIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("attributeId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("attributeId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("attributeId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.GetHeroIdFromAttributeId(null!));
            Assert.AreEqual("Murky", heroDataReader.GetHeroIdFromAttributeId("hero3"));
            Assert.IsNull(heroDataReader.GetHeroIdFromAttributeId("hero5"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void IsHeroExistsByHeroIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.IsHeroExistsByHeroId(null!));
            Assert.IsTrue(heroDataReader.IsHeroExistsByHeroId("Alarak"));
            Assert.IsFalse(heroDataReader.IsHeroExistsByHeroId("Peon"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void IsHeroExistsByNameTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("attributeId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("attributeId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("attributeId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.IsHeroExistsByName(null!));
            Assert.IsTrue(heroDataReader.IsHeroExistsByName("AlarakName"));
            Assert.IsFalse(heroDataReader.IsHeroExistsByName("Peon"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void IsHeroExistsByUnitIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("unitId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("unitId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("unitId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.IsHeroExistsByUnitId(null!));
            Assert.IsTrue(heroDataReader.IsHeroExistsByUnitId("hero2"));
            Assert.IsFalse(heroDataReader.IsHeroExistsByUnitId("Peon"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void IsHeroExistsByHyperlinkIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("hyperlinkId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("hyperlinkId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("hyperlinkId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.IsHeroExistsByHyperlinkId(null!));
            Assert.IsTrue(heroDataReader.IsHeroExistsByHyperlinkId("hero2"));
            Assert.IsFalse(heroDataReader.IsHeroExistsByHyperlinkId("Peon"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void IsHeroExistsByAttributeIdTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteString("attributeId", "hero1");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteString("attributeId", "hero2");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteString("attributeId", "hero3");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);
            Assert.ThrowsException<ArgumentNullException>(() => heroDataReader.IsHeroExistsByAttributeId(null!));
            Assert.IsTrue(heroDataReader.IsHeroExistsByAttributeId("hero2"));
            Assert.IsFalse(heroDataReader.IsHeroExistsByAttributeId("Peon"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetIdsTest()
        {
            List<string> items = _heroDataReader.GetIds.ToList();

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Contains("Alarak"));
            Assert.IsTrue(items.Contains("Ragnaros"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroNamesTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("name", "AbathurName");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "AlarakName");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("name", "MurkyName");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);

            List<string> items = heroDataReader.GetNames.ToList();

            Assert.AreEqual(3, items.Count);
            Assert.IsTrue(items.Contains("AbathurName"));
            Assert.IsTrue(items.Contains("AlarakName"));
            Assert.IsTrue(items.Contains("MurkyName"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroUnitIds()
        {
            List<string> items = _heroDataReader.GetUnitIds.ToList();

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Contains("HeroAlarak"));
            Assert.IsTrue(items.Contains("HeroRagnaros"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroHyperlinkIdsTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteString("hyperlinkId", "AbathurName");
            writer.WriteEndObject();
            writer.WriteStartObject("Alarak");
            writer.WriteString("hyperlinkId", "AlarakName");
            writer.WriteEndObject();
            writer.WriteStartObject("Murky");
            writer.WriteString("hyperlinkId", "MurkyName");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.ENUS);

            List<string> items = heroDataReader.GetHyperlinkIds.ToList();

            Assert.AreEqual(3, items.Count);
            Assert.IsTrue(items.Contains("AbathurName"));
            Assert.IsTrue(items.Contains("AlarakName"));
            Assert.IsTrue(items.Contains("MurkyName"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GetHeroAttributeIdsTest()
        {
            List<string> items = _heroDataReader.GetAttributeIds.ToList();

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Contains("Alar"));
            Assert.IsTrue(items.Contains("Ragn"));
        }

        [TestMethod]
        [TestCategory("Helper")]
        public void GCountTest()
        {
            Assert.AreEqual(2, _heroDataReader.Count);
        }

        [TestMethod]
        [TestCategory("DataReader")]
        public void DataReaderFileTest()
        {
            using HeroDataReader heroDataReader = new HeroDataReader(_dataFile);

            Assert.AreEqual(Localization.KOKR, heroDataReader.Localization);
            Assert.IsTrue(heroDataReader.JsonDataDocument.RootElement.TryGetProperty("Abathur", out JsonElement _));
        }

        [TestMethod]
        [TestCategory("DataReader")]
        public void DataReaderFileLocaleTest()
        {
            using HeroDataReader heroDataReader = new HeroDataReader(_dataFile, Localization.FRFR);

            Assert.AreEqual(Localization.FRFR, heroDataReader.Localization);
            Assert.IsTrue(heroDataReader.JsonDataDocument.RootElement.TryGetProperty("Abathur", out JsonElement _));
        }

        [TestMethod]
        [TestCategory("DataReader")]
        public void DataReaderRomLocaleTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using HeroDataReader heroDataReader = new HeroDataReader(bytes, Localization.FRFR);

            Assert.AreEqual(Localization.FRFR, heroDataReader.Localization);
            Assert.IsTrue(heroDataReader.JsonDataDocument.RootElement.TryGetProperty("Abathur", out JsonElement _));
        }

        [TestMethod]
        [TestCategory("DataReader")]
        public void DataReaderFileGSRTest()
        {
            using GameStringReader gameStringReader = new GameStringReader(_jsonGameStringFileFRFR);
            using HeroDataReader heroDataReader = new HeroDataReader(_dataFile, gameStringReader);

            Assert.AreEqual(Localization.FRFR, heroDataReader.Localization);
            Assert.IsTrue(heroDataReader.TryGetHeroById("Abathur", out Hero _, false, false, false, false));
        }

        [TestMethod]
        [TestCategory("DataReader")]
        public void DataReaderROMGSRTest()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Abathur");
            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.Flush();

            byte[] bytes = memoryStream.ToArray();

            using GameStringReader gameStringReader = new GameStringReader(_jsonGameStringFileKOKR);
            using HeroDataReader heroDataReader = new HeroDataReader(bytes, gameStringReader);

            Assert.AreEqual(Localization.KOKR, heroDataReader.Localization);
            Assert.IsTrue(heroDataReader.TryGetHeroById("Abathur", out Hero _, false, false, false, false));
        }

        private void BasicRagnarosAsserts(Hero hero)
        {
            Assert.AreEqual("Ragnaros", hero.CHeroId);
            Assert.AreEqual("Ragnaros", hero.Name);
            Assert.AreEqual("HeroRagnaros", hero.CUnitId);

            Assert.AreEqual("Ragnaros", hero.HyperlinkId);
            Assert.AreEqual("Ragn", hero.AttributeId);
            Assert.AreEqual("Medium", hero.Difficulty);
            Assert.AreEqual(HeroFranchise.Unknown, hero.Franchise);
            Assert.AreEqual(UnitGender.Neutral, hero.Gender);
            Assert.AreEqual(Rarity.Unknown, hero.Rarity);
        }

        private byte[] LoadJsonTestData()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);
            writer.WriteStartObject();

            writer.WriteStartObject("Alarak");
            writer.WriteString("name", "Alarak");
            writer.WriteString("unitId", "HeroAlarak");
            writer.WriteString("hyperlinkId", "Alarak");
            writer.WriteString("attributeId", "Alar");
            writer.WriteString("difficulty", "Hard");
            writer.WriteString("franchise", "Starcraft");
            writer.WriteString("gender", "Male");
            writer.WriteString("title", "Highlord of the Tal'darim");
            writer.WriteNumber("innerRadius", 0.8125);
            writer.WriteNumber("radius", 0.8125);
            writer.WriteString("releaseDate", "2016-09-13");
            writer.WriteNumber("sight", 12.0);
            writer.WriteNumber("speed", 4.8398);
            writer.WriteString("type", "Melee");
            writer.WriteString("rarity", "Epic");
            writer.WriteString("scalingLinkId", "ExcellentMana");
            writer.WriteString("searchText", "Alarak Ascendant Protoss SC SC2 StarCraft Star2 Starcraft2 II 2 Legacy of the Void LotV Covert Ops CO");
            writer.WriteString("description", "A combo Assassin that can move enemies around and punish mistakes.");

            writer.WriteStartArray("descriptors");
            writer.WriteStringValue("EnergyImportant");
            writer.WriteStringValue("Escaper");
            writer.WriteEndArray();

            writer.WriteStartArray("units");
            writer.WriteStringValue("AbathurEvolvedMonstrosity");
            writer.WriteStringValue("AbathurLocustAssaultStrain");
            writer.WriteEndArray();

            writer.WriteStartObject("portraits");
            writer.WriteString("heroSelect", "storm_ui_ingame_heroselect_btn_alarak.png");
            writer.WriteString("leaderboard", "storm_ui_ingame_hero_leaderboard_alarak.png");
            writer.WriteString("loading", "storm_ui_ingame_hero_loadingscreen_alarak.png");
            writer.WriteString("partyPanel", "storm_ui_ingame_partypanel_btn_alarak.png");
            writer.WriteString("target", "ui_targetportrait_hero_alarak.png");
            writer.WriteString("draftScreen", "storm_ui_glues_draft_portrait_alarak.png");
            writer.WriteStartArray("partyFrames");
            writer.WriteStringValue("storm_ui_ingame_partyframe_alarak.png");
            writer.WriteEndArray();
            writer.WriteString("minimap", "storm_ui_minimapicon_alarak.png");
            writer.WriteString("targetInfo", "storm_ui_ingame_partyframe_alarak.png");
            writer.WriteEndObject();

            writer.WriteStartObject("life");
            writer.WriteNumber("amount", 755);
            writer.WriteNumber("scale", 0.04);
            writer.WriteString("type", "Life");
            writer.WriteNumber("regenRate", 5.4256);
            writer.WriteNumber("regenScale", 0.04);
            writer.WriteEndObject();

            writer.WriteStartObject("shield");
            writer.WriteNumber("amount", 755);
            writer.WriteNumber("scale", 0.04);
            writer.WriteString("type", "Shield");
            writer.WriteNumber("regenDelay", 5);
            writer.WriteNumber("regenRate", 5.245);
            writer.WriteNumber("regenScale", 0.04);
            writer.WriteEndObject();

            writer.WriteStartObject("energy");
            writer.WriteNumber("amount", 755);
            writer.WriteString("type", "Ammo");
            writer.WriteNumber("regenRate", 5.4256);
            writer.WriteEndObject();

            writer.WriteStartObject("armor");
            writer.WriteStartObject("structure");
            writer.WriteNumber("basic", 30);
            writer.WriteNumber("ability", 20);
            writer.WriteNumber("splash", 30);
            writer.WriteEndObject();
            writer.WriteStartObject("hero");
            writer.WriteNumber("basic", 10);
            writer.WriteNumber("ability", 0);
            writer.WriteNumber("splash", 0);
            writer.WriteEndObject();
            writer.WriteEndObject();

            writer.WriteStartArray("roles");
            writer.WriteStringValue("Assassin");
            writer.WriteEndArray();
            writer.WriteString("expandedRole", "Melee Assassin");

            writer.WriteStartObject("ratings");
            writer.WriteNumber("complexity", 8.0);
            writer.WriteNumber("damage", 7.0);
            writer.WriteNumber("survivability", 6.0);
            writer.WriteNumber("utility", 7.0);
            writer.WriteEndObject();

            writer.WriteStartArray("weapons");
            writer.WriteStartObject();
            writer.WriteString("nameId", "AllianceSuperCavalryWeapon");
            writer.WriteNumber("range", 5);
            writer.WriteNumber("period", 3.9);
            writer.WriteNumber("damage", 454);
            writer.WriteNumber("damageScale", 0.65);
            writer.WriteStartObject("damageFactor");
            writer.WriteNumber("minion", 1.5);
            writer.WriteNumber("structure", 0.75);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteStartObject();
            writer.WriteString("nameId", "AlteracBossWeaponParent");
            writer.WriteNumber("range", 4);
            writer.WriteNumber("period", 40.1);
            writer.WriteNumber("damage", 124);
            writer.WriteNumber("damageScale", 0.65);
            writer.WriteStartObject("damageFactor");
            writer.WriteNumber("minion", 1.5);
            writer.WriteNumber("summoned", 1.0);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WriteStartObject("abilities");
            writer.WriteStartArray("heroic"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "AlarakDeadlyChargeActivate");
            writer.WriteString("buttonId", "AlarakDeadlyCharge");
            writer.WriteString("name", "Deadly Charge");
            writer.WriteString("icon", "storm_ui_icon_alarak_recklesscharge.png");
            writer.WriteNumber("toggleCooldown", 0.5);
            writer.WriteString("energyTooltip", "<s val=\"bfd4fd\" name=\"StandardTooltipDetails\">Mana: 60</s>");
            writer.WriteString("cooldownTooltip", "Cooldown: 45 seconds");
            writer.WriteString("shortTooltip", "Channel to charge a long distance");
            writer.WriteString("fullTooltip", "After channeling, Alarak charges forward dealing <c val=\"bfd4fd\">200~~0.04~~</c> damage to all enemies in his path. Distance is increased based on the amount of time channeled, up to <c val=\"bfd4fd\">1.5</c> seconds.<n/><n/>Issuing a Move order while this is channeling will cancel it at no cost. Taking damage will interrupt the channeling.");
            writer.WriteString("abilityType", "Heroic");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end heroic

            writer.WriteStartArray("trait"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "AlarakSadism");
            writer.WriteString("buttonId", "AlarakSadism");
            writer.WriteString("name", "Sadism");
            writer.WriteString("icon", "storm_ui_icon_alarak_sadism.png");
            writer.WriteString("shortTooltip", "Each point of Sadism increases Alarak's Ability damage and self-healing against enemy Heroes by <c val=\"bfd4fd\">1%</c><n/><n/><img path=\"@UI/StormTalentInTextQuestIcon\" alignment=\"uppermiddle\" color=\"B48E4C\" width=\"20\" height=\"22\"/><c val=\"e4b800\">Repeatable Quest:</c> Takedowns increase Sadism by <c val=\"bfd4fd\">3%</c>, up to <c val=\"bfd4fd\">30%</c>. Sadism gained from Takedowns is lost on death.");
            writer.WriteString("fullTooltip", "Alarak's Ability damage and self-healing are increased by <c val=\"bfd4fd\">100%</c> against enemy Heroes.<n/><n/><img path=\"@UI/StormTalentInTextQuestIcon\" alignment=\"uppermiddle\" color=\"B48E4C\" width=\"20\" height=\"22\"/><c val=\"e4b800\">Repeatable Quest:</c> Takedowns increase Sadism by <c val=\"bfd4fd\">3%</c>, up to <c val=\"bfd4fd\">30%</c>. Sadism gained from Takedowns is lost on death.");
            writer.WriteString("abilityType", "Trait");
            writer.WriteBoolean("isActive", false);
            writer.WriteBoolean("isPassive", true);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end trait

            writer.WriteStartArray("mount"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "Mount");
            writer.WriteString("buttonId", "SummonMount");
            writer.WriteString("name", "Summon Mount");
            writer.WriteString("icon", "storm_ui_icon_generic_mount.png");
            writer.WriteString("cooldownTooltip", "Cooldown: 4 seconds");
            writer.WriteString("fullTooltip", "After Channeling for <c val=\"bfd4fd\">1</c> second, gain <s val=\"bfd4fd\" name=\"StandardTooltipDetails\">30%</s> Movement Speed.");
            writer.WriteString("abilityType", "Z");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end mount

            writer.WriteStartArray("hearth"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "Hearthstone");
            writer.WriteString("buttonId", "Hearthstone");
            writer.WriteString("abilityType", "B");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end hearth

            writer.WriteStartArray("spray"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "LootSpray");
            writer.WriteString("buttonId", "LootSpray");
            writer.WriteString("abilityType", "Spray");

            writer.WriteEndObject();
            writer.WriteEndArray(); // end spray

            writer.WriteStartArray("voice"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "LootYellVoiceLine");
            writer.WriteString("buttonId", "LootYellVoiceLine");
            writer.WriteString("abilityType", "Voice");
            writer.WriteBoolean("isActive", true);
            writer.WriteBoolean("isQuest", true);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end voice

            writer.WriteStartArray("activable"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "GallTalentEyeOfKilrogg");
            writer.WriteString("buttonId", "GallEyeofKilroggHotbar");
            writer.WriteString("lifeTooltip", "sappity sap");
            writer.WriteString("abilityType", "Active");
            writer.WriteStartObject("charges");
            writer.WriteNumber("countMax", 2);
            writer.WriteNumber("countUse", 1);
            writer.WriteNumber("countStart", 2);
            writer.WriteNumber("recastCooldown", 1);
            writer.WriteBoolean("hideCount", true);
            writer.WriteEndObject();

            writer.WriteEndObject();
            writer.WriteEndArray(); // end activable

            writer.WriteStartArray("taunt"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "tauntId");
            writer.WriteString("buttonId", "tauntId");
            writer.WriteString("abilityType", "Taunt");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end taunt

            writer.WriteStartArray("dance"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "danceId");
            writer.WriteString("buttonId", "danceId");
            writer.WriteString("abilityType", "Dance");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end dance

            writer.WriteStartArray("action"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "actionId");
            writer.WriteString("buttonId", "actionId");
            writer.WriteString("abilityType", "unknown");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end action

            writer.WriteStartArray("unknown"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "unknownId");
            writer.WriteString("buttonId", "unknownId");
            writer.WriteString("abilityType", "unknown");
            writer.WriteEndObject();
            writer.WriteEndArray(); // end unknown

            writer.WriteEndObject(); // end abilities

            writer.WriteStartArray("subAbilities");
            writer.WriteStartObject();
            writer.WriteStartObject("Mount|SummonMount|Z");
            writer.WriteStartArray("mount"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "Dismount");
            writer.WriteString("buttonId", "UnsummonMount");
            writer.WriteString("name", "Unsummon Mount");
            writer.WriteString("abilityType", "Z");
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.WriteStartObject("AlarakDeadlyChargeActivate|AlarakDeadlyCharge|Heroic");
            writer.WriteStartArray("heroic"); // array start
            writer.WriteStartObject();
            writer.WriteString("nameId", "AlarakDeadlyChargeExecute");
            writer.WriteString("buttonId", "AlarakUnleashDeadlyCharge");
            writer.WriteString("name", "Unleash Deadly Charge");
            writer.WriteString("abilityType", "Heroic");
            writer.WriteEndObject();
            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.WriteEndObject();
            writer.WriteEndArray(); // end subabilities

            writer.WriteEndObject(); // end Alarak

            // ======================================================
            writer.WriteStartObject("Ragnaros"); // start Ragnaros
            writer.WriteString("name", "Ragnaros");
            writer.WriteString("unitId", "HeroRagnaros");
            writer.WriteString("hyperlinkId", "Ragnaros");
            writer.WriteString("attributeId", "Ragn");
            writer.WriteString("difficulty", "Medium");
            writer.WriteString("franchise", "asdf");
            writer.WriteString("gender", "adsf");
            writer.WriteString("rarity", "asdf");
            writer.WriteNumber("innerRadius", 0.8125);
            writer.WriteNumber("radius", 0.8125);
            writer.WriteString("releaseDate", "2016-09-13");
            writer.WriteNumber("sight", 12.0);
            writer.WriteNumber("speed", 4.8398);
            writer.WriteString("type", "Melee");

            writer.WriteStartObject("talents"); // start talents
            writer.WriteStartArray("level1"); // start level 1
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosEmpowerSulfurasSulfurasHungers");
            writer.WriteString("buttonId", "RagnarosEmpowerSulfurasSulfurasHungers");
            writer.WriteString("name", "Sulfuras Hungers");
            writer.WriteString("icon", "storm_ui_icon_ragnaros_empowersulfuras.png");
            writer.WriteString("shortTooltip", "<c val=\"e4b800\">Quest:</c> Kill Minions with Empower Sulfuras to increase its damage");
            writer.WriteString("fullTooltip", "This is a full tooltip");
            writer.WriteString("abilityType", "Q");
            writer.WriteBoolean("isQuest", true);
            writer.WriteNumber("sort", 1);
            writer.WriteStartArray("abilityTalentLinkIds");
            writer.WriteStringValue("RagnarosEmpowerSulfurasActive");
            writer.WriteStringValue("RagnarosEmpowerSulfuras");
            writer.WriteEndArray();
            writer.WriteEndObject();

            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosLivingMeteorShiftingMeteor");
            writer.WriteString("buttonId", "RagnarosLivingMeteorShiftingMeteor");
            writer.WriteString("name", "Sulfuras Hungers");
            writer.WriteString("icon", "storm_ui_icon_ragnaros_empowersulfuras.png");
            writer.WriteString("abilityType", "W");
            writer.WriteNumber("sort", 2);
            writer.WriteEndObject();

            writer.WriteEndArray(); // end level 1

            writer.WriteStartArray("level4"); // start level 4
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosCatchingFire");
            writer.WriteString("buttonId", "RagnarosCatchingFireTalent");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "Active");
            writer.WriteBoolean("isActive", true);
            writer.WriteNumber("sort", 3);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 4

            writer.WriteStartArray("level7"); // start level 7
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosEmpowerSulfurasHandOfRagnaros");
            writer.WriteString("buttonId", "RagnarosEmpowerSulfurasHandOfRagnaros");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "Q");
            writer.WriteNumber("sort", 1);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 7

            writer.WriteStartArray("level10"); // start level 10
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosSulfurasSmash");
            writer.WriteString("buttonId", "RagnarosSulfurasSmash");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "Heroic");
            writer.WriteNumber("sort", 1);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 10

            writer.WriteStartArray("level13"); // start level 13
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosEmpowerSulfurasCauterizeWounds");
            writer.WriteString("buttonId", "RagnarosEmpowerSulfurasCauterizeWounds");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "Q");
            writer.WriteNumber("sort", 1);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 13

            writer.WriteStartArray("level16"); // start level 16
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosLivingMeteorMeteorBomb");
            writer.WriteString("buttonId", "RagnarosLivingMeteorMeteorBomb");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "W");
            writer.WriteNumber("sort", 2);
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 16

            writer.WriteStartArray("level20"); // start level 20
            writer.WriteStartObject();
            writer.WriteString("nameId", "RagnarosSulfurasSmashFlamesOfSulfuron");
            writer.WriteString("buttonId", "RagnarosSulfurasSmashFlamesOfSulfuron");
            writer.WriteString("name", "name");
            writer.WriteString("icon", "temp.png");
            writer.WriteString("abilityType", "Heroic");
            writer.WriteNumber("sort", 1);
            writer.WriteStartArray("prerequisiteTalentIds");
            writer.WriteStringValue("RagnarosSulfurasSmash");
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndArray(); // end level 20

            writer.WriteEndObject(); // end talents

            writer.WriteStartArray("heroUnits");
            writer.WriteStartObject();
            writer.WriteStartObject("RagnarosBigRag");
            writer.WriteString("name", "Ragnaros");
            writer.WriteString("hyperlinkId", "RagnarosBigRag");
            writer.WriteNumber("radius", 3);
            writer.WriteNumber("sight", 12);
            writer.WriteNumber("speed", 4.8398);
            writer.WriteString("name", "Ragnaros");
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndArray();

            writer.WriteEndObject(); // end raganros

            writer.WriteEndObject();

            writer.Flush();

            return memoryStream.ToArray();
        }

        private byte[] LoadEnusLocalizedStringData()
        {
            using MemoryStream memoryStream = new MemoryStream();
            using Utf8JsonWriter writer = new Utf8JsonWriter(memoryStream);

            writer.WriteStartObject();

            writer.WriteStartObject("meta");
            writer.WriteString("locale", "enus");
            writer.WriteEndObject(); // meta

            writer.WriteStartObject("gamestrings");
            writer.WriteStartObject("abiltalent");

            writer.WriteStartObject("cooldown");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "Cooldown: 8 seconds");
            writer.WriteEndObject();

            writer.WriteStartObject("energy");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "<s val=\"bfd4fd\" name=\"StandardTooltipDetails\">Mana: 55</s>");
            writer.WriteEndObject();

            writer.WriteStartObject("full");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "After a <c val=\"bfd4fd\">0.5</c> second delay, enemies in front of Alarak take <c val=\"bfd4fd\">175~~0.04~~</c> damage and are silenced for <c val=\"bfd4fd\">1.5</c> seconds. ");
            writer.WriteEndObject();

            writer.WriteStartObject("life");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "No life");
            writer.WriteEndObject();

            writer.WriteStartObject("name");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "Discord Strike");
            writer.WriteEndObject();

            writer.WriteStartObject("short");
            writer.WriteString("AlarakDiscordStrike|AlarakDiscordStrike|Q|False", "Damage and silence enemies in an area");
            writer.WriteEndObject();

            writer.WriteEndObject(); // end abiltalent

            writer.WriteStartObject("unit");

            writer.WriteStartObject("difficulty");
            writer.WriteString("Alarak", "Hard");
            writer.WriteEndObject();

            writer.WriteStartObject("expandedrole");
            writer.WriteString("Alarak", "Melee Assassin");
            writer.WriteEndObject();

            writer.WriteStartObject("role");
            writer.WriteString("Alarak", "Assassin,Warrior");
            writer.WriteEndObject();

            writer.WriteStartObject("searchtext");
            writer.WriteString("Alarak", "Alarak Ascendant Protoss SC SC2 StarCraft Star2 Starcraft2 II 2 Legacy of the Void LotV Covert Ops CO");
            writer.WriteEndObject();

            writer.WriteStartObject("title");
            writer.WriteString("Alarak", "Highlord of the Tal'darim");
            writer.WriteEndObject();

            writer.WriteStartObject("type");
            writer.WriteString("Alarak", "Melee");
            writer.WriteEndObject();

            writer.WriteEndObject(); // unit
            writer.WriteEndObject(); // gamestrings

            writer.WriteEndObject();

            writer.Flush();

            return memoryStream.ToArray();
        }
    }
}
