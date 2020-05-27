namespace FantasyEuroleague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FirstName], [LastName], [DateOfBirth], [IdentityPapersNumber], [PrimaryResidence], [Street], [Town], [PostalCode], [Discriminator]) VALUES (N'6f7b17e0-d483-4988-bb5a-4afac13c7c92', N'admin@draftheroes.gr', 0, N'AJQT/Ok4LeokA+fOu82hgE/o9B+2voEb22MEACoIFnoeZewEO9CekvP5BWtNlNNHoQ==', N'e855e680-67d1-42dc-9eec-5fa90301c023', NULL, 0, 0, NULL, 1, 0, N'admin@draftheroes.gr', N'Admin', N'DraftHeroes', CAST(N'2000-01-01T00:00:00.000' AS DateTime), N'$$100', N'Ohio', N'74, Pivet Drive', N'Missouri', 15483, N'UserAccount')
                INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6f7b17e0-d483-4988-bb5a-4afac13c7c92', N'd4e8af4a-8e71-4e3e-88c5-a698fdbfd252')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
