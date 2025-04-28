using Api.Controllers;
using Api.DeductionEngine;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Repository;

namespace Api.Services
{
    public class BenefitService
    {
        private DependentRepository _dependantRepository;
        private EmployeeDependentRelationshipRepository _employeeDependentRelationship;
        private EmployeeRepository _employeeRepository;
        private IConfiguration _configuration;
        private DeductionManager _manager;
        public BenefitService(IConfiguration configuration, DependentRepository dependantRepository,
         EmployeeDependentRelationshipRepository employeeDependentRelationship, EmployeeRepository employeeRepository,
            DeductionManager manager)
        {
            _configuration = configuration;
            _dependantRepository = dependantRepository;
            _employeeRepository = employeeRepository;
            _employeeDependentRelationship = employeeDependentRelationship;
            _manager = manager;
        }
        /// <summary>
        /// This returns list of all the dependents
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetDependentDto>> GetAllDependents()
        {
            try 
            { 
            List<Dependent> dependents = await _dependantRepository.GetAllDependents();

            List<GetDependentDto> dependentDtos = dependents.Select(e => new GetDependentDto
            {
                Id = e.DependentId,
                DateOfBirth = e.DateOfBirth,
                Relationship = e.Relationship,
                FirstName = e.FirstName,
                LastName = e.LastName,
            }).ToList();

            return dependentDtos;
            }
            catch (Exception ex)
            {
                //can log the exception here
                return new List<GetDependentDto>();
            }
        }
        /// <summary>
        /// This returns dependent basic info for the given dependent
        /// </summary>
        /// <param name="dependentId"></param>
        /// <returns></returns>
        public async Task<GetDependentDto> GetDependentById(Int64 dependentId)
        {
            try
            {
                Dependent dependent = await _dependantRepository.GetDependent(dependentId);

                GetDependentDto dependentDto = new GetDependentDto()
                {
                    Id = dependent.DependentId,
                    DateOfBirth = dependent.DateOfBirth,
                    Relationship = dependent.Relationship,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                };


                return dependentDto;
            }
            catch (Exception ex)
            {
                //can log the exception here
                return new GetDependentDto();
            }
        }
        /// <summary>
        /// This returns Employee information with Job details including Salary details
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            try
            { 
            List<GetEmployeeDto> employeeDtos = new List<GetEmployeeDto>();

            List<Employee> employees = await _employeeRepository.GetEmployees();

            List<Dependent> dependents = new List<Dependent>();

            foreach (var employee in employees)
            {
                dependents = await _employeeDependentRelationship.GetDependents(employee.Id);

                List<GetDependentDto> dependentDtos = dependents.Select(e => new GetDependentDto
                {
                    Id = e.DependentId,
                    DateOfBirth = e.DateOfBirth,
                    Relationship = e.Relationship,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                }).ToList();

                List<EmployeeJobDetail> jobdetails = await _employeeRepository.GetEmplopyeeJobDetails(employee.Id);


                List<GetEmployeeSalaryDto> employeeSalaryDtos = jobdetails.Select(e => new GetEmployeeSalaryDto
                {
                    JobDescription = e.JobDescription,
                    Salary = e.BasicSalary,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                })
                .ToList();

                employeeDtos.Add(new GetEmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    Dependents = dependentDtos,
                    SalaryDetail = employeeSalaryDtos

                });
            }

