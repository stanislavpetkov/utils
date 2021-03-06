using System;
using System.Globalization;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead
{
    public static class FbExt
    {

        public static float? GetFloatN(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            switch (value)
            {
                case null:
                case DBNull _:
                    return null;
                default:
                    return Convert.ToSingle(value, CultureInfo.InvariantCulture);
            }
        }

        public static short? GetInt16N(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            switch (value)
            {
                case null:
                case DBNull _:
                    return null;
                default:
                    return Convert.ToInt16(value, CultureInfo.InvariantCulture);
            }
        }

        public static int? GetInt32N(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            switch (value)
            {
                case null:
                case DBNull _:
                    return null;
                default:
                    return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
        }

        public static DateTime? GetDateTimeN(this FbDataReader reader, int i)
        {
            var value = reader.GetValue(i);
            switch (value)
            {
                case null:
                case DBNull _:
                    return null;
                default:
                    return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
            }
        }
    }

}