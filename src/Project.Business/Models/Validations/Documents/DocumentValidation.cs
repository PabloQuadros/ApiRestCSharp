namespace Project.Business.Models.Validations.Documents;

    public class CpfValidation
    {
        public const int CpfLength = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = Utils.OnlyNumbers(cpf);

            if (!ValidLength(cpfNumbers)) return false;
            return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
        }

        private static bool ValidLength(string value)
        {
            return value.Length == CpfLength;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string value)
        {
            var number = value.Substring(0, CpfLength - 2);
            var verifyDigit = new VerifyingDigit(number)
                .ComMultiplicadoresDeAte(2, 11)
                .Replacing("0", 10, 11);
            var firstDigit = verifyDigit.CalculateDigit();
            verifyDigit.AddDigit(firstDigit);
            var secondDigit = verifyDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == value.Substring(CpfLength - 2, 2);
        }
    }

    public class CnpjValidation
    {
        public const int CnpjLength = 14;

        public static bool Validate(string cpnj)
        {
            var cnpjNumbers = Utils.OnlyNumbers(cpnj);

            if (!HasValidLength(cnpjNumbers)) return false;
            return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
        }

        private static bool HasValidLength(string value)
        {
            return value.Length == CnpjLength;
        }

        private static bool HasRepeatedDigits(string value)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
            return invalidNumbers.Contains(value);
        }

        private static bool HasValidDigits(string valor)
        {
            var number = valor.Substring(0, CnpjLength - 2);

            var verifyDigit = new VerifyingDigit(number)
                .ComMultiplicadoresDeAte(2, 9)
                .Replacing("0", 10, 11);
            var firstDigit = verifyDigit.CalculateDigit();
            verifyDigit.AddDigit(firstDigit);
            var secondDigit = verifyDigit.CalculateDigit();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CnpjLength - 2, 2);
        }
    }

    public class VerifyingDigit
    {
        private string _number;
        private const int Module = 11;
        private readonly List<int> _multipliers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substitues = new Dictionary<int, string>();
        private bool _complementaryModule = true;

        public VerifyingDigit(string number)
        {
            _number = number;
        }

        public VerifyingDigit ComMultiplicadoresDeAte(int firstMultiplier, int lastMultiplier)
        {
            _multipliers.Clear();
            for (var i = firstMultiplier; i <= lastMultiplier; i++)
                _multipliers.Add(i);

            return this;
        }

        public VerifyingDigit Replacing(string substitute, params int[] digits)
        {
            foreach (var i in digits)
            {
                _substitues[i] = substitute;
            }
            return this;
        }

        public void AddDigit(string digit)
        {
            _number = string.Concat(_number, digit);
        }

        public string CalculateDigit()
        {
            return !(_number.Length > 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var sum = 0;
            for (int i = _number.Length - 1, m = 0; i >= 0; i--)
            {
                var product = (int)char.GetNumericValue(_number[i]) * _multipliers[m];
                sum += product;

                if (++m >= _multipliers.Count) m = 0;
            }

            var mod = (sum % Module);
            var result = _complementaryModule ? Module - mod : mod;

            return _substitues.ContainsKey(result) ? _substitues[result] : result.ToString();
        }
    }

    public class Utils
    {
        public static string OnlyNumbers(string value)
        {
            var onlyNumber = "";
            foreach (var s in value)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }

