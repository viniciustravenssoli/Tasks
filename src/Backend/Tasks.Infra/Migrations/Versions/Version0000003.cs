using FluentMigrator;

namespace Tasks.Infra.Migrations.Versions;
[Migration((long)EnumVersionsNumber.AlterTableTasks, "Adicionando coluna priority para o tarefa")]
public class Version0000003 : Migration
{
    public override void Down()
    {
       
    }

    public override void Up()
    {
        Alter.Table("Tasks").AddColumn("Priority").AsInt16().NotNullable();
    }
}
