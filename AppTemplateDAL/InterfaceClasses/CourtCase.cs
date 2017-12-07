using System;

namespace AppTemplateDAL.InterfaceClasses
{
    public interface ICourtCase
    {
        string Name { get; set; }
        int Location { get; set; }
        DateTime DateFiled { get; set; }
        string CaseNature { get; set; }
        string CourtFileNumber { get; set; }
        string AssignedETCounsel { get; set; }
        string AssignedLocalCounsel { get; set; }
        string CaseStatus { get; set; }
        double Budget { get; set; }
        double BudgetInETB { get; set; }
        int CurrencyName { get; set; }
        string Description { get; set; }
        string Remark { get; set; }
        int CaseTypeCatagoryID { get; set; }
        string Party { get; set; }
        int CourtLevel { get; set; }
        string CourtName { get; set; }
        string CaseCreatedBy { get; set; }
        string fileAddress { get; set; }
        DateTime DateUpdated { get; set; }
        int idSquence { get; set; }

        void AddCourtCase(ICourtCase IcourtCase);
    }


    public interface IMyInterface
    {
        void SomeMethod();
        void SomeOtherMethod();
    }

    public abstract class MyClass : IMyInterface
    {
        // Really implementing this
        public void SomeMethod()
        {
            // ...
        }

        // Derived class must implement this
        public abstract void SomeOtherMethod();
    }

}