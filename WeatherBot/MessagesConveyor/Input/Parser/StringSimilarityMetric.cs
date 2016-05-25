///
/// Please Don't use ReSharper on this source file! Thanks. - Art.Stea1th.
///

namespace WeatherBot.MessagesConveyor.Input.Parser {

    internal sealed class StringSimilarityMetric {

        /// <summary>Damerau-Levenshtein Algorithm, 20-30x Faster that classical C# implementation
        /// 
        /// author:            Steve Hatchett
        /// http://stackoverflow.com/users/834261/hatchet
        /// original post:     Optimizing the Damerau-Levenshtein Algorithm in C#
        /// http://blog.softwx.net/2015/01/optimizing-damerau-levenshtein_15.html
        /// rewriter:          Art.Stea1th.
        /// 
        /// Only cosmetic changes to improve code readability, the algorithm is not affected. </summary>
        /// 
        /// <param name="searchLine">String being compared for distance.</param>
        /// <param name="targetLine">String being compared against other string.</param>
        /// 
        /// <returns>
        /// int edit distance, >= 0 representing the number of edits required to transform one string to the other.
        /// </returns>
        public int DamerauLevenshtein(string searchLine, string targetLine) {

            if (string.IsNullOrEmpty(searchLine)) return (targetLine ?? "").Length;
            if (string.IsNullOrEmpty(targetLine)) return searchLine.Length;

            if (searchLine.Length > targetLine.Length) {
                var temp = searchLine; searchLine = targetLine; targetLine = temp;
            }

            int searchLength = searchLine.Length;
            int targetLength = targetLine.Length;

            while ((searchLength > 0) && (searchLine[searchLength - 1] == targetLine[targetLength - 1])) {
                searchLength--; targetLength--;
            }

            int start = 0;

            if ((searchLine[0] == targetLine[0]) || (searchLength == 0)) {

                while ((start < searchLength) && (searchLine[start] == targetLine[start]))
                    start++;

                searchLength -= start;
                targetLength -= start;

                if (searchLength == 0)
                    return targetLength;

                targetLine = targetLine.Substring(start, targetLength);
            }

            var v0 = new int[targetLength];
            var v2 = new int[targetLength];

            for (int j = 0; j < targetLength; j++) v0[j] = j + 1;

            char searchChar = searchLine[0];
            int distance = 0;

            for (int i = 0; i < searchLength; i++) {

                char prevSearchChar = searchChar;
                searchChar = searchLine[start + i];
                char targetChar = targetLine[0];
                int left = i;
                distance = i + 1;
                int nextTransCost = 0;

                for (int j = 0; j < targetLength; j++) {

                    int above = distance;
                    int thisTransCost = nextTransCost;

                    nextTransCost = v2[j];
                    v2[j] = distance = left;
                    left = v0[j];
                    char prevTargetChar = targetChar;
                    targetChar = targetLine[j];

                    if (searchChar != targetChar) {

                        if (left < distance) distance = left;
                        if (above < distance) distance = above;
                        distance++;

                        if ((i != 0) && (j != 0) && (searchChar == prevTargetChar) && (prevSearchChar == targetChar)) {
                            thisTransCost++;
                            if (thisTransCost < distance) distance = thisTransCost;
                        }
                    }
                    v0[j] = distance;
                }
            }
            return distance;
        }
    }
}