using System.Text;

namespace Chalk
{
    public class Output
    {
        private StringBuilder _sb = new StringBuilder();
        

        public string Green(params string[] input)
        {
            
            foreach (var i in input)
            {
                _sb.Append(Color(i, "32"));
            }

            return _sb.ToString();
        }

        private string Color(string input, string code)
        {
            return $"\u001b[{code}m{input}\u001b[0m";
        }

        public string Red(string input)
        {
            return Color(input, "31");
        }
    }
}