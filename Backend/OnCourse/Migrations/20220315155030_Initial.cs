using Microsoft.EntityFrameworkCore.Migrations;

namespace OnCourse.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(128)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "varchar(32)", nullable: false),
                    Password = table.Column<string>(type: "varchar(32)", nullable: false),
                    Role = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "Id", "Duration", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 45, (byte)2, "C#: Primeiros Passos" },
                    { 2, 15, (byte)2, "C#: Eventos, Delegates e Lambda" },
                    { 3, 12, (byte)2, "Entity Framework Core: Banco de Dados de forma eficiente" },
                    { 4, 25, (byte)2, "Microsserviços na prática: Entendendo a tomada de decisões" },
                    { 5, 20, (byte)1, "Amazon Code Deploy: Deploy Continuo com AWS" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "dsteixeira", "pass1&", (byte)2 },
                    { 2, "rwsilva", "pass2&", (byte)3 },
                    { 3, "mcmorais", "pass3&", (byte)1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
