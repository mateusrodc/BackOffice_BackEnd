using BackOffice.Application.Interface;
using BackOffice.Application.Interface.Departamento;
using BackOffice.Application.Interface.Endereco;
using BackOffice.Application.Interface.Pessoa;
using BackOffice.Application.Interface.Usuario;
using BackOffice.Application.Service;
using BackOffice.Application.Service.Departamentos;
using BackOffice.Application.Service.Endereco;
using BackOffice.Application.Service.Pessoa;
using BackOffice.Application.Service.Usuario;
using BackOffice.Data.Contexto;
using BackOffice.Data.Repositories.Departamento;
using BackOffice.Data.Repositories.Endereco;
using BackOffice.Data.Repositories.Pessoa;
using BackOffice.Data.Repositories.Usuario;
using BackOffice.Domain.InterfaceRepository.Departamento;
using BackOffice.Domain.InterfaceRepository.Endereco;
using BackOffice.Domain.InterfaceRepository.Pessoa;
using BackOffice.Domain.InterfaceRepository.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Informe o token Bearer no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});



var key = Encoding.ASCII.GetBytes(Settings.Secret);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Administrador");
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddScoped<ICadastroPessoaApplication, CadastroPessoaApplication>();
builder.Services.AddScoped<ICadastroPessoaRepository, CadastroPessoaRepository>();

builder.Services.AddScoped<IListarPessoaApplication, ListarPessoaApplication>();
builder.Services.AddScoped<IListarPessoaRepository, ListarPessoaRepository>();

builder.Services.AddScoped<ICadastroUsuarioApplication, CadastroUsuarioApplication>();
builder.Services.AddScoped<ICadastroUsuarioRepository, CadastroUsuarioRepository>();

builder.Services.AddScoped<IBuscarUsuarioRepository, BuscarUsuarioRepository>();

builder.Services.AddScoped<ILoginApplication, LoginApplication>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IEnderecoApplication, EnderecoApplication>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddScoped<IBuscaDocumentoRepository, BuscaDocumentoRepository>();

builder.Services.AddScoped<IListarUsuariosApplication, ListarUsuariosApplication>();
builder.Services.AddScoped<IListarUsuariosRepository, ListarUsuariosRepository>();

builder.Services.AddScoped<IAtualizarUsuarioApplication, AtualizarUsuarioApplication>();
builder.Services.AddScoped<IAtualizarUsuarioRepository, AtualizarUsuarioRepository>();

builder.Services.AddScoped<IListarDepartamentos, ListarDepartamentos>();
builder.Services.AddScoped<IListarDepartamentosRepository, ListarDepartamentosRepository>();

builder.Services.AddScoped<IAdicionarDepartamentoApplication, AdicionarDepartamentoApplication>();
builder.Services.AddScoped<IAlterarDepartamentoApplication, AlterarDepartamentoApplication>();
builder.Services.AddScoped<IDeletarDepartamentoApplication, DeletarDepartamentoApplication>();

builder.Services.AddScoped<IAdicionarDepartamentoRepository, AdicionarDepartamentoRepository>();
builder.Services.AddScoped<IAlterarDepartamentoRepository, AlterarDepartamentoRepository>();
builder.Services.AddScoped<IDeletarDepartamentoRepository, DeletarDepartamentoRepository>();

builder.Services.AddScoped<IBuscaPessoaColaboradorRepository, BuscaPessoaColaboradorRepository>();
builder.Services.AddScoped<IBuscarDepartamentoRepository, BuscarDepartamentoRepository>();

builder.Services.AddScoped<IAlterarPessoaApplication, AlterarPessoaApplication>();
builder.Services.AddScoped<IAlterarPessoaRepository, AlterarPessoaRepository>();

builder.Services.AddScoped<SqlContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
