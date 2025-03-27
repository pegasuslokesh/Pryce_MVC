using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pryce_MVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Module_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SubModuleId = table.Column<int>(type: "int", nullable: true),
                    Module_Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterModules_MasterModules_SubModuleId",
                        column: x => x.SubModuleId,
                        principalTable: "MasterModules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ObjectModules",
                columns: table => new
                {
                    ObjectModule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObjectModule_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ObjectModule_Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Module_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectModules", x => x.ObjectModule_id);
                    table.ForeignKey(
                        name: "FK_ObjectModules_MasterModules_Module_id",
                        column: x => x.Module_id,
                        principalTable: "MasterModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterModules_SubModuleId",
                table: "MasterModules",
                column: "SubModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectModules_Module_id",
                table: "ObjectModules",
                column: "Module_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectModules");

            migrationBuilder.DropTable(
                name: "MasterModules");
        }
    }
}
