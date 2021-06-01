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
    class ControllerReconhecimento
    {
        public static DataTable getConsulta(NpgsqlConnection conexao)
        {
            DataTable dados = new DataTable();
            try
            {
                NpgsqlCommand comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "select recsequencia, " +
                                      "recdata, " +
                                      "pessoaidentificador.identificador, " +
                                      "pessoa.pescodigo as codigo_pessoa, " +
                                      "pessoa.unicodigo as codigo_universitario_pessoa, " +
                                      "pessoa.pesnome   as nome_pessoa " +
                                      "from reconhecimento " +
                                      "left join pessoaidentificador on reconhecimento.identificador = pessoaidentificador.identificador " +
                                      "left join pessoa on pessoaidentificador.pescodigo = pessoa.pescodigo ";
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
                String sql = "select recsequencia, " +
                                      "recdata, " +
                                      "pessoa.pescodigo as codigo_pessoa, " +
                                      "pessoa.unicodigo as codigo_universitario_pessoa, " +
                                      "pessoa.pesnome   as nome_pessoa " +
                                      "from reconhecimento " +
                                      "left join pessoaidentificador on reconhecimento.identificador = pessoaidentificador.identificador " +
                                      "left join pessoa on pessoaidentificador.pescodigo = pessoa.pescodigo ";

                String CampoOrdenacao = " recsequencia";
                switch (campo)
                {
                    case 0:
                        sql += "where cast(recsequencia as text)";
                        CampoOrdenacao = "recsequencia";
                        break;
                    case 1:
                        sql += "where cast(recdata as text)";
                        CampoOrdenacao = "recdata";
                        break;
                    case 2:
                        sql += "where cast(pessoaidentificador.identificador as text)";
                        CampoOrdenacao = "pessoaidentificador.identificador";
                        break;
                    case 3:
                        sql += "where cast(pessoa.pescodigo as text)";
                        CampoOrdenacao = "pessoa.pescodigo";
                        break;
                    case 4:
                        sql += "where cast(pessoa.unicodigo as text)";
                        CampoOrdenacao = "pessoa.unicodigo";
                        break;
                    case 5:
                        sql += "where pessoa.pesnome";
                        CampoOrdenacao = "pessoa.pesnome";
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

        public static bool setInclui(NpgsqlConnection conexao, ModelReconhecimento reconhecimento)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into reconhecimento(recsequencia,identificador,recdata) values(@sequencia,@identificador,@data)";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@sequencia", NpgsqlTypes.NpgsqlDbType.Integer).Value = reconhecimento.sequencia;
                comando.Parameters.Add("@data", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = reconhecimento.data;
                comando.Parameters.Add("@identificador", NpgsqlTypes.NpgsqlDbType.Varchar).Value = reconhecimento.identificador;
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

        public static bool setIncluiAsync(NpgsqlConnection conexao, string idPessoa)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into reconhecimento (recsequencia, identificador, recdata) values ((select (coalesce(max(recsequencia),0)+1) from reconhecimento),@identificador,now())";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@identificador", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idPessoa;
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
        public static bool setAltera(NpgsqlConnection conexao, ModelReconhecimento reconhecimento)
        {
            bool alterou = false;
            try
            {
                String sql = "update reconhecimento set recdata = @data, identificador = @identificador where recsequencia = @sequencia";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@codigo", NpgsqlTypes.NpgsqlDbType.Integer).Value = reconhecimento.sequencia;
                comando.Parameters.Add("@identificador", NpgsqlTypes.NpgsqlDbType.Varchar).Value = reconhecimento.identificador;
                comando.Parameters.Add("@data", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = reconhecimento.data;
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

        public static bool setExclui(NpgsqlConnection conexao, int sequencia)
        {
            bool excluiu = false;
            try
            {
                String sql = "delete from reconhecimento where recsequencia = @sequencia";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conexao);
                comando.Parameters.Add("@sequencia", NpgsqlTypes.NpgsqlDbType.Integer).Value = sequencia;
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
