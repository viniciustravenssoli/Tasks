using FluentMigrator;

namespace Tasks.Infra.Migrations.Versions;
[Migration((long)EnumVersionsNumber.CreateUserTables, "Cria tabela usuarios ")]
public class Version0000001 : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        CreateUsersTable();
    }

    private void CreateUsersTable()
    {
        // Create "Users" table with default columns
        var usersTable = BaseVersion.InsertDefaultColumns(Create.Table("Users"));
        usersTable
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(180).NotNullable()
            .WithColumn("Password").AsString(1200).NotNullable()
            .WithColumn("Phone").AsString(14).NotNullable();
    }


}
