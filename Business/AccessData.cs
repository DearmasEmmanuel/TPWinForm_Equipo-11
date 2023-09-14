﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AccessData
    {
        private readonly SqlConnection connection;
        private readonly SqlCommand command;
        private SqlDataReader reader;
        private string connectionString = "server=.\\SQLEXPRESS ;Database=CATALOGO_P3_DB;Trusted_Connection=True;";
        public SqlDataReader Reader
        {
            get { return reader; }
        }
        public AccessData()
        {
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
        }

        public void SetQuery(string query, List<SqlParameter> parameters = null)
        {
            this.command.CommandType = CommandType.Text;
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            this.command.CommandText = query;
        }

        public int GetLastId(string table)
        {
            SetQuery($"SELECT MAX(Id) FROM {table}");
            reader = command.ExecuteReader();
            reader.Read();
            return Reader.GetInt32(0);
        }

        public bool ExecuteQuery()
        {
            try
            {
                this.command.Connection = this.connection;
                this.connection.Open();
                this.reader = this.command.ExecuteReader();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ejecutarAccion()
        {
            command.Connection = this.connection;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int ExecuteNonQuery()
        {
            try
            {
                this.command.Connection = this.connection;
                this.connection.Open();
                return this.command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public void setearParametros(string nombre, object valor)
        {
            command.Parameters.AddWithValue(nombre, valor);
        }



        public void Close()
        {
            if (this.reader != null)
            {
                this.reader.Close();
            }
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }
    }
}
