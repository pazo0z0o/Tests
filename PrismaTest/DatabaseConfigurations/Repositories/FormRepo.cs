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
    {//TODO: Form Repo crud


        private readonly IConfiguration _configuration;
        //injection
        public FormRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Forms entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(int ID, Forms entity)
        {
            throw new NotImplementedException();
        }
    }
}
