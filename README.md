# PaylocityBenefitsCalculator

**How to execute**
1. Clone the repo.
2. Import Paylocity.bacpac using sql server which is included under Api\DatabaseScript.
3. Change ""ConnectionStrings": {  "DefaultConnection":" value in appsettings.Json.
      For example: "Server=localhost;Database=Paylocity;User Id=sa;password="
4. Changed target version of Dotnet to 8.0 from 6.0. Install the dotnet 8 runtime if it is not installed on the system.
   

**DB**
Normalization:
1. Employee_Job_Details table is created to manage multiple records of Salary and Jobdescription (Promotions or Salary changes). 
2. Employee_Dependent_Association table is created to manage the relationship between Employee dependent relationships.
3. Created_Date, Created_By, Last_Edit_date and Last_Edit_By columns can be used for auditing purposes. 
4. Identity columns are used as primary key in all the tables for the indexes and better performance.
5. Non-clustered Indexes are created on the frequently used columns in the where condition and also search conditions.
6. WITH(NOLOCK) in the select statements is used to prevent dead locks and improve concurrency.
7. GetAllEmployeesByPagination - This sp is created for the pagination to retrieve all the employees in real time production scenario.

   **API**
   
1. This solution is designed using SOLID design principles.
2. Request FLow for the Benefits Calculation :
Controller -> Benefit service -> Repository -> Deductions Engine 
3. All the shared code lives in the Utils.cs which is called through out the application. This class include helper methods to read column data from the database tables.
4. The controller class implements the API for Benefits calculator. The contoller uses the Benefits Service to calculate the paycheck. Benefit service uses the Repository classes to retrieve the data from SQL server.
5. Benefit Service uses Deduction Engine to calculate the deductions. Deduction engine is implemented with open-closed principle. Deduction Engine comprises of Deduction Manager and IDeduction interface. IDeduction interface is defined with Execute method and this is where deductions are calculated. Each Deduction rule class implements the IDeduction interface. Deduction Manager instantiates the derived deduction classes and calls the Execute method on them sequentially. Deduction Engine can be expanded by adding  new deduction classes implementing IDeduction interface. 
6. All the objects used by the application are defined in the Models. The models include Employee, Dependent, Relationship, EmployeeDependentAssociation and EmployeeJobDetails.
7. Repository is responsible for querying data from SQL server. Single responsibility design principle is used to design Repository. Employee repository is responsible for fetching employee data from the database. Dependents repository is responsible for fetching dependent data from the database.
8. Unit tests are included in the API tests.
   
 


