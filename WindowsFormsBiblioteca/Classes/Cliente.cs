using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WindowsFormsBiblioteca.Databases;
using System.Data;

namespace WindowsFormsBiblioteca.Classes
{
    public class Cliente
    {
        public class Unit
        {
            [Required(ErrorMessage = "Código do Cliente é obrigatório.")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Código do Cliente somente aceita valores numéricos.")]
            [StringLength(6, MinimumLength = 6, ErrorMessage = "Código do Cliente deve ter 6 dígitos.")]
            public string Id { get; set; }

            [Required(ErrorMessage = "Nome do Cliente é obrigatório.")]
            [StringLength(50, ErrorMessage = "Nome do Cliente deve ter no máximo 50 caracteres.")]
            public string Nome { get; set; }

            [StringLength(50, ErrorMessage = "Nome do Pai deve ter no máximo 50 caracteres.")]
            public string NomePai { get; set; }

            [Required(ErrorMessage = "Nome da Mãe é obrigatório.")]
            [StringLength(50, ErrorMessage = "Nome da Mãe deve ter no máximo 50 caracteres.")]
            public string NomeMae { get; set; }

            public int NaoTemPai { get; set; }

            [Required(ErrorMessage = "CPF é obrigatório.")]
            [RegularExpression(@"^\d+$", ErrorMessage = "CPF somente aceita valores numéricos.")]
            [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve ter 11 dígitos.")]
            public string Cpf { get; set; }

            [Required(ErrorMessage = "Genero é obrigatório.")]
            public int? Genero { get; set; } = null;

            [Required(ErrorMessage = "CEP é obrigatório.")]
            [RegularExpression(@"^\d+$", ErrorMessage = "CEP somente aceita valores numéricos.")]
            [StringLength(8, MinimumLength = 8, ErrorMessage = "CEP deve ter 8 dígitos.")]
            public string Cep { get; set; }

            [Required(ErrorMessage = "Logradouro é obrigatório.")]
            [StringLength(100, ErrorMessage = "Logradouro deve ter no máximo 100 caracteres.")]
            public string Logradouro { get; set; }

            [Required(ErrorMessage = "Complemento é obrigatório.")]
            [StringLength(100, ErrorMessage = "Complemento deve ter no máximo 100 caracteres.")]
            public string Complemento { get; set; }

            [Required(ErrorMessage = "Bairro é obrigatório.")]
            [StringLength(50, ErrorMessage = "Bairro deve ter no máximo 50 caracteres.")]
            public string Bairro { get; set; }

            [Required(ErrorMessage = "Cidade é obrigatória.")]
            [StringLength(50, ErrorMessage = "Cidade deve ter no máximo 50 caracteres.")]
            public string Cidade { get; set; }

            [Required(ErrorMessage = "Estado é obrigatório.")]
            [StringLength(50, ErrorMessage = "Estado deve ter no máximo 50 caracteres.")]
            public string Estado { get; set; }

            [Required(ErrorMessage = "Número do Telefone é obrigatório.")]
            [RegularExpression(@"^\d+$", ErrorMessage = "Número do Telefone somente aceita valores numéricos.")]
            public string Telefone { get; set; }

            public string Profissao { get; set; }

            [Required(ErrorMessage = "Renda Familiar é obrigatória.")]
            [Range(0, double.MaxValue, ErrorMessage = "Renda Familiar deve ser um valor positivo.")]
            public double? RendaFamiliar { get; set; }

            public void ValidaClasse()
            {
                ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
                List<ValidationResult> results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(this, context, results, true);

                if (isValid == false)
                {
                    StringBuilder sbrErrors = new StringBuilder();

                    foreach (var validationResult in results)
                    {
                        sbrErrors.AppendLine(validationResult.ErrorMessage);
                    }

                    throw new ValidationException(sbrErrors.ToString());
                }
            }

            public void ValidaComplemento()
            {
                if (this.NomePai == this.NomeMae)
                {
                    throw new Exception("nome do Pai e da Mãe não podem ser iguais.");
                }

                if (this.NaoTemPai == 0)
                {
                    if (this.NomePai == "")
                    {
                        throw new Exception("Nome do Pai não pode estar vazio quando a propriedade Pai Desconhecido não estiver marcada.");
                    }
                }

                bool validaCPF = Cls_Uteis.Valida(this.Cpf);

                if (validaCPF == false)
                {
                    throw new Exception("CPF inválido.");
                }
            }

            #region "CRUD do Fichario DB SQL Server Relacional"

            #region "Funções auxiliares"

            public string ToInsert()
            {
                string SQL;

                SQL = @"
                    INSERT INTO TB_Cliente
                    (
                        Id,
                        Nome,
                        NomePai,
                        NomeMae,
                        NaoTemPai,
                        Cpf,
                        Genero,
                        Cep,
                        Logradouro,
                        Complemento,
                        Bairro,
                        Cidade,
                        Estado,
                        Telefone,
                        Profissao,
                        RendaFamiliar
                    )
                    VALUES
                    (";
                SQL += "'" + this.Id + "', ";
                SQL += "'" + this.Nome + "', ";
                SQL += "'" + this.NomePai + "', ";
                SQL += "'" + this.NomeMae + "', ";
                SQL += Convert.ToString(this.NaoTemPai) + ",";
                SQL += "'" + this.Cpf + "', ";
                SQL += Convert.ToString(this.Genero) + ",";
                SQL += "'" + this.Cep + "', ";
                SQL += "'" + this.Logradouro + "', ";
                SQL += "'" + this.Complemento + "', ";
                SQL += "'" + this.Bairro + "', ";
                SQL += "'" + this.Cidade + "', ";
                SQL += "'" + this.Estado + "', ";
                SQL += "'" + this.Telefone + "', ";
                SQL += "'" + this.Profissao + "', ";
                SQL += Convert.ToString(this.RendaFamiliar);
                SQL += ");";

                return SQL;
            }

            public string ToUpdate(string Id)
            {
                string SQL;

                SQL = @"UPDATE TB_Cliente
                        SET ";
                SQL += "Id = '" + this.Id + "', ";
                SQL += "Nome = '" + this.Nome + "', ";
                SQL += "NomePai = '" + this.NomePai + "', ";
                SQL += "NomeMae = '" + this.NomeMae + "', ";
                SQL += "NaoTemPai = " + Convert.ToString(this.NaoTemPai) + ", ";
                SQL += "Cpf = '" + this.Cpf + "', ";
                SQL += "Genero = " + Convert.ToString(this.Genero) + ", ";
                SQL += "Cep = '" + this.Cep + "', ";
                SQL += "Logradouro = '" + this.Logradouro + "', ";
                SQL += "Complemento = '" + this.Complemento + "', ";
                SQL += "Bairro = '" + this.Bairro + "', ";
                SQL += "Cidade = '" + this.Cidade + "', ";
                SQL += "Estado = '" + this.Estado + "', ";
                SQL += "Telefone = '" + this.Telefone + "', ";
                SQL += "Profissao = '" + this.Profissao + "', ";
                SQL += "RendaFamiliar = " + Convert.ToDouble(this.RendaFamiliar);
                SQL += " WHERE Id = '" + this.Id + "';";

                return SQL;
            }

            public Unit DataRowToUnit(DataRow dr)
            {
                Unit u = new Unit();

                u.Id = dr["Id"].ToString();
                u.Nome = dr["Nome"].ToString();
                u.NomePai = dr["NomePai"].ToString();
                u.NomeMae = dr["NomeMae"].ToString();
                u.NaoTemPai = Convert.ToInt32(dr["NaoTemPai"]);
                u.Cpf = dr["Cpf"].ToString();
                u.Genero = Convert.ToInt32(dr["Genero"]);
                u.Cep = dr["Cep"].ToString();
                u.Logradouro = dr["Logradouro"].ToString();
                u.Complemento = dr["Complemento"].ToString();
                u.Bairro = dr["Bairro"].ToString();
                u.Cidade = dr["Cidade"].ToString();
                u.Estado = dr["Estado"].ToString();
                u.Telefone = dr["Telefone"].ToString();
                u.Profissao = dr["Profissao"].ToString();
                u.RendaFamiliar = Convert.ToDouble(dr["RendaFamiliar"]);

                return u;
            }

            #endregion

            public void IncluirFicharioSQLREL()
            {
                try
                {
                    string SQL;
                    SQL = this.ToInsert();

                    var db = new SQLServeClass();
                    db.SQLCommand(SQL);
                    db.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception("Inlucsão não permitida. Identificador: " + this.Id + ", erro: " + ex.Message);
                }
            }

            public Unit BuscarFicharioSQLREL(string Id)
            {
                try
                {
                    string SQL = "SELECT * FROM TB_Cliente WHERE Id = '" + Id + "'";

                    var db = new SQLServeClass();
                    var dt = db.SQLQuery(SQL);

                    if (dt.Rows.Count == 0)
                    {
                        db.Close();
                        throw new Exception("Identificador não existe: " + Id);
                    }
                    else
                    {
                        Unit u = this.DataRowToUnit(dt.Rows[0]);
                        return u;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao buscar o conteúdo do identificador: " + Id + ", erro: " + ex.Message);
                }
            }

            public void AlterarFicharioSQLREL()
            {
                try
                {
                    string SQL = "SELECT * FROM TB_Cliente WHERE Id = '" + Id + "'";

                    var db = new SQLServeClass();
                    var dt = db.SQLQuery(SQL);

                    if (dt.Rows.Count == 0)
                    {
                        db.Close();
                        throw new Exception("Identificador não existe: " + Id);
                    }
                    else
                    {
                        SQL = this.ToUpdate(this.Id);
                        db.SQLCommand(SQL);
                        db.Close();
                    }
                }
                catch (Exception ex) 
                { 
                    throw new Exception("Erro ao alterar o conteúdo do identificador: " + Id + ", erro: " + ex.Message);
                }
            }

            public void ApagarFicharioSQLREL(string conexao)
            {
                try
                {
                    string SQL = "SELECT * FROM TB_Cliente WHERE Id = '" + this.Id + "'";

                    var db = new SQLServeClass();
                    var dt = db.SQLQuery(SQL);

                    if (dt.Rows.Count == 0)
                    {
                        db.Close();
                        throw new Exception("Indentificador não existe: " + this.Id);
                    }
                    else
                    {
                        SQL = "DELETE FROM TB_cliente WHERE Id = '" + this.Id + "'";
                        db.SQLCommand(SQL);
                        db.Close() ;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao apagar o conteúdo do identificador: " + Id + ", erro: " + ex.Message);
                }
            }
            public List<List<string>> BuscarFicharioDBTodosSQLREL(string conexao)
            {
                List<List<string>> ListaBusca = new List<List<string>>();

                try
                {
                    var SQL = "SELECT * FROM TB_Cliente";

                    var db = new SQLServeClass();
                    var dt = db.SQLQuery(SQL);

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ListaBusca.Add(new List<string> { dt.Rows[i]["Id"].ToString(), dt.Rows[i]["Nome"].ToString() });
                    }

                    return ListaBusca;
                }
                catch (Exception ex)
                {
                    throw new Exception("Conexão com a base ocasionou um erro: " + ex.Message);
                }
            }

            #endregion
        }

        public class List
        {
            public List<Unit> ListUnit { get; set; }
        }

        public static Unit DesSerializedClassUnit(string vJson)
        {
            return JsonConvert.DeserializeObject<Unit>(vJson);
        }

        public static string SerializedClassUnit(Unit unit)
        {
            return JsonConvert.SerializeObject(unit);
        }
    }
}
