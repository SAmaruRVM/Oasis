using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oasis.Dados.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assunto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmailContactante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataContacto = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Distrito = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactoTelefonico = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CssClassIcone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LinkCdn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposUtilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUtilizador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RespostasContactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resposta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasContactos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasContactos_Contactos_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Contactos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConteudoPaginaPrincipalEscolas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConteudoHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConteudoPaginaPrincipalEscolas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConteudoPaginaPrincipalEscolas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagemPerfil = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DescricaoPerfil = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DataUltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilizadores_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Utilizadores_Temas_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Temas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_TiposUtilizador_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TiposUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Utilizadores_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Utilizadores_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_TiposUtilizador_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TiposUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Utilizadores_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Utilizadores_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    FoiVista = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AlunoId = table.Column<int>(type: "int", nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Utilizadores_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Utilizadores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequisicaoEquipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRequisicao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DataEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaAprovado = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisicaoEquipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisicaoEquipamentos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisicaoEquipamentos_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Banner = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupos_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupos_Utilizadores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GruposAlunos",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposAlunos", x => new { x.ApplicationUserId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_GruposAlunos_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GruposAlunos_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TiposPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposPosts_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Ficheiro = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TipoPostId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_TiposPosts_TipoPostId",
                        column: x => x.TipoPostId,
                        principalTable: "TiposPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComentariosPostsUtilizadores",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentariosPostsUtilizadores", x => new { x.ApplicationUserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_ComentariosPostsUtilizadores_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComentariosPostsUtilizadores_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostsGostosUtilizadores",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ReacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsGostosUtilizadores", x => new { x.ApplicationUserId, x.PostId, x.ReacaoId });
                    table.ForeignKey(
                        name: "FK_PostsGostosUtilizadores_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostsGostosUtilizadores_Reacoes_ReacaoId",
                        column: x => x.ReacaoId,
                        principalTable: "Reacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostsGostosUtilizadores_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostsUtilizadoresGuardados",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsUtilizadoresGuardados", x => new { x.ApplicationUserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostsUtilizadoresGuardados_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostsUtilizadoresGuardados_Utilizadores_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Temas",
                columns: new[] { "Id", "LinkCdn", "Nome" },
                values: new object[,]
                {
                    { 1, "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/yeti/bootstrap.min.css", "Yeti" },
                    { 2, "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/darkly/bootstrap.min.css", "Darkly" },
                    { 3, "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/minty/bootstrap.min.css", "Minty" },
                    { 4, "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/sketchy/bootstrap.min.css", "Sketchy" },
                    { 5, "https://cdn.jsdelivr.net/npm/bootswatch@4.5.2/dist/sandstone/bootstrap.min.css", "Sandstone" }
                });

            migrationBuilder.InsertData(
                table: "TiposUtilizador",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "87d8aee9-b7d6-4637-a61b-4897e275839f", "Administrador", "ADMINISTRADOR" },
                    { 2, "b4e8a415-6218-4809-bb21-4b9f5bf2e873", "Diretor", "DIRETOR" },
                    { 3, "a0adfa42-2f1d-4391-92b4-2a09076095b2", "Professor", "PROFESSOR" },
                    { 4, "849109e4-06d8-41b3-87de-39a9aecebc37", "Aluno", "ALUNO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ComentariosPostsUtilizadores_PostId",
                table: "ComentariosPostsUtilizadores",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ConteudoPaginaPrincipalEscolas_EscolaId",
                table: "ConteudoPaginaPrincipalEscolas",
                column: "EscolaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ApplicationUserId",
                table: "Disciplinas",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_EscolaId",
                table: "Disciplinas",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_EscolaId",
                table: "Equipamentos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_DisciplinaId",
                table: "Grupos",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_ProfessorId",
                table: "Grupos",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposAlunos_GrupoId",
                table: "GruposAlunos",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_ApplicationUserId",
                table: "Notificacoes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TipoPostId",
                table: "Posts",
                column: "TipoPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsGostosUtilizadores_PostId",
                table: "PostsGostosUtilizadores",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsGostosUtilizadores_ReacaoId",
                table: "PostsGostosUtilizadores",
                column: "ReacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PostsUtilizadoresGuardados_PostId",
                table: "PostsUtilizadoresGuardados",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AlunoId",
                table: "Reports",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProfessorId",
                table: "Reports",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamentos_ApplicationUserId",
                table: "RequisicaoEquipamentos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisicaoEquipamentos_EquipamentoId",
                table: "RequisicaoEquipamentos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasContactos_ContactoId",
                table: "RespostasContactos",
                column: "ContactoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposPosts_GrupoId",
                table: "TiposPosts",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TiposUtilizador",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Utilizadores",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_EscolaId",
                table: "Utilizadores",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_TemaId",
                table: "Utilizadores",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Utilizadores",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ComentariosPostsUtilizadores");

            migrationBuilder.DropTable(
                name: "ConteudoPaginaPrincipalEscolas");

            migrationBuilder.DropTable(
                name: "GruposAlunos");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "PostsGostosUtilizadores");

            migrationBuilder.DropTable(
                name: "PostsUtilizadoresGuardados");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RequisicaoEquipamentos");

            migrationBuilder.DropTable(
                name: "RespostasContactos");

            migrationBuilder.DropTable(
                name: "TiposUtilizador");

            migrationBuilder.DropTable(
                name: "Reacoes");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "TiposPosts");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Escolas");

            migrationBuilder.DropTable(
                name: "Temas");
        }
    }
}
