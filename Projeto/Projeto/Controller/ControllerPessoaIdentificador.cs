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
    class ControllerPessoaIdentificador
    {
        public static int getSequenciaIdentificador(NpgsqlConnection conexao)
        {
            int sequencia = 1;
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "select (coalesce(max(idcodigo),0) +1) as sequencia from pessoaidentificador";
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
                comando.CommandText = "select idcodigo, " +
                                      "identificador, " +
                                      "pessoa.pescodigo as codigo_pessoa, " +
                                      "pessoa.unicodigo as codigo_universitario_pessoa, "  +
                                      "pessoa.pesnome as nome_pessoa " +
                                      "from pessoaidentificador " +
                                      "join pessoa on pessoaidentificador.pescodigo = pessoa.pescodigo ";
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
        public static DataTable getConsulta(NpgsqlConnection conexao, int campo, int tipo, String descricao)
        {
            DataTable dados = new DataTable();
            try
            {
                
                String sql = "select idcodigo," +
                             "identificador, " +
                             "pessoa.pescodigo as codigo_pessoa, " +
                             "pessoa.unicodigo as codigo_universitario_pessoa, "  +
                             "pessoa.pesnome as nome_pessoa " +
                             "from pessoaidentificador " +
                             "join pessoa on pessoaidentificador.pescodigo = pessoa.pescodigo ";
                String CampoOrdenacao = "pessoaidentificador.idcodigo";

                switch (campo)
                {
                    case 0:
                        sql += "where cast(pessoaidentificador.idcodigo as text)";
                        CampoOrdenacao = "pessoaidentificador.idcodigo";
                        break;
                    case 1:
                        sql += "where cast(pessoa.unicodigo as varchar)";
                        CampoOrdenacao = "pessoa.unicodigo";
                        break;
                    case 2:
                        sql += "where cast(pessoa.pescodigo as varchar)";
                        CampoOrdenacao = "pessoa.pescodigo";
                        break;
                    case 3:
                        sql += "where pessoaidentificador.identificador";
                        CampoOrdenacao = "pessoaidentificador.identificador";
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

        public static bool setInclui(NpgsqlConnection conexao, ModelPessoaIdentificador pessoaIdentificador)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into pessoaidentificador (idcodigo,identificador,pescodigo) values(@codigo,@identificador,@codigo_pessoa)";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoaIdentificador.codigo;
                comando.Parameters.Add("@codigo_pessoa", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoaIdentificador.codigo_pessoa;
                comando.Parameters.Add("@identificador", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pessoaIdentificador.identificador;
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

        internal static string getNomePessoa(NpgsqlConnection conexao, string identificador)
        {
            string pessoa = "Desconhecido";
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "select pesnome from pessoaidentificador " +
                                      "join pessoa on pessoaidentificador.pescodigo = pessoa.pescodigo " +
                                      "where pessoaidentificador.identificador = '"+identificador+"'";
                pessoa = Convert.ToString(comando.ExecuteScalar());
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro de SQL:" + erro.Message);
                Console.WriteLine("Erro de SQL:" + erro.Message);
            }

            return pessoa;
        }

        public static bool setAltera(NpgsqlConnection conexao, ModelPessoaIdentificador pessoaIdentificador)
        {
            bool alterou = false;
            try
            {
                String sql = "update pessoaidentificador set pescodigo = @codigo_pessoa, identificador = @identificador where idcodigo = @codigo";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoaIdentificador.codigo;
                comando.Parameters.Add("@identificador", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pessoaIdentificador.identificador;
                comando.Parameters.Add("@codigo_pessoa", NpgsqlTypes.NpgsqlDbType.Integer).Value = pessoaIdentificador.codigo_pessoa;
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
                String sql = "delete from pessoaidentificacao where idcodigo = @codigo";
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
