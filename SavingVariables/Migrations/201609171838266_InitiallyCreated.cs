namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitiallyCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        VariableId = c.Int(nullable: false, identity: true),
                        VarSym = c.String(nullable: false),
                        Val = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VariableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Variables");
        }
    }
}
