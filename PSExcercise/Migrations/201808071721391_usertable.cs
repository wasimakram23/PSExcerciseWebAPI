namespace PSExcercise.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
