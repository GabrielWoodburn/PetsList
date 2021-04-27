using Microsoft.EntityFrameworkCore.Migrations;

namespace PetList.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    ClassificationId = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.ClassificationId);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    ClassificationId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "ClassificationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PetOwners",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOwners", x => new { x.PetId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_PetOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetOwners_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classifications",
                columns: new[] { "ClassificationId", "Name" },
                values: new object[,]
                {
                    { "dog", "Dog" },
                    { "cat", "Cat" },
                    { "bird", "Bird" },
                    { "elephant", "Elephant" },
                    { "kangaroo", "Kangaroo" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Sam", "Smith" },
                    { 2, "Calvin", "John" },
                    { 3, "Luke", "Jackson" },
                    { 4, "Jerry", "Springer" },
                    { 5, "Jack", "Smithson" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "ClassificationId", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Cat", "Kumar", 12.1 },
                    { 2, "Dog", "Max", 7.2000000000000002 },
                    { 3, "Bird", "Suzie", 0.80000000000000004 },
                    { 4, "Dog", "Mimi", 55.5 },
                    { 5, "Dog", "Fluffy", 22.100000000000001 }
                });

            migrationBuilder.InsertData(
                table: "PetOwners",
                columns: new[] { "PetId", "OwnerId" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 2, 33 },
                    { 3, 18 },
                    { 4, 55 },
                    { 5, 99 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetOwners_OwnerId",
                table: "PetOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_ClassificationId",
                table: "Pets",
                column: "ClassificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetOwners");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Classifications");
        }
    }
}
