﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    [Collection("1")]
    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {

        }

        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);
            // when
            await companyService.AddCompany(companyDto);
            // then
            Assert.Equal(1, context.Companies.Count());

        }

        [Fact]
        public async Task Should_delete_company_by_id_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);
            var id = await companyService.AddCompany(companyDto);
            Assert.Equal(1, context.Companies.Count());
            // when
            await companyService.DeleteCompany(id);
            // then
            Assert.Equal(0, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_by_id_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);
            var id = await companyService.AddCompany(companyDto);
            // when
            var getCompanyDto = await companyService.GetById(id);
            // then
            Assert.Equal(companyDto.Name, getCompanyDto.Name);
            Assert.Equal(companyDto.EmployeeDtos[0].Name, getCompanyDto.EmployeeDtos[0].Name);
            Assert.Equal(companyDto.EmployeeDtos[0].Age, getCompanyDto.EmployeeDtos[0].Age);
            Assert.Equal(companyDto.ProfileDto.RegisteredCapital, getCompanyDto.ProfileDto.RegisteredCapital);
            Assert.Equal(companyDto.ProfileDto.CertId, getCompanyDto.ProfileDto.CertId);
        }

        [Fact]
        public async Task Should_get_all_company_success_via_company_service()
        {
            // given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyDto companyDto1 = new CompanyDto();
            companyDto1.Name = "Intel";
            companyDto1.EmployeeDtos = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Alex",
                    Age = 59,
                },
            };
            companyDto1.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100011,
                CertId = "101",
            };
            CompanyService companyService = new CompanyService(context);
            await companyService.AddCompany(companyDto);
            await companyService.AddCompany(companyDto1);
            // when
            var geCompanyDtos= await companyService.GetAll();
            // then
            Assert.Equal(2, geCompanyDtos.Count());

        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            CompanyDbContext context = scopeService.GetRequiredService<CompanyDbContext>();
            return context;
        }
    }
}
