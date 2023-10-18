﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Prj_Capa_Entidad;
using System.Windows.Forms;


namespace Prj_Capa_Datos
{
   public  class BD_usuario : Cls_Conexion 
    {
        public bool BD_Verificar_Acceso(string Usuario, string Contraseña)
        {
            bool functionReturnValue = false;
            Int32 xfil = 0;

            SqlConnection Cn = new SqlConnection();
            SqlCommand Cmd = new SqlCommand ();
            Cn.ConnectionString = Conectar();

            var _With1 = Cmd;

            _With1.CommandText = "Sp_Login";
            _With1.Connection = Cn;
            _With1.CommandTimeout = 20;
            _With1.CommandType = CommandType.StoredProcedure;
            //Parametros de entrada
            _With1.Parameters.AddWithValue("@Usuario",Usuario);
            _With1.Parameters.AddWithValue("@Contraseña", Contraseña);
            try
            {
                Cn.Open();
                xfil = (Int32)Cmd.ExecuteScalar();
                if (xfil > 0) 
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
                Cmd.Parameters.Clear();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
            }
        
            catch (Exception ex)
            {
                if (Cn.State == ConnectionState.Open)
                    Cn.Close();
                Cmd.Dispose();
                Cmd = null;
                Cn.Close();
                Cn = null;
                MessageBox.Show("Algo malo paso: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return functionReturnValue;
        }


        public DataTable BD_Leer_Datos_Usuario(string Usuario)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                SqlDataAdapter da = new SqlDataAdapter("Sp_Usuario_Login", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Usuario", Usuario);
                DataTable dato = new DataTable();
                da.Fill(dato);
                da = null;
                return dato;

            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Close();
                cn = null;
                MessageBox.Show("Algo malo paso " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return null;
        }



    }
}
