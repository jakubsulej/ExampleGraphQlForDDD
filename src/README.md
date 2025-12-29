```bash
	// add new migration
	Add-Migration <YourMigrationName> -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext -OutputDir EntityFramework\Migrations
	
	// update database
	Update-Database -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext
```