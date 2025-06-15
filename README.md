## 🚀 Run:

1. Add SQL Server connection string to user secrets

    ```shell
    dotnet user-secrets --project API set "ConnectionStrings:DefaultConnection" "<SQL Server Connection String>"
    ```

2. Install Entity Framework Core tools

    ```shell
    dotnet tool install --global dotnet-ef
    ```

3. Add migrations (if there's any)

    ```shell
    dotnet-ef migrations --project API add <Migration Name>
    ```

4. Update the database

    ```shell
    dotnet-ef database --project API update
    ```

5. Run the project

    ```shell
    dotnet run --project API
    ```

6. Open `tests.http` to test the endpoints.
