#Ef migration

```bash
	// add new migration
	Add-Migration <YourMigrationName> -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext -OutputDir EntityFramework\Migrations
	
	// update database
	Update-Database -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext
```

#Example queries

##GetServiceOffersPage
```
query GetServiceOffersPage($page: Int!, $pageSize: Int!) {
  serviceOffersPage(page: $page, pageSize: $pageSize) {
    serviceOffers {
      id,
      description,
      createdAt,
      cleaner {
        name
        description,
        offeredServices {
          title
          description
        }
      }
    }
  }
}
```

or

##GetCleanersPage
```
query GetCleanersPage($page: Int!, $pageSize: Int!) {
  cleanersPage(page: $page, pageSize: $pageSize) {
    cleaners {
      id,
      description,
      createdAt,
      offeredServices {
        id,
        title,
        description,
      }
    },
    totalCount
  }
}
```
