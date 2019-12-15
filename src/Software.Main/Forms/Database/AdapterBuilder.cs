using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Software.Main
{
    class AdapterBuilder
    {
        private SQLiteConnection conn;
        private string tableName;
        private DataTable SchemaTable;

        public AdapterBuilder(SQLiteConnection conn, string tableName)
        {
            this.conn = conn;
            this.tableName = tableName;
        }

        public SQLiteDataAdapter GetAdapter(string where = "")
        {
            GetSchemaTable();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter();

            string selectSql = "select * from " + tableName;
            if (!string.IsNullOrEmpty(where))
            {
                selectSql += " where " + where;
            }
            adapter.SelectCommand = conn.CreateCommand();
            adapter.SelectCommand.CommandText = selectSql;

            var Columns = SchemaTable.AsEnumerable().Select(r => r["ColumnName"].ToString()).ToArray();
            string insertSql = string.Join(",", Columns.Select(c => string.Format("@{0}", c)).ToArray()).Replace("@ID", "NULL");
            adapter.InsertCommand = conn.CreateCommand();
            adapter.InsertCommand.CommandText = string.Format("insert into {0} values({1})", tableName, insertSql);
            adapter.InsertCommand.Parameters.AddRange(GetParameters());

            var expr = Columns.Select(c => string.Format("[{0}]=@{1}", c, c)).ToArray();
            string updateSql = string.Join(",", expr.Skip(1).ToArray());
            adapter.UpdateCommand = conn.CreateCommand();
            adapter.UpdateCommand.CommandText = string.Format("update {0} set {1} where {2}", tableName, updateSql, expr[0]);
            adapter.UpdateCommand.Parameters.AddRange(GetParameters());

            adapter.DeleteCommand = conn.CreateCommand();
            adapter.DeleteCommand.CommandText = string.Format("delete from {0} where {1}", tableName, expr[0]);
            adapter.DeleteCommand.Parameters.Add(CreateParameter(SchemaTable.Rows[0]["ColumnName"].ToString(),
                SchemaTable.Rows[0]["DataType"].ToString()));

            return adapter;
        }

        private SQLiteParameter[] GetParameters()
        {
            var colNameAndType = SchemaTable.AsEnumerable().Select(r => new { name = r["ColumnName"].ToString(), type = r["DataType"].ToString() });
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            foreach (var col in colNameAndType)
            {
                var parameter = CreateParameter(col.name.Trim(), col.type.Trim());
                parameters.Add(parameter);
            }
            return parameters.ToArray();
        }

        private SQLiteParameter CreateParameter(string colname, string coltype)
        {
            DbType type = DbType.Object;
            switch (coltype)
            {
                case "System.Int32":
                    type = DbType.Int32;

                    break;
                case "System.Int64":

                    type = DbType.Int64;
                    break;
                case "System.String":

                    type = DbType.String;
                    break;
                case "System.Boolean":

                    type = DbType.Boolean;
                    break;
            }
            SQLiteParameter parameter = new SQLiteParameter(colname, type, colname);
            return parameter;
        }

        public DataTable GetSchemaTable()
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("select * from {0}  LIMIT 1", tableName);
            var ret = cmd.ExecuteReader();
            SchemaTable = ret.GetSchemaTable();
            return SchemaTable;
        }
    }
}
