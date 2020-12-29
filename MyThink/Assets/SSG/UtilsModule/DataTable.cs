using System;
using System.Collections.Generic;
using UnityEngine;
namespace SSG
{
    public class DataTable
    {
        public List<List<string>> rows { get; internal set; }
        public List<string> columnNames { get; internal set; }
        public int columnNumber { get; internal set; }

        public bool GetBool(int row, int col, bool defaultValue = false)
        {
            string val = GetString(row, col, "");
            return String.IsNullOrEmpty(val) ? defaultValue : bool.Parse(val);
        }

        public int GetInt( int row, int col, int defaultValue = 0 )
        {
            string val = GetString( row, col, "" );
            return String.IsNullOrEmpty( val ) ? defaultValue : int.Parse( val );
        }

        public long GetLong( int row, int col, long defaultValue = 0 )
        {
            string val = GetString(row, col, "");
            return String.IsNullOrEmpty(val) ? defaultValue : long.Parse(val);
        }

        public string GetString( int row, int col, string defaultValue = "" )
        {
            object val = rows[row][col];
            return String.IsNullOrEmpty( val.ToString() ) ? defaultValue : val.ToString();
        }

        public float GetFloat( int row, int col, float defaultValue = 0f )
        {
            object val = rows[row][col];
            if ( val == null )
                return defaultValue;
            else
            {
                float result = 0f;
                if ( float.TryParse( val.ToString(), out result ) )
                {
                    return result;
                }
            }
            return defaultValue;
        }

        public Vector2 GetVector2( int row, int col )
        {
            string val = GetString( row, col );
            if ( string.IsNullOrEmpty( val ) )
                return Vector2.zero;
            string[] vecStr = val.Split( '_' );
            if ( vecStr == null || vecStr.Length != 2 )
                return Vector2.zero;
            return new Vector2( Convert.ToSingle( vecStr[0] ), Convert.ToSingle( vecStr[1] ) );
        }

        public T GetEnum<T>( int row, int col ) where T : struct, IComparable, IConvertible, IFormattable
        {
            if ( !typeof( T ).IsEnum )
                throw new ArgumentException( "TEnum must be an enumerated type" );
            int val = GetInt( row, col );
            object enumObj = Enum.Parse( typeof( T ), val.ToString() );
            return (T)enumObj;
        }
    }
}

