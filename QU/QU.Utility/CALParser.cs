using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QU.Utility
{
    /// <summary>
    /// Parse CAL output.
    /// </summary>
    public class CALParser
    {
        private const char SeparatorDollar = '$';
        private const char SeparatorEqualTo = '=';
        private const char SeparatorDoubleQuote = '"';
        private const char SeparatorWhitespace = ' ';

        private const string RawQuerySpellCorrectionsBeginMarker = "word:(";
        private const string RawQuerySpellCorrectionsEndMarker = ")";

        private const string CustomAugmentationKeyWordRankAlt = "rankalt";
        private const string CustomAugmentationKeyWordAlterationCandidateStartPattern = "a=";

        // Set of all expansions
        readonly List<List<string>> wordBagList = new List<List<string>>();

        // Adds a text to the wordbag of expansions.
        // E.g., For a given text "a b c", a list containing {a, b, c} is generated and added
        // to the wordbag
        public static void AddTextToWordBagList(List<List<string>> wordBagList, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            // Strip any surrounding double quotes in the input string, so that the tokens added to the wordbag are 
            // clean text.
            string cleansedText = text.Replace(SeparatorDoubleQuote, SeparatorWhitespace).Trim();

            // Tokenize the input string and add to the expansions list.
            if (!String.IsNullOrWhiteSpace(cleansedText) && cleansedText.Length > 1)
            {
                var tokens =
                    cleansedText.Split(new char[] { SeparatorWhitespace }, StringSplitOptions.RemoveEmptyEntries);

                wordBagList.Add(tokens.ToList());
            }
        }

        // Adds the tokens of a text to the wordbag of expansions
        // E.g., For a given text "a b c", three lists each containing one element are added to the 
        public static void AddTextTokensToWordBagList(List<List<string>> wordBagList, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            // Strip any surrounding double quotes in the input string, so that the tokens added to the wordbag are 
            // clean text.
            string cleansedText = text.Replace(SeparatorDoubleQuote, SeparatorWhitespace).Trim();

            // Tokenize the query and add tokens to the expansions list.
            if (cleansedText.Length > 1)
            {
                var tokens =
                    cleansedText.Split(new char[] { SeparatorWhitespace }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var token in tokens)
                {
                    wordBagList.Add(new List<string> { token });
                }
            }
        }

        private void ParseAlterationQuery(CalInterface.AlterationQuery alterationQuery)
        {
            if (!string.IsNullOrEmpty(alterationQuery.RawQuery))
            {
                ParseRawQueryForSpellCorrections(alterationQuery.RawQuery);
            }

            if (!String.IsNullOrEmpty(alterationQuery.CustomAugmentation))
            {
                ParseCustomAugmentations(alterationQuery.CustomAugmentation);
            }
        }

        private void ParseCustomAugmentations(string customAugmentation)
        {
            var alterations =
                    customAugmentation.Split(new[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrEmpty(x) 
                            && !string.IsNullOrWhiteSpace(x) && x.StartsWith(CustomAugmentationKeyWordRankAlt));

            // Each alteration in the list above is of the form
            // "rankalt$w=<queryword>$a=<expansion>$s=<score>"
            // E.g.
            // "rankalt$w=restaurant$a=restaurants$s=98925064"
            // We are only interested in the a=<expansion> part so parse that.

            foreach (var alteration in alterations)
            {
                var alterationFields =
                    alteration.Split(new[] { SeparatorDollar }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim());

                if (!alterationFields.Any() ||
                    (alterationFields.Count() != 4) ||
                    !alterationFields.ElementAt(2).StartsWith(CustomAugmentationKeyWordAlterationCandidateStartPattern))
                {
                    // Looks like a malformed alteration response. Continue parsing the rest of the alterations.
                    continue;
                }

                var alterationValues =
                    alterationFields.ElementAt(2)
                        .Split(new[] { SeparatorEqualTo }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim());

                if (!alterationValues.Any() || (alterationValues.Count() != 2))
                {
                    // Looks like a malformed alteration value. Continue parsing the rest of the alteration values.
                    continue;
                }

                // We have the correct alteration. Add it to the wordbag.
                AddTextToWordBagList(wordBagList, alterationValues.ElementAt(1));
            }
        }

        // This method parses the spell corrections embedded in the Raw Query 
        // Examples of the raw query with spell corrections in the alteration response
        // RawQuery = "nibbana restaurant official word:(website site web)";
        // RawQuery = "word:(walterdrake \"walter drake\")";
        private void ParseRawQueryForSpellCorrections(string rawQuery)
        {
            for (int i = 0; i < rawQuery.Length; i++)
            {
                int spellingCorrectionsBeginIndex = rawQuery.IndexOf(RawQuerySpellCorrectionsBeginMarker, i, System.StringComparison.Ordinal);

                if (spellingCorrectionsBeginIndex < 0)
                {
                    break;
                }

                int spellingCorrectionsEndIndex = rawQuery.IndexOf(RawQuerySpellCorrectionsEndMarker,
                    spellingCorrectionsBeginIndex + RawQuerySpellCorrectionsBeginMarker.Length,
                    System.StringComparison.Ordinal);

                if (spellingCorrectionsEndIndex < 0)
                {
                    break;
                }

                string spellCorrectionsCompoundString = rawQuery.Substring(spellingCorrectionsBeginIndex + RawQuerySpellCorrectionsBeginMarker.Length,
                    spellingCorrectionsEndIndex - (spellingCorrectionsBeginIndex + RawQuerySpellCorrectionsBeginMarker.Length));

                // Given the examples above, spellCorrectionsCompoundString can be "website site web" as well as "walterdrake \"walter drake\""
                // For the second example, we need to parse the enclosed string in quotes and treat as separate correction.
                var spellCorrectionsList =
                    spellCorrectionsCompoundString.Split(new[] { SeparatorDoubleQuote }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x));

                foreach (var spellCorrection in spellCorrectionsList)
                {
                    AddTextToWordBagList(wordBagList, spellCorrection); // For cases like discovercard, the spell correction comes as "discover card"
                    AddTextTokensToWordBagList(wordBagList, spellCorrection);
                }

                i = spellingCorrectionsEndIndex;
            }
        }


        // Extract query expansions from the alteration response and update the wordbag.
        private void GenerateQueryExpansionsFromQueryAlterations(CalInterface.AlterationResponse alterationResponse)
        {
            if (alterationResponse.AlterationQueryList != null)
            {
                foreach (var alterationQuery in alterationResponse.AlterationQueryList)
                {
                    ParseAlterationQuery(alterationQuery);
                }
            }

            // Store the raw query variants if available
            if (alterationResponse.RawQueryVariantList != null)
            {
                foreach (var rawQueryVariant in alterationResponse.RawQueryVariantList)
                {
                    AddTextToWordBagList(wordBagList, rawQueryVariant.RawQuery);

                    // Add raw query variant tokens to the wordbag.
                    AddTextTokensToWordBagList(wordBagList, rawQueryVariant.RawQuery);
                }
            }
        }
    }
}
