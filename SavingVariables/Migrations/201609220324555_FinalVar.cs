namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalVar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Variables", "VarSym", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Variables", "VarSym", c => c.String(nullable: false));
        }
    }
}
