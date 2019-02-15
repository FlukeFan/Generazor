namespace DALGenerator
{
    public static class StringUtil
    {
        public static string ToCsName(this string source)
        {
            var csName = char.ToUpper(source[0]) + source.Substring(1);

            while (csName.Contains('_'))
            {
                var index = csName.IndexOf('_');
                csName = csName.Substring(0, index) + char.ToUpper(csName[index + 1]) + csName.Substring(index + 2, csName.Length - 2 - index);
            }

            return csName;
        }

        public static string ToCsNameSingular(this string source)
        {
            var csName = source.ToCsName();

            if (csName.EndsWith('s'))
                csName = csName.Substring(0, csName.Length - 1);

            return csName;
        }

        public static string ToCsType(this string type)
        {
            switch(type)
            {
                case "datetime": return "DateTime";
                case "integer": return "int";
                case "numeric": return "Decimal";
                case "nvarchar": return "string";

                default:
                    throw new System.Exception($"Type '{type}' not handled");
            }
        }
    }
}
