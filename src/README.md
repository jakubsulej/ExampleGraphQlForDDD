#Ef migration

```bash
	// add new migration
	Add-Migration <YourMigrationName> -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext -OutputDir EntityFramework\Migrations
	
	// update database
	Update-Database -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext
```

#Example query
```
	query GetServiceOffersPage($page: Int!, $pageSize: Int!) {
	  serviceOffersPage(page: $page, pageSize: $pageSize) {
		serviceOffers {
		  id
		}
	  }
	}
```
