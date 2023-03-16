
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConfigurations.Repositories
{
    public class FieldsRepo : IEntityField<Fields>
    {
        private readonly IConfiguration _configuration;
        //injection
        public FieldsRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Fields entity, int formId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                SqlCommand command = new SqlCommand(
                    "Insert Into Fields(NoteName, Note, FormId)" +
                    "Values(@NoteName, @Note, @FormId)", connection);

                command.Parameters.AddWithValue("@NoteName", entity.NoteName);
                command.Parameters.AddWithValue("@Note", entity.Note);
                command.Parameters.AddWithValue("@FormId", formId);

                command.ExecuteNonQuery(); // number of rows affected should be 1
            }
        }

        public void Delete(int ID)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                SqlCommand command = new SqlCommand("Delete From Fields Where Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", ID);
                command.ExecuteNonQuery();
                int count = (int)command.ExecuteNonQuery();
                if (count == 0)
                {
                    throw new Exception("FormId does not exist in Forms table");
                }
            }
        }
        public IList<Fields> GetAll()
        {
            List<Fields> fieldsList = new List<Fields>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                // created adapter and command to pass to the adapter
                SqlDataAdapter fieldsAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(
                    "Select * From Fields", connection);
                fieldsAdapter.SelectCommand = command;

                DataTable fieldsTable = new DataTable("Fields");
                fieldsAdapter.Fill(fieldsTable);

                if (fieldsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < fieldsTable.Rows.Count; i++)
                    {
                        Fields field = new Fields();
                        field.Id = Convert.ToInt32(fieldsTable.Rows[i]["Id"]);
                        field.NoteName = Convert.ToString(fieldsTable.Rows[i]["NoteName"]);
                        field.Note = Convert.ToInt32(fieldsTable.Rows[i]["Note"]);
                        field.FormId = Convert.ToInt32(fieldsTable.Rows[i]["FormId"]);

                        fieldsList.Add(field);
                    }
                }
            }
            return fieldsList;
        }

        public Fields? GetById(int ID)
        {
            Fields field = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                SqlDataAdapter fieldsAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("Select * From Fields Where Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", ID);
                fieldsAdapter.SelectCommand = command;

                DataTable fieldsTable = new DataTable("Fields");
                fieldsAdapter.Fill(fieldsTable);

                if (fieldsTable.Rows.Count > 0)
                {
                    field = new Fields();
                    field.Id = Convert.ToInt32(fieldsTable.Rows[0]["Id"]);
                    field.NoteName = Convert.ToString(fieldsTable.Rows[0]["NoteName"]);
                    field.Note = Convert.ToInt32(fieldsTable.Rows[0]["Note"]);
                    field.FormId = Convert.ToInt32(fieldsTable.Rows[0]["FormId"]);
                }
            }
            return field;
        }
        public void Update(Fields entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                SqlCommand command = new SqlCommand(
                    "Update Fields Set NoteName = @NoteName, Note = @Note, FormId = @FormId " +
                    "Where Id = @id", connection);
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@NoteName", entity.NoteName);
                command.Parameters.AddWithValue("@Note", entity.Note);
                command.Parameters.AddWithValue("@FormId", entity.FormId);

                command.ExecuteNonQuery(); // number of rows affected should be 1
            }
        }
        //Get all the Fields of the same form
        public IList<Fields>? GetByFormId(int ID)
        {
            List<Fields>? formFields = new();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLConnection")))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                SqlDataAdapter fieldsAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("Select * From Fields Where FormId=@Id", connection);
                command.Parameters.AddWithValue("@Id", ID);
                fieldsAdapter.SelectCommand = command;

                DataTable fieldsTable = new DataTable("SameFormFields");
                fieldsAdapter.Fill(fieldsTable);

                if (fieldsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < fieldsTable.Rows.Count; i++)
                    {
                        Fields field = new Fields();
                        field.Id = Convert.ToInt32(fieldsTable.Rows[i]["Id"]);
                        field.NoteName = Convert.ToString(fieldsTable.Rows[i]["NoteName"]);
                        try
                        {
                            if (IsNumeric(fieldsTable.Rows[i]["Note"].ToString()))
                            {
                                field.Note = Convert.ToInt32(fieldsTable.Rows[i]["Note"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }


                        
                        field.FormId = Convert.ToInt32(fieldsTable.Rows[i]["FormId"]);

                        formFields.Add(field);
                    }
                }

            }
            return formFields;
        }

        //Validator for numeric
        public bool IsNumeric(string noteValue)
        {
            int result;
            return int.TryParse(noteValue, out result);
        }

    }
}