# Run instructions:
 
1. Clone the application using : git clone https://github.com/muradhossen/Todo.git
2. Run postgreSQL on docker : docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Test12$!" -e "MSSQL_PID=Evaluation" -p 11433:1433  --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2019-latest
3. And run the application. Browse https://localhost:7071/swagger/index.html to see Swagger UI.

Default user :
1. Admin : admin@demo.com / Admin123!

# Project Description:

This is a simple Todo application, allow user to assign a todo (task) to other user or the user himself.
It has implemented role base authentication where only admin user can create,update or delete the todo items and any user can view the todo items.
