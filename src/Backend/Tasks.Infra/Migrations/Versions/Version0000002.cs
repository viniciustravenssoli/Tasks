using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Infra.Migrations.Versions;
[Migration((long)EnumVersionsNumber.CreateTasksTables, "Cria tabela tarefas")]
public class Version0000002  : Migration
{
    public override void Down()
    {
        
    }

    public override void Up()
    {
        CreateTasksTable();
    }

    private void CreateTasksTable()
    {
        // Create "Tasks" table with default columns
        var tasksTable = BaseVersion.InsertDefaultColumns(Create.Table("Tasks"));
        tasksTable
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Description").AsString(255).NotNullable()
            .WithColumn("StartedAt").AsDateTime().Nullable()
            .WithColumn("EndedAt").AsDateTime().Nullable()
            .WithColumn("Status").AsInt16().NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable()
            .ForeignKey("FK_Tasks_Users_Id", "Users", "Id");
    }
}
