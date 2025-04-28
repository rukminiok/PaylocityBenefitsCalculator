using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.DeductionEngine;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                DateOfBirth = new DateTime(1984, 12, 30),
                SalaryDetail= new List<GetEmployeeSalaryDto>() { new GetEmployeeSalaryDto(){
                JobDescription= "Junior Programmer",
                Salary= 75420.99m,
                StartDate = new DateTime( 2023, 10, 01),
                EndDate= null}
      }
},
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                DateOfBirth = new DateTime(1999, 8, 10),
                SalaryDetail= new List<GetEmployeeSalaryDto>() 
                { 
                 new GetEmployeeSalaryDto(){
                JobDescription= "Project Leader",
                Salary= 92365.22m,
                StartDate = new DateTime( 2023, 10, 01),
                EndDate= null }
                 },
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                DateOfBirth = new DateTime(1963, 2, 17),
                SalaryDetail= new List<GetEmployeeSalaryDto>()
                {
                 new GetEmployeeSalaryDto(){
                JobDescription= "Project Leader",
                Salary= 143211.12m,
                StartDate = new DateTime( 2023, 10, 01),
                EndDate= null }
                 },
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await HttpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto()
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            DateOfBirth = new DateTime(1984, 12, 30),
            SalaryDetail = new List<GetEmployeeSalaryDto>() { new GetEmployeeSalaryDto()
            {
                JobDescription= "Junior Programmer",
                Salary= 75420.99m,
                StartDate = new DateTime( 2023, 10, 01),
                EndDate= null}
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
    [Fact]
    public async Task WhenAskedFor26Paychecksforemployees_ShouldReturnAllPaychecks()
    {
        
        var response = await HttpClient.GetAsync("/api/v1/Employees/paycheck?employeeId=1&year=2024");
        var payCheckperPeriods = new GetEmployeePayCheckDto()
        {
            EmployeeId = 1,
            FirstName = "LeBron",
            LastName = "James",
            DateOfBirth = new DateTime(1984, 12, 30),
            SSN = null,
            EmploymentId = null,
            PayCheckPerPeriod = new List<GetPayCheckPerPeriodDto>()
            {
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "1/1/2024 12:00:00 AM-1/14/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "1/15/2024 12:00:00 AM-1/28/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "1/29/2024 12:00:00 AM-2/11/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "2/12/2024 12:00:00 AM-2/25/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "2/26/2024 12:00:00 AM-3/10/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "3/11/2024 12:00:00 AM-3/24/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "3/25/2024 12:00:00 AM-4/7/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "4/8/2024 12:00:00 AM-4/21/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "4/22/2024 12:00:00 AM-5/5/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "5/6/2024 12:00:00 AM-5/19/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "5/20/2024 12:00:00 AM-6/2/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "6/3/2024 12:00:00 AM-6/16/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "6/17/2024 12:00:00 AM-6/30/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "7/1/2024 12:00:00 AM-7/14/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "7/15/2024 12:00:00 AM-7/28/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "7/29/2024 12:00:00 AM-8/11/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "8/12/2024 12:00:00 AM-8/25/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "8/26/2024 12:00:00 AM-9/8/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "9/9/2024 12:00:00 AM-9/22/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "9/23/2024 12:00:00 AM-10/6/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "10/7/2024 12:00:00 AM-10/20/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "10/21/2024 12:00:00 AM-11/3/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "11/4/2024 12:00:00 AM-11/17/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "11/18/2024 12:00:00 AM-12/1/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "12/2/2024 12:00:00 AM-12/15/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
                new GetPayCheckPerPeriodDto()
                {
                    PayPeriod = "12/16/2024 12:00:00 AM-12/31/2024 12:00:00 AM",
                    BaseSalary = 2900.81m,
                    Deductions = new Dictionary<string, decimal>
                    {
                        { "EmployeeBaseDeduction", 923.08m },
                    },
                    NetSalary = 1977.73m
                },
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, payCheckperPeriods);
    }
}


