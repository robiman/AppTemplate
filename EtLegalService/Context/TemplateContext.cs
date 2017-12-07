using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EtLegalService.Models.Master;
using EtLegalService.Models.Operational;
using EtLegalService.Models;
//using ECASDAL.Models.Members;


namespace EtLegalService.Context
{
    public class EtLegalContext : DbContext
    {
        public EtLegalContext()
            : base("name=EtLegalContext")
        {
        }
        public static EtLegalContext Create()
        {
            return new EtLegalContext();
        }

       // public DbSet<Employee> Employees { get; set; }

        public DbSet<ClaimTypeBO> claimtype { get; set; }
        public DbSet<CaseTypeBO> casetype { get; set; }
        public DbSet<ClaimCategoryBO> claimcategory { get; set; }
        public DbSet<CaseCategoryBO> casecategory { get; set; }
        public DbSet<DestinationBO> destination { get; set; }
        public DbSet<CourtNameBO> courtname { get; set; }
        public DbSet<EtLawyerBO> etlawyer { get; set; }
        public DbSet<CaseTypeCategoryBO> casetypecategory { get; set; }
        public DbSet<FollowUpBO> folowup { get; set; } 
        public DbSet<ClaimBO> claimbo { get; set; }
        public DbSet<ClaimFollowUpBO> claimfollowup { get; set; }
        public DbSet<CourtCaseBO> courtcase { get; set; }
        public DbSet<ReasonForAppointmentBO> reasonbo { get; set; }
        public DbSet<CurrencyBO> Currency { get; set; }
        public DbSet<CourtLevelBO> courtLevel { get; set; }
    }
}