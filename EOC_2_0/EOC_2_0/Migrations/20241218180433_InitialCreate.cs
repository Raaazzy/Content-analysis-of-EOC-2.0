using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EOC_2_0.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFilePath = Path.Combine("Migrations", "SQLScripts", "init.sql");
            if (File.Exists(sqlFilePath))
            {
                var sqlScript = File.ReadAllText(sqlFilePath);
                migrationBuilder.Sql(sqlScript);
            }
            else
            {
                throw new FileNotFoundException($"SQL файл {sqlFilePath} не найден.");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassifiedVerbs");

            migrationBuilder.DropTable(
                name: "NewVerbs");

            migrationBuilder.DropTable(
                name: "Nouns");
        }
    }
}
