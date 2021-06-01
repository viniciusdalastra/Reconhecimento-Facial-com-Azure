using Npgsql;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Controller
{
    class ControllerPessoa
    {
        public static int getSequenciaPessoa(NpgsqlConnection conexao)
        {
            int sequencia = 1;
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "select (coalesce(max(pescodigo),0) +1) as sequencia from pessoa";
                sequencia = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de SQL:" + erro.Message);
                Console.WriteLine("Erro de SQL:" + erro.Message);
            }

            return sequencia;
        }

        public static DataTable getConsulta(NpgsqlConnection conexao)
        {
            DataTable dados = new DataTable();
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "select * from pessoa";
                NpgsqlDataAdapter dat = new NpgsqlDataAdapter(comando);
                dat.Fill(dados);
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de SQL:" + erro.Message);
                Console.WriteLine("Erro de SQL:" + erro.Message);
            }

            return dados;
        }

        public static DataTable getConsulta(NpgsqlConnection conexao,int campo, int tipo, String descricao)
        {
            DataTable dados = new DataTable();
            try
            {
                String sql = "select * from pessoa ";
                String CampoOrdenacao = "pessoa.pesnome";

                switch (campo)
                {
                    case 0:
                        sql += "where cast(pessoa.pescodigo as text)";
                        CampoOrdenacao = "pessoa.pescodigo";
                        break;
                    case 1:
                        sql += "where pessoa.pesnome";
                        CampoOrdenacao = "pessoa.pesnome";
                        break;
                    case 2:
                        sql += "where cast(pessoa.unicodigo as text)";
                        CampoOrdenacao = "pessoa.unicodigo";
                        break;
                }
                switch (tipo)
                {
                    case 0:
                        sql += " like '%" + descricao + "%' ";
                        break;
                    case 1:
                        sql += " like '" + descricao + "%' ";
                        break;
                    case 2:
                        sql += " like '%" + descricao + "' ";
                        break;
                    case 3:
                        sql += " = '" + descricao + "' ";
                        break;
                    case 4:
                        sql += " >= '" + descricao + "' ";
                        break;
                    case 5:
                        sql += " <= '" + descricao + "' ";
                        break;
                }

                sql += " order by " + CampoOrdenacao;
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                NpgsqlDataAdapter dat = new NpgsqlDataAdapter(comando);
                dat.Fill(dados);
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de SQL:" + erro.Message);
                Console.WriteLine("Erro de SQL:" + erro.Message);
            }

            return dados;
        }

        public static bool setInclui(NpgsqlConnection conexao, ModelPessoa pessoa)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into pessoa(pescodigo,pesnome,unicodigo) values(@codigo,@nome,@codigo_universitario)";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoa.codigo;
                comando.Parameters.Add("@codigo_universitario", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoa.codigo_universitario;
                comando.Parameters.Add("@nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pessoa.nome;
                int valor = comando.ExecuteNonQuery();
                if (valor == 1)
                {
                    incluiu = true;
                }
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de SQL:" + erro.Message);
                Console.WriteLine("Erro de SQL:" + erro.Message);
            }

            return incluiu;
        }

        public static bool setAltera(NpgsqlConnection conexao, ModelPessoa pessoa)
        {
            bool alterou = false;
            try
            {
                String sql = "update pessoa set unicodigo = @codigo_universitario, pesnome = @nome where pescodigo = @codigo";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoa.codigo;
                comando.Parameters.Add("@nome", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pessoa.nome;
                comando.Parameters.Add("@codigo_universitario", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoa.codigo_universitario;

                int valor = comando.ExecuteNonQuery();
                if (valor == 1)
                {
                    alterou = true;
                }
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de sql:" + erro.Message);
                Console.WriteLine("Erro de sql:" + erro.Message);
            }

            return alterou;
        }

        public static bool setExclui(NpgsqlConnection conexao, int codigo)
        {
            bool excluiu = false;
            try
            {
                String sql = "delete from pessoa where pescodigo = @codigo";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = codigo;
                int valor = comando.ExecuteNonQuery();
                if (valor == 1)
                {
                    excluiu = true;
                }
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de sql:" + erro.Message);
                Console.WriteLine("Erro de sql:" + erro.Message);
            }
            return excluiu;
        }
    }
}
