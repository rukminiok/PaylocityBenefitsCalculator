# PaylocityBenefitsCalculator

**Prerequites**
1. Import Paylocity.bacpac using sql server which is included under Api\DatabaseScript.
2. Change ""ConnectionStrings": {  "DefaultConnection":" value in appsettings.Json.
      For example: "Server=localhost;Database=Paylocity;User Id=sa;password="
3. Changed target version of Dotnet to 8.0 from 6.0

**DB**
Normalization:
1. Employee_Job_Details table is created to manage multiple records of Salary and Jobdescription (Promotions or Salary changes). 
2. Employee_Dependent_Association table is created to manage the relationship between Employee dependent relationships.
3. Created_Date, Created_By, Last_Edit_date and Last_Edit_By can be used for auditing purposes. Still we can to have an Audit to manage all the changes made to an emplyee.
4. Identity columns are used as primary key in all the tables for the indexes and better performance.
5. Non-clustered Indexes are created on the frequently used columns in the where condition and also search conditions.
6. WITH(NOLOCK) in the select statements is used to prevent dead locks and improve concurrency.

   **API**
   
1. Used Utils.cs to write the common methods called through out the application.
2. Created Rule classes for each deduction. Used dictionary object to itemize the deductions for each paycheck period. Data methods are also included here, but can be written seperately.
3. Separated service layer and repository layer to Business logic code can be reused in other parts of the application.
4. Repositories and controllers are separated from service layer helped in the unit-test the business logic code.
5. IDeduction interface is used and inherited in all the Deductions rules.


