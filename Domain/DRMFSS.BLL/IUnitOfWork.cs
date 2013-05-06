using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;

namespace DRMFSS.BLL
{

		public interface IUnitOfWork
		{

           
           IAccountRepository Account { get; set; }
                   
           IAdjustmentRepository Adjustment { get; set; }
                   
           IAdjustmentReasonRepository AdjustmentReason { get; set; }
                   
           IAdminUnitRepository AdminUnit { get; set; }
                   
           IAdminUnitTypeRepository AdminUnitType { get; set; }
                   
           IAuditRepository Audit { get; set; }
                   
           ICommodityRepository Commodity { get; set; }
                   
           ICommodityGradeRepository CommodityGrade { get; set; }
                   
           ICommoditySourceRepository CommoditySource { get; set; }
                   
           ICommodityTypeRepository CommodityType { get; set; }
                   
           IContactRepository Contact { get; set; }
                   
           IDetailRepository Detail { get; set; }
                   
           IDispatchRepository Dispatch { get; set; }
                   
           IDispatchAllocationRepository DispatchAllocation { get; set; }
                   
           IDispatchDetailRepository DispatchDetail { get; set; }
                   
           IDonorRepository Donor { get; set; }
                   
           IFDPRepository FDP { get; set; }
                   
           IForgetPasswordRequestRepository ForgetPasswordRequest { get; set; }
                   
           IGiftCertificateRepository GiftCertificate { get; set; }
                   
           IGiftCertificateDetailRepository GiftCertificateDetail { get; set; }
                   
           IHubRepository Hub { get; set; }
                   
           IHubOwnerRepository HubOwner { get; set; }
                   
           IHubSettingRepository HubSetting { get; set; }
                   
           IHubSettingValueRepository HubSettingValue { get; set; }
                   
           IInternalMovementRepository InternalMovement { get; set; }
                   
           ILedgerRepository Ledger { get; set; }
                   
           ILedgerTypeRepository LedgerType { get; set; }
                   
           ILetterTemplateRepository LetterTemplate { get; set; }
                   
           IMasterRepository Master { get; set; }
                   
           IOtherDispatchAllocationRepository OtherDispatchAllocation { get; set; }
                   
           IPartitionRepository Partition { get; set; }
                   
           IPeriodRepository Period { get; set; }
                   
           IProgramRepository Program { get; set; }
                   
           IProjectCodeRepository ProjectCode { get; set; }
                   
           IReceiptAllocationRepository ReceiptAllocation { get; set; }
                   
           IReceiveRepository Receive { get; set; }
                   
           IReceiveDetailRepository ReceiveDetail { get; set; }
                   
           IReleaseNoteRepository ReleaseNote { get; set; }
                   
           IRoleRepository Role { get; set; }
                   
           ISessionAttemptRepository SessionAttempt { get; set; }
                   
           ISessionHistoryRepository SessionHistory { get; set; }
                   
           ISettingRepository Setting { get; set; }
                   
           IShippingInstructionRepository ShippingInstruction { get; set; }
                   
           ISMSRepository SMS { get; set; }
                   
           IStackEventRepository StackEvent { get; set; }
                   
           IStackEventTypeRepository StackEventType { get; set; }
                   
           IStoreRepository Store { get; set; }
                   
           ITransactionRepository Transaction { get; set; }
                   
           ITransactionGroupRepository TransactionGroup { get; set; }
                   
           ITranslationRepository Translation { get; set; }
                   
           ITransporterRepository Transporter { get; set; }
                   
           IUnitRepository Unit { get; set; }
                   
           IUserHubRepository UserHub { get; set; }
                   
           IUserProfileRepository UserProfile { get; set; }
                   
           IUserRoleRepository UserRole { get; set; }
        	   }
}


