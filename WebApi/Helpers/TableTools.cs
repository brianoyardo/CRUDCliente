using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace WebApi.Helpers
{
    public static class TableTools
    {
        public static object DataTableToJSON(this DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    if (row[col].ToString().Trim() == String.Empty)
                    {
                        dict[col.ColumnName] = null;
                    }
                    else
                    {
                        dict[col.ColumnName] = row[col];
                    }
                }
                list.Add(dict);
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            return list;
        }

        public static List<Dictionary<string, object>> DataTableToJSONlist(this DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    if (row[col].ToString().Trim() == String.Empty)
                    {
                        dict[col.ColumnName] = null;
                    }
                    else
                    {
                        dict[col.ColumnName] = row[col];
                    }
                }
                list.Add(dict);
            }

            return list;
        }

        public static List<T> TableToList<T>(this DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = datatable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => GetObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }
        }

        public static T TableToEntidad<T>(this DataTable datatable) where T : new()
        {
            T Temp = new T();
            try
            {
                List<string> columnsNames = datatable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => GetObject<T>(row, columnsNames))[0];
                return Temp;
            }
            catch
            {
                return Temp;
            }
        }

        public static T GetObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                        {
                            value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(objProperty.PropertyType)));
                        }
                        else
                        {
                            value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, objProperty.PropertyType));
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }


        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = Props.Select(prop => prop.GetValue(item)).ToArray();
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
