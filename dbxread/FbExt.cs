using System;
using System.Globalization;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread
{
    public static class FbExt
    {

        public static float? GetFloatN(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            if (value == null) return null;
            if (value is DBNull)
            {
                return null;
            }

            return Convert.ToSingle(value, CultureInfo.InvariantCulture);
        }

        public static short? GetInt16N(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            if (value == null) return null;
            if (value is DBNull)
            {
                return null;
            }

            return Convert.ToInt16(value, CultureInfo.InvariantCulture);
        }

        public static int? GetInt32N(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            if (value == null) return null;
            if (value is DBNull)
            {
                return null;
            }

            return Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        public static DateTime? GetDateTimeN(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            if (value == null) return null;
            if (value is DBNull)
            {
                return null;
            }

            return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
        }
    }

}