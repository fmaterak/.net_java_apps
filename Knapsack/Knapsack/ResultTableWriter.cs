using System;
using System.Linq;



namespace Knapsack
{
    public class ResultTableWriter
    {
        public interface ILang
        {
            // Header
            string Weight(); string Value(); string DoTake();

            // Footer
            string Total(); string Taken();
        }

        public class LangEN: ILang
        {
            // Header
            public string Weight() { return "Weight"; }
            public string Value() { return "Value"; }
            public string DoTake() { return "Take?"; }

            // Footer
            public string Total() { return "Total"; }
            public string Taken() { return "Taken"; }
        }

        public class LangPL: ILang
        {
            // Header
            public string Weight() { return "Waga"; }
            public string Value() { return "Wartość"; }
            public string DoTake() { return "Dodajemy?"; }

            // Footer
            public string Total() { return "Razem"; }
            public string Taken() { return "W plecaku"; }
        }

        private static string TAKE_FLAG = "[X]";

        private readonly ILang Lang;
        private readonly int[] ColumnWidths;
        private int TotalWeight = 0, TotalValue = 0, TakenWeight = 0, TakenValue = 0;

        public ResultTableWriter(ILang lang)
        {
            Lang = lang;
            ColumnWidths = new int[] {
                Math.Max(Lang.Total().Length, Lang.Taken().Length),
                Math.Max(Lang.Weight().Length, 2),
                Math.Max(Lang.Value().Length, 2),
                Math.Max(Lang.DoTake().Length, TAKE_FLAG.Length)
            };
        }

        public void WriteHeader()
        {
            string headerContent(int colIndex, int colWidth)
            {
                // "|       | Weight | Value | Take? |"
                switch (colIndex)
                {
                    case 1:  return Lang.Weight();
                    case 2:  return Lang.Value();
                    case 3:  return Lang.DoTake();
                    default: return null;
                }
            }

            Console.WriteLine(HorizontalLine());
            Console.WriteLine(TableRow(headerContent));
            Console.WriteLine(HorizontalLine());
        }

        public void WriteRow(Item item, bool taken)
        {
            string rowContent(int colIndex, int colWidth)
            {
                // "|       |     12 |     5 |  [X]  |"
                switch (colIndex)
                {
                    case 1:  return RightAlignInt(colWidth, item.Weight);
                    case 2:  return RightAlignInt(colWidth, item.Value);
                    case 3:  return
                        taken ? new String(' ', (colWidth-TAKE_FLAG.Length)/2) + TAKE_FLAG : null;
                    default: return null;
                }
            }

            TotalValue += item.Value;
            TotalWeight += item.Weight;

            if (taken)
            {
                TakenValue += item.Value;
                TakenWeight += item.Weight;
            }

            Console.WriteLine(TableRow(rowContent));
        }

        public void WriteFooter()
        {
            string footerContent(bool total, int colIndex, int colWidth)
            {
                // "| Total |     93 |    62 |       |"
                switch (colIndex)
                {
                    case 0:  return total ? Lang.Total() : Lang.Taken();
                    case 1:  return RightAlignInt(colWidth, total ? TotalWeight : TakenWeight);
                    case 2:  return RightAlignInt(colWidth, total ? TotalValue : TakenValue);
                    default: return null;
                }
            }

            Console.WriteLine(HorizontalLine());
            Console.WriteLine(TableRow((i, w) => footerContent(true, i, w)));
            Console.WriteLine(TableRow((i, w) => footerContent(false, i, w)));
            Console.WriteLine(HorizontalLine());
        }

        private string TableRow(Func<int, int, string> columnContent)
        {
            string columnContentWrapper(int colIndex)
            {
                int colWidth = ColumnWidths[colIndex];
                string content = columnContent(colIndex, colWidth);
                if (content == null)
                    content = new String(' ', colWidth);
                else if (content.Length < colWidth)
                    content += new String(' ', colWidth - content.Length);
                else if (content.Length > colWidth)
                    content = content.Substring(0, colWidth);
                return content;
            }

            var columnStrings =
                Enumerable.Range(0, ColumnWidths.Length).Select(columnContentWrapper);
            return "| " + String.Join(" | ", columnStrings) + " |";
        }

        private string HorizontalLine()
        {
            var columnStrings = ColumnWidths.Select(width => new String('-', width));
            return "+-" + String.Join("-+-", columnStrings) + "-+";
        }

        private string RightAlignInt(int colWidth, int num)
        {
            string numStr = num.ToString();
            return new String(' ', Math.Max(0, colWidth - numStr.Length)) + numStr;
        }
    }
}
