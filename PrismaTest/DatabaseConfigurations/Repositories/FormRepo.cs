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
    public class FormRepo : IEntityRepo<Forms>
    {
        private readonly IConfiguration _configuration;
        //injection
        public FormRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Forms entity)
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
                //created adapter and command to pass to the adapter
                SqlDataAdapter formAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(
                    "Insert Into Forms(Title,Description,DateOfCreation,LastUpdated)" +
                    "Values(@title,@Description,@DateOfCreation,@LastUpdated)", connection);
                formAdapter.InsertCommand = command; //I am unsure about how I would use the adapter here tbh

                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@DateOfCreation", entity.DateOfCreation);
                command.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);

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

                SqlCommand command = new SqlCommand(
                    "Delete From Forms Where Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", ID);

                command.ExecuteNonQuery();
            }
        }

        public IList<Forms> GetAll()
        {
            List<Forms> FormList = new List<Forms>();

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
                //created adapter and command to pass to the adapter
                SqlDataAdapter formAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(
                    "Select * Forms;", connection);
                formAdapter.SelectCommand = command;

                DataTable formTable = new DataTable("Forms");
                formAdapter.Fill(formTable);
                if (formTable.Rows.Count > 0)
                {
                    for (int i = 0; i < formTable.Rows.Count; i++)
                    {
                        Forms form = new Forms();
                        form.Id = Convert.ToInt32(formTable.Rows[i]["Id"]);
                        form.Title = Convert.ToString(formTable.Rows[i]["Title"]);
                        form.Description = Convert.ToString((formTable.Rows[i]["Description"]));
                        form.DateOfCreation = Convert.ToDateTime((formTable.Rows[i]["DateOfCreation"]));
                        form.LastUpdated = Convert.ToDateTime((formTable.Rows[i]["LastUpdated"]));

                        FormList.Add(form);
                    }
                }
            }
            return FormList;
        }

        public Forms? GetById(int ID)
        {
            Forms form = null;
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

                SqlDataAdapter formAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("Select * From Forms Where Id=@Id", connection);
                command.Parameters.AddWithValue("@Id", ID);
                formAdapter.SelectCommand = command;

                DataTable formTable = new DataTable("Forms");
                formAdapter.Fill(formTable);

                if (formTable.Rows.Count > 0)
                {
                    form = new Forms();
                    form.Id = Convert.ToInt32(formTable.Rows[0]["Id"]);
                    form.Title = Convert.ToString(formTable.Rows[0]["Title"]);
                    form.Description = Convert.ToString((formTable.Rows[0]["Description"]));
                    form.DateOfCreation = Convert.ToDateTime((formTable.Rows[0]["DateOfCreation"]));
                    form.LastUpdated = Convert.ToDateTime((formTable.Rows[0]["LastUpdated"]));
                }
                return form;
            }
        }
        public void Update(int ID, Forms entity)
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
                //created adapter and command to pass to the adapter
                SqlDataAdapter formAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(
                    "UPDATE Forms SET Title=@Title, Description=@Description, " +
                    "DateOfCreation=@DateOfCreation, LastUpdated=@LastUpdated " +
                    "WHERE Id=@Id", connection);

                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@DateOfCreation", entity.DateOfCreation);
                command.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);

                command.ExecuteNonQuery();
            }
        }
    }
}