            return employeeDtos;
            }
            catch (Exception ex)
            {
                //can log the exception here
                return new List<GetEmployeeDto>();
            }
        }
        /// <summary>
        /// This returns all the information for the given employee includes job details, dependents, relationship
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<GetEmployeeDto> GetEmployeeById(Int64 employeeId)
        {
            try
            { 
            GetEmployeeDto employeeDto;

            Employee employee = await _employeeRepository.GetEmployee(employeeId);

            List<Dependent> dependents = new List<Dependent>();

            dependents = await _employeeDependentRelationship.GetDependents(employeeId);
            //mapping
            List<GetDependentDto> dependentDtos = dependents.Select(e => new GetDependentDto
            {
                Id = e.DependentId,
                DateOfBirth = e.DateOfBirth,
                Relationship = e.Relationship,
                FirstName = e.FirstName,
                LastName = e.LastName,
            }).ToList();

            List<EmployeeJobDetail> jobdetails = await _employeeRepository.GetEmplopyeeJobDetails(employee.Id);


            List<GetEmployeeSalaryDto> employeeSalaryDtos = jobdetails.Select(e => new GetEmployeeSalaryDto
            {
                JobDescription = e.JobDescription,
                Salary = e.BasicSalary,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            })
            .ToList();

            employeeDto = new GetEmployeeDto()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Dependents = dependentDtos,
                SalaryDetail = employeeSalaryDtos

            };

            return employeeDto;
            }
            catch (Exception ex)
            {
                //can log the exception here
                return new GetEmployeeDto();
            }
        }

        /// <summary>
        /// This is where the paycheck calculation starts, Such as getting 26 pay checks for the given pay period and loop through each pay period to calculate the paycheck
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<GetEmployeePayCheckDto> GetEmployeePayChecks(Int64 employeeId, int year)
        {
            try 
            { 
            var employeeDetails = await this.GetEmployeeById(employeeId);
            
            GetEmployeePayCheckDto getEmployeePayCheckDto = new GetEmployeePayCheckDto();

            getEmployeePayCheckDto.DateOfBirth = employeeDetails.DateOfBirth;
            getEmployeePayCheckDto.FirstName = employeeDetails.FirstName;
            getEmployeePayCheckDto.LastName = employeeDetails.LastName;
            getEmployeePayCheckDto.EmployeeId = employeeDetails.Id;

            var payPeriods = Utils.GetPayPeriods(year); 

            List<GetPayCheckPerPeriodDto> payChecksPerPeriod = new List<GetPayCheckPerPeriodDto>();

            foreach (var payPeriod in payPeriods)
            {
                GetPayCheckPerPeriodDto payCheckPerPeriod = new GetPayCheckPerPeriodDto();
                payCheckPerPeriod.PayPeriod = $"{payPeriod.Value.startDate.ToString()}-{payPeriod.Value.endDate.ToString()}";
                
                await CalculateDeductions(employeeDetails, payPeriod.Value.startDate, payPeriod.Value.endDate, payCheckPerPeriod);

                payChecksPerPeriod.Add(payCheckPerPeriod);
            }

            getEmployeePayCheckDto.PayCheckPerPeriod = payChecksPerPeriod;

            return getEmployeePayCheckDto;

            }
            catch (Exception ex)
            {
                //can log the exception here
                return new GetEmployeePayCheckDto();
            }
        }
        /// <summary>
        /// Calculating deduction per each pay period
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <param name="payPeriodStartDate"></param>
        /// <param name="payPeriodEndDate"></param>
        /// <param name="payCheckPerPeriod"></param>
        /// <returns></returns>
        public async Task CalculateDeductions(GetEmployeeDto employeeDetails, DateTime payPeriodStartDate, DateTime payPeriodEndDate, GetPayCheckPerPeriodDto payCheckPerPeriod)
        {
            //set base salary for that payperiod
            try
            {
                payCheckPerPeriod.BaseSalary = employeeDetails.SalaryDetail.Where(sd => sd.StartDate <= payPeriodStartDate &&
                                 (!sd.EndDate.HasValue || sd.EndDate >= payPeriodEndDate))
                    .Select(sd => sd.Salary)
                    .FirstOrDefault();

                payCheckPerPeriod.BaseSalary = Math.Round ( payCheckPerPeriod.BaseSalary / 26,2);

                await _manager.RunDeductions(employeeDetails, payPeriodStartDate, payPeriodEndDate, payCheckPerPeriod);
            }
            catch (Exception ex) { 
                //log the exception
            }

        }
    }
}
