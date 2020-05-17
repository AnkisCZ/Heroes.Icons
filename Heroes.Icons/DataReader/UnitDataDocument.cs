﻿using Heroes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Heroes.Icons.DataReader
{
    /// <summary>
    /// Provides access to obtain unit data as well as updating localized strings.
    /// </summary>
    public class UnitDataDocument : UnitBaseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// <see cref="Localization"/> will be inferred from <paramref name="jsonDataFilePath"/>.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing the unit data.</param>
        protected UnitDataDocument(string jsonDataFilePath)
            : base(jsonDataFilePath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        protected UnitDataDocument(string jsonDataFilePath, Localization localization)
            : base(jsonDataFilePath, localization)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// </summary>
        /// <param name="jsonData">The JSON file containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        protected UnitDataDocument(ReadOnlyMemory<byte> jsonData, Localization localization)
            : base(jsonData, localization)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// The <paramref name="gameStringReader"/> overrides the <paramref name="jsonDataFilePath"/> <see cref="Localization"/>.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing the unit data.</param>
        /// <param name="gameStringReader">Instance of a <see cref="GameStringReader"/>.</param>
        protected UnitDataDocument(string jsonDataFilePath, GameStringReader gameStringReader)
            : base(jsonDataFilePath, gameStringReader)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// </summary>
        /// <param name="jsonData">The JSON file containing the unit data.</param>
        /// <param name="gameStringReader">Instance of a <see cref="GameStringReader"/>.</param>
        protected UnitDataDocument(ReadOnlyMemory<byte> jsonData, GameStringReader gameStringReader)
            : base(jsonData, gameStringReader)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitDataDocument"/> class.
        /// </summary>
        /// <param name="utf8Json">The JSON data containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        /// <param name="isAsync">Value indicating whether to parse the <paramref name="utf8Json"/> as async.</param>
        protected UnitDataDocument(Stream utf8Json, Localization localization, bool isAsync = false)
            : base(utf8Json, localization, isAsync)
        {
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing unit data.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(string jsonDataFilePath)
        {
            return new UnitDataDocument(jsonDataFilePath);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(string jsonDataFilePath, Localization localization)
        {
            return new UnitDataDocument(jsonDataFilePath, localization);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="jsonData">The JSON data containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(ReadOnlyMemory<byte> jsonData, Localization localization)
        {
            return new UnitDataDocument(jsonData, localization);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="jsonDataFilePath">The JSON file containing unit data.</param>
        /// <param name="gameStringReader">Instance of a <see cref="GameStringReader"/>.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(string jsonDataFilePath, GameStringReader gameStringReader)
        {
            return new UnitDataDocument(jsonDataFilePath, gameStringReader);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="jsonData">The JSON data containing the unit data.</param>
        /// <param name="gameStringReader">Instance of a <see cref="GameStringReader"/>.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(ReadOnlyMemory<byte> jsonData, GameStringReader gameStringReader)
        {
            return new UnitDataDocument(jsonData, gameStringReader);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="utf8Json">The JSON data containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static UnitDataDocument Parse(Stream utf8Json, Localization localization)
        {
            return new UnitDataDocument(utf8Json, localization);
        }

        /// <summary>
        /// Parses a json file as UTF-8-encoded text to allow for <see cref="Unit"/> data reading.
        /// </summary>
        /// <param name="utf8Json">The JSON data containing the unit data.</param>
        /// <param name="localization">The <see cref="Localization"/> of the file.</param>
        /// <returns>an <see cref="UnitDataDocument"/> representation of the JSON value.</returns>
        public static Task<UnitDataDocument> ParseAsync(Stream utf8Json, Localization localization)
        {
            return new UnitDataDocument(utf8Json, localization, true).InitializeParseAsync<UnitDataDocument>();
        }

        /// <summary>
        /// Gets a <see cref="Unit"/> from the unit <paramref name="id"/> property value.
        /// </summary>
        /// <param name="id">A unit id property value.</param>
        /// <param name="abilities">A value indicating whether to include ability parsing.</param>
        /// <param name="subAbilities">A value indicating whether to include sub-ability parsing.</param>
        /// <returns>a <see cref="Unit"/> object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">The <paramref name="id"/> property value was not found.</exception>
        public Unit GetUnitById(string id, bool abilities, bool subAbilities)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));

            if (TryGetUnitById(id, out Unit? value, abilities, subAbilities))
                return value;
            else
                throw new KeyNotFoundException();
        }

        /// <summary>
        /// Looks for a unit with the <paramref name="id"/> property value, returning a value that indicates whether such value exists.
        /// </summary>
        /// <param name="id">A unit id property value.</param>
        /// <param name="value">When this method returns, contains the <see cref="Unit"/> associated with the <paramref name="id"/> property value.</param>
        /// <param name="abilities">A value indicating whether to include ability parsing.</param>
        /// <param name="subAbilities">A value indicating whether to include sub-ability parsing.</param>
        /// <returns>true if the value was found; otherwise false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="id"/> is null.</exception>
        public bool TryGetUnitById(string id, [NotNullWhen(true)] out Unit? value, bool abilities, bool subAbilities)
        {
            if (id is null)
                throw new ArgumentNullException(nameof(id));

            value = null;

            if (JsonDataDocument.RootElement.TryGetProperty(id, out JsonElement element))
            {
                value = GetUnitData(id, element, abilities, subAbilities);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a <see cref="Unit"/> from the unit <paramref name="hyperlinkId"/> property value.
        /// </summary>
        /// <param name="hyperlinkId">A unit hyperlinkId property value.</param>
        /// <param name="abilities">A value indicating whether to include ability parsing.</param>
        /// <param name="subAbilities">A value indicating whether to include sub-ability parsing.</param>
        /// <returns>a <see cref="Unit"/> object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="hyperlinkId"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">The <paramref name="hyperlinkId"/> property value was not found.</exception>
        public Unit GetUnitByHyperlinkId(string hyperlinkId, bool abilities, bool subAbilities)
        {
            if (hyperlinkId is null)
                throw new ArgumentNullException(nameof(hyperlinkId));

            if (TryGetUnitByHyperlinkId(hyperlinkId, out Unit? value, abilities, subAbilities))
                return value;
            else
                throw new KeyNotFoundException();
        }

        /// <summary>
        /// Looks for a unit with the <paramref name="hyperlinkId"/> property value, returning a value that indicates whether such value exists.
        /// </summary>
        /// <param name="hyperlinkId">A unit hyperlinkId property value.</param>
        /// <param name="value">When this method returns, contains the <see cref="Unit"/> associated with the <paramref name="hyperlinkId"/> property value.</param>
        /// <param name="abilities">A value indicating whether to include ability parsing.</param>
        /// <param name="subAbilities">A value indicating whether to include sub-ability parsing.</param>
        /// <returns>true if the value was found; otherwise false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="hyperlinkId"/> is null.</exception>
        public bool TryGetUnitByHyperlinkId(string hyperlinkId, [NotNullWhen(true)] out Unit? value, bool abilities, bool subAbilities)
            => PropertyLookup("hyperlinkId", hyperlinkId, out value, abilities, subAbilities);

        /// <summary>
        /// Gets a collection of all units.
        /// </summary>
        /// <param name="abilities">A value indicating whether to include ability parsing.</param>
        /// <param name="subAbilities">A value indicating whether to include sub-ability parsing.</param>
        /// <returns>a collection of <see cref="Unit"/>s.</returns>
        public IEnumerable<Unit> GetUnits(bool abilities, bool subAbilities)
        {
            List<Unit> unitList = new List<Unit>();

            foreach (JsonProperty unit in JsonDataDocument.RootElement.EnumerateObject())
            {
                unitList.Add(GetUnitById(unit.Name, abilities, subAbilities));
            }

            return unitList;
        }

        private Unit GetUnitData(string id, JsonElement element, bool includeAbilities, bool includeSubAbilities)
        {
            Unit unit = new Unit
            {
                Id = id,
                CUnitId = id,
            };

            if (element.TryGetProperty("hyperlinkId", out JsonElement value))
                unit.HyperlinkId = value.GetString();

            int index = id.IndexOf('-', StringComparison.InvariantCultureIgnoreCase);
            if (index > -1)
            {
                unit.MapName = id.Substring(0, index);
            }

            if (element.TryGetProperty("name", out value))
                unit.Name = value.GetString();
            if (element.TryGetProperty("innerRadius", out value))
                unit.InnerRadius = value.GetDouble();
            if (element.TryGetProperty("radius", out value))
                unit.Radius = value.GetDouble();
            if (element.TryGetProperty("sight", out value))
                unit.Sight = value.GetDouble();
            if (element.TryGetProperty("speed", out value))
                unit.Speed = value.GetDouble();
            if (element.TryGetProperty("killXP", out value))
                unit.KillXP = value.GetInt32();
            if (element.TryGetProperty("damageType", out value))
                unit.DamageType = value.GetString();
            if (element.TryGetProperty("scalingLinkId", out value))
                unit.ScalingBehaviorLink = value.GetString();
            if (element.TryGetProperty("description", out value))
                unit.Description = new TooltipDescription(value.GetString(), Localization);

            if (element.TryGetProperty("descriptors", out value))
            {
                foreach (JsonElement descriptorArrayElement in value.EnumerateArray())
                    unit.HeroDescriptors.Add(descriptorArrayElement.GetString());
            }

            if (element.TryGetProperty("attributes", out value))
            {
                foreach (JsonElement attributeArrayElement in value.EnumerateArray())
                    unit.Attributes.Add(attributeArrayElement.GetString());
            }

            if (element.TryGetProperty("units", out value))
            {
                foreach (JsonElement unitArrayElement in value.EnumerateArray())
                    unit.UnitIds.Add(unitArrayElement.GetString());
            }

            // portraits
            if (element.TryGetProperty("portraits", out value))
            {
                if (value.TryGetProperty("targetInfo", out JsonElement portraitValue))
                    unit.UnitPortrait.TargetInfoPanelFileName = portraitValue.GetString();
                if (value.TryGetProperty("minimap", out portraitValue))
                    unit.UnitPortrait.MiniMapIconFileName = portraitValue.GetString();
            }

            // life
            SetUnitLife(element, unit);

            // shield
            SetUnitShield(element, unit);

            // energy
            SetUnitEnergy(element, unit);

            // armor
            SetUnitArmor(element, unit);

            // weapons
            SetUnitWeapons(element, unit);

            // abilities
            if (includeAbilities && element.TryGetProperty("abilities", out JsonElement abilities))
            {
                AddAbilities(unit, abilities);
            }

            if (includeSubAbilities && element.TryGetProperty("subAbilities", out JsonElement subAbilities))
            {
                foreach (JsonElement subAbilityArrayElement in subAbilities.EnumerateArray())
                {
                    foreach (JsonProperty subAbilityProperty in subAbilityArrayElement.EnumerateObject())
                    {
                        string parentLink = subAbilityProperty.Name;

                        AddAbilities(unit, subAbilityProperty.Value, parentLink);
                    }
                }
            }

            GameStringReader?.UpdateGameStrings(unit);

            return unit;
        }

        private bool PropertyLookup(string propertyId, string propertyValue, [NotNullWhen(true)] out Unit? value, bool abilities, bool subAbilities)
        {
            if (propertyValue is null)
                throw new ArgumentNullException(nameof(propertyValue));

            value = null;

            foreach (JsonProperty heroProperty in JsonDataDocument.RootElement.EnumerateObject())
            {
                if (heroProperty.Value.TryGetProperty(propertyId, out JsonElement nameElement) && nameElement.ValueEquals(propertyValue))
                {
                    value = GetUnitData(heroProperty.Name, heroProperty.Value, abilities, subAbilities);

                    return true;
                }
            }

            return false;
        }
    }
}
