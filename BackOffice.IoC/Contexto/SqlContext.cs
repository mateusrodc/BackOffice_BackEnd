using BackOffice.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Data.Contexto
{
    public class SqlContext : DbContext
    {
        public SqlContext() { }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Endereco> Endereco { get; set; }


        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        public IDbConnection CreateConnection()
        {
            string connectionString = configuration?.GetSection("Dev").Value ?? string.Empty;
            return new SqlConnection(connectionString);
        }
    }
}
