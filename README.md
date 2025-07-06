# Time Spent on Task: 6h

# YAML File

Docker Compose file from: [https://hub.docker.com/_/postgres](https://hub.docker.com/_/postgres)

1. Run `docker compose up`.
2. Go to [http://localhost:8080](http://localhost:8080).
3. System: PostgreSQL. Login with credentials (`postgres`, `example`). Choose or create a database.
4. In the left menu, click **SQL command**.
5. Paste the scripts and execute.

Alternatively you could use pgAdmin.

# SQL Files

1. **migration** – Contains schema migration.
2. **mock_data_generation** – Script to generate mock data.
3. **queries** – Exercise queries with comments.

Execute these files in the order listed above.

# Rest API
Project based on Clean Architecture.

Core – Application logic layer.
Infrastructure – Communication with external systems.
DTOs – Data Transfer Objects; can be packaged as a NuGet package for sharing across teams.
Adform – Application layer.


API version: 1

1. Set connection string in <code>appsettings.json</code>.
2. Launch application.
3. Navigate to [https://localhost:port/swagger/index.html](https://localhost:port/swagger/index.html)

TODO: 
1. Add unit tests.

## Improvements:
Enhanced infrastructure layer, including:
- Error handling
- SQL query building